using SSELex.TranslateCore;
using System.Text;
using System.Text.RegularExpressions;

namespace SSELex.TranslateManage
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/
    public static class LanguageExtensions
    {
        public static bool IsSpaceDelimitedLanguage(this Languages Lang)
        {
            return Lang == Languages.English ||
                   Lang == Languages.German ||
                   Lang == Languages.Turkish ||
                   Lang == Languages.Brazilian ||
                   Lang == Languages.Russian ||
                   Lang == Languages.Italian ||
                   Lang == Languages.Spanish ||
                   Lang == Languages.Indonesian ||
                   Lang == Languages.Hindi ||
                   Lang == Languages.Urdu;
        }

        public static bool IsNoSpaceLanguage(this Languages Lang)
        {
            return Lang == Languages.SimplifiedChinese ||
                   Lang == Languages.TraditionalChinese ||
                   Lang == Languages.Japanese ||
                   Lang == Languages.Korean;
        }
    }

    public class AITranslationMemory
    {
        private const int MAX_BYTE_COUNT = 125;

        private readonly Dictionary<string, string> _TranslationDictionary = new Dictionary<string, string>();
        private readonly Dictionary<string, HashSet<string>> _WordIndex = new Dictionary<string, HashSet<string>>();

        public void Clear()
        {
            _TranslationDictionary.Clear();
            _WordIndex.Clear();
        }

        /// <summary>
        /// Add translation and create index (filter out long text)
        /// </summary>
        public void AddTranslation(Languages SourceLang, string Original, string Translated)
        {
            int ByteSize = Encoding.UTF8.GetByteCount(Original);
            if (ByteSize > MAX_BYTE_COUNT)
                return;

            if (!_TranslationDictionary.ContainsKey(Original))
            {
                _TranslationDictionary[Original] = Translated;

                string[] Tokens = Tokenize(SourceLang, Original);
                foreach (string Word in Tokens)
                {
                    string Key = Word.ToLower();
                    if (!_WordIndex.ContainsKey(Key))
                        _WordIndex[Key] = new HashSet<string>();

                    _WordIndex[Key].Add(Original);
                }
            }
        }

        /// <summary>
        /// Find the most relevant translations
        /// </summary>
        public List<string> FindRelevantTranslations(Languages SourceLang, string Query, int MaxResults = 3)
        {
            string[] Words = Tokenize(SourceLang, Query);
            Dictionary<string, int> RelevanceMap = new Dictionary<string, int>();
            HashSet<string> CandidateSentences = new HashSet<string>();

            foreach (string Word in Words)
            {
                string Key = Word.ToLower();
                if (_WordIndex.ContainsKey(Key))
                {
                    foreach (var Sentence in _WordIndex[Key])
                    {
                        CandidateSentences.Add(Sentence);
                    }
                }
            }

            foreach (var Sentence in CandidateSentences)
            {
                int MatchCount = 0;
                foreach (string Word in Words)
                {
                    string Key = Word.ToLower();
                    if (_WordIndex.TryGetValue(Key, out var SentencesForWord))
                    {
                        if (SentencesForWord.Contains(Sentence))
                            MatchCount++;
                    }
                }
                if (MatchCount > 0)
                {
                    RelevanceMap[Sentence] = MatchCount;
                }
            }

            return RelevanceMap.OrderByDescending(kvp => kvp.Value)
                               .Take(MaxResults)
                               .Select(kvp => $"{kvp.Key} -> {_TranslationDictionary[kvp.Key]}")
                               .ToList();
        }


        private static readonly HashSet<string> EnglishStopWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "a", "an", "the", "and", "or", "but", "if", "then", "else",
            "on", "in", "at", "to", "for", "with", "by", "of", "is",
            "it", "this", "that", "these", "those", "as", "are", "was",
            "were", "be", "been", "has", "have", "had", "do", "does", "did",
            "so", "such", "not", "no", "nor", "too", "very", "can", "will",
            "just", "up", "down", "out", "over", "under", "again", "more",
            "most", "some", "any", "each", "all"
        };

        public const int MaxGram = 3 + 1;
        /// <summary>
        /// Tokenizer: supports English splitting + simplified N-gram for no-space languages
        /// </summary>
        private string[] Tokenize(Languages Lang, string Text)
        {
            Text = Text.Replace('_', ' ').Replace('-', ' ');

            if (Lang.IsSpaceDelimitedLanguage())
            {
                Text = Regex.Replace(Text, "(?<!^)([A-Z])", " $1");
                var tokens = Text.Split(new[] { ' ', '.', ',', '?', '!', ';', ':', '(', ')', '[', ']', '{', '}', '"', '\'' },
                    StringSplitOptions.RemoveEmptyEntries);

                return tokens
                    .Where(t => t.Length > 1)
                    .Where(t => !(Lang == Languages.English && EnglishStopWords.Contains(t))) 
                    .ToArray();
            }

            if (!Lang.IsNoSpaceLanguage())
            {
                return Text.Split(new[] { ' ', '.', ',', '?', '!', ';', ':', '(', ')', '[', ']', '{', '}', '"', '\'' },
                    StringSplitOptions.RemoveEmptyEntries)
                    .Where(t => t.Length > 1)
                    .ToArray();
            }

            List<(string Token, int Index)> TokensWithIndex = new();
            for (int I = 0; I < Text.Length; I++)
            {
                TokensWithIndex.Add((" " + Text[I] + " ", I));
            }

            List<string> Result = new();

            string[] SingleTokens = TokensWithIndex.Select(T => T.Token).ToArray();
            int[] Indices = TokensWithIndex.Select(T => T.Index).ToArray();

            for (int I = 0; I < SingleTokens.Length; I++)
            {
                for (int Len = 1; Len <= MaxGram && I + Len <= SingleTokens.Length; Len++)
                {
                    bool IsContinuous = true;
                    for (int J = I; J < I + Len - 1; J++)
                    {
                        if (Indices[J + 1] != Indices[J] + 1)
                        {
                            IsContinuous = false;
                            break;
                        }
                    }
                    if (!IsContinuous) continue;

                    var TokenSb = new System.Text.StringBuilder();
                    for (int K = I; K < I + Len; K++)
                    {
                        TokenSb.Append(SingleTokens[K]);
                    }
                    string Token = TokenSb.ToString().Replace(" ", "");
                    if (!string.IsNullOrWhiteSpace(Token) && Token.Length > 1)
                    {
                        Result.Add(Token);
                    }
                }
            }

            return Result.ToArray();
        }
    }
}
