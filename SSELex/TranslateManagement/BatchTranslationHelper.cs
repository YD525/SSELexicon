using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SSELex.ConvertManager;
using SSELex.TranslateCore;
using SSELex.UIManage;

namespace SSELex.TranslateManage
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class TransItem
    {
        public int WorkEnd = 0;
        public Thread CurrentTrd;
        public string Key = "";
        public string Type = "";
        public string SourceText = "";
        public string TransText = "";
        public FakeGrid Handle = null;
        public bool IsDuplicateSource = false;

        public bool Transing = false;

        private CancellationTokenSource TransThreadToken;

        public void SyncProgress()
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                DeFine.WorkingWin.GetStatistics();
            }));
        }

        public void StartWork()
        {
            if (this.IsDuplicateSource)
            {
                if (!BatchTranslationHelper.SameItems.ContainsKey(this.SourceText))
                {
                    BatchTranslationHelper.SameItems.Add(this.SourceText, string.Empty);
                }
                else
                {
                    this.Transing = false;
                    WorkEnd = 2;
                    return;
                }
            }
            WorkEnd = 1;
            this.Transing = true;
            CurrentTrd = new Thread(() =>
            {
                TransThreadToken = new CancellationTokenSource();
                var Token = TransThreadToken.Token;
                try
                {
                    NextGet:
                    Token.ThrowIfCancellationRequested();

                    if (this.SourceText.Trim().Length > 0)
                    {
                        if (this.Type.Equals("Book")&&(!Key.EndsWith("(Description)")&&!Key.EndsWith("(Name)")))
                        {
                            if (DeFine.WorkingWin != null)
                            {
                                DeFine.WorkingWin.SetLog("Skip Book fields:" + this.Key);
                            }

                            WorkEnd = 2;
                        }
                        else
                        if (this.Key.Contains("Score:") && this.Key.Contains(","))
                        {
                            string []Params = this.Key.Split(',');
                            if (Params.Length > 1)
                            {
                                if (Params[Params.Length - 1].Contains("Score:"))
                                {
                                    double GetScore = ConvertHelper.ObjToDouble(Params[Params.Length - 1].Substring(Params[Params.Length - 1].IndexOf("Score:") + "Score:".Length));
                                    if (GetScore < 5)
                                    {
                                        if (DeFine.WorkingWin != null)
                                        {
                                            DeFine.WorkingWin.SetLog("Skip dangerous fields:" + this.Key);
                                        }
                                        WorkEnd = 2;
                                    }
                                }
                            }
                        }

                        if (WorkEnd != 2)
                        {
                            bool CanSleep = true;
                            var GetResult = Translator.QuickTrans(this.SourceText, DeFine.TargetLanguage, ref CanSleep);
                            if (GetResult.Trim().Length > 0)
                            {
                                TransText = GetResult.Trim();

                                Token.ThrowIfCancellationRequested();

                                this.Handle.TransText = TransText;
                                this.Handle.BorderColor = Colors.Green;
                                this.Handle.UPDateView();

                                SyncProgress();

                                if (Translator.TransData.ContainsKey(this.Key))
                                {
                                    Translator.TransData[this.Key] = GetResult;
                                }
                                else
                                {
                                    Translator.TransData.Add(this.Key, GetResult);
                                }

                                if (this.IsDuplicateSource)
                                {
                                    if (BatchTranslationHelper.SameItems.ContainsKey(this.SourceText))
                                    {
                                        BatchTranslationHelper.SameItems[this.SourceText] = GetResult;
                                    }
                                }

                                WorkEnd = 2;
                            }
                            else
                            {
                                goto NextGet;
                            }
                        }
                       
                    }
                    else
                    {
                        SyncProgress();

                        WorkEnd = 2;
                    }
                }
                catch (OperationCanceledException)
                {
                    try {
                    this.Transing = false;
                    this.CurrentTrd = null;
                    }
                    catch  { }
                }
                this.Transing = false;
                this.CurrentTrd = null;
            });
            CurrentTrd.Start();
        }

        public void CancelWorkThread()
        {
            WorkEnd = 2;
            TransThreadToken?.Cancel();
        }

        public TransItem(string Key,string Type, string SourceText, string TransText, FakeGrid Handle)
        {
            this.Key = Key;
            this.Type = Type;
            this.SourceText = SourceText;
            this.TransText = TransText;
            this.Handle = Handle;
        }
    }

        
    public class BatchTranslationHelper
    {
        public static Dictionary<string, string> SameItems = new Dictionary<string, string>();
        public static List<TransItem> TransItems = new List<TransItem>();

        public static void MarkDuplicates(List<TransItem> Items)
        {
            var CountDict = new Dictionary<string, int>();

            foreach (var Item in Items)
            {
                string Key = Item.SourceText ?? "";
                if (CountDict.ContainsKey(Key))
                    CountDict[Key]++;
                else
                    CountDict[Key] = 1;
            }

            foreach (var Item in Items)
            {
                string Key = Item.SourceText ?? "";
                Item.IsDuplicateSource = CountDict[Key] > 1;
            }
        }

        public static int GetWorkCount()
        {
            int WorkCount = 0;
            for (int i = 0; i < TransItems.Count; i++)
            {
                if (TransItems[i].Transing)
                {
                    WorkCount++;
                }
            }
            return WorkCount;
        }

        public static void Init()
        {
            SameItems.Clear();
            TransItems.Clear();

            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                FakeGrid MainGrid = DeFine.WorkingWin.TransViewList.RealLines[i];

                TransItems.Add(new TransItem(MainGrid.Key, MainGrid.Type, MainGrid.SourceText, MainGrid.TransText, MainGrid));
            }

            MarkDuplicates(TransItems);
        }

        public static CancellationTokenSource TransMainTrdCancel = null;
        public static Thread TransMainTrd = null;

        public static int CurrentTrdCount = 0;

        public static void CancelMainTransThread()
        {
            TransMainTrdCancel?.Cancel();
        }
        public static int AutoSleep = 1;

        public static void SetDuplicateSource(string GetKey)
        {
            for (int ir = 0; ir < TransItems.Count; ir++)
            {
                if (TransItems[ir].SourceText.Equals(GetKey))
                {
                    TransItems[ir].Handle.TransText = SameItems[GetKey];
                    TransItems[ir].Handle.BorderColor = Colors.Green;
                    TransItems[ir].Handle.UPDateView();

                    if (Translator.TransData.ContainsKey(TransItems[ir].Key))
                    {
                        Translator.TransData[TransItems[ir].Key] = SameItems[GetKey];
                    }
                    else
                    {
                        Translator.TransData.Add(TransItems[ir].Key, SameItems[GetKey]);
                    }
                }
            }
        }
        public static void Start()
        {
            AutoSleep = 1;

            if (DeFine.GlobalLocalSetting.MaxThreadCount <= 0)
            {
                DeFine.GlobalLocalSetting.MaxThreadCount = 1;
            }

            DeFine.WorkingWin.Dispatcher.Invoke(new Action(() => {
                DeFine.WorkingWin.ThreadInFo.Visibility = System.Windows.Visibility.Visible;
            }));

            Init();

            TransMainTrd = new Thread(() =>
            {
                TransMainTrdCancel = new CancellationTokenSource();
                var Token = TransMainTrdCancel.Token;

                int CurrentTrds = 0;
                while (true)
                {
                    try
                    {
                        NextFind:
                        DeFine.WorkingWin.Dispatcher.Invoke(new Action(() => {
                            DeFine.WorkingWin.ThreadInFoLog.Content = string.Format("Thread(Current:{0},Max:{1})", CurrentTrds, DeFine.GlobalLocalSetting.MaxThreadCount);
                        }));

                        bool CanExit = true;
                        Token.ThrowIfCancellationRequested();
                        CurrentTrds = GetWorkCount();
                        if (CurrentTrds < DeFine.GlobalLocalSetting.MaxThreadCount)
                        {
                            for (int i = 0; i < TransItems.Count; i++)
                            {
                                if (TransItems[i].WorkEnd <= 0)
                                {
                                    TransItems[i].StartWork();
                                    CanExit = false;
                                    break;
                                }
                            }
                            if (CurrentTrds > DeFine.GlobalLocalSetting.MaxThreadCount / 2)
                            {
                                AutoSleep = 500;
                            }
                            Thread.Sleep(AutoSleep);
                        }

                        if (CanExit)
                        {
                            int SucessCount = 0;
                            for (int i = 0; i < TransItems.Count; i++)
                            {
                                if (TransItems[i].WorkEnd == 2)
                                {
                                    SucessCount++;
                                }
                            }
                            if (SucessCount == TransItems.Count)
                            {
                                if (SameItems != null)
                                {
                                    if (SameItems.Count > 0)
                                    {
                                        for (int i = 0; i < SameItems.Count; i++)
                                        {
                                            string GetKey = SameItems.ElementAt(i).Key;
                                            SetDuplicateSource(GetKey);
                                        }
                                    }
                                }

                                DeFine.WorkingWin.GetStatistics();

                                DeFine.WorkingWin.Dispatcher.Invoke(new Action(() =>
                                {
                                    DeFine.WorkingWin.ClosetTransTrd();
                                }));
                                return;
                            }
                            else
                            {
                                Thread.Sleep(1);
                                goto NextFind;
                            }
                        }
                    }
                    catch(OperationCanceledException) 
                    {
                        TransMainTrd = null;
                        return;
                    }

                    Thread.Sleep(1);
                }

            });

            TransMainTrd.Start();
        }

        public static void Close()
        {
            DeFine.WorkingWin.Dispatcher.Invoke(new Action(() => {
                DeFine.WorkingWin.ThreadInFo.Visibility = System.Windows.Visibility.Collapsed;
            }));

            try
            {
                CancelMainTransThread();
            }
            catch { }

            for (int i = 0; i < TransItems.Count; i++)
            {
                if (TransItems[i].Transing)
                {
                    try
                    {
                        TransItems[i].CancelWorkThread();
                    }
                    catch { }
                }
            }
           
            TransMainTrd = null;
        }


    }
}
