using Mutagen.Bethesda.Starfield;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using SSELex;
using SSELex.ConvertManager;
using SSELex.SkyrimManage;
using SSELex.TranslateManage;
using SSELex.UIManage;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static SSELex.TranslateCore.LanguageHelper;

namespace SSELex.TranslateCore
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class WordProcess
    {
        private static CancellationTokenSource AutoMainTransThread;

        public static void CancelMainTransThread()
        {
            AutoMainTransThread?.Cancel();
        }

        public static Languages DetectLanguage()
        {
            Languages Lang = Languages.English;

            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                Grid MainGrid = DeFine.WorkingWin.TransViewList.RealLines[i];
                if ((MainGrid.Children[4] as TextBox).Text.Trim().Length == 0)
                {
                    var GetTransText = (MainGrid.Children[3] as TextBox).Text.Trim();

                    if (Regex.IsMatch(GetTransText, "[\u4e00-\u9fff]+"))
                    {
                        if (Regex.IsMatch(GetTransText, "[\uFA0E\uFA0F\uFA11\uFA13\uFA14\uFA1F\uFA21\uFA23\uFA24\uFA27\uFA28\uFA29\u9FA5\u9FD5\u9FFF]"))
                        {
                            Lang = Languages.TraditionalChinese;
                        }
                        else
                        {
                            Lang = Languages.SimplifiedChinese;
                        }
                    }
                    if (Regex.IsMatch(GetTransText, "[\u3040-\u30ff]+"))
                    {
                        Lang = Languages.Japanese;
                    }
                    if (Regex.IsMatch(GetTransText, "[\uac00-\ud7af]+"))
                    {
                        Lang = Languages.Korean;
                    }
                    if (Regex.IsMatch(GetTransText, "[\u00C0-\u017F]+"))
                    {
                        Lang = Languages.German;
                    }
                    if (Regex.IsMatch(GetTransText, "[\u0400-\u04FF]+"))
                    {
                        Lang = Languages.Turkish;
                    }
                    if (Regex.IsMatch(GetTransText, "[\u00C0-\u00FF]+|[a-zA-Z]+") && GetTransText.Contains("ã") || GetTransText.Contains("õ"))
                    {
                        Lang = Languages.Brazilian;
                    }
                }
            }

            return Lang;
        }

        public static int ReplaceAllLine(string Key,string Value)
        {
            int ReplaceCount = 0;
            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                Grid MainGrid = DeFine.WorkingWin.TransViewList.RealLines[i];
                int GetTransHashKey = ConvertHelper.ObjToInt(MainGrid.Tag);
                var TargetText = ConvertHelper.ObjToStr((MainGrid.Children[4] as TextBox).Text);
                if (TargetText.Trim().Length > 0)
                {
                    TargetText = TargetText.Replace(Key, Value);
                    Translator.TransData[GetTransHashKey] = TargetText;
                    (MainGrid.Children[4] as TextBox).Text = TargetText;
                    ReplaceCount++;
                }
            }

            return ReplaceCount;
        }

        public static void ReSetAllTransText()
        {
            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                Grid MainGrid = DeFine.WorkingWin.TransViewList.RealLines[i];
                int GetTransHashKey = ConvertHelper.ObjToInt(MainGrid.Tag);
                var TargetText = ConvertHelper.ObjToStr((MainGrid.Children[4] as TextBox).Text);
                if (TargetText.Trim().Length > 0)
                {
                    Translator.TransData[GetTransHashKey] = string.Empty;
                    (MainGrid.Children[4] as TextBox).Text = string.Empty;
                    if (Translator.TransData.ContainsKey(GetTransHashKey))
                    {
                        Translator.TransData.Remove(GetTransHashKey);
                    }
                    (MainGrid.Children[4] as TextBox).BorderBrush = new SolidColorBrush(Color.FromRgb(76, 76, 76));
                }
            }
            UIHelper.ModifyCount = 0;
            DeFine.WorkingWin.GetStatistics();
            DeFine.WorkingWin.Dispatcher.Invoke(new Action(() => {
                DeFine.WorkingWin.ProcessBar.Width = 0;
            }));
        }
        public static void ReStoreAllTransText()
        {
            UIHelper.ModifyCount = 0;
            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                Grid MainGrid = DeFine.WorkingWin.TransViewList.RealLines[i];
                int GetTransHashKey = ConvertHelper.ObjToInt(MainGrid.Tag);
                var TargetText = ConvertHelper.ObjToStr((MainGrid.Children[4] as TextBox).Text);
                var GetSourceText = (MainGrid.Children[3] as TextBox).Text.Trim();
                if (Translator.TransData.ContainsKey(GetTransHashKey))
                {
                    Translator.TransData[GetTransHashKey] = GetSourceText;
                    UIHelper.ModifyCount++;
                }
                (MainGrid.Children[4] as TextBox).Text = GetSourceText;

                (MainGrid.Children[4] as TextBox).BorderBrush = new SolidColorBrush(Colors.Green);
            }
            DeFine.WorkingWin.GetStatistics();
        }

        public static int ReadDictionary()
        {
            int ReplaceCount = 0;
            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                Grid MainGrid = DeFine.WorkingWin.TransViewList.RealLines[i];
                int GetTransHashKey = ConvertHelper.ObjToInt(MainGrid.Tag);
                string GetKey = ConvertHelper.ObjToStr((MainGrid.Children[2] as TextBox).Text);
                var TargetText = ConvertHelper.ObjToStr((MainGrid.Children[4] as TextBox).Text);

                var GetData = YDDictionaryHelper.CheckDictionary(GetKey);

                if (GetData != null)
                {
                    (MainGrid.Children[4] as TextBox).Text = GetData.TransText;
                }
            }

            return ReplaceCount;
        }

        public static int WriteDictionary()
        {
            int ReplaceCount = 0;
            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                Grid MainGrid = DeFine.WorkingWin.TransViewList.RealLines[i];
                int GetTransHashKey = ConvertHelper.ObjToInt(MainGrid.Tag);
                string GetKey = ConvertHelper.ObjToStr((MainGrid.Children[2] as TextBox).Text);
                var TargetText = ConvertHelper.ObjToStr((MainGrid.Children[4] as TextBox).Text);
                string GetTransText = (MainGrid.Children[3] as TextBox).Text.Trim();

                YDDictionaryHelper.UPDateTransText(GetKey, GetTransText, TargetText);
            }

            return ReplaceCount;
        }

        //public static Thread MainTransThread = null;
        //public static bool LockerAutoTransService = false;
        //public static void StartAutoTransService(bool Check)
        //{
        //    if (Check)
        //    {
        //        if (!LockerAutoTransService)
        //        {
        //            LockerAutoTransService = true;

        //            AutoMainTransThread = new CancellationTokenSource();
        //            var Token = AutoMainTransThread.Token;

        //            MainTransThread = new Thread(() =>
        //            {
        //                try
        //                {
        //                    Task.Delay(200, Token).Wait(Token);

        //                    LanguageHelper.Init();

        //                    Token.ThrowIfCancellationRequested();

        //                    int GetTransCount = 0;

        //                    DeFine.WorkingWin.Dispatcher.Invoke(new Action(() =>
        //                    {
        //                        GetTransCount = DeFine.WorkingWin.TransViewList.Rows;
        //                    }));

        //                    for (int i = 0; i < GetTransCount; i++)
        //                    {
        //                        if (!LockerAutoTransService)
        //                        {
        //                            MainTransThread = null;
        //                            return;
        //                        }

        //                        int GetTransHashKey = 0;
        //                        string GetTransText = "";
        //                        string GetTag = "";
        //                        string GetKey = "";
        //                        string GetTilp = "";
        //                        string TargetText = "";

        //                        Grid MainGrid = null;
        //                        DeFine.WorkingWin.Dispatcher.Invoke(new Action(() =>
        //                        {
        //                            MainGrid = DeFine.WorkingWin.TransViewList.RealLines[i];
        //                            GetTilp = ConvertHelper.ObjToStr(MainGrid.ToolTip);
        //                            GetTag = ConvertHelper.ObjToStr((MainGrid.Children[1] as Label).Content);
        //                            GetKey = ConvertHelper.ObjToStr((MainGrid.Children[2] as TextBox).Text);
        //                            TargetText = ConvertHelper.ObjToStr((MainGrid.Children[4] as TextBox).Text);
        //                            GetTransText = (MainGrid.Children[3] as TextBox).Text.Trim();
        //                            GetTransHashKey = ConvertHelper.ObjToInt(MainGrid.Tag);
        //                        }));

        //                        if (GetTransText.Trim().Length > 0 && GetTransHashKey != 0)
        //                        {
        //                            if (GetTilp == "Danger")
        //                            {
        //                                DeFine.WorkingWin.SetLog("Skip dangerous fields:" + GetKey);
        //                            }
        //                            else
        //                            if (GetTag.ToLower() != "book")
        //                            {
        //                                if (TargetText.Trim().Length == 0)
        //                                {
        //                                    NextGet:
        //                                    if (!LockerAutoTransService)
        //                                    {
        //                                        MainTransThread = null;
        //                                        return;
        //                                    }

        //                                    List<EngineProcessItem> EngineProcessItems = new List<EngineProcessItem>();
        //                                    var GetResult = new WordProcess().ProcessWords(ref EngineProcessItems, GetTransText, DeFine.SourceLanguage, DeFine.TargetLanguage);
        //                                    if (GetResult.Trim().Length > 0)
        //                                    {
        //                                        Translator.TransData[GetTransHashKey] = GetResult;

        //                                        DeFine.WorkingWin.Dispatcher.Invoke(new Action(() =>
        //                                        {
        //                                            if (UIHelper.ModifyCount < DeFine.WorkingWin.TransViewList.RealLines.Count)
        //                                            {
        //                                                UIHelper.ModifyCount++;
        //                                            }

        //                                            DeFine.WorkingWin.GetStatistics();

        //                                            (MainGrid.Children[4] as TextBox).Text = GetResult;
        //                                            (MainGrid.Children[4] as TextBox).BorderBrush = new SolidColorBrush(Colors.Green);
        //                                        }));
        //                                    }
        //                                    else
        //                                    {
        //                                        DeFine.WorkingWin.SetLog("The interface returns empty and requests again.");
        //                                        if (!StrChecker.ContainsChinese(GetTransText))
        //                                        {
        //                                            Thread.Sleep(new Random(Guid.NewGuid().GetHashCode()).Next(500, 2000));
        //                                            goto NextGet;
        //                                        }
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    DeFine.WorkingWin.SetLog("Skip the translated content:" + GetTransText);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                DeFine.WorkingWin.SetLog("Skip the translation BOOK field:" + GetKey);
        //                            }

        //                        }
        //                    }

        //                    LockerAutoTransService = false;
        //                    MainTransThread = null;

        //                    DeFine.WorkingWin.SetLog("The current tag translation ends");

        //                    if (DeFine.WorkingWin != null)
        //                    {
        //                        DeFine.WorkingWin.Dispatcher.Invoke(new Action(() => {
        //                            DeFine.WorkingWin.CheckTransTrdState();
        //                        }));
        //                    }
        //                }
        //                catch (OperationCanceledException)
        //                {
        //                    // 取消时静默处理
        //                    DeFine.WorkingWin.SetLog("Cancel Trans");
        //                    MainTransThread = null;
        //                    DeFine.WorkingWin.Dispatcher.Invoke(new Action(() => {
        //                        DeFine.WorkingWin.CheckTransTrdState();
        //                    }));
        //                }
        //            });

        //            MainTransThread.Start();
        //        }
        //    }
        //    else
        //    {
        //        CancelMainTransThread();
        //        LockerAutoTransService = false;
        //    }
        //}

        public delegate void TranslateMsg(string EngineName, string Text, string Result);

        public static TranslateMsg SendTranslateMsg;

       
        public static void FormatText(ref string Str)
        {
            Str = Str.Replace("。", ".").Replace("，", ",").Replace("(", " ( ").Replace(")", " ) ").Replace("（", " ( ").Replace("）", " ) ").Replace("【", "[").Replace("】", "]").Replace("《", "<").Replace("》", ">").Replace("‘", " ‘ ").Replace("'", " ‘ ");
            Str = Str.Trim();
        }

        public string QuickTranslate(string OneWord, Languages From, Languages To, int WordType = 0)
        {
            string SqlOrder = "Select [Result] From Languages Where [From] = '{0}' And [To] = '{1}' And Type = '{2}' And Text = '{3}' COLLATE NOCASE";

            string TranslateWord = ConvertHelper.ObjToStr(DeFine.GlobalDB.ExecuteScalar(string.Format(SqlOrder, (int)From, (int)To, WordType, OneWord)));

            if (TranslateWord.Trim().Length > 0)
            {
                FormatText(ref TranslateWord);
                return "（" + TranslateWord.Trim().Replace("\r\n", "") + "）";
            }
            else
            {
                return null;
            }
        }
        public string GetTranslate(ref List<EngineProcessItem> EngineMsgs, string OneWord, Languages From, Languages To, int WordType = 0)
        {
            string TempLine = "";
            string RichText = "";

            for (int i = 0; i < OneWord.Length; i++)
            {
                string Char = OneWord.Substring(i, 1);

                if (Char == "," || Char == "." || Char == "/" || Char == "<" || Char == ">" || Char == "|" || Char == "!" || Char == "\"" || Char == "'")
                {
                    string GetResult = QuickTranslate(TempLine, From, To, WordType);

                    if (GetResult != null)
                    {
                        EngineMsgs.Add(new EngineProcessItem("Languages", TempLine, GetResult, 1));
                    }

                    if (GetResult != null)
                    {
                        TempLine = GetResult;

                        RichText += TempLine + Char;
                        TempLine = string.Empty;
                    }
                    else
                    {
                        RichText += TempLine + Char;
                        TempLine = string.Empty;
                    }
                }
                else
                {
                    TempLine += Char;
                }
            }


            if (TempLine.Trim().Length > 0)
            {
                string GetResult = QuickTranslate(TempLine, From, To, WordType);

                if (GetResult != null)
                {
                    EngineMsgs.Add(new EngineProcessItem("Languages", TempLine, GetResult, 2));
                }

                if (GetResult != null)
                {
                    RichText += GetResult;
                }
                else
                {
                    RichText += TempLine;
                }
            }


            return RichText.Trim();
        }

        public string ProcessWordGroups(ref List<EngineProcessItem> EngineMsgs, string Content, Languages From, Languages To)
        {
            if (!DeFine.GlobalLocalSetting.PhraseEngineUsing) return Content;
            int MaxLength = 2;

            string RichText = "";

            string WordGroup = "";

            string TempStr = "";

            foreach (var Get in Content.Split(' '))
            {
                if (Get.Trim().Length > 0)
                {
                    if (MaxLength > 0)
                    {
                        MaxLength--;
                        WordGroup += Get + " ";
                        TempStr += Get + " ";
                    }

                    if (MaxLength == 0)
                    {
                        MaxLength = 2;

                        var Result = QuickTranslate(WordGroup.Trim(), From, To, 1);

                        if (Result != null)
                        {
                            EngineMsgs.Add(new EngineProcessItem("Languages", WordGroup.Trim(), Result, 0));
                        }

                        if (Result != null)
                        {
                            RichText += Result + " ";
                        }
                        else
                        {
                            RichText += WordGroup + " ";
                        }


                        WordGroup = string.Empty;
                        TempStr = string.Empty;
                    }
                }
            }

            if (TempStr.Trim().Length > 0)
            {
                RichText += TempStr;
                TempStr = string.Empty;
            }

            return RichText.Trim();
        }


        public bool HasChinese(string str)
        {
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        }

        public bool HasEnglish(string str)
        {
            char[] Chars = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            foreach (var Get in Chars)
            {
                if (str.Contains(Get.ToString().ToUpper()))
                {
                    return true;
                }
                if (str.Contains(Get.ToString().ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
        public int GetEnglishCount(string str)
        {
            int EngCount = 0;

            char[] Chars = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            for (int i = 0; i < str.Length; i++)
            {
                string GetChar = str.Substring(i, 1);
                foreach (var Get in Chars)
                {
                    if (GetChar.Equals(Get.ToString().ToUpper()))
                    {
                        EngCount++;
                    }
                    if (GetChar.Equals(Get.ToString().ToLower()))
                    {
                        EngCount++;
                    }
                }

            }
            return EngCount;
        }
        public string ProcessContent(string Content)
        {

            return Content;
        }

        public static void ClearAICache()
        {
            EngineSelect.AIMemory.Clear();
        }

        public LanguageHelper CurrentLanguageHelper = new LanguageHelper();
        public string QuickTrans(string Content, Languages From, Languages To)
        {
            if (Content.Trim().Length == 0) return string.Empty;

            string RealContent = Content;

            if (Content.Contains("u200b"))
            {
                Content = Content.Replace(@"\u200b", "");
            }

            Content = Content.Replace("\u200b", "").Replace("\u200B", "");

            Content = CurrentLanguageHelper.TransAny(DeFine.SourceLanguage, DeFine.TargetLanguage,RealContent,Content);

            Content = Content.Replace("“", "'");

            Content = Content.Replace("”", "'");

            Content = Content.Replace("。", ".");

            Content = Content.Replace("！", "!");

            Content = Content.Replace("，", ",");

            Content = Content.Replace("：", ":");

            Content = Content.Replace("？", "?");

            Content = Content.Replace("-", " - ");

            Content = Content.Replace("\u200b", "").Replace("\u200B", "");

            return Content;
        }

        public string CurrentLine = "";
        public string ProcessWords(ref List<EngineProcessItem> EngineProcessItems, string Content, Languages From, Languages To)
        {
            //Thread.Sleep(1000);
            //return "1";
            string RealContent = Content;
            if (Content.Trim().Length == 0) return string.Empty;

            if (Content.Contains("u200b"))
            {
                Content = Content.Replace(@"\u200b", "");
            }

            Content = Content.Replace("\u200b", "").Replace("\u200B", "");

            if (CurrentLine != Content)
            {
                CurrentLine = Content;
            }
            else
            {
                CurrentLine = Content;
            }

            string TempTextA = Content;

            FormatText(ref Content);

            WordProcess.SendTranslateMsg("Step0 String Correction", TempTextA, Content);

            Content = ProcessWordGroups(ref EngineProcessItems, Content, From, To);

            string RichText = "";

            if (DeFine.GlobalLocalSetting.PhraseEngineUsing)
            {
                var Contents = Content.Split(' ');

                foreach (var Get in Contents)
                {
                    if (Get.Trim().Length > 0)
                    {
                        string GetWord = Get;

                        GetWord = GetTranslate(ref EngineProcessItems, GetWord, From, To) + " ";

                        RichText += GetWord;
                    }
                }

                RichText = RichText.Trim();
            }
            else
            {
                RichText = Content;
            }

            //‘s 的 //in 在里面
            TranslateCache CreatTranslate = new TranslateCache(ref EngineProcessItems, RichText);

            WordProcess.SendTranslateMsg("Step1 Local Translation Engine(污)", Content, CreatTranslate.Content);

            string TempValue = CreatTranslate.Content;

            if (CreatTranslate.GetRemainingText(CreatTranslate.Content).Trim().Length > 0)
            {
                var GetTrans = new LanguageHelper().TransAny(DeFine.SourceLanguage, DeFine.TargetLanguage, RealContent, CreatTranslate.Content);

                string TempText = CreatTranslate.Content;
                CreatTranslate.Content = GetTrans;

                EngineProcessItems.Add(new EngineProcessItem("CloudEngine", TempText, CreatTranslate.Content, 0));
            }
            else
            {

            }

            CreatTranslate.UsingDBWord();

            if (DeFine.GlobalLocalSetting.DivCacheEngineUsing)
            {
                string TempStr = CreatTranslate.Content;

                DivTranslateEngine.UsingCacheEngine(ref CreatTranslate.Content);

                WordProcess.SendTranslateMsg("Custom Engine", TempStr, CreatTranslate.Content);
            }

            WordProcess.SendTranslateMsg("Final Result", "Joint Translation", CreatTranslate.Content);

            CreatTranslate.Content = CreatTranslate.Content.Replace("“", "'");

            CreatTranslate.Content = CreatTranslate.Content.Replace("”", "'");

            CreatTranslate.Content = CreatTranslate.Content.Replace("。", ".");

            CreatTranslate.Content = CreatTranslate.Content.Replace("！", "!");

            CreatTranslate.Content = CreatTranslate.Content.Replace("，", ",");

            CreatTranslate.Content = CreatTranslate.Content.Replace("：", ":");

            CreatTranslate.Content = CreatTranslate.Content.Replace("？", "?");

            CreatTranslate.Content = CreatTranslate.Content.Replace("-", " - ");

            CreatTranslate.Content = CreatTranslate.Content.Replace("\u200b", "").Replace("\u200B", "");

            return CreatTranslate.Content;
        }



    }

    public class EngineProcessItem
    {
        public string EngineName = "";
        public string Text = "";
        public string Result = "";
        public int State = 0;

        public EngineProcessItem(string EngineName, string Text, string Result, int State)
        {
            this.EngineName = EngineName;
            this.Text = Text;
            this.Result = Result;
            this.State = State;
        }
    }



    public class CardItem
    {
        public string Str = "";
        public string EN = "";
        public CardItem(string Str, string EN)
        {
            this.Str = Str;
            this.EN = EN;
        }
    }

    public class TranslateCache
    {
        public string Content = "";
        public List<CardItem> CardItems = new List<CardItem>();
        public CodeProcess CodeParsing = new CodeProcess();
        public ConjunctionHelper Conjunction = new ConjunctionHelper();

        public TranslateCache(ref List<EngineProcessItem> EngineMsgs, string Msg)
        {
            if (DeFine.GlobalLocalSetting.CodeParsingEngineUsing)
            {
                this.Content = this.CodeParsing.ProcessCode(Msg);
            }
            else
            {
                this.Content = Msg;
            }

            if (DeFine.GlobalLocalSetting.ConjunctionEngineUsing && DeFine.SourceLanguage == Languages.English && (DeFine.TargetLanguage == Languages.SimplifiedChinese || DeFine.TargetLanguage == Languages.TraditionalChinese))
            {
                this.Content = this.Conjunction.ProcessStr(ref EngineMsgs, this.Content);
            }

            if (DeFine.GlobalLocalSetting.PhraseEngineUsing && DeFine.SourceLanguage == Languages.English && (DeFine.TargetLanguage == Languages.SimplifiedChinese || DeFine.TargetLanguage == Languages.TraditionalChinese))
            {
                this.Content = DetachDBWord(this.Content, ref CardItems);
            }
        }

        public string DetachDBWord(string Content, ref List<CardItem> Cards)
        {
            bool IsSign = false;

            string SignText = "";
            string RichText = "";

            int AutoAdd = 0;

            for (int i = 0; i < Content.Length; i++)
            {
                string Char = Content.Substring(i, 1);

                if (Char == "）")
                {
                    if (SignText.Trim().Length > 0)
                    {
                        AutoAdd++;

                        string NewName = "30" + AutoAdd.ToString() + "03";

                        CardItem OneCard = new CardItem(SignText, NewName);

                        Cards.Add(OneCard);

                        RichText += "（" + OneCard.EN + "）";

                        SignText = String.Empty;
                    }

                    IsSign = false;
                }

                if (IsSign)
                {
                    SignText += Char;
                }

                if (Char == "（")
                {
                    IsSign = true;
                }

                if (Char != "（" && Char != "）" && IsSign == false)
                {
                    RichText += Char;
                }
            }


            return RichText;
        }

        public void UsingDBWord()
        {
            string GetText = this.Content;

            if (DeFine.GlobalLocalSetting.CodeParsingEngineUsing)
            {
                GetText = CodeParsing.UsingCode(GetText);
            }

            if (DeFine.GlobalLocalSetting.ConjunctionEngineUsing)
            {
                GetText = Conjunction.UsingStr(GetText);
            }

            if (DeFine.GlobalLocalSetting.PhraseEngineUsing)
            {
                foreach (var Get in this.CardItems)
                {
                    GetText = GetText.Replace(" （" + Get.EN.ToString() + "） ", Get.Str);
                    GetText = GetText.Replace("（" + Get.EN.ToString() + "）", Get.Str);
                    GetText = GetText.Replace(" (" + Get.EN.ToString() + ") ", Get.Str);
                    GetText = GetText.Replace("(" + Get.EN.ToString() + ")", Get.Str);
                    GetText = GetText.Replace(Get.EN.ToString(), Get.Str);
                }

                GetText = GetText.Replace("  ", " ").Replace(" ( ", "(").Replace(" ) ", ")").Replace(" )", ")").Replace("( ", "(").Replace(" (", "(").Replace(") ", ")").Replace(" ‘ ", "‘").Replace("  ‘  ", "‘").Replace(" ‘", "‘").Replace("‘ ", "‘").Trim();
            }

            this.Content = GetText;
        }

        public string GetRemainingText(string Text)
        {
            string GetText = Text;

            if (DeFine.GlobalLocalSetting.CodeParsingEngineUsing)
            {
                GetText = this.CodeParsing.GetRemainingText(GetText);
            }

            if (DeFine.GlobalLocalSetting.ConjunctionEngineUsing)
            {
                GetText = this.Conjunction.GetRemainingText(GetText);
            }

            if (DeFine.GlobalLocalSetting.PhraseEngineUsing)
            {
                foreach (var Get in this.CardItems)
                {
                    GetText = GetText.Replace("（" + Get.EN.ToString() + "）", string.Empty);
                    GetText = GetText.Replace("(" + Get.EN.ToString() + ")", string.Empty);
                    GetText = GetText.Replace(Get.EN.ToString(), string.Empty);
                }
            }

            WordProcess.FormatText(ref GetText);

            GetText = GetText.Replace(".", "").Replace(",", "").Replace("-", "").Replace(":", "").Replace("'", "").Replace("\"", "");
            GetText = GetText.Replace(">", "").Replace("<", "");
            GetText = GetText.Replace(" ", "");

            return GetText;
        }
    }
}
