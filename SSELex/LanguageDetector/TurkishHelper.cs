using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSELex.LanguageDetector
{
    public static class TurkishHelper
    {
        // Matches common Turkish function words: conjunctions, pronouns, question particles, etc.
        private static readonly Regex TurkishKeywordRegex = new Regex(
            @"\b(ve|bir|bu|çok|ama|değil|için|ile|sen|ben|mı|mu|mi|mü|şu)\b",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        // Matches Turkish-specific accented characters: ç, ğ, ı, İ, ö, ş, ü and their uppercase variants
        private static readonly Regex TurkishAccentCharRegex = new Regex("[çğıİöşüÇĞİÖŞÜ]", RegexOptions.Compiled);

        /// <summary>
        /// Determines whether a given text is likely to be Turkish
        /// </summary>
        /// <param name="Input">Input text to check</param>
        /// <param name="KeywordHitsThreshold">Minimum number of matched keywords required (default 2)</param>
        /// <param name="AccentCharRatioThreshold">Minimum ratio of accented chars to total length (default 0.01)</param>
        /// <returns>True if text is probably Turkish, otherwise false</returns>
        public static bool IsProbablyTurkish(string Input, int KeywordHitsThreshold = 2, double AccentCharRatioThreshold = 0.01)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return false;

            int TotalLength = Input.Length;
            int KeywordHits = TurkishKeywordRegex.Matches(Input).Count;
            int AccentCharCount = TurkishAccentCharRegex.Matches(Input).Count;
            double AccentCharRatio = (double)AccentCharCount / TotalLength;

            // Return true if keyword hits exceed threshold OR accent char ratio exceeds threshold
            return KeywordHits >= KeywordHitsThreshold || AccentCharRatio > AccentCharRatioThreshold;
        }

        /// <summary>
        /// Calculates a feature score indicating likelihood of Turkish language
        /// </summary>
        /// <param name="Input">Input text</param>
        /// <returns>Score value representing Turkish language likelihood</returns>
        public static double GetTurkishScore(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return 0;

            int TotalLength = Input.Length;
            int KeywordHits = TurkishKeywordRegex.Matches(Input).Count;
            int AccentCharCount = TurkishAccentCharRegex.Matches(Input).Count;

            // Weighted score combining keywords and accented char count normalized by input length
            return (KeywordHits * 1.5 + AccentCharCount * 2.0) / TotalLength;
        }
    }
}
