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
using Microsoft.VisualBasic.Logging;
using PhoenixEngine.DelegateManagement;
using Mutagen.Bethesda.Plugins;
using PhoenixEngine.RequestManagement;

namespace SSELex.TranslateManage
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/PhoenixEngine
    public class TranslatorExtend
    {
        public static void Init()
        {
            DelegateHelper.SetLog += LogCall;
        }

        public static void LogCall(string Log, int LogViewType)
        {
            if (DeFine.WorkingWin != null)
            {
                DeFine.WorkingWin.Dispatcher.Invoke(new Action(() => {
                    DeFine.WorkingWin.MainLog.Text = Log;
                }));
            }
        }

        public static Dictionary<int, List<TranslatorHistoryCache>> TranslatorHistoryCaches = new Dictionary<int, List<TranslatorHistoryCache>>();

        public static BatchTranslationCore? TranslationCore = null;
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

        public static void SetLog(string Log)
        {
            if (DeFine.WorkingWin != null)
            {
                DeFine.WorkingWin.Dispatcher.Invoke(new Action(() =>
                {
                    DeFine.WorkingWin.TransProcess.Content = Log;
                }));
            }
        }

        public static bool WaitStopSign()
        {
            while (TranslationStatus == StateControl.Stop)
            {
                Thread.Sleep(1000);

                if (TranslationStatus == StateControl.Cancel)
                {
                    if (TranslationCore != null)
                    {
                        TranslationCore.Close();
                    }

                    return true;
                }
            }

            if (TranslationStatus == StateControl.Cancel)
            {
                if (TranslationCore != null)
                {
                    TranslationCore.Close();
                }

                return true;
            }

            return false;
        }

        public static bool SyncTransStateFreeze = false;

        public static StateControl TranslationStatus = StateControl.Null;

        public static void SyncTransState(Action EndAction, bool IsKeep = false)
        {
            if (SyncTransStateFreeze)
            {
                EndAction.Invoke();
                return;
            }

            if (DeFine.WorkingWin == null)
            {
                return;
            }

            new Thread(() =>
            {
                if (TranslationStatus == StateControl.Run && !IsKeep)
                {
                    YDListView? GetListView = DeFine.WorkingWin.TransViewList;

                    if (GetListView != null)
                    {
                        SyncTransStateFreeze = true;

                        ProxyCenter.UsingProxy();

                        SetLog("Engine Initialization(1/2)");

                        List<TranslationUnit> TranslationUnits = new List<TranslationUnit>();

                        for (int i = 0; i < GetListView.Rows; i++)
                        {
                            var Row = GetListView.RealLines[i];
                            Row.SyncData();

                            if (Row.TransText.Trim().Length == 0)
                            {
                                bool CanSet = true;

                                if (Row.Key.EndsWith("(BookText)") && DeFine.WorkingWin.CurrentTransType == 2)
                                {
                                    DeFine.WorkingWin.SetLog("Skip Book fields:" + Row.Key);

                                    CanSet = false;
                                }
                                else
                                if (Row.Key.Contains("Score:") && Row.Key.Contains(",") && DeFine.WorkingWin.CurrentTransType == 3)
                                {
                                    string[] Params = Row.Key.Split(',');
                                    if (Params.Length > 1)
                                    {
                                        if (Params[Params.Length - 1].Contains("Score:"))
                                        {
                                            double GetScore = ConvertHelper.ObjToDouble(Params[Params.Length - 1].Substring(Params[Params.Length - 1].IndexOf("Score:") + "Score:".Length));

                                            if (GetScore < 5)
                                            {
                                                DeFine.WorkingWin.SetLog("Skip Dangerous fields:" + Row.Key);

                                                CanSet = false;
                                            }
                                        }
                                    }
                                }

                                if (CanSet)
                                {
                                    TranslationUnits.Add(new TranslationUnit(Engine.GetModName(),
                                  Row.Key, Row.Type, Row.SourceText, Row.TransText));
                                }
                            }
                        }

                        SetLog("Engine Initialization(2/2)");

                        TranslationCore = new BatchTranslationCore(Engine.From, Engine.To, TranslationUnits);

                        TranslationCore.Start();

                        SyncTransStateFreeze = false;

                        EndAction.Invoke();

                        while (TranslationCore.WorkState <= 1)
                        {
                            Thread.Sleep(500);

                            SetLog("Word Sorting(" + TranslationCore.MarkLeadersPercent + "%)...");

                            if (WaitStopSign())
                            {
                                return;
                            }
                        }

                        if (WaitStopSign())
                        {
                            EndAction.Invoke();
                            return;
                        }

                        int ModifyCount = Engine.TranslatedCount;

                        SetLog(string.Format("STRINGS({0}/{1})", ModifyCount, GetListView.Rows));

                        Thread.Sleep(1000);

                        if (WaitStopSign())
                        {
                            EndAction.Invoke();
                            return;
                        }

                        bool IsEnd = false;

                        while (!IsEnd)
                        {
                            var GetGrid = TranslationCore.DequeueTranslated(out IsEnd);

                            if (GetGrid != null)
                            {
                                var GetFakeGrid = GetListView.KeyToFakeGrid(GetGrid.Key);
                                if (GetFakeGrid != null)
                                {
                                    GetFakeGrid.TransText = GetGrid.TransText;
                                    GetFakeGrid.SyncUI(GetListView);

                                    Engine.TranslatedCount++;
                                    SetLog(string.Format("STRINGS({0}/{1})", Engine.TranslatedCount, GetListView.Rows));
                                }
                            }

                            Thread.Sleep(20);

                            if (WaitStopSign())
                            {
                                return;
                            }
                        }

                        TranslationStatus = StateControl.Cancel;

                        EndAction.Invoke();
                    }
                }
                else
                if (TranslationStatus == StateControl.Stop)
                {
                    SyncTransStateFreeze = true;

                    if (TranslationCore != null)
                    {
                        TranslationCore.Stop();
                    }
                    
                    EndAction.Invoke();

                    SyncTransStateFreeze = false;
                }
                else
                if (TranslationStatus == StateControl.Cancel || TranslationStatus == StateControl.Null)
                {
                    SyncTransStateFreeze = true;

                    EndAction.Invoke();

                    if (TranslationCore != null)
                    {
                        while (TranslationCore.TransMainTrd != null)
                        {
                            Thread.Sleep(10);
                        }
                    }

                    SyncTransStateFreeze = false;
                }
                else
                {
                    SyncTransStateFreeze = true;

                    if (TranslationCore != null)
                    {
                        TranslationCore.Keep();
                    }

                    EndAction.Invoke();

                    SyncTransStateFreeze = false;
                }
            }).Start();
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

                        DeFine.WorkingWin.Dispatcher.Invoke(new Action(() =>
                        {
                            DeFine.WorkingWin.TransViewList.RealLines[i].SyncUI(DeFine.WorkingWin.TransViewList);
                        }));
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

        public TranslatorHistoryCache(string Translated, bool IsCloud)
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