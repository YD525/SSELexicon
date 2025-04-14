using System.Security.Cryptography;
using System.Text;
using SSELex.TranslateManage;
using SSELex.ConvertManager;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace SSELex.TranslateCore
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public enum Languages
    {
        English = 0, SimplifiedChinese = 1, Japanese = 2, German = 5, Korean = 6, Turkish = 7 , Brazilian = 8 , Russian = 9 , TraditionalChinese = 10
    }

    public class LanguageHelper
    {
        private static readonly Regex SimplifiedChineseRegex = new Regex("[\u4e00-\u9fff]+", RegexOptions.Compiled);
        private static readonly Regex TraditionalChineseRegex = new Regex("[\uFA0E\uFA0F\uFA11\uFA13\uFA14\uFA1F\uFA21\uFA23\uFA24\uFA27\uFA28\uFA29\u9FA5\u9FD5\u9FFF]", RegexOptions.Compiled);
        private static readonly Regex JapaneseRegex = new Regex("[\u3040-\u30ff]+", RegexOptions.Compiled);
        private static readonly Regex KoreanRegex = new Regex("[\uac00-\ud7af]+", RegexOptions.Compiled);
        private static readonly Regex GermanRegex = new Regex("[\u00C0-\u017F]+", RegexOptions.Compiled);
        private static readonly Regex TurkishRegex = new Regex("[\u0400-\u04FF]+", RegexOptions.Compiled);
        private static readonly Regex BrazilianRegex = new Regex("[\u00C0-\u00FF]+|[a-zA-Z]+", RegexOptions.Compiled);
        private static readonly Regex EnglishRegex = new Regex("^[a-zA-Z0-9\\s\\p{P}]+$", RegexOptions.Compiled);

        public class LanguageDetect
        {
            public Dictionary<Languages, int> Array = new Dictionary<Languages, int>();

            public void Add(Languages Lang)
            {
                if (Array.ContainsKey(Lang))
                {
                    Array[Lang] = Array[Lang] + 1;
                }
                else
                {
                    Array.Add(Lang, 1);
                }
            }

            public Languages GetMaxLang()
            {
                if (Array.Count > 0)
                {
                    return Array
                      .OrderByDescending(kv => kv.Value)
                      .First().Key;
                }
                return Languages.English;
            }
        }

        public static void DetectLanguage(ref LanguageDetect OneDetect, string Str)
        {
            if (string.IsNullOrWhiteSpace(Str))
                return;

            if (SimplifiedChineseRegex.IsMatch(Str))
            {
                if (TraditionalChineseRegex.IsMatch(Str))
                    OneDetect.Add(Languages.TraditionalChinese);
                else
                    OneDetect.Add(Languages.SimplifiedChinese);
            }
            else if (JapaneseRegex.IsMatch(Str))
            {
                OneDetect.Add(Languages.Japanese);
            }
            else if (KoreanRegex.IsMatch(Str))
            {
                OneDetect.Add(Languages.Korean);
            }
            else if (GermanRegex.IsMatch(Str))
            {
                OneDetect.Add(Languages.German);
            }
            else if (TurkishRegex.IsMatch(Str))
            {
                OneDetect.Add(Languages.Turkish);
            }
            else if (BrazilianRegex.IsMatch(Str) && (Str.Contains("ã") || Str.Contains("õ")))
            {
                OneDetect.Add(Languages.Brazilian);
            }
            else if (EnglishRegex.IsMatch(Str))
            {
                OneDetect.Add(Languages.English);
            }
        }

        public static Languages DetectLanguageByLine(string String)
        {
            LanguageDetect OneDetect = new LanguageDetect();
            DetectLanguage(ref OneDetect, String);
            return OneDetect.GetMaxLang();
        }

        public static Languages DetectLanguageByContent(string Text)
        {
            LanguageDetect OneDetect = new LanguageDetect();

            foreach (var GetLine in Text.Split(new char[2] { '\r', '\n' }))
            {
                if (GetLine.Trim().Length > 0)
                {
                    DetectLanguage(ref OneDetect, GetLine);
                }
            }

            return OneDetect.GetMaxLang();
        }

        public static Languages DetectLanguage()
        {
            LanguageDetect OneDetect = new LanguageDetect();
            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                Grid MainGrid = DeFine.WorkingWin.TransViewList.RealLines[i];

                var GetSourceText = (MainGrid.Children[3] as TextBox).Text.Trim();
                DetectLanguage(ref OneDetect, GetSourceText);
            }

            return OneDetect.GetMaxLang();
        }
    }
}
