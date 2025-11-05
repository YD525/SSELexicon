using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Mutagen.Bethesda.Plugins;
using PhoenixEngine.DataBaseManagement;
using PhoenixEngine.TranslateCore;
using PhoenixEngine.TranslateManage;
using PhoenixEngine.TranslateManagement;
using SSELex.ConvertManager;
using SSELex.SkyrimModManager;
using SSELex.TranslateManage;
using SSELex.UIManage;
using static PhoenixEngine.EngineManagement.DataTransmission;
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
            TranslatorExtend.RegListener("KeyWordsListen", new List<int>() { 2 }, new Action<int, object>((Sign, Any) =>
            {
                if (Any is PreTranslateCall)
                {
                    PreTranslateCall GetPreCall = (PreTranslateCall)Any;
                    if (GetPreCall.Key.Equals("TestLocalKey255"))
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            MatchedKeywords.Items.Clear();
                        }));

                        foreach (var GetKey in GetPreCall.ReplaceTags)
                        {
                            this.Dispatcher.Invoke(new Action(() =>
                            {
                                MatchedKeywords.Items.Add(GetKey.Rowid + "," + GetKey.Key + "->" + GetKey.Value);
                            }));
                        }

                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            Output.Text = GetPreCall.ReceiveString;
                        }));
                    }
                }
            }));
        }

        public void Init()
        {
            foreach (var Value in Enum.GetValues(typeof(ObjSelect)))
            {
                TypeSelector.Items.Add(Value.ToString());
            }
            TypeSelector.Items.Remove("All");

            TypeSelector.SelectedValue = ObjSelect.Null.ToString();

            foreach (var Get in UILanguageHelper.GetSupportedLanguages())
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
                            TargetFileName = GetItem.TargetFileName,
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

                    TranslationUnit NewUnit = new TranslationUnit(-1, "TestLocalKey255", GetType, GetFromStr, "", "", FilterFrom, FilterTo, 100);

                    new Thread(() =>
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            ExecuteBtn.Content = "Executing...";
                        }));
                        bool CanSleep = false;
                        bool CanAddCache = false;

                        var GetResult = Translator.QuickTrans(NewUnit, ref CanSleep,true);

                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            FinalText.Text = GetResult;
                            ExecuteBtn.Content = "Execute";
                        }));

                        Translator.ClearCloudCache(-1);

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

        public int ImportCount = 0;
        public string LeftOver = "";
        public void ProcessRecord(string Combined)
        {
            var Parts = Combined.Split(',');

            for (int i = 0; i < Parts.Length - 1; i++)
            {
                string Record = Parts[i];
                if (!string.IsNullOrWhiteSpace(Record))
                {
                    string[] Params = Record.Split('|');
                    if (Params.Length == 4)
                    {
                        try
                        {
                            if (AdvancedDictionary.AddItem(new AdvancedDictionaryItem(
                                string.Empty,
                                string.Empty,
                                SqlSafeCodec.Decode(Params[2]),
                                SqlSafeCodec.Decode(Params[3]),
                                Params[0],
                                Params[1],
                                1,
                                1,
                                string.Empty)))
                            {
                                ImportCount++;
                            }
                        }
                        catch(Exception Ex) 
                        {
                        
                        }
                    }
                }
            }

            LeftOver = Parts[Parts.Length - 1];
        }
        public void ProcessBuffer(char[] Buffer, int Count)
        {
            string Chunk = new string(Buffer, 0, Count);
            string Combined = LeftOver + Chunk;

            ProcessRecord(Combined);
        }

        public bool ExitAny = false;
        private void ExportAll(object sender, MouseButtonEventArgs e)
        {
            new Thread(() =>
            {
                ProcessWin.Dispatcher.Invoke(new Action(() =>
                {
                    ProcessWin.Visibility = Visibility.Visible;
                }));

                string SetOutPutPath = DeFine.GetFullPath(@"\Cache\Output.txt");

                if (File.Exists(SetOutPutPath))
                {
                    File.Delete(SetOutPutPath);
                }

                int Count = 0;
                int MaxPage = 1;
                int CurrentPage = 0;

                using (var Writer = new StreamWriter(SetOutPutPath, true, Encoding.UTF8))
                {
                    while (CurrentPage < MaxPage)
                    {
                        if (ExitAny) goto QuickExit;
                        var GetData = AdvancedDictionary.QueryByPage((int)FilterFrom, (int)FilterTo, CurrentPage);

                        Count += GetData.CurrentPage.Count;


                        foreach (var Get in GetData.CurrentPage)
                        {
                            if (ExitAny) goto QuickExit;
                            Writer.Write(string.Format("{0}|{1}|{2}|{3},", Get.From, Get.To, SqlSafeCodec.Encode(Get.Source), SqlSafeCodec.Encode(Get.Result)));
                        }

                        MaxPage = GetData.MaxPage;
                        CurrentPage++;

                        Log.Dispatcher.Invoke(new Action(() =>
                        {
                            Log.Content = string.Format("Exporting dictionary...({0}%)({1}Record)",
                            Math.Round(((double)(CurrentPage) / (double)MaxPage) * 100, 0),
                            Count);
                        }));
                    }
                }

                string TimeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                this.Dispatcher.Invoke(new Action(() =>
                {
                    var GetWritePath = DataHelper.ShowSaveFileDialog("Sql_" + TimeStamp + ".txt", "Text (*.txt)|*.txt");

                    if (GetWritePath != null)
                    {
                        File.Copy(SetOutPutPath, GetWritePath);

                        if (SetOutPutPath.ToLower().EndsWith(".txt"))
                        {
                            File.Delete(SetOutPutPath);
                        }
                    }
                }));

                QuickExit:
                Thread.Sleep(100);
                ExitAny = false;
                ProcessWin.Dispatcher.Invoke(new Action(() =>
                {
                    ProcessWin.Visibility = Visibility.Hidden;
                }));
            }).Start();
        }

        private void CancelProcess(object sender, MouseButtonEventArgs e)
        {
            ExitAny = true;
        }
        private void ImportTable(object sender, MouseButtonEventArgs e)
        {
            ImportCount = 0;
            LeftOver = string.Empty;

            var Dialog = new System.Windows.Forms.OpenFileDialog();
            Dialog.Title = "Please select a file";
            Dialog.Filter = "Text|*.txt";
            Dialog.Multiselect = false;

            if (Dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string SelectedFile = Dialog.FileName;

                if (File.Exists(SelectedFile))
                {
                    new Thread(() =>
                    {
                        ProcessWin.Dispatcher.Invoke(new Action(() =>
                        {
                            ProcessWin.Visibility = Visibility.Visible;
                        }));

                        int BufferSize = 1024 * 2;
                        char[] Buffer = new char[BufferSize];

                        using (var Reader = new StreamReader(SelectedFile, Encoding.UTF8))
                        {
                            while (true)
                            {
                                if (ExitAny) goto QuickExit;

                                int ReadCount = Reader.ReadBlock(Buffer, 0, BufferSize);
                                if (ReadCount == 0) break;   // EOF

                                ProcessBuffer(Buffer, ReadCount);

                                Log.Dispatcher.Invoke(new Action(() =>
                                {
                                    Log.Content = string.Format("Number of imported records:({0})", ImportCount);
                                }));
                            }

                            if (!string.IsNullOrWhiteSpace(LeftOver))
                            {
                                ProcessRecord(LeftOver);
                            }

                            QuickExit:
                            Thread.Sleep(100);
                            ExitAny = false;
                            ProcessWin.Dispatcher.Invoke(new Action(() =>
                            {
                                ProcessWin.Visibility = Visibility.Hidden;
                            }));
                        }

                    }).Start();
                }
            }
        }
    }
}
