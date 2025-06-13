using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSELex.LanguageDetector
{
    public static class IndonesianHelper
    {
        // Common Indonesian function words: conjunctions, prepositions, demonstratives, etc.
        private static readonly string[] IndonesianKeywords = new[]
        {
        "atau", "dan", "dari", "ke", "di", "ini", "itu", "untuk"
        };

        /// <summary>
        /// Determines whether the input text is likely Indonesian based on keyword presence.
        /// </summary>
        /// <param name="Input">Text to check</param>
        /// <param name="KeywordThreshold">Minimum number of keyword hits required (default 2)</param>
        /// <returns>True if text is probably Indonesian, else false</returns>
        public static bool IsProbablyIndonesian(string Input, int KeywordThreshold = 2)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return false;

            int KeywordHits = IndonesianKeywords.Count(k => Input.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0);
            return KeywordHits >= KeywordThreshold;
        }

        /// <summary>
        /// Calculates a score indicating the likelihood that the text is Indonesian.
        /// </summary>
        /// <param name="Input">Text to score</param>
        /// <returns>Score value representing Indonesian language likelihood</returns>
        public static double GetIndonesianScore(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return 0;

            int KeywordHits = IndonesianKeywords.Count(k => Input.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0);
            int TotalLength = Input.Length;
            return (double)KeywordHits / TotalLength;
        }
    }
}
