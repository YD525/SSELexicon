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
using SSELex.TranslateManage;
using static PhoenixEngine.SSELexiconBridge.NativeBridge;

namespace SSELex
{
    /// <summary>
    /// Interaction logic for ReplaceWin.xaml
    /// </summary>
    public partial class ReplaceWin : Window
    {
        public ReplaceWin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Mode.Items.Clear();
            Mode.Items.Add("Source text");
            Mode.Items.Add("Translated text");

            Mode.SelectedValue = Mode.Items[Mode.Items.Count-1];

            Scope.Items.Clear();
            Scope.Items.Add("Current");
            Scope.Items.Add("All");

            Scope.SelectedValue = Scope.Items[0];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SourceStr.Text.Trim().Length > 0 && TargetStr.Text.Trim().Length > 0)
            {
                string GetMode = ConvertHelper.ObjToStr(Mode.SelectedValue);
                string GetScope = ConvertHelper.ObjToStr(Scope.SelectedValue);

                if (GetMode.Equals("Source text"))
                {
                    if (GetScope.Equals("Current"))
                    {
                        DeFine.WorkingWin.ToStr.Text = DeFine.WorkingWin.FromStr.Text.Replace(SourceStr.Text,TargetStr.Text);
                    }
                    else
                    {
                        if (DeFine.WorkingWin.TransViewList != null)
                        {
                            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
                            {
                                var GetRow = DeFine.WorkingWin.TransViewList.RealLines[i];

                                GetRow.SyncData();

                                if (GetRow.SourceText.Contains(SourceStr.Text))
                                {
                                    string GetNewTrans = GetRow.SourceText.Replace(SourceStr.Text, TargetStr.Text);

                                    GetRow.TransText = GetNewTrans;

                                    try
                                    {
                                        TranslatorBridge.SetTransData(GetRow.Key, GetRow.SourceText, GetRow.TransText);
                                    }
                                    catch { }

                                    TranslatorExtend.SetTranslatorHistoryCache(GetRow.Key, GetRow.TransText);

                                    GetRow.SyncUI(DeFine.WorkingWin.TransViewList);
                                }
                            }
                        }
                    }
                }
                else
                if (GetMode.Equals("Translated text"))
                {
                    if (GetScope.Equals("Current"))
                    {
                        DeFine.WorkingWin.ToStr.Text = DeFine.WorkingWin.ToStr.Text.Replace(SourceStr.Text, TargetStr.Text);
                    }
                    else
                    {
                        if (DeFine.WorkingWin.TransViewList != null)
                        {
                            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
                            {
                                var GetRow = DeFine.WorkingWin.TransViewList.RealLines[i];

                                GetRow.SyncData();

                                if (GetRow.TransText.Contains(SourceStr.Text))
                                {
                                    string GetNewTrans = GetRow.TransText.Replace(SourceStr.Text, TargetStr.Text);

                                    GetRow.TransText = GetNewTrans;

                                    try
                                    {
                                        TranslatorBridge.SetTransData(GetRow.Key, GetRow.SourceText, GetRow.TransText);
                                    }
                                    catch { }

                                    TranslatorExtend.SetTranslatorHistoryCache(GetRow.Key, GetRow.TransText);

                                  
                                    GetRow.SyncUI(DeFine.WorkingWin.TransViewList);
                                }
                            }
                        }
                    }
                }
            }

            SourceStr.Text = string.Empty;
            TargetStr.Text = string.Empty;

            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
