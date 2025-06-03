using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using DynamicData;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using SSELex.ConvertManager;
using SSELex.UIManagement;

namespace SSELex
{
    public class DashBoardViewModel : INotifyPropertyChanged
    {
        public ChartValues<double> _SetValues;
        public ChartValues<double> SetValues
        {
            get => _SetValues;
            set
            {
                if (_SetValues != value)
                {
                    _SetValues = value;
                    OnPropertyChanged(nameof(SetValues));
                }
            }
        }

        private SeriesCollection _FontUsageSeries;
        public SeriesCollection FontUsageSeries
        {
            get => _FontUsageSeries;
            set
            {
                if (_FontUsageSeries != value)
                {
                    _FontUsageSeries = value;
                    OnPropertyChanged(nameof(FontUsageSeries));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Interaction logic for DashBoardView.xaml
    /// </summary>
    public partial class DashBoardView : Window
    {
        private DashBoardViewModel CurrentModel = new DashBoardViewModel();

        private string LastSetFilePath = "";
        public DashBoardView()
        {
            InitializeComponent();

            this.DataContext = CurrentModel;

            CurrentModel.SetValues = new ChartValues<double>() { 0, 0, 0, 0, 0, 0 };

            CurrentModel.FontUsageSeries = new SeriesCollection
            {
                new LineSeries
                {
                    AreaLimit = -10,
                    Values = new ChartValues<ObservableValue>
                    {
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0)
                    }
                }
            };
        }

        #region Def
        public bool IsLeftMouseDown = false;
        private void WinHead_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                IsLeftMouseDown = true;
            }

            if (IsLeftMouseDown)
            {
                try
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        this.DragMove();
                    }));

