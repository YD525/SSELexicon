using SSELex.ConvertManager;
using SSELex.SkyrimManage;
using SSELex.UIManage;
using System.Windows.Media;
using PhoenixEngine.TranslateManagement;
using PhoenixEngine.TranslateCore;
using PhoenixEngine.TranslateManage;
using PhoenixEngine.EngineManagement;
using static PhoenixEngine.SSELexiconBridge.NativeBridge;

namespace SSELex.TranslateManage
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/PhoenixEngine
    public class TranslatorExtend
    {
        public static StateControl TranslationStatus = StateControl.Null;

        public static void SyncTransState()
        {
            if (TranslationStatus == StateControl.Run)
            { 
            
            }
            else
            if (TranslationStatus == StateControl.Stop)
            {

            }
            else
            if (TranslationStatus == StateControl.Cancel || TranslationStatus == StateControl.Null)
            {

            }
        }

        public static int WriteDictionary()
        {
            int ReplaceCount = 0;
            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                FakeGrid GetFakeGrid = DeFine.WorkingWin.TransViewList.RealLines[i];

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
            ReSetAllTransText();

            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                FakeGrid GetFakeGrid = DeFine.WorkingWin.TransViewList.RealLines[i];

                string GetKey = GetFakeGrid.Key;

                var FindDictionary = YDDictionaryHelper.CheckDictionary(GetKey);

                string GetSourceText = GetFakeGrid.SourceText;

                if (FindDictionary != null)
                {
                    GetSourceText = FindDictionary.OriginalText;
                }

                if (Translator.TransData.ContainsKey(GetKey))
                {
                    Translator.TransData[GetKey] = GetSourceText;
                }
                else
                {
                    Translator.TransData[GetKey] = GetSourceText;
                }

                DeFine.WorkingWin.TransViewList.RealLines[i].TransText = GetSourceText;

                GetFakeGrid.TransText = GetSourceText;
            }

            DeFine.WorkingWin.ReloadData();
        }

        public static void ReSetAllTransText()
        {
            DeFine.WorkingWin.Dispatcher.Invoke(new Action(() =>
            {
                DeFine.WorkingWin.TransViewList.MainCanvas.Visibility = System.Windows.Visibility.Collapsed;
            }));

            LocalDBCache.DeleteCacheByModName(Engine.GetModName(), DeFine.GlobalLocalSetting.TargetLanguage);
            Translator.TransData.Clear();
            DeFine.WorkingWin.ReloadData();

            DeFine.WorkingWin.Dispatcher.Invoke(new Action(() =>
            {
                DeFine.WorkingWin.ProcessBar.Width = 0;
            }));
            DeFine.WorkingWin.Dispatcher.Invoke(new Action(() =>
            {
                DeFine.WorkingWin.TransViewList.MainCanvas.Visibility = System.Windows.Visibility.Visible;
            }));
        }

        public static int ReplaceAllLine(string Key, string Value)
        {
            int ReplaceCount = 0;
            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                FakeGrid GetFakeGrid = DeFine.WorkingWin.TransViewList.RealLines[i];

                string GetKey = ConvertHelper.ObjToStr(GetFakeGrid.Key);
                var TargetText = GetFakeGrid.TransText;
                if (TargetText.Trim().Length > 0)
                {
                    TargetText = TargetText.Replace(Key, Value);
                    Translator.TransData[GetKey] = TargetText;
                    DeFine.WorkingWin.TransViewList.RealLines[i].TransText = TargetText;

                    ReplaceCount++;
                }
            }

            return ReplaceCount;
        }

        public static bool ClearCloudCache(string ModName)
        {
            return CloudDBCache.ClearCloudCache(ModName);
        }

        public static void TestAll()
        {
            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                FakeGrid GetFakeGrid = DeFine.WorkingWin.TransViewList.RealLines[i];
                string GetKey = GetFakeGrid.Key;
                Translator.TransData[GetKey] = i.ToString();
            }

            DeFine.WorkingWin.Dispatcher.Invoke(new Action(() =>
            {
                DeFine.WorkingWin.ProcessBar.Width = 0;
            }));
        }
    }

    public enum StateControl
    {
        Null = 0, Run = 1, Stop = 2, Cancel = 3
    }
}