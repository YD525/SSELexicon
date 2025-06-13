using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSELex.LanguageDetector
{
    public static class ItalianHelper
    {
        // Common Italian function words: articles, conjunctions, prepositions, verbs, etc.
        private static readonly string[] ItalianKeywords = new[]
        {
        "il", "la", "e", "di", "che", "per", "un", "una", "non", "sono", "da", "con"
        };

        // Italian-specific accented characters for improved accuracy
        private static readonly Regex ItalianAccentRegex = new Regex(
            "[àèéìíòóùú]",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// Determines whether the input text is likely Italian (based on keywords and accented characters).
        /// </summary>
        /// <param name="Input">The text to check</param>
        /// <param name="KeywordThreshold">Minimum number of keyword matches required (default 2)</param>
        /// <returns>True if the text is probably Italian, otherwise false</returns>
        public static bool IsProbablyItalian(string Input, int KeywordThreshold = 2)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return false;

            int KeywordHits = ItalianKeywords.Count(k => Input.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0);
            bool HasAccent = ItalianAccentRegex.IsMatch(Input);

            return KeywordHits >= KeywordThreshold && HasAccent;
        }

        /// <summary>
        /// Calculates a score indicating the likelihood that the text is Italian
        /// </summary>
        /// <param name="Input">The text to score</param>
        /// <returns>A score representing the likelihood of Italian language</returns>
        public static double GetItalianScore(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return 0;

            int KeywordHits = ItalianKeywords.Count(k => Input.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0);
            int AccentMatches = ItalianAccentRegex.Matches(Input).Count;
            int TotalLength = Input.Length;

            return (KeywordHits * 2.0 + AccentMatches) / TotalLength;
        }
    }
}
