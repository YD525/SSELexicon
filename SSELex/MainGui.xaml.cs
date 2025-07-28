using System.Diagnostics;
using System.IO;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using PhoenixEngine.ConvertManager;
using PhoenixEngine.EngineManagement;
using PhoenixEngine.TranslateCore;
using PhoenixEngine.TranslateManage;
using SSELex.SkyrimManage;
using SSELex.SkyrimModManager;
using SSELex.TranslateManage;
using SSELex.UIManage;
using SSELex.UIManagement;
using static SSELex.UIManage.SkyrimDataLoader;

namespace SSELex
{
    /// <summary>
    /// Interaction logic for MainGui.xaml
    /// </summary>
    public partial class MainGui : Window
    {
        #region Breathing Light
        public Storyboard? BreathingStoryboard = null;

        private void StartBreathingEffect()
        {
            BreathingStoryboard = (Storyboard)FindResource("BreathingEffect");
            Storyboard.SetTarget(BreathingStoryboard, NoteEffect);
            BreathingStoryboard.Begin();

        }

        private void StopBreathingEffect()
        {
            BreathingStoryboard?.Stop();
        }

        #endregion

        #region WinControl

        private void Min_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public int SizeChangeState = 0;
        private double OriginalLeft;
        private double OriginalTop;
        private double OriginalWidth;
        private double OriginalHeight;
        private void AutoMax_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var Screen = SystemParameters.WorkArea;

