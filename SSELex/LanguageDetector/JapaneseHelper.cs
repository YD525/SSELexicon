using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSELex.LanguageDetector
{
    public class JapaneseHelper
    {
        // Matches kana characters and Japanese-specific symbols (hiragana, katakana, extended kana, half-width kana, and some kanji symbols)
        private static readonly Regex JapaneseCharRegex = new Regex(@"[\u3040-\u30FF\u31F0-\u31FF\uFF66-\uFF9F々〆ヵヶ]", RegexOptions.Compiled);

        // Common Japanese functional words: particles, auxiliary verbs, and structure words often used in sentences
        private static readonly string[] JapaneseKeywords = new[]
        {
        "です", "ます", "する", "した", "して", "いる", "いない",
        "から", "まで", "だけ", "そして", "しかし", "など", "という",
        "こと", "もの", "よう", "それ", "これ", "あれ", "どれ",
        "なに", "なん", "はい", "いいえ"
    };

        /// <summary>
        /// Determines whether the input string is likely Japanese based on kana density and keyword matches.
        /// </summary>
        /// <param name="Input">Input text to check</param>
        /// <param name="KanaThreshold">Threshold ratio of kana characters to total length (default 0.1)</param>
        /// <param name="KeywordHitsThreshold">Minimum number of matched keywords required (default 1)</param>
        /// <returns>True if text is probably Japanese, otherwise false</returns>
        public static bool IsProbablyJapanese(string Input, double KanaThreshold = 0.1, int KeywordHitsThreshold = 1)
        {
            if (string.IsNullOrWhiteSpace(Input)) return false;

            // Count of kana characters (hiragana, katakana, extended kana, half-width kana)
            int KanaCount = JapaneseCharRegex.Matches(Input).Count;
            int TotalLength = Input.Length;

            // Kana density (to avoid false positives from very few kana characters)
            double KanaRatio = (double)KanaCount / TotalLength;

            // Number of matched keywords found in the input text
            int KeywordHits = JapaneseKeywords.Count(word => Input.Contains(word));

            // Evaluation rule: either kana density is sufficiently high, or enough keywords are matched
            return KanaRatio > KanaThreshold || KeywordHits >= KeywordHitsThreshold;
        }

        /// <summary>
        /// Calculates a weighted score representing the likelihood of the input text being Japanese.
        /// </summary>
        /// <param name="Input">Input text to evaluate</param>
        /// <param name="KanaWeight">Weight factor for kana character count (default 1.5)</param>
        /// <param name="KeywordWeight">Weight factor for keyword matches (default 2.0)</param>
        /// <returns>Score value indicating Japanese language likelihood</returns>
        public static double GetJapaneseScore(string Input, double KanaWeight = 1.5, double KeywordWeight = 2.0)
        {
            if (string.IsNullOrWhiteSpace(Input)) return 0;

            int TotalLength = Input.Length;
            int KanaCount = JapaneseCharRegex.Matches(Input).Count;
            int KeywordHits = JapaneseKeywords.Count(word => Input.Contains(word));

            // Weighted score combines kana count and keyword hits normalized by total length
            return (KanaCount * KanaWeight + KeywordHits * KeywordWeight) / TotalLength;
        }
    }
}
