using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSELex.TranslateManagement
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/
    public class TranslationPreprocessorExtend
    {
        public static bool IsNumeric(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return false;

            return double.TryParse(Input.Trim(), out _);
        }

        /// <summary>
        /// Removes common invisible Unicode characters from the input string.
        /// These include zero-width spaces, non-breaking spaces, and similar hidden characters
        /// that might interfere with text processing.
        /// </summary>
        /// <param name="Input">The string to be cleaned (passed by reference).</param>
        public static void RemoveInvisibleCharacters(ref string Input)
        {
            if (string.IsNullOrEmpty(Input))
            {
                return;
            }
            //Remove common "invisible" characters
            var InvisibleChars = new[] { '\u200B', '\u200C', '\u200D', '\uFEFF', '\u00A0', '\u200b' };
            foreach (var Char in InvisibleChars)
            {
                Input = Input.Replace(Char.ToString(), "");
            }
        }

        /// <summary>
        /// Trims trailing newline characters from the translated text.
        /// Removes either CRLF ("\r\n") or LF ("\n") at the end of the string.
        /// </summary>
        /// <param name="TransText">The translated text to process (passed by reference).</param>
        public static void ProcessEmptyEndLine(ref string TransText)
        {
            TransText = Regex.Replace(TransText, @"((\r\n)|\n|\\n)+$", "");
        }

        /// <summary>
        /// Normalizes Chinese punctuation marks to their standard English equivalents.
        /// This ensures consistency in translated output, especially when targeting English text.
        /// </summary>
        /// <param name="Str">The string to normalize (passed by reference).</param>
        public static void NormalizePunctuation(ref string Str)
        {
            Str = Str.Replace("（", "(");
            Str = Str.Replace("）", ")");
            Str = Str.Replace("【", "[");
            Str = Str.Replace("】", "]");
            Str = Str.Replace("《", "<");
            Str = Str.Replace("》", ">");
            Str = Str.Replace("｛", "{");
            Str = Str.Replace("｝", "}");
            Str = Str.Replace("［", "[");
            Str = Str.Replace("］", "]");
            Str = Str.Replace("‘", "'");
            Str = Str.Replace("’", "'");
            Str = Str.Replace("“", "\"");
            Str = Str.Replace("”", "\"");
            Str = Str.Replace("＂", "\""); 
            Str = Str.Replace("。", ".");
            Str = Str.Replace("，", ",");
            Str = Str.Replace("：", ":");
            Str = Str.Replace("；", ";");
            Str = Str.Replace("？", "?");
            Str = Str.Replace("！", "!");
            Str = Str.Replace("、", ",");
            Str = Str.Replace("·", ".");
            Str = Str.Replace("——", "--");
            Str = Str.Replace("—", "-");
            Str = Str.Replace("…", "...");
            Str = Str.Replace("　", " ");
        }

        public static void StripOuterQuotes(ref string Input)
        {
            if (Input.Trim().Length == 0)
            {
                return;
            }
            int Start = 0;
            while (Start < Input.Length && (Input[Start] == '\\' || Input[Start] == '/' || Input[Start] == '"' || Input[Start] == '“' || Input[Start] == '”'))
            {
                Start++;
            }

            int End = Input.Length - 1;
            while (End >= Start && (Input[End] == '"' || Input[End] == '“' || Input[End] == '”'))
            {
                End--;
            }

            Input = Input.Substring(Start, End - Start + 1);
        }

        public static bool HasOuterQuotes(string Input)
        {
            if (string.IsNullOrEmpty(Input) || Input.Length < 2)
                return false;

            char First = Input[0];
            char Last = Input[Input.Length - 1];

            return (IsQuote(First) && IsQuote(Last));
        }

        static bool IsQuote(char c)
        {
            return c == '"' || c == '“' || c == '”';
        }

        public static void ConditionalSplitCamelCase(ref string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return;

            if (!Input.Contains(' '))
            {
                Input = Regex.Replace(Input, @"([a-z])([A-Z])", "$1 $2");
                Input = Regex.Replace(Input, @"([a-zA-Z])([0-9])", "$1 $2");
                Input = Regex.Replace(Input, @"\s+", " ");

                Input = Input.Trim();
            }

            return;
        }
    }
}
