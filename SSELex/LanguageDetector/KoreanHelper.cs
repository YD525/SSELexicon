using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSELex.LanguageDetector
{
    public class KoreanHelper
    {
        // Matches modern Hangul syllables in the Unicode range U+AC00 to U+D7AF
        private static readonly Regex KoreanRegex = new Regex("[\uac00-\ud7af]+", RegexOptions.Compiled);

        /// <summary>
        /// Determines whether the input string likely contains Korean text
        /// </summary>
        /// <param name="Input">Input text to check</param>
        /// <returns>True if Korean characters are detected, otherwise false</returns>
        public static bool IsProbablyKorean(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return false;
            return KoreanRegex.IsMatch(Input);
        }

        /// <summary>
        /// Calculates a simple weighted score indicating the likelihood of Korean text
        /// </summary>
        /// <param name="Input">Input text</param>
        /// <returns>A score based on the ratio of Hangul characters to total length</returns>
        public static double GetKoreanScore(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return 0;

            int TotalLength = Input.Length;
            int MatchCount = KoreanRegex.Matches(Input).Count;
            double Ratio = (double)MatchCount / TotalLength;

            // Weighted score based on the ratio of matched Hangul characters
            return Ratio;
        }
    }
}
