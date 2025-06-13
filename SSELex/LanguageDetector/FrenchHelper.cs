using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSELex.LanguageDetector
{
    public static class FrenchHelper
    {
        // Matches common French function words: articles, pronouns, verbs, conjunctions, question words, etc.
        private static readonly Regex FrenchKeywordRegex = new Regex(
            @"\b(le|la|les|un|une|des|et|est|être|avoir|je|tu|il|elle|nous|vous|ils|elles|ne|pas|dans|sur|avec|pour|par|mais|ou|donc|car|que|qui|quoi|où|quand|comment|pourquoi|fait|beau|allons|au|parc|promener|aujourd'hui)\b",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        // Matches French accented characters (common diacritics)
        private static readonly Regex FrenchAccentCharRegex = new Regex("[éèêëàâäùûüç]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// Determines whether a given text is likely to be French
        /// </summary>
        /// <param name="Input">Input text to check</param>
        /// <param name="AccentCharThreshold">Threshold ratio for accented characters (default 0.01)</param>
        /// <param name="KeywordHitsThreshold">Minimum number of matched keywords required (default 2)</param>
        /// <returns>True if text is probably French, otherwise false</returns>
        public static bool IsProbablyFrench(string Input, double AccentCharThreshold = 0.01, int KeywordHitsThreshold = 2)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return false;

            int TotalLength = Input.Length;

            // Count of matched accented French characters
            int AccentCharCount = FrenchAccentCharRegex.Matches(Input).Count;
            double AccentCharRatio = (double)AccentCharCount / TotalLength;

            // Count of matched French keywords
            int KeywordHits = FrenchKeywordRegex.Matches(Input).Count;

            // Return true if either accented char ratio or keyword hits exceed threshold
            return AccentCharRatio > AccentCharThreshold || KeywordHits >= KeywordHitsThreshold;
        }

        /// <summary>
        /// Calculates a feature score indicating likelihood of French language
        /// </summary>
        /// <param name="Input">Input text</param>
        /// <returns>Score value representing French language likelihood</returns>
        public static double GetFrenchScore(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return 0;

            int TotalLength = Input.Length;
            int AccentCharCount = FrenchAccentCharRegex.Matches(Input).Count;
            int KeywordHits = FrenchKeywordRegex.Matches(Input).Count;

            // Weighted score combining accented characters and keywords, normalized by input length
            return (AccentCharCount * 1.5 + KeywordHits * 2.0) / TotalLength;
        }
    }
}
