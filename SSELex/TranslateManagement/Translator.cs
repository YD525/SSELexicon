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
        public static string QuickTrans(string Content, Languages From, Languages To,ref bool CanSleep, bool IsBook = false)
        {
            if (Content.Trim().Length == 0) return string.Empty;

            string GetSourceStr = Content;

            bool CanAddCache = true;

            bool HasOuterQuotes = TranslationPreprocessor.HasOuterQuotes(Content.Trim());

            TranslationPreprocessor.RemoveInvisibleCharacters(ref Content);

            Content = CurrentTransCore.TransAny(DeFine.SourceLanguage, DeFine.TargetLanguage, Content,IsBook,ref CanAddCache,ref CanSleep);

            TranslationPreprocessor.NormalizePunctuation(ref Content);
            TranslationPreprocessor.ProcessEmptyEndLine(ref Content);
            TranslationPreprocessor.RemoveInvisibleCharacters(ref Content);

            TranslationPreprocessor.StripOuterQuotes(Content);
            Content = Content.Trim();

            if (HasOuterQuotes)
            {
                Content = "\"" + HasOuterQuotes + "\"";
            }

            Content = ReturnStr(Content);

            if (CanAddCache)
            {
                TranslateDBCache.AddCache(GetSourceStr, (int)From, (int)To, Content);
            }

            return Content;
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

        public static void ReStoreAllTransText()
        {
            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                Grid MainGrid = DeFine.WorkingWin.TransViewList.RealLines[i];
                string GetKey = ConvertHelper.ObjToStr(MainGrid.Tag);
                var TargetText = ConvertHelper.ObjToStr((MainGrid.Children[4] as TextBox).Text);
                var GetSourceText = (MainGrid.Children[3] as TextBox).Text.Trim();
                if (Translator.TransData.ContainsKey(GetKey))
                {
                    Translator.TransData[GetKey] = GetSourceText;
                }
                (MainGrid.Children[4] as TextBox).Text = GetSourceText;

                (MainGrid.Children[4] as TextBox).BorderBrush = new SolidColorBrush(Colors.Green);
            }
            DeFine.WorkingWin.GetStatistics();
        }

        public static void ReSetAllTransText()
        {
            string EMP = "";
            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                Grid MainGrid = DeFine.WorkingWin.TransViewList.RealLines[i];
                string GetKey = ConvertHelper.ObjToStr(MainGrid.Tag);
                var TargetText = ConvertHelper.ObjToStr((MainGrid.Children[4] as TextBox).Text);
                if (TargetText.Trim().Length > 0)
                {
                    Translator.TransData[GetKey] = EMP;
                    (MainGrid.Children[4] as TextBox).Text = EMP;
                    if (Translator.TransData.ContainsKey(GetKey))
                    {
                        Translator.TransData.Remove(GetKey);
                    }
                    (MainGrid.Children[4] as TextBox).BorderBrush = new SolidColorBrush(Color.FromRgb(76, 76, 76));
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
                Grid MainGrid = DeFine.WorkingWin.TransViewList.RealLines[i];
                string GetKey = ConvertHelper.ObjToStr(MainGrid.Tag);
                var TargetText = ConvertHelper.ObjToStr((MainGrid.Children[4] as TextBox).Text);
                if (TargetText.Trim().Length > 0)
                {
                    TargetText = TargetText.Replace(Key, Value);
                    Translator.TransData[GetKey] = TargetText;
                    (MainGrid.Children[4] as TextBox).Text = TargetText;
                    ReplaceCount++;
                }
            }

            return ReplaceCount;
        }
    }
}
