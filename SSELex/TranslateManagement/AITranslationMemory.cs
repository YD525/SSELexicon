using SSELex.TranslateCore;
using System.Text;
using System.Text.RegularExpressions;

namespace SSELex.TranslateManage
{
    public class AITranslationMemory
    {
        private const int MAX_BYTE_COUNT = 500;

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
        public void AddTranslation(Languages SourceLang,string Original, string Translated)
        {
            int ByteSize = Encoding.UTF8.GetByteCount(Original);

            if (ByteSize > MAX_BYTE_COUNT)
            {
                return;
            }

            string Normalized = NormalizeText(SourceLang,Original);
            if (!_TranslationDictionary.ContainsKey(Original))
            {
                _TranslationDictionary[Original] = Translated;

                string[] Words = Normalized.Split(' ');
                foreach (string Word in Words)
                {
                    string Key = Word.ToLower();
                    if (!_WordIndex.ContainsKey(Key))
                    {
                        _WordIndex[Key] = new HashSet<string>();
                    }
                    _WordIndex[Key].Add(Original);
                }
            }
        }

        /// <summary>
        /// Normalize text, support camel case splitting and underline conversion
        /// </summary>
        private string NormalizeText(Languages SourceLang,string Text)
        {
            Text = Text.Replace('_', ' ').Replace('-', ' ');

            if (SourceLang == Languages.English)
            {
                Text = Regex.Replace(Text, "(?<!^)([A-Z])", " $1");
            }
           
            return Text;
        }

        /// <summary>
        /// Find the most relevant translations
        /// </summary>
        public List<string> FindRelevantTranslations(Languages SourceLang,string Query, int MaxResults = 3)
        {
            Query = NormalizeText(SourceLang,Query);
            Dictionary<string, int> RelevanceMap = new Dictionary<string, int>();
            string[] Words = Query.Split(' ');

            foreach (string Word in Words)
            {
                string Key = Word.ToLower();
                if (_WordIndex.ContainsKey(Key))
                {
                    foreach (var Sentence in _WordIndex[Key])
                    {
                        if (!RelevanceMap.ContainsKey(Sentence))
                            RelevanceMap[Sentence] = 0;

                        RelevanceMap[Sentence]++;
                    }
                }
            }

            return RelevanceMap.OrderByDescending(KVP => KVP.Value)
                               .Take(MaxResults)
                               .Select(KVP => $"{KVP.Key} -> {_TranslationDictionary[KVP.Key]}")
                               .ToList();
        }
    }
}
