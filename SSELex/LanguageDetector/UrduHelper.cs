using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSELex.LanguageDetector
{
    public static class UrduHelper
    {
        private static readonly Regex UrduCharRegex = new Regex(@"[\u0600-\u06FF]", RegexOptions.Compiled);

        private static readonly string[] UrduKeywords = new[]
        {
        "ہے", "میں", "کے", "اور", "کا", "کی", "سے", "یہ", "میں", "تم", "یہاں"
        };

        public static bool IsProbablyUrdu(string input, int keywordThreshold = 2)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            int keywordHits = UrduKeywords.Count(k => input.Contains(k));
            bool hasUrduChars = UrduCharRegex.IsMatch(input);

            return hasUrduChars && keywordHits >= keywordThreshold;
        }

        public static double GetUrduScore(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return 0;

            int keywordHits = UrduKeywords.Count(k => input.Contains(k));
            int charCount = UrduCharRegex.Matches(input).Count;
            int length = input.Length;

            return (keywordHits * 2.0 + charCount) / length;
        }
    }

}
