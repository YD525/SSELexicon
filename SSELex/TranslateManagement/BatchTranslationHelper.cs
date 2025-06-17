using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SSELex.ConvertManager;
using SSELex.SkyrimManage;
using SSELex.TranslateCore;
using SSELex.TranslateManagement;
using SSELex.UIManage;
using SSELex.UIManagement;

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
        public bool Leader = false;

        private CancellationTokenSource TransThreadToken;

        public void StartWork()
        {
            UPDataThis();

            if (this.TransText.Trim().Length > 0)
            {
                WorkEnd = 2;
                return;
            }

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
                        if (this.Type.Equals("Book") && (!Key.EndsWith("(Description)") && !Key.EndsWith("(Name)")))
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
                            string[] Params = this.Key.Split(',');
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
                            var GetResult = Translator.QuickTrans(DeFine.CurrentModName,this.Type,this.Key, this.SourceText, DeFine.SourceLanguage, DeFine.TargetLanguage, ref CanSleep);
                            if (GetResult.Trim().Length > 0)
                            {
                                TransText = GetResult.Trim();

                                Token.ThrowIfCancellationRequested();

                                this.Handle.TransText = TransText;
                                this.Handle.BorderColor = Colors.Green;
                                this.Handle.UPDateView();

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
                        WorkEnd = 2;
                    }
                }
                catch (OperationCanceledException)
                {
                    try
                    {
                        this.Transing = false;
                        this.CurrentTrd = null;
                    }
                    catch { }
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

        public void UPDataThis()
        {
            if (DeFine.WorkingWin != null)
            {
                DeFine.WorkingWin.Dispatcher.Invoke(new Action(() =>
                {
                    this.Handle.UPDataThis();
                }));

                this.Key = this.Handle.Key;
                this.Type = this.Handle.Type;

                this.SourceText = this.Handle.SourceText;
                this.TransText = this.Handle.TransText;
            }

        }

        public TransItem(string Key, string Type, string SourceText, string TransText, FakeGrid Handle)
        {
            this.Key = Key;
            this.Type = Type;
            this.SourceText = SourceText;
            this.TransText = TransText;
            this.Handle = Handle;
        }

        public static double TokenBasedSimilarity(string TextA, string TextB, Languages Lang)
        {
            // Tokenize
            var TokensA = TextTokenizer.Tokenize(Lang, TextA).Select(t => t.ToLowerInvariant()).ToHashSet();
            var TokensB = TextTokenizer.Tokenize(Lang, TextB).Select(t => t.ToLowerInvariant()).ToHashSet();

            if (TokensA.Count == 0 && TokensB.Count == 0) return 1.0;
            if (TokensA.Count == 0 || TokensB.Count == 0) return 0.0;

            var Intersection = TokensA.Intersect(TokensB).Count();
            var Union = TokensA.Union(TokensB).Count();

            return (double)Intersection / Union; // Jaccard similarity
        }

        public static List<TransItem> MarkLeadersAndSortWithTokenSimilarity(List<TransItem> Items, Languages Lang)
        {
            int N = Items.Count;
            double[,] SimMatrix = new double[N, N];

            // Calculate similarity matrix
            for (int I = 0; I < N; I++)
            {
                for (int J = I; J < N; J++)
                {
                    double Sim = TokenBasedSimilarity(Items[I].SourceText, Items[J].SourceText, Lang);
                    SimMatrix[I, J] = Sim;
                    SimMatrix[J, I] = Sim;
                }
            }

            // Calculate similarity sums for each item
            double[] SimSums = new double[N];
            for (int I = 0; I < N; I++)
            {
                double Sum = 0;
                for (int J = 0; J < N; J++)
                {
                    if (I != J) Sum += SimMatrix[I, J];
                }
                SimSums[I] = Sum;
            }

            // Find the item with the highest similarity sum as leader
            double MaxSimSum = SimSums.Max();
            int LeaderIndex = Array.IndexOf(SimSums, MaxSimSum);

            // Reset all leader flags
            foreach (var Item in Items)
                Item.Leader = false;

            if (LeaderIndex >= 0 && LeaderIndex < N)
                Items[LeaderIndex].Leader = true;

            // Sort: leaders first, then by Key
            return Items.OrderByDescending(x => x.Leader).ThenBy(x => x.Key).ToList();
        }
    }


    public class BatchTranslationHelper
    {
        public static Dictionary<string, string> SameItems = new Dictionary<string, string>();
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
        public static void Init()
        {
            SameItems.Clear();
            TransItems.Clear();

            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                FakeGrid MainGrid = DeFine.WorkingWin.TransViewList.RealLines[i];

                var FindDictionary = YDDictionaryHelper.CheckDictionary(MainGrid.Key);

                if (FindDictionary != null)
                {
                    MainGrid.SourceText = FindDictionary.OriginalText;
                }

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

        public static bool IsWork = false;
        public static void Start()
        {
            AutoSleep = 1;

            if (DeFine.GlobalLocalSetting.MaxThreadCount <= 0)
            {
                DeFine.GlobalLocalSetting.MaxThreadCount = 1;
            }

            DeFine.WorkingWin.Dispatcher.Invoke(new Action(() =>
            {
                DeFine.WorkingWin.ThreadInFo.Visibility = System.Windows.Visibility.Visible;
            }));

            try
            {
                DashBoardService.Init();

                DeFine.CurrentDashBoardView.Dispatcher.Invoke(new Action(() =>
                {
                    DeFine.CurrentDashBoardView.SetTransState(true);
                }));
            }
            catch { }

            Init();

            TransMainTrd = new Thread(() =>
            {
                IsWork = true;
                TransMainTrdCancel = new CancellationTokenSource();
                var Token = TransMainTrdCancel.Token;

                int CurrentTrds = 0;
                while (true)
                {
                    try
                    {
                    NextFind:
                        DeFine.WorkingWin.Dispatcher.Invoke(new Action(() =>
                        {
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
                                AutoSleep = 200;
                            }
                            else
                            {
                                AutoSleep = 0;
                            }

                            if (AutoSleep > 0)
                            {
                                Thread.Sleep(AutoSleep);
                            }
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

                                IsWork = false;

                                try
                                {
                                    DeFine.CurrentDashBoardView.Dispatcher.Invoke(new Action(() =>
                                    {
                                        DeFine.CurrentDashBoardView.SetTransState(false);
                                    }));
                                }
                                catch { }

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
                    catch (OperationCanceledException)
                    {
                        IsWork = false;
                        TransMainTrd = null;

                        try
                        {
                            DeFine.CurrentDashBoardView.Dispatcher.Invoke(new Action(() =>
                            {
                                DeFine.CurrentDashBoardView.SetTransState(false);
                            }));
                        }
                        catch { }
                        return;
                    }

                    Thread.Sleep(1);
                }

            });

            TransMainTrd.Start();
        }

        public static void Close()
        {
            DeFine.WorkingWin.Dispatcher.Invoke(new Action(() =>
            {
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
