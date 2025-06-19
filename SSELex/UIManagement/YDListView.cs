using System.Windows.Controls;
using System.Windows;
using SSELex;
using SSELex.UIManage;
using System.Windows.Media;
using SSELex.ConvertManager;
using Loqui.Translators;
using SSELex.TranslateManage;

// Copyright (C) 2025 YD525
// Licensed under the GNU GPLv3
// See LICENSE for details
//https://github.com/YD525/YDSkyrimToolR/

//R3

public class FakeGrid
{
    public double Height = 0;
    public string Type = "";
    public string EditorID = "";
    public string Key = "";
    public string SourceText = "";
    public string TransText = "";
    public Color BorderColor;
    public Color FontColor;
    public double Score = 0;

    public FakeGrid(double Height, string Type, string EditorID, string Key, string SourceText, string TransText, double Score)
    {
        this.Height = Height;
        this.Type = Type;
        this.EditorID = EditorID;
        this.Key = Key;
        this.SourceText = SourceText;
        this.TransText = TransText;
        this.Score = Score;
    }

    public void UPDataThis()
    {
        var QueryData = TranslatorExtend.QueryTransData(this.Key,this.SourceText);
        this.BorderColor = QueryData.Color;
        this.TransText = QueryData.TransText;
    }

    public void UPDateView()
    {
        var GetResult = TranslatorExtend.SetTransData(this.Key, this.SourceText, this.TransText);
        this.BorderColor = GetResult.Color;

        if (DeFine.WorkingWin != null)
        {
            for (int i = 0; i < DeFine.WorkingWin.TransViewList.VisibleRows.Count; i++)
            {
                object SelectItem = DeFine.WorkingWin.TransViewList.VisibleRows[i];

                if (SelectItem is Grid MainGrid)
                {
                    string GetKey = "";
                    DeFine.WorkingWin.Dispatcher.Invoke(new Action(() =>
                    {
                        GetKey = UIHelper.GetMainGridKey(MainGrid);
                    }));

                    if (GetKey.Equals(this.Key))
                    {
                        DeFine.WorkingWin.Dispatcher.Invoke(new Action(() =>
                        {
                            Grid NewGrid = UIHelper.CreatLine(this);
                            NewGrid.Width = DeFine.WorkingWin.TransViewList.Parent.ActualWidth - 15;
                            NewGrid.Tag = MainGrid.Tag;
                            double Top = Canvas.GetTop(MainGrid);
                            double Left = Canvas.GetLeft(MainGrid);

                            DeFine.WorkingWin.TransViewList.VisibleRows[i] = NewGrid;

                            for (int ir = 0; ir < DeFine.WorkingWin.TransViewList.MainCanvas.Children.Count; ir++)
                            {
                                if (DeFine.WorkingWin.TransViewList.MainCanvas.Children[ir] is Grid GridInCanvas &&
                                    GridInCanvas.Equals(MainGrid))
                                {
                                    DeFine.WorkingWin.TransViewList.MainCanvas.Children.RemoveAt(ir);
                                    Canvas.SetTop(NewGrid, Top);
                                    Canvas.SetLeft(NewGrid, Left);
                                    DeFine.WorkingWin.TransViewList.MainCanvas.Children.Insert(ir, NewGrid);
                                    break;
                                }
                            }
                        }));
                    }
                }
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

    private int GetRows()
    {
        return this.RealLines.Count;
    }

    public YDListView(Grid Parent)
    {
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
                Thread.Sleep(200);

                if (CanSet)
                {
                    try
                    {
                        DeFine.WorkingWin.Dispatcher.Invoke(new Action(() =>
                        {
                            UpdateVisibleRows();
                        }));
                    }
                    catch { }
                }
                else
                {
                    Thread.Sleep(500);
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
                    Canvas.SetTop(Grid, CurrentTop);
                    Canvas.SetLeft(Grid, 0);
                    MainCanvas.Children.Add(Grid);
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


    public int AddRowR(FakeGrid? Item)
    {
        if (Item == null) return 0;

        this.RealLines.Add(Item);

        MainCanvas.Height += Item.Height;

        return this.RealLines.Count;
    }

    public void DeleteRow(int Offset)
    {
        try
        {
            if (this.RealLines.Count > Offset)
            {
                var Get = this.RealLines[Offset];

                for (int i = 0; i < this.VisibleRows.Count; i++)
                {
                    if (UIHelper.GetMainGridKey((Grid)this.VisibleRows[i]).Equals(Get.Key))
                    {
                        this.VisibleRows.RemoveAt(i);
                        break;
                    }
                }

                UpdateVisibleRows();
            }
        }
        catch { }
    }
}

