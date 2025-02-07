using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using YDSkyrimToolR.SkyrimModManager;
using YDSkyrimToolR.SQLManager;
using YDSkyrimToolR.TranslateCore;
using YDSkyrimTools.SkyrimModManager;

namespace YDSkyrimToolR
{
    /*
    * @Author: 约定
    * @GitHub: https://github.com/tolove336/YDSkyrimToolR
    * @Date: 2025-02-06
    */
    public class DeFine
    {
        public static int DefPageSize = 100;

        public static bool PhraseEngineUsing = true;//词组引擎
        public static bool CodeParsingEngineUsing = true;//代码处理引擎
        public static bool ConjunctionEngineUsing = true;//连词引擎
        public static bool BaiDuYunApiUsing = true;//百度翻译引擎
        public static bool ICIBAApiUsing = false;//爱词霸翻译引擎
        public static bool GoogleYunApiUsing = true;//谷歌翻译引擎
        public static bool DivCacheEngineUsing = true;//自定义内存翻译引擎(一次性)
        public static bool AutoTranslate = true;//始终关闭自动翻译

        public static string BackupPath = @"\BackUpData\";//自动备份路径

        public static string CurrentVersion = "3.1.1 Alpha";
        public static LocalSetting GlobalLocalSetting = new LocalSetting();

        public static MainWindow WorkingWin = null;
        public static SqlCore<SQLiteHelper> GlobalDB = null;
        public static CodeView CurrentCodeView = null;
        public static TextEditor ActiveIDE = null;
        public static TransTool DefTransTool = null;
        public static WordProcess WordProcessEngine = null;

        public static string GetFullPath(string Path)
        {
            string GetShellPath = System.Windows.Forms.Application.StartupPath;
            return GetShellPath + Path;
        }

        public static void Init(MainWindow Work)
        {
            LanguageHelper.Init();
            GlobalLocalSetting.ReadConfig();
            WorkingWin = Work;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            GlobalDB = new SqlCore<SQLiteHelper>(DeFine.GetFullPath(@"\system.db"));
            CurrentCodeView = new CodeView();
            DefTransTool = new TransTool();
            WordProcessEngine = new WordProcess();
            ConjunctionHelper.Init();
            DefTransTool.Hide();
            CurrentCodeView.Hide();
        }
    }

    public class LocalSetting
    {
        public string BackUpPath { get; set; } = "";
        public string APath { get; set; } = "";
        public string BPath { get; set; } = "";
        public string SkyrimPath { get; set; } = "";
        public MoConfigItem ModOrganizerConfig { get; set; } = new MoConfigItem();
        public bool PlaySound = false;
        public string GoogleKey { get; set; } = "";
        public string BaiDuAppID { get; set; } = "";
        public string BaiDuSecretKey { get; set; } = "";
        public int TransCount { get; set; } = 0;
        public void ReadConfig()
        {
            if (File.Exists(DeFine.GetFullPath(@"\setting.config")))
            {
                var GetStr = FileHelper.ReadFileByStr(DeFine.GetFullPath(@"\setting.config"), Encoding.UTF8);
                if (GetStr.Trim().Length > 0)
                {
                    var GetSetting =JsonSerializer.Deserialize<LocalSetting>(GetStr);

                    this.APath = GetSetting.APath;
                    this.BPath = GetSetting.BPath;
                    this.SkyrimPath = GetSetting.SkyrimPath;
                    this.GoogleKey = GetSetting.GoogleKey;
                    this.BaiDuAppID = GetSetting.BaiDuAppID;
                    this.BaiDuSecretKey = GetSetting.BaiDuSecretKey;
                    this.TransCount = GetSetting.TransCount;
                    this.ModOrganizerConfig = GetSetting.ModOrganizerConfig;
                    this.PlaySound = GetSetting.PlaySound;
                    this.BackUpPath = GetSetting.BackUpPath;
                }
                else
                {
                    LocalSetting CopySetting = this;
                    var GetSettingContent = JsonSerializer.Serialize(CopySetting);

                    FileHelper.WriteFile(DeFine.GetFullPath(@"\setting.config"), GetSettingContent, Encoding.UTF8);
                }
            }
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
