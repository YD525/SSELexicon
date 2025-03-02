using GameFinder.StoreHandlers.Steam.Models.ValueTypes;
using Mutagen.Bethesda.Starfield;
using System;
using System.Collections.Generic;
using System.IO;
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
using YDSkyrimToolR.ConvertManager;
using YDSkyrimToolR.TranslateCore;
using YDSkyrimToolR.UIManage;

namespace YDSkyrimToolR
{
    /*
* @Author: YD525
* @GitHub: https://github.com/YD525/YDSkyrimToolR
* @Date: 2025-02-06
*/
    /// <summary>
    /// Interaction logic for SettingView.xaml
    /// </summary>
    public partial class SettingView : Window
    {
        public SettingView()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SkyrimSEPath.Text = DeFine.GlobalLocalSetting.SkyrimPath;
            Languages.Items.Clear();

            foreach (var Get in UILanguageHelper.SupportLanguages)
            {
                Languages.Items.Add(Get.ToString());
            }

            Languages.SelectedValue = DeFine.GlobalLocalSetting.CurrentUILanguage.ToString();

            BaiDuAppID.Text = DeFine.GlobalLocalSetting.BaiDuAppID;
            BaiDuKey.Password = DeFine.GlobalLocalSetting.BaiDuSecretKey;
            ChatGptKey.Password = DeFine.GlobalLocalSetting.ChatGptKey;
            DeepSeekKey.Password = DeFine.GlobalLocalSetting.DeepSeekKey;
        }

        private void Languages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeFine.GlobalLocalSetting.CurrentUILanguage = Enum.Parse<Languages>(ConvertHelper.ObjToStr(Languages.SelectedValue));
            UILanguageHelper.ChangeLanguage(DeFine.GlobalLocalSetting.CurrentUILanguage);
            DeFine.GlobalLocalSetting.SaveConfig();
        }

        private void BaiDuAppID_TextChanged(object sender, TextChangedEventArgs e)
        {
            DeFine.GlobalLocalSetting.BaiDuAppID = BaiDuAppID.Text;
        }

        private void BaiDuKey_PasswordChanged(object sender, RoutedEventArgs e)
        {
            DeFine.GlobalLocalSetting.BaiDuSecretKey = BaiDuKey.Password;
        }

        private void ChatGptKey_PasswordChanged(object sender, RoutedEventArgs e)
        {
            DeFine.GlobalLocalSetting.ChatGptKey = ChatGptKey.Password;
        }

        private void DeepSeekKey_PasswordChanged(object sender, RoutedEventArgs e)
        {
            DeFine.GlobalLocalSetting.DeepSeekKey = DeepSeekKey.Password;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MakeReady();

            e.Cancel = true;
            this.Hide();
            DeFine.GlobalLocalSetting.SaveConfig();
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

        public void MakeReady()
        {
            if (!DeFine.GlobalLocalSetting.SkyrimPath.EndsWith(@"\"))
            {
                if (Directory.Exists(DeFine.GlobalLocalSetting.SkyrimPath))
                {
                    DeFine.GlobalLocalSetting.SkyrimPath += @"\";
                    this.SkyrimSEPath.Text = DeFine.GlobalLocalSetting.SkyrimPath;
                }
            }
        }

        private void CloseThis(object sender, MouseButtonEventArgs e)
        {
            MakeReady();
            this.Hide();
            DeFine.GlobalLocalSetting.SaveConfig();
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

        private void SkyrimSEPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            DeFine.GlobalLocalSetting.SkyrimPath = SkyrimSEPath.Text;
        }
    }
}
