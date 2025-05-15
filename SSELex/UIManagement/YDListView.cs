using System.Windows.Controls;
using System.Windows;
using SSELex;

// Copyright (C) 2025 YD525
// Licensed under the GNU GPLv3
// See LICENSE for details
//https://github.com/YD525/YDSkyrimToolR/

//R2
public class YDListView
{
    private Grid Parent = null;
    private ScrollViewer Scroll = null;
    private Canvas MainCanvas;

    public Thread UpdateTrd = null;

    public List<Grid> RealLines = new List<Grid>();
    public List<Grid> VisibleRows = new List<Grid>();

    public int Rows { get { return GetRows(); } }
    public int BufferRows = 3;

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
                try 
                {
                    DeFine.WorkingWin.Dispatcher.Invoke(new Action(() =>
                    {
                        UpdateVisibleRows();
                    }));
                }
                catch { }
            }
            
        });
        UpdateTrd.Start();
    }

    public Canvas GetMainCanvas()
    {
        return this.MainCanvas;
    }

    public void Clear()
    {
        this.RealLines.Clear();
        this.MainCanvas.Children.Clear();
        this.MainCanvas.Height = 0;
        this.Scroll.ScrollToTop();
    }

    private bool CanUpDate = false;
    private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        // 使用 Dispatcher 在主线程中处理更新，避免直接在滚动事件中处理
        CanUpDate = true;
    }

    public double GetHight(Grid Grid)
    {
        if (!double.IsNaN(Grid.ActualHeight))
        {
            if (Grid.ActualHeight > 0)
            {
                return Grid.ActualHeight;
            }
        }
        return Grid.Height;
    }

    public void UpdateVisibleRows()
    {
        if (CanUpDate)
        {
            CanUpDate = false;
        }
        else
        {
            return;
        }
        VisibleRows.Clear();

        double ScrollTop = this.Scroll.VerticalOffset;
        double ScrollHeight = this.Scroll.ExtentHeight;
        double ViewHeight = this.Scroll.ViewportHeight;
        double AccumulatedHeight = 0;
        int FirstVisibleRow = 0;

        // 计算第一条可见行
        for (int i = 0; i < this.RealLines.Count; i++)
        {
            if (AccumulatedHeight >= ScrollTop)
            {
                FirstVisibleRow = i;
                break;
            }
            AccumulatedHeight += GetHight(this.RealLines[i]);
        }

        // 计算最后一条可见行
        double VisibleHeight = 0;
        int LastVisibleRow = FirstVisibleRow;

        for (int i = FirstVisibleRow; i < this.RealLines.Count; i++)
        {
            VisibleHeight += GetHight(this.RealLines[i]);
            if (VisibleHeight >= ViewHeight)
            {
                LastVisibleRow = i;
                break;
            }
        }

        LastVisibleRow += BufferRows; // 额外缓存行
        if (LastVisibleRow >= GetRows())
        {
            LastVisibleRow = GetRows() - 1;
        }

        int FirstVisibleRowWithBuffer = Math.Max(0, FirstVisibleRow - BufferRows);

        MainCanvas.Children.Clear();

        if (ScrollTop + ViewHeight >= ScrollHeight - 100)
        {
            for (int i = 0; i < this.RealLines.Count; i++)
            {
                if (i >= FirstVisibleRowWithBuffer)
                {
                    MainCanvas.Children.Add(this.RealLines[i]);

                    VisibleRows.Add(this.RealLines[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < this.RealLines.Count; i++)
            {
                if (i >= FirstVisibleRowWithBuffer && i <= LastVisibleRow)
                {
                    MainCanvas.Children.Add(this.RealLines[i]);

                    VisibleRows.Add(this.RealLines[i]);
                }
            }
        }
    }

    public int AddRowR(Grid Item)
    {
        return AddRow(Item);
    }

    public int AddRow(Grid Item)
    {
        if (Item == null) return 0;

        if (Parent != null)
        {
            Item.Width = Parent.ActualWidth - 15;
        }

        this.RealLines.Add(Item);

        Canvas.SetTop(Item, MainCanvas.Height);
        Canvas.SetLeft(Item, 0);

        MainCanvas.Height += Item.Height;

        return this.RealLines.Count;
    }

    public void DeleteRow(int Offset)
    {
        var GetGridHeight = (this.MainCanvas.Children[Offset] as Grid).Height;
        this.MainCanvas.Children.RemoveAt(Offset);

        this.MainCanvas.Height -= GetGridHeight;

        this.RealLines.RemoveAt(Offset);

        UpdateVisibleRows();
    }
}

