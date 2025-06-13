using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSELex.LanguageDetector
{
    public static class UkrainianHelper
    {
        // Matches Ukrainian-specific characters: Ґ ґ Є є І і Ї ї
        private static readonly Regex UkrainianRegex = new Regex(@"[ґҐєЄіІїЇ]", RegexOptions.Compiled);

        /// <summary>
        /// Determines whether the input string is probably Ukrainian.
        /// </summary>
        /// <param name="Input">The input string.</param>
        /// <returns>True if the string contains Ukrainian-specific characters.</returns>
        public static bool IsProbablyUkrainian(string Input)
        {
            if (string.IsNullOrEmpty(Input))
                return false;

            return UkrainianRegex.IsMatch(Input);
        }

        /// <summary>
        /// Calculates a confidence score based on the number of Ukrainian-specific characters.
        /// </summary>
        /// <param name="Input">The input string.</param>
        /// <returns>A confidence score between 0 and 1.</returns>
        public static double GetUkrainianScore(string Input)
        {
            if (string.IsNullOrEmpty(Input))
                return 0;

            int Total = Input.Length;
            int MatchCount = UkrainianRegex.Matches(Input).Count;

            return Total == 0 ? 0 : (double)MatchCount / Total;
        }
    }
}
