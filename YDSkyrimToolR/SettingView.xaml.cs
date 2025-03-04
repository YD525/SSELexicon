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
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

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
            MaxThreadCount.Text = ConvertHelper.ObjToStr(DeFine.GlobalLocalSetting.MaxThreadCount);
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
                    LockerGrid.Background = DeFine.SelectBackGround;
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
                    LockerGrid.Background = DeFine.DefBackGround;
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

        private void MaxThreadCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            DeFine.GlobalLocalSetting.MaxThreadCount = ConvertHelper.ObjToInt(MaxThreadCount.Text);
        }

        private void ShowSkyrimPathConfigHelp(object sender, MouseButtonEventArgs e)
        {
            ActionWin.Show("SkyrimPathConfigHelpMsg", @"If you have installed CK, there should be a Papyrus Compiler folder in the game root directory, which is necessary for compiling PEX. If you don't have the game installed and haven't downloaded CK, then manually create a folder Skyrim Special Edition in any directory, then create Papyrus Compiler in the directory and put all the associated files in it. After everything is done, there should be PapyrusAssembler.exe and PapyrusCompiler.exe in this directory. Then fill in the text box with your Skyrim Special Edition path.Look Like [C:\xxxx\Skyrim Special] That's it.", MsgAction.Yes, MsgType.Info, 390);
        }
    }
}
