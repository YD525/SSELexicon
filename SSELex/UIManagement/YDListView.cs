using System.Windows.Controls;
using System.Windows;
using SSELex;
using SSELex.UIManage;
using System.Windows.Media;
using SSELex.ConvertManager;
using Loqui.Translators;
using SSELex.TranslateManage;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using PhoenixEngine.TranslateManagement;
using SSELex.UIManagement;
using PhoenixEngine.SSELexiconBridge;
using static PhoenixEngine.SSELexiconBridge.NativeBridge;
using PhoenixEngine.EngineManagement;
using Mutagen.Bethesda.Skyrim;
using SSELex.SkyrimManage;

// Copyright (C) 2025 YD525
// Licensed under the GNU GPLv3
// See LICENSE for details
//https://github.com/YD525/YDSkyrimToolR/

//V5

public class FakeGrid
{
    public double Height = 0;
    public string Type = "";
    public string Key = "";
    public string SourceText = "";
    public string TransText = "";
    public double Score = 0;

    public FakeGrid(double Height, string Type, string Key, string SourceText, string TransText, double Score)
    {
        this.Height = Height;
        this.Type = Type;
        this.Key = Key;
        this.SourceText = SourceText;
        this.TransText = TransText;
        this.Score = Score;
    }

    public void SyncUI(YDListView ListViewHandle)
    {
        for (int i = 0; i < ListViewHandle.VisibleRows.Count; i++)
        {
            bool CanExit = false;

            if (RowStyleWin.GetKey(ListViewHandle.VisibleRows[i]).Equals(this.Key))
            {
                ListViewHandle.Parent.Dispatcher.Invoke(new Action(() =>
                {
                    RowStyleWin.SetTranslated(ListViewHandle.VisibleRows[i], this.TransText);
                    CanExit = true;
                }));
            }

            if (CanExit)
            {
                break;
            }
        }
    }

    public void SyncData()
    {
        var QueryResult = TranslatorBridge.QueryTransData(this.Key, this.SourceText);

        if (QueryResult != null)
        {
            this.TransText = QueryResult.TransText;
        }

        var FindDictionary = YDDictionaryHelper.CheckDictionary(this.Key);

        if (FindDictionary != null)
        {
            if (FindDictionary.OriginalText.Trim().Length > 0)
            {
                if (this.SourceText != FindDictionary.OriginalText)
                {
                    this.SourceText = FindDictionary.OriginalText;
                }
            }
        }
    }

    public void SetFontColor(YDListView ListViewHandle, int R, int G, int B)
    {
        for (int i = 0; i < ListViewHandle.VisibleRows.Count; i++)
        {
            if (RowStyleWin.GetKey(ListViewHandle.VisibleRows[i]).Equals(this.Key))
            {
                RowStyleWin.SetColor(ListViewHandle.VisibleRows[i], R, G, B);
                break;
            }
        }
    }
}

public class YDListView
{
    public Grid Parent = null;
    private ScrollViewer Scroll = null;
    public Canvas MainCanvas;

    public Thread UpdateTrd = null;

    public List<FakeGrid> RealLines = new List<FakeGrid>();
    public List<Grid> VisibleRows = new List<Grid>();

    public int Rows { get { return GetRows(); } }
    public int BufferRows = 3;
    public bool CanSet = true;

    public int SelectLineID = 0;
    public Border? LastSelectBorder = null;

    public bool IsSearchBox = false;

    public YDListView ParentView = null;

