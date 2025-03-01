using GameFinder.StoreHandlers.Steam.Models.ValueTypes;
using Mutagen.Bethesda.Starfield;
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
using YDSkyrimToolR.ConvertManager;
using YDSkyrimToolR.TranslateCore;
using YDSkyrimToolR.UIManage;

namespace YDSkyrimToolR
{
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
            DeFine.GlobalLocalSetting.SaveConfig();
        }
    }
}
