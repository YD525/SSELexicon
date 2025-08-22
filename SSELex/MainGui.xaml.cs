using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using PhoenixEngine.ConvertManager;
using PhoenixEngine.DelegateManagement;
using PhoenixEngine.EngineManagement;
using PhoenixEngine.RequestManagement;
using PhoenixEngine.SSELexiconBridge;
using PhoenixEngine.TranslateCore;
using PhoenixEngine.TranslateManage;
using PhoenixEngine.TranslateManagement;
using SSELex.FileManagement;
using SSELex.SkyrimManage;
using SSELex.SkyrimManagement;
using SSELex.SkyrimModManager;
using SSELex.TranslateManage;
using SSELex.UIManage;
using SSELex.UIManagement;
using static PhoenixEngine.SSELexiconBridge.NativeBridge;
using static PhoenixEngine.TranslateCore.LanguageHelper;
using static SSELex.SkyrimManage.EspReader;
using static SSELex.UIManage.SkyrimDataLoader;
using static SSELex.UIManagement.DashBoardService;

namespace SSELex
{
    /// <summary>
    /// Interaction logic for MainGui.xaml
    /// </summary>
    public partial class MainGui : Window
    {
        #region Breathing Light
        public Storyboard? XTGlowLoopStoryboard = null;
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

        public YDListView? SearchResultsViewList = null;
        public YDListView? TransViewList = null;

        private ScanAnimator? ScanAnimator = null;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DeFine.Init(this);

            TranslatorExtend.Init();

            this.DataContext = CurrentModel;

            InitDashBoard();

            DelegateHelper.SetBookTranslateCallback += BookTransCallBack;

            SetSelectedNav("TransHub");

            if (TransViewList == null)
            {
                TransViewList = new YDListView(TransView);
                TransViewList.Clear();
            }

            if (SearchResultsViewList == null)
            {
                SearchResultsViewList = new YDListView(SearchResultsView);
            }

            ReloadLanguageMode();

            GlobalRamCacheReader = new RamCacheReader();
            GlobalEspReader = new EspReader();
            GlobalMCMReader = new MCMReader();
            GlobalPexReader = new PexReader();

            ScanAnimator = new ScanAnimator(ScanTransform, ProcessBar, 60);

            LastSetLogButton = InputLogButton;

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

            SyncTransStateUI();

            Engine.From = DeFine.GlobalLocalSetting.SourceLanguage;
            Engine.To = DeFine.GlobalLocalSetting.TargetLanguage;

            SelectFristSettingNav();
        }


        #region Drag
        public bool IsDragEnter = false;