    public Thread? SelectLineThread = null;
    private CancellationTokenSource? CancelSelectLineThread = null;
    private CancellationToken? CancelToken = null;
    public void SetSelectLine(Grid MainGrid, bool UPDate)
    {
        if (LastSelectBorder != null)
        {
            LastSelectBorder.BorderBrush = new SolidColorBrush(Colors.Black);
        }

        SelectLineID = ConvertHelper.ObjToInt(MainGrid.Tag);

        Border MainBorder = (Border)MainGrid.Children[0];

        Grid GetChildGrid = (Grid)MainBorder.Child;

        Grid GetTranslatedGrid = (Grid)GetChildGrid.Children[3];
        TextBox GetTranslated = (TextBox)(((Border)GetTranslatedGrid.Children[0]).Child);

        if (!IsSearchBox)
        {
            if (UPDate)
            {
                GetTranslated.Focus();
                string GetKey = RowStyleWin.GetKey(MainGrid);
                DeFine.WorkingWin.SetSelectFromAndToText(GetKey);

                if (DeFine.GlobalLocalSetting.ShowCode && DeFine.WorkingWin.CurrentTransType == 3)
                {
                    RowStyleWin.SelectLineFromIDE(GetKey);
                }
            }

            MainBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(10, 97, 175));
            LastSelectBorder = MainBorder;
        }
        else
        {
            if (ParentView != null)
            {
                string GetKey = RowStyleWin.GetKey(MainGrid);

                int SelectID = ParentView.KeyToSelectID(GetKey);

                Action SelectAction = new Action(() =>
                {
                    if (UPDate)
                    {
                        if (ParentView != null)
                        {
                            ParentView.SetSelectLineByKey(GetKey, UPDate);

                            MainBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(10, 97, 175));
                            LastSelectBorder = MainBorder;
                        }
                    }
                    else
                    {
                        if (SelectID == -1)
                        {
                            if (LastSelectBorder != null)
                            {
                                LastSelectBorder.BorderBrush = new SolidColorBrush(Colors.Black);
                            }
                        }
                        else
                        {
                            if (LastSelectBorder != null)
                            {
                                MainBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(10, 97, 175));
                                LastSelectBorder = MainBorder;
                            }
                        }
                    }
                });

