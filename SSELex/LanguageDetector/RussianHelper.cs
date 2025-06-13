using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSELex.LanguageDetector
{
    public static class RussianHelper
    {
        // Matches Cyrillic characters in the range U+0400 to U+04FF
        private static readonly Regex RussianRegex = new Regex(@"[\u0400-\u04FF]", RegexOptions.Compiled);

        /// <summary>
        /// Determines whether the input text contains Russian (Cyrillic) characters.
        /// </summary>
        /// <param name="Input">Input text to check</param>
        /// <returns>True if text contains Russian characters; otherwise false</returns>
        public static bool ContainsRussian(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return false;

            return RussianRegex.IsMatch(Input);
        }

        /// <summary>
        /// Calculates the ratio of Russian (Cyrillic) characters to total length in the input text.
        /// </summary>
        /// <param name="Input">Input text</param>
        /// <returns>Ratio of Russian characters in the text</returns>
        public static double GetRussianRatio(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return 0;

            int TotalLength = Input.Length;
            int MatchCount = RussianRegex.Matches(Input).Count;

            return (double)MatchCount / TotalLength;
        }
    }
}
