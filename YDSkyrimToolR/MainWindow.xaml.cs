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
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YDSkyrimToolR.ConvertManager;
using YDSkyrimToolR.SkyrimManage;
using YDSkyrimToolR.TranslateCore;
using YDSkyrimToolR.TranslateManage;
using YDSkyrimToolR.UIManage;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static YDSkyrimToolR.UIManage.SkyrimDataLoader;
/*
* @Author: 约定
* @GitHub: https://github.com/tolove336/YDSkyrimToolR
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
            TransTargetType.Items.Clear();
            foreach (var GetType in CanSetSelecter)
            {
                TransTargetType.Items.Add(GetType.ToString());
            }

            TransTargetType.SelectedValue = ObjSelect.All.ToString();
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
            Source.Items.Add("English");
            Source.Items.Add("Chinese");
            Source.Items.Add("Japanese");
            Source.Items.Add("Korean");
            Source.Items.Add("German");
            Source.SelectedValue = DeFine.SourceLanguage.ToString();

            Target.Items.Clear();
            Target.Items.Add("Chinese");
            Target.Items.Add("English");
            Target.Items.Add("Japanese");
            Target.Items.Add("Korean");
            Target.Items.Add("German");
            Target.SelectedValue = DeFine.TargetLanguage.ToString();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DeFine.Init(this);
            ReloadViewMode();
            ReloadLanguageMode();
            this.Topmost = true;
            this.Show();
            this.Activate();

            new Thread(() =>
            {
                ShowFrameByTag("LoadingView");

                LocalTrans.Init();

                CheckEngineState();

                GlobalEspReader = new EspReader();
                GlobalMCMReader = new MCMReader();
                GlobalPexReader = new PexReader();

                Thread.Sleep(1000);

                //Frist Check ToolPath
                if (!File.Exists(DeFine.GetFullPath(@"Tool\Champollion.exe")))
                {
                    ActionWin.Show("Lack of pre-processing", "Please manually install the dependent program\nhttps://github.com/Orvid/Champollion\nPlease download the release version and put it in this path\n"+ DeFine.GetFullPath(@"Tool\")+ "\n Path required\n" + DeFine.GetFullPath(@"Tool\Champollion.exe"), MsgAction.Yes,MsgType.Info,390);
                }

  
                string GetPapyrusAssemblerPath = DeFine.GetFullPath(@"Tool\") + @"Data\Processors\CreationKit\PapyrusAssembler.exe";

                if (!File.Exists(GetPapyrusAssemblerPath))
                {
                    ActionWin.Show("Lack of pre-processing", @"Please Download CK Url: http://www.creationkit.com And Copy Papyrus Compiler\*.*  directory To" + DeFine.GetFullPath(@"Tool\") + @"Data\Processors\CreationKit\" + "\n"
                        + "Path required\n" + GetPapyrusAssemblerPath, MsgAction.Yes, MsgType.Info, 580);
                }

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

                    DeFine.DefTransTool.Show();
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
            if (System.IO.File.Exists(FilePath))
            {
                string GetFileName = FilePath.Substring(FilePath.LastIndexOf(@"\") + @"\".Length);
                Caption.Content = GetFileName;
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
                        CancelTransBtn.Opacity = 1;
                        CancelTransBtn.IsEnabled = true;
                        LoadBtnContent.Content = "SaveFile";
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
                        CancelTransBtn.Opacity = 1;
                        CancelTransBtn.IsEnabled = true;
                        LoadBtnContent.Content = "SaveFile";
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
                        CancelTransBtn.Opacity = 1;
                        CancelTransBtn.IsEnabled = true;
                        LoadBtnContent.Content = "SaveFile";
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
                    Footer.Background = SelectGrid.Background;
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
                        string GetContent = ConvertHelper.ObjToStr((GetEngine as SvgViewbox).ToolTip);

                        switch (GetContent)
                        {
                            case "本地词组引擎":
                                {
                                    if (!DeFine.GlobalLocalSetting.PhraseEngineUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.1;
                                    }
                                }
                                break;
                            case "代码识别引擎":
                                {
                                    if (!DeFine.GlobalLocalSetting.CodeParsingEngineUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.1;
                                    }
                                }
                                break;
                            case "本地连词引擎":
                                {
                                    if (!DeFine.GlobalLocalSetting.ConjunctionEngineUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.1;
                                    }
                                }
                                break;
                            case "百度云翻译":
                                {
                                    if (!DeFine.GlobalLocalSetting.BaiDuYunApiUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.1;
                                    }
                                }
                                break;
                            case "谷歌云翻译":
                                {
                                    if (!DeFine.GlobalLocalSetting.GoogleYunApiUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.1;
                                    }
                                }
                                break;
                            case "DeepSeekAI翻译":
                                {
                                    if (!DeFine.GlobalLocalSetting.DeepSeekApiUsing)
                                    {
                                        (GetEngine as SvgViewbox).Opacity = 0.1;
                                    }
                                }
                                break;
                            case "自定义引擎":
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
            string GetContent = ConvertHelper.ObjToStr((sender as SvgViewbox).ToolTip);

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
                case "本地词组引擎":
                    {
                        DeFine.GlobalLocalSetting.PhraseEngineUsing = OneState;
                    }
                    break;
                case "代码识别引擎":
                    {
                        MessageBox.Show("为了确保不会翻译到代码禁止禁用!");
                        (sender as SvgViewbox).Opacity = 1;
                        //DeFine.CodeParsingEngineUsing = OneState;
                    }
                    break;
                case "本地连词引擎":
                    {
                        DeFine.GlobalLocalSetting.ConjunctionEngineUsing = OneState;
                    }
                    break;
                case "百度云翻译":
                    {
                        DeFine.GlobalLocalSetting.BaiDuYunApiUsing = OneState;
                    }
                    break;
                case "谷歌云翻译":
                    {
                        DeFine.GlobalLocalSetting.GoogleYunApiUsing = OneState;
                    }
                    break;
                case "DeepSeekAI翻译":
                    {
                        DeFine.GlobalLocalSetting.DeepSeekApiUsing = OneState;
                    }
                    break;
                case "自定义引擎":
                    {
                        DeFine.GlobalLocalSetting.DivCacheEngineUsing = OneState;
                    }
                    break;
            }
        }


        private void ChangeTransProcessState(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border || sender is SvgViewbox)
            {
                if (Translator.StopAny == true)
                {
                    ProcessStateBackGround.Background = new SolidColorBrush(Color.FromRgb(247, 137, 178));

                    AutoKeep.Source = new Uri("pack://application:,,,/YDSkyrimToolR;component/Material/Stop.svg"); ;
                    Translator.StopAny = false;
                }
                else
                {
                    ProcessStateBackGround.Background = new SolidColorBrush(Color.FromRgb(147, 85, 108));

                    AutoKeep.Source = new Uri("pack://application:,,,/YDSkyrimToolR;component/Material/Keep.svg"); ;
                    Translator.StopAny = true;
                }
                //.Source = ;
                ////.Source = new BitmapImage(new Uri("/Material/Setting.png", UriKind.Relative));
            }
        }

        private void AutoLoadOrSave(object sender, MouseButtonEventArgs e)
        {
            string GetButtonContent = "";
            if (sender is Border)
            {
                GetButtonContent = ConvertHelper.ObjToStr(((sender as Border).Child as Label).Content);
            }
            if (sender is Label)
            {
                GetButtonContent = ConvertHelper.ObjToStr((sender as Label).Content);
            }

            string SetButtonContent = "";

            if (GetButtonContent.Equals("LoadFile"))
            {
                LoadAny();
            }
            else
            {
                Caption.Name = "TranslationCore";
                CancelTransBtn.Opacity = 0.3;
                CancelTransBtn.IsEnabled = false;

                string GetFilePath = LastSetPath.Substring(0, LastSetPath.LastIndexOf(@"\")) + @"\";
                string GetFileFullName = LastSetPath.Substring(LastSetPath.LastIndexOf(@"\") + @"\".Length);
                string GetFileSuffix = GetFileFullName.Split('.')[1];
                string GetFileName = GetFileFullName.Split('.')[0];

                if (CurrentTransType == 3)
                {
                    if (UIHelper.ModifyCount > 0)
                        if (GlobalPexReader != null)
                        {
                            string GetBackUPPath = GetFilePath + GetFileFullName + ".backup";
                            if (File.Exists(GetBackUPPath))
                            {
                                File.Delete(GetBackUPPath);
                            }
                            File.Copy(LastSetPath, GetBackUPPath);
                            File.Delete(LastSetPath);

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

                CancelTransEsp(null, null);
                SetButtonContent = "LoadFile";
            }

            if (sender is Border)
            {
                ((sender as Border).Child as Label).Content = SetButtonContent;
            }
            if (sender is Label)
            {
                (sender as Label).Content = SetButtonContent;
            }
        }

        private void CancelTransEsp(object sender, MouseButtonEventArgs e)
        {
            Caption.Name = "TranslationCore";

            TransViewList.Clear();
            GlobalEspReader.Close();
            GlobalMCMReader.Close();
            GlobalPexReader.Close();

            (StartTransBtn.Child as Label).Content = "LoadFile";
            CancelTransBtn.Opacity = 0.3;
            CancelTransBtn.IsEnabled = false;
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
            if (DeFine.DefTransTool != null)
            {
                DeFine.DefTransTool.Top = this.Top + this.Height;
                DeFine.DefTransTool.Width = this.Width;
                DeFine.DefTransTool.Left = this.Left;
            }
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
            switch (GetValue)
            {
                case "English":
                    {
                        DeFine.SourceLanguage = TranslateCore.Languages.English;
                    }
                    break;
                case "Chinese":
                    {
                        DeFine.SourceLanguage = TranslateCore.Languages.Chinese;
                    }
                    break;
                case "Japanese":
                    {
                        DeFine.SourceLanguage = TranslateCore.Languages.Japanese;
                    }
                    break;
                case "German":
                    {
                        DeFine.SourceLanguage = TranslateCore.Languages.German;
                    }
                    break;
                case "Korean":
                    {
                        DeFine.SourceLanguage = TranslateCore.Languages.Korean;
                    }
                    break;
            }
            DeFine.GlobalLocalSetting.SourceLanguage = DeFine.SourceLanguage;
        }

        private void Target_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string GetValue = ConvertHelper.ObjToStr(Target.SelectedValue);
            switch (GetValue)
            {
                case "English":
                    {
                        DeFine.TargetLanguage = TranslateCore.Languages.English;
                    }
                    break;
                case "Chinese":
                    {
                        DeFine.TargetLanguage = TranslateCore.Languages.Chinese;
                    }
                    break;
                case "Japanese":
                    {
                        DeFine.TargetLanguage = TranslateCore.Languages.Japanese;
                    }
                    break;
                case "German":
                    {
                        DeFine.TargetLanguage = TranslateCore.Languages.German;
                    }
                    break;
                case "Korean":
                    {
                        DeFine.TargetLanguage = TranslateCore.Languages.Korean;
                    }
                    break;
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

        private void ViewModeChange(object sender, SelectionChangedEventArgs e)
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

                    string To = "";
                    if (DeFine.TargetLanguage == Languages.English)
                    {
                        To = "英文";
                    }
                    if (DeFine.TargetLanguage == Languages.Chinese)
                    {
                        To = "中文";
                    }
                    if (DeFine.TargetLanguage == Languages.Japanese)
                    {
                        To = "日文";
                    }
                    if (DeFine.TargetLanguage == Languages.German)
                    {
                        To = "德文";
                    }
                    if (DeFine.TargetLanguage == Languages.Korean)
                    {
                        To = "韩文";
                    }

                    var GetResult = new DeepSeekApi().CallAI($"{GetSendAIText} ,请用 {To} 回复,能有点猫娘的口吻吗可爱点的.");

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

                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        SendQuestionBtn.Content = "SendQuestion";
                    }));

                }).Start();
            }
        }
    }
}