using SSELex.ConvertManager;
using SSELex.SkyrimManage;
using System.Windows.Controls;
using SSELex.TranslateCore;
using static SSELex.TranslateManage.TransCore;
using SSELex.UIManage;
using System.Windows.Media;
using SSELex.TranslateManagement;
using Mutagen.Bethesda.Starfield;
using Reloaded.Memory.Pointers.Sourced;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace SSELex.TranslateManage
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/
    public class Translator
    {
        public delegate void TranslateMsg(string EngineName, string Text, string Result);

        public static TranslateMsg SendTranslateMsg;

        public static Dictionary<string, string> TransData = new Dictionary<string, string>();

        public static void ClearCache()
        {
            TransData.Clear();
        }

        public static void ClearAICache()
        {
            EngineSelect.AIMemory.Clear();
        }

        public static TransCore CurrentTransCore = new TransCore();

        public static string ReturnStr(string Str)
        {
            if (string.IsNullOrWhiteSpace(Str.Replace("　", "")))
            {
                return string.Empty;
            }
            else
            {
                return Str;
            }
        }

        public static bool IsOnlySymbolsAndSpaces(string Input)
        {
            return Regex.IsMatch(Input, @"^[\p{P}\p{S}\s]+$");
        }

        public static string QuickTrans(string Content,Languages To,ref bool CanSleep, bool IsBook = false)
        {
            string GetSourceStr = Content;

            if (IsOnlySymbolsAndSpaces(GetSourceStr))
            {
                return GetSourceStr;
            }

            if (GetSourceStr.Trim().Length == 0)
            {
                return GetSourceStr;
            } 

            bool HasOuterQuotes = TranslationPreprocessor.HasOuterQuotes(GetSourceStr.Trim());

            TranslationPreprocessor.ConditionalSplitCamelCase(ref Content);
            TranslationPreprocessor.RemoveInvisibleCharacters(ref Content);

            Languages SourceLanguage = LanguageHelper.DetectLanguageByLine(Content);
            if (SourceLanguage == To)
            {
                return GetSourceStr;
            }

            bool CanAddCache = true;
            Content = CurrentTransCore.TransAny(SourceLanguage, To, Content,IsBook,ref CanAddCache,ref CanSleep);

            TranslationPreprocessor.NormalizePunctuation(ref Content);
            TranslationPreprocessor.ProcessEmptyEndLine(ref Content);
            TranslationPreprocessor.RemoveInvisibleCharacters(ref Content);

            TranslationPreprocessor.StripOuterQuotes(ref Content);

            Content = Content.Trim();

            if (HasOuterQuotes)
            {
                Content = "\"" + HasOuterQuotes + "\"";
            }

            Content = ReturnStr(Content);

            if (CanAddCache && Content.Trim().Length>0)
            {
                TranslateDBCache.AddCache(DeFine.CurrentModName, GetSourceStr, (int)SourceLanguage, (int)To, Content);
            }

            return Content;
        }

        public static int WriteDictionary()
        {
            int ReplaceCount = 0;
            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                FakeGrid GetFakeGrid = DeFine.WorkingWin.TransViewList.RealLines[i];
                GetFakeGrid.UPDataThis();

                string GetKey = GetFakeGrid.Key;
                string GetSourceText = GetFakeGrid.SourceText;
                var TargetText = GetFakeGrid.TransText;

                YDDictionaryHelper.UPDateTransText(GetKey, GetSourceText, TargetText);
            }

            return ReplaceCount;
        }
        public static int ReadDictionary()
        {
            int ReplaceCount = 0;
            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                FakeGrid GetFakeGrid = DeFine.WorkingWin.TransViewList.RealLines[i];
                GetFakeGrid.UPDataThis();

                string GetTransKey = GetFakeGrid.Key;
              
                var TargetText = DeFine.WorkingWin.TransViewList.RealLines[i].TransText;

                var GetData = YDDictionaryHelper.CheckDictionary(GetTransKey);

                if (GetData != null && TargetText.Length == 0)
                {
                    DeFine.WorkingWin.TransViewList.RealLines[i].TransText = GetData.TransText;
                }
            }

            return ReplaceCount;
        }

        public static void ReStoreAllTransText()
        {
            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                FakeGrid GetFakeGrid = DeFine.WorkingWin.TransViewList.RealLines[i];
                GetFakeGrid.UPDataThis();

                string GetKey = GetFakeGrid.Key;
                var TargetText = GetFakeGrid.TransText;

                var GetSourceText = GetFakeGrid.SourceText;
                if (Translator.TransData.ContainsKey(GetKey))
                {
                    Translator.TransData[GetKey] = GetSourceText;
                }
                DeFine.WorkingWin.TransViewList.RealLines[i].TransText = GetSourceText;

                DeFine.WorkingWin.TransViewList.RealLines[i].BorderColor = Colors.Green;
            }
            DeFine.WorkingWin.GetStatistics();
        }

        public static void ReSetAllTransText()
        {
            string EMP = "";
            for (int i = 0; i < DeFine.WorkingWin.TransViewList.RealLines.Count; i++)
            {
                FakeGrid GetFakeGrid = DeFine.WorkingWin.TransViewList.RealLines[i];
                GetFakeGrid.UPDataThis();

                string GetKey = GetFakeGrid.Key;
                var TargetText = GetFakeGrid.TransText;

                DeFine.WorkingWin.TransViewList.RealLines[i].TransText = string.Empty;

                if (Translator.TransData.ContainsKey(GetKey))
                {
                    Translator.TransData[GetKey] = EMP;
                    DeFine.WorkingWin.TransViewList.RealLines[i].TransText = EMP;
                    if (Translator.TransData.ContainsKey(GetKey))
                    {
                        Translator.TransData.Remove(GetKey);
                    }
                    DeFine.WorkingWin.TransViewList.RealLines[i].BorderColor = Color.FromRgb(76, 76, 76);

                    DeFine.WorkingWin.TransViewList.RealLines[i].UPDateView();
                }
            }
            DeFine.WorkingWin.GetStatistics();
            DeFine.WorkingWin.Dispatcher.Invoke(new Action(() => {
                DeFine.WorkingWin.ProcessBar.Width = 0;
            }));
        }

        public static int ReplaceAllLine(string Key, string Value)
        {
            int ReplaceCount = 0;
            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                FakeGrid GetFakeGrid = DeFine.WorkingWin.TransViewList.RealLines[i];
                GetFakeGrid.UPDataThis();

                string GetKey = ConvertHelper.ObjToStr(GetFakeGrid.Key);
                var TargetText = GetFakeGrid.TransText;
                if (TargetText.Trim().Length > 0)
                {
                    TargetText = TargetText.Replace(Key, Value);
                    Translator.TransData[GetKey] = TargetText;
                    DeFine.WorkingWin.TransViewList.RealLines[i].TransText = TargetText;

                    DeFine.WorkingWin.TransViewList.RealLines[i].UPDateView();
                    ReplaceCount++;
                }
            }

            return ReplaceCount;
        }

        private void TestAll(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                FakeGrid GetFakeGrid = DeFine.WorkingWin.TransViewList.RealLines[i];
                GetFakeGrid.UPDataThis();
                string GetKey = GetFakeGrid.Key;
                Translator.TransData[GetKey] = i.ToString();

                DeFine.WorkingWin.TransViewList.RealLines[i].TransText = i.ToString();
                DeFine.WorkingWin.TransViewList.RealLines[i].BorderColor = Color.FromRgb(76, 76, 76);

                DeFine.WorkingWin.TransViewList.RealLines[i].UPDateView();
            }
            UIHelper.ModifyCount = DeFine.WorkingWin.TransViewList.RealLines.Count;
            DeFine.WorkingWin.GetStatistics();
            DeFine.WorkingWin.Dispatcher.Invoke(new Action(() => {
                DeFine.WorkingWin.ProcessBar.Width = 0;
            }));
        }
    }
}
