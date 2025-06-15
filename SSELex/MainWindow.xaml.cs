using SharpVectors.Converters;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using SSELex.ConvertManager;
using SSELex.SkyrimManage;
using SSELex.SkyrimModManager;
using SSELex.TranslateCore;
using SSELex.TranslateManage;
using SSELex.UIManage;
using static SSELex.UIManage.SkyrimDataLoader;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Highlighting;
using SSELex.SkyrimManagement;
using System.Text.Json;
using System.Text;
using SSELex.PlatformManagement;
using SSELex.RequestManagement;
using SSELex.TranslateManagement;
using SSELex.UIManagement;
using static SSELex.SkyrimManage.EspReader;
using System.Timers;
using System.Xml.Schema;
using SSELex.PlatformManagement.LocalAI;

// Copyright (C) 2025 YD525
// Licensed under the GNU GPLv3
// See LICENSE for details
//https://github.com/YD525/YDSkyrimToolR/

namespace SSELex
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void SetLog(string Str)
        {
            DeFine.CurrentDashBoardView.SetLogA(Str);
        }

        public void EndLoadViewEffect()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                LoadingEffect.RepeatBehavior = new RepeatBehavior();
                LoadingEffect.Begin(this);
                LoadingEffect.Stop(this);
            }));
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
        public MCMReader GlobalMCMReader = null;
        public EspReader GlobalEspReader = null;
        public PexReader GlobalPexReader = null;

        public YDListView TransViewList = null;

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

        private System.Timers.Timer ReloadDebounceTimer;
        private readonly object ReloadLock = new object();
        private bool UseHotReloadFlag;
        public void ReloadData(bool UseHotReload = false)
        {
            if (LoadSaveState != 1)
            return;
            lock (ReloadLock)
            {
                UseHotReloadFlag = UseHotReload;

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


        private System.Timers.Timer SearchTimer;
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

        private System.Timers.Timer DebounceTimer;
        public void GetStatisticsR()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                CalcStatistics();
            }));
        }

        public int EffectStart = 0;

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
                        var Storyboard = (Storyboard)ProcessBar.Resources["BarEffect"];
                        double GetRate = ((double)UIHelper.ModifyCount / (double)TransViewList.Rows);
                        if (GetRate > 0)
                        {
                            try
                            {
                                ProcessBar.Width = ProcessBarFrame.ActualWidth * GetRate;
                            }
                            catch { }

                            try
                            {
                                if (Storyboard != null)
                                {
                                    if (EffectStart == 0)
                                    {
                                        EffectStart = 1;
                                        Storyboard.Begin();
                                    }
                                }
                            }
                            catch { }
                        }
                        else
                        {
                            try
                            {
                                ProcessBar.Width = 1;

                                if (Storyboard != null)
                                {
                                    Storyboard.Stop();
                                    EffectStart = 0;
                                }
                            }
                            catch { }
                        }
                        MaxTransCount = TransViewList.Rows;

                        if (ReadTrdWorkState)
                        {
                            TransProcess.Content = string.Format("Loading({0}/{1})", UIHelper.ModifyCount, MaxTransCount);
                        }
                        else
                        {
                            TransProcess.Content = string.Format("STRINGS({0}/{1})", UIHelper.ModifyCount, MaxTransCount);
                        }
                    }
                }));
            }
            catch { }
           
        }

        public void ReloadViewMode()
        {
            ViewMode.Items.Clear();
            ViewMode.Items.Add("Quick");
            ViewMode.Items.Add("Normal");


            ViewMode.SelectedValue = DeFine.GlobalLocalSetting.ViewMode;
        }

        public void ReloadLanguageMode()
        {
            ToL.Items.Clear();
            foreach (var Get in UILanguageHelper.SupportLanguages)
            {
                ToL.Items.Add(Get.ToString());
            }

            ToL.SelectedValue = DeFine.TargetLanguage.ToString();

            FromL.Items.Clear();
            foreach (var Get in UILanguageHelper.SupportLanguages)
            {
                FromL.Items.Add(Get.ToString());
            }

            FromL.SelectedValue = DeFine.SourceLanguage.ToString();
        }

        public bool CheckINeed()
        {
            bool State = true;
            //Frist Check ToolPath
            if (!File.Exists(DeFine.GetFullPath(@"Tool\Champollion.exe")))
            {
                string Msg = "Please manually install the dependent program\n[https://github.com/Orvid/Champollion]\nPlease download the release version and put it in this path\n[" + DeFine.GetFullPath(@"Tool\") + "]\n Path required\n[" + DeFine.GetFullPath(@"Tool\Champollion.exe") + "]";
                ActionWin.Show("HelpMsg", Msg, MsgAction.Yes, MsgType.Info, 500);

                if (ActionWin.Show("HelpMsg", "Do you want to download the Champollion component now?\nIf you need.Click Yes to jump to the URL", MsgAction.YesNo, MsgType.Info, 230) > 0)
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
                ActionWin.Show("PEX File lacks support", Msg, MsgAction.Yes, MsgType.Info, 300);
                this.Dispatcher.Invoke(new Action(() =>
                {
                    ShowSettingsView(Settings.Game);
                }));
                State = false;
            }
            return State;
        }
        public void TranslateMsg(string EngineName, string Text, string Result)
        {
            SetLog(string.Format("{0}->{1},{2}", EngineName, Text, Result));
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

        public void InitEncodings()
        {
            EspEncodings.Items.Clear();
            EspEncodings.Items.Add(EncodingTypes.UTF8_1256.ToString());
            EspEncodings.Items.Add(EncodingTypes.UTF8_1253.ToString());
            EspEncodings.Items.Add(EncodingTypes.UTF8_1252.ToString());
            EspEncodings.Items.Add(EncodingTypes.UTF8_1250.ToString());
            EspEncodings.Items.Add(EncodingTypes.UTF8.ToString());

            EspEncodings.SelectedValue = DeFine.GlobalLocalSetting.FileEncoding.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DeFine.Init(this);
            try
            {
                HotPatch.Apply();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to apply patch: {ex.Message}");
            }
            this.Width = DeFine.GlobalLocalSetting.FormWidth;
            this.Height = DeFine.GlobalLocalSetting.FormHeight;

            ReloadViewMode();
            ReloadLanguageMode();
            InitEncodings();
            this.MainCaption.Content = string.Format("SSELex {0}", DeFine.CurrentVersion);

            ReplaceMode.Items.Clear();
            ReplaceMode.Items.Add("Current");
            ReplaceMode.Items.Add("ALL");

            if (ReplaceMode.Items.Count > 0)
            {
                ReplaceMode.SelectedValue = ReplaceMode.Items[0];
            }

            Translator.SendTranslateMsg += TranslateMsg;

            if (DeFine.GlobalLocalSetting.AutoLoadDictionaryFile)
            {
                UsingDictionary.IsChecked = true;
            }

            if (DeFine.GlobalLocalSetting.UsingContext)
            {
                UsingContext.IsChecked = true;
            }

            ExportTypes.Items.Clear();
            ExportTypes.Items.Add("Json(Dynamic String Distributor)");
            ExportTypes.Items.Add("Json(SSELex)");
            ExportTypes.Items.Add("Html");

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

            NodePanel.Visibility = Visibility.Hidden;

            new Thread(() =>
            {
                ShowFrameByTag("LoadingView");

                DeFine.LoadData();
                AdvancedDictionary.Init();
                //LocalTrans.Init();

                CheckEngineState();

                GlobalEspReader = new EspReader();
                GlobalMCMReader = new MCMReader();
                GlobalPexReader = new PexReader();

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    UILanguageHelper.ChangeLanguage(DeFine.GlobalLocalSetting.CurrentUILanguage);
                }));

                InitIDE();

                EndLoadViewEffect();
                ShowFrameByTag("MainView");

                this.Dispatcher.Invoke(new Action(() =>
                {
                    this.Topmost = false;
                    Storyboard GetStoryboard = this.Resources["HideSmallNav"] as Storyboard;
                    if (GetStoryboard != null)
                    {
                        GetStoryboard.Begin();
                    }

                    if (TransViewList == null)
                    {
                        TransViewList = new YDListView(TransView);
                        TransViewList.Clear();
                    }
                }));

                if (FileAttributesHelper.IsAdministrator())
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        ShowSettingsView(Settings.Game);
                    }));
                }

                SetLog(string.Empty);

                ProxyCenter.GlobalProxyIP = DeFine.GlobalLocalSetting.ProxyIP;

                this.Dispatcher.Invoke(new Action(() =>
                {
                    this.GeminiModel.Items.Clear();
                    this.GeminiModel.Items.Add("gemini-2.0-flash");


                    this.ChatGptModel.Items.Clear();

                    this.ChatGptModel.Items.Add("gpt-3.5-turbo-0613");
                    this.ChatGptModel.Items.Add("gpt-3.5-turbo");
                    this.ChatGptModel.Items.Add("gpt-3.5-turbo-1106");
                    this.ChatGptModel.Items.Add("gpt-4o");
                    this.ChatGptModel.Items.Add("gpt-4o-mini");
                    this.ChatGptModel.Items.Add("gpt-4-turbo");

                    if (DeFine.GlobalLocalSetting.ChatGptModel.Trim().Length > 0)
                    {
                        this.ChatGptModel.SelectedValue = DeFine.GlobalLocalSetting.ChatGptModel;
                    }
                    else
                    {
                        this.ChatGptModel.SelectedValue = "gpt-4o-mini";
                    }

                    this.DeepSeekModel.Items.Clear();
                    this.DeepSeekModel.Items.Add("deepseek-chat");
                    if (DeFine.GlobalLocalSetting.DeepSeekModel.Trim().Length > 0)
                    {
                        this.DeepSeekModel.SelectedValue = DeFine.GlobalLocalSetting.DeepSeekModel;
                    }
                    else
                    {
                        this.DeepSeekModel.SelectedValue = "deepseek-chat";
                    }

                    this.BaichuanModel.Items.Clear();
                    this.BaichuanModel.Items.Add("Baichuan4-Turbo");
                    this.BaichuanModel.Items.Add("Baichuan4-Air");
                    this.BaichuanModel.Items.Add("Baichuan4");
                    this.BaichuanModel.Items.Add("Baichuan3-Turbo");
                    this.BaichuanModel.Items.Add("Baichuan3-Turbo-128k");
                    this.BaichuanModel.Items.Add("Baichuan2-Turbo");
                    if (DeFine.GlobalLocalSetting.BaichuanModel.Trim().Length > 0)
                    {
                        this.BaichuanModel.SelectedValue = DeFine.GlobalLocalSetting.BaichuanModel;
                    }
                    else
                    {
                        this.BaichuanModel.SelectedValue = "Baichuan4-Turbo";
                    }

                    NodePanel.Visibility = Visibility.Visible;
                }));

                var GetStr = new LMStudio().QuickTrans(new List<string>() { },"Test Str", Languages.English, Languages.SimplifiedChinese, true, 3, "");

                //new CohereApi().QuickTrans("Test Line", Languages.English, Languages.SimplifiedChinese, true, 1, "");
                //DeFine.GlobalLocalSetting.BaichuanKey = "";
                //var GetStr = new BaichuanApi().QuickTrans("Test Line", Languages.English, Languages.SimplifiedChinese, true, 1, "");

                //DeFine.GlobalLocalSetting.GeminiKey = "";
                //var Get = new GeminiApi().QuickTrans("TestStr", Languages.English, Languages.SimplifiedChinese, false, 3, "");

                //Test
                //DeFine.GlobalLocalSetting.DeepLKey = "";
                //ProxyCenter.GlobalProxyIP = "127.0.0.1:7890";
                //new DeepLApi().QuickTrans("Test",Languages.English,Languages.SimplifiedChinese);

            }).Start();
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

        public void LoadAny(string FilePath)
        {
            DeFine.ShowLogView();
            Translator.ClearAICache();
            SetLog("Load:" + FilePath);
            ClosetTransTrd();
            if (System.IO.File.Exists(FilePath))
            {
                DeFine.CurrentDashBoardView.Open(FilePath);
                DeFine.GlobalLocalSetting.AutoLoadDictionaryFile = false;
                FromStr.Text = "";
                ToStr.Text = "";

                string GetFileName = FilePath.Substring(FilePath.LastIndexOf(@"\") + @"\".Length);
                Caption.Text = GetFileName;

                DeFine.CurrentModName = GetFileName;

                string GetModName = GetFileName;
                FModName = LModName = GetModName;
                if (LModName.Contains("."))
                {
                    LModName = LModName.Substring(0, LModName.LastIndexOf("."));
                }

                if (!CheckDictionary())
                {
                    DeleteDictionaryBtn.Opacity = 0.3;
                }
                else
                {
                    DeleteDictionaryBtn.Opacity = 1;
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

        public void ShowFrameByTag(string TagName)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                Grid SelectGrid = null;
                foreach (var GetView in Frames.Children)
                {
                    if (GetView is Grid)
                    {
                        if (ConvertHelper.ObjToStr((GetView as Grid).Tag) == TagName)
                        {
                            (GetView as Grid).Visibility = Visibility.Visible;
                            SelectGrid = (GetView as Grid);
                        }
                        else
                        {
                            (GetView as Grid).Visibility = Visibility.Hidden;
                        }
                    }
                }
                if (SelectGrid != null)
                {
                    //Footer.Background = SelectGrid.Background;
                }
            }));

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
                            DeFine.CloseAny();
                        }
                        break;
                }
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


        public int SmallNavState = 0;

        private void AutoShowSmallNav(object sender, MouseButtonEventArgs e)
        {
            if (SmallNavState == 0)
            {
                Storyboard GetStoryboard = this.Resources["ShowSmallNav"] as Storyboard;
                if (GetStoryboard != null)
                {
                    GetStoryboard.Begin();
                }

                ShowNavSign.Source = new BitmapImage(new Uri("/Material/WhiteUP.png", UriKind.Relative));
                ShowNavSign.Visibility = Visibility.Visible;
                SmallNavState = 1;
            }
            else
            {
                Storyboard GetStoryboard = this.Resources["HideSmallNav"] as Storyboard;
                if (GetStoryboard != null)
                {
                    GetStoryboard.Begin();
                }

                ShowNavSign.Source = new BitmapImage(new Uri("/Material/WhiteDown.png", UriKind.Relative));
                ShowNavSign.Visibility = Visibility.Visible;
                SmallNavState = 0;
            }
        }

        public void CheckEngineState()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                foreach (var GetEngine in EngineUIs.Children)
                {
                    if (GetEngine is SvgViewbox)
                    {
                        string GetContent = ConvertHelper.ObjToStr((GetEngine as SvgViewbox).Tag);

                        switch (GetContent)
                        {
                            case "GoogleEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.GoogleYunApiUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.15;
                                    }
                                }
                                break;
                            case "ChatGptEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.ChatGptApiUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.15;
                                    }
                                }
                                break;
                            case "GeminiEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.GeminiApiUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.15;
                                    }
                                }
                                break;
                            case "CohereEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.CohereApiUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.15;
                                    }
                                }
                                break;
                            case "DeepSeekEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.DeepSeekApiUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.15;
                                    }
                                }
                                break;
                            case "BaichuanEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.BaichuanApiUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.15;
                                    }
                                }
                                break;
                            case "LMLocalEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.LMLocalAIEngineUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.15;
                                    }
                                }
                                break;
                            case "DeepLEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.DeepLApiUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.15;
                                    }
                                }
                                break;
                            case "DivEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.DivCacheEngineUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.15;
                                    }
                                }
                                break;
                        }
                    }
                }
            }));
        }

        private void SelectEngine(object sender, MouseButtonEventArgs e)
        {
            string GetContent = ConvertHelper.ObjToStr((sender as SvgViewbox).Tag);

            double GetOpacity = ConvertHelper.ObjToDouble((sender as SvgViewbox).Opacity);

            bool OneState = false;

            if (GetOpacity == 1)
            {
                OneState = false;
                (sender as SvgViewbox).Opacity = 0.15;
            }
            else
            {
                OneState = true;
                (sender as SvgViewbox).Opacity = 1;
            }

            switch (GetContent)
            {
                case "GoogleEngine":
                    {
                        DeFine.GlobalLocalSetting.GoogleYunApiUsing = OneState;
                    }
                    break;
                case "ChatGptEngine":
                    {
                        DeFine.GlobalLocalSetting.ChatGptApiUsing = OneState;
                    }
                    break;
                case "GeminiEngine":
                    {
                        DeFine.GlobalLocalSetting.GeminiApiUsing = OneState;
                    }
                    break;
                case "CohereEngine":
                    {
                        DeFine.GlobalLocalSetting.CohereApiUsing = OneState;
                    }
                    break;
                case "DeepSeekEngine":
                    {
                        DeFine.GlobalLocalSetting.DeepSeekApiUsing = OneState;
                    }
                    break;
                case "BaichuanEngine":
                    {
                        DeFine.GlobalLocalSetting.BaichuanApiUsing = OneState;
                    }
                    break;
                case "LMLocalEngine":
                    {
                        DeFine.GlobalLocalSetting.LMLocalAIEngineUsing = OneState;
                    }
                    break;
                case "DeepLEngine":
                    {
                        DeFine.GlobalLocalSetting.DeepLApiUsing = OneState;
                    }
                    break;
                case "DivEngine":
                    {
                        DeFine.GlobalLocalSetting.DivCacheEngineUsing = OneState;
                    }
                    break;
            }
        }

        public void CheckTransTrdState()
        {
            if (BatchTranslationHelper.TransMainTrd == null)
            {
                StopAny = true;
                AutoKeepTag.Background = new SolidColorBrush(Color.FromRgb(11, 116, 209));
                AutoKeep.Source = new Uri("pack://application:,,,/SSELex;component/Material/Keep.svg");
            }
        }

        public void ClosetTransTrd()
        {
            StopAny = true;
            AutoKeepTag.Background = new SolidColorBrush(Color.FromRgb(11, 116, 209));

            AutoKeep.Source = new Uri("pack://application:,,,/SSELex;component/Material/Keep.svg");
            BatchTranslationHelper.Close();
        }



        public bool StopAny = true;

        private void ChangeTransProcessState(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border || sender is SvgViewbox)
            {
                if (StopAny == true)
                {
                    StopAny = false;
                    AutoKeepTag.Background = new SolidColorBrush(Color.FromRgb(0, 51, 97));

                    AutoKeep.Source = new Uri("pack://application:,,,/SSELex;component/Material/Stop.svg");
                    if (DeFine.GlobalLocalSetting.AutoSetThreadLimit)
                    {
                        DeFine.GlobalLocalSetting.MaxThreadCount = 0;
                        if (DeFine.GlobalLocalSetting.BaichuanApiUsing && DeFine.GlobalLocalSetting.BaichuanKey.Trim().Length > 0)
                        {
                            DeFine.GlobalLocalSetting.MaxThreadCount++;
                        }
                        if (DeFine.GlobalLocalSetting.GoogleYunApiUsing && DeFine.GlobalLocalSetting.GoogleApiKey.Trim().Length > 0)
                        {
                            DeFine.GlobalLocalSetting.MaxThreadCount++;
                        }
                        if (DeFine.GlobalLocalSetting.DeepLApiUsing && DeFine.GlobalLocalSetting.DeepLKey.Trim().Length > 0)
                        {
                            DeFine.GlobalLocalSetting.MaxThreadCount++;
                        }
                        if (DeFine.GlobalLocalSetting.ChatGptApiUsing && DeFine.GlobalLocalSetting.ChatGptKey.Trim().Length > 0)
                        {
                            DeFine.GlobalLocalSetting.MaxThreadCount++;
                        }
                        if (DeFine.GlobalLocalSetting.GeminiApiUsing && DeFine.GlobalLocalSetting.GeminiKey.Trim().Length > 0)
                        {
                            DeFine.GlobalLocalSetting.MaxThreadCount++;
                        }
                        if (DeFine.GlobalLocalSetting.DeepSeekApiUsing && DeFine.GlobalLocalSetting.DeepSeekKey.Trim().Length > 0)
                        {
                            DeFine.GlobalLocalSetting.MaxThreadCount++;
                        }
                        if (DeFine.GlobalLocalSetting.MaxThreadCount == 1)
                        {
                            DeFine.GlobalLocalSetting.MaxThreadCount = 2;
                        }
                        if (DeFine.GlobalLocalSetting.MaxThreadCount == 0)
                        {
                            DeFine.GlobalLocalSetting.MaxThreadCount = 1;
                        }
                    }
                    BatchTranslationHelper.Start();
                }
                else
                {
                    ClosetTransTrd();
                }
            }
        }

        public void ExportHtml()
        {
            var GetData = SkyrimDataConvert.GenDictionary(FModName);
            string GenHtml = "<html><tittle>{0}</tittle>\n<body>\n{1}\n</body>\n</html>";
            string GenLine = "<li><span><a>{0}</a>|<a>{1}</a>|<a>{2}</a></span></li>\n";
            string BodyContent = "";
            foreach (var Get in GetData.Dictionarys)
            {
                string GetLine = string.Format(GenLine, Get.Key, Get.OriginalText, Get.TransText);
                BodyContent += GetLine;
            }
            GenHtml = string.Format(GenHtml, FModName, BodyContent);
            var GetWritePath = DataHelper.ShowSaveFileDialog(LModName + ".html", "html (*.html)|*.html");
            if (GetWritePath != null)
            {
                if (GetWritePath.Trim().Length > 0)
                {
                    if (File.Exists(GetWritePath))
                    {
                        File.Delete(GetWritePath);
                    }
                    DataHelper.WriteFile(GetWritePath, Encoding.UTF8.GetBytes(GenHtml));
                }
            }
        }
        public void ExportJson()
        {
            var JsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var GetData = SkyrimDataConvert.GenDictionary(FModName);
            string GetJson = JsonSerializer.Serialize(GetData, JsonOptions);

            var GetWritePath = DataHelper.ShowSaveFileDialog(LModName + ".json", "SSELex (*.json)|*.json");
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
        public void ExportAs()
        {
            string GetNeedType = ConvertHelper.ObjToStr(ExportTypes.SelectedValue);

            if (CurrentTransType == 3)
            {
                if (GetNeedType.Equals("Json(Dynamic String Distributor)"))
                {
                    MessageBox.Show("Conversion of PEX, MCM. files to DSD is not supported");
                }
                else
                if (GetNeedType.Equals("Json(SSELex)"))
                {
                    ExportJson();
                }
                else
                if (GetNeedType.Equals("Html"))
                {
                    ExportHtml();
                }
            }
            if (CurrentTransType == 2)
            {
                if (GetNeedType.Equals("Json(Dynamic String Distributor)"))
                {
                    var JsonOptions = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    };
                    //Json(Dynamic String Distributor)
                    var GetData = SkyrimDataDSDConvert.EspExportAllByDSD(GlobalEspReader);
                    string GetJson = JsonSerializer.Serialize(GetData, JsonOptions);
                    var GetWritePath = DataHelper.ShowSaveFileDialog(LModName + ".json", "DSD (*.json)|*.json");
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
                else
                if (GetNeedType.Equals("Json(SSELex)"))
                {
                    ExportJson();
                }
                else
                if (GetNeedType.Equals("Html"))
                {
                    ExportHtml();
                }
            }
            else
            if (CurrentTransType == 1)
            {
                if (GetNeedType.Equals("Json(Dynamic String Distributor)"))
                {
                    MessageBox.Show("Conversion of ESP, ESM, ESL. files to DSD is not supported");
                }
                else
                if (GetNeedType.Equals("Json(SSELex)"))
                {
                    ExportJson();
                }
                else
                if (GetNeedType.Equals("Html"))
                {
                    ExportHtml();
                }
            }
        }

        public int LoadSaveState = 0;
        private void AutoLoadOrSave(object sender, MouseButtonEventArgs e)
        {
            if (LoadSaveState == 0)
            {
                LoadSaveState = 1;
                LoadAny();
                LoadFileButton.Content = UILanguageHelper.SearchStateChangeStr("LoadFileButton", 1);
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

                Translator.WriteDictionary();
                YDDictionaryHelper.CreatDictionary();

                CancelTransEsp(null, null);
                LoadFileButton.Content = UILanguageHelper.SearchStateChangeStr("LoadFileButton", 0);
            }
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

                (StartTransBtn.Child as Label).Content = UILanguageHelper.SearchStateChangeStr("LoadFileButton", 0);
                LoadSaveState = 0;

                CancelBtn.Opacity = 0.3;
                CancelBtn.IsEnabled = false;

                TypeSelector.Items.Clear();
                YDDictionaryHelper.Close();

                UIHelper.ModifyCount = 0;
                
                Caption.Text = "ModTranslator";
            }));
        }
        private void CancelTransEsp(object sender, MouseButtonEventArgs e)
        {
            CancelAny();
        }

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
            SmallMenu.Visibility = Visibility.Collapsed;
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
            SmallMenu.Visibility = Visibility.Visible;
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
                            LoadAny(GetFilePath);
                            ModTransView_DragLeave(null, null);
                        }
                    }
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

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            SyncCodeViewLocation();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DeFine.CloseAny();
        }

        private void Source_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string GetValue = ConvertHelper.ObjToStr(FromL.SelectedValue);

            if (GetValue.Trim().Length > 0)
            {
                DeFine.SourceLanguage = Enum.Parse<Languages>(GetValue);
            }

            DeFine.GlobalLocalSetting.SourceLanguage = DeFine.SourceLanguage;
        }

        private void Target_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string GetValue = ConvertHelper.ObjToStr(ToL.SelectedValue);

            if (GetValue.Trim().Length > 0)
            {
                DeFine.TargetLanguage = Enum.Parse<Languages>(GetValue);
            }

            DeFine.GlobalLocalSetting.TargetLanguage = DeFine.TargetLanguage;
        }

        private void ClearCache(object sender, MouseButtonEventArgs e)
        {
            Translator.ClearAICache();
            if (ActionWin.Show("Clear the cache for translation?", "Warning: After cleanup, all current content (including previous translations) will no longer be cached. Translating again will retrieve data from the cloud, which may waste your previous translation work and increase word count consumption.", MsgAction.YesNo, MsgType.Info) > 0)
            {
                if (Translator.ClearCloudCache(DeFine.CurrentModName))
                {
                    ActionWin.Show("DBMsg", "Done!", MsgAction.Yes, MsgType.Info);
                    DeFine.GlobalDB.ExecuteNonQuery("vacuum");
                    DeFine.WorkingWin.ReloadData();
                }
            }
            else
            {

            }
        }

        public void AutoViewMode()
        {
            if (ConvertHelper.ObjToStr(ViewMode.SelectedValue).Equals("Quick"))
            {
                CaptionRow.Height = new GridLength(30, GridUnitType.Pixel);
                DefRow.Height = new GridLength(1, GridUnitType.Star);
                ExtendRowA.Height = new GridLength(0, GridUnitType.Pixel);
                ExtendRowB.Height = new GridLength(0, GridUnitType.Star);

                DeFine.CurrentSearchStr = string.Empty;
                SearchBox.Text = string.Empty;
                ExtendBox.Width = 0;

                DeFine.ViewMode = 0;

                if (TransViewList != null)
                {
                    if (TransViewList.RealLines != null)
                    {
                        ReloadData(true);
                    }
                }

                DeFine.GlobalLocalSetting.ViewMode = "Quick";
            }
            else
            {
                CaptionRow.Height = new GridLength(30, GridUnitType.Pixel);
                DefRow.Height = new GridLength(1, GridUnitType.Star);
                ExtendRowB.Height = new GridLength(0.7, GridUnitType.Star);
                ExtendRowA.Height = new GridLength(3, GridUnitType.Pixel);
                ExtendBox.Width = this.Width - TransViewBox.Width;

                DeFine.ViewMode = 1;

                if (TransViewList != null)
                {
                    if (TransViewList.RealLines != null)
                    {
                        ReloadData(true);
                    }
                }

                DeFine.GlobalLocalSetting.ViewMode = "Normal";
            }
        }

        private void ViewModeChange(object sender, SelectionChangedEventArgs e)
        {
            AutoViewMode();
        }
        private CancellationTokenSource MainTransTrdToken;
        private Thread MainTransTrd = null;
        private void TransCurrentItem(object sender, MouseButtonEventArgs e)
        {
            if (DeFine.ViewMode == 1)
            {
                if (ConvertHelper.ObjToStr(TransBtn.Content).Equals("Translate(F1)"))
                {
                    TextSegmentTranslator SegmentTranslator = null;
                    MainTransTrd = new Thread(() =>
                    {
                        MainTransTrdToken = new CancellationTokenSource();
                        var Token = MainTransTrdToken.Token;


                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            TransViewList.GetMainCanvas().IsHitTestVisible = false;
                        }));
                        string GetFromStr = "";
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            GetFromStr = FromStr.Text;
                        }));

                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            TransBtn.Content = "Processing...";
                        }));

                        Token.ThrowIfCancellationRequested();

                        if (UIHelper.ActiveKey.EndsWith("(BookText)") && UIHelper.ActiveType.Equals("Book"))
                        {
                            this.Dispatcher.Invoke(new Action(() =>
                            {
                                SegmentTranslator = new TextSegmentTranslator(ToStr, TransBtn);
                            }));

                            SegmentTranslator.TransBook(UIHelper.ActiveKey, GetFromStr, Token);
                        }
                        else
                        {
                            bool CanSleep = true;
                            var GetResult = Translator.QuickTrans(DeFine.CurrentModName,UIHelper.ActiveType, UIHelper.ActiveKey, GetFromStr,DeFine.SourceLanguage,DeFine.TargetLanguage, ref CanSleep);

                            this.Dispatcher.Invoke(new Action(() =>
                            {
                                ToStr.Text = GetResult;
                            }));
                        }

                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            TransViewList.GetMainCanvas().IsHitTestVisible = true;
                        }));
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            TransBtn.Content = "Translate(F1)";
                        }));

                    });
                    MainTransTrd.Start();
                }
                else
                {
                    MainTransTrdToken.Cancel();
                }
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F1 && ConvertHelper.ObjToStr(ViewMode.SelectedValue) == "Normal")
            {
                if (TransViewList.GetMainCanvas().IsHitTestVisible)
                {
                    TransCurrentItem(null, null);
                }
            }
            if (e.Key == System.Windows.Input.Key.F2 && ConvertHelper.ObjToStr(ViewMode.SelectedValue) == "Normal")
            {
                if (TransViewList.GetMainCanvas().IsHitTestVisible)
                {
                    ApplyTransStr(null, null);
                }
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.ActualHeight > 200 && this.ActualWidth > 200)
            {
                DeFine.GlobalLocalSetting.FormHeight = this.ActualHeight;
                DeFine.GlobalLocalSetting.FormWidth = this.ActualWidth;
            }

            AutoViewMode();

            SyncCodeViewLocation();
        }
        public void SyncCodeViewLocation()
        {
            try
            {
                if (DeFine.GlobalLocalSetting.ShowLog)
                {
                    if (DeFine.CurrentDashBoardView != null)
                    {
                        if (DeFine.CurrentDashBoardView.Visibility == Visibility.Visible)
                        {
                            DeFine.CurrentDashBoardView.Left = this.Left + this.Width + 5;
                        }
                    }
                }
            }
            catch { }

            try
            {
                var GetHeight = this.Height;
                var GetLeft = this.Left;
                var GetTop = this.Top;
                if (DeFine.CurrentCodeView != null)
                {
                    DeFine.CurrentCodeView.Dispatcher.Invoke(new Action(() =>
                    {
                        DeFine.CurrentCodeView.Height = GetHeight;
                        DeFine.CurrentCodeView.Left = GetLeft - DeFine.CurrentCodeView.Width - 10;
                        DeFine.CurrentCodeView.Top = GetTop;
                    }));
                }
            }
            catch { }
        }

        private void UsingContext_Click(object sender, RoutedEventArgs e)
        {
            if (UsingContext.IsChecked == true)
            {
                DeFine.GlobalLocalSetting.UsingContext = true;
            }
            else
            {
                DeFine.GlobalLocalSetting.UsingContext = false;
            }
        }

        private void UsingDictionary_Click(object sender, RoutedEventArgs e)
        {
            if (LoadSaveState != 0)
            {
                if (DeFine.GlobalLocalSetting.AutoLoadDictionaryFile)
                {
                    UsingDictionary.IsChecked = true;
                }
                else
                {
                    UsingDictionary.IsChecked = false;
                }
                MessageBox.Show("The stop dictionary cannot be enabled during the translation phase.");
            }
            else
            {
                if (UsingDictionary.IsChecked == true)
                {
                    DeFine.GlobalLocalSetting.AutoLoadDictionaryFile = true;
                }
                else
                {
                    DeFine.GlobalLocalSetting.AutoLoadDictionaryFile = false;
                }

                DeFine.GlobalLocalSetting.SaveConfig();
            }
        }

        private void ReSetTransLang(object sender, MouseButtonEventArgs e)
        {
            if (TransViewList.Rows > 0)
                if (ActionWin.Show("Do you agree?", "This will restore all fields to their initial state.", MsgAction.YesNo, MsgType.Info, 230) > 0)
                {
                    Translator.ReSetAllTransText();
                }
        }

        private void ReStoreTransLang(object sender, MouseButtonEventArgs e)
        {
            if (TransViewList.Rows > 0)
            {
                string GetModName = YDDictionaryHelper.CurrentModName;
                string SetPath = DeFine.GetFullPath(@"Librarys\" + GetModName) + ".Json";
                if (File.Exists(SetPath))
                {
                    if (ActionWin.Show("Do you agree?", "This will revert back to the original file.", MsgAction.YesNo, MsgType.Info, 230) > 0)
                    {
                        if (CurrentSelect != ObjSelect.All)
                        {
                            ClosetTransTrd();
                            CurrentSelect = ObjSelect.All;
                            ReloadDataFunc();
                        }

                        Translator.ReStoreAllTransText();

                        if (ActionWin.Show("Do you agree?", "Delete DataBase and save", MsgAction.YesNo, MsgType.Info, 230) > 0)
                        {
                            string SqlOrder = "Delete From CloudTranslation Where ModName = '" + DeFine.CurrentModName + "'";
                            int State = DeFine.GlobalDB.ExecuteNonQuery(SqlOrder);
                            if (State != 0)
                            {
                                DeFine.GlobalDB.ExecuteNonQuery("vacuum");
                            }
                        }
                    }
                }
            }

        }

        private void Nav_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image)
            {
                string GetTag = ConvertHelper.ObjToStr(((Image)sender).Tag);
                switch (GetTag)
                {
                    case "ModTransView":
                        {
                            ShowModTransView();
                        }
                        break;
                    case "SettingView":
                        {
                            ShowSettingsView(Settings.ApiKey);
                        }
                        break;
                }
            }
        }

        public enum Settings
        {
            Software = 0, Game = 2, ApiKey = 3, AI = 5, LocalEngine = 6
        }

        public void ShowModTransView()
        {
            ModTransView.Visibility = Visibility.Visible;
            SettingView.Visibility = Visibility.Hidden;
            DeFine.GlobalLocalSetting.SaveConfig();
        }

        public void ShowSettingsView(Settings View)
        {
            ModTransView.Visibility = Visibility.Hidden;
            SettingView.Visibility = Visibility.Visible;

            if (View == Settings.ApiKey)
            {
                ApiKeyView.Visibility = Visibility.Visible;
                SoftView.Visibility = Visibility.Hidden;
                GameView.Visibility = Visibility.Hidden;
                AIView.Visibility = Visibility.Hidden;
                EngineConfigView.Visibility = Visibility.Hidden;

                HttpProxy.Text = DeFine.GlobalLocalSetting.ProxyIP;

                BaichuanKey.Password = DeFine.GlobalLocalSetting.BaichuanKey;
                CohereKey.Password = DeFine.GlobalLocalSetting.CohereKey;

                GoogleKey.Password = DeFine.GlobalLocalSetting.GoogleApiKey;

                GeminiKey.Password = DeFine.GlobalLocalSetting.GeminiKey;
                ChatGptKey.Password = DeFine.GlobalLocalSetting.ChatGptKey;
                DeepSeekKey.Password = DeFine.GlobalLocalSetting.DeepSeekKey;

                BaichuanKey.Password = DeFine.GlobalLocalSetting.BaichuanKey;

                if (DeFine.GlobalLocalSetting.GeminiModel.Trim().Length > 0)
                {
                    GeminiModel.SelectedValue = DeFine.GlobalLocalSetting.GeminiModel;
                }

                if (DeFine.GlobalLocalSetting.DeepSeekModel.Trim().Length > 0)
                {
                    DeepSeekModel.SelectedValue = DeFine.GlobalLocalSetting.DeepSeekModel;
                }

                if (DeFine.GlobalLocalSetting.ChatGptModel.Trim().Length > 0)
                {
                    ChatGptModel.SelectedValue = DeFine.GlobalLocalSetting.ChatGptModel;
                }


                DeepLKey.Password = DeFine.GlobalLocalSetting.DeepLKey;

                if (DeFine.GlobalLocalSetting.IsFreeDeepL)
                {
                    IsFreeDeepL.IsChecked = true;
                }

                UIHelper.AnimateCanvasLeft(SettingBlock, 139);
            }
            if (View == Settings.Software)
            {
                SoftView.Visibility = Visibility.Visible;
                ApiKeyView.Visibility = Visibility.Hidden;
                GameView.Visibility = Visibility.Hidden;
                AIView.Visibility = Visibility.Hidden;
                EngineConfigView.Visibility = Visibility.Hidden;

                MaxThreadCount.Text = ConvertHelper.ObjToStr(DeFine.GlobalLocalSetting.MaxThreadCount);

                UILanguages.Items.Clear();

                foreach (var Get in UILanguageHelper.SupportLanguages)
                {
                    UILanguages.Items.Add(Get.ToString());
                }

                UILanguages.SelectedValue = DeFine.GlobalLocalSetting.CurrentUILanguage.ToString();

                if (DeFine.GlobalLocalSetting.AutoSetThreadLimit)
                {
                    AutoSetThreadLimit.IsChecked = true;
                }
                if (DeFine.GlobalLocalSetting.ShowLog)
                {
                    ShowLog.IsChecked = true;
                }
                if (DeFine.GlobalLocalSetting.ShowCode)
                {
                    ShowCode.IsChecked = true;
                }

                UIHelper.AnimateCanvasLeft(SettingBlock, 39);
            }
            if (View == Settings.Game)
            {
                GameView.Visibility = Visibility.Visible;
                ApiKeyView.Visibility = Visibility.Hidden;
                SoftView.Visibility = Visibility.Hidden;
                AIView.Visibility = Visibility.Hidden;
                EngineConfigView.Visibility = Visibility.Hidden;

                SkyrimTypes.Items.Clear();
                SkyrimTypes.Items.Add(SkyrimType.SkyrimSE.ToString());
                SkyrimTypes.Items.Add(SkyrimType.SkyrimLE.ToString());
                SkyrimTypes.SelectedValue = DeFine.GlobalLocalSetting.SkyrimType.ToString();

                SkyrimSEPath.Text = DeFine.GlobalLocalSetting.SkyrimPath;

                if (DeFine.GlobalLocalSetting.AutoCompress)
                {
                    AutoCompress.IsChecked = true;
                }

                UIHelper.AnimateCanvasLeft(SettingBlock, 229);
            }
            if (View == Settings.AI)
            {
                GameView.Visibility = Visibility.Hidden;
                ApiKeyView.Visibility = Visibility.Hidden;
                SoftView.Visibility = Visibility.Hidden;
                AIView.Visibility = Visibility.Visible;
                EngineConfigView.Visibility = Visibility.Hidden;

                ContextLimit.Text = DeFine.GlobalLocalSetting.ContextLimit.ToString();
                CustomAIPrompt.Text = DeFine.GlobalLocalSetting.UserCustomAIPrompt;

                UIHelper.AnimateCanvasLeft(SettingBlock, 312);
            }
            if (View == Settings.LocalEngine)
            {
                GameView.Visibility = Visibility.Hidden;
                ApiKeyView.Visibility = Visibility.Hidden;
                SoftView.Visibility = Visibility.Hidden;
                AIView.Visibility = Visibility.Hidden;
                EngineConfigView.Visibility = Visibility.Visible;

                UIHelper.AnimateCanvasLeft(SettingBlock, 399);
            }
        }

        private void HttpProxy_TextChanged(object sender, TextChangedEventArgs e)
        {
            DeFine.GlobalLocalSetting.ProxyIP = HttpProxy.Text.Trim();
        }
        private void ChatGptKey_PasswordChanged(object sender, RoutedEventArgs e)
        {
            DeFine.GlobalLocalSetting.ChatGptKey = ChatGptKey.Password;
        }

        private void ChatGptModelChange(object sender, SelectionChangedEventArgs e)
        {
            string GetModel = ConvertHelper.ObjToStr(ChatGptModel.SelectedValue);
            if (GetModel.Trim().Length > 0)
            {
                DeFine.GlobalLocalSetting.ChatGptModel = GetModel;
            }
        }
        private void Gemini_PasswordChanged(object sender, RoutedEventArgs e)
        {
            DeFine.GlobalLocalSetting.GeminiKey = GeminiKey.Password;
        }

        private void GeminiModelChange(object sender, SelectionChangedEventArgs e)
        {
            string GetModel = ConvertHelper.ObjToStr(GeminiModel.SelectedValue);
            if (GetModel.Trim().Length > 0)
            {
                DeFine.GlobalLocalSetting.GeminiModel = GetModel;
            }
        }
        private void DeepSeekKey_PasswordChanged(object sender, RoutedEventArgs e)
        {
            DeFine.GlobalLocalSetting.DeepSeekKey = DeepSeekKey.Password;
        }

        private void DeepSeekModelChange(object sender, SelectionChangedEventArgs e)
        {
            string GetModel = ConvertHelper.ObjToStr(DeepSeekModel.SelectedValue);
            if (GetModel.Trim().Length > 0)
            {
                DeFine.GlobalLocalSetting.DeepSeekModel = GetModel;
            }
        }
        private void BaichuanModelChange(object sender, SelectionChangedEventArgs e)
        {
            string GetModel = ConvertHelper.ObjToStr(BaichuanModel.SelectedValue);
            if (GetModel.Trim().Length > 0)
            {
                DeFine.GlobalLocalSetting.BaichuanModel = GetModel;
            }
        }

        private void BaichuanKey_PasswordChanged(object sender, RoutedEventArgs e)
        {
            DeFine.GlobalLocalSetting.BaichuanKey = BaichuanKey.Password;
        }

        private void CohereKey_PasswordChanged(object sender, RoutedEventArgs e)
        {
            DeFine.GlobalLocalSetting.CohereKey = CohereKey.Password;
        }
        private void DeepLKey_PasswordChanged(object sender, RoutedEventArgs e)
        {
            DeFine.GlobalLocalSetting.DeepLKey = DeepLKey.Password;
        }

        private void IsFreeDeepL_Click(object sender, RoutedEventArgs e)
        {
            if (IsFreeDeepL.IsChecked == true)
            {
                DeFine.GlobalLocalSetting.IsFreeDeepL = true;
            }
            else
            {
                DeFine.GlobalLocalSetting.IsFreeDeepL = false;
            }
        }
      
        private void GoogleKey_PasswordChanged(object sender, RoutedEventArgs e)
        {
            DeFine.GlobalLocalSetting.GoogleApiKey = GoogleKey.Password;
        }

        private void MaxThreadCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            DeFine.GlobalLocalSetting.MaxThreadCount = ConvertHelper.ObjToInt(MaxThreadCount.Text);
        }

        private void Languages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string GetValue = ConvertHelper.ObjToStr(UILanguages.SelectedValue);
            if (GetValue.Trim().Length > 0)
            {
                DeFine.GlobalLocalSetting.CurrentUILanguage = Enum.Parse<Languages>(ConvertHelper.ObjToStr(UILanguages.SelectedValue));
                UILanguageHelper.ChangeLanguage(DeFine.GlobalLocalSetting.CurrentUILanguage);

                if (DeFine.WorkingWin != null)
                {
                    if (DeFine.WorkingWin.LoadSaveState == 1)
                    {
                        DeFine.WorkingWin.LoadFileButton.Content = UILanguageHelper.SearchStateChangeStr("LoadFileButton", 1);
                    }
                }
            }
        }

        private void ShowLog_Click(object sender, RoutedEventArgs e)
        {
            if (ShowLog.IsChecked == true)
            {
                DeFine.GlobalLocalSetting.ShowLog = true;
            }
            else
            {
                DeFine.GlobalLocalSetting.ShowLog = false;
            }
        }

        private void ShowCode_Click(object sender, RoutedEventArgs e)
        {
            if (ShowCode.IsChecked == true)
            {
                DeFine.GlobalLocalSetting.ShowCode = true;
            }
            else
            {
                DeFine.GlobalLocalSetting.ShowCode = false;
            }
        }

        private void SettingsChild_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Label)
            {
                string GetTag = ConvertHelper.ObjToStr(((Label)sender).Tag);
                switch (GetTag)
                {
                    case "Software":
                        {
                            ShowSettingsView(Settings.Software);
                        }
                        break;
                    case "ApiKey":
                        {
                            ShowSettingsView(Settings.ApiKey);
                        }
                        break;
                    case "Game":
                        {
                            ShowSettingsView(Settings.Game);
                        }
                        break;
                    case "AI":
                        {
                            ShowSettingsView(Settings.AI);
                        }
                        break;
                }

            }

        }

        private void SkyrimSEPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            DeFine.GlobalLocalSetting.SkyrimPath = SkyrimSEPath.Text;
        }

        private void EspEncodings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string GetSelectValue = ConvertHelper.ObjToStr(EspEncodings.SelectedValue);
            if (GetSelectValue.Trim().Length > 0)
            {
                EncodingTypes GetType = Enum.Parse<EncodingTypes>(GetSelectValue);
                DeFine.GlobalLocalSetting.FileEncoding = GetType;
            }
        }

        private void SkyrimTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string GetSelectValue = ConvertHelper.ObjToStr(SkyrimTypes.SelectedValue);
            if (GetSelectValue.Trim().Length > 0)
            {
                SkyrimType GetType = Enum.Parse<SkyrimType>(GetSelectValue);
                DeFine.GlobalLocalSetting.SkyrimType = GetType;
            }
        }

        private void StartPexFix(object sender, MouseButtonEventArgs e)
        {
            string GetInstallPath = DeFine.GetFullPath(@"\");

            if (GetInstallPath.Contains(@"\Desktop"))
            {
                MessageBox.Show("Please do not unzip to the desktop or a sub-path of the desktop.");
            }
            else
            if (!File.Exists(DeFine.GetFullPath(@"Tool\Champollion.exe")))
            {
                MessageBox.Show("Please Download Champollion.exe");
            }
            else
            {
                if (FileAttributesHelper.IsAdministrator())
                {
                    FileAttributesHelper.UnlockAllFiles(DeFine.GetFullPath(@"Tool\"));

                    FileAttributesHelper.UnlockAllFiles(DeFine.GetFullPath(@"Tool\Original Compiler\"));

                    MessageBox.Show("Repair has been attempted. Please rerun the program.");

                    DeFine.CloseAny();
                }
                else
                {
                    if (ActionWin.Show("Prompt", "This feature requires administrator privileges. Do you agree to request administrator permissions? After restarting with administrator rights, you will need to click this button again.", MsgAction.YesNo, MsgType.Info, 330) > 0)
                    {
                        FileAttributesHelper.RestartAsAdmin();
                    }
                }
            }
        }

        private void ApplyTransStr(object sender, MouseButtonEventArgs e)
        {
            if (LoadSaveState != 0)
            {
                string TransText = ToStr.Text;

                if (UIHelper.ActiveTextBox != null)
                {
                    if (UIHelper.ActiveKey.Contains("Score:"))
                    {
                        double GetScore = ConvertHelper.ObjToDouble(UIHelper.ActiveKey.Substring(UIHelper.ActiveKey.IndexOf("Score:") + "Score:".Length).Trim());
                        if (GetScore < 0)
                        {
                            return;
                        }
                    }
                    UIHelper.ActiveTextBox.Text = TransText;
                    UIHelper.ActiveTextBox.BorderBrush = new SolidColorBrush(Colors.Green);

                    Translator.TransData[UIHelper.ActiveKey] = TransText;

                    LocalDBCache.UPDateLocalTransItem(new LocalTransItem(UIHelper.ActiveKey,ToStr.Text));

                    UIHelper.MainGrid_MouseLeave(UIHelper.ActiveTextBox.Tag, null);
                }

                if (TransText.Trim().Length == 0)
                {
                    UIHelper.ActiveTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(87, 87, 87));
                }
            }
        }


        private void SearchKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LoadSaveState != 0)
            {
                if (SearchBox.Text.Trim().Length == 0)
                {
                    DeFine.CurrentSearchStr = string.Empty;
                    ReloadData();
                }
            }
        }

        private void AutoCompress_Click(object sender, RoutedEventArgs e)
        {
            if (AutoCompress.IsChecked == true)
            {
                DeFine.GlobalLocalSetting.AutoCompress = true;
            }
            else
            {
                DeFine.GlobalLocalSetting.AutoCompress = false;
            }
        }

        private void SaveAs_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ExportAs();
        }

        private void GridSplitter_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {

        }

        private void TransView_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void AutoSetThreadLimit_Click(object sender, RoutedEventArgs e)
        {
            if (AutoSetThreadLimit.IsChecked == true)
            {
                DeFine.GlobalLocalSetting.AutoSetThreadLimit = true;
            }
            else
            {
                DeFine.GlobalLocalSetting.AutoSetThreadLimit = false;
            }
        }

        private void Publishedon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ActionWin.Show("HelpMsg", "Do you want to visit this website?\nIf you need.Click Yes to jump to the URL", MsgAction.YesNo, MsgType.Info, 230) > 0)
                Process.Start(new ProcessStartInfo("https://www.nexusmods.com/skyrimspecialedition/mods/143056") { UseShellExecute = true });
        }

        private void OpenSource_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ActionWin.Show("HelpMsg", "Do you want to visit this website?\nIf you need.Click Yes to jump to the URL", MsgAction.YesNo, MsgType.Info, 230) > 0)
                Process.Start(new ProcessStartInfo("https://github.com/YD525/SSELexicon") { UseShellExecute = true });
        }

        private void CustomAIPrompt_TextChanged(object sender, TextChangedEventArgs e)
        {
            DeFine.GlobalLocalSetting.UserCustomAIPrompt = CustomAIPrompt.Text.Trim();
        }

        private void ContextLimit_TextChanged(object sender, TextChangedEventArgs e)
        {
            DeFine.GlobalLocalSetting.ContextLimit = ConvertHelper.ObjToInt(ContextLimit.Text);
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DeFine.CurrentSearchStr = SearchBox.Text.Trim();
            StartSearch();
        }
        private void CancelSelect(object sender, MouseButtonEventArgs e)
        {
            UIHelper.ActiveKey = string.Empty;
            UIHelper.ActiveTextBox = null;
            UIHelper.ActiveType = string.Empty;
            FromStr.Text = string.Empty;
            ToStr.Text = string.Empty;
            ReplaceKeyBox.Text = string.Empty;
            ReplaceValueBox.Text = string.Empty;
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

        private void DeleteDictionary(object sender, MouseButtonEventArgs e)
        {
            if (DeFine.CurrentModName.Trim().Length > 0)
            {
                string SetPath = DeFine.GetFullPath(@"\Librarys\" + DeFine.CurrentModName + ".Json");
                if (File.Exists(SetPath))
                {
                    File.Delete(SetPath);
                    MessageBox.Show("Done!");
                }
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

        private void ReplaceStr(object sender, MouseButtonEventArgs e)
        {
            string GetSelectMode = ConvertHelper.ObjToStr(ReplaceMode.SelectedValue);

            if (GetSelectMode.Trim().Equals("Current"))
            {
                ToStr.Text = ToStr.Text.Replace(ReplaceKeyBox.Text.Trim(), ReplaceValueBox.Text.Trim());
            }
            else
            {
                if (ActionWin.Show("Do you agree?", $"This will replace the contents of all rows. \"{ReplaceKeyBox.Text}\"->\"{ReplaceValueBox.Text}\"", MsgAction.YesNo, MsgType.Info, 230) > 0)
                {
                    Translator.ReplaceAllLine(ReplaceKeyBox.Text.Trim(), ReplaceValueBox.Text.Trim());
                }
            }
        }

        private void TestBtn_Click(object sender, RoutedEventArgs e)
        {
            Translator.TestAll();
        }

        private void ShowLocalSetting(object sender, MouseButtonEventArgs e)
        {
            DeFine.LocalConfigView.Owner = this;
            DeFine.LocalConfigView.Show();
        }
    }
}