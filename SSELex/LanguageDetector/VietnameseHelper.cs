using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSELex.LanguageDetector
{
    public static class VietnameseHelper
    {
        // Common Vietnamese function words: articles, pronouns, conjunctions, etc.
        private static readonly string[] VietnameseKeywords = new[]
        {
        "và", "của", "là", "có", "một", "những", "này", "tôi", "anh", "em", "không",
        "rất", "đã", "đang", "sẽ", "được", "với", "cho", "khi", "nào", "ở", "trong",
        "ra", "nhiều", "ít", "vì", "như", "nhưng", "thì", "đây", "kia", "đó", "ai",
        "gì", "đâu", "sao"
    };

        // Regex to match Vietnamese accented characters for better accuracy
        private static readonly Regex VietnameseAccentRegex = new Regex(
            "[áàảãạăắằẳẵặâấầẩẫậđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵ]",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// Determines whether the input text is likely Vietnamese (based on keywords and accented characters).
        /// </summary>
        /// <param name="Input">The text to check</param>
        /// <param name="KeywordThreshold">Minimum number of keyword matches required (default 2)</param>
        /// <returns>True if the text is probably Vietnamese, otherwise false</returns>
        public static bool IsProbablyVietnamese(string Input, int KeywordThreshold = 2)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return false;

            // Count the number of keyword matches
            int KeywordHits = VietnameseKeywords.Count(k => Input.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0);

            // Check if input contains Vietnamese accented characters
            bool HasAccent = VietnameseAccentRegex.IsMatch(Input);

            // Consider it Vietnamese if keyword hits meet threshold and accents are present
            return KeywordHits >= KeywordThreshold && HasAccent;
        }

        /// <summary>
        /// Calculates a score indicating the likelihood that the text is Vietnamese
        /// </summary>
        /// <param name="Input">The text to score</param>
        /// <returns>A score representing the likelihood of Vietnamese language</returns>
        public static double GetVietnameseScore(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return 0;

            int KeywordHits = VietnameseKeywords.Count(k => Input.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0);
            int AccentMatches = VietnameseAccentRegex.Matches(Input).Count;
            int TotalLength = Input.Length;

            // Weighted score with keywords having higher weight, normalized by length
            return (KeywordHits * 2.0 + AccentMatches) / TotalLength;
        }
    }
}
