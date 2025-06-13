using System.Security.Cryptography;
using System.Text;
using SSELex.TranslateManage;
using SSELex.ConvertManager;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using SSELex.LanguageDetector;

namespace SSELex.TranslateCore
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public enum Languages
    {
        Null = -2, English = 0, SimplifiedChinese = 1, Japanese = 2, German = 5, Korean = 6, Turkish = 7, Brazilian = 8, Russian = 9, TraditionalChinese = 10, Italian = 11, Spanish = 12, Hindi = 13, Urdu = 15, Indonesian = 16, French = 17, Vietnamese = 20, Polish = 22, CanadianFrench = 23, Portuguese = 25, Auto = 99
    }

    public class LanguageHelper
    {
       
        public static string ToLanguageCode(Languages Lang)
        {
            return Lang switch
            {
                Languages.Null => "",
                Languages.English => "en",
                Languages.SimplifiedChinese => "zh-CN",
                Languages.TraditionalChinese => "zh-TW",
                Languages.Japanese => "ja",
                Languages.German => "de",
                Languages.Korean => "ko",
                Languages.Turkish => "tr",
                Languages.Brazilian => "pt-BR",
                Languages.Russian => "ru",
                Languages.Italian => "it",
                Languages.Spanish => "es",
                Languages.Hindi => "hi",
                Languages.Urdu => "ur",
                Languages.Indonesian => "id",
                Languages.French => "fr",
                Languages.CanadianFrench => "fr-CA",
                Languages.Vietnamese => "vi",
                Languages.Polish => "pl",
                Languages.Portuguese => "pt",
                Languages.Auto => "",
                _ => ""
            };
        }

        public static void DetectLanguage(ref LanguageDetect OneDetect, string Str)
        {
            if (string.IsNullOrWhiteSpace(Str))
                return;

            if (EnglishHelper.IsProbablyEnglish(Str)) //100%
            {
                OneDetect.Add(Languages.English,0.01);
            }

            if (RussianHelper.ContainsRussian(Str)) //100%
            {
                OneDetect.Add(Languages.Russian,0.01);
            }

            if (JapaneseHelper.IsProbablyJapanese(Str)) //90%
            {
                OneDetect.Add(Languages.Japanese, JapaneseHelper.GetJapaneseScore(Str));
            }
            else
            {
                if (TraditionalChineseHelper.ContainsTraditionalChinese(Str))
                {
                    OneDetect.Add(Languages.TraditionalChinese, 1);
                }

                if (SimplifiedChineseHelper.ContainsSimplifiedChinese(Str))  //100%
                {
                    OneDetect.Add(Languages.SimplifiedChinese, 0.01);
                }
            }

            if (KoreanHelper.IsProbablyKorean(Str)) //100%
            {
                OneDetect.Add(Languages.Korean, KoreanHelper.GetKoreanScore(Str));
            }

            if (FrenchHelper.IsProbablyFrench(Str)) //85%
            {
                if (CanadianFrenchHelper.IsProbablyCanadianFrench(Str))
                {
                    OneDetect.Add(Languages.CanadianFrench);
                }
                else
                {
                    OneDetect.Add(Languages.French, FrenchHelper.GetFrenchScore(Str));
                }
            }

            if (PortugueseHelper.IsProbablyPortuguese(Str)) //85%
            {
                OneDetect.Add(Languages.Portuguese, PortugueseHelper.GetPortugueseScore(Str));

                if (BrazilianPortugueseHelper.IsProbablyBrazilianPortuguese(Str))
                {
                    OneDetect.Add(Languages.Brazilian, BrazilianPortugueseHelper.GetBrazilianPortugueseScore(Str));
                }
            }

            if (GermanHelper.IsProbablyGerman(Str)) //85%
            {
                OneDetect.Add(Languages.German, GermanHelper.GetGermanScore(Str));
            }

            if (ItalianHelper.IsProbablyItalian(Str))
            {
                OneDetect.Add(Languages.Italian, ItalianHelper.GetItalianScore(Str));
            }

            if (SpanishHelper.IsProbablySpanish(Str))
            {
                OneDetect.Add(Languages.Spanish, SpanishHelper.GetSpanishScore(Str));
            }
           
            if (PolishHelper.IsProbablyPolish(Str))
            {
                OneDetect.Add(Languages.Polish, PolishHelper.GetPolishScore(Str));
            }

            if (TurkishHelper.IsProbablyTurkish(Str))
            {
                OneDetect.Add(Languages.Turkish, TurkishHelper.GetTurkishScore(Str));
            }

            if (HindiHelper.IsProbablyHindi(Str))
            {
                OneDetect.Add(Languages.Hindi,HindiHelper.GetHindiScore(Str));
            }

            if (UrduHelper.IsProbablyUrdu(Str))
            {
                OneDetect.Add(Languages.Urdu,UrduHelper.GetUrduScore(Str));
            }

            if (IndonesianHelper.IsProbablyIndonesian(Str))
            {
                OneDetect.Add(Languages.Indonesian,IndonesianHelper.GetIndonesianScore(Str));
            }

            if (VietnameseHelper.IsProbablyVietnamese(Str))//??? 20%
            {
                OneDetect.Add(Languages.Vietnamese,VietnameseHelper.GetVietnameseScore(Str));
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
                FakeGrid MainGrid = DeFine.WorkingWin.TransViewList.RealLines[i];

                var GetSourceText = MainGrid.SourceText.Trim();
                DetectLanguage(ref OneDetect, GetSourceText);
            }

            return OneDetect.GetMaxLang();
        }

        public class LanguageDetect
        {
            public Dictionary<Languages, double> Array = new Dictionary<Languages, double>();

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

            public void Add(Languages Lang,double Ratio)
            {
                if (Array.ContainsKey(Lang))
                {
                    Array[Lang] = Array[Lang] + Ratio;
                }
                else
                {
                    Array.Add(Lang, Ratio);
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

    }
}
