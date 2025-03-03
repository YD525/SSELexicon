using Microsoft.WindowsAPICodePack.Dialogs;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Starfield;
using SharpVectors.Converters;
using System.IO;
using System.Text;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YDSkyrimToolR.ConvertManager;
using YDSkyrimToolR.SkyrimManage;
using YDSkyrimToolR.SkyrimModManager;
using YDSkyrimToolR.TranslateCore;
using YDSkyrimToolR.TranslateManage;
using YDSkyrimToolR.UIManage;
using YDSkyrimTools.SkyrimModManager;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static YDSkyrimToolR.UIManage.SkyrimDataLoader;
/*
* @Author: YD525
* @GitHub: https://github.com/YD525/YDSkyrimToolR
* @Date: 2025-02-06
*/
namespace YDSkyrimToolR
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

        public void ReloadData(bool CanReloadCountCache = true)
        {
            new Thread(() =>
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
            }).Start();
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
                MaxTransCount = TransViewList.Rows;
                TransProcess.Content = string.Format("STRINGS({0}/{1})", UIHelper.ModifyCount, MaxTransCount);
            }));
        }

        public void ReloadViewMode()
        {
            ViewMode.Items.Clear();
            ViewMode.Items.Add("Quick");
            ViewMode.Items.Add("Normal");

            ViewMode.SelectedValue = ViewMode.Items[0];
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

        public void CheckINeed()
        {
            //Frist Check ToolPath
            if (!File.Exists(DeFine.GetFullPath(@"Tool\Champollion.exe")))
            {
                ActionWin.Show("PEX File lacks support", "Please manually install the dependent program\n[https://github.com/Orvid/Champollion]\nPlease download the release version and put it in this path\n[" + DeFine.GetFullPath(@"Tool\") + "]\n Path required\n[" + DeFine.GetFullPath(@"Tool\Champollion.exe") + "]", MsgAction.Yes, MsgType.Info, 390);
            }

            if (!Directory.Exists(DeFine.GlobalLocalSetting.SkyrimPath))
            {
                DeFine.ShowSetting();
                ActionWin.Show("Msg", "Please Configure SkyrimSE path", MsgAction.Yes, MsgType.Info, 200);
            }
            else
            {
                string CompilerPath = "";
                if (!SkyrimHelper.FindPapyrusCompilerPath(ref CompilerPath))
                {
                    var GetStr = DeFine.GlobalLocalSetting.SkyrimPath + "Papyrus Compiler" + @"\PapyrusAssembler.exe";
                    string Msg = "Please Download CreationKit [" + GetStr + "] Must exist.";
                    ActionWin.Show("PEX File lacks support", Msg, MsgAction.Yes, MsgType.Info, 300);
                }
            }
        }
        public void TranslateMsg(string EngineName, string Text, string Result)
        {
            SetLog(string.Format("{0}->{1},{2}", EngineName, Text, Result));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DeFine.Init(this);
            ReloadViewMode();
            ReloadLanguageMode();

            WordProcess.SendTranslateMsg += TranslateMsg;

            SetLog("Copyright (C) 2025 YD525 OpenSource:https://github.com/YD525/YDSkyrimToolR");

            CheckINeed();

            if (DeFine.GlobalLocalSetting.AutoLoadDictionaryFile)
            {
                UsingDictionary.IsChecked = true;
            }

            new Thread(() =>
            {
                ShowFrameByTag("LoadingView");

                //LocalTrans.Init();

                CheckEngineState();

                GlobalEspReader = new EspReader();
                GlobalMCMReader = new MCMReader();
                GlobalPexReader = new PexReader();

                Thread.Sleep(1000);
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    UILanguageHelper.ChangeLanguage(DeFine.GlobalLocalSetting.CurrentUILanguage);
                }));

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

        string LastSetPath = "";

        public void LoadAny(string FilePath)
        {
            SetLog("Load:" + FilePath);
            if (System.IO.File.Exists(FilePath))
            {
                string GetFileName = FilePath.Substring(FilePath.LastIndexOf(@"\") + @"\".Length);
                Caption.Content = GetFileName;

                string GetModName = GetFileName;
                if (DeFine.GlobalLocalSetting.AutoLoadDictionaryFile)
                {
                    YDDictionaryHelper.ReadDictionary(GetModName);
                }
                else
                {
                    YDDictionaryHelper.Close();
                }

                //3.1.1 Version esl not support
                if (FilePath.ToLower().EndsWith(".pex"))
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
                            Application.Current.Shutdown();
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
                    LockerGrid.Background = new SolidColorBrush(Color.FromRgb(244, 101, 155));
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
                    LockerGrid.Background = new SolidColorBrush(Color.FromRgb(247, 127, 172));
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

                SmallNavState = 1;
            }
            else
            {
                Storyboard GetStoryboard = this.Resources["HideSmallNav"] as Storyboard;
                if (GetStoryboard != null)
                {
                    GetStoryboard.Begin();
                }

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
                                        (GetEngine as SvgViewbox).Opacity = 0.1;
                                    }
                                }
                                break;
                            case "CodeEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.CodeParsingEngineUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.1;
                                    }
                                }
                                break;
                            case "ConjunctionEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.ConjunctionEngineUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.1;
                                    }
                                }
                                break;
                            case "GoogleEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.GoogleYunApiUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.1;
                                    }
                                }
                                break;
                            case "BaiduEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.BaiDuYunApiUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.1;
                                    }
                                }
                                break;
                            case "ChatGptEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.ChatGptApiUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.1;
                                    }
                                }
                                break;
                            case "DeepSeekEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.DeepSeekApiUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.1;
                                    }
                                }
                                break;
                            case "DivEngine":
                                {
                                    if (!DeFine.GlobalLocalSetting.DivCacheEngineUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.1;
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
                (sender as SvgViewbox).Opacity = 0.1;
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
                case "DivEngine":
                    {
                        DeFine.GlobalLocalSetting.DivCacheEngineUsing = OneState;
                    }
                    break;
            }
        }

        public void CheckTransTrdState()
        {
            if (WordProcess.MainTransThread == null)
            {
                StopAny = true;
                AutoKeepTag.Background = new SolidColorBrush(Color.FromRgb(247, 137, 178));
                AutoKeep.Source = new Uri("pack://application:,,,/YDSkyrimToolR;component/Material/Keep.svg");
            }
        }

        public bool StopAny = true;

        private void ChangeTransProcessState(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border || sender is SvgViewbox)
            {
                if (StopAny == true)
                {
                    StopAny = false;
                    AutoKeepTag.Background = new SolidColorBrush(Color.FromRgb(147, 85, 108));

                    AutoKeep.Source = new Uri("pack://application:,,,/YDSkyrimToolR;component/Material/Stop.svg");
                    WordProcess.StartAutoTransService(true);
                }
                else
                {
                    StopAny = true;
                    AutoKeepTag.Background = new SolidColorBrush(Color.FromRgb(247, 137, 178));

                    AutoKeep.Source = new Uri("pack://application:,,,/YDSkyrimToolR;component/Material/Keep.svg");
                    WordProcess.StartAutoTransService(false);
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
                                SystemDataWriter.WriteAllMemoryData(ref GlobalEspReader);
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

        private void CancelTransEsp(object sender, MouseButtonEventArgs e)
        {
            WordProcess.StartAutoTransService(false);

            StopAny = true;
            AutoKeepTag.Background = new SolidColorBrush(Color.FromRgb(247, 137, 178));

            AutoKeep.Source = new Uri("pack://application:,,,/YDSkyrimToolR;component/Material/Keep.svg");

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
                CurrentSelect = Enum.Parse<ObjSelect>(GetSelectValue);
                ReloadData();
            }
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DeFine.GlobalLocalSetting.SaveConfig();
            Environment.Exit(0);
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
            if (ActionWin.Show("clear the cache for translation?", "Warning: After cleaning up, all content including previous translations will no longer be cached. Translating again will retrieve it from the cloud, which will waste the results of your previous translations and increase word count consumption!", MsgAction.YesNo, MsgType.Info) > 0)
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

        private void ShowAboutUS(object sender, MouseButtonEventArgs e)
        {
            ActionWin.Show("约定酱的YDSkyrimTools", "YDSkyrimTools It is an open-source and free translation tool for Skyrim SE Mod\r\nIntended to significantly reduce the workload of Sinicizers\r\nProtocol GPL3.0\r\n See Github https://github.com/tolove336/YDSkyrimToolR\r\n I only handle it when I have free time", MsgAction.Null, MsgType.Info);
        }

        public void AutoViewMode()
        {
            if (ConvertHelper.ObjToStr(ViewMode.SelectedValue).Equals("Quick"))
            {
                TransViewBox.Width = double.NaN;
                TransViewBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                ExtendBox.Width = 0;
            }
            else
            {
                TransViewBox.Width = this.Width * 0.6;
                TransViewBox.HorizontalAlignment = HorizontalAlignment.Left;
                ExtendBox.Width = this.Width - TransViewBox.Width;
            }
        }

        private void ViewModeChange(object sender, SelectionChangedEventArgs e)
        {
            AutoViewMode();
        }

        private void ToStr_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FromStr.Text.Trim().Length == 0)
            {
                return;
            }
            if (sender is TextBox)
            {
                if ((sender as TextBox).Tag != null)
                {
                    int Key = ConvertHelper.ObjToInt((sender as TextBox).Tag);
                    string GetText = (sender as TextBox).Text;
                    if (GetText.Trim().Length > 0)
                    {
                        (UIHelper.SelectLine.Children[4] as TextBox).Text = GetText;
                        Translator.TransData[Key] = GetText;
                    }
                    else
                    {
                        (UIHelper.SelectLine.Children[4] as TextBox).Text = string.Empty;
                        (UIHelper.SelectLine.Children[4] as TextBox).BorderBrush = new SolidColorBrush(Color.FromRgb(87, 87, 87));
                        if (Translator.TransData.ContainsKey(Key))
                        {
                            Translator.TransData.Remove(Key);
                        }
                    }
                }
            }
        }


        private void TransCurrentItem(object sender, MouseButtonEventArgs e)
        {
            new Thread(() =>
            {
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
                var GetResult = new WordProcess().ProcessWords(ref EngineProcessItems, GetFromStr, DeFine.SourceLanguage, DeFine.TargetLanguage);
                this.Dispatcher.Invoke(new Action(() =>
                {
                    ToStr.Text = GetResult;
                }));
                this.Dispatcher.Invoke(new Action(() =>
                {
                    TransViewList.GetMainGrid().IsHitTestVisible = true;
                }));
            }).Start();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F1)
            {
                if (TransViewList.GetMainGrid().IsHitTestVisible)
                {
                    TransCurrentItem(null, null);
                }
            }
        }

        public void SendQuestionToAI(object Engine, string QuestionStr)
        {
            if (Engine is ChatGptApi)
            {
                var GetResult = (Engine as ChatGptApi).CallAI($"{QuestionStr} ,请用 {LanguageHelper.GetLanguageString(
                              DeFine.TargetLanguage
                              )} 回复,能有点猫娘的口吻吗可爱点的.");

                if (GetResult != null)
                {
                    if (GetResult.choices != null)
                    {
                        string CNStr = "";
                        if (GetResult.choices.Length > 0)
                        {
                            CNStr = GetResult.choices[0].message.content.Trim();
                        }
                        if (CNStr.Trim().Length > 0)
                        {
                            this.Dispatcher.Invoke(new Action(() =>
                            {
                                AILog.Text = CNStr;
                            }));
                        }
                        else
                        {
                            this.Dispatcher.Invoke(new Action(() =>
                            {
                                AILog.Text = string.Empty;
                            }));
                        }
                    }
                }
            }
            else
            if (Engine is DeepSeekApi)
            {
                var GetResult = (Engine as DeepSeekApi).CallAI($"{QuestionStr} ,请用 {LanguageHelper.GetLanguageString(
                              DeFine.TargetLanguage
                              )} 回复,能有点猫娘的口吻吗可爱点的.");

                if (GetResult != null)
                {
                    if (GetResult.choices != null)
                    {
                        string CNStr = "";
                        if (GetResult.choices.Length > 0)
                        {
                            CNStr = GetResult.choices[0].message.content.Trim();
                        }
                        if (CNStr.Trim().Length > 0)
                        {
                            this.Dispatcher.Invoke(new Action(() =>
                            {
                                AILog.Text = CNStr;
                            }));
                        }
                        else
                        {
                            this.Dispatcher.Invoke(new Action(() =>
                            {
                                AILog.Text = string.Empty;
                            }));
                        }
                    }
                }
            }
        }

        private void SendQuestionToAI(object sender, MouseButtonEventArgs e)
        {
            string GetSendAIText = SendAIText.Text;
            if (ConvertHelper.ObjToStr(SendQuestionBtn.Content).Equals("SendQuestion"))
            {
                new Thread(() =>
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        SendQuestionBtn.Content = "AI Thinking...";
                    }));

                    List<object> AllEngines = new List<object>();

                    if (DeFine.GlobalLocalSetting.ChatGptApiUsing && DeFine.GlobalLocalSetting.ChatGptKey.Trim().Length > 0)
                    {
                        AllEngines.Add(new ChatGptApi());
                    }
                    if (DeFine.GlobalLocalSetting.DeepSeekApiUsing && DeFine.GlobalLocalSetting.DeepSeekKey.Trim().Length > 0)
                    {
                        AllEngines.Add(new DeepSeekApi());
                    }

                    var GetText = SendAIText.Text;

                    if (AllEngines.Count == 1)
                    {
                        SendQuestionToAI(AllEngines[0], GetText);
                    }
                    else
                    if (AllEngines.Count > 0)
                    {
                        SendQuestionToAI(AllEngines[new Random(Guid.NewGuid().GetHashCode()).Next(0, AllEngines.Count)], GetText);
                    }

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        SendQuestionBtn.Content = "SendQuestion";
                    }));

                }).Start();
            }
        }

        private void OpenSetting(object sender, MouseButtonEventArgs e)
        {
            DeFine.ShowSetting();
        }

        private void DetectLang(object sender, MouseButtonEventArgs e)
        {
            var GetLang = WordProcess.DetectLanguage();
            DeFine.SourceLanguage = GetLang;
            Source.SelectedValue = GetLang.ToString();
        }

        private void ReplaceAllLine(object sender, MouseButtonEventArgs e)
        {
            if (ActionWin.Show("Confirm?", $"This will replace the contents of all rows. \"{ReplaceKey.Text}\"->\"{ReplaceValue.Text}\"", MsgAction.YesNo, MsgType.Info, 230) > 0)
            {
                WordProcess.ReplaceAllLine(ReplaceKey.Text.Trim(), ReplaceValue.Text.Trim());
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            AutoViewMode();
        }

        private void UsingDictionary_Click(object sender, RoutedEventArgs e)
        {
            if (LoadSaveState != 0)
            {
                MessageBox.Show("The stop dictionary cannot be enabled during the translation phase.");
                if (DeFine.GlobalLocalSetting.AutoLoadDictionaryFile)
                {
                    UsingDictionary.IsChecked = true;
                }
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
    }
}