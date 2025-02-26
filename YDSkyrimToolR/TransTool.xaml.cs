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
using YDSkyrimToolR.TranslateManage;

namespace YDSkyrimToolR
{
    /// <summary>
    /// Interaction logic for TransTool.xaml
    /// </summary>
    public partial class TransTool : Window
    {
        public TransTool()
        {
            InitializeComponent();
            WordProcess.SendTranslateMsg += TranslateMsg;
        }
        int AutoRow = 0;
        public void TranslateMsg(string EngineName, string Text, string Result)
        {
            AutoRow++;
            this.Dispatcher.Invoke(new Action(() => 
            {
                if (this.Log.Items.Count > 99)
                {
                    this.Log.Items.RemoveAt(0);
                }

                this.Log.Items.Add(AutoRow + "." + string.Format("{0}->{1}->{2}", EngineName, Text, Result));
                this.Log.ScrollIntoView(this.Log.Items[this.Log.Items.Count-1]);
            }));
        }

        public void TranslateMsg(string Msg)
        {
            AutoRow++;
            this.Dispatcher.Invoke(new Action(() =>
            {
                if (this.Log.Items.Count > 99)
                {
                    this.Log.Items.RemoveAt(0);
                }

                this.Log.Items.Add(AutoRow + "." + Msg);
                this.Log.ScrollIntoView(this.Log.Items[this.Log.Items.Count - 1]);
            }));
        }

        public void QuickSearchStr(string Str)
        {
            if (DeFine.SourceLanguage == Languages.English && DeFine.SourceLanguage == Languages.Chinese)
            {
                var GetStr = LocalTrans.SearchLocalData(Str);
                LocalTransCN.Text = Str + "->" + GetStr;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void Appid_MouseLeave(object sender, MouseEventArgs e)
        {
            DeFine.GlobalLocalSetting.BaiDuAppID = Appid.Text.Trim();
            DeFine.GlobalLocalSetting.SaveConfig();
        }

        private void Key_MouseLeave(object sender, MouseEventArgs e)
        {
            DeFine.GlobalLocalSetting.BaiDuSecretKey = Key.Password.Trim();
            DeFine.GlobalLocalSetting.SaveConfig();
        }

        private void DeepSeekKey_MouseLeave(object sender, MouseEventArgs e)
        {
            DeFine.GlobalLocalSetting.DeepSeekKey = DeepSeekKey.Password.Trim();
            DeFine.GlobalLocalSetting.SaveConfig();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = DeFine.WorkingWin.Left;
            this.Width = DeFine.WorkingWin.Width;
            this.Top = DeFine.WorkingWin.Top + DeFine.WorkingWin.Height;
            Appid.Text = DeFine.GlobalLocalSetting.BaiDuAppID;
            Key.Password = DeFine.GlobalLocalSetting.BaiDuSecretKey;
            DeepSeekKey.Password = DeFine.GlobalLocalSetting.DeepSeekKey;
            this.Owner = DeFine.WorkingWin;
        }

        private void StartTransing(object sender, MouseButtonEventArgs e)
        {
            if (ConvertHelper.ObjToStr(TransControlBtn.Content) == "StartTranslation")
            {
                AutoRow = 0;
                WordProcess.StartAutoTransService(false);
                WordProcess.StartAutoTransService(true);
                TransControlBtn.Content = "StopTranslation";
            }
            else
            {
                WordProcess.StartAutoTransService(false);
                this.Dispatcher.Invoke(new Action(() =>
                {
                    TransControlBtn.Content = "StartTranslation";
                }));
            }
        }

        
        private void EditEngineRule(object sender, MouseButtonEventArgs e)
        {
            new EngineEditView().Show();
        }
    }
}
