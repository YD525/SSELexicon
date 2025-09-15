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
using static PhoenixEngine.EngineManagement.DataTransmission;
using static SSELex.TranslateManage.TranslatorExtend;
using System.Windows.Controls;

namespace SSELex.TranslateManage
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/SSELexicon
    public class TranslatorExtend
    {
        public static void Init()
        {
            DelegateHelper.SetDataCall += Recv;
            DelegateHelper.SetTranslationUnitCallBack += TranslationUnitStartWorkCall;

            RegListener("PreLog", new List<int>() { 2 }, new Action<int, object>((Sign, Any) =>
            {
                if (Sign == 2)
                {
                    if (Any is PreTranslateCall)
                    {
                        PreTranslateCall GetCall = (PreTranslateCall)Any;

                        UIHelper.NodeCallCallback(GetCall.Platform);
                    }
                }
            }));

            RegListener("MainLog", new List<int>() { 0 }, new Action<int, object>((Sign, Any) =>
            {
                if (Sign == 0)
                {
                    if (Any is string)
                    {
                        LogHelper.SetMainLog((string)Any);
                    }
                }
            }));

            RegListener("InputOutputLog", new List<int>() { 3, 5 }, new Action<int, object>((Sign, Any) =>
            {
                if (Sign == 5 || Sign == 3)
                {
                    if (Any is AICall)
                    {
                        AICall GetCall = (AICall)Any;

                        UIHelper.NodeCallCallback(GetCall.Platform);

                        LogHelper.SetInputLog(GetCall.Platform.ToString() + "->\n" + GetCall.SendString);
                        LogHelper.SetOutputLog(GetCall.Platform.ToString() + "->\n" + GetCall.ReceiveString);

                        DashBoardService.TokenStatistics(GetCall.Platform, GetCall.SendString, GetCall.ReceiveString);
                    }
                    if (Any is PlatformCall)
                    {
                        PlatformCall GetCall = (PlatformCall)Any;

                        UIHelper.NodeCallCallback(GetCall.Platform);

                        LogHelper.SetInputLog(GetCall.Platform.ToString() + "->\n" + GetCall.SendString);
                        LogHelper.SetOutputLog(GetCall.Platform.ToString() + "->\n" + GetCall.ReceiveString);
                    }
                }
            }));
        }

        /// <summary>
        /// Protect the translation object being entered by the user from being changed
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public static bool TranslationUnitStartWorkCall(TranslationUnit Item)
        {
            if (DeFine.WorkingWin != null)
            {
                if (DeFine.WorkingWin.TransViewList != null)
                {
                    FakeGrid? QueryGrid = DeFine.WorkingWin.TransViewList.KeyToFakeGrid(Item.Key);

                    if (QueryGrid != null)
                    {
                        bool IsCloud = false;
                        QueryGrid.SyncData(ref IsCloud);

                        if (QueryGrid.TransText.Length > 0)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public class RecvListener
        {
            public string Key = "";
            public List<int> ActiveIDs = new List<int>();
            public Action<int, object> Method = null;

            public RecvListener(string Key, List<int> ActiveIDs, Action<int, object> Func)
            {
                this.Key = Key;
                this.ActiveIDs = ActiveIDs;
                this.Method = Func;
            }
        }

        private static ReaderWriterLockSlim ListenersLock = new ReaderWriterLockSlim();
        public static void RemoveListener(string Key)
        {
            ListenersLock.EnterWriteLock();
            try
            {
                for (int i = 0; i < RecvListeners.Count; i++)
                {
                    if (RecvListeners[i].Key.Equals(Key))
                    {
                        RecvListeners.RemoveAt(i);
                        break;
                    }
                }
            }
            finally
            {
                ListenersLock.ExitWriteLock();
            }
        }

        public static void RegListener(string Key, List<int> ActiveIDs, Action<int, object> Action)
        {
            ListenersLock.EnterWriteLock();
            try
            {
                foreach (var Get in RecvListeners)
                {
                    if (Get.Key.Equals(Key))
                    {
                        return;
                    }
                }

                RecvListeners.Add(new RecvListener(Key, ActiveIDs, Action));
            }
            finally
            {
                ListenersLock.ExitWriteLock();
            }
        }

        public static List<RecvListener> RecvListeners = new List<RecvListener>();

        //Null = 0, CacheCall = 1, PreTranslateCall = 2, PlatformCall = 3, AICall = 5
        public static void Recv(int Sign, object Any)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    for (int i = 0; i < RecvListeners.Count; i++)
                    {
                        if (RecvListeners[i].ActiveIDs.Contains(Sign))
                        {
                            RecvListeners[i].Method.Invoke(Sign, Any);
                        }
                    }
                }
                catch { }
            });
        }

        public static void LogCall(string Log)
        {
            if (DeFine.WorkingWin != null)
            {
                DeFine.WorkingWin.Dispatcher.Invoke(new Action(() =>
                {
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

        public static void SetTranslatorHistoryCache(string Key, string Translated, bool IsCloud)
        {
            int GetKey = Key.GetHashCode();

            if (!TranslatorHistoryCaches.ContainsKey(GetKey))
            {
                TranslatorHistoryCaches.Add(GetKey, new List<TranslatorHistoryCache>());
            }

            if (!TranslatorHistoryCaches[GetKey].Any(C => C.Translated == Translated))
            {
                TranslatorHistoryCaches[GetKey].Add(new TranslatorHistoryCache(Translated, IsCloud));
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

        public static void SetTransBarTittle(string Log)
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
                    if (EngineConfig.AutoSetThreadLimit)
                    {
                        EngineConfig.MaxThreadCount = EngineConfig.AutoCalcThreadLimit();
                    }

                    YDListView? GetListView = DeFine.WorkingWin.TransViewList;

                    if (GetListView != null)
                    {
                        SyncTransStateFreeze = true;

                        ProxyCenter.UsingProxy();

                        SetTransBarTittle("Preparing Translation Units...");

                        List<TranslationUnit> TranslationUnits = new List<TranslationUnit>();

                        for (int i = 0; i < GetListView.Rows; i++)
                        {
                            var Row = GetListView.RealLines[i];
                            bool IsCloud = false;
                            Row.SyncData(ref IsCloud);

                            if (Row.TransText.Trim().Length == 0)
                            {
                                bool CanSet = true;

                                if (Row.Type.Equals("Book"))
                                {
                                    if (Row.Key.EndsWith("(BookText)"))
                                    {
                                        if (DelegateHelper.SetDataCall != null)
                                        {
                                            DelegateHelper.SetDataCall(0, "Skip Book fields:" + Row.Key);
                                        }

                                        CanSet = false;
                                    }
                                }
                                else
                                if (Row.Score < 5)
                                {
                                    if (DelegateHelper.SetDataCall != null)
                                    {
                                        DelegateHelper.SetDataCall(0, "Skip Dangerous fields:" + Row.Key);
                                    }

                                    CanSet = false;
                                }

                                if (CanSet)
                                {
                                    TranslationUnits.Add(new TranslationUnit(Engine.GetFileUniqueKey(),
                                  Row.Key, Row.Type, Row.SourceText, Row.TransText, "", Engine.From, Engine.To,Row.Score));
                                }
                            }
                        }

                        TranslationCore = new BatchTranslationCore(Engine.From, Engine.To, TranslationUnits);

                        TranslationCore.Start();

                        SyncTransStateFreeze = false;

                        EndAction.Invoke();

                        while (TranslationCore.WorkState <= 1)
                        {
                            Thread.Sleep(100);

                            SetTransBarTittle("Analyzing Words(" + TranslationCore.MarkLeadersPercent + "%)...");

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

                        SetTransBarTittle(string.Format("STRINGS({0}/{1})", ModifyCount, GetListView.Rows));

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
                                    SetTranslatorHistoryCache(GetGrid.Key, GetGrid.TransText, true);

                                    Engine.TranslatedCount++;
                                    SetTransBarTittle(string.Format("STRINGS({0}/{1})", Engine.TranslatedCount, GetListView.Rows));
                                }
                            }

                            Thread.Sleep(20);

                            if (WaitStopSign())
                            {
                                return;
                            }
                        }

                        TranslationStatus = StateControl.Cancel;

                        Engine.TranslatedCount = Engine.GetTranslatedCount(Engine.GetFileUniqueKey());

                        if (GetListView != null)
                        {
                            GetListView.QuickRefresh();
                        }

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

                    if (TranslationCore != null)
                    {
                        try 
                        { 
                            TranslationCore.Close();
                        }
                        catch { }
                    }

                    EndAction.Invoke();

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

                YDDictionaryHelper.UPDateTransText(GetKey, GetSourceText);
            }

            return ReplaceCount;
        }

        public static void ClearLocalCache(int FileUniqueKey)
        {
            LocalDBCache.DeleteCacheByFileUniqueKey(FileUniqueKey, DeFine.GlobalLocalSetting.TargetLanguage);
            Translator.TransData.Clear();
        }

        public static bool ClearCloudCache(int FileUniqueKey)
        {
            return CloudDBCache.ClearCloudCache(FileUniqueKey);
        }
    }

    public class TranslatorHistoryCache
    {
        public DateTime ChangeTime;
        public string Translated = "";
        public bool IsCloud = false;

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