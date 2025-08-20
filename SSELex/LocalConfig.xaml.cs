using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PhoenixEngine.DataBaseManagement;
using PhoenixEngine.TranslateCore;
using PhoenixEngine.TranslateManage;
using PhoenixEngine.TranslateManagement;
using SSELex.ConvertManager;
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

        #region Default Form Behavior

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
                SFrom.Items.Add(Get.ToString());
                To.Items.Add(Get.ToString());
                STo.Items.Add(Get.ToString());
            }

            From.Items.Remove(Languages.Auto.ToString());
            SFrom.Items.Remove(Languages.Auto.ToString());
            To.Items.Remove(Languages.Auto.ToString());
            STo.Items.Remove(Languages.Auto.ToString());

            AutoDetect.IsChecked = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void FromStr_TextChanged(object sender, TextChangedEventArgs e)
        {
            SFrom.SelectedValue = LanguageHelper.DetectLanguageByLine(FromStr.Text).ToString();
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

        public void AutoReload()
        {
            if (FilterFrom != Languages.Null && FilterTo != Languages.Null)
            {
                if (FilterFrom != FilterTo)
                {
                    Pages = AdvancedDictionary.QueryByPage((int)FilterFrom, (int)FilterTo, CurrentPage);
                    KeywordList.Items.Clear();
                    foreach (var GetItem in Pages.CurrentPage)
                    {
                        KeywordList.Items.Add(new
                        {
                            TargetModName = GetItem.TargetModName,
                            Type = GetItem.Type,
                            Source = GetItem.Source,
                            Result = GetItem.Result,
                            From = GetItem.From,
                            To = GetItem.To,
                            ExactMatch = GetItem.ExactMatch,
                            IgnoreCase = GetItem.IgnoreCase,
                            Regex = GetItem.Regex,
                            Rowid = GetItem.Rowid
                        });
                    }

                    MaxPage = Pages.MaxPage;
                    CurrentPage = Pages.PageNo;

                    if (MaxPage > 0)
                    {
                        PageInFo.Content = string.Format("{0}/{1}", CurrentPage, MaxPage);
                    }
                    else
                    {
                        PageInFo.Content = "0/0";
                    }

                    if (KeywordList.Items.Count > 0)
                    {
                        var FristItem = KeywordList.Items[0];
                        KeywordList.ScrollIntoView(FristItem);
                    }
                }
                else
                {
                    KeywordList.Items.Clear();
                }
            }
            else
            {
                KeywordList.Items.Clear();
            }
        }

        public int CanReload = 1;

        public PageItem<List<AdvancedDictionaryItem>>? Pages = null;

        public int CurrentPage = 1;
        public int MaxPage = 0;
        public Languages FilterFrom = Languages.Null;
        public Languages FilterTo = Languages.Null;

        private void SFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string GetLang = ConvertHelper.ObjToStr(SFrom.SelectedValue);
            if (GetLang.Trim().Length > 0)
            {
                FilterFrom = (Languages)Enum.Parse(typeof(Languages), GetLang.Trim());
            }
            if (CanReload > 0)
                AutoReload();
        }

        private void STo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string GetLang = ConvertHelper.ObjToStr(STo.SelectedValue);
            if (GetLang.Trim().Length > 0)
            {
                FilterTo = (Languages)Enum.Parse(typeof(Languages), GetLang.Trim());
            }
            if (CanReload > 0)
                AutoReload();
        }

        public void SetOutput(string Str)
        {
            try
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    if (this.Visibility == Visibility.Visible)
                    {
                        Output.Text = Str;
                    }
                }));
            }
            catch { }
        }

        public void SetKeyWords(List<ReplaceTag> KeyWords)
        {
            try
            {
                List<ReplaceTag> CopyKeyWords = new List<ReplaceTag>();
                CopyKeyWords.AddRange(KeyWords);
                this.Dispatcher.Invoke(new Action(() =>
                {
                    if (this.Visibility == Visibility.Visible)
                    {
                        MatchedKeywords.Items.Clear();
                        foreach (var Get in CopyKeyWords)
                        {
                            if (Get.Key.StartsWith("__(") && Get.Key.EndsWith(")__"))
                            {
                                string Source = AdvancedDictionary.GetSourceByRowid(Get.Rowid);
                                MatchedKeywords.Items.Add(Get.Rowid + "," + Source);
                            }
                            else
                            {
                                MatchedKeywords.Items.Add(Get.Rowid + "," + Get.Key);
                            }
                        }
                    }
                }));
            }
            catch { }
        }

        private void AddKeyWord(object sender, MouseButtonEventArgs e)
        {
            string FromStr = ConvertHelper.ObjToStr(From.SelectedValue);
            string ToStr = ConvertHelper.ObjToStr(To.SelectedValue);

            int FromID = 0;
            if (FromStr.Trim().Length > 0)
            {
                FromID = (int)(Languages)Enum.Parse(typeof(Languages), FromStr.Trim());
            }

            int ToID = 0;
            if (ToStr.Trim().Length > 0)
            {
                ToID = (int)(Languages)Enum.Parse(typeof(Languages), ToStr.Trim());
            }


            if (FromStr.Trim().Length == 0 || ToStr.Trim().Length == 0)
            {
                MessageBox.Show("Source language or target language cannot be empty.");
                return;
            }
            if (FromStr == ToStr)
            {
                MessageBox.Show("The source and target languages ​​cannot be the same.");
                return;
            }

            if (SourceStr.Text.Trim().Length == 0)
            {
                MessageBox.Show("Source text cannot be empty.");
                return;
            }

            if (TargetStr.Text.Trim().Length == 0)
            {
                MessageBox.Show("Result text cannot be empty.");
                return;
            }

            int GetExactMatch = 0;
            if (ExactMatch.IsChecked == true)
            {
                GetExactMatch = 1;
            }
            int GetIgnoreCase = 0;
            if (IgnoreCase.IsChecked == true)
            {
                GetIgnoreCase = 1;
            }

            string GetType = ConvertHelper.ObjToStr(TypeSelector.SelectedValue);
            if (GetType.Equals(ObjSelect.Null.ToString()))
            {
                GetType = string.Empty;
            }

            if (AdvancedDictionary.AddItem(new AdvancedDictionaryItem(TargetModName.Text, GetType, SourceStr.Text, TargetStr.Text, FromID, ToID, GetExactMatch, GetIgnoreCase, Regex.Text)))
            {
                SFrom.SelectedValue = FromStr;
                STo.SelectedValue = ToStr;
                CanReload = 0;
                AutoReload();
                CanReload = 1;
            }
        }
        private void Previous_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                AutoReload();
            }
        }

        private void Next_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CurrentPage < MaxPage)
            {
                CurrentPage++;
                AutoReload();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Pages != null)
            {
                foreach (var GetItem in KeywordList.SelectedItems)
                {
                    int Rowid = ConvertHelper.ObjToInt(ConvertHelper.ObjToStr(KeywordList.SelectedItem.GetType().GetProperty("Rowid").GetValue(GetItem, null)));
                    AdvancedDictionary.DeleteByRowid(Rowid);
                }

                AutoReload();
            }

        }

        private void Execute_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            string GetBtnContent = ConvertHelper.ObjToStr(ExecuteBtn.Content);
            string GetType = ConvertHelper.ObjToStr(TypeSelector.SelectedValue);
            if (GetBtnContent.Equals("Execute"))
            {
                if (FilterFrom != Languages.Null && FilterTo != Languages.Null)
                {
                    FinalText.Text = string.Empty;
                    MatchedKeywords.Items.Clear();
                    Output.Text = string.Empty;

                    string GetFromStr = FromStr.Text;

                    new Thread(() =>
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            ExecuteBtn.Content = "Executing...";
                        }));
                        bool CanSleep = false;
                        bool CanAddCache = false;
                        var GetResult = Translator.QuickTrans("Test", GetType, DeFine.WorkingWin.TransViewList.GetSelectedKey(), GetFromStr, FilterFrom, FilterTo, ref CanSleep,ref CanAddCache);

                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            FinalText.Text = GetResult;
                            ExecuteBtn.Content = "Execute";
                        }));

                        Translator.ClearCloudCache("Test");

                    }).Start();
                }
                else
                {
                    MessageBox.Show("Both source and target languages ​​must be set.");
                }
            }
        }

        private void DeleteSelectItem(object sender, MouseButtonEventArgs e)
        {
            List<string> Removes = new List<string>();
            foreach (var Get in MatchedKeywords.SelectedItems)
            {
                string GetStr = ConvertHelper.ObjToStr(Get);
                if (GetStr.Contains(","))
                {
                    Removes.Add(GetStr);
                    int Rowid = ConvertHelper.ObjToInt(GetStr.Split(',')[0]);
                    AdvancedDictionary.DeleteByRowid(Rowid);
                }
            }

            foreach (var Get in Removes)
            {
                MatchedKeywords.Items.Remove(Get);
            }

            AutoReload();
        }
    }
}
