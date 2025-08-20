using SSELex.ConvertManager;
using SSELex.SkyrimManage;
using SSELex.UIManage;
using System.Windows.Media;
using PhoenixEngine.TranslateManagement;
using PhoenixEngine.TranslateCore;
using PhoenixEngine.TranslateManage;
using PhoenixEngine.EngineManagement;
using static PhoenixEngine.SSELexiconBridge.NativeBridge;
using Loqui.Translators;
using SSELex.UIManagement;

namespace SSELex.TranslateManage
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/PhoenixEngine
    public class TranslatorExtend
    {
        public static Dictionary<int,List<TranslatorHistoryCache>> TranslatorHistoryCaches = new Dictionary<int, List<TranslatorHistoryCache>>();

        public static void ClearTranslatorHistoryCache()
        {
            RowStyleWin.RecordModifyStates.Clear();
            TranslatorHistoryCaches.Clear();
        }

        public static void SetTranslatorHistoryCache(string Key, string Translated)
        {
            int GetKey = Key.GetHashCode();

            if (!TranslatorHistoryCaches.ContainsKey(GetKey))
            {
                TranslatorHistoryCaches.Add(GetKey, new List<TranslatorHistoryCache>());
            }

            if (!TranslatorHistoryCaches[GetKey].Any(C => C.Translated == Translated))
            {
                TranslatorHistoryCaches[GetKey].Add(new TranslatorHistoryCache(Translated));
            }
        }

        public static List<TranslatorHistoryCache>? GetTranslatorCache(string Key)
        {
            int GetKey = Key.GetHashCode();
            if (TranslatorHistoryCaches.ContainsKey(GetKey))
            {
               return TranslatorHistoryCaches[GetKey];
            }

            return null;
        }


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

        public static void ClearLocalCache(string ModName)
        {
            LocalDBCache.DeleteCacheByModName(ModName, DeFine.GlobalLocalSetting.TargetLanguage);
            Translator.TransData.Clear();
        }

        public static void ReSetAllTransText()
        {
            if (DeFine.WorkingWin != null)
            {
                if (DeFine.WorkingWin.TransViewList != null)
                {
                    for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
                    {
                        DeFine.WorkingWin.TransViewList.RealLines[i].SyncData();

                        if (DeFine.WorkingWin.TransViewList.RealLines[i].TransText.Length > 0)
                        {
                            DeFine.WorkingWin.Dispatcher.Invoke(new Action(() => {
                                DeFine.WorkingWin.TransViewList.RealLines[i].SyncUI(DeFine.WorkingWin.TransViewList);
                            }));
                        }
                    }
                }
            }
        }
      
        public static bool ClearCloudCache(string ModName)
        {
            return CloudDBCache.ClearCloudCache(ModName);
        }
    }

    public class TranslatorHistoryCache
    {
        public DateTime ChangeTime;
        public string Translated = "";
        public bool IsCloud = false;

        public TranslatorHistoryCache(string Translated)
        {
            this.ChangeTime = DateTime.Now;
            this.Translated = Translated;
        }

        public TranslatorHistoryCache(string Translated,bool IsCloud)
        {
            this.ChangeTime = DateTime.Now;
            this.Translated = Translated;
            this.IsCloud = IsCloud;
        }
    }
    public enum StateControl
    {
        Null = 0, Run = 1, Stop = 2, Cancel = 3
    }
}