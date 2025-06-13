using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSELex.LanguageDetector
{
    public static class CanadianFrenchHelper
    {
        // Matches common Quebecois (Canadian French) colloquial and slang words/expressions
        private static readonly Regex CanadianFrenchKeywordRegex = new Regex(
            @"\b(tu|toé|moé|ben|ça|tabarnak|câlisse|osti|calisse|maudit|chus|chu|yé|yait|té|là|affaire|pogne|cé|faque|joual|pitoune|char|magasiner|gang|tanné|c'est|frette|bonne|piasse|marde|m’a|t’es|j’suis|québécois)\b",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// Determines whether a given text is likely to be Canadian French (Quebecois)
        /// </summary>
        /// <param name="Input">Input text to check</param>
        /// <param name="KeywordHitsThreshold">Minimum number of matched keywords required (default 1)</param>
        /// <returns>True if text is probably Canadian French, otherwise false</returns>
        public static bool IsProbablyCanadianFrench(string Input, int KeywordHitsThreshold = 1)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return false;

            // Count of matched Canadian French keywords in the input text
            int KeywordHits = CanadianFrenchKeywordRegex.Matches(Input).Count;

            // Return true if the number of keyword matches meets or exceeds the threshold
            return KeywordHits >= KeywordHitsThreshold;
        }
    }
}
