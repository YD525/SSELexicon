using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SSELex.TranslateCore;
using SSELex.TranslateManage;

namespace SSELex.TranslateManagement
{
    public class TextTokenizer
    {

        private static readonly HashSet<string> EnglishStopWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "a", "an", "the", "and", "or", "but", "if", "then", "else",
            "on", "in", "at", "to", "for", "with", "by", "of", "is",
            "it", "this", "that", "these", "those", "as", "are", "was",
            "were", "be", "been", "has", "have", "had", "do", "does", "did",
            "so", "such", "not", "no", "nor", "too", "very", "can", "will",
            "just", "up", "down", "out", "over", "under", "again", "more",
            "most", "some", "any", "each", "all"
        };

        public const int MaxGram = 3 + 1;

        public static string[] Tokenize(Languages Lang, string Text)
        {
            Text = Text.Replace('_', ' ').Replace('-', ' ');

            if (Lang.IsSpaceDelimitedLanguage())
            {
                Text = Regex.Replace(Text, "(?<!^)([A-Z])", " $1");
                var tokens = Text.Split(new[] { ' ', '.', ',', '?', '!', ';', ':', '(', ')', '[', ']', '{', '}', '"', '\'' },
                    StringSplitOptions.RemoveEmptyEntries);

                return tokens
                    .Where(t => t.Length > 1)
                    .Where(t => !(Lang == Languages.English && EnglishStopWords.Contains(t)))
                    .ToArray();
            }

            if (!Lang.IsNoSpaceLanguage())
            {
                return Text.Split(new[] { ' ', '.', ',', '?', '!', ';', ':', '(', ')', '[', ']', '{', '}', '"', '\'' },
                    StringSplitOptions.RemoveEmptyEntries)
                    .Where(t => t.Length > 1)
                    .ToArray();
            }

            List<(string Token, int Index)> TokensWithIndex = new();
            for (int I = 0; I < Text.Length; I++)
            {
                TokensWithIndex.Add((" " + Text[I] + " ", I));
            }

            List<string> Result = new();

            string[] SingleTokens = TokensWithIndex.Select(T => T.Token).ToArray();
            int[] Indices = TokensWithIndex.Select(T => T.Index).ToArray();

            for (int I = 0; I < SingleTokens.Length; I++)
            {
                for (int Len = 1; Len <= MaxGram && I + Len <= SingleTokens.Length; Len++)
                {
                    bool IsContinuous = true;
                    for (int J = I; J < I + Len - 1; J++)
                    {
                        if (Indices[J + 1] != Indices[J] + 1)
                        {
                            IsContinuous = false;
                            break;
                        }
                    }
                    if (!IsContinuous) continue;

                    var TokenSb = new System.Text.StringBuilder();
                    for (int K = I; K < I + Len; K++)
                    {
                        TokenSb.Append(SingleTokens[K]);
                    }
                    string Token = TokenSb.ToString().Replace(" ", "");
                    if (!string.IsNullOrWhiteSpace(Token) && Token.Length > 1)
                    {
                        Result.Add(Token);
                    }
                }
            }

            return Result.ToArray();
        }
    }
}
