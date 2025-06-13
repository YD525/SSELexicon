using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSELex.LanguageDetector
{
    public static class GermanHelper
    {
        // Matches German special characters with umlauts and sharp s (ß)
        private static readonly Regex GermanCharRegex = new Regex("[äöüß]", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        // Matches common German function words (articles, verbs, conjunctions, pronouns, etc.)
        private static readonly Regex GermanKeywordRegex = new Regex(
            @"\b(der|die|das|und|ich|nicht|du|er|sie|es|wir|ihr|sie|ist|bin|bist|sind|seid|war|waren|habe|hat|haben|sein|kann|können|muss|müssen|soll|sollen|will|wollen|ein|eine|einer|eines|dem|den|des|mit|für|auf|in|an|zu|von|über|unter|um|aber|oder|wenn|weil|dass|was|wer|wie|wo|da|hier|dort|jetzt|dann|nur|schon|noch|mehr|als|auch)\b",
            RegexOptions.Compiled | RegexOptions.IgnoreCase
        );

        /// <summary>
        /// Determines whether a given text is likely to be German
        /// </summary>
        /// <param name="Input">Input text to check</param>
        /// <param name="CharThreshold">Threshold for ratio of special characters</param>
        /// <param name="KeywordHitsThreshold">Minimum number of matched keywords</param>
        public static bool IsProbablyGerman(string Input, double CharThreshold = 0.01, int KeywordHitsThreshold = 2)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return false;

            int TotalLength = Input.Length;

            // Count of special German characters matched
            int SpecialCharCount = GermanCharRegex.Matches(Input).Count;
            double SpecialCharRatio = (double)SpecialCharCount / TotalLength;

            // Count of matched keywords
            int KeywordHits = GermanKeywordRegex.Matches(Input).Count;

            // Return true if either special character ratio or keyword hits exceed threshold
            return SpecialCharRatio > CharThreshold || KeywordHits >= KeywordHitsThreshold;
        }

        /// <summary>
        /// Calculates a feature score indicating likelihood of German language
        /// </summary>
        /// <param name="Input">Input text</param>
        /// <returns>Score value representing German language likelihood</returns>
        public static double GetGermanScore(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return 0;

            int TotalLength = Input.Length;
            int SpecialCharCount = GermanCharRegex.Matches(Input).Count;
            int KeywordHits = GermanKeywordRegex.Matches(Input).Count;

            // Simple weighted score formula combining special characters and keywords
            return (SpecialCharCount * 1.5 + KeywordHits * 2.0) / TotalLength;
        }
    }
}
