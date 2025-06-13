using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSELex.LanguageDetector
{
    public static class BrazilianPortugueseHelper
    {
        // Matches common Brazilian Portuguese function words: articles, prepositions, pronouns, verbs, conjunctions, etc.
        private static readonly Regex BrazilianKeywordRegex = new Regex(
            @"\b(o|a|os|as|de|do|da|dos|das|que|e|em|por|para|com|como|mas|se|foi|sou|está|estão|ser|estar|ter|tem|não|sim|eu|você|ele|ela|nós|eles|elas|um|uma|uns|umas|há|vai|vou|fui|tive|também|muito|mais|menos)\b",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        // Matches Brazilian Portuguese-specific accented characters: á, é, í, ó, ú, â, ê, ô, ã, õ, ç, and their uppercase variants
        private static readonly Regex BrazilianAccentCharRegex = new Regex("[áéíóúâêôãõçÁÉÍÓÚÂÊÔÃÕÇ]", RegexOptions.Compiled);

        /// <summary>
        /// Determines whether a given text is likely to be Brazilian Portuguese
        /// </summary>
        /// <param name="Input">Input text to check</param>
        /// <param name="KeywordHitsThreshold">Minimum number of matched keywords required (default 2)</param>
        /// <param name="AccentCharRatioThreshold">Minimum ratio of accented chars to total length (default 0.01)</param>
        /// <returns>True if text is probably Brazilian Portuguese, otherwise false</returns>
        public static bool IsProbablyBrazilianPortuguese(string Input, int KeywordHitsThreshold = 2, double AccentCharRatioThreshold = 0.01)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return false;

            int TotalLength = Input.Length;
            int KeywordHits = BrazilianKeywordRegex.Matches(Input).Count;
            int AccentCharCount = BrazilianAccentCharRegex.Matches(Input).Count;
            double AccentCharRatio = (double)AccentCharCount / TotalLength;

            // Return true if keyword hits exceed threshold OR accent char ratio exceeds threshold
            return KeywordHits >= KeywordHitsThreshold || AccentCharRatio > AccentCharRatioThreshold;
        }

        /// <summary>
        /// Calculates a feature score indicating likelihood of Brazilian Portuguese language
        /// </summary>
        /// <param name="Input">Input text</param>
        /// <returns>Score value representing Brazilian Portuguese language likelihood</returns>
        public static double GetBrazilianPortugueseScore(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return 0;

            int TotalLength = Input.Length;
            int KeywordHits = BrazilianKeywordRegex.Matches(Input).Count;
            int AccentCharCount = BrazilianAccentCharRegex.Matches(Input).Count;

            // Weighted score combining keywords and accented char count normalized by input length
            return (KeywordHits * 1.5 + AccentCharCount * 2.0) / TotalLength;
        }
    }
}
