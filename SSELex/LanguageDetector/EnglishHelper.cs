using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSELex.LanguageDetector
{
    public static class EnglishHelper
    {
        // Matches English letters (a-z, A-Z), digits, whitespace, and common English punctuation marks.
        // Excludes accented characters (like é, ü etc.) to avoid误判其他语言
        private static readonly Regex EnglishRegex = new Regex(
            @"^[a-zA-Z0-9\s\p{P}]+$",
            RegexOptions.Compiled);

        /// <summary>
        /// Determines whether the input string is likely to be English text,
        /// containing only English letters, digits, whitespace, and punctuation.
        /// Excludes accented characters to improve accuracy.
        /// </summary>
        /// <param name="Input">Input text to check</param>
        /// <returns>True if input is likely English; otherwise false.</returns>
        public static bool IsProbablyEnglish(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return false;

            return EnglishRegex.IsMatch(Input);
        }
    }
}
