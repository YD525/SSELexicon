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

    public class CustomWord
    {
        public Languages From = Languages.Null;
        public Languages To = Languages.Null;

        public string SourceWord = "";
        public string TargetWord = "";
    }

    public class ReplaceTag
    {
        public string Key = "";
        public string Value = "";
        public ReplaceTag(string Key, string Value)
        {
            this.Key = Key;
            this.Value = Value;
        }
    }

    public class TranslationPreprocessor : TranslationPreprocessorExtend
    {
        public static List<CustomWord> Datas = new List<CustomWord>();

        public bool HasPlaceholder = false;
        public string SourceStr = "";

        public List<ReplaceTag> ReplaceTags = new List<ReplaceTag>();

        public TranslationPreprocessor(Languages From, Languages To, string SourceStr)
        {
            this.SourceStr = SourceStr;
        }

        public string ApplyCustomWordDirectlyForAI(Languages From, Languages To, string Str)
        {
            if (string.IsNullOrEmpty(Str))
                return Str;

            bool UseWordBoundary = IsSpaceLanguage(From);

            for (int i = 0; i < Datas.Count; i++)
            {
                var Word = Datas[i];
                if (Word.From != From || Word.To != To || string.IsNullOrWhiteSpace(Word.SourceWord))
                    continue;

                string Source = Word.SourceWord;
                string Target = Word.TargetWord;

                if (string.IsNullOrEmpty(Source) || string.IsNullOrEmpty(Target))
                    continue;

                if (UseWordBoundary)
                {
                    string Pattern = $@"\b{Regex.Escape(Source)}\b";
                    if (Regex.IsMatch(Str, Pattern, RegexOptions.IgnoreCase))
                    {
                        Str = Regex.Replace(Str, Pattern, Target, RegexOptions.IgnoreCase);
                    }
                }
                else
                {
                    if (Str.Contains(Source))
                    {
                        Str = Str.Replace(Source, Target);
                    }
                }
            }

            return Str;
        }

        public string GeneratePlaceholderText(Languages From, Languages To, string Str)
        {
            ReplaceTags.Clear();
            HasPlaceholder = false;

            bool UseWordBoundary = From == Languages.English ||
                                   From == Languages.German ||
                                   From == Languages.Italian ||
                                   From == Languages.Spanish;

            for (int i = 0; i < Datas.Count; i++)
            {
                var Word = Datas[i];
                if (Word.From != From || Word.To != To || string.IsNullOrWhiteSpace(Word.SourceWord))
                    continue;

                string Placeholder = $"__({i})__";
                string Source = Word.SourceWord;

                if (UseWordBoundary)
                {
                    string Pattern = $@"\b{Regex.Escape(Source)}\b";
                    if (Regex.IsMatch(Str, Pattern, RegexOptions.IgnoreCase))
                    {
                        Str = Regex.Replace(Str, Pattern, Placeholder, RegexOptions.IgnoreCase);
                        ReplaceTags.Add(new ReplaceTag(Placeholder, Word.TargetWord));
                        HasPlaceholder = true;
                    }
                }
                else
                {
                    if (Str.Contains(Source))
                    {
                        Str = Str.Replace(Source, Placeholder);
                        ReplaceTags.Add(new ReplaceTag(Placeholder, Word.TargetWord));
                        HasPlaceholder = true;
                    }
                }
            }

            return Str;
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
        private string FindBestMatchingPlaceholder(string Input)
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
