using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSELex.LanguageDetector
{
    public static class SimplifiedChineseHelper
    {
        // Matches CJK Unified Ideographs block (mainly simplified Chinese characters)
        private static readonly Regex SimplifiedChineseRegex = new Regex("[\u4e00-\u9fff]+", RegexOptions.Compiled);

        /// <summary>
        /// Determines whether the input text contains simplified Chinese characters.
        /// </summary>
        /// <param name="Input">Input text to check</param>
        /// <returns>True if text contains simplified Chinese characters; otherwise false</returns>
        public static bool ContainsSimplifiedChinese(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return false;

            return SimplifiedChineseRegex.IsMatch(Input);
        }

        /// <summary>
        /// Calculates the ratio of simplified Chinese characters to total length in the input text.
        /// </summary>
        /// <param name="Input">Input text</param>
        /// <returns>Ratio of simplified Chinese characters in the text</returns>
        public static double GetSimplifiedChineseRatio(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return 0;

            int TotalLength = Input.Length;
            var Matches = SimplifiedChineseRegex.Matches(Input);
            int MatchedLength = 0;

            foreach (Match Match in Matches)
            {
                MatchedLength += Match.Length;
            }

            return (double)MatchedLength / TotalLength;
        }
    }
}
