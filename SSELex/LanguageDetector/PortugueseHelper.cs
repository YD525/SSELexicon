using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSELex.LanguageDetector
{
    public static class PortugueseHelper
    {
        // Matches common Portuguese (both European and Brazilian) function words, pronouns, verbs, conjunctions, etc.
        private static readonly Regex PortugueseKeywordRegex = new Regex(
            @"\b(o|a|os|as|de|do|da|dos|das|que|e|em|por|para|com|como|mas|se|foi|sou|está|estão|ser|estar|ter|tem|não|sim|eu|você|ele|ela|nós|eles|elas|um|uma|uns|umas|há|vai|vou|fui|tive|também|muito|mais|menos|vós|lhe|meu|minha|teu|tua|seu|sua|nos|vos|este|esse|aquele|aquela|onde|quando|porque|porquê|qual|como|sem|além|até|entre|sobre|contra)\b",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        // Matches Portuguese special characters with diacritics (accents and cedilla)
        private static readonly Regex PortugueseSpecialCharRegex = new Regex("[áàâãäéèêëíìîïóòôõöúùûüçÁÀÂÃÄÉÈÊËÍÌÎÏÓÒÔÕÖÚÙÛÜÇ]", RegexOptions.Compiled);

        /// <summary>
        /// Determines whether a given text is likely to be Portuguese (European or Brazilian)
        /// </summary>
        /// <param name="Input">Input text to check</param>
        /// <param name="CharThreshold">Threshold for ratio of special characters (default 0.01)</param>
        /// <param name="KeywordHitsThreshold">Minimum number of matched keywords (default 2)</param>
        /// <returns>True if text is probably Portuguese, otherwise false</returns>
        public static bool IsProbablyPortuguese(string Input, double CharThreshold = 0.01, int KeywordHitsThreshold = 2)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return false;

            int TotalLength = Input.Length;

            int SpecialCharCount = PortugueseSpecialCharRegex.Matches(Input).Count;
            double SpecialCharRatio = (double)SpecialCharCount / TotalLength;

            int KeywordHits = PortugueseKeywordRegex.Matches(Input).Count;

            return SpecialCharRatio > CharThreshold || KeywordHits >= KeywordHitsThreshold;
        }

        /// <summary>
        /// Calculates a feature score indicating likelihood of Portuguese language
        /// </summary>
        /// <param name="Input">Input text</param>
        /// <returns>Score value representing Portuguese language likelihood</returns>
        public static double GetPortugueseScore(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return 0;

            int TotalLength = Input.Length;
            int SpecialCharCount = PortugueseSpecialCharRegex.Matches(Input).Count;
            int KeywordHits = PortugueseKeywordRegex.Matches(Input).Count;

            return (SpecialCharCount * 1.5 + KeywordHits * 2.0) / TotalLength;
        }
    }
}
