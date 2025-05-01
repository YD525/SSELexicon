using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSELex.TranslateManagement
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/
    public class TranslationPreprocessorExtend
    {
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
            var InvisibleChars = new[] { '\u200B', '\u200C', '\u200D', '\uFEFF', '\u00A0' };
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
            if (TransText.EndsWith("\r\n"))
            {
                TransText = TransText.Substring(0, TransText.Length - "\r\n".Length);
            }
            else
            if (TransText.EndsWith("\n"))
            {
                TransText = TransText.Substring(0, TransText.Length - "\n".Length);
            }
        }

        /// <summary>
        /// Normalizes Chinese punctuation marks to their standard English equivalents.
        /// This ensures consistency in translated output, especially when targeting English text.
        /// </summary>
        /// <param name="Str">The string to normalize (passed by reference).</param>
        public static void NormalizePunctuation(ref string Str)
        {
            Str = Str.Replace("“", "\"");

            Str = Str.Replace("”", "\"");

            Str = Str.Replace("。", ".");

            Str = Str.Replace("！", "!");

            Str = Str.Replace("，", ",");

            Str = Str.Replace("：", ":");

            Str = Str.Replace("？", "?");
        }

    }
}