        private void ModTransView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data != null)
            {
                string[] OneFile = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (OneFile == null)
                {
                    return;
                }
                if (OneFile.Length == 0)
                {
                    return;
                }
            }

            ModTransView.Visibility = Visibility.Collapsed;
            DragDropView.Visibility = Visibility.Visible;
            IsDragEnter = true;
        }

        private void ModTransView_DragLeave(object sender, DragEventArgs e)
        {
            if (e != null)
                if (e.Data != null)
                {
                    string[] OneFile = (string[])e.Data.GetData(DataFormats.FileDrop);
                    if (OneFile == null)
                    {
                        return;
                    }
                    if (OneFile.Length == 0)
                    {
                        return;
                    }
                }

            ModTransView.Visibility = Visibility.Visible;
            DragDropView.Visibility = Visibility.Collapsed;
            if (IsDragEnter)
            {
                IsDragEnter = false;
            }
        }
        private void ModTransView_Drop(object sender, DragEventArgs e)
        {
            if (e != null)
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] OneFile = (string[])e.Data.GetData(DataFormats.FileDrop);

                    if (OneFile.Length > 0)
                    {
                        string GetFilePath = OneFile[0];
                        if (File.Exists(GetFilePath))
                        {
                            new Thread(() =>
                            {
                                this.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    LoadAny(GetFilePath);
                                }), System.Windows.Threading.DispatcherPriority.Background);
                            }).Start();

                            ModTransView_DragLeave(null, null);
                        }
                    }
                }
        }

        #endregion


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DeFine.CloseAny();
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

            AutoSizeHistoryList();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                if (TransView.IsHitTestVisible == true)
                {
                    e.Handled = true;

                    AutoApplyStr();

                    if (SearchResultsView.Visibility == Visibility.Visible)
                    {
                        SearchResultsViewList?.Down();
                    }
                    else
                    {
                        TransViewList?.Down();
                    }
                }
            }
            if (e.Key == Key.F2)
            {
                if (DeFine.GlobalLocalSetting.ViewMode == "Normal")
                {
                    ApplyTranslatedText();
                }
            }
            if (e.Key == Key.F1)
            {
                if (DeFine.GlobalLocalSetting.ViewMode == "Normal")
                {
                    TranslateCurrent();
                }
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

        public void ShowMenu(bool Show)
        {
            var Rotate = NavBtn.RenderTransform as RotateTransform;

            if (Show)
            {
                var Storyboard = this.FindResource("ExpandStoryboard") as Storyboard;

                if (Storyboard != null)
                {
                    Storyboard.Begin();
                }

                Mask.Visibility = Visibility.Visible;

                Mask.Tag = 1;

                if (Rotate != null)
                {
                    Rotate.Angle = 180;
                }

                MainNavIsExpanded = true;
            }
            else
            {
                var Storyboard = this.FindResource("CollapseStoryboard") as Storyboard;

                if (Storyboard != null)
                {
                    Storyboard.Begin();
                }

                Mask.Visibility = Visibility.Collapsed;

                Mask.Tag = 0;

                if (Rotate != null)
                {
                    Rotate.Angle = 0;
                }

                MainNavIsExpanded = false;
            }
        }

        public void ShowLeftMenu(bool Show)
        {
            if (DeFine.WorkingWin != null)
            {
                DeFine.WorkingWin.Dispatcher.Invoke(new Action(() =>
                {
                    if (Show)
                    {
                        UIHelper.LeftMenuIsShow = true;
                        Mask.Visibility = Visibility.Visible;
                        Storyboard Storyboard = (Storyboard)this.Resources["ExpandMenu"];
                        Storyboard.Begin();

                        IsExpanded = true;
                    }
                    else
                    {
                        UIHelper.LeftMenuIsShow = false;
                        Mask.Visibility = Visibility.Collapsed;
                        Storyboard Storyboard = (Storyboard)this.Resources["CollapseMenu"];
                        Storyboard.Begin();

                        IsExpanded = false;
                    }
                }));
            }
        }

        private bool IsExpanded = false;
        private void ShowLeftMenu(object sender, MouseButtonEventArgs e)
        {
            if (IsExpanded)
            {
                ShowLeftMenu(false);
                LogView.Visibility = Visibility.Collapsed;
            }
            else
            {
                ShowLeftMenu(true);
                LogView.Visibility = Visibility.Visible;
            }
        }


        private void Mask_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ConvertHelper.ObjToInt(Mask.Tag) == 0)
            {
                ShowLeftMenu(false);
                LogView.Visibility = Visibility.Collapsed;
            }
            else
            {
                ShowMenu(false);
                LogView.Visibility = Visibility.Collapsed;
            }
        }


        private void ContextGeneration_Click(object sender, RoutedEventArgs e)
        {
            if (ContextGeneration.IsChecked == true)
            {
                RightContextIndicator.Visibility = Visibility.Visible;
                EngineConfig.ContextEnable = true;
            }
            else
            {
                RightContextIndicator.Visibility = Visibility.Collapsed;
                EngineConfig.ContextEnable = false;
            }

            EngineConfig.Save();
        }


        public void SetLog(string Str)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                CurrentLog.Text = "Log: " + Str;
            }));
        }

        public void GetStatisticsR()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                CalcStatistics();
            }));
        }

        public int GlobalTransCount = 0;

        public void GetGlobalTransCount()
        {
            if (CurrentTransType == 2)
            {
                if (ConvertHelper.ObjToStr(TypeSelector.SelectedValue).Equals("All"))
                {
                    if (TransViewList != null)
                        GlobalTransCount = TransViewList.RealLines.Count;
                }
            }
            else
            {
                if (TransViewList != null)
                    GlobalTransCount = TransViewList.RealLines.Count;
            }
        }

        public void CalcStatistics()
        {
            try
            {
                int ModifyCount = Engine.TranslatedCount;

                this.Dispatcher.Invoke(new Action(() =>
                {
                    if (TranslatorExtend.TranslationCore != null)
                    {
                        if (ScanAnimator != null)
                        {
                            if (ModifyCount > 0 && TranslatorExtend.TranslationCore.IsWork && !TranslatorExtend.TranslationCore.IsStop)
                            {
                                ScanAnimator.Start();
                            }
                            else
                            {
                                ScanAnimator.Stop();
                            }
                        }

                        if (TranslatorExtend.TranslationCore.IsWork && !TranslatorExtend.TranslationCore.IsStop)
                        {
                            ThreadInFoFont.Content = string.Format("Thread(Current:{0},Max:{1})", TranslatorExtend.TranslationCore.ThreadUsage.CurrentThreads, EngineConfig.MaxThreadCount);
                        }
                        else
                        if (TranslatorExtend.TranslationCore.IsWork && TranslatorExtend.TranslationCore.IsStop)
                        {
                            ThreadInFoFont.Content = string.Format("Thread(Current:0,Max:{0})", EngineConfig.MaxThreadCount);
                        }
                    }

                    if (TransViewList != null)
                    {
                        GetGlobalTransCount();

                        if (ReadTrdWorkState)
                        {
                            if (TranslatorExtend.TranslationStatus == StateControl.Cancel || TranslatorExtend.TranslationStatus == StateControl.Null)
                            {
                                TransProcess.Content = string.Format("Loading({0}/{1})", ModifyCount, GlobalTransCount);
                            }

                            TypeSelector.Opacity = 0.5;
                            TypeSelector.IsEnabled = false;

                            ViewModel.Opacity = 0.5;
                            ViewModel.IsHitTestVisible = false;
                        }
                        else
                        {
                            if (TranslatorExtend.TranslationStatus == StateControl.Cancel || TranslatorExtend.TranslationStatus == StateControl.Null)
                            {
                                TransProcess.Content = string.Format("STRINGS({0}/{1})", ModifyCount, GlobalTransCount);
                            }

                            TypeSelector.Opacity = 1;
                            TypeSelector.IsEnabled = true;

                            ViewModel.Opacity = 1;
                            ViewModel.IsHitTestVisible = true;
                        }

                        double GetRate = ((double)ModifyCount / (double)GlobalTransCount);
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
                    }
                }));
            }
            catch { }
        }

        public void ReloadLanguageMode()
        {
            LangFrom.Items.Clear();
            foreach (var Get in UILanguageHelper.SupportLanguages)
            {
                LangFrom.Items.Add(Get.ToString());
            }

            LangFrom.SelectedValue = DeFine.GlobalLocalSetting.SourceLanguage.ToString();

            LangTo.Items.Clear();
            foreach (var Get in UILanguageHelper.SupportLanguages)
            {
                if (Get != Languages.Auto)
                {
                    LangTo.Items.Add(Get.ToString());
                }
            }

            LangTo.SelectedValue = DeFine.GlobalLocalSetting.TargetLanguage.ToString();
        }



        //Champollion Auto Download
        public bool CheckINeed()
        {
            bool State = true;
            //Frist Check ToolPath
            if (!File.Exists(DeFine.GetFullPath(@"Tool\Champollion.exe")))
            {
                string Msg = "Do you want to download the Champollion component?\nIf you click Yes, the program will automatically download and scan to ensure the file is safe and then install it in the Tool directory.";

                if (MessageBoxExtend.Show(this, "HelpMsg", Msg, MsgAction.YesNo, MsgType.Info) > 0)
                {
                    if (ToolDownloader.DownloadChampollion())
                    {
                        State = true;
                    }
                    else
                    {
                        MessageBoxExtend.Show(this, "HelpMsg", "Download failed. The source URL cannot be accessed or the file has changed.", MsgAction.Yes, MsgType.Info);
                        State = false;
                    }
                }
                else
                {
                    State = false;
                }
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

        public RamCacheReader? GlobalRamCacheReader = null;
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
                    else
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
                    else
                    if (CurrentTransType == 6)
                    {
                        foreach (var GetItem in GlobalRamCacheReader.RamLines)
                        {
                            this.Dispatcher.Invoke(new Action(() =>
                            {
                                TransViewList.AddRowR(LineRenderer.CreatLine(GetItem.Type, "", GetItem.Key, GetItem.SourceText, GetItem.TransText, GetItem.Score));
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
            string SetPath = DeFine.GetFullPath(@"\Librarys\" + Engine.GetModName() + ".Json");
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

        public void ReloadData(bool UseHotReload = false, bool ForceReload = false)
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
                    Caption.Content = string.Format("SSELexicon XT - {0}", Tittle);
                    this.Title = Tittle;
                }));
            }
            else
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    Caption.Content = "SSELexicon XT";
                    this.Title = "SSELexicon XT";
                }));
            }
        }

        public void LoadAny(string FilePath)
        {
            CancelBatchTranslation();

            IsValidFile = false;

            Translator.ClearAICache();
            SetLog("Load:" + FilePath);
            
            ClosetTransTrd();
            if (System.IO.File.Exists(FilePath))
            {
                DeFine.GlobalLocalSetting.AutoLoadDictionaryFile = false;
                //FromStr.Text = "";
                //ToStr.Text = "";

                string GetFileName = FilePath.Substring(FilePath.LastIndexOf(@"\") + @"\".Length);
                //Caption.Text = GetFileName;

                Engine.ChangeModName(GetFileName);

                string GetModName = GetFileName;
                FModName = LModName = GetModName;
                if (LModName.Contains("."))
                {
                    LModName = LModName.Substring(0, LModName.LastIndexOf("."));
                }

                if (!CheckDictionary())
                {
                    RefreshDictionary.Opacity = 0.5;
                }
                else
                {
                    RefreshDictionary.Opacity = 1;
                }

                CurrentTransType = 0;

                YDDictionaryHelper.ReadDictionary(GetModName);

                if (FilePath.ToLower().EndsWith(".json"))
                {
                    SetTittle(FModName);
                    CurrentTransType = 6;

                    GlobalRamCacheReader.Close();
                    GlobalEspReader.Close();
                    GlobalMCMReader.Close();
                    GlobalPexReader.Close();

                    LastSetPath = FilePath;

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        TransViewList.Clear();
                    }));

                    CanSetSelecter.Clear();

                    GlobalRamCacheReader.Load(LastSetPath);

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        CancelBtn.Opacity = 1;
                        CancelBtn.IsEnabled = true;
                        LoadSaveState = 1;
                    }));

                    ReSetTransTargetType();
                    ReloadData();

                    IsValidFile = true;
                }
                if (FilePath.ToLower().EndsWith(".pex"))
                {
                    if (CheckINeed())
                    {
                        SetTittle(FModName);
                        CurrentTransType = 3;

                        GlobalRamCacheReader.Close();
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
                        }));

                        ReSetTransTargetType();
                        ReloadData();

                        IsValidFile = true;
                    }
                }
                if (FilePath.ToLower().EndsWith(".txt"))
                {
                    SetTittle(FModName);
                    CurrentTransType = 1;

                    GlobalRamCacheReader.Close();
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
                    }));

                    ReSetTransTargetType();
                    ReloadData();

                    IsValidFile = true;
                }
                if (FilePath.ToLower().EndsWith(".esp") || FilePath.ToLower().EndsWith(".esm") || FilePath.ToLower().EndsWith(".esl"))
                {
                    SetTittle(FModName);
                    CurrentTransType = 2;

                    GlobalRamCacheReader.Close();
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
                    }));

                    ReSetTransTargetType();
                    //ReloadData();

                    IsValidFile = true;
                }

                if (CurrentTransType != 0)
                {
                    LoadSaveState = 1;
                    CheckLoadSaveButtonState();
                }
            }
        }

        public void CancelBatchTranslation()
        {
            try
            {
                TranslatorExtend.TranslationStatus = StateControl.Cancel;

                TranslatorExtend.SyncTransState(new Action(() =>
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        SyncTransStateUI();
                    }));
                }), false);
            }
            catch { }
        }

        private void CancelTransEsp(object sender, MouseButtonEventArgs e)
        {
            CancelAny();
        }

        public void CancelAny()
        {
            GlobalTransCount = 0;

            CancelBatchTranslation();

            EmptyFromAndToText();

            Engine.ChangeModName(string.Empty);

            this.Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    SearchBox.Text = string.Empty;
                    SearchResultsViewList?.Clear();
                    SearchResultsView.Visibility = Visibility.Collapsed;

                    LoadFileButton.Content = "LoadFile";

                    ClosetTransTrd();

                    TransViewList?.Clear();
                    GlobalEspReader?.Close();
                    GlobalMCMReader?.Close();
                    GlobalPexReader?.Close();

                    LoadSaveState = 0;

                    CancelBtn.Opacity = 0.3;
                    CancelBtn.IsEnabled = false;

                    TypeSelector.Items.Clear();
                    YDDictionaryHelper.Close();
                }
                catch { }
            }));

            SetTittle();

            new Thread(() =>
            {
                Thread.Sleep(500);

                this.Dispatcher.Invoke(new Action(() =>
                {
                    TransViewList.Clear();
                }));

                GlobalTransCount = 0;
            }).Start();
        }

        public bool IsValidFile = false;
        public int LoadSaveState = 0;
        private void AutoLoadOrSave(object sender, MouseButtonEventArgs e)
        {
            this.LoadFileButton.Dispatcher.Invoke(new Action(() =>
            {
                if (ConvertHelper.ObjToStr(LoadFileButton.Content).Equals("Saving..."))
                {
                    return;
                }
            }));

            if (LoadSaveState == 0)
            {
                LoadAny();
            }
            else
            {
                SaveFile();
            }

            CheckLoadSaveButtonState();
        }

        public void SaveFile()
        {
            new Thread(() =>
            {
                this.LoadFileButton.Dispatcher.Invoke(new Action(() =>
                {
                    LoadFileButton.Content = "Saving...";
                }));

                try
                {
                    CancelBatchTranslation();

                    EmptyFromAndToText();

                    CalcStatistics();

                    Thread.Sleep(100);

                    if (CurrentTransType == 6)
                    {
                        UPDateFile(false);
                    }
                    else
                    {
                        UPDateFile(true);
                    }

                    LoadSaveState = 0;

                    this.CancelBtn.Dispatcher.Invoke(new Action(() =>
                    {
                        CancelBtn.Opacity = 0.3;
                        CancelBtn.IsEnabled = false;
                    }));

                    string GetFilePath = LastSetPath.Substring(0, LastSetPath.LastIndexOf(@"\")) + @"\";
                    string GetFileFullName = LastSetPath.Substring(LastSetPath.LastIndexOf(@"\") + @"\".Length);
                    string GetFileSuffix = GetFileFullName.Split('.')[1];
                    string GetFileName = GetFileFullName.Split('.')[0];

                    if (CurrentTransType == 6)
                    {
                        if (Translator.TransData.Count > 0)
                            if (GlobalRamCacheReader != null)
                            {
                                if (!GlobalRamCacheReader.Save(LastSetPath))
                                {
                                    MessageBox.Show("Build RamCache Error!");
                                }
                            }
                    }
                    if (CurrentTransType == 3)
                    {
                        if (Translator.TransData.Count > 0)
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
                        if (Translator.TransData.Count > 0)
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
                        if (Translator.TransData.Count > 0)
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
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                CancelTransEsp(null, null);
            }).Start();
        }

        public void CheckLoadSaveButtonState()
        {
            if (IsValidFile)
            {
                if (LoadSaveState == 0)
                {
                    LoadFileButton.Content = "Load File";
                }
                else
                {
                    LoadFileButton.Content = "Save File";
                }
            }
            else
            {
                LoadFileButton.Content = "Load File";
                LoadSaveState = 0;
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

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            QuickSearch();
        }

        public void QuickSearch()
        {
            if (SearchBox.Text.Trim().Length > 0)
            {
                EmptyFromAndToText();

                TransView.Visibility = Visibility.Hidden;

                if (SearchResultsViewList != null && TransViewList != null)
                {
                    SearchResultsViewList.SelectLineID = -1;
                    SearchResultsViewList.IsSearchBox = true;
                    SearchResultsViewList.ParentView = TransViewList;

                    SearchResultsViewList.Clear();

                    foreach (var GetLine in TransViewList.QuickSearch(SearchBox.Text.Trim()))
                    {
                        SearchResultsViewList.AddRowR(GetLine);
                    }
                }

                SearchResultsView.Visibility = Visibility.Visible;
            }
            else
            {
                SearchResultsView.Visibility = Visibility.Collapsed;
                TransView.Visibility = Visibility.Visible;
            }
        }

        #endregion

        private void LangFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string GetValue = ConvertHelper.ObjToStr(LangFrom.SelectedValue);

            if (GetValue.Trim().Length > 0)
            {
                DeFine.GlobalLocalSetting.SourceLanguage = Enum.Parse<Languages>(GetValue);
                DeFine.LocalConfigView.SFrom.SelectedValue = GetValue;

                Engine.From = DeFine.GlobalLocalSetting.SourceLanguage;
            }
        }

        private void LangTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string GetValue = ConvertHelper.ObjToStr(LangTo.SelectedValue);

            if (GetValue.Trim().Length > 0)
            {
                DeFine.GlobalLocalSetting.TargetLanguage = Enum.Parse<Languages>(GetValue);
                DeFine.LocalConfigView.STo.SelectedValue = GetValue;

                Engine.To = DeFine.GlobalLocalSetting.TargetLanguage;
            }
        }

        public Thread? ClearCacheTrd = null;

        private void ClearCache_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (TransViewList != null)
            {
                if (TransViewList.Rows > 0)
                {
                    if (ConvertHelper.ObjToStr(ClearCacheFont.Content).Equals("ClearCache"))
                    {
                        if (ClearCacheTrd == null)
                        {
                            bool? GetCloudTranslationCache = CloudTranslationCache.IsChecked;
                            bool? GetUserTranslationCache = UserTranslationCache.IsChecked;

                            ClearCacheTrd = new Thread(() =>
                            {
                                try
                                {
                                    this.Dispatcher.Invoke(new Action(() =>
                                    {
                                        TransView.IsHitTestVisible = false;
                                    }));
                                    ClearCacheFont.Dispatcher.Invoke(new Action(() =>
                                    {
                                        ClearCacheFont.Content = "Cleaning..";
                                    }));

                                    int CallFuncCount = 0;
                                    if (GetCloudTranslationCache == true)
                                    {
                                        Translator.ClearAICache();
                                        if (Translator.ClearCloudCache(Engine.GetModName()))
                                        {
                                            Engine.Vacuum();
                                            CallFuncCount++;
                                        }

                                    }
                                    if (GetUserTranslationCache == true)
                                    {
                                        TranslatorExtend.ClearLocalCache(Engine.GetModName());
                                        {
                                            Engine.Vacuum();
                                            CallFuncCount++;
                                        }
                                    }

                                    if (CallFuncCount > 0)
                                    {
                                        Engine.GetTranslatedCount(Engine.GetModName());
                                        TranslatorExtend.ReSetAllTransText();

                                        this.Dispatcher.Invoke(new Action(() =>
                                        {
                                            TransView.IsHitTestVisible = true;
                                        }));
                                    }

                                }
                                catch { }

                                ClearCacheFont.Dispatcher.Invoke(new Action(() =>
                                {
                                    ClearCacheFont.Content = "ClearCache";
                                }));

                                ClearCacheTrd = null;
                            });

                            ClearCacheTrd.Start();
                        }
                    }
                }
            }
        }

        private void RefreshDictionary_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ConvertHelper.ObjToStr(RefreshButton.Content).Equals("Refreshing..."))
            {
                return;
            }

            new Thread(() =>
            {
                RefreshButton.Dispatcher.Invoke(new Action(() => {
                    RefreshButton.Content = "Refreshing...";
                }));
                var ModName = Engine.GetModName();

                if (ModName.Trim().Length > 0)
                {
                    int CallFuncCount = 0;

                    string SetPath = DeFine.GetFullPath(@"\Librarys\" + ModName + ".Json");

                    if (File.Exists(SetPath))
                    {
                        File.Delete(SetPath);
                        
                        CallFuncCount++;
                    }

                    YDDictionaryHelper.Dictionarys.Clear();

                    if (TransViewList != null)
                    {
                        for (int i = 0; i < TransViewList.Rows; i++)
                        {
                            bool IsCloud = false;
                            TransViewList.RealLines[i].SyncData(ref IsCloud);

                            this.Dispatcher.Invoke(new Action(() =>
                            {
                                TransViewList.RealLines[i].SyncUI(TransViewList);
                            }));
                        }
                    }

                    MessageBoxExtend.Show(this, "Original source text has been refreshed from the current file.");
                }

                RefreshButton.Dispatcher.Invoke(new Action(() => {
                    RefreshButton.Content = "Refresh";
                }));
            }).Start();
        }

        public void InitIDE()
        {
            string GetName = "SSELex" + ".IDERule.TextStyle.xshd";

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

            using (System.IO.Stream s = assembly.GetManifestResourceStream(GetName))
            {
                using (System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(s))
                {
                    var xshd = HighlightingLoader.LoadXshd(reader);

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        FromStr.SyntaxHighlighting = HighlightingLoader.Load(xshd, HighlightingManager.Instance);
                        ToStr.SyntaxHighlighting = HighlightingLoader.Load(xshd, HighlightingManager.Instance);
                    }));
                }
            }
        }


        public void SyncConfig()
        {
            InitIDE();

            if (DeFine.GlobalLocalSetting.ViewMode == "Normal")
            {
                EnableNormalModel();
            }
            else
            {
                EnableQuickModel();
                EmptyFromAndToText();
            }

            LangFrom.SelectedValue = DeFine.GlobalLocalSetting.SourceLanguage.ToString();
            LangTo.SelectedValue = DeFine.GlobalLocalSetting.TargetLanguage.ToString();

            if (DeFine.GlobalLocalSetting.CanClearCloudTranslationCache)
            {
                CloudTranslationCache.IsChecked = true;
            }
            else
            {
                CloudTranslationCache.IsChecked = false;
            }

            if (DeFine.GlobalLocalSetting.CanClearUserInputTranslationCache)
            {
                UserTranslationCache.IsChecked = true;
            }
            else
            {
                UserTranslationCache.IsChecked = false;
            }

            if (EngineConfig.ContextEnable)
            {
                ContextGeneration.IsChecked = true;
                RightContextIndicator.Visibility = Visibility.Visible;
            }
            else
            {
                ContextGeneration.IsChecked = false;
                RightContextIndicator.Visibility = Visibility.Collapsed;
            }

            SyncNodeStates();

            if (DeFine.GlobalLocalSetting.AutoSpeak)
            {
                AutoSpeak.IsChecked = true;
            }
            else
            {
                AutoSpeak.IsChecked = false;
            }

            if (DeFine.GlobalLocalSetting.AutoApply)
            {
                AutoApply.IsChecked = true;
            }
            else
            {
                AutoApply.IsChecked = false;
            }
        }

        public void EnableNormalModel()
        {
            DeFine.GlobalLocalSetting.ViewMode = "Normal";
            NormalModel.Style = (Style)this.FindResource("ModelSelected");
            QuickModel.Style = (Style)this.FindResource("ModelUnSelected");

            NormalModelBlock.Visibility = Visibility.Visible;
            QuickModelBlock.Visibility = Visibility.Collapsed;

            ReloadData(true, true);

            double AutoHeight = DeFine.GlobalLocalSetting.WritingAreaHeight;

            if (AutoHeight < 100)
            {
                AutoHeight = 300;
            }
            WritingArea.Height = new GridLength(AutoHeight, GridUnitType.Pixel);
            SplictLine.Height = new GridLength(3.5, GridUnitType.Pixel);
        }

        public void EnableQuickModel()
        {
            DeFine.GlobalLocalSetting.ViewMode = "Quick";
            NormalModel.Style = (Style)this.FindResource("ModelUnSelected");
            QuickModel.Style = (Style)this.FindResource("ModelSelected");

            NormalModelBlock.Visibility = Visibility.Collapsed;
            QuickModelBlock.Visibility = Visibility.Visible;

            ReloadData(true, true);

            WritingArea.Height = new GridLength(0, GridUnitType.Pixel);
            SplictLine.Height = new GridLength(0, GridUnitType.Pixel);
        }

        private void NormalModel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            EnableNormalModel();
        }
        private void QuickModel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            EnableQuickModel();
        }
        private void CloudTranslationCache_Click(object sender, RoutedEventArgs e)
        {
            if (CloudTranslationCache.IsChecked == true)
            {
                DeFine.GlobalLocalSetting.CanClearCloudTranslationCache = true;
            }
            else
            {
                DeFine.GlobalLocalSetting.CanClearCloudTranslationCache = false;
            }
        }
        private void UserTranslationCache_Click(object sender, RoutedEventArgs e)
        {
            if (UserTranslationCache.IsChecked == true)
            {
                DeFine.GlobalLocalSetting.CanClearUserInputTranslationCache = true;
            }
            else
            {
                DeFine.GlobalLocalSetting.CanClearUserInputTranslationCache = false;
            }
        }
        private void SpeakFromStr(object sender, MouseButtonEventArgs e)
        {
            SpeechHelper.TryPlaySound(FromStr.Text);
        }

        private void SpeakToStr(object sender, MouseButtonEventArgs e)
        {
            SpeechHelper.TryPlaySound(ToStr.Text);
        }


        private void GridSplitter_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            DeFine.GlobalLocalSetting.WritingAreaHeight = WritingArea.Height.Value;
        }

        public void EmptyFromAndToText()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                CurrentKeyBox.Visibility = Visibility.Collapsed;
                FromStr.Text = string.Empty;
                ToStr.Text = string.Empty;
            }));
            LastSetKey = string.Empty;
        }

        public void AutoApplyStr()
        {
            if (DeFine.GlobalLocalSetting.ViewMode == "Normal")
            {
                if (DeFine.GlobalLocalSetting.AutoApply && CanAutoApply)
                {
                    if (LastSetKey.Trim().Length > 0)
                    {
                        ApplyTranslatedText();
                    }

                    CanAutoApply = false;
                }
            }
        }

        public string LastSetKey = "";
        public void SetSelectFromAndToText(string Key)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                AutoApplyStr();
            }));

            EmptyFromAndToText();

            LastSetKey = Key;

            SetLog("Key:" + LastSetKey);

            if (Key.Length > 0)
            {
                CurrentKeyBox.Visibility = Visibility.Visible;
            }

            if (DeFine.GlobalLocalSetting.ViewMode == "Normal")
            {
                if (TransViewList != null)
                {
                    var GridHandle = TransViewList.KeyToFakeGrid(Key);

                    if (GridHandle != null)
                    {
                        if (GridHandle.Score < 0)
                        {
                            LastSetKey = string.Empty;
                            return;
                        }

                        if (GridHandle.Score < 5)
                        {
                            ToStr.Foreground = new SolidColorBrush(Colors.Red);
                        }
                        else
                        {
                            ToStr.Foreground = new SolidColorBrush(Colors.White);
                        }

                        bool IsCloud = false;
                        GridHandle.SyncData(ref IsCloud);

                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            FromStr.Text = TransViewList.RealLines[TransViewList.SelectLineID].SourceText;
                            ToStr.Text = TransViewList.RealLines[TransViewList.SelectLineID].TransText;

                            UIHelper.ShowButton(ApplyOTButton, true);

                            if (FromStr.Text.Length > 0)
                            {
                                UIHelper.ShowButton(CancelOTButton, true);
                                ShowFormatToStrButton(true);
                            }

                            if (DeFine.GlobalLocalSetting.AutoSpeak)
                            {
                                SpeechHelper.TryPlaySound(FromStr.Text, true);
                            }

                            Point MousePos = Mouse.GetPosition(ToStr);
                            if (MousePos.X >= 0 && MousePos.X <= ToStr.ActualWidth &&
                                MousePos.Y >= 0 && MousePos.Y <= ToStr.ActualHeight)
                            {
                                ToStr.Focus();
                            }

                            AutoLoadHistoryList();
                        }));
                    }
                }
            }
        }

        private bool MainNavIsExpanded = false;

        private void ShowNav(object sender, MouseButtonEventArgs e)
        {
            if (!MainNavIsExpanded)
            {
                ShowMenu(true);
            }
            else
            {
                ShowMenu(false);
            }
        }

        private void ShowLocalEngineSettingView(object sender, MouseButtonEventArgs e)
        {
            DeFine.LocalConfigView.Owner = this;
            DeFine.LocalConfigView.Show();
            if (DeFine.GlobalLocalSetting.SourceLanguage == Languages.Auto)
            {
                LanguageDetect OneDetect = new LanguageDetect();

                for (int i = 0; i < TransViewList.RealLines.Count; i++)
                {
                    LanguageHelper.DetectLanguage(ref OneDetect, TransViewList.RealLines[i].SourceText);
                    if (i > (TransViewList.RealLines.Count / 2))
                    {
                        break;
                    }
                }

                DeFine.LocalConfigView.SFrom.SelectedValue = OneDetect.GetMaxLang().ToString();

            }
        }

        private void ShowView(object sender, MouseButtonEventArgs e)
        {
            ShowView(ConvertHelper.ObjToStr(((Border)sender).Tag));
        }

        public void SetSelectedNav(string View)
        {
            for (int i = 0; i < MainNav.Children.Count; i++)
            {
                if (MainNav.Children[i] is Grid)
                {
                    Border CurrentNav = (Border)((Grid)MainNav.Children[i]).Children[0];
                    string GetName = ConvertHelper.ObjToStr(CurrentNav.Tag);
                    if (View.Equals(GetName))
                    {
                        CurrentNav.Style = (Style)this.FindResource("MenuBlockSelected");
                    }
                    else
                    {
                        CurrentNav.Style = (Style)this.FindResource("MenuBlockStyle");
                    }
                }
            }
        }

        private void StartXTGlowLoop()
        {
            XTGlowLoopStoryboard = (Storyboard)FindResource("XTGlowLoop");
            XTGlowLoopStoryboard.Begin();
        }

        private void StopXTGlowLoop()
        {
            XTGlowLoopStoryboard?.Stop();
        }
        public void ShowView(string View)
        {
            DeFine.CanUpdateChart = false;

            SetSelectedNav(View);

            if (View == "About")
            {
                StartXTGlowLoop();
            }
            else
            {
                StopXTGlowLoop();
            }

            switch (View)
            {
                case "TransHub":
                    {
                        ModTransView.Visibility = Visibility.Visible;
                        AboutView.Visibility = Visibility.Collapsed;
                        SettingView.Visibility = Visibility.Collapsed;
                        DashBoardView.Visibility = Visibility.Collapsed;
                    }
                    break;
                case "DashBoard":
                    {
                        ModTransView.Visibility = Visibility.Collapsed;
                        AboutView.Visibility = Visibility.Collapsed;
                        SettingView.Visibility = Visibility.Collapsed;
                        DashBoardView.Visibility = Visibility.Visible;
                        DeFine.CanUpdateChart = true;
                        UPDateChart();
                    }
                    break;
                case "Settings":
                    {
                        ModTransView.Visibility = Visibility.Collapsed;
                        AboutView.Visibility = Visibility.Collapsed;
                        SettingView.Visibility = Visibility.Visible;
                        DashBoardView.Visibility = Visibility.Collapsed;
                    }
                    break;
                case "About":
                    {
                        AboutView.Visibility = Visibility.Visible;
                        ModTransView.Visibility = Visibility.Collapsed;
                        SettingView.Visibility = Visibility.Collapsed;
                        DashBoardView.Visibility = Visibility.Collapsed;

                        ProgramVersion.Content = string.Format("SSE Lexicon(GPLv3) Version: {0}", DeFine.CurrentVersion);
                        TranslationEngineVersion.Content = string.Format("Translation Engine(LGPLv3) Version: {0}", Engine.Version);
                    }
                    break;
            }
        }

        public void SyncTransStateUI()
        {
            TStop.Opacity = 0.5;

            if (TranslatorExtend.TranslationStatus == StateControl.Run)
            {
                TRun.Visibility = Visibility.Collapsed;
                TStop.Visibility = Visibility.Visible;
                TCancel.Visibility = Visibility.Visible;

                ThreadInFo.Visibility = Visibility.Visible;
            }
            else
            if (TranslatorExtend.TranslationStatus == StateControl.Stop)
            {
                TStop.Opacity = 1;
                TRun.Visibility = Visibility.Collapsed;
                TStop.Visibility = Visibility.Visible;
                TCancel.Visibility = Visibility.Visible;

                ThreadInFo.Visibility = Visibility.Visible;
            }
            else
            if (TranslatorExtend.TranslationStatus == StateControl.Cancel || TranslatorExtend.TranslationStatus == StateControl.Null)
            {
                TRun.Visibility = Visibility.Visible;
                TStop.Visibility = Visibility.Collapsed;
                TCancel.Visibility = Visibility.Collapsed;

                ThreadInFo.Visibility = Visibility.Collapsed;
            }
        }

        private void ChangeTransState(object sender, MouseButtonEventArgs e)
        {
            bool IsKeep = false;
            bool CallSucess = false;
            if (TransViewList != null)
            {
                if (TransViewList.Rows > 0)
                {
                    if (sender is Border)
                    {
                        Border ButtonHandle = (Border)sender;

                        string GetButtonName = ButtonHandle.Name;

                        switch (GetButtonName)
                        {
                            case "TRun":
                                {
                                    if (ConvertHelper.ObjToStr(TransProcess.Content).StartsWith("STRINGS("))
                                    {
                                        TRun.Visibility = Visibility.Collapsed;

                                        TranslatorExtend.TranslationStatus = StateControl.Run;
                                        CallSucess = true;
                                        IsKeep = false;
                                    }
                                }
                                break;
                            case "TStop":
                                {
                                    if (TStop.Opacity == 0.5)
                                    {
                                        TranslatorExtend.TranslationStatus = StateControl.Stop;
                                        CallSucess = true;
                                    }
                                    else
                                    {
                                        if (TranslatorExtend.TranslationStatus == StateControl.Stop)
                                        {
                                            IsKeep = true;
                                        }

                                        TranslatorExtend.TranslationStatus = StateControl.Run;
                                        CallSucess = true;
                                    }
                                }
                                break;
                            case "TCancel":
                                {
                                    TranslatorExtend.TranslationStatus = StateControl.Cancel;
                                    CallSucess = true;
                                }
                                break;
                        }

                        if (CallSucess)
                        {
                            TranslatorExtend.SyncTransState(new Action(() =>
                            {
                                this.Dispatcher.Invoke(new Action(() =>
                                {
                                    SyncTransStateUI();
                                }));
                            }), IsKeep);
                        }
                    }
                }
            }
            if (!CallSucess)
            {
                MessageBoxExtend.Show(this, "Batch translation is not possible at the current state.\nPlease wait until the file loading is finished.");
            }
        }

        private void ChangeColor(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border)
            {
                Border ButtonHandle = (Border)sender;
                Color GetColor = ((SolidColorBrush)ButtonHandle.Background).Color;

                if (TransViewList != null)
                {
                    TransViewList.ChangeFontColor(Engine.GetModName(), GetColor.R, GetColor.G, GetColor.B);
                }
                if (SearchResultsViewList != null)
                {
                    if (SearchResultsView.Visibility == Visibility.Visible)
                    {
                        SearchResultsViewList.ChangeFontColor(Engine.GetModName(), GetColor.R, GetColor.G, GetColor.B);
                    }
                }
            }
        }

        private void CancelTranslatedText(object sender, MouseButtonEventArgs e)
        {
            EmptyFromAndToText();

            UIHelper.ShowButton(CancelOTButton, false);
            UIHelper.ShowButton(ApplyOTButton, false);

            ShowFormatToStrButton(false);
            ShowClearToStrButton(false);
        }

        public void ApplyTranslatedText()
        {
            if (TransViewList != null)
            {
                if (LastSetKey.Trim().Length > 0)
                {
                    var GetGrid = TransViewList.KeyToFakeGrid(LastSetKey);

                    if (GetGrid != null)
                    {
                        GetGrid.TransText = ToStr.Text;

                        try
                        {
                            TranslatorBridge.SetTransData(GetGrid.Key, GetGrid.SourceText, GetGrid.TransText);
                        }
                        catch { }

                        TranslatorExtend.SetTranslatorHistoryCache(GetGrid.Key, GetGrid.TransText, false);

                        GetGrid.SyncUI(TransViewList);

                        if (SearchResultsViewList != null)
                        {
                            if (SearchResultsView.Visibility == Visibility.Visible)
                            {
                                GetGrid.SyncUI(SearchResultsViewList);
                            }
                        }
                    }

                    UIHelper.ShowButton(ApplyOTButton, false);
                }
            }
        }
        private void ApplyTranslatedText(object sender, MouseButtonEventArgs e)
        {
            ApplyTranslatedText();
        }

        public void SyncNodeStates()
        {
            foreach (var GetNode in Nodes.Children)
            {
                if (GetNode is Grid)
                {
                    Grid GetNodeGridHandle = (Grid)GetNode;
                    string GetNodeName = ConvertHelper.ObjToStr(GetNodeGridHandle.Tag);

                    if (GetNodeGridHandle.Children.Count >= 2)
                    {
                        if (GetNodeGridHandle.Children[1] is StackPanel)
                        {
                            StackPanel GetStackPanel = (StackPanel)GetNodeGridHandle.Children[1];

                            if (GetStackPanel.Children[0] is Grid)
                            {
                                Grid GetStateGrid = (Grid)GetStackPanel.Children[0];

                                Style NodeEnable = new Style(typeof(Grid))
                                {
                                    BasedOn = (Style)Application.Current.FindResource("NodeEnable")
                                };

                                Style NodeDisable = new Style(typeof(Grid))
                                {
                                    BasedOn = (Style)Application.Current.FindResource("NodeDisable")
                                };

                                switch (GetNodeName)
                                {
                                    case "PreTranslate":
                                        {
                                            if (EngineConfig.PreTranslateEnable)
                                            {
                                                GetStateGrid.Style = NodeEnable;
                                            }
                                            else
                                            {
                                                GetStateGrid.Style = NodeDisable;
                                            }
                                        }
                                        break;
                                    case "Gemini":
                                        {
                                            if (EngineConfig.GeminiApiEnable)
                                            {
                                                GetStateGrid.Style = NodeEnable;
                                            }
                                            else
                                            {
                                                GetStateGrid.Style = NodeDisable;
                                            }
                                        }
                                        break;
                                    case "ChatGpt":
                                        {
                                            if (EngineConfig.ChatGptApiEnable)
                                            {
                                                GetStateGrid.Style = NodeEnable;
                                            }
                                            else
                                            {
                                                GetStateGrid.Style = NodeDisable;
                                            }
                                        }
                                        break;
                                    case "Cohere":
                                        {
                                            if (EngineConfig.CohereApiEnable)
                                            {
                                                GetStateGrid.Style = NodeEnable;
                                            }
                                            else
                                            {
                                                GetStateGrid.Style = NodeDisable;
                                            }
                                        }
                                        break;
                                    case "DeepSeek":
                                        {
                                            if (EngineConfig.DeepSeekApiEnable)
                                            {
                                                GetStateGrid.Style = NodeEnable;
                                            }
                                            else
                                            {
                                                GetStateGrid.Style = NodeDisable;
                                            }
                                        }
                                        break;
                                    case "Baichuan":
                                        {
                                            if (EngineConfig.BaichuanApiEnable)
                                            {
                                                GetStateGrid.Style = NodeEnable;
                                            }
                                            else
                                            {
                                                GetStateGrid.Style = NodeDisable;
                                            }
                                        }
                                        break;
                                    case "LMLocalAI":
                                        {
                                            if (EngineConfig.LMLocalAIEnable)
                                            {
                                                GetStateGrid.Style = NodeEnable;
                                            }
                                            else
                                            {
                                                GetStateGrid.Style = NodeDisable;
                                            }
                                        }
                                        break;
                                    case "DeepL":
                                        {
                                            if (EngineConfig.DeepLApiEnable)
                                            {
                                                GetStateGrid.Style = NodeEnable;
                                            }
                                            else
                                            {
                                                GetStateGrid.Style = NodeDisable;
                                            }
                                        }
                                        break;
                                    case "Google":
                                        {
                                            if (EngineConfig.GoogleYunApiEnable)
                                            {
                                                GetStateGrid.Style = NodeEnable;
                                            }
                                            else
                                            {
                                                GetStateGrid.Style = NodeDisable;
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void ChangeNode(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid)
            {
                Grid GetNodeGridHandle = (Grid)sender;
                string GetNodeName = ConvertHelper.ObjToStr(GetNodeGridHandle.Tag);

                if (GetNodeGridHandle.Children.Count >= 2)
                {
                    if (GetNodeGridHandle.Children[1] is StackPanel)
                    {
                        StackPanel GetStackPanel = (StackPanel)GetNodeGridHandle.Children[1];
                        Grid GetStateGrid = (Grid)GetStackPanel.Children[0];

                        Style NodeEnable = new Style(typeof(Grid))
                        {
                            BasedOn = (Style)Application.Current.FindResource("NodeEnable")
                        };

                        Style NodeDisable = new Style(typeof(Grid))
                        {
                            BasedOn = (Style)Application.Current.FindResource("NodeDisable")
                        };

                        switch (GetNodeName)
                        {
                            case "PreTranslate":
                                {
                                    if (!EngineConfig.PreTranslateEnable)
                                    {
                                        EngineConfig.PreTranslateEnable = true;
                                        GetStateGrid.Style = NodeEnable;
                                    }
                                    else
                                    {
                                        EngineConfig.PreTranslateEnable = false;
                                        GetStateGrid.Style = NodeDisable;
                                    }
                                }
                                break;
                            case "Gemini":
                                {
                                    if (!EngineConfig.GeminiApiEnable)
                                    {
                                        EngineConfig.GeminiApiEnable = true;
                                        GetStateGrid.Style = NodeEnable;
                                    }
                                    else
                                    {
                                        EngineConfig.GeminiApiEnable = false;
                                        GetStateGrid.Style = NodeDisable;
                                    }
                                }
                                break;
                            case "ChatGpt":
                                {
                                    if (!EngineConfig.ChatGptApiEnable)
                                    {
                                        EngineConfig.ChatGptApiEnable = true;
                                        GetStateGrid.Style = NodeEnable;
                                    }
                                    else
                                    {
                                        EngineConfig.ChatGptApiEnable = false;
                                        GetStateGrid.Style = NodeDisable;
                                    }
                                }
                                break;
                            case "Cohere":
                                {
                                    if (!EngineConfig.CohereApiEnable)
                                    {
                                        EngineConfig.CohereApiEnable = true;
                                        GetStateGrid.Style = NodeEnable;
                                    }
                                    else
                                    {
                                        EngineConfig.CohereApiEnable = false;
                                        GetStateGrid.Style = NodeDisable;
                                    }
                                }
                                break;
                            case "DeepSeek":
                                {
                                    if (!EngineConfig.DeepSeekApiEnable)
                                    {
                                        EngineConfig.DeepSeekApiEnable = true;
                                        GetStateGrid.Style = NodeEnable;
                                    }
                                    else
                                    {
                                        EngineConfig.DeepSeekApiEnable = false;
                                        GetStateGrid.Style = NodeDisable;
                                    }
                                }
                                break;
                            case "Baichuan":
                                {
                                    if (!EngineConfig.BaichuanApiEnable)
                                    {
                                        EngineConfig.BaichuanApiEnable = true;
                                        GetStateGrid.Style = NodeEnable;
                                    }
                                    else
                                    {
                                        EngineConfig.BaichuanApiEnable = false;
                                        GetStateGrid.Style = NodeDisable;
                                    }
                                }
                                break;
                            case "LMLocalAI":
                                {
                                    if (!EngineConfig.LMLocalAIEnable)
                                    {
                                        EngineConfig.LMLocalAIEnable = true;
                                        GetStateGrid.Style = NodeEnable;
                                    }
                                    else
                                    {
                                        EngineConfig.LMLocalAIEnable = false;
                                        GetStateGrid.Style = NodeDisable;
                                    }
                                }
                                break;
                            case "DeepL":
                                {
                                    if (!EngineConfig.DeepLApiEnable)
                                    {
                                        EngineConfig.DeepLApiEnable = true;
                                        GetStateGrid.Style = NodeEnable;
                                    }
                                    else
                                    {
                                        EngineConfig.DeepLApiEnable = false;
                                        GetStateGrid.Style = NodeDisable;
                                    }
                                }
                                break;
                            case "Google":
                                {
                                    if (!EngineConfig.GoogleYunApiEnable)
                                    {
                                        EngineConfig.GoogleYunApiEnable = true;
                                        GetStateGrid.Style = NodeEnable;
                                    }
                                    else
                                    {
                                        EngineConfig.GoogleYunApiEnable = false;
                                        GetStateGrid.Style = NodeDisable;
                                    }
                                }
                                break;
                        }
                        EngineConfig.Save();
                    }
                }
            }
        }

        private void ProcessBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ScanAnimator != null)
            {
                if (TranslatorExtend.TranslationCore != null)
                {
                    if (TranslatorExtend.TranslationCore.IsWork && !TranslatorExtend.TranslationCore.IsStop)
                    {
                        ScanAnimator.UpdateAnimationTarget();
                    }
                }
            }
        }

        private void UPSelecter_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            AutoApplyStr();
            TransViewList?.UP();
        }

        private void DownSelecter_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            AutoApplyStr();
            TransViewList?.Down();
        }

        private void ClearToStr(object sender, MouseButtonEventArgs e)
        {
            ToStr.Text = string.Empty;
            ShowFormatToStrButton(true);
        }

        private void CloneFromStr(object sender, MouseButtonEventArgs e)
        {
            if (ToStr.Text.Length == 0)
            {
                ToStr.Text = FromStr.Text;
            }
        }

        public void ShowClearToStrButton(bool Enable)
        {
            UIHelper.ShowButton(ClearToStrButton, Enable);
        }

        public void ShowFormatToStrButton(bool Enable)
        {
            UIHelper.ShowButton(FormatToStrButton, Enable);
        }

        private void ToStr_TextChanged(object sender, EventArgs e)
        {
            if (ToStr.Text.Length > 0)
            {
                ShowClearToStrButton(true);
                ShowFormatToStrButton(true);
                UIHelper.ShowButton(ApplyOTButton, true);
            }
            else
            {
                ShowClearToStrButton(false);
            }
        }

        private void AutoSpeak_Click(object sender, RoutedEventArgs e)
        {
            if (AutoSpeak.IsChecked == true)
            {
                DeFine.GlobalLocalSetting.AutoSpeak = true;
            }
            else
            {
                DeFine.GlobalLocalSetting.AutoSpeak = false;
            }
        }

        private void AutoApply_Click(object sender, RoutedEventArgs e)
        {
            if (AutoApply.IsChecked == true)
            {
                DeFine.GlobalLocalSetting.AutoApply = true;
            }
            else
            {
                DeFine.GlobalLocalSetting.AutoApply = false;
            }
        }

        private void FormatToStr(object sender, MouseButtonEventArgs e)
        {
            if (ToStr.Text.Length > 0)
            {
                ToStr.Text = Translator.FormatStr(ToStr.Text);
            }
            else
            {
                ToStr.Text = Translator.FormatStr(FromStr.Text);
            }

            ShowFormatToStrButton(false);
        }


        private void ReplaceStr(object sender, MouseButtonEventArgs e)
        {
            DeFine.CurrentReplaceView.Show();
        }

        public bool CanAutoApply = false;
        private void ToStr_MouseEnter(object sender, MouseEventArgs e)
        {
            CanAutoApply = true;
        }

        public void AutoSizeHistoryList()
        {
            if (HistoryLayer.Visibility == Visibility.Visible)
            {
                ChangeTimeCol.Width = 100;
                IsCloudCol.Width = 50;
                double Width = HistoryLayer.ActualWidth - 150;
                if (Width < 0) Width = 300;
                TranslatedCol.Width = Width;
            }
        }

        public void AutoLoadHistoryList()
        {
            if (HistoryLayer.Visibility == Visibility.Visible)
            {
                HistoryList.Items.Clear();

                var QueryHistorys = TranslatorExtend.GetTranslatorCache(LastSetKey);
                if (QueryHistorys != null)
                {
                    foreach (var Get in QueryHistorys)
                    {
                        HistoryList.Items.Add(new
                        {
                            ChangeTime = Get.ChangeTime,
                            IsCloud = Get.IsCloud,
                            Translated = Get.Translated
                        });
                    }
                }
            }
        }

        private void ShowHistorys(object sender, MouseButtonEventArgs e)
        {
            if (HistoryLayer.Visibility == Visibility.Collapsed)
            {
                HistoryLayer.Visibility = Visibility.Visible;

                AutoSizeHistoryList();
                HistoryButtonFont.Content = "History↑";

                AutoLoadHistoryList();
            }
            else
            {
                HistoryLayer.Visibility = Visibility.Collapsed;
                HistoryButtonFont.Content = "History↓";
            }
        }

        private void HistoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var GetItem in HistoryList.SelectedItems)
            {
                var GetCol = HistoryList.SelectedItem.GetType().GetProperty("Translated");
                if (GetCol != null)
                {
                    string Translated = ConvertHelper.ObjToStr(ConvertHelper.ObjToStr(GetCol.GetValue(GetItem, null)));
                    ToStr.Text = Translated;
                }
            }
        }

        private void OpenUrl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string GetUrl = "";
            string GetTag = "";

            if (sender is Label)
            {
                GetUrl = "";
                GetTag = ConvertHelper.ObjToStr(((Label)sender).Tag);

                if (GetTag.Length > 0)
                {
                    GetUrl = GetTag;
                }
                else
                {
                    GetUrl = ConvertHelper.ObjToStr(((Label)sender).Content);
                }
            }
            if (sender is Run)
            {
                GetUrl = "";
                GetTag = ConvertHelper.ObjToStr(((Run)sender).Tag);

                if (GetTag.Length > 0)
                {
                    GetUrl = GetTag;
                }
                else
                {
                    GetUrl = ConvertHelper.ObjToStr(((Run)sender).Text);
                }
            }

            if (GetUrl.Length > 0)
            {
                if (MessageBoxExtend.Show(this, "Prompt", "Do you want to open your default browser and visit\n " + GetUrl + "\n?", MsgAction.YesNo, MsgType.Info) > 0)
                {
                    ExplorerHelper.OpenUrl(GetUrl);
                }
            }
        }

        public void BookTransCallBack(string Key, string CurrentText)
        {
            if (Key.Equals(LastSetKey) && CurrentText.Length > 0)
            {
                ToStr.Dispatcher.Invoke(new Action(() =>
                {
                    ToStr.Text = CurrentText;
                }));
            }
        }

        public object TranslateLocker = new object();
        public Thread? TranslateTrd = null;

        private TextSegmentTranslator CurrentTextSegmentTranslator = null;
        public void TranslateCurrent()
        {
            ProxyCenter.UsingProxy();

            lock (TranslateLocker)
            {
                if (TransViewList != null)
                {
                    if (ConvertHelper.ObjToStr(TranslateOTButtonFont.Content).Equals("Translate(F1)"))
                    {
                        FakeGrid? QueryGrid = TransViewList.KeyToFakeGrid(LastSetKey);

                        if (QueryGrid != null && TranslateTrd == null)
                        {
                            bool IsCloud = false;
                            QueryGrid.SyncData(ref IsCloud);

                            TranslationUnit NewUnit = new TranslationUnit(Engine.GetModName(), QueryGrid.Key, QueryGrid.Type, QueryGrid.SourceText, QueryGrid.TransText, "", Engine.From, Engine.To,100);

                            bool CanSleep = false;

                            CanAutoApply = true;

                            if (!QueryGrid.Key.EndsWith("(BookText)"))
                            {
                                this.Dispatcher.Invoke(new Action(() =>
                                {
                                    TransView.IsHitTestVisible = false;
                                }));

                                TranslateTrd = new Thread(() =>
                                {
                                    this.Dispatcher.Invoke(new Action(() =>
                                    {
                                        TranslateOTButtonFont.Content = "Translating..";
                                    }));

                                    string GetTranslated = Translator.QuickTrans(
                                    NewUnit,
                                    ref CanSleep);

                                    this.Dispatcher.Invoke(new Action(() =>
                                    {
                                        TransView.IsHitTestVisible = true;
                                    }));

                                    this.Dispatcher.Invoke(new Action(() =>
                                    {
                                        TranslateOTButtonFont.Content = "Translate(F1)";
                                        ToStr.Text = GetTranslated;
                                    }));

                                    TranslateTrd = null;
                                });

                                TranslateTrd.Start();
                            }
                            else
                            {
                                this.Dispatcher.Invoke(new Action(() =>
                                {
                                    TransView.IsHitTestVisible = false;
                                }));

                                CurrentTextSegmentTranslator = new TextSegmentTranslator();

                                TranslateTrd = new Thread(() =>
                                {
                                    this.Dispatcher.Invoke(new Action(() =>
                                    {
                                        TranslateOTButtonFont.Content = "Translating(Click to Cancel)..";
                                    }));

                                    try
                                    {
                                        CurrentTextSegmentTranslator.TransBook(NewUnit);
                                    }
                                    catch { }

                                    this.Dispatcher.Invoke(new Action(() =>
                                    {
                                        TransView.IsHitTestVisible = true;
                                    }));

                                    this.Dispatcher.Invoke(new Action(() =>
                                    {
                                        TranslateOTButtonFont.Content = "Translate(F1)";
                                    }));

                                    TranslateTrd = null;
                                });
                                TranslateTrd.Start();
                            }
                        }
                    }
                    else
                    if (ConvertHelper.ObjToStr(TranslateOTButtonFont.Content).Equals("Translating(Click to Cancel).."))
                    {
                        if (CurrentTextSegmentTranslator != null)
                        {
                            CurrentTextSegmentTranslator.Cancel();
                        }

                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            TranslateOTButtonFont.Content = "Translate(F1)";
                        }));
                    }
                }
            }
        }
        private void TranslateOTButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TranslateCurrent();
        }

        public void UPDateFile(bool CanSetSource)
        {
            if (TransViewList != null)
            {
                for (int i = 0; i < TransViewList.Rows; i++)
                {
                    bool IsCloud = false;

                    TransViewList.RealLines[i].SyncData(ref IsCloud);

                    string GetKey = TransViewList.RealLines[i].Key;

                    string GetTransText = TransViewList.RealLines[i].TransText;

                    if (CanSetSource)
                    {
                        if (string.IsNullOrEmpty(GetTransText))
                        {
                            GetTransText = TransViewList.RealLines[i].SourceText;
                        }
                    }

                    if (Translator.TransData.ContainsKey(GetKey))
                    {
                        Translator.TransData[GetKey] = GetTransText;
                    }
                    else
                    {
                        Translator.TransData.Add(GetKey, GetTransText);
                    }
                }
            }
        }

        private void ImportRamCache_Click(object sender, RoutedEventArgs e)
        {
            var Dialog = new System.Windows.Forms.OpenFileDialog();
            Dialog.Title = "Please select a file";
            Dialog.Filter = "All files|*.*";
            Dialog.Multiselect = false;

            if (Dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string SelectedFile = Dialog.FileName;

                if (File.Exists(SelectedFile))
                {
                    if (TransViewList != null)
                    {
                        if (TransViewList.Rows > 0)
                        {
                            string GetRamCache = Encoding.UTF8.GetString(DataHelper.ReadFile(SelectedFile));
                            List<FakeGrid>? RealLines = JsonSerializer.Deserialize<List<FakeGrid>>(GetRamCache);

                            if (RealLines != null)
                            {
                                for (int i = 0; i < RealLines.Count; i++)
                                {
                                    if (RealLines[i].SourceText != RealLines[i].TransText)
                                    {
                                        TranslatorBridge.SetTransCache(RealLines[i].Key, RealLines[i].TransText);
                                    }
                                }

                                for (int i = 0; i < TransViewList.Rows; i++)
                                {
                                    bool IsCloud = false;
                                    TransViewList.RealLines[i].SyncData(ref IsCloud);
                                    TransViewList.RealLines[i].SyncUI(TransViewList);
                                }
                            }
                        }
                        else
                        {
                            LoadAny(SelectedFile);
                        }
                    }
                }
            }
        }

        private void ExportToRamCache_Click(object sender, RoutedEventArgs e)
        {
            if (TransViewList != null)
            {
                if (TransViewList.Rows > 0)
                {
                    var GetWritePath = DataHelper.ShowSaveFileDialog(LModName + ".Json", "RamCache (*.Json)|*.Json");

                    UPDateFile(false);

                    var JsonOptions = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    };

                    string GetJson = JsonSerializer.Serialize(TransViewList.RealLines, JsonOptions);

                    if (GetWritePath != null)
                    {
                        if (GetWritePath.Trim().Length > 0)
                        {
                            if (File.Exists(GetWritePath))
                            {
                                File.Delete(GetWritePath);
                            }
                            DataHelper.WriteFile(GetWritePath, Encoding.UTF8.GetBytes(GetJson));
                        }
                    }
                }
            }
        }
        private void ExportToDsd_Click(object sender, RoutedEventArgs e)
        {
            if (TransViewList != null)
            {
                if (CurrentTransType == 2 && TransViewList.Rows > 0)
                {
                    if (GlobalEspReader != null)
                    {
                        var GetWritePath = DataHelper.ShowSaveFileDialog(LModName + ".json", "DSD (*.json)|*.json");

                        UPDateFile(false);

                        var JsonOptions = new JsonSerializerOptions
                        {
                            WriteIndented = true,
                            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                        };

                        var GetData = SkyrimDataDSDConvert.EspExportAllByDSD(GlobalEspReader);
                        string GetJson = JsonSerializer.Serialize(GetData, JsonOptions);

                        if (GetWritePath != null)
                        {
                            if (GetWritePath.Trim().Length > 0)
                            {
                                if (File.Exists(GetWritePath))
                                {
                                    File.Delete(GetWritePath);
                                }
                                DataHelper.WriteFile(GetWritePath, Encoding.UTF8.GetBytes(GetJson));
                            }
                        }
                    }
                   
                }
                else
                {
                    MessageBoxExtend.Show(this, "The current file does not support exporting to DSD format.");
                }
            }
        }

        #region Setting

        public void SelectFristSettingNav()
        {
            if (SettingNavs.Children.Count > 0)
            {
                if (SettingNavs.Children[0] is Border)
                    SelectSettingNav((Border)SettingNavs.Children[0]);
            }
        }

        public Border LastSetBorder = null;
        public void SetSelectSettingNav(Border Nav)
        {
            if (Nav.Child is Grid)
            {
                Grid GetMainGrid = (Grid)Nav.Child;
                if (GetMainGrid.Children.Count == 2)
                {
                    Nav.Style = (Style)this.FindResource("ModelSelected");
                    ((Grid)GetMainGrid.Children[1]).Visibility = Visibility.Visible;
                }
            }

        }

        public void SetUnSelectSettingNav(Border Nav)
        {
            if (Nav.Child is Grid)
            {
                Grid GetMainGrid = (Grid)Nav.Child;
                if (GetMainGrid.Children.Count == 2)
                {
                    Nav.Style = (Style)this.FindResource("ModelUnSelected");
                    ((Grid)GetMainGrid.Children[1]).Visibility = Visibility.Hidden;
                }
            }

        }

        public string GetSettingNavName(Border Nav)
        {
            if (Nav.Child is Grid)
            {
                Grid GetMainGrid = (Grid)Nav.Child;
                if (GetMainGrid.Children.Count == 2)
                {
                    if (GetMainGrid.Children[0] is Label)
                        return ConvertHelper.ObjToStr(((Label)GetMainGrid.Children[0]).Content);
                }
            }
            return string.Empty;
        }

        public void SelectSettingNav(Border Nav)
        {
            if (LastSetBorder != null)
            {
                SetUnSelectSettingNav(LastSetBorder);
            }

            SetSelectSettingNav(Nav);

            string GetName = GetSettingNavName(Nav);
            ShowFrame(GetName);
            SyncSettingUI(GetName);

            LastSetBorder = Nav;
        }

        public void SyncSettingUI(string Name)
        {
            if (Name.Equals("Request And ApiKey Configs"))
            {
                STimeOut.Text = EngineConfig.GlobalRequestTimeOut.ToString();

                SProxyUrl.Text = EngineConfig.ProxyUrl;
                SProxyUserName.Text = EngineConfig.ProxyUserName;
                SProxyPassword.Password = EngineConfig.ProxyPassword;

                SGeminiKey.Password = EngineConfig.GeminiKey;
                SGeminiModel.Text = EngineConfig.GeminiModel;

                SGeminiModelSelect.Items.Clear();
                SGeminiModelSelect.Items.Add("gemini-2.5-flash");
                SGeminiModelSelect.Items.Add("gemini-2.0-flash");
                SGeminiModelSelect.SelectedValue = null;

                SChatGptKey.Password = EngineConfig.ChatGptKey;
                SChatGptModel.Text = EngineConfig.ChatGptModel;
                SChatGptModelSelect.Items.Clear();
                SChatGptModelSelect.Items.Add("gpt-5-nano");
                SChatGptModelSelect.Items.Add("gpt-5-mini");
                SChatGptModelSelect.Items.Add("gpt-4.1-nano");
                SChatGptModelSelect.Items.Add("gpt-4.1-mini");
                SChatGptModelSelect.Items.Add("gpt-4o-mini");
                SChatGptModelSelect.SelectedValue = null;

                SCohereKey.Password = EngineConfig.CohereKey;

                SDeepSeekKey.Password = EngineConfig.DeepSeekKey;
                SDeepSeekModel.Text = EngineConfig.DeepSeekModel;
                SDeepSeekModelSelect.Items.Clear();
                SDeepSeekModelSelect.Items.Add("deepseek-chat");
                SDeepSeekModelSelect.Items.Add("deepseek-reasoner");
                SDeepSeekModelSelect.SelectedValue = null;

                SBaichuanKey.Password = EngineConfig.BaichuanKey;
                SBaichuanModel.Text = EngineConfig.BaichuanModel;
                SBaichuanModelSelect.Items.Clear();
                SBaichuanModelSelect.Items.Add("Baichuan4-Turbo");
                SBaichuanModelSelect.SelectedValue = null;

                SLMHost.Text = EngineConfig.LMHost;
                SLMPort.Text = EngineConfig.LMPort.ToString();
                SLMModel.Text = EngineConfig.LMModel;

                SDeepLKey.Password = EngineConfig.DeepLKey;

                if (EngineConfig.IsFreeDeepL)
                {
                    IsFreeDeepL.IsChecked = true;
                }
                else
                {
                    IsFreeDeepL.IsChecked = false;
                }


                GoogleKey.Password = EngineConfig.GoogleApiKey;
            }
            else
            if (Name.Equals("AI Configs"))
            {
                SContextLimit.Text = EngineConfig.ContextLimit.ToString();

                if (EngineConfig.ContextEnable)
                {
                    SContextEnable.IsChecked = true;
                }
                else
                {
                    SContextEnable.IsChecked = false;
                }

                SAIKeyword.Text = EngineConfig.UserCustomAIPrompt;
            }
            else
            if (Name.Equals("Game Configs"))
            {
                SGame.Items.Clear();
                SGame.Items.Add(GameNames.SkyrimSE.ToString());
                SGame.Items.Add(GameNames.SkyrimLE.ToString());

                SGame.SelectedValue = DeFine.GlobalLocalSetting.GameType.ToString();

                SGameFileEncoding.Items.Clear();
                SGameFileEncoding.Items.Add(EncodingTypes.UTF8.ToString());
                SGameFileEncoding.Items.Add(EncodingTypes.UTF8_1250.ToString());
                SGameFileEncoding.Items.Add(EncodingTypes.UTF8_1252.ToString());
                SGameFileEncoding.Items.Add(EncodingTypes.UTF8_1253.ToString());
                SGameFileEncoding.Items.Add(EncodingTypes.UTF8_1256.ToString());

                SGameFileEncoding.SelectedValue = DeFine.GlobalLocalSetting.FileEncoding.ToString();
            }
            else
            if (Name.Equals("UI Configs"))
            {
                SUILanguages.Items.Clear();
                SUILanguages.Items.Add("English");

                SUILanguages.SelectedValue = SUILanguages.Items[0];

                if (DeFine.GlobalLocalSetting.ShowCode)
                {
                    ShowCodeView.IsChecked = true;
                }
                else
                {
                    ShowCodeView.IsChecked = false;
                }
            }
            else
            if (Name.Equals("Engine Configs"))
            {
                SThrottlingRatio.Text = EngineConfig.ThrottleRatio.ToString();

                SRotationDelay.Text = EngineConfig.ThrottleDelayMs.ToString();

                SMaxThread.Text = EngineConfig.MaxThreadCount.ToString();

                if (EngineConfig.AutoSetThreadLimit)
                {
                    SAutoSetThreadLimit.IsChecked = true;
                }
                else
                {
                    SAutoSetThreadLimit.IsChecked = false;
                }
            }
        }

        private void ShowCodeView_Click(object sender, RoutedEventArgs e)
        {
            if (ShowCodeView.IsChecked == true)
            {
                DeFine.GlobalLocalSetting.ShowCode = true;
            }
            else
            {
                DeFine.GlobalLocalSetting.ShowCode = false;
            }
        }

        public void ShowFrame(string Name)
        {
            for (int i = 0; i < this.SettingFrames.Children.Count; i++)
            {
                if (this.SettingFrames.Children[i] is Border)
                {
                    Border GetFrame = (Border)this.SettingFrames.Children[i];
                    string GetTag = ConvertHelper.ObjToStr(GetFrame.Tag);
                    if (GetTag.Equals(Name))
                    {
                        GetFrame.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        GetFrame.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void SelectSettingNav(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border)
            {
                Border GetNav = (Border)sender;

                SelectSettingNav(GetNav);
            }
        }

        private void STimeOut_TextChanged(object sender, TextChangedEventArgs e)
        {
            int GetTimeOut = ConvertHelper.ObjToInt(STimeOut.Text);

            if (GetTimeOut > 0)
            {
                EngineConfig.GlobalRequestTimeOut = GetTimeOut;
            }
        }
        private void SProxyUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            EngineConfig.ProxyUrl = SProxyUrl.Text;
        }
        private void SProxyUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            EngineConfig.ProxyUserName = SProxyUserName.Text;
        }
        private void SProxyPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            EngineConfig.ProxyPassword = SProxyPassword.Password;
        }

        private void SGeminiKey_PasswordChanged(object sender, RoutedEventArgs e)
        {
            EngineConfig.GeminiKey = SGeminiKey.Password;
        }
        private void SGeminiModel_TextChanged(object sender, TextChangedEventArgs e)
        {
            EngineConfig.GeminiModel = SGeminiModel.Text;
        }
        private void SGeminiModelSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string GetModel = ConvertHelper.ObjToStr(SGeminiModelSelect.SelectedValue);

            if (GetModel.Trim().Length > 0)
            {
                SGeminiModel.Text = GetModel;
            }
        }

        private void SChatGptKey_PasswordChanged(object sender, RoutedEventArgs e)
        {
            EngineConfig.ChatGptKey = SChatGptKey.Password;
        }
        private void SChatGptModel_TextChanged(object sender, TextChangedEventArgs e)
        {
            EngineConfig.ChatGptModel = SChatGptModel.Text;
        }
        private void SChatGptModelSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string GetModel = ConvertHelper.ObjToStr(SChatGptModelSelect.SelectedValue);

            if (GetModel.Trim().Length > 0)
            {
                SChatGptModel.Text = GetModel;
            }
        }

        private void SCohereKey_PasswordChanged(object sender, RoutedEventArgs e)
        {
            EngineConfig.CohereKey = SCohereKey.Password;
        }

        private void SDeepSeekKey_PasswordChanged(object sender, RoutedEventArgs e)
        {
            EngineConfig.DeepSeekKey = SDeepSeekKey.Password;
        }
        private void SDeepSeekModel_TextChanged(object sender, TextChangedEventArgs e)
        {
            EngineConfig.DeepSeekModel = SDeepSeekModel.Text;
        }
        private void SDeepSeekModelSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string GetModel = ConvertHelper.ObjToStr(SDeepSeekModelSelect.SelectedValue);

            if (GetModel.Trim().Length > 0)
            {
                SDeepSeekModel.Text = GetModel;
            }
        }

        private void SBaichuanKey_PasswordChanged(object sender, RoutedEventArgs e)
        {
            EngineConfig.BaichuanKey = SBaichuanKey.Password;
        }
        private void SBaichuanModel_TextChanged(object sender, TextChangedEventArgs e)
        {
            EngineConfig.BaichuanModel = SBaichuanModel.Text;
        }
        private void SBaichuanModelSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string GetModel = ConvertHelper.ObjToStr(SBaichuanModelSelect.SelectedValue);

            if (GetModel.Trim().Length > 0)
            {
                SBaichuanModel.Text = GetModel;
            }
        }

        private void SLMHost_TextChanged(object sender, TextChangedEventArgs e)
        {
            EngineConfig.LMHost = SLMHost.Text;
        }
        private void SLMPort_TextChanged(object sender, TextChangedEventArgs e)
        {
            EngineConfig.LMPort = ConvertHelper.ObjToInt(SLMPort.Text);
        }
        private void SLMModel_TextChanged(object sender, TextChangedEventArgs e)
        {
            EngineConfig.LMModel = SLMModel.Text;
        }
        private void SLMHost_MouseLeave(object sender, MouseEventArgs e)
        {
            //Auto Format
            string Url = SLMHost.Text;

            Uri? Uri = null;

            if (Uri.TryCreate(Url, UriKind.Absolute, out Uri))
            {
                if (Uri != null)
                {
                    string HostWithScheme = Uri.Scheme + "://" + Uri.Host;
                    int Port = Uri.Port;

                    if (Port > 0 && Port != 80)
                    {
                        EngineConfig.LMHost = HostWithScheme;
                        EngineConfig.LMPort = Port;

                        SLMHost.Text = EngineConfig.LMHost;
                        SLMPort.Text = EngineConfig.LMPort.ToString();
                    }
                }
            }
        }

        private void SDeepLKey_PasswordChanged(object sender, RoutedEventArgs e)
        {
            EngineConfig.DeepLKey = SDeepLKey.Password;
        }

        private void IsFreeDeepL_Click(object sender, RoutedEventArgs e)
        {
            if (IsFreeDeepL.IsChecked == true)
            {
                EngineConfig.IsFreeDeepL = true;
            }
            else
            {
                EngineConfig.IsFreeDeepL = false;
            }
        }

        private void GoogleKey_PasswordChanged(object sender, RoutedEventArgs e)
        {
            EngineConfig.GoogleApiKey = GoogleKey.Password;
        }

        private void SContextLimit_TextChanged(object sender, TextChangedEventArgs e)
        {
            EngineConfig.ContextLimit = ConvertHelper.ObjToInt(SContextLimit.Text);
        }

        private void SAIKeyword_TextChanged(object sender, TextChangedEventArgs e)
        {
            EngineConfig.UserCustomAIPrompt = SAIKeyword.Text.Trim();
        }

        private void SContextEnable_Click(object sender, RoutedEventArgs e)
        {
            if (SContextEnable.IsChecked == true)
            {
                EngineConfig.ContextEnable = true;
                ContextGeneration.IsChecked = true;
                RightContextIndicator.Visibility = Visibility.Visible;
            }
            else
            {
                EngineConfig.ContextEnable = false;
                ContextGeneration.IsChecked = false;
                RightContextIndicator.Visibility = Visibility.Collapsed;
            }
        }

        private void SGame_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string GetName = ConvertHelper.ObjToStr(SGame.SelectedValue);
            if (GetName.Trim().Length > 0)
            {
                DeFine.GlobalLocalSetting.GameType = Enum.Parse<GameNames>(GetName);
            }
        }

        private void SGameFileEncoding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string GetFileEncoding = ConvertHelper.ObjToStr(SGameFileEncoding.SelectedValue);
            if (GetFileEncoding.Trim().Length > 0)
            {
                DeFine.GlobalLocalSetting.FileEncoding = Enum.Parse<EncodingTypes>(GetFileEncoding);
            }
        }

        private void SThrottlingRatio_TextChanged(object sender, TextChangedEventArgs e)
        {
            EngineConfig.ThrottleRatio = ConvertHelper.ObjToDouble(SThrottlingRatio.Text);
        }

        private void SRotationDelay_TextChanged(object sender, TextChangedEventArgs e)
        {
            EngineConfig.ThrottleDelayMs = ConvertHelper.ObjToInt(SRotationDelay.Text);
        }

        private void SMaxThread_TextChanged(object sender, TextChangedEventArgs e)
        {
            EngineConfig.MaxThreadCount = ConvertHelper.ObjToInt(SMaxThread.Text);
        }

        private void SAutoSetThreadLimit_Click(object sender, RoutedEventArgs e)
        {
            if (SAutoSetThreadLimit.IsChecked == true)
            {
                EngineConfig.AutoSetThreadLimit = true;
            }
            else
            {
                EngineConfig.AutoSetThreadLimit = false;
            }
        }

        #endregion

        #region LogView

        private Border? LastSetLogButton = null;
        private void SelectLogNav(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border)
            {
                Border GetBorderHandle = (Border)sender;

                if (GetBorderHandle.Child is Label)
                {
                    if (LastSetLogButton != null)
                    {
                        LastSetLogButton.Style = (Style)this.FindResource("LogViewButtonUnSelected");
                    }

                    string GetContent = ConvertHelper.ObjToStr(((Label)GetBorderHandle.Child).Content);

                    if (GetContent == "InputLog")
                    {
                        InputLog.Visibility = Visibility.Visible;
                        OutputLog.Visibility = Visibility.Collapsed;
                        MainLog.Visibility = Visibility.Collapsed;
                    }
                    else
                    if (GetContent == "OutputLog")
                    {
                        InputLog.Visibility = Visibility.Collapsed;
                        OutputLog.Visibility = Visibility.Visible;
                        MainLog.Visibility = Visibility.Collapsed;
                    }
                    else
                    if (GetContent == "Log")
                    {
                        InputLog.Visibility = Visibility.Collapsed;
                        OutputLog.Visibility = Visibility.Collapsed;
                        MainLog.Visibility = Visibility.Visible;
                    }

                    GetBorderHandle.Style = (Style)this.FindResource("LogViewButtonSelected");

                    LastSetLogButton = GetBorderHandle;
                }

            }
        }
        #endregion

        #region DashBoardView
        public class DashBoardViewModel : INotifyPropertyChanged
        {
            public ChartValues<double>? _SetValues;
            public ChartValues<double> SetValues
            {
                get => _SetValues;
                set
                {
                    if (_SetValues != value)
                    {
                        _SetValues = value;
                        OnPropertyChanged(nameof(SetValues));
                    }
                }
            }

            private SeriesCollection? _FontUsageSeries;
            public SeriesCollection FontUsageSeries
            {
                get => _FontUsageSeries;
                set
                {
                    if (_FontUsageSeries != value)
                    {
                        _FontUsageSeries = value;
                        OnPropertyChanged(nameof(FontUsageSeries));
                    }
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            public Func<double, string> Formatter { get; set; } = value => ((int)value).ToString();
            protected void OnPropertyChanged(string propertyName)
                => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DashBoardViewModel CurrentModel = new DashBoardViewModel();

        public void InitDashBoard()
        {
            CurrentModel.SetValues = new ChartValues<double>() { 0, 0, 0, 0, 0, 0 };

            CurrentModel.FontUsageSeries = new SeriesCollection
            {
                new LineSeries
                {
                    AreaLimit = -10,
                    Values = new ChartValues<ObservableValue>
                    {
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0),
                        new ObservableValue(0)
                    }
                }
            };
        }

        public SpeedMonitor CurrentMonitor = null;
        //ChatGPT,Gemini,Cohere,DeepSeek,Baichuan,LocalAI
        public void UPDateChart(string SendStr = "")
        {
            if (CurrentMonitor == null)
            {
                CurrentMonitor = new SpeedMonitor(CurrentModel);
            }

            if (SendStr.Length > 0)
            {
                CurrentMonitor.AddCount(SendStr.Length);
            }

            UsageCount.Content = $"Average Speed (last 30s): {CurrentMonitor.AverageSpeed:F1}";

            CurrentModel.SetValues[0] = DeFine.GlobalLocalSetting.ChatGPTTokenUsage;
            CurrentModel.SetValues[1] = DeFine.GlobalLocalSetting.GeminiTokenUsage;
            CurrentModel.SetValues[2] = DeFine.GlobalLocalSetting.CohereTokenUsage;
            CurrentModel.SetValues[3] = DeFine.GlobalLocalSetting.DeepSeekTokenUsage;
            CurrentModel.SetValues[4] = DeFine.GlobalLocalSetting.BaichuanTokenUsage;
            CurrentModel.SetValues[5] = DeFine.GlobalLocalSetting.LocalAITokenUsage;
        }

        private void ReSetToken_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DeFine.GlobalLocalSetting.ChatGPTTokenUsage = 0;
            DeFine.GlobalLocalSetting.GeminiTokenUsage = 0;
            DeFine.GlobalLocalSetting.CohereTokenUsage = 0;
            DeFine.GlobalLocalSetting.DeepSeekTokenUsage = 0;
            DeFine.GlobalLocalSetting.BaichuanTokenUsage = 0;
            DeFine.GlobalLocalSetting.LocalAITokenUsage = 0;

            UPDateChart();

            DeFine.GlobalLocalSetting.SaveConfig();
        }


        #endregion
    }
}
