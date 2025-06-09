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
       Null = -2, English = 0, SimplifiedChinese = 1, Japanese = 2, German = 5, Korean = 6, Turkish = 7 , Brazilian = 8 , Russian = 9 , TraditionalChinese = 10, Italian = 11, Spanish = 12, Hindi = 13, Urdu = 15, Indonesian = 16, French = 17, Vietnamese = 20, Polish = 22 , CanadianFrench = 23, Auto = 99
    }

    public class LanguageHelper
    {
        private static readonly Regex SimplifiedChineseRegex = new Regex("[\u4e00-\u9fff]+", RegexOptions.Compiled);
        private static readonly Regex TraditionalChineseRegex = new Regex("[\uFA0E\uFA0F\uFA11\uFA13\uFA14\uFA1F\uFA21\uFA23\uFA24\uFA27\uFA28\uFA29\u9FA5\u9FD5\u9FFF]", RegexOptions.Compiled);
        private static readonly Regex JapaneseRegex = new Regex(
    @"[\u3040-\u30FF\u31F0-\u31FF\uFF66-\uFF9F一-龯々〆ヵヶ]|\b(です|ます|する|した|して|いる|いない|から|まで|だけ|そして|しかし|など|という|こと|もの|よう|それ|これ|あれ|どれ|なに|なん|はい|いいえ)\b",
    RegexOptions.Compiled
        );
        private static readonly Regex KoreanRegex = new Regex("[\uac00-\ud7af]+", RegexOptions.Compiled);
        private static readonly Regex GermanRegex = new Regex(
    @"[äöüß]|\b(der|die|das|und|ich|nicht|du|er|sie|es|wir|ihr|sie|ist|bin|bist|sind|seid|war|waren|habe|hat|haben|sein|kann|können|muss|müssen|soll|sollen|will|wollen|ein|eine|einer|eines|dem|den|des|mit|für|auf|in|an|zu|von|über|unter|um|aber|oder|wenn|weil|dass|was|wer|wie|wo|da|hier|dort|jetzt|dann|nur|schon|noch|mehr|als|auch)\b",
    RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex TurkishRegex = new Regex(@"\b(ve|bir|bu|çok|ama|değil|için|ile|sen|ben|mı|mu|mi|mü|şu)\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex BrazilianRegex = new Regex(
    @"\b(o|a|os|as|de|do|da|dos|das|que|e|em|por|para|com|como|mas|se|foi|sou|está|estão|ser|estar|ter|tem|não|sim|eu|você|ele|ela|nós|eles|elas|um|uma|uns|umas|há|vai|vou|fui|tive|também|muito|mais|menos)\b",
    RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex ItalianRegex = new Regex(@"\b(il|la|e|di|che|per|un|una|non|sono|da|con)\b", RegexOptions.IgnoreCase);
        private static readonly Regex SpanishRegex = new Regex(@"\b(el|la|y|de|que|en|un|una|no|soy|con|por)\b", RegexOptions.IgnoreCase);
        private static readonly Regex HindiRegex = new Regex(@"\p{IsDevanagari}", RegexOptions.Compiled);
        private static readonly Regex UrduRegex = new Regex(@"[\u0600-\u06FF]", RegexOptions.Compiled);
        private static readonly Regex IndonesianRegex = new Regex(@"\b(atau|dan|dari|ke|di|ini|itu|untuk)\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex EnglishRegex = new Regex("^[a-zA-Z0-9\\s\\p{P}]+$", RegexOptions.Compiled);
        private static readonly Regex FrenchRegex = new Regex(
    @"\b(le|la|les|un|une|des|et|est|être|avoir|je|tu|il|elle|nous|vous|ils|elles|ne|pas|dans|sur|avec|pour|par|mais|ou|donc|car|que|qui|quoi|où|quand|comment|pourquoi)\b",
    RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex RussianRegex = new Regex(
    @"\b(и|в|во|не|что|он|на|я|с|со|как|а|то|все|она|так|его|но|да|ты|к|у|же|вы|за|бы|по|только|ее|мне|было|вот|от|меня|еще|нет|о|из|ему|теперь|когда|даже|ну|вдруг|ли|если|уже|или|ни|быть|был|него|до|вас|нибудь|опять|уж|вам|ведь|там|потом|себя|ничего|ей|может|они|тут|где|есть|надо|ней|для|мы|тебя|их|чем|была|сам|чтоб|без|будто|чего|раз|тоже|себе|под|будет|ж|тогда|кто|этот|того|потому|этого|какой|совсем|ним|здесь|этом|один|почти|мой|тем|чтобы|нее|сейчас|были|куда|зачем|всех|никогда|можно|при|наконец|два|об|другой|хоть|после|над|больше|тот|через|эти|нас|про|всего|них|какая|много|разве|три|эту|моя|впрочем|хорошо|свою|этой|перед|иногда|лучше|чуть|том|нельзя|такой|им|более|всегда|конечно|всю|между)\b",
    RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex VietnameseRegex = new Regex(
    @"\b(và|của|là|có|một|những|này|tôi|anh|em|không|rất|đã|đang|sẽ|được|với|cho|khi|nào|ở|trong|ra|nhiều|ít|vì|như|nhưng|thì|đây|kia|đó|ai|gì|đâu|sao)\b",
    RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex PolishRegex = new Regex(
    @"\b(że|i|w|z|na|do|jest|nie|tak|jak|to|co|czy|go|za|po|się|dla|ten|tego|być|był|była|było|jestem|jesteś|są|mamy|macie|oni|one|który|która|które|którego|którą|więc|bardzo|już|jeszcze|może|muszę|chcę)\b",
    RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex CanadianFrenchRegex = new Regex(@"\b(tu|toé|moé|ben|ça|tabarnak|câlisse)\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static string ToLanguageCode(Languages lang)
        {
            return lang switch
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
                Languages.Auto => "",
                _ => ""
            };
        }

        private static bool ContainsKana(string Input)
        {
            // 平假名范围：U+3040–U+309F
            // 片假名范围：U+30A0–U+30FF
            // 片假名扩展：U+31F0–U+31FF
            // 半角片假名：U+FF66–U+FF9F
            return Regex.IsMatch(Input, "[\u3040-\u30FF\u31F0-\u31FF\uFF66-\uFF9F]");
        }

        public static void DetectLanguage(ref LanguageDetect OneDetect, string Str)
        {
            // 先检测英文，优先排除纯英文文本
            if (EnglishRegex.IsMatch(Str))
            {
                OneDetect.Add(Languages.English);
                return;
            }

            if (JapaneseRegex.IsMatch(Str) && ContainsKana(Str))
            {
                OneDetect.Add(Languages.Japanese);
            }
            else if (KoreanRegex.IsMatch(Str))
            {
                OneDetect.Add(Languages.Korean);
            }
            else if (HindiRegex.IsMatch(Str))
            {
                OneDetect.Add(Languages.Hindi);
            }
            else if (UrduRegex.IsMatch(Str))
            {
                OneDetect.Add(Languages.Urdu);
            }
            if (TraditionalChineseRegex.IsMatch(Str))
            {
                OneDetect.Add(Languages.TraditionalChinese);
            }
            else if (SimplifiedChineseRegex.IsMatch(Str))
            {
                OneDetect.Add(Languages.SimplifiedChinese);
            }
            else if (RussianRegex.IsMatch(Str))
            {
                OneDetect.Add(Languages.Russian);
            }
            else if (PolishRegex.IsMatch(Str))
            {
                OneDetect.Add(Languages.Polish);
            }
            else if (VietnameseRegex.IsMatch(Str))
            {
                OneDetect.Add(Languages.Vietnamese);
            }
            else if (FrenchRegex.IsMatch(Str))
            {
                if (CanadianFrenchRegex.IsMatch(Str))
                {
                    OneDetect.Add(Languages.CanadianFrench);
                }
                else
                {
                    OneDetect.Add(Languages.French);
                }
            }
            else if (GermanRegex.IsMatch(Str))
            {
                OneDetect.Add(Languages.German);
            }
            else if (TurkishRegex.IsMatch(Str))
            {
                OneDetect.Add(Languages.Turkish);
            }
            else if (BrazilianRegex.IsMatch(Str) && (Str.Contains('ã') || Str.Contains('õ')))
            {
                OneDetect.Add(Languages.Brazilian);
            }
            else if (ItalianRegex.IsMatch(Str))
            {
                OneDetect.Add(Languages.Italian);
            }
            else if (SpanishRegex.IsMatch(Str))
            {
                OneDetect.Add(Languages.Spanish);
            }
            else if (IndonesianRegex.IsMatch(Str))
            {
                OneDetect.Add(Languages.Indonesian);
            }
            else
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
                FakeGrid MainGrid = DeFine.WorkingWin.TransViewList.RealLines[i];

                var GetSourceText = MainGrid.SourceText.Trim();
                DetectLanguage(ref OneDetect, GetSourceText);
            }

            return OneDetect.GetMaxLang();
        }

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

    }
}
