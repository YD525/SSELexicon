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
using SSELex.SQLManager;
using PhoenixEngine.DataBaseManagement;

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
        public static string CurrentModName = "";

        public static int GlobalRequestTimeOut = 5000;
        public static int ViewMode = 0;

        public static SolidColorBrush DefBackGround = new SolidColorBrush(Color.FromRgb(11, 116, 209));
        public static SolidColorBrush SelectBackGround = new SolidColorBrush(Color.FromRgb(7,82,149));

        public static string PapyrusCompilerPath = "";

        public static int DefPageSize = 100;

        public static bool AutoTranslate = true;

        public static string BackupPath = @"\BackUpData\";

        public static string CurrentVersion = "2.7.52";
        public static LocalSetting GlobalLocalSetting = new LocalSetting();

        public static MainWindow WorkingWin = null;
        public static DashBoardView CurrentDashBoardView = null;
        public static CodeView CurrentCodeView = null;
        public static TextEditor ActiveIDE = null;
        public static LocalConfig LocalConfigView = null;

        public static SqlCore<SQLiteHelper> LocalDB = null;

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

        public static void Init(MainWindow Work)
        {
            MakeReady();
            GlobalLocalSetting.ReadConfig();
            string GetFilePath = GetFullPath(@"\System.db");
            DeFine.LocalDB = new SqlCore<SQLiteHelper>(GetFilePath);

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

            CurrentDashBoardView = new DashBoardView();
            CurrentDashBoardView.Hide();

            LocalConfigView = new LocalConfig();
            LocalConfigView.Hide();
            LocalConfigView.Init();
        }

        public static void ShowLogView()
        {
            if (DeFine.GlobalLocalSetting.ShowLog)
            {
                CurrentDashBoardView.Owner = DeFine.WorkingWin;
                CurrentDashBoardView.Left = DeFine.WorkingWin.Left + DeFine.WorkingWin.Width + 5;
                CurrentDashBoardView.Top = DeFine.WorkingWin.Top;

                CurrentDashBoardView.Show();
            }
        }
    }

    public class LocalSetting
    {
        public double FormHeight { get; set; } = 850;
        public double FormWidth { get; set; } = 1200;
        public Languages CurrentUILanguage { get; set; } = Languages.English;
        public string SkyrimPath { get; set; } = "";

        public bool PlaySound = false;     
        public bool ShowLog { get; set; } = true;
        public bool ShowCode { get; set; } = true;
        public bool AutoCompress { get; set; } = true;
        public SkyrimType SkyrimType { get; set; } = SkyrimType.SkyrimSE;
        public EncodingTypes FileEncoding { get; set; } = EncodingTypes.UTF8;
        public string ViewMode { get; set; } = "Normal";

        public bool AutoLoadDictionaryFile = false;

        public Languages SourceLanguage = Languages.Auto;
        public Languages TargetLanguage = Languages.English;

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
                        this.CurrentUILanguage = GetSetting.CurrentUILanguage;
                        this.SkyrimPath = GetSetting.SkyrimPath;
                        this.PlaySound = GetSetting.PlaySound;
                        this.ShowCode = GetSetting.ShowCode;
                        this.ShowLog = GetSetting.ShowLog;
                        this.SkyrimType = GetSetting.SkyrimType;
                        this.AutoCompress = GetSetting.AutoCompress;
                        this.FileEncoding = GetSetting.FileEncoding;
                        this.ViewMode = GetSetting.ViewMode;
                        this.AutoLoadDictionaryFile = GetSetting.AutoLoadDictionaryFile;
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

            EngineConfig.Save();
        }
    }
}