                    IsLeftMouseDown = false;
                }
                catch { }
            }
        }

        private void RMouserEffectByEnter(object sender, MouseEventArgs e)
        {
            if (sender is Border)
            {
                Border LockerGrid = sender as Border;
                if (LockerGrid.Child != null)
                {
                    if (LockerGrid.Child is Image)
                    {
                        Image LockerImg = LockerGrid.Child as Image;
                        LockerImg.Opacity = 0.9;
                    }
                    LockerGrid.Background = new SolidColorBrush(Color.FromRgb(0, 61, 109));
                }
            }
        }

        private void RMouserEffectByLeave(object sender, MouseEventArgs e)
        {
            if (sender is Border)
            {
                Border LockerGrid = sender as Border;
                if (LockerGrid.Child != null)
                {
                    if (LockerGrid.Child is Image)
                    {
                        Image LockerImg = LockerGrid.Child as Image;
                        LockerImg.Opacity = 0.6;
                    }
                    LockerGrid.Background = new SolidColorBrush(Color.FromRgb(0, 84, 149));
                }
            }
        }

        public int SizeChangeState = 0;
        public void AnyHeaderButtonClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border || sender is Image)
            {
                string Tag = "";

                if (sender is Border)
                {
                    Tag = ConvertHelper.ObjToStr((sender as Border).Tag);
                }
                if (sender is Image)
                {
                    Tag = ConvertHelper.ObjToStr((sender as Image).Tag);
                }

                switch (Tag)
                {
                    case "Min":
                        {
                            this.WindowState = WindowState.Minimized;
                        }
                        break;
                    case "MaxWin":
                        {
                            if (SizeChangeState == 0)
                            {
                                this.WindowState = WindowState.Maximized;
                                SizeChangeState = 1;
                            }
                            else
                            {
                                this.WindowState = WindowState.Normal;
                                SizeChangeState = 0;
                            }

                        }
                        break;
                    case "Close":
                        {
                            this.Hide();
                        }
                        break;
                }
            }
        }
        #endregion

        private Storyboard? LoadingEffect;

        public void StartLoadViewEffect()
        {
            Dispatcher.Invoke(() =>
            {
                LoadingEffect ??= LoadingFrame.FindResource("LoadingEffect") as Storyboard;
                LoadingEffect?.Begin(this, true);
            });
        }

        public void EndLoadViewEffect()
        {
            this.Dispatcher.Invoke(() =>
            {
                LoadingEffect?.Remove(this);
            });
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentTime.Text = DateTime.Now.ToString("yyyy.MM.dd");
        }

        public void SetLogA(string Value)
        {
            LogBoxA.Dispatcher.Invoke(new Action(() =>
            {
                LogBoxA.Text = Value;
            }));
        }

        public void SetLogB(string Value)
        {
            LogBoxB.Dispatcher.Invoke(new Action(() =>
            {
                LogBoxB.Text = Value;
            }));
        }

        public void Open(string FilePath)
        {
            string GetFileName = FilePath.Substring(FilePath.LastIndexOf(@"\") + @"\".Length);
            Tittle.Content = GetFileName;
            LastSetFilePath = FilePath;
        }

        private void ReloadData(object sender, RoutedEventArgs e)
        {

        }

        private DispatcherTimer? TransTimer;
        private int ElapsedSeconds = 0;



        public void InitSetValues()
        {
            CurrentModel.SetValues[0] = 0;
            CurrentModel.SetValues[1] = 0;
            CurrentModel.SetValues[2] = 0;
            CurrentModel.SetValues[3] = 0;
            CurrentModel.SetValues[4] = 0;
            CurrentModel.SetValues[5] = 0;
        }
        public void SetTransState(bool IsStart)
        {
            Action IntervalAct = new Action(() =>
            {
                try
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        int GetNumber = DashBoardService.GetCurrentSecondUsage();
                        CurrentModel.FontUsageSeries[0].Values.Add(new ObservableValue(GetNumber));
                        CurrentModel.FontUsageSeries[0].Values.RemoveAt(0);
                        UsageCount.Content = string.Format("Words used: {0} / per second", GetNumber);
                    }));

                    this.Dispatcher.Invoke(new Action(() =>
                    {


                        var GetDataInFo = DashBoardService.FontUsageCounts;

                        if (GetDataInFo.ContainsKey(PlatformType.ChatGpt))
                        {
                            CurrentModel.SetValues[0] = GetDataInFo[PlatformType.ChatGpt];
                        }
                        else
                        {
                            CurrentModel.SetValues[0] = 0;
                        }

                        if (GetDataInFo.ContainsKey(PlatformType.Gemini))
                        {
                            CurrentModel.SetValues[1] = GetDataInFo[PlatformType.Gemini];
                        }
                        else
                        {
                            CurrentModel.SetValues[1] = 0;
                        }

                        if (GetDataInFo.ContainsKey(PlatformType.DeepSeek))
                        {
                            CurrentModel.SetValues[2] = GetDataInFo[PlatformType.DeepSeek];
                        }
                        else
                        {
                            CurrentModel.SetValues[2] = 0;
                        }

                        if (GetDataInFo.ContainsKey(PlatformType.DeepL))
                        {
                            CurrentModel.SetValues[3] = GetDataInFo[PlatformType.DeepL];
                        }
                        else
                        {
                            CurrentModel.SetValues[3] = 0;
                        }

                        if (GetDataInFo.ContainsKey(PlatformType.GoogleApi))
                        {
                            CurrentModel.SetValues[4] = GetDataInFo[PlatformType.GoogleApi];
                        }
                        else
                        {
                            CurrentModel.SetValues[4] = 0;
                        }

                        if (GetDataInFo.ContainsKey(PlatformType.BaiduApi))
                        {
                            CurrentModel.SetValues[5] = GetDataInFo[PlatformType.BaiduApi];
                        }
                        else
                        {
                            CurrentModel.SetValues[5] = 0;
                        }
                    }));
                }
                catch { }
            });

            if (IsStart)
            {
                InitSetValues();
                StartLoadViewEffect();
                LoadingControl.Visibility = Visibility.Visible;
                ElapsedSeconds = 0;

                if (TransTimer == null)
                {
                    TransTimer = new DispatcherTimer();
                    TransTimer.Interval = TimeSpan.FromSeconds(1);
                    TransTimer.Tick += (s, e) =>
                    {
                        ElapsedSeconds++;
                        if (LastSetFilePath.Trim().Length > 0)
                        {
                            string GetFileName = LastSetFilePath.Substring(LastSetFilePath.LastIndexOf(@"\") + 1);
                            Tittle.Content = string.Format("Translating \"{0}\"... {1}s", GetFileName, ElapsedSeconds);
                        }
                    };
                }

                TransTimer.Start();

                if (LastSetFilePath.Trim().Length > 0)
                {
                    string GetFileName = LastSetFilePath.Substring(LastSetFilePath.LastIndexOf(@"\") + 1);
                    Tittle.Content = string.Format("Translating \"{0}\"... 0s", GetFileName);
                }

                DashBoardService.StartListenService(true, IntervalAct);
            }
            else
            {
                EndLoadViewEffect();
                LoadingControl.Visibility = Visibility.Collapsed;

                if (TransTimer != null)
                {
                    TransTimer.Stop();
                }

                if (LastSetFilePath.Trim().Length > 0)
                {
                    string GetFileName = LastSetFilePath.Substring(LastSetFilePath.LastIndexOf(@"\") + 1);
                    Tittle.Content = string.Format("Translated \"{0}\" in {1}s", GetFileName, ElapsedSeconds);
                }

                ElapsedSeconds = 0;

                DashBoardService.StartListenService(false, IntervalAct);
            }
        }

        private void Border_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ShowFilePath(object sender, MouseButtonEventArgs e)
        {
            if (File.Exists(LastSetFilePath))
            {
                string Argument = "/select, \"" + LastSetFilePath + "\"";
                System.Diagnostics.Process.Start("explorer.exe", Argument);
            }
        }
    }
}