                if (UPDate)
                {
                    if (!ParentView.IsKeyInViewport(GetKey))
                    {
                        if (SelectLineThread == null)
                        {
                            CancelSelectLineThread = new CancellationTokenSource();

                            CancelToken = CancelSelectLineThread.Token;

                            SelectLineThread = new Thread(() =>
                            {
                                try
                                {
                                    ParentView.Parent.Dispatcher.BeginInvoke(new Action(() =>
                                    {
                                        ParentView.ScrollTo(SelectID);
                                    }));

                                    bool IsVisible = false;

                                    while (!IsVisible)
                                    {
                                        CancelToken?.ThrowIfCancellationRequested();

                                        Thread.Sleep(50);
                                        ParentView.Parent.Dispatcher.BeginInvoke(new Action(() =>
                                        {
                                            IsVisible = ParentView.IsKeyInViewport(GetKey);
                                        }));
                                    }

                                    CancelToken?.ThrowIfCancellationRequested();

                                    ParentView.Parent.Dispatcher.BeginInvoke(new Action(() =>
                                    {
                                        ParentView.SetSelectLineByKey(GetKey, true);
                                    }));

                                    CancelToken?.ThrowIfCancellationRequested();

                                    ParentView.Parent.Dispatcher.BeginInvoke(new Action(() =>
                                    {
                                        MainBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(10, 97, 175));
                                        LastSelectBorder = MainBorder;
                                    }));

                                    SelectLineThread = null;
                                }
                                catch
                                {
                                    SelectLineThread = null;
                                }
                            });

                            SelectLineThread.Start();
                        }
                        else
                        {
                            if (CancelSelectLineThread != null)
                            {
                                CancelSelectLineThread.Cancel();
                                SelectLineThread = null;
                            }
                        }
                    }
                    else
                    {
                        SelectAction.Invoke();
                    }
                }
                else
                {
                    SelectAction.Invoke();
                }

                if (UPDate && SelectLineID != -1)
                {
                    DeFine.WorkingWin.SetSelectFromAndToText(GetKey);
                }
            }
        }
    }

    private bool IsGridInViewport(Grid Grid)
    {
        double GridTop = Canvas.GetTop(Grid);
        double GridBottom = GridTop + Grid.ActualHeight;

        double ViewTop = Scroll.VerticalOffset;
        double ViewBottom = ViewTop + Scroll.ViewportHeight;

        return GridTop >= ViewTop && GridBottom <= ViewBottom;
    }

    public void UP()
    {
        if (RealLines.Count == 0)
            return;

        SelectLineID--;

        if (SelectLineID < 0)
        {
            SelectLineID = RealLines.Count - 1;
        }

        var TargetLogicItem = RealLines[SelectLineID];

        Grid? MatchGrid = null;
        foreach (var G in VisibleRows)
        {
            if (RowStyleWin.GetKey(G).Equals(TargetLogicItem.Key))
            {
                MatchGrid = G;
                break;
            }
        }

        if (MatchGrid != null && IsGridInViewport(MatchGrid))
        {
            SetSelectLine(MatchGrid, true);
        }
        else
        {
            double Offset = 0;
            for (int i = 0; i < SelectLineID; i++)
            {
                Offset += RealLines[i].Height;
            }

            Scroll.ScrollToVerticalOffset(Offset);

            UpdateVisibleRows(true);

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                foreach (var G in VisibleRows)
                {
                    if (RowStyleWin.GetKey(G).Equals(TargetLogicItem.Key))
                    {
                        SetSelectLine(G, true);
                        break;
                    }
                }
            }));
        }
    }

    public void Down()
    {
        if (RealLines.Count == 0)
            return;

        SelectLineID++;

        if (SelectLineID >= RealLines.Count)
        {
            SelectLineID = 0;
        }

        else
        {
            var TargetLogicItem = RealLines[SelectLineID];

            Grid? MatchGrid = null;
            foreach (var G in VisibleRows)
            {
                if (RowStyleWin.GetKey(G).Equals(TargetLogicItem.Key))
                {
                    MatchGrid = G;
                    break;
                }
            }

            if (MatchGrid != null && IsGridInViewport(MatchGrid))
            {
                SetSelectLine(MatchGrid, true);
            }
            else
            {
                double Offset = 0;
                for (int i = 0; i < SelectLineID; i++)
                {
                    Offset += RealLines[i].Height;
                }

                Scroll.ScrollToVerticalOffset(Offset);

                UpdateVisibleRows(true);

                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                {
                    foreach (var G in VisibleRows)
                    {
                        if (RowStyleWin.GetKey(G).Equals(TargetLogicItem.Key))
                        {
                            SetSelectLine(G, true);
                            break;
                        }
                    }
                }));
            }
        }
    }

    private int GetRows()
    {
        return this.RealLines.Count;
    }

    public YDListView(Grid Parent)
    {
        Style ScrollBarStyle = new Style(typeof(ScrollBar))
        {
            BasedOn = (Style)Application.Current.FindResource("for_scrollbar")
        };
        Parent.Resources.Add(typeof(ScrollBar), ScrollBarStyle);

        this.MainCanvas = new Canvas();
        this.MainCanvas.Background = null;
        this.MainCanvas.VerticalAlignment = VerticalAlignment.Top;
        this.MainCanvas.SnapsToDevicePixels = true;
        this.MainCanvas.UseLayoutRounding = true;
        this.MainCanvas.IsItemsHost = false;
        ScrollViewer OneScroll = new ScrollViewer();
        OneScroll.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        OneScroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
        OneScroll.Content = MainCanvas;
        OneScroll.ScrollChanged += OnScrollChanged; // 监听滚动

        this.Scroll = OneScroll;

        Parent.Children.Add(OneScroll);
        this.Parent = Parent;

        UpdateTrd = new Thread(() =>
        {
            while (true)
            {
                Thread.Sleep(100);

                if (CanSet)
                {
                    if (CanUpDate)
                    {
                        try
                        {
                            DeFine.WorkingWin.Dispatcher.Invoke(new Action(() =>
                            {
                                UpdateVisibleRows();
                            }));
                        }
                        catch { }

                        CanUpDate = false;
                    }
                }
            }

        });
        UpdateTrd.Start();
    }

    public Canvas GetMainCanvas()
    {
        return this.MainCanvas;
    }

    public void HotReload()
    {
        this.CanSet = false;
        this.MainCanvas.Children.Clear();
        this.VisibleRows.Clear();
        UpdateVisibleRows();
        this.CanSet = true;
    }

    public void Clear()
    {
        this.SelectLineID = 0;
        this.CanSet = false;
        this.VisibleRows.Clear();
        this.RealLines.Clear();
        this.MainCanvas.Children.Clear();
        this.MainCanvas.Height = 0;
        this.Scroll.ScrollToTop();
        GC.SuppressFinalize(this);
        GC.Collect();
        this.CanSet = true;
    }

    private bool CanUpDate = false;
    private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        CanUpDate = true;
    }

    public void UpdateVisibleRows(bool ForceUPDate = false)
    {
        if (ForceUPDate)
        {
            CanUpDate = true;
        }

        if (!CanUpDate)
            return;

        CanUpDate = false;

        double ScrollTop = this.Scroll.VerticalOffset;
        double ScrollHeight = this.Scroll.ExtentHeight;
        double ViewHeight = this.Scroll.ViewportHeight;

        double AccumulatedHeight = 0;
        int FirstVisibleRow = 0;

        for (int I = 0; I < RealLines.Count; I++)
        {
            if (AccumulatedHeight >= ScrollTop)
            {
                FirstVisibleRow = I;
                break;
            }
            AccumulatedHeight += RealLines[I].Height;
        }

        double VisibleHeight = 0;
        int LastVisibleRow = FirstVisibleRow;

        for (int I = FirstVisibleRow; I < RealLines.Count; I++)
        {
            VisibleHeight += RealLines[I].Height;
            if (VisibleHeight >= ViewHeight)
            {
                LastVisibleRow = I;
                break;
            }
        }

        LastVisibleRow += BufferRows;
        if (LastVisibleRow >= GetRows())
            LastVisibleRow = GetRows() - 1;

        int FirstVisibleRowWithBuffer = Math.Max(0, FirstVisibleRow - BufferRows);

        bool IsNearBottom = ScrollTop + ViewHeight >= ScrollHeight - 150;

        HashSet<int> NewVisibleIndices = new();
        if (IsNearBottom)
        {
            for (int I = FirstVisibleRowWithBuffer; I < RealLines.Count; I++)
                NewVisibleIndices.Add(I);
        }
        else
        {
            for (int I = FirstVisibleRowWithBuffer; I <= LastVisibleRow; I++)
                NewVisibleIndices.Add(I);
        }

        for (int I = MainCanvas.Children.Count - 1; I >= 0; I--)
        {
            UIElement Child = MainCanvas.Children[I];
            if (Child is FrameworkElement Fe && Fe.Tag is int RowIndex && !NewVisibleIndices.Contains(RowIndex))
            {
                MainCanvas.Children.RemoveAt(I);
            }
        }

        double CurrentTop = 0;
        for (int I = 0; I < RealLines.Count; I++)
        {
            var Row = RealLines[I];

            if (NewVisibleIndices.Contains(I))
            {
                bool AlreadyExists = false;
                foreach (UIElement Child in MainCanvas.Children)
                {
                    if (Child is FrameworkElement Fe && Fe.Tag is int TagIndex && TagIndex == I)
                    {
                        AlreadyExists = true;
                        break;
                    }
                }

                if (!AlreadyExists)
                {
                    Grid Grid = UIHelper.CreatLine(Row);
                    Grid.Tag = I;
                    Grid.Width = this.Parent.ActualWidth - 15;
                    Grid.PreviewMouseDown += MainGrid_PreviewMouseDown;
                    Canvas.SetTop(Grid, CurrentTop);
                    Canvas.SetLeft(Grid, 0);
                    MainCanvas.Children.Add(Grid);
                    if (I.Equals(SelectLineID))
                    {
                        SetSelectLine(Grid, false);
                    }
                }
            }

            CurrentTop += Row.Height;
        }

        VisibleRows.Clear();
        foreach (UIElement Child in MainCanvas.Children)
        {
            if (Child is Grid G)
                VisibleRows.Add(G);
        }
    }

    public object AddLocker = new object();
    public int AddRowR(FakeGrid? Item)
    {
        lock (AddLocker)
        {
            if (Item == null) return 0;

            this.RealLines.Add(Item);

            MainCanvas.Height += Item.Height;

            return this.RealLines.Count;
        }
    }

    public object DeleteLocker = new object();
    public void DeleteRow(int Offset)
    {
        lock (DeleteLocker)
        {
            if (this.RealLines.Count > Offset)
            {
                var Get = this.RealLines[Offset];

                for (int i = 0; i < this.VisibleRows.Count; i++)
                {
                    if (RowStyleWin.GetKey((Grid)this.VisibleRows[i]).Equals(Get.Key))
                    {
                        this.VisibleRows.RemoveAt(i);
                        break;
                    }
                }

                UpdateVisibleRows();
            }
        }
    }

    private void MainGrid_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        SetSelectLine((Grid)sender, true);
    }

    public void ChangeFontColor(string ModName, int R, int G, int B)
    {
        var GetLine = this.RealLines[SelectLineID];
        FontColorFinder.SetColor(ModName, GetLine.Key, R, G, B);
        GetLine.SetFontColor(this, R, G, B);
    }

    public string GetSelectedKey()
    {
        if (this.RealLines.Count > this.SelectLineID)
        {
            return this.RealLines[this.SelectLineID].Key;
        }

        return string.Empty;
    }

    public FakeGrid? GetSelectedGrid()
    {
        if (this.RealLines.Count > this.SelectLineID)
        {
            return this.RealLines[this.SelectLineID];
        }

        return null;
    }

    public void ScrollTo(int SelectID)
    {
        double Offset = 0;
        for (int i = 0; i < SelectID; i++)
        {
            if (RealLines.Count > i)
            {
                Offset += RealLines[i].Height;
            }
            else
            {
                break;
            }
        }

        Scroll.ScrollToVerticalOffset(Offset);

        UpdateVisibleRows(true);
    }

    public bool IsKeyInViewport(string Key)
    {
        foreach (var Get in this.VisibleRows)
        {
            if (RowStyleWin.GetKey(Get).Equals(Key))
            {
                if (IsGridInViewport(Get))
                {
                    return true;
                }

                break;
            }
        }

        return false;
    }

    public void SetSelectLineByKey(string Key, bool UPDate)
    {
        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
        {
            foreach (var Get in this.VisibleRows)
            {
                if (RowStyleWin.GetKey(Get).Equals(Key))
                {
                    SetSelectLine(Get, UPDate);
                    break;
                }
            }
        }));
    }

    public FakeGrid? KeyToFakeGrid(string Key)
    {
        for (int i = 0; i < this.RealLines.Count; i++)
        {
            if (this.RealLines[i].Key.Equals(Key))
            {
                return this.RealLines[i];
            }
        }

        return null;
    }

    public int KeyToSelectID(string Key)
    {
        int TempSelectLineID = 0;

        for (int i = 0; i < this.RealLines.Count; i++)
        {
            if (this.RealLines[i].Key.Equals(Key))
            {
                TempSelectLineID = i;
                break;
            }
        }

        return TempSelectLineID;
    }

    public List<FakeGrid> QuickSearch(string Keyword)
    {
        if (string.IsNullOrEmpty(Keyword))
            return new List<FakeGrid>();

        return RealLines
            .Where(Line =>
                (!string.IsNullOrEmpty(Line.SourceText) &&
                 Line.SourceText.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                ||
                (!string.IsNullOrEmpty(Line.TransText) &&
                 Line.TransText.IndexOf(Keyword, StringComparison.OrdinalIgnoreCase) >= 0)
            )
            .ToList();
    }
}

