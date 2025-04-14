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
        public string ItemType = "";
        public string Type = "";
        public string SourceText = "";
        public string TransText = "";
        public TextBox TextBoxHandle = null;

        public bool Transing = false;

        private CancellationTokenSource TransThreadToken;

        public void SyncProgress()
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                if (UIHelper.ModifyCount < DeFine.WorkingWin.TransViewList.RealLines.Count)
                {
                    UIHelper.ModifyCount++;
                }

                DeFine.WorkingWin.GetStatistics();
            }));
        }

        public void StartWork()
        {
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
                        if (ItemType.Equals("Book"))
                        {
                            if (DeFine.WorkingWin != null)
                            {
                                DeFine.WorkingWin.SetLog("Skip Book fields:" + this.Key);
                            }

                            WorkEnd = 2;
                        }
                        else
                        if (Type.Equals("Danger"))
                        {
                            if (DeFine.WorkingWin != null)
                            {
                                DeFine.WorkingWin.SetLog("Skip dangerous fields:" + this.Key);
                            }

                            WorkEnd = 2;
                        }
                        else
                        {
                            var GetResult = new WordProcess().QuickTrans(this.SourceText, DeFine.SourceLanguage, DeFine.TargetLanguage);
                            if (GetResult.Trim().Length > 0)
                            {
                                TransText = GetResult.Trim();

                                Token.ThrowIfCancellationRequested();

                                TextBoxHandle.Dispatcher.Invoke(new Action(() =>
                                {
                                    TextBoxHandle.Text = TransText;
                                    TextBoxHandle.BorderBrush = new SolidColorBrush(Colors.Green);
                                }));

                                SyncProgress();

                                if (Translator.TransData.ContainsKey(this.Key))
                                {
                                    Translator.TransData[this.Key] = GetResult;
                                }
                                else
                                {
                                    Translator.TransData.Add(this.Key, GetResult);
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

        public TransItem(string Key, int HashID,string ItemType,string Type, string SourceText, string TransText, TextBox TextBoxHandle)
        {
            this.Key = Key;
            this.ItemType = ItemType;
            this.Type = Type;
            this.SourceText = SourceText;
            this.TransText = TransText;
            this.TextBoxHandle = TextBoxHandle;
        }
    }
    public class BatchTranslationHelper
    {
        public static List<TransItem> TransItems = new List<TransItem>();

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
            TransItems.Clear();

            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                Grid MainGrid = DeFine.WorkingWin.TransViewList.RealLines[i];

                int HashID = 0;
                string Key = "";
                string SourceText = "";
                string TransText = "";
                string Type = "";
                string ItemType = "";
                TextBox TextBoxHandle = null;

                DeFine.WorkingWin.TransViewList.GetMainGrid().Dispatcher.Invoke(new Action(() =>
                {
                    ItemType = ConvertHelper.ObjToStr((MainGrid.Children[1] as Label).Content);
                    Type = ConvertHelper.ObjToStr(MainGrid.ToolTip);
                    HashID = ConvertHelper.ObjToInt(MainGrid.Tag);
                    SourceText = (MainGrid.Children[3] as TextBox).Text.Trim();
                    TransText = ConvertHelper.ObjToStr((MainGrid.Children[4] as TextBox).Text);
                    TextBoxHandle = (MainGrid.Children[4] as TextBox);
                    Key = ConvertHelper.ObjToStr((MainGrid.Children[2] as TextBox).Text);
                    if (TransText.Trim().Length == 0)
                    {
                        TransItems.Add(new TransItem(Key, HashID,ItemType,Type, SourceText, TransText, TextBoxHandle));
                    }
                }));
            }
        }

        public static CancellationTokenSource TransMainTrdCancel = null;
        public static Thread TransMainTrd = null;

        public static int CurrentTrdCount = 0;

        public static void CancelMainTransThread()
        {
            TransMainTrdCancel?.Cancel();
        }
        public static int AutoSleep = 1;
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
                                AutoSleep = 600;
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
