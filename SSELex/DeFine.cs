using ICSharpCode.AvalonEdit;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Media;
using SSELex.SkyrimModManager;
using System.Windows.Threading;
using System.Windows;
using static SSELex.SkyrimManage.EspReader;
using PhoenixEngine.TranslateCore;
using PhoenixEngine.EngineManagement;
using SSELex.UIManagement;
using SSELex.FileManagement;

namespace SSELex
{
    public enum GameNames
    {
        SkyrimLE = 0, SkyrimSE = 1
    }
    public class DeFine
    {
        public static bool CanUpdateChart = false;
        public static int GlobalRequestTimeOut = 5000;
        public static int ViewMode = 0;

        public static SolidColorBrush DefBackGround = new SolidColorBrush(Color.FromRgb(11, 116, 209));
        public static SolidColorBrush SelectBackGround = new SolidColorBrush(Color.FromRgb(7, 82, 149));

        public static string PapyrusCompilerPath = "";

        public static int DefPageSize = 100;

        public static bool AutoTranslate = true;

        public static string BackupPath = @"\BackUpData\";

        public static string CurrentVersion = "3.1.3.3";
        public static LocalSetting GlobalLocalSetting = new LocalSetting();

        public static MainGui WorkingWin = null;
        public static CodeView CurrentCodeView = new CodeView();
        public static ReplaceWin CurrentReplaceView = new ReplaceWin();
        public static TextEditor ActiveIDE = null;
        public static LocalConfig LocalConfigView = null;
        public static RowStyleWin RowStyleWin = new RowStyleWin();
        public static ExtendWin ExtendWin = null;

        public static void CloseAny()
        {
            if (WorkingWin != null)
            {
                WorkingWin.Hide();
            }

            if (CurrentCodeView != null)
            {
                CurrentCodeView.Dispatcher.Invoke(new Action(() =>
                {
                    CurrentCodeView.Hide();
                }));
            }

            EngineConfig.Save();
            DeFine.GlobalLocalSetting.SaveConfig();
            Environment.Exit(0);
        }

        public static void HideCodeView()
        {
            CurrentCodeView.Dispatcher.Invoke(new Action(() =>
            {
                CurrentCodeView.Hide();
            }));
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
                CreatNewLocalSetting.SaveConfig();
            }
        }

        public static void ShowExtendWin()
        {
            ExtendWin.ShowUI();
        }

        public static void CloseExtendWin()
        {
            ExtendWin.CloseUI();
        }

        public static void Init(MainGui Work)
        {
            if (ToolDownloader.SCanToolPath() == null)
            {
                CloseAny();
            }

            CurrentReplaceView.Owner = Work;
            CurrentReplaceView.Hide();
            RowStyleWin.Hide();

            MakeReady();

            string GetFilePath = GetFullPath(@"\System.db");

            WorkingWin = Work;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            Engine.Init();

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

            LocalConfigView = new LocalConfig();
            LocalConfigView.Hide();
            LocalConfigView.Init();

            ExtendWin = new ExtendWin();
        }
    }

    public class LocalSetting
    {
        public int Style { get; set; } = 1;
        public double FormHeight { get; set; } = 850;
        public double FormWidth { get; set; } = 1200;
        public Languages CurrentUILanguage { get; set; } = Languages.English;
        public string SkyrimPath { get; set; } = "";

        public bool ShowCode { get; set; } = true;
        public bool AutoCompress { get; set; } = true;
        public GameNames GameType { get; set; } = GameNames.SkyrimSE;
        public EncodingTypes FileEncoding { get; set; } = EncodingTypes.UTF8;

        public double WritingAreaHeight { get; set; } = 0;
        public string ViewMode { get; set; } = "Normal";

        public Languages SourceLanguage { get; set; } = Languages.Auto;
        public Languages TargetLanguage { get; set; } = Languages.English;

        public bool CanClearCloudTranslationCache { get; set; } = false;
        public bool CanClearUserInputTranslationCache { get; set; } = false;

        public bool AutoSpeak { get; set; } = false;

        public int ChatGPTTokenUsage { get; set; } = 0;
        public int GeminiTokenUsage { get; set; } = 0;
        public int CohereTokenUsage { get; set; } = 0;
        public int DeepSeekTokenUsage { get; set; } = 0;
        public int BaichuanTokenUsage { get; set; } = 0;
        public int LocalAITokenUsage { get; set; } = 0;
        public bool EnableLanguageDetect { get; set; } = true;
        public void ReadConfig()
        {
            try
            {
                if (File.Exists(DeFine.GetFullPath(@"\setting.config")))
                {
                    var GetStr = Encoding.UTF8.GetString(DataHelper.ReadFile(DeFine.GetFullPath(@"\setting.config")));
                    if (GetStr.Trim().Length > 0)
                    {
                        var GetSetting = JsonSerializer.Deserialize<LocalSetting>(GetStr);
                        if (GetSetting != null)
                        {
                            this.Style = GetSetting.Style;
                            this.FormHeight = GetSetting.FormHeight;
                            this.FormWidth = GetSetting.FormWidth;
                            this.CurrentUILanguage = GetSetting.CurrentUILanguage;
                            this.SkyrimPath = GetSetting.SkyrimPath;
                            this.ShowCode = GetSetting.ShowCode;
                            this.GameType = GetSetting.GameType;
                            this.AutoCompress = GetSetting.AutoCompress;
                            this.FileEncoding = GetSetting.FileEncoding;
                            this.WritingAreaHeight = GetSetting.WritingAreaHeight;
                            this.ViewMode = GetSetting.ViewMode;
                            this.SourceLanguage = GetSetting.SourceLanguage;
                            this.TargetLanguage = GetSetting.TargetLanguage;
                            this.CanClearCloudTranslationCache = GetSetting.CanClearCloudTranslationCache;
                            this.CanClearUserInputTranslationCache = GetSetting.CanClearUserInputTranslationCache;
                            this.AutoSpeak = GetSetting.AutoSpeak;

                            this.ChatGPTTokenUsage = GetSetting.ChatGPTTokenUsage;
                            this.GeminiTokenUsage = GetSetting.GeminiTokenUsage;
                            this.CohereTokenUsage = GetSetting.CohereTokenUsage;
                            this.DeepSeekTokenUsage = GetSetting.DeepSeekTokenUsage;
                            this.BaichuanTokenUsage = GetSetting.BaichuanTokenUsage;
                            this.LocalAITokenUsage = GetSetting.LocalAITokenUsage;

                            this.EnableLanguageDetect = GetSetting.EnableLanguageDetect;
                        }
                    }
                    else
                    {
                        LocalSetting CopySetting = this;
                        var GetSettingContent = JsonSerializer.Serialize(CopySetting);
                        DataHelper.WriteFile(DeFine.GetFullPath(@"\setting.config"), Encoding.UTF8.GetBytes(GetSettingContent));
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

            DataHelper.WriteFile(DeFine.GetFullPath(@"\setting.config"), Encoding.UTF8.GetBytes(GetSettingContent));

            EngineConfig.Save();
        }
    }
}
