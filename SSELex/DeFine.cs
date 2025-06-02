using ICSharpCode.AvalonEdit;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media;
using SSELex.SkyrimModManager;
using SSELex.SQLManager;
using SSELex.TranslateCore;
using System.Windows.Threading;
using System.Windows;
using SSELex.TranslateManage;
using static SSELex.SkyrimManage.EspReader;

namespace SSELex
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public enum SkyrimType
    { 
       SkyrimLE = 0, SkyrimSE = 1
    }
    public class DeFine
    {
        public static int GlobalRequestTimeOut = 10000;
        public static string CurrentModName = "";
        public static int ViewMode = 0;

        public static SolidColorBrush DefBackGround = new SolidColorBrush(Color.FromRgb(11, 116, 209));
        public static SolidColorBrush SelectBackGround = new SolidColorBrush(Color.FromRgb(7,82,149));

        public static string PapyrusCompilerPath = "";

        public static Languages SourceLanguage = Languages.English;
        public static Languages TargetLanguage = Languages.English;

        public static int DefPageSize = 100;

        public static bool AutoTranslate = true;

        public static string BackupPath = @"\BackUpData\";

        public static string CurrentVersion = "2.6.36";
        public static LocalSetting GlobalLocalSetting = new LocalSetting();

        public static MainWindow WorkingWin = null;
        public static LogView CurrentLogView = null;
        public static SqlCore<SQLiteHelper> GlobalDB = null;
        public static CodeView CurrentCodeView = null;
        public static TextEditor ActiveIDE = null;

        public static string CurrentSearchStr = "";

        public static void CloseAny()
        {
            DeFine.GlobalLocalSetting.SaveConfig();
            Environment.Exit(0);
        }

        public static void HideCodeView()
        {
            CurrentCodeView.Dispatcher.Invoke(new Action(() => {
                CurrentCodeView.Hide();
            }));
        }

        public static void ShowCodeView()
        {
            CurrentCodeView.Dispatcher.Invoke(new Action(() => {
                CurrentCodeView.Show();
            }));

            if (DeFine.GlobalLocalSetting.ShowCode)
            {
                if (DeFine.WorkingWin != null)
                {
                    DeFine.WorkingWin.SyncCodeViewLocation();
                }
                CurrentCodeView.Dispatcher.Invoke(new Action(() => {
                    CurrentCodeView.Opacity = 1;
                    CurrentCodeView.IsHitTestVisible = true;
                }));
            }
            else
            {
                CurrentCodeView.Dispatcher.Invoke(new Action(() => {
                    CurrentCodeView.Opacity = 0;
                    CurrentCodeView.IsHitTestVisible = false;
                }));
            }
        }

        public static string GetFullPath(string Path)
        {
            string GetShellPath = System.Windows.Forms.Application.StartupPath;
            if (GetShellPath.EndsWith(@"\"))
            {
                if (Path.StartsWith(@"\"))
                {
                    Path = Path.Substring(1);
                }
            }
            return GetShellPath + Path;
        }

        public static void MakeReady()
        {
            var HasWriteAccess = new DirectoryInfo(DeFine.GetFullPath(@"\")).GetAccessControl().AreAccessRulesProtected == false;
            if (!HasWriteAccess)
            {
                MessageBox.Show("The current path does not have write permission. Please move to another path Or right-click to run as administrator.");
                DeFine.CloseAny();
            }

            if (!File.Exists(DeFine.GetFullPath(@"\system.db")))
            {
                File.Copy(DeFine.GetFullPath(@"LocalDB\system.db"), DeFine.GetFullPath(@"\system.db"));
            }
            if (!Directory.Exists(DeFine.GetFullPath(@"\Librarys")))
            {
                Directory.CreateDirectory(DeFine.GetFullPath(@"\Librarys"));
            }
            if (!Directory.Exists(DeFine.GetFullPath(@"\Cache")))
            {
                Directory.CreateDirectory(DeFine.GetFullPath(@"\Cache"));
            }
            if (!File.Exists(DeFine.GetFullPath(@"\setting.config")))
            {
                var CreatNewLocalSetting = new LocalSetting();
                CreatNewLocalSetting.ChatGptApiUsing = true;
                CreatNewLocalSetting.MaxThreadCount = 2;
                CreatNewLocalSetting.UsingContext = true;
                CreatNewLocalSetting.AutoLoadDictionaryFile = true;
                CreatNewLocalSetting.SaveConfig();
            }
        }

        public static void Init(MainWindow Work)
        {
            MakeReady();
            GlobalLocalSetting.ReadConfig();
            WorkingWin = Work;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            GlobalDB = new SqlCore<SQLiteHelper>(DeFine.GetFullPath(@"\system.db"));

            Thread NewWindowThread = new Thread(() =>
            {
                Dispatcher NewDispatcher = Dispatcher.CurrentDispatcher;

                NewDispatcher.Invoke(() =>
                {
                    CurrentCodeView = new CodeView();
                    CurrentCodeView.Hide();
                });
                Dispatcher.Run();
            });

            NewWindowThread.SetApartmentState(ApartmentState.STA);
            NewWindowThread.Start();

            CurrentLogView = new LogView();
            CurrentLogView.Hide();
        }

        public static void ShowLogView()
        {
            if (DeFine.GlobalLocalSetting.ShowLog)
            {
                CurrentLogView.Owner = DeFine.WorkingWin;
                CurrentLogView.Left = DeFine.WorkingWin.Left + DeFine.WorkingWin.Width + 5;
                CurrentLogView.Top = DeFine.WorkingWin.Top;

                CurrentLogView.Show();
            }
        }

        public static void LoadData()
        {
            TransCore.Init();
        }
    }

    public class LocalSetting
    {
        public double FormHeight { get; set; } = 850;
        public double FormWidth { get; set; } = 1200;
        public bool PhraseEngineUsing { get; set; } = false;
        public bool CodeParsingEngineUsing { get; set; } = true;
        public bool ConjunctionEngineUsing { get; set; } = false;
        public bool BaiDuYunApiUsing { get; set; } = false;
        public bool ChatGptApiUsing { get; set; } = false;
        public bool GeminiApiUsing { get; set; } = false;
        public bool DeepSeekApiUsing { get; set; } = false;
        public bool GoogleYunApiUsing { get; set; } = false;
        public bool DivCacheEngineUsing { get; set; } = false;
        public bool DeepLApiUsing { get; set; } = false;
        public Languages SourceLanguage { get; set; } = Languages.Auto;
        public Languages TargetLanguage { get; set; } = Languages.English;
        public Languages CurrentUILanguage { get; set; } = Languages.English;
        public string BackUpPath { get; set; } = "";
        public string APath { get; set; } = "";
        public string BPath { get; set; } = "";
        public string SkyrimPath { get; set; } = "";
        public bool PlaySound = false;
        public string GoogleApiKey { get; set; } = "";
        public string BaiDuAppID { get; set; } = "";
        public string BaiDuSecretKey { get; set; } = "";
        public string ChatGptKey { get; set; } = "";
        public string ChatGptModel { get; set; } = "gpt-4o-mini";
        public string GeminiKey { get; set; } = "";
        public string GeminiModel { get; set; } = "gemini-2.0-flash";
        public string DeepSeekKey { get; set; } = "";
        public string DeepSeekModel{ get; set; } = "deepseek-chat";
        public string DeepLKey { get; set; } = "";
        public string UserCustomAIPrompt { get; set; } = "";
        public bool IsFreeDeepL { get; set; } = true;
        public int ContextLimit { get; set; } = 3;
        public string ProxyIP { get; set; } = "";
        public int TransCount { get; set; } = 0;
        public int MaxThreadCount { get; set; } = 0;
        public bool AutoSetThreadLimit { get; set; } = true;
        public bool AutoLoadDictionaryFile { get; set; } = false;
        public bool UsingContext { get; set; } = true;
        public bool ShowLog { get; set; } = true;
        public bool ShowCode { get; set; } = true;
        public bool AutoCompress { get; set; } = true;
        public SkyrimType SkyrimType { get; set; } = SkyrimType.SkyrimSE;
        public EncodingTypes FileEncoding { get; set; } = EncodingTypes.UTF8_1256;
        public string ViewMode { get; set; } = "Normal";

        public void ReadConfig()
        {
            try { 
            if (File.Exists(DeFine.GetFullPath(@"\setting.config")))
            {
                var GetStr = FileHelper.ReadFileByStr(DeFine.GetFullPath(@"\setting.config"), Encoding.UTF8);
                if (GetStr.Trim().Length > 0)
                {
                    var GetSetting = JsonSerializer.Deserialize<LocalSetting>(GetStr);
                    if (GetSetting != null)
                    {
                        this.FormHeight = GetSetting.FormHeight;
                        this.FormWidth = GetSetting.FormWidth;
                        this.MaxThreadCount = GetSetting.MaxThreadCount;
                        this.AutoSetThreadLimit = GetSetting.AutoSetThreadLimit;
                        this.AutoLoadDictionaryFile = GetSetting.AutoLoadDictionaryFile;
                        this.PhraseEngineUsing = GetSetting.PhraseEngineUsing;
                        this.CodeParsingEngineUsing = GetSetting.CodeParsingEngineUsing;
                        this.ConjunctionEngineUsing = GetSetting.ConjunctionEngineUsing;
                        this.BaiDuYunApiUsing = GetSetting.BaiDuYunApiUsing;
                        this.ChatGptApiUsing = GetSetting.ChatGptApiUsing;
                        this.GeminiApiUsing = GetSetting.GeminiApiUsing;
                        this.DeepSeekApiUsing = GetSetting.DeepSeekApiUsing;
                        this.DeepLApiUsing = GetSetting.DeepLApiUsing;
                        this.GoogleYunApiUsing = GetSetting.GoogleYunApiUsing;
                        this.DivCacheEngineUsing = GetSetting.DivCacheEngineUsing;
                        this.ContextLimit = GetSetting.ContextLimit;
                        this.SourceLanguage = GetSetting.SourceLanguage;
                        DeFine.SourceLanguage = GetSetting.SourceLanguage;
                        this.TargetLanguage = GetSetting.TargetLanguage;
                        DeFine.TargetLanguage = GetSetting.TargetLanguage;
                        this.CurrentUILanguage = GetSetting.CurrentUILanguage;
                        this.APath = GetSetting.APath;
                        this.BPath = GetSetting.BPath;
                        this.SkyrimPath = GetSetting.SkyrimPath;
                        this.GoogleApiKey = GetSetting.GoogleApiKey;
                        this.BaiDuAppID = GetSetting.BaiDuAppID;
                        this.BaiDuSecretKey = GetSetting.BaiDuSecretKey;
                        this.ChatGptKey = GetSetting.ChatGptKey;
                        this.ChatGptModel = GetSetting.ChatGptModel;
                        this.GeminiKey = GetSetting.GeminiKey;
                        this.GeminiModel = GetSetting.GeminiModel;
                        this.DeepSeekKey = GetSetting.DeepSeekKey;
                        this.DeepSeekModel = GetSetting.DeepSeekModel;
                        this.DeepLKey = GetSetting.DeepLKey;
                        this.IsFreeDeepL = GetSetting.IsFreeDeepL;
                        this.UserCustomAIPrompt = GetSetting.UserCustomAIPrompt;
                        this.ProxyIP = GetSetting.ProxyIP;
                        this.TransCount = GetSetting.TransCount;
                        this.PlaySound = GetSetting.PlaySound;
                        this.BackUpPath = GetSetting.BackUpPath;
                        this.UsingContext = GetSetting.UsingContext;
                        this.ShowCode = GetSetting.ShowCode;
                        this.ShowLog = GetSetting.ShowLog;
                        this.SkyrimType = GetSetting.SkyrimType;
                        this.AutoCompress = GetSetting.AutoCompress;
                        this.FileEncoding = GetSetting.FileEncoding;
                        this.ViewMode = GetSetting.ViewMode;
                    }
                }
                else
                {
                    LocalSetting CopySetting = this;
                    var GetSettingContent = JsonSerializer.Serialize(CopySetting);

                    FileHelper.WriteFile(DeFine.GetFullPath(@"\setting.config"), GetSettingContent, Encoding.UTF8);
                }
            }
            }
            catch { }
        }

        public void SaveConfig()
        {
            var Options = new JsonSerializerOptions { WriteIndented = true };

            LocalSetting CopySetting = this;
            var GetSettingContent = JsonSerializer.Serialize(CopySetting, Options);

            FileHelper.WriteFile(DeFine.GetFullPath(@"\setting.config"), GetSettingContent, Encoding.UTF8);
        }
    }
}
