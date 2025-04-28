using Microsoft.WindowsAPICodePack.Dialogs;
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
using ICSharpCode.AvalonEdit;
using SSELex.TranslateManagement;
using SSELex.SkyrimManagement;
using System.Text.Json;
using System.Text;

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
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.Log.Text = Str;
            }));
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

        public void ReloadDataFunc(bool CanReloadCountCache = true)
        {
            lock (LockerAddTrd)
            {
                GC.Collect();

                if (CanReloadCountCache)
                {
                    UIHelper.ModifyCountCache.Clear();
                }
                UIHelper.ModifyCount = 0;

                this.Dispatcher.Invoke(new Action(() =>
                {
                    TransViewList.Clear();
                }));

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
                            TransViewList.AddRowR(UIHelper.CreatLine(GetItem.Type, GetItem.EditorID, GetItem.Key, GetItem.SourceText, GetItem.GetTextIfTransR(), false));
                        }));

                    }
                }
                if (CurrentTransType == 3)
                {
                    foreach (var GetItem in GlobalPexReader.SafeStringParams)
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            TransViewList.AddRowR(UIHelper.CreatLine(GetItem.Type, GetItem.EditorID, GetItem.Key, GetItem.SourceText, GetItem.GetTextIfTransR(), GetItem.Danger));
                        }));
                    }
                }

                GetStatistics();
            }
        }

        public void ReloadData(bool CanReloadCountCache = true)
        {
            if (LoadSaveState == 1)
            {
                new Thread(() =>
                {
                    ReloadDataFunc(CanReloadCountCache);
                }).Start();
            }
        }

        public int MaxTransCount = 0;
        public void GetStatistics()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                double GetRate = ((double)UIHelper.ModifyCount / (double)TransViewList.Rows);
                if (GetRate > 0)
                {
                    try
                    {
                        ProcessBar.Width = ProcessBarFrame.ActualWidth * GetRate;
                    }
                    catch { }
                }
                else
                {
                    ProcessBar.Width = 1;
                }
                MaxTransCount = TransViewList.Rows;
                TransProcess.Content = string.Format("STRINGS({0}/{1})", UIHelper.ModifyCount, MaxTransCount);
            }));
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
            Source.Items.Clear();
            foreach (var Get in UILanguageHelper.SupportLanguages)
            {
                Source.Items.Add(Get.ToString());
            }

            Source.SelectedValue = DeFine.SourceLanguage.ToString();

            Target.Items.Clear();
            foreach (var Get in UILanguageHelper.SupportLanguages)
            {
                Target.Items.Add(Get.ToString());
            }

            Target.SelectedValue = DeFine.TargetLanguage.ToString();
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

                    this.Dispatcher.Invoke(new Action(() => {
                        FromStr.SyntaxHighlighting = HighlightingLoader.Load(xshd, HighlightingManager.Instance);
                        ToStr.SyntaxHighlighting = HighlightingLoader.Load(xshd, HighlightingManager.Instance);
                    }));
                }
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DeFine.Init(this);
            ReloadViewMode();
            ReloadLanguageMode();
            this.MainCaption.Content = string.Format("SSELex Gui {0}", DeFine.CurrentVersion);

            WordProcess.SendTranslateMsg += TranslateMsg;

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
                ShowFrameByTag("LoadingView");

                DeFine.LoadData();
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
                    this.Dispatcher.Invoke(new Action(() => {
                        ShowSettingsView(Settings.Game);
                    }));
                }

                SetLog("OpenSource:https://github.com/YD525/YDSkyrimToolR");
                Thread.Sleep(3000);
                SetLog(string.Empty);

                ProxyCenter.GlobalProxyIP = DeFine.GlobalLocalSetting.ProxyIP;

                this.Dispatcher.Invoke(new Action(() =>
                {
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
                }));

               
                //Test
                //DeFine.GlobalLocalSetting.DeepLKey = "";
                //ProxyCenter.GlobalProxyIP = "127.0.0.1:7890";
                //new DeepLApi().QuickTrans("Test",Languages.English,Languages.SimplifiedChinese);

            }).Start();
        }

        public void LoadAny()
        {
            CommonOpenFileDialog Dialog = new CommonOpenFileDialog();
            Dialog.IsFolderPicker = false;   //设置为选择文件夹
            if (Dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                LoadAny(Dialog.FileName);
            }
        }

        public int CurrentTransType = 0;
        public string FModName = "";
        public string LModName = "";

        string LastSetPath = "";

        public void LoadAny(string FilePath)
        {
            DeFine.ShowLogView();
            WordProcess.ClearAICache();
            SetLog("Load:" + FilePath);
            ClosetTransTrd();
            if (System.IO.File.Exists(FilePath))
            {
                FromStr.Text = "";
                ToStr.Text = "";

                string GetFileName = FilePath.Substring(FilePath.LastIndexOf(@"\") + @"\".Length);
                Caption.Text = GetFileName;

                string GetModName = GetFileName;
                FModName = LModName = GetModName;
                if (LModName.Contains("."))
                {
                    LModName = LModName.Substring(0, LModName.LastIndexOf("."));
                }

                if (DeFine.GlobalLocalSetting.AutoLoadDictionaryFile)
                {
                    YDDictionaryHelper.ReadDictionary(GetModName);
                }
                else
                {
                    YDDictionaryHelper.Close();
                }

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
                    ReloadData();
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
                            case "PhraseEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.PhraseEngineUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.15;
                                    }
                                }
                                break;
                            case "CodeEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.CodeParsingEngineUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.15;
                                    }
                                }
                                break;
                            case "ConjunctionEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.ConjunctionEngineUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.15;
                                    }
                                }
                                break;
                            case "GoogleEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.GoogleYunApiUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.15;
                                    }
                                }
                                break;
                            case "BaiduEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.BaiDuYunApiUsing)
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
                            case "DeepSeekEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.DeepSeekApiUsing)
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
                case "PhraseEngine":
                    {
                        DeFine.GlobalLocalSetting.PhraseEngineUsing = OneState;
                    }
                    break;
                case "CodeEngine":
                    {
                        DeFine.GlobalLocalSetting.CodeParsingEngineUsing = OneState;
                    }
                    break;
                case "ConjunctionEngine":
                    {
                        DeFine.GlobalLocalSetting.ConjunctionEngineUsing = OneState;
                    }
                    break;
                case "GoogleEngine":
                    {
                        DeFine.GlobalLocalSetting.GoogleYunApiUsing = OneState;
                    }
                    break;
                case "BaiduEngine":
                    {
                        DeFine.GlobalLocalSetting.BaiDuYunApiUsing = OneState;
                    }
                    break;
                case "ChatGptEngine":
                    {
                        DeFine.GlobalLocalSetting.ChatGptApiUsing = OneState;
                    }
                    break;
                case "DeepSeekEngine":
                    {
                        DeFine.GlobalLocalSetting.DeepSeekApiUsing = OneState;
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

        public bool CheckPlatformStates()
        {
            if (DeFine.GlobalLocalSetting.BaiDuYunApiUsing)
            {
                if ((DeFine.GlobalLocalSetting.BaiDuAppID.Trim().Length > 0 && DeFine.GlobalLocalSetting.BaiDuSecretKey.Trim().Length > 0)==false)
                {
                    return false;
                }
            }
            if (DeFine.GlobalLocalSetting.ChatGptApiUsing)
            {
                if ((DeFine.GlobalLocalSetting.ChatGptKey.Trim().Length > 0) == false)
                {
                    return false;
                }
            }
            if (DeFine.GlobalLocalSetting.DeepSeekApiUsing)
            {
                if ((DeFine.GlobalLocalSetting.DeepSeekKey.Trim().Length > 0) == false)
                {
                    return false;
                }
            }

            return true;
        }

        public bool StopAny = true;

        private void ChangeTransProcessState(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border || sender is SvgViewbox)
            {
                if (StopAny == true)
                {
                    if (CheckPlatformStates())
                    {
                        StopAny = false;
                        AutoKeepTag.Background = new SolidColorBrush(Color.FromRgb(0, 51, 97));

                        AutoKeep.Source = new Uri("pack://application:,,,/SSELex;component/Material/Stop.svg");
                        BatchTranslationHelper.Start();
                    }
                    else
                    {
                        MessageBox.Show("ApiKey is not configured or is incorrect.");
                    }
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
                string GetLine = string.Format(GenLine,Get.Key,Get.OriginalText,Get.TransText);
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
            if (GetWritePath!=null)
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
                LoadSaveState = 0;

                Caption.Name = "TranslationCore";
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
                            GlobalPexReader.SavePexFile(LastSetPath);
                        }
                }
                if (CurrentTransType == 2)
                {
                    if (UIHelper.ModifyCount > 0)
                        if (GlobalEspReader != null)
                        {
                            if (GlobalEspReader.CurrentReadMod != null)
                            {
                                if (CurrentSelect != ObjSelect.All)
                                {
                                    CurrentSelect = ObjSelect.All;
                                    SkyrimDataLoader.Load(CurrentSelect, GlobalEspReader, TransViewList);
                                }

                                string GetBackUPPath = GetFilePath + GetFileFullName + ".backup";
                                if (File.Exists(GetBackUPPath))
                                {
                                    File.Delete(GetBackUPPath);
                                }
                                File.Copy(LastSetPath, GetBackUPPath);
                                File.Delete(LastSetPath);
                                SkyrimDataWriter.WriteAllMemoryData(ref GlobalEspReader);
                                GlobalEspReader.DefSaveMod(GlobalEspReader.CurrentReadMod, LastSetPath);
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
                            if (File.Exists(GetBackUPPath))
                            {
                                File.Delete(GetBackUPPath);
                            }
                            File.Copy(LastSetPath, GetBackUPPath);
                            File.Delete(LastSetPath);

                            GlobalMCMReader.SaveMCMConfig(LastSetPath);
                        }
                }
                if (DeFine.GlobalLocalSetting.AutoLoadDictionaryFile)
                {
                    WordProcess.WriteDictionary();
                    YDDictionaryHelper.CreatDictionary();
                }
                else
                {
                    YDDictionaryHelper.Close();
                }

                CancelTransEsp(null, null);
                LoadFileButton.Content = UILanguageHelper.SearchStateChangeStr("LoadFileButton", 0);
            }
        }
        public void CancelAny()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                ClosetTransTrd();

                Caption.Name = "TranslationCore";

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
                GetStatistics();
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
            //  English = 0, Chinese = 1, Japanese = 2, German = 5, Korean = 6
            string GetValue = ConvertHelper.ObjToStr(Source.SelectedValue);

            if (GetValue.Trim().Length > 0)
            {
                DeFine.SourceLanguage = Enum.Parse<Languages>(GetValue);
            }

            DeFine.GlobalLocalSetting.SourceLanguage = DeFine.SourceLanguage;
        }

        private void Target_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string GetValue = ConvertHelper.ObjToStr(Target.SelectedValue);

            if (GetValue.Trim().Length > 0)
            {
                DeFine.TargetLanguage = Enum.Parse<Languages>(GetValue);
            }

            DeFine.GlobalLocalSetting.TargetLanguage = DeFine.TargetLanguage;
        }

        private void ClearCache(object sender, MouseButtonEventArgs e)
        {
            WordProcess.ClearAICache();
            if (ActionWin.Show("Clear the cache for translation?", "Warning: After cleaning up, all content including previous translations will no longer be cached. Translating again will retrieve it from the cloud, which will waste the results of your previous translations and increase word count consumption!", MsgAction.YesNo, MsgType.Info) > 0)
            {
                string SqlOrder = "Delete From CloudTranslation Where 1=1";
                int State = DeFine.GlobalDB.ExecuteNonQuery(SqlOrder);
                if (State != 0)
                {
                    ActionWin.Show("DBMsg", "Done!", MsgAction.Yes, MsgType.Info);
                    DeFine.GlobalDB.ExecuteNonQuery("vacuum");
                }
                else
                {

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
                DeFine.CurrentSearchStr = string.Empty;
                SearchKey.Text = string.Empty;
                ExtendFrame.Height = new GridLength(0,GridUnitType.Pixel);
                ExtendBox.Width = 0;

                DeFine.ViewMode = 0;
                ReloadData();
                DeFine.GlobalLocalSetting.ViewMode = "Quick";
            }
            else
            {
                ExtendFrame.Height = new GridLength(1.8, GridUnitType.Star);
                ExtendBox.Width = this.Width - TransViewBox.Width;

                DeFine.ViewMode = 1;
                ReloadData();
                DeFine.GlobalLocalSetting.ViewMode = "Normal";
            }
        }

        private void ViewModeChange(object sender, SelectionChangedEventArgs e)
        {
            AutoViewMode();
        }

        private void TransCurrentItem(object sender, MouseButtonEventArgs e)
        {
            if (DeFine.ViewMode == 1)
            {
                if (ConvertHelper.ObjToStr(TransBtn.Content).Equals("Translate(F1)"))
                {
                    new Thread(() =>
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            TransBtn.Content = "Working..";
                        }));
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            TransViewList.GetMainGrid().IsHitTestVisible = false;
                        }));
                        string GetFromStr = "";
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            GetFromStr = FromStr.Text;
                        }));
                        List<EngineProcessItem> EngineProcessItems = new List<EngineProcessItem>();
                        var GetResult = new WordProcess().QuickTrans(GetFromStr, DeFine.SourceLanguage, DeFine.TargetLanguage);
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            ToStr.Text = GetResult;
                        }));
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            TransViewList.GetMainGrid().IsHitTestVisible = true;
                        }));
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            TransBtn.Content = "Translate(F1)";
                        }));
                    }).Start();
                }
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F1 && ConvertHelper.ObjToStr(ViewMode.SelectedValue) == "Normal")
            {
                if (TransViewList.GetMainGrid().IsHitTestVisible)
                {
                    TransCurrentItem(null, null);
                }
            }
            if (e.Key == System.Windows.Input.Key.F2 && ConvertHelper.ObjToStr(ViewMode.SelectedValue) == "Normal")
            {
                if (TransViewList.GetMainGrid().IsHitTestVisible)
                {
                    ApplyTransStr(null, null);
                }
            }
        }

        public void SendQuestionToAI(object Engine, string QuestionStr)
        {
            //if (Engine is ChatGptApi)
            //{
            //    var GetResult = (Engine as ChatGptApi).CallAI($"{QuestionStr} ,请用 {DeFine.TargetLanguage.ToString()} 回复,能有点猫娘的口吻吗可爱点的.");

            //    if (GetResult != null)
            //    {
            //        if (GetResult.choices != null)
            //        {
            //            string CNStr = "";
            //            if (GetResult.choices.Length > 0)
            //            {
            //                CNStr = GetResult.choices[0].message.content.Trim();
            //            }
            //            if (CNStr.Trim().Length > 0)
            //            {
            //                this.Dispatcher.Invoke(new Action(() =>
            //                {
            //                    AILog.Text = CNStr;
            //                }));
            //            }
            //            else
            //            {
            //                this.Dispatcher.Invoke(new Action(() =>
            //                {
            //                    AILog.Text = string.Empty;
            //                }));
            //            }
            //        }
            //    }
            //}
            //else
            //if (Engine is DeepSeekApi)
            //{
            //    var GetResult = (Engine as DeepSeekApi).CallAI($"{QuestionStr} ,请用 {DeFine.TargetLanguage} 回复,能有点猫娘的口吻吗可爱点的.");

            //    if (GetResult != null)
            //    {
            //        if (GetResult.choices != null)
            //        {
            //            string CNStr = "";
            //            if (GetResult.choices.Length > 0)
            //            {
            //                CNStr = GetResult.choices[0].message.content.Trim();
            //            }
            //            if (CNStr.Trim().Length > 0)
            //            {
            //                this.Dispatcher.Invoke(new Action(() =>
            //                {
            //                    AILog.Text = CNStr;
            //                }));
            //            }
            //            else
            //            {
            //                this.Dispatcher.Invoke(new Action(() =>
            //                {
            //                    AILog.Text = string.Empty;
            //                }));
            //            }
            //        }
            //    }
            //}
        }

        private void SendQuestionToAI(object sender, MouseButtonEventArgs e)
        {
            //if (!StopAny)
            //{
            //    return;
            //}
            //string GetSendAIText = SendAIText.Text;
            //if (ConvertHelper.ObjToStr(SendQuestionBtn.Content).Equals("SendQuestion"))
            //{
            //    new Thread(() =>
            //    {
            //        this.Dispatcher.Invoke(new Action(() =>
            //        {
            //            SendQuestionBtn.Content = "AI Thinking...";
            //        }));

            //        List<object> AllEngines = new List<object>();

            //        if (DeFine.GlobalLocalSetting.ChatGptApiUsing && DeFine.GlobalLocalSetting.ChatGptKey.Trim().Length > 0)
            //        {
            //            AllEngines.Add(new ChatGptApi());
            //        }
            //        if (DeFine.GlobalLocalSetting.DeepSeekApiUsing && DeFine.GlobalLocalSetting.DeepSeekKey.Trim().Length > 0)
            //        {
            //            AllEngines.Add(new DeepSeekApi());
            //        }

            //        if (AllEngines.Count == 1)
            //        {
            //            SendQuestionToAI(AllEngines[0], GetSendAIText);
            //        }
            //        else
            //        if (AllEngines.Count > 0)
            //        {
            //            SendQuestionToAI(AllEngines[new Random(Guid.NewGuid().GetHashCode()).Next(0, AllEngines.Count)], GetSendAIText);
            //        }

            //        this.Dispatcher.Invoke(new Action(() =>
            //        {
            //            SendQuestionBtn.Content = "SendQuestion";
            //        }));

            //    }).Start();
            //}
        }

        private void DetectLang(object sender, MouseButtonEventArgs e)
        {
            var GetLang = LanguageHelper.DetectLanguage();
            DeFine.SourceLanguage = GetLang;
            Source.SelectedValue = GetLang.ToString();
        }

        private void ReplaceAllLine(object sender, MouseButtonEventArgs e)
        {
            if (ActionWin.Show("Do you agree?", $"This will replace the contents of all rows. \"{ReplaceKey.Text}\"->\"{ReplaceValue.Text}\"", MsgAction.YesNo, MsgType.Info, 230) > 0)
            {
                WordProcess.ReplaceAllLine(ReplaceKey.Text.Trim(), ReplaceValue.Text.Trim());
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            AutoViewMode();
            SyncCodeViewLocation();
        }
        public void SyncCodeViewLocation()
        {
            try
            {
                if (DeFine.GlobalLocalSetting.ShowLog)
                {
                    if (DeFine.CurrentLogView != null)
                    {
                        if (DeFine.CurrentLogView.Visibility == Visibility.Visible)
                        {
                            DeFine.CurrentLogView.Left = this.Left + this.Width + 5;
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
                    WordProcess.ReSetAllTransText();
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
                            ReloadDataFunc(true);
                        }

                        WordProcess.ReStoreAllTransText();

                        if (ActionWin.Show("Do you agree?", "Delete dictionary file and save", MsgAction.YesNo, MsgType.Info, 230) > 0)
                        {
                            if (File.Exists(SetPath))
                            {
                                File.Delete(SetPath);
                            }
                            bool BackUPState = DeFine.GlobalLocalSetting.AutoLoadDictionaryFile;
                            DeFine.GlobalLocalSetting.AutoLoadDictionaryFile = false;
                            AutoLoadOrSave(LoadFileButton, null);
                            DeFine.GlobalLocalSetting.AutoLoadDictionaryFile = BackUPState;
                        }
                    }
                }
            }

        }

        private void TestCall(object sender, MouseButtonEventArgs e)
        {
            HeuristicCore.CheckPexParams(GlobalPexReader);
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
            Software = 0,Game = 2,ApiKey = 3
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

                HttpProxy.Text = DeFine.GlobalLocalSetting.ProxyIP;

                BaiDuAppID.Text = DeFine.GlobalLocalSetting.BaiDuAppID;
                BaiDuKey.Password = DeFine.GlobalLocalSetting.BaiDuSecretKey;

                GoogleKey.Password = DeFine.GlobalLocalSetting.GoogleApiKey;
                ChatGptKey.Password = DeFine.GlobalLocalSetting.ChatGptKey;
                DeepSeekKey.Password = DeFine.GlobalLocalSetting.DeepSeekKey;

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

                UIHelper.AnimateCanvasLeft(SettingBlock, 149);
            }
            if (View == Settings.Software)
            {
                SoftView.Visibility = Visibility.Visible;
                ApiKeyView.Visibility = Visibility.Hidden;
                GameView.Visibility = Visibility.Hidden;

                MaxThreadCount.Text = ConvertHelper.ObjToStr(DeFine.GlobalLocalSetting.MaxThreadCount);

                UILanguages.Items.Clear();

                foreach (var Get in UILanguageHelper.SupportLanguages)
                {
                    UILanguages.Items.Add(Get.ToString());
                }

                UILanguages.SelectedValue = DeFine.GlobalLocalSetting.CurrentUILanguage.ToString();

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

                SkyrimTypes.Items.Clear();
                SkyrimTypes.Items.Add(SkyrimType.SkyrimSE.ToString());
                SkyrimTypes.Items.Add(SkyrimType.SkyrimLE.ToString());
                SkyrimTypes.SelectedValue = DeFine.GlobalLocalSetting.SkyrimType.ToString();

                SkyrimSEPath.Text = DeFine.GlobalLocalSetting.SkyrimPath;

                if (DeFine.GlobalLocalSetting.AutoCompress)
                {
                    AutoCompress.IsChecked = true;
                }

                UIHelper.AnimateCanvasLeft(SettingBlock, 249);
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
        private void BaiDuAppID_TextChanged(object sender, TextChangedEventArgs e)
        {
            DeFine.GlobalLocalSetting.BaiDuAppID = BaiDuAppID.Text;
        }

        private void BaiDuKey_PasswordChanged(object sender, RoutedEventArgs e)
        {
            DeFine.GlobalLocalSetting.BaiDuSecretKey = BaiDuKey.Password;
        }

        private void GoogleKey_PasswordChanged(object sender, RoutedEventArgs e)
        {
            DeFine.GlobalLocalSetting.GoogleApiKey = GoogleKey.Password;
        }

        private void StartApiKeyTest(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border)
            {
                sender = ((Border)sender).Child;
            }
            string GetBtnValue = ConvertHelper.ObjToStr(((Label)sender).Content);

            if (GetBtnValue != "Testing...")
            {
                KeyTestBtn.Content = "Testing...";

                new Thread(() =>
                {
                    string RichText = "";
                    if (DeFine.GlobalLocalSetting.ChatGptApiUsing && DeFine.GlobalLocalSetting.ChatGptKey.Trim().Length == 0)
                    {
                        RichText += "Please configure ChatGPT key\r\n";
                    }
                    if (DeFine.GlobalLocalSetting.DeepSeekApiUsing && DeFine.GlobalLocalSetting.DeepSeekKey.Trim().Length == 0)
                    {
                        RichText += "Please configure DeepSeek key\r\n";
                    }
                    if (DeFine.GlobalLocalSetting.DeepLApiUsing && DeFine.GlobalLocalSetting.DeepLKey.Trim().Length == 0)
                    {
                        RichText += "Please configure DeepL key\r\n";
                    }

                    if (DeFine.GlobalLocalSetting.DeepLKey.Trim().Length > 0)
                    {
                        if (new DeepLApi().QuickTrans("Test", Languages.English, Languages.Japanese).Trim().Length == 0)
                        {
                            RichText += "DeepL API Key is not configured correctly.\r\n";
                        }
                    }
                    if (DeFine.GlobalLocalSetting.ChatGptKey.Trim().Length > 0)
                    {
                        if (new ChatGptApi().QuickTrans("Test", Languages.English, Languages.Japanese).Trim().Length == 0)
                        {
                            RichText += "ChatGPT API Key is not configured correctly.\r\n";
                        }
                    }
                    if (DeFine.GlobalLocalSetting.DeepSeekKey.Trim().Length > 0)
                    {
                        if (new DeepSeekApi().QuickTrans("Test", Languages.English, Languages.SimplifiedChinese).Trim().Length == 0)
                        {
                            RichText += "DeepSeek API Key is not configured correctly.\r\n";
                        }
                    }
                    if (DeFine.GlobalLocalSetting.GoogleApiKey.Trim().Length > 0)
                    {
                        if (new GoogleTransApi().Translate("Test", Languages.English, Languages.Japanese).Trim().Length == 0)
                        {
                            RichText += "Google API Key is not configured correctly.\r\n";
                        }
                    }
                    if (DeFine.GlobalLocalSetting.BaiDuAppID.Trim().Length > 0 && DeFine.GlobalLocalSetting.BaiDuSecretKey.Trim().Length > 0)
                    {
                        var GetResult = new BaiDuApi().TransStr("Test", Languages.English, Languages.SimplifiedChinese);
                        if (GetResult != null)
                        {
                            if (GetResult.to == null)
                            {
                                RichText += "BaiDu API Key is not configured correctly.\r\n";
                            }
                        }
                        else
                        {
                            RichText += "BaiDu API Key is not configured correctly.\r\n";
                        }
                    }

                    KeyTestBtn.Dispatcher.Invoke(new Action(() => {
                        KeyTestBtn.Content = "TestKey";
                    }));

                    if (RichText.Trim().Length > 0)
                    {
                        MessageBox.Show(RichText);
                    }
                    else
                    {
                        MessageBox.Show("No invalid configuration found.");
                    }
                }).Start();
            }
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
                }
               
            }
           
        }

        private void SkyrimSEPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            DeFine.GlobalLocalSetting.SkyrimPath = SkyrimSEPath.Text;
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

                if (UIHelper.ActiveTextBox != null && FromStr.Text.Trim().Length > 0)
                {
                    UIHelper.ActiveTextBox.Text = TransText;

                    Translator.TransData[UIHelper.ActiveKey] = TransText;
                }
            }
        }

        private void ToStr_MouseLeave(object sender, MouseEventArgs e)
        {
            ApplyTransStr(null,null);
        }

        private void ReplaceLine(object sender, MouseButtonEventArgs e)
        {
            ToStr.Text = ToStr.Text.Replace(ReplaceKey.Text.Trim(), ReplaceValue.Text.Trim());
        }

        private void SearchStr(object sender, MouseButtonEventArgs e)
        {
            DeFine.CurrentSearchStr = SearchKey.Text.Trim();
            ReloadData();
        }

        private void SearchKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LoadSaveState != 0)
            {
                if (SearchKey.Text.Trim().Length == 0)
                {
                    DeFine.CurrentSearchStr = string.Empty;
                    ReloadData();
                }
            }
        }

        private void ClearTrans_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ToStr.Text = "";
        }

        private void ToStr_TextChanged(object sender, EventArgs e)
        {
            if (ToStr.Text.Length == 0)
            {
                ClearBtn.Visibility = Visibility.Hidden;
            }
            else
            {
                ClearBtn.Visibility = Visibility.Visible;
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

       
    }
}