            if (SizeChangeState == 0)
            {
                OriginalLeft = this.Left;
                OriginalTop = this.Top;
                OriginalWidth = this.Width;
                OriginalHeight = this.Height;

                double TargetWidth = Screen.Width - 100;
                double TargetHeight = Screen.Height - 100;

                this.Width = TargetWidth;
                this.Height = TargetHeight;

                this.Left = Screen.Left + (Screen.Width - TargetWidth) / 2;
                this.Top = Screen.Top + (Screen.Height - TargetHeight) / 2;

                SizeChangeState = 1;
            }
            else
            {
                this.Left = OriginalLeft;
                this.Top = OriginalTop;
                this.Width = OriginalWidth;
                this.Height = OriginalHeight;

                SizeChangeState = 0;
            }
        }
        private void Close_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DeFine.CloseAny();
        }

        #endregion
        public MainGui()
        {
            InitializeComponent();
        }

        public YDListView TransViewList = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DeFine.Init(this);

            if (TransViewList == null)
            {
                TransViewList = new YDListView(TransView);
                TransViewList.Clear();
            }

            ReloadLanguageMode();

            GlobalEspReader = new EspReader();
            GlobalMCMReader = new MCMReader();
            GlobalPexReader = new PexReader();

            StartBreathingEffect();
            var Storyboard = (Storyboard)this.Resources["ScanAnimation"];
            Storyboard.Begin(this, true);

            //new Thread(() =>
            //{
            //    MessageBoxExtend.Show(this, "TestTittle", "TestLine", MsgAction.YesNo, MsgType.Info);
            //}).Start();

            ContextGeneration.IsChecked = true;
            RightContextIndicator.Visibility = Visibility.Visible;

            SyncConfig();

            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    try
                    {
                        GetStatisticsR();
                    }
                    catch { }
                }
            }).Start();
        }

        private void ModTransView_Drop(object sender, DragEventArgs e)
        {

        }

        private void ModTransView_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void ModTransView_DragLeave(object sender, DragEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (TransViewList != null)
            {
                if (TransViewList.RealLines != null)
                {
                    ReloadData(true);
                }
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                TransViewList.Down();
            }
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

        private void TransView_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        public void ShowLeftMenu(bool Show)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                if (Show)
                {
                    Mask.Visibility = Visibility.Visible;
                    Storyboard Storyboard = (Storyboard)this.Resources["ExpandMenu"];
                    Storyboard.Begin();

                    IsExpanded = true;
                }
                else
                {
                    Mask.Visibility = Visibility.Collapsed;
                    Storyboard Storyboard = (Storyboard)this.Resources["CollapseMenu"];
                    Storyboard.Begin();

                    IsExpanded = false;
                }
            }));
        }

        private bool IsExpanded = false;
        private void ShowLeftMenu(object sender, MouseButtonEventArgs e)
        {
            if (IsExpanded)
            {
                ShowLeftMenu(false);
            }
            else
            {
                ShowLeftMenu(true);
            }
        }
        private void Mask_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowLeftMenu(false);
        }


        private void ContextGeneration_Click(object sender, RoutedEventArgs e)
        {
            if (ContextGeneration.IsChecked == true)
            {
                RightContextIndicator.Visibility = Visibility.Visible;
            }
            else
            {
                RightContextIndicator.Visibility = Visibility.Collapsed;
            }

        }


        public void SetLog(string Str)
        {

        }

        public void GetStatisticsR()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                CalcStatistics();
            }));
        }

        public int MaxTransCount = 0;

        public void CalcStatistics()
        {
            try
            {
                int ModifyCount = Translator.TransData.Count(Kvp => !string.IsNullOrWhiteSpace(Kvp.Value));

                UIHelper.ModifyCount = ModifyCount;
            }
            catch { }

            try
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    if (TransViewList != null)
                    {
                        double GetRate = ((double)UIHelper.ModifyCount / (double)TransViewList.Rows);
                        if (GetRate > 0)
                        {
                            try
                            {
                                ProcessBar.Width = ProcessBarControl.ActualWidth * GetRate;
                            }
                            catch { }
                        }
                        else
                        {
                            try
                            {
                                ProcessBar.Width = 0;
                            }
                            catch { }
                        }

                        MaxTransCount = TransViewList.Rows;

                        if (ReadTrdWorkState)
                        {
                            TransProcess.Content = string.Format("Loading({0}/{1})", UIHelper.ModifyCount, MaxTransCount);
                            TypeSelector.Opacity = 0.5;
                            TypeSelector.IsEnabled = false;
                        }
                        else
                        {
                            TransProcess.Content = string.Format("STRINGS({0}/{1})", UIHelper.ModifyCount, MaxTransCount);
                            TypeSelector.Opacity = 1;
                            TypeSelector.IsEnabled = true;
                        }
                    }
                }));
            }
            catch { }

        }

        public void ReloadLanguageMode()
        {
            LangTo.Items.Clear();
            foreach (var Get in UILanguageHelper.SupportLanguages)
            {
                LangTo.Items.Add(Get.ToString());
            }

            LangTo.SelectedValue = DeFine.GlobalLocalSetting.TargetLanguage.ToString();

            LangFrom.Items.Clear();
            foreach (var Get in UILanguageHelper.SupportLanguages)
            {
                LangFrom.Items.Add(Get.ToString());
            }

            LangFrom.SelectedValue = DeFine.GlobalLocalSetting.SourceLanguage.ToString();
        }



        //Champollion Auto Download
        public bool CheckINeed()
        {
            bool State = true;
            //Frist Check ToolPath
            if (!File.Exists(DeFine.GetFullPath(@"Tool\Champollion.exe")))
            {
                string Msg = "Please manually install the dependent program\n[https://github.com/Orvid/Champollion]\nPlease download the release version and put it in this path\n[" + DeFine.GetFullPath(@"Tool\") + "]\n Path required\n[" + DeFine.GetFullPath(@"Tool\Champollion.exe") + "]";
                MessageBoxExtend.Show(this, "HelpMsg", Msg, MsgAction.Yes, MsgType.Info);

                if (MessageBoxExtend.Show(this, "HelpMsg", "Do you want to download the Champollion component now?\nIf you need.Click Yes to jump to the URL", MsgAction.YesNo, MsgType.Info) > 0)
                {
                    Process.Start(new ProcessStartInfo("https://github.com/Orvid/Champollion/releases") { UseShellExecute = true });
                }
                State = false;
            }

            string CompilerPath = "";
            if (!SkyrimHelper.FindPapyrusCompilerPath(ref CompilerPath))
            {
                var GetStr = DeFine.GlobalLocalSetting.SkyrimPath + "Papyrus Compiler" + @"\PapyrusAssembler.exe";
                string Msg = "Please Download CreationKit [" + GetStr + "] Must exist. \n Your Need Configure SkyrimSE path";
                MessageBoxExtend.Show(this, "PEX File lacks support", Msg, MsgAction.Yes, MsgType.Info);
                //this.Dispatcher.Invoke(new Action(() =>
                //{
                //    ShowSettingsView(Settings.Game);
                //}));
                State = false;
            }
            return State;
        }

        public void LoadAny()
        {
            var Dialog = new System.Windows.Forms.OpenFileDialog();
            Dialog.Title = "Please select a file";
            Dialog.Filter = "All files|*.*";
            Dialog.Multiselect = false;

            if (Dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string SelectedFile = Dialog.FileName;
                LoadAny(SelectedFile);
            }
        }

        public int CurrentTransType = 0;
        public string FModName = "";
        public string LModName = "";
        string LastSetPath = "";

        public MCMReader? GlobalMCMReader = null;
        public EspReader? GlobalEspReader = null;
        public PexReader? GlobalPexReader = null;

        public List<ObjSelect> CanSetSelecter = new List<ObjSelect>();
        public ObjSelect CurrentSelect = ObjSelect.Null;

        public object LockerAddTrd = new object();
        public bool ReadTrdWorkState = false;
        public void ReloadDataFunc(bool UseHotReload = false)
        {
            lock (LockerAddTrd)
            {
                ReadTrdWorkState = true;

                if (!UseHotReload)
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        TransViewList.Clear();
                    }));
                }
                else
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        TransViewList.HotReload();
                    }));
                }

                if (!UseHotReload)
                {
                    if (CurrentTransType == 2)
                    {
                        SkyrimDataLoader.Load(CurrentSelect, GlobalEspReader, TransViewList);
                    }
                    else
                   if (CurrentTransType == 1)
                    {
                        foreach (var GetItem in GlobalMCMReader.MCMItems)
                        {
                            this.Dispatcher.Invoke(new Action(() =>
                            {
                                TransViewList.AddRowR(LineRenderer.CreatLine(GetItem.Type, GetItem.EditorID, GetItem.Key, GetItem.SourceText, GetItem.GetTextIfTransR(), 999));
                            }));
                        }
                    }
                    if (CurrentTransType == 3)
                    {
                        foreach (var GetItem in GlobalPexReader.Strings)
                        {
                            this.Dispatcher.Invoke(new Action(() =>
                            {
                                TransViewList.AddRowR(LineRenderer.CreatLine(GetItem.Type, GetItem.EditorID, GetItem.Key, GetItem.SourceText, GetItem.GetTextIfTransR(), GetItem.TranslationSafetyScore));
                            }));
                        }
                    }
                }

                ReadTrdWorkState = false;

                this.Dispatcher.Invoke(new Action(() =>
                {
                    TransViewList.UpdateVisibleRows(true);
                }));
            }
        }
        public bool CheckDictionary()
        {
            string SetPath = DeFine.GetFullPath(@"\Librarys\" + DeFine.CurrentModName + ".Json");
            if (File.Exists(SetPath))
            {
                return true;
            }
            return false;
        }

        public void ReSetTransTargetType()
        {
            TypeSelector.Items.Clear();
            foreach (var GetType in CanSetSelecter)
            {
                TypeSelector.Items.Add(GetType.ToString());
            }

            TypeSelector.SelectedValue = ObjSelect.All.ToString();
        }

        private System.Timers.Timer? ReloadDebounceTimer;
        private readonly object ReloadLock = new object();
        private bool UseHotReloadFlag;

        public void ReloadData(bool UseHotReload = false,bool ForceReload = false)
        {
            if (LoadSaveState != 1)
                return;
            lock (ReloadLock)
            {
                UseHotReloadFlag = UseHotReload;

                if (ForceReload)
                {
                    ReloadDebounceTimer = null;
                }

                if (ReloadDebounceTimer == null)
                {
                    ReloadDebounceTimer = new System.Timers.Timer(200);
                    ReloadDebounceTimer.AutoReset = false;
                    ReloadDebounceTimer.Elapsed += (s, e) =>
                    {
                        if (!ReadTrdWorkState)
                        {
                            new Thread(() =>
                            {
                                ReloadDataFunc(UseHotReloadFlag);
                            }).Start();
                        }
                    };
                }

                ReloadDebounceTimer.Stop();
                ReloadDebounceTimer.Start();
            }
        }
        public void ClosetTransTrd()
        {
            //StopAny = true;
            //AutoKeepTag.Background = new SolidColorBrush(Color.FromRgb(11, 116, 209));

            //AutoKeep.Source = new Uri("pack://application:,,,/SSELex;component/Material/Keep.svg");
            //BatchTranslationHelper.Close();
        }

        public void SetTittle(string Tittle = "")
        {
            if (Tittle.Trim().Length > 0)
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    Caption.Content = string.Format("SSELexicon XT - {0}",Tittle);
                }));
            }
            else
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    Caption.Content = "SSELexicon XT";
                }));
            }
           
        }

        public void LoadAny(string FilePath)
        {
            Translator.ClearAICache();
            SetLog("Load:" + FilePath);
            DashBoardService.Clear();
            ClosetTransTrd();
            if (System.IO.File.Exists(FilePath))
            {
                DeFine.CurrentDashBoardView.Open(FilePath);
                DeFine.GlobalLocalSetting.AutoLoadDictionaryFile = false;
                //FromStr.Text = "";
                //ToStr.Text = "";

                string GetFileName = FilePath.Substring(FilePath.LastIndexOf(@"\") + @"\".Length);
                //Caption.Text = GetFileName;

                DeFine.CurrentModName = GetFileName;

                string GetModName = GetFileName;
                FModName = LModName = GetModName;
                if (LModName.Contains("."))
                {
                    LModName = LModName.Substring(0, LModName.LastIndexOf("."));
                }

                SetTittle(FModName);

                if (!CheckDictionary())
                {
                    RefreshDictionary.Opacity = 0.5;
                }
                else
                {
                    RefreshDictionary.Opacity = 1;
                }

                YDDictionaryHelper.ReadDictionary(GetModName);

                if (FilePath.ToLower().EndsWith(".pex"))
                {
                    if (CheckINeed())
                    {
                        CurrentTransType = 3;

                        GlobalEspReader.Close();
                        GlobalMCMReader.Close();
                        GlobalPexReader.Close();

                        LastSetPath = FilePath;

                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            TransViewList.Clear();
                        }));

                        CanSetSelecter.Clear();

                        GlobalPexReader.LoadPexFile(LastSetPath);

                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            CancelBtn.Opacity = 1;
                            CancelBtn.IsEnabled = true;
                            LoadSaveState = 1;
                            LoadFileButton.Content = UILanguageHelper.SearchStateChangeStr("LoadFileButton", 1);
                        }));

                        ReSetTransTargetType();
                        ReloadData();
                    }
                }
                if (FilePath.ToLower().EndsWith(".txt"))
                {
                    CurrentTransType = 1;

                    GlobalEspReader.Close();
                    GlobalMCMReader.Close();
                    GlobalPexReader.Close();

                    LastSetPath = FilePath;

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        TransViewList.Clear();
                    }));

                    CanSetSelecter.Clear();

                    GlobalMCMReader.LoadMCM(LastSetPath);

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        CancelBtn.Opacity = 1;
                        CancelBtn.IsEnabled = true;
                        LoadSaveState = 1;
                        LoadFileButton.Content = UILanguageHelper.SearchStateChangeStr("LoadFileButton", 1);
                    }));

                    ReSetTransTargetType();
                    ReloadData();
                }
                if (FilePath.ToLower().EndsWith(".esp") || FilePath.ToLower().EndsWith(".esm") || FilePath.ToLower().EndsWith(".esl"))
                {
                    CurrentTransType = 2;

                    GlobalEspReader.Close();
                    GlobalMCMReader.Close();
                    GlobalPexReader.Close();

                    LastSetPath = FilePath;

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        TransViewList.Clear();
                    }));
                    GlobalEspReader.DefReadMod(FilePath);
                    CanSetSelecter.Clear();
                    CanSetSelecter.AddRange(SkyrimDataLoader.QueryParams(GlobalEspReader));

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        CancelBtn.Opacity = 1;
                        CancelBtn.IsEnabled = true;
                        LoadSaveState = 1;
                        LoadFileButton.Content = UILanguageHelper.SearchStateChangeStr("LoadFileButton", 1);
                    }));

                    ReSetTransTargetType();
                }
            }
        }

        private void CancelTransEsp(object sender, MouseButtonEventArgs e)
        {
            CancelAny();
        }

        public void CancelAny()
        {
            DeFine.CurrentModName = string.Empty;

            this.Dispatcher.Invoke(new Action(() =>
            {
                ClosetTransTrd();

                TransViewList.Clear();
                GlobalEspReader.Close();
                GlobalMCMReader.Close();
                GlobalPexReader.Close();

                LoadSaveState = 0;

                CancelBtn.Opacity = 0.3;
                CancelBtn.IsEnabled = false;

                TypeSelector.Items.Clear();
                YDDictionaryHelper.Close();

                UIHelper.ModifyCount = 0;
            }));
        }

        public int LoadSaveState = 0;
        private void AutoLoadOrSave(object sender, MouseButtonEventArgs e)
        {
            if (LoadSaveState == 0)
            {
                LoadSaveState = 1;
                LoadAny();
            }
            else
            {
                CalcStatistics();

                LoadSaveState = 0;

                CancelBtn.Opacity = 0.3;
                CancelBtn.IsEnabled = false;

                string GetFilePath = LastSetPath.Substring(0, LastSetPath.LastIndexOf(@"\")) + @"\";
                string GetFileFullName = LastSetPath.Substring(LastSetPath.LastIndexOf(@"\") + @"\".Length);
                string GetFileSuffix = GetFileFullName.Split('.')[1];
                string GetFileName = GetFileFullName.Split('.')[0];

                if (CurrentTransType == 3)
                {
                    if (UIHelper.ModifyCount > 0)
                        if (GlobalPexReader != null)
                        {
                            if (!GlobalPexReader.SavePexFile(LastSetPath))
                            {
                                MessageBox.Show("Build Script Error!");
                            }
                        }
                }
                if (CurrentTransType == 2)
                {
                    if (UIHelper.ModifyCount > 0)
                        if (GlobalEspReader != null)
                        {
                            if (GlobalEspReader.CurrentReadMod != null)
                            {
                                string GetBackUPPath = GetFilePath + GetFileFullName + ".backup";

                                if (!File.Exists(GetBackUPPath))
                                {
                                    File.Copy(LastSetPath, GetBackUPPath);
                                }

                                if (File.Exists(LastSetPath))
                                {
                                    File.Delete(LastSetPath);
                                }

                                SkyrimDataWriter.WriteAllMemoryData(ref GlobalEspReader);
                                GlobalEspReader.DefSaveMod(GlobalEspReader.CurrentReadMod, LastSetPath);

                                if (!File.Exists(LastSetPath))
                                {
                                    MessageBox.Show("Save File Error!");
                                    File.Copy(GetBackUPPath, LastSetPath);
                                }
                            }
                        }
                }
                else
                if (CurrentTransType == 1)
                {
                    if (UIHelper.ModifyCount > 0)
                        if (GlobalMCMReader != null)
                        {
                            string GetBackUPPath = GetFilePath + GetFileFullName + ".backup";

                            if (!File.Exists(GetBackUPPath))
                            {
                                File.Copy(LastSetPath, GetBackUPPath);
                            }

                            if (File.Exists(LastSetPath))
                            {
                                File.Delete(LastSetPath);
                            }

                            GlobalMCMReader.SaveMCMConfig(LastSetPath);

                            if (!File.Exists(LastSetPath))
                            {
                                MessageBox.Show("Save File Error!");
                                File.Copy(GetBackUPPath, LastSetPath);
                            }
                        }
                }

                TranslatorExtend.WriteDictionary();
                YDDictionaryHelper.CreatDictionary();

                CancelTransEsp(null, null);
                LoadFileButton.Content = UILanguageHelper.SearchStateChangeStr("LoadFileButton", 0);
            }
        }

        private void TransTargetType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string GetSelectValue = ConvertHelper.ObjToStr((sender as ComboBox).SelectedValue);
            if (GetSelectValue.Trim().Length > 0)
            {
                ClosetTransTrd();
                CurrentSelect = Enum.Parse<ObjSelect>(GetSelectValue);
                ReloadData();
            }
        }

        #region Search

        private System.Timers.Timer ?SearchTimer;
        public void StartSearch()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {

                if (SearchTimer == null)
                {
                    SearchTimer = new System.Timers.Timer(500);
                    SearchTimer.AutoReset = false;
                    SearchTimer.Elapsed += (s, e) =>
                    {
                        this.Dispatcher.Invoke(() => ReloadData());
                    };
                }
                SearchTimer.Stop();
                SearchTimer.Start();
            }));
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DeFine.CurrentSearchStr = SearchBox.Text.Trim();
            StartSearch();
        }

        private void SearchBox_MouseLeave(object sender, MouseEventArgs e)
        {
            DeFine.CurrentSearchStr = SearchBox.Text.Trim();
            StartSearch();
        }

        #endregion

        private void LangFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string GetValue = ConvertHelper.ObjToStr(LangFrom.SelectedValue);

            if (GetValue.Trim().Length > 0)
            {
                DeFine.GlobalLocalSetting.SourceLanguage = Enum.Parse<Languages>(GetValue);
            }
        }

        private void LangTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string GetValue = ConvertHelper.ObjToStr(LangTo.SelectedValue);

            if (GetValue.Trim().Length > 0)
            {
                DeFine.GlobalLocalSetting.TargetLanguage = Enum.Parse<Languages>(GetValue);
            }
        }

        private void ClearCache_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (TransViewList.Rows > 0)
            {
                int CallFuncCount = 0;
                if (ManualTranslation.IsChecked == true)
                {
                    Translator.ClearAICache();
                    if (Translator.ClearCloudCache(DeFine.CurrentModName))
                    {
                        Engine.Vacuum();
                    }
                    CallFuncCount++;
                }
                if (UserTranslation.IsChecked == true)
                {
                    TranslatorExtend.ReSetAllTransText();
                    CallFuncCount++;
                }

                if (CallFuncCount > 0)
                {
                    MessageBoxExtend.Show(this,"Done!");
                    DeFine.WorkingWin.ReloadData();
                }
            }
        }

        private void RefreshDictionary_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DeFine.CurrentModName.Trim().Length > 0)
            {
                int CallFuncCount = 0;

                string SetPath = DeFine.GetFullPath(@"\Librarys\" + DeFine.CurrentModName + ".Json");

                if (File.Exists(SetPath))
                {
                    File.Delete(SetPath);
                    CallFuncCount++;
                }

                MessageBoxExtend.Show(this, "Original source text has been refreshed from the current file.");

                if (CallFuncCount > 0)
                {
                    DeFine.WorkingWin.ReloadData();
                }
            }
        }

        public void SyncConfig()
        {
            if (DeFine.GlobalLocalSetting.ViewMode == "Normal")
            {
                EnableNormalModel();
            }
            else
            {
                EnableQuickModel();
            }

            LangFrom.SelectedValue = DeFine.GlobalLocalSetting.SourceLanguage.ToString();
            LangTo.SelectedValue = DeFine.GlobalLocalSetting.TargetLanguage.ToString();

            if (DeFine.GlobalLocalSetting.CanClearManualTranslation)
            {
                ManualTranslation.IsChecked = true;
            }
            else
            {
                ManualTranslation.IsChecked = false;
            }

            if (DeFine.GlobalLocalSetting.CanClearUserTranslation)
            {
                UserTranslation.IsChecked = true;
            }
            else
            {
                UserTranslation.IsChecked = false;
            }
        }

        public void EnableNormalModel()
        {
            DeFine.GlobalLocalSetting.ViewMode = "Normal";
            NormalModel.Style = (Style)this.FindResource("ModelSelected");
            QuickModel.Style = (Style)this.FindResource("ModelUnselected");

            NormalModelBlock.Visibility = Visibility.Visible;
            QuickModelBlock.Visibility = Visibility.Collapsed;

            ReloadData(true, true);
        }

        public void EnableQuickModel()
        {
            DeFine.GlobalLocalSetting.ViewMode = "Quick";
            NormalModel.Style = (Style)this.FindResource("ModelUnselected");
            QuickModel.Style = (Style)this.FindResource("ModelSelected");

            NormalModelBlock.Visibility = Visibility.Collapsed;
            QuickModelBlock.Visibility = Visibility.Visible;

            ReloadData(true, true);
        }
       
        private void NormalModel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            EnableNormalModel();
        }
        private void QuickModel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            EnableQuickModel();
        }
        private void ManualTranslation_Click(object sender, RoutedEventArgs e)
        {
            if (ManualTranslation.IsChecked == true)
            {
                DeFine.GlobalLocalSetting.CanClearManualTranslation = true;
            }
            else
            {
                DeFine.GlobalLocalSetting.CanClearManualTranslation = false;
            }
        }
        private void UserTranslation_Click(object sender, RoutedEventArgs e)
        {
            if (UserTranslation.IsChecked == true)
            {
                DeFine.GlobalLocalSetting.CanClearUserTranslation = true;
            }
            else 
            {
                DeFine.GlobalLocalSetting.CanClearUserTranslation = false;
            }
        }
    }
}
