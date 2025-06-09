using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SSELex.TranslateCore;
using SSELex.TranslateManagement;

namespace SSELex.TranslateManage
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/



    public class ReplaceTag
    {
        public string Key { get; set; } = "";
        public string Value { get; set; } = "";
        public ReplaceTag(string Key, string Value)
        {
            this.Key = Key;
            this.Value = Value;
        }
    }

    public class TranslationPreprocessor : TranslationPreprocessorExtend
    {
        public bool HasPlaceholder = false;
        public string SourceStr = "";

        public List<ReplaceTag> ReplaceTags = new List<ReplaceTag>();

        public TranslationPreprocessor()
        {
        
        }

        public List<string> GeneratePlaceholderTextByAI(Languages From, Languages To, string SourceStr, string Type, out bool NeedFurtherTranslate)
        {
            ReplaceTags.Clear();

            List<string> Words = new List<string>();

            var Datas = AdvancedDictionary.Query(DeFine.CurrentModName, Type, From, To, SourceStr);
            bool UseWordBoundary = LanguageExtensions.IsSpaceDelimitedLanguage(From);

            string TempStr = SourceStr;

            for (int i = 0; i < Datas.Count; i++)
            {
                var Source = Datas[i].Source;
                Words.Add($"{Source} -> {Datas[i].Result}");
                ReplaceTags.Add(new ReplaceTag(Source, Datas[i].Result));
                if (UseWordBoundary)
                {
                    string Pattern = $@"\b{Regex.Escape(Source)}\b";
                    TempStr = Regex.Replace(TempStr, Pattern, "", RegexOptions.IgnoreCase);
                }
                else
                {
                    TempStr = TempStr.Replace(Source, "");
                }
            }

            NeedFurtherTranslate = !string.IsNullOrWhiteSpace(TempStr.Trim());

            return Words;
        }

        public string GeneratePlaceholderText(Languages From, Languages To, string SourceStr, string Type, out bool NeedFurtherTranslate)
        {
            ReplaceTags.Clear();
            HasPlaceholder = false;

            bool UseWordBoundary = LanguageExtensions.IsSpaceDelimitedLanguage(From);

            var Datas = AdvancedDictionary.Query(DeFine.CurrentModName, Type, From, To, SourceStr);

            for (int i = 0; i < Datas.Count; i++)
            {
                var Word = Datas[i];
                string Placeholder = $"__({i})__";
                string Source = Word.Source;

                if (UseWordBoundary)
                {
                    string Pattern = $@"\b{Regex.Escape(Source)}\b";
                    if (Regex.IsMatch(SourceStr, Pattern, RegexOptions.IgnoreCase))
                    {
                        SourceStr = Regex.Replace(SourceStr, Pattern, Placeholder, RegexOptions.IgnoreCase);
                        ReplaceTags.Add(new ReplaceTag(Placeholder, Word.Result));
                        HasPlaceholder = true;
                    }
                }
                else
                {
                    if (SourceStr.Contains(Source))
                    {
                        SourceStr = SourceStr.Replace(Source, Placeholder);
                        ReplaceTags.Add(new ReplaceTag(Placeholder, Word.Result));
                        HasPlaceholder = true;
                    }
                }
            }

            string Residual = UseWordBoundary
                ? Regex.Replace(SourceStr, @"__\(\d+\)__", "")
                : ReplaceTags.Aggregate(SourceStr, (str, tag) => str.Replace(tag.Key, ""));

            NeedFurtherTranslate = !string.IsNullOrWhiteSpace(Residual.Trim());

            this.SourceStr = SourceStr;
            return SourceStr;
        }

        public string RestoreFromPlaceholder(string Str, Languages Lang)
        {
            if (string.IsNullOrEmpty(Str) || ReplaceTags.Count == 0)
                return Str;

            bool HasSpace = IsSpaceLanguage(Lang);

            StringBuilder Result = new StringBuilder();
            int I = 0;

            while (I < Str.Length)
            {
                if (Str[I] == '_' && I + 6 < Str.Length && Str.Substring(I, 3) == "__(")
                {
                    int Start = I;
                    int J = I + 3;

                    while (J < Str.Length && char.IsDigit(Str[J]))
                        J++;

                    if (J < Str.Length - 2 && Str[J] == ')' && Str[J + 1] == '_' && Str[J + 2] == '_')
                    {
                        string Token = Str.Substring(Start, J - Start + 3); // +3 for ")__"

                        string MatchToken = HasSpace ? Token : Regex.Replace(Token, @"\s+", "");

                        string MatchedKey = FindBestMatchingPlaceholder(MatchToken);

                        if (MatchedKey != null)
                        {
                            string Replacement = ReplaceTags.First(Tag => Tag.Key == MatchedKey).Value;
                            Result.Append(Replacement);
                            I = J + 3;
                            continue;
                        }
                    }
                }

                Result.Append(Str[I]);
                I++;
            }

            return Result.ToString();
        }

        private bool IsSpaceLanguage(Languages lang)
        {
            return lang == Languages.English ||
                   lang == Languages.German ||
                   lang == Languages.Italian ||
                   lang == Languages.Spanish;
        }
        private string? FindBestMatchingPlaceholder(string Input)
        {
            foreach (var Tag in ReplaceTags)
            {
                string Key = Tag.Key;
                if (Input.Contains(Key) || Normalize(Input) == Normalize(Key))
                    return Key;
            }
            return null;
        }

        private string Normalize(string Input)
        {
            return ToHalfWidth(Input).Replace("_", "").Replace("(", "").Replace(")", "").ToUpperInvariant();
        }

        private string ToHalfWidth(string Input)
        {
            StringBuilder Sb = new StringBuilder();
            foreach (char C in Input)
            {
                if (C >= 0xFF01 && C <= 0xFF5E)
                    Sb.Append((char)(C - 0xFEE0));
                else if (C == 0x3000)
                    Sb.Append(' ');
                else
                    Sb.Append(C);
            }
            return Sb.ToString();
        }
    }
}
