using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SSELex.ConvertManager;
using SSELex.TranslateCore;
using SSELex.UIManage;
using static SSELex.UIManage.SkyrimDataLoader;

namespace SSELex
{
    /// <summary>
    /// Interaction logic for LocalConfig.xaml
    /// </summary>
    public partial class LocalConfig : Window
    {
        public LocalConfig()
        {
            InitializeComponent();
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void Init()
        {
            foreach (var Value in Enum.GetValues(typeof(ObjSelect)))
            {
                TypeSelector.Items.Add(Value.ToString());
            }
            TypeSelector.Items.Remove("All");

            TypeSelector.SelectedValue = ObjSelect.Null.ToString();

            foreach (var Get in UILanguageHelper.SupportLanguages)
            {
                From.Items.Add(Get.ToString());
                To.Items.Add(Get.ToString());
            }

            From.Items.Remove(Languages.Auto.ToString());
            To.Items.Remove(Languages.Auto.ToString());

            AutoDetect.IsChecked = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void SourceStr_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AutoDetect.IsChecked == true)
            {
                From.SelectedValue = LanguageHelper.DetectLanguageByLine(SourceStr.Text).ToString();
            }
        }

        private void TargetStr_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AutoDetect.IsChecked == true)
            {
                To.SelectedValue = LanguageHelper.DetectLanguageByLine(TargetStr.Text).ToString();
            }
        }
    }
}
