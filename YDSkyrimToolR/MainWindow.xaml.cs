using Microsoft.WindowsAPICodePack.Dialogs;
using Mutagen.Bethesda.Plugins;
using SharpVectors.Converters;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YDSkyrimToolR.ConvertManager;
using YDSkyrimToolR.SkyrimManage;
using YDSkyrimToolR.TranslateManage;
using YDSkyrimToolR.UIManage;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static YDSkyrimToolR.UIManage.SkyrimDataLoader;
/*
* @Author: 约定
* @GitHub: https://github.com/tolove336/YDSkyrimToolR
* @Date: 2025-02-06
*/
namespace YDSkyrimToolR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void EndLoadViewEffect()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                LoadingEffect.RepeatBehavior = new RepeatBehavior();
                LoadingEffect.Begin(this);
                LoadingEffect.Stop(this);
            }));
        }

        public void ReSetTransTargetType()
        {
            TransTargetType.Items.Clear();
            foreach (var GetType in CanSetSelecter)
            {
                TransTargetType.Items.Add(GetType.ToString());
            }

            if(TransTargetType.Items.Count>0)
            TransTargetType.SelectedValue = TransTargetType.Items[0];
        }

        public EspReader GlobalEspReader = null;
        public YDListView TransViewList = null;

        public List<ObjSelect> CanSetSelecter = new List<ObjSelect>();
        public ObjSelect CurrentSelect = ObjSelect.Null;

        public void ReloadData()
        {
            UIHelper.ModifyCountCache.Clear();
            UIHelper.ModifyCount = 0;
            this.Dispatcher.Invoke(new Action(() =>
            {
                TransViewList.Clear();
            }));

            SkyrimDataLoader.Load(CurrentSelect, GlobalEspReader, TransViewList);
            GetStatistics();
        }

        public void GetStatistics()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                double GetRate = ((double)UIHelper.ModifyCount / (double)TransViewList.Rows.Count);
                ProcessBar.Width = ProcessBarFrame.ActualWidth * GetRate;
                TransProcess.Content = string.Format("STRINGS({0}/{1})", UIHelper.ModifyCount, TransViewList.Rows.Count);
            }));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DeFine.Init(this);
           
            new Thread(() =>
            {
                ShowFrameByTag("LoadingView");
                GlobalEspReader = new EspReader();
                Thread.Sleep(2000);
                EndLoadViewEffect();
                ShowFrameByTag("MainView");

                this.Dispatcher.Invoke(new Action(() =>
                {
                    Storyboard GetStoryboard = this.Resources["HideSmallNav"] as Storyboard;
                    if (GetStoryboard != null)
                    {
                        GetStoryboard.Begin();
                    }

                    if (TransViewList == null)
                    {
                        TransViewList = new YDListView(TransView, 40);
                        TransViewList.ExColStyles.Add(new ExColStyle(1, GridUnitType.Star));
                        TransViewList.Clear();
                    }
                }));
            }).Start();
        }

        public void LoadESPOrESM()
        {
            CommonOpenFileDialog Dialog = new CommonOpenFileDialog();
            Dialog.IsFolderPicker = false;   //设置为选择文件夹
            if (Dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                LoadESPOrESM(Dialog.FileName);
            }
        }

        //C:\Users\Administrator\Desktop\TestPy\AA
        public void LoadESPOrESM(string FilePath)
        {
            if (System.IO.File.Exists(FilePath))
            {
                //3.1.1 Version esl not support
                if (FilePath.ToLower().EndsWith(".esp") || FilePath.ToLower().EndsWith(".esm"))
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        TransViewList.Clear();
                    }));
                    GlobalEspReader.DefReadMod(FilePath);
                    CanSetSelecter.Clear();
                    CanSetSelecter.AddRange(SkyrimDataLoader.QueryParams(GlobalEspReader));
                    CancelTransBtn.Opacity = 1;
                    CancelTransBtn.IsEnabled = true;
                    ReSetTransTargetType();
                    ReloadData();
                }
            }
        }

        public void ShowFrameByTag(string TagName)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                Grid SelectGrid = null;
                foreach (var GetView in Frames.Children)
                {
                    if (GetView is Grid)
                    {
                        if (ConvertHelper.ObjToStr((GetView as Grid).Tag) == TagName)
                        {
                            (GetView as Grid).Visibility = Visibility.Visible;
                            SelectGrid = (GetView as Grid);
                        }
                        else
                        {
                            (GetView as Grid).Visibility = Visibility.Hidden;
                        }
                    }
                }
                if (SelectGrid != null)
                {
                    Footer.Background = SelectGrid.Background;
                }
            }));

        }

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
                                ReloadData();
                                SizeChangeState = 1;
                            }
                            else
                            {
                                this.WindowState = WindowState.Normal;
                                ReloadData();
                                SizeChangeState = 0;
                            }

                        }
                        break;
                    case "Close":
                        {
                            Application.Current.Shutdown();
                        }
                        break;
                }
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
                    LockerGrid.Background = new SolidColorBrush(Color.FromRgb(244, 101, 155));
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
                    LockerGrid.Background = new SolidColorBrush(Color.FromRgb(247, 127, 172));
                }
            }
        }


        public int SmallNavState = 0;

        private void AutoShowSmallNav(object sender, MouseButtonEventArgs e)
        {
            if (SmallNavState == 0)
            {
                Storyboard GetStoryboard = this.Resources["ShowSmallNav"] as Storyboard;
                if (GetStoryboard != null)
                {
                    GetStoryboard.Begin();
                }

                SmallNavState = 1;
            }
            else
            {
                Storyboard GetStoryboard = this.Resources["HideSmallNav"] as Storyboard;
                if (GetStoryboard != null)
                {
                    GetStoryboard.Begin();
                }

                SmallNavState = 0;
            }
        }

        private void SelectEngine(object sender, MouseButtonEventArgs e)
        {

        }


        private void ChangeTransProcessState(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border || sender is SvgViewbox)
            {
                if (Translator.StopAny == true)
                {
                    ProcessStateBackGround.Background = new SolidColorBrush(Color.FromRgb(247, 137, 178));

                    AutoKeep.Source = new Uri("pack://application:,,,/YDSkyrimToolR;component/Material/Stop.svg"); ;
                    Translator.StopAny = false;
                }
                else
                {
                    ProcessStateBackGround.Background = new SolidColorBrush(Color.FromRgb(147, 85, 108));

                    AutoKeep.Source = new Uri("pack://application:,,,/YDSkyrimToolR;component/Material/Keep.svg"); ;
                    Translator.StopAny = true;
                }
                //.Source = ;
                ////.Source = new BitmapImage(new Uri("/Material/Setting.png", UriKind.Relative));
            }
        }

        private void AutoLoadEsp(object sender, MouseButtonEventArgs e)
        {
            string GetButtonContent = "";
            if (sender is Border)
            {
                GetButtonContent = ConvertHelper.ObjToStr(((sender as Border).Child as Label).Content);
            }
            if (sender is Label)
            {
                GetButtonContent = ConvertHelper.ObjToStr((sender as Label).Content);
            }

            string SetButtonContent = "";

            if (GetButtonContent.Equals("LoadFile"))
            {
                LoadESPOrESM();
                SetButtonContent = "SaveFile";
            }
            else
            {
                CancelTransBtn.Opacity = 0.3;
                CancelTransBtn.IsEnabled = false;
                SetButtonContent = "LoadFile";
            }

            if (sender is Border)
            {
                ((sender as Border).Child as Label).Content = SetButtonContent;
            }
            if (sender is Label)
            {
                (sender as Label).Content = SetButtonContent;
            }
        }

        private void CancelTransEsp(object sender, MouseButtonEventArgs e)
        {
            TransViewList.Clear();
            GlobalEspReader.Close();
            (StartTransBtn.Child as Label).Content = "LoadFile";
            CancelTransBtn.Opacity = 0.3;
            CancelTransBtn.IsEnabled = false;
        }

        public bool IsDragEnter = false;

        private void ModTransView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data != null)
            {
                string[] OneFile = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (OneFile == null)
                {
                    return;
                }
                if (OneFile.Length == 0)
                {
                    return;
                }
            }

            ModTransView.Visibility = Visibility.Collapsed;
            SmallMenu.Visibility = Visibility.Collapsed;
            DragDropView.Visibility = Visibility.Visible;
            IsDragEnter = true;
        }

        private void ModTransView_DragLeave(object sender, DragEventArgs e)
        {
            if (e != null)
                if (e.Data != null)
                {
                    string[] OneFile = (string[])e.Data.GetData(DataFormats.FileDrop);
                    if (OneFile == null)
                    {
                        return;
                    }
                    if (OneFile.Length == 0)
                    {
                        return;
                    }
                }

            ModTransView.Visibility = Visibility.Visible;
            SmallMenu.Visibility = Visibility.Visible;
            DragDropView.Visibility = Visibility.Collapsed;
            if (IsDragEnter)
            {
                IsDragEnter = false;
            }
        }
        private void ModTransView_Drop(object sender, DragEventArgs e)
        {
            if (e != null)
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] OneFile = (string[])e.Data.GetData(DataFormats.FileDrop);

                    if (OneFile.Length > 0)
                    {
                        string GetFilePath = OneFile[0];
                        if (File.Exists(GetFilePath))
                        {
                            LoadESPOrESM(GetFilePath);
                            ModTransView_DragLeave(null, null);
                        }
                    }
                }
        }

        private void TransTargetType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string GetSelectValue = ConvertHelper.ObjToStr((sender as ComboBox).SelectedValue);
            if (GetSelectValue.Trim().Length > 0)
            {
                CurrentSelect = Enum.Parse<ObjSelect>(GetSelectValue);
                ReloadData();
            }
        }
    }
}