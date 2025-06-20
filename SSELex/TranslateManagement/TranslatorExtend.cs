using SSELex.ConvertManager;
using SSELex.SkyrimManage;
using SSELex.UIManage;
using System.Windows.Media;
using PhoenixEngine.TranslateManagement;
using PhoenixEngine.TranslateCore;
using PhoenixEngine.TranslateManage;

namespace SSELex.TranslateManage
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/PhoenixEngine
    public class TranslatorExtend
    {

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
            ReSetAllTransText();

            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                FakeGrid GetFakeGrid = DeFine.WorkingWin.TransViewList.RealLines[i];
                GetFakeGrid.UPDataThis();

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

                DeFine.WorkingWin.TransViewList.RealLines[i].BorderColor = Colors.Green;
                GetFakeGrid.TransText = GetSourceText;
                GetFakeGrid.UPDateView();
            }

            DeFine.WorkingWin.ReloadData();
        }

        public static void ReSetAllTransText()
        {
            DeFine.WorkingWin.Dispatcher.Invoke(new Action(() => {
                DeFine.WorkingWin.TransViewList.MainCanvas.Visibility = System.Windows.Visibility.Collapsed;
            }));

            LocalDBCache.DeleteCacheByModName(DeFine.CurrentModName,DeFine.GlobalLocalSetting.TargetLanguage);
            Translator.TransData.Clear();
            DeFine.WorkingWin.ReloadData();

            DeFine.WorkingWin.Dispatcher.Invoke(new Action(() => {
                DeFine.WorkingWin.ProcessBar.Width = 0;
            }));
            DeFine.WorkingWin.Dispatcher.Invoke(new Action(() => {
                DeFine.WorkingWin.TransViewList.MainCanvas.Visibility = System.Windows.Visibility.Visible;
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
            UIHelper.ModifyCount = DeFine.WorkingWin.TransViewList.RealLines.Count;

            DeFine.WorkingWin.Dispatcher.Invoke(new Action(() => {
                DeFine.WorkingWin.ProcessBar.Width = 0;
            }));
        }

        public class QueryTransItem
        {
            public string Key = "";
            public string TransText = "";
            public Color Color;
            public int State = 0;
        }

        public class SetTransItem
        {
            public string Key = "";
            public Color Color;
            public int State = 0;
        }

        public static Color DefTransTextBorder = Color.FromRgb(87, 87, 87);
        public static QueryTransItem QueryTransData(string Key, string SourceText)
        {
            QueryTransItem NQueryTransItem = new QueryTransItem();

            string TransText = "";
            Color Color = DefTransTextBorder;

            string GetRamSource = "";
            if (Translator.TransData.ContainsKey(Key))
            {
                GetRamSource = Translator.TransData[Key];
            }

            var FindDictionary = YDDictionaryHelper.CheckDictionary(Key);

            if (GetRamSource.Trim().Length == 0)
            {
                TransText = LocalDBCache.GetCacheText(DeFine.CurrentModName,Key,DeFine.GlobalLocalSetting.TargetLanguage);

                if (TransText.Trim().Length > 0)
                {
                    Color = Colors.Green;
                }
                else
                {
                    TransText = CloudDBCache.FindCache(DeFine.CurrentModName,Key,DeFine.GlobalLocalSetting.TargetLanguage);

                    if (TransText.Trim().Length > 0)
                    {
                        Color = Colors.DarkOliveGreen;
                    }
                }

                if (DeFine.GlobalLocalSetting.AutoLoadDictionaryFile)
                {
                    if (FindDictionary != null)
                    {
                        if (FindDictionary.TransText.Trim().Length > 0)
                        {
                            TransText = FindDictionary.TransText;
                            Color = Colors.Green;
                        }
                    }
                }
                NQueryTransItem.State = 1;
            }
            else
            {
                var GetStr = CloudDBCache.FindCache(DeFine.CurrentModName,Key,DeFine.GlobalLocalSetting.TargetLanguage);
                TransText = GetRamSource;
                if (GetStr.Equals(GetRamSource))
                {
                    Color = Colors.DarkOliveGreen;
                }
                else
                {
                    Color = Colors.Green;
                }
                NQueryTransItem.State = 0;
            }


            NQueryTransItem.Key = Key;
            NQueryTransItem.TransText = TransText;
            NQueryTransItem.Color = Color;
            return NQueryTransItem;
        }

        public static SetTransItem SetTransData(string Key, string SourceText, string TransText)
        {
            SetTransItem NSetTransItem = new SetTransItem();
            NSetTransItem.Color = DefTransTextBorder;

            if (TransText.Trim().Length > 0)
            {
                Translator.TransData[Key] = TransText;
            }
            else
            {
                if (Translator.TransData.ContainsKey(Key))
                {
                    Translator.TransData.Remove(Key);
                }

                CloudDBCache.DeleteCache(DeFine.CurrentModName,Key,DeFine.GlobalLocalSetting.TargetLanguage);
            }

            bool CanUPDate = true;
            var FindDictionary = YDDictionaryHelper.CheckDictionary(Key);
            if (FindDictionary != null)
            {
                if (FindDictionary.TransText.Equals(TransText))
                {
                    LocalDBCache.DeleteCache(DeFine.CurrentModName,Key,DeFine.GlobalLocalSetting.TargetLanguage);
                    CanUPDate = false;
                }
                NSetTransItem.State = 0;
            }

            if (CanUPDate)
            {
                LocalDBCache.UPDateLocalTransItem(new LocalTransItem(DeFine.CurrentModName,Key,DeFine.GlobalLocalSetting.TargetLanguage, TransText));
                NSetTransItem.State = 1;
            }

            if (TransText.Trim().Length == 0)
            {
                NSetTransItem.Color = DefTransTextBorder;
            }
            else
            {
                if (TransText.Equals(CloudDBCache.FindCache(DeFine.CurrentModName,Key,DeFine.GlobalLocalSetting.TargetLanguage)))
                {
                    NSetTransItem.Color = Colors.DarkOliveGreen;
                }
                else
                {
                    NSetTransItem.Color = Colors.Green;
                }
            }

            return NSetTransItem;
        }
    }
}