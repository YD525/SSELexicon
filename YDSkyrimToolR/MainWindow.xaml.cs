using Mutagen.Bethesda.Plugins;
using SharpVectors.Converters;
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

        public void InitTransTargetType()
        {
            TransTargetType.Items.Clear();
            TransTargetType.Items.Add("Armor");
            TransTargetType.Items.Add("All");
        }

        public EspReader GlobalEspReader = null;
        public YDListView TransViewList = null;

        public void ReloadData()
        {
            this.Dispatcher.Invoke(new Action(() => {
                TransViewList.Clear();
            }));
            SkyrimDataLoader.LoadArmor(GlobalEspReader,TransViewList);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DeFine.Init(this);
            InitTransTargetType();
            new Thread(() => 
            {
                ShowFrameByTag("LoadingView");
                GlobalEspReader =new EspReader();
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

                //Try Load ESM C:\Users\Administrator\Desktop\TestPy\AA\2.esm

                GlobalEspReader.DefReadMod("C:\\Users\\Administrator\\Desktop\\TestPy\\AA\\2.esm");
                //StartTestCreat
                ReloadData();





            }).Start();
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

        private void AutoLoadFile(object sender, MouseButtonEventArgs e)
        {

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
    }
}