using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSELex.LanguageDetector
{
    public static class SpanishHelper
    {
        // Matches common Spanish function words: articles, conjunctions, prepositions, verbs, negatives, pronouns, etc.
        private static readonly Regex SpanishKeywordRegex = new Regex(
            @"\b(el|la|los|las|un|una|unos|unas|y|de|que|en|no|soy|eres|es|somos|son|con|por|para|se|me|te|le|lo|la|nos|os|les|más|pero|sí|porque|como|cuando|donde|qué|quién|este|ese|aquel)\b",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        // Matches Spanish-specific accented characters and ñ, ü
        private static readonly Regex SpanishAccentCharRegex = new Regex("[áéíóúñüÁÉÍÓÚÑÜ]", RegexOptions.Compiled);

        /// <summary>
        /// Determines whether a given text is likely to be Spanish
        /// </summary>
        /// <param name="Input">Input text to check</param>
        /// <param name="KeywordHitsThreshold">Minimum number of matched keywords required (default 2)</param>
        /// <param name="AccentCharRatioThreshold">Minimum ratio of accented chars to total length (default 0.01)</param>
        /// <returns>True if text is probably Spanish, otherwise false</returns>
        public static bool IsProbablySpanish(string Input, int KeywordHitsThreshold = 2, double AccentCharRatioThreshold = 0.01)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return false;

            int TotalLength = Input.Length;
            int KeywordHits = SpanishKeywordRegex.Matches(Input).Count;
            int AccentCharCount = SpanishAccentCharRegex.Matches(Input).Count;
            double AccentCharRatio = (double)AccentCharCount / TotalLength;

            // Return true if keyword hits exceed threshold OR accent char ratio exceeds threshold
            return KeywordHits >= KeywordHitsThreshold || AccentCharRatio > AccentCharRatioThreshold;
        }

        /// <summary>
        /// Calculates a feature score indicating likelihood of Spanish language
        /// </summary>
        /// <param name="Input">Input text</param>
        /// <returns>Score value representing Spanish language likelihood</returns>
        public static double GetSpanishScore(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return 0;

            int TotalLength = Input.Length;
            int KeywordHits = SpanishKeywordRegex.Matches(Input).Count;
            int AccentCharCount = SpanishAccentCharRegex.Matches(Input).Count;

            // Weighted score combining keywords and accented char count normalized by input length
            return (KeywordHits * 1.5 + AccentCharCount * 2.0) / TotalLength;
        }
    }
}
