using SSELex.TranslateCore;
using System.Text;
using System.Text.RegularExpressions;

namespace SSELex.TranslateManage
{
    public class AITranslationCache
    {
        AITranslationMemory Memory = new AITranslationMemory();
    }
    public class AITranslationMemory
    {
        private const int MAX_BYTE_COUNT = 500;

        private readonly Dictionary<string, string> _translationDictionary = new Dictionary<string, string>();
        private readonly Dictionary<string, HashSet<string>> _wordIndex = new Dictionary<string, HashSet<string>>();

        public void Clear()
        {
            _translationDictionary.Clear();
            _wordIndex.Clear();
        }

        /// <summary>
        /// 添加翻译，并建立索引（过滤超长文本）
        /// </summary>
        public void AddTranslation(Languages SourceLang,string original, string translated)
        {
            int byteSize = Encoding.UTF8.GetByteCount(original);

            if (byteSize > MAX_BYTE_COUNT)
            {
                return;
            }

            string normalized = NormalizeText(SourceLang,original);
            if (!_translationDictionary.ContainsKey(original))
            {
                _translationDictionary[original] = translated;

                string[] words = normalized.Split(' ');
                foreach (string word in words)
                {
                    string key = word.ToLower();
                    if (!_wordIndex.ContainsKey(key))
                    {
                        _wordIndex[key] = new HashSet<string>();
                    }
                    _wordIndex[key].Add(original);
                }
            }
        }

        /// <summary>
        /// 规范化文本，支持驼峰拆分、下划线转换
        /// </summary>
        private string NormalizeText(Languages SourceLang,string text)
        {
            text = text.Replace('_', ' ').Replace('-', ' ');

            if (SourceLang == Languages.English)
            {
                text = Regex.Replace(text, "(?<!^)([A-Z])", " $1");
            }
           
            return text;
        }

        /// <summary>
        /// 查询最相关的翻译
        /// </summary>
        public List<string> FindRelevantTranslations(Languages SourceLang,string query, int maxResults = 5)
        {
            query = NormalizeText(SourceLang,query);
            Dictionary<string, int> relevanceMap = new Dictionary<string, int>();
            string[] words = query.Split(' ');

            foreach (string word in words)
            {
                string key = word.ToLower();
                if (_wordIndex.ContainsKey(key))
                {
                    foreach (var sentence in _wordIndex[key])
                    {
                        if (!relevanceMap.ContainsKey(sentence))
                            relevanceMap[sentence] = 0;

                        relevanceMap[sentence]++;
                    }
                }
            }

            return relevanceMap.OrderByDescending(kvp => kvp.Value)
                               .Take(maxResults)
                               .Select(kvp => $"{kvp.Key} -> {_translationDictionary[kvp.Key]}")
                               .ToList();
        }
    }
}
