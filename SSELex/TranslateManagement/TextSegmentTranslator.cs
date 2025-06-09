using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using SSELex.TranslateManage;

namespace SSELex.TranslateManagement
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/
    public class Segment
    {
        public string Tag { get; set; } = "";
        public string RawContent { get; set; } = "";
        public string TextToTranslate { get; set; } = "";
    }
    public class TextSegmentTranslator
    {
        public string Source = "";
        public List<string> TransLines = new List<string>();
        public int TransCount = 0;
        public int CurrentTransCount = 0;
        public TextEditor LockerTextHandle = null;
        public Label ProcessTagHandle = null;

        public TextSegmentTranslator(TextEditor SetBox,Label SetProcessTag)
        {
            this.LockerTextHandle = SetBox;
            this.ProcessTagHandle = SetProcessTag;
        }

        private string StripHtmlTags(string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }
        public List<Segment> Load(string Input)
        {
            List<Segment> Segments = new List<Segment>();

            var Matches = Regex.Matches(Input,
                @"(\[[^\]]+\])\s*([\s\S]*?)(?=(\[[^\]]+\])|<[^<>]+>|$)" +            
                @"|<([a-zA-Z0-9]+(?:\s[^<>]*?)?)>([\s\S]*?)</\4>" +                 
                @"|^([\s\S]+?)(?=(\[[^\]]+\])|<[^<>]+>|$)",                         
                RegexOptions.Compiled);

            foreach (Match Match in Matches)
            {
                string Tag = "";
                string Content = "";

                if (!string.IsNullOrEmpty(Match.Groups[1].Value)) 
                {
                    Tag = Match.Groups[1].Value.Trim();
                    Content = Match.Groups[2].Value.Trim();
                }
                else if (!string.IsNullOrEmpty(Match.Groups[4].Value)) 
                {
                    string tagFull = Match.Groups[4].Value.Trim();                      
                    string inner = Match.Groups[5].Value;
                    string tagName = tagFull.Split(' ')[0];                            
                    string openTag = $"<{tagFull}>";
                    string closeTag = $"</{tagName}>";

                    Tag = openTag;
                    Content = (openTag + inner + closeTag).Trim();
                }
                else if (!string.IsNullOrEmpty(Match.Groups[6].Value))
                {
                    Tag = "";
                    Content = Match.Groups[6].Value.Trim();
                }

                string TextOnly = StripHtmlTags(Content).Trim();

                Segments.Add(new Segment
                {
                    Tag = Tag,
                    RawContent = Content,
                    TextToTranslate = string.IsNullOrWhiteSpace(TextOnly) ? null : TextOnly
                });
            }

            return Segments;
        }

        public void ApplyAllLine(string Source)
        {
            if (this.LockerTextHandle != null)
            {
                this.LockerTextHandle.Dispatcher.Invoke(new Action(() =>
                {
                    this.LockerTextHandle.Text = Source;
                }));
            }
            if (this.ProcessTagHandle != null)
            {
                this.ProcessTagHandle.Dispatcher.Invoke(new Action(() =>
                {
                    ProcessTagHandle.Content = string.Format("Translating ({0}/{1})... (Click to Cancel)", CurrentTransCount,TransCount);
                }));
            }
        }
        List<Segment> Content = new List<Segment>();
        public void TransBook(string Key,string Source, CancellationToken Token)
        {
            Content.Clear();
            this.Source = Source;
            Content = Load(Source);
            List<Segment> GetSegments = new List<Segment>();

            GetSegments.AddRange(Content);

            foreach (var Segment in GetSegments)
            {
                if (Segment.TextToTranslate != null)
                    foreach (var GetLine in Segment.TextToTranslate.Split(new char[2] { '\r', '\n' }))
                    {
                        if (GetLine.Trim().Length > 0)
                        {
                            TransCount++;
                        }
                    }
            }

            int LineID = 0;
            for (int i = 0; i < GetSegments.Count; i++)
            {
                if (GetSegments[i].TextToTranslate != null)
                    foreach (var GetSourceLine in GetSegments[i].TextToTranslate.Split(new char[2] { '\r', '\n' }))
                    {
                        if (GetSourceLine.Trim().Length > 0)
                        {
                            NextCall:
                            try
                            {
                                Token.ThrowIfCancellationRequested();
                            }
                            catch { return; }

                            bool CanSleep = false;
                            LineID++;
                            var GetTransLine = Translator.QuickTrans("Book",Key + LineID.ToString(),GetSourceLine,DeFine.SourceLanguage,DeFine.TargetLanguage,ref CanSleep,true);

                            if (GetTransLine.Trim().Length > 0)
                            {
                                Source = ReplaceFirst(Source,GetSourceLine, GetTransLine);
                                CurrentTransCount++;
                                ApplyAllLine(Source);
                            }
                            else
                            {
                                goto NextCall;
                            }
                        }
                    }
            }
        }

        public static string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0) return text;
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

    }
}
