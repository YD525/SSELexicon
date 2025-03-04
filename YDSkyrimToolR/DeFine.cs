using ICSharpCode.AvalonEdit;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Security.RightsManagement;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media;
using YDSkyrimToolR.SkyrimModManager;
using YDSkyrimToolR.SQLManager;
using YDSkyrimToolR.TranslateCore;
using YDSkyrimTools.SkyrimModManager;

namespace YDSkyrimToolR
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class DeFine
    {
        public static SolidColorBrush DefBackGround = new SolidColorBrush(Color.FromRgb(11, 116, 209));
        public static SolidColorBrush SelectBackGround = new SolidColorBrush(Color.FromRgb(7,82,149));

        public static string PapyrusCompilerPath = "";

        public static Languages SourceLanguage = Languages.English;
        public static Languages TargetLanguage = Languages.Chinese;

        public static int DefPageSize = 100;

        public static bool AutoTranslate = true;//始终关闭自动翻译

        public static string BackupPath = @"\BackUpData\";//自动备份路径

        public static string CurrentVersion = "3.1.1 Alpha";
        public static LocalSetting GlobalLocalSetting = new LocalSetting();

        public static MainWindow WorkingWin = null;
        public static SettingView CurrentSettingView = null;
        public static SqlCore<SQLiteHelper> GlobalDB = null;
        public static CodeView CurrentCodeView = null;
        public static TextEditor ActiveIDE = null;
        public static WordProcess WordProcessEngine = null;

        public static void ShowSetting()
        {
            if (DeFine.WorkingWin != null)
            {
                if (CurrentSettingView == null)
                {
                    CurrentSettingView = new SettingView();
                }

                CurrentSettingView.Owner = DeFine.WorkingWin;
                CurrentSettingView.Show();
            }
        }

        public static string GetFullPath(string Path)
        {
            string GetShellPath = System.Windows.Forms.Application.StartupPath;
            return GetShellPath + Path;
        }

        public static void Init(MainWindow Work)
        {
            GlobalLocalSetting.ReadConfig();
            WorkingWin = Work;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            GlobalDB = new SqlCore<SQLiteHelper>(DeFine.GetFullPath(@"\system.db"));
            CurrentCodeView = new CodeView();
            WordProcessEngine = new WordProcess();
            CurrentCodeView.Hide();
        }

        public static void LoadData()
        {
            LanguageHelper.Init();
            ConjunctionHelper.Init();
        }
    }

    public class LocalSetting
    {
        public bool PhraseEngineUsing { get; set; } = true;//词组引擎
        public bool CodeParsingEngineUsing { get; set; } = true;//代码处理引擎
        public bool ConjunctionEngineUsing { get; set; } = true;//连词引擎
        public bool BaiDuYunApiUsing { get; set; } = true;//百度翻译引擎
        public bool ChatGptApiUsing { get; set; } = true;//ChatGpt机能搭载
        public bool DeepSeekApiUsing { get; set; } = true;//DeepSeek机能搭载
        public bool GoogleYunApiUsing { get; set; } = true;//谷歌翻译引擎
        public bool DivCacheEngineUsing { get; set; } = true;//自定义内存翻译引擎(一次性)
        public Languages SourceLanguage { get; set; } = Languages.English;
        public Languages TargetLanguage { get; set; } = Languages.Chinese;
        public Languages CurrentUILanguage { get; set; } = Languages.English;
        public string BackUpPath { get; set; } = "";
        public string APath { get; set; } = "";
        public string BPath { get; set; } = "";
        public string SkyrimPath { get; set; } = "";
        public bool PlaySound = false;
        public string GoogleKey { get; set; } = "";
        public string BaiDuAppID { get; set; } = "";
        public string BaiDuSecretKey { get; set; } = "";
        public string ChatGptKey { get; set; } = "";
        public string DeepSeekKey { get; set; } = "";
        public int TransCount { get; set; } = 0;
        public int MaxThreadCount { get; set; } = 0;
        public bool AutoLoadDictionaryFile { get; set; } = false;

        public void ReadConfig()
        {
            if (File.Exists(DeFine.GetFullPath(@"\setting.config")))
            {
                var GetStr = FileHelper.ReadFileByStr(DeFine.GetFullPath(@"\setting.config"), Encoding.UTF8);
                if (GetStr.Trim().Length > 0)
                {
                    var GetSetting = JsonSerializer.Deserialize<LocalSetting>(GetStr);
                    if (GetSetting != null)
                    {
                        this.MaxThreadCount = GetSetting.MaxThreadCount;
                        this.AutoLoadDictionaryFile = GetSetting.AutoLoadDictionaryFile;
                        this.PhraseEngineUsing = GetSetting.PhraseEngineUsing;
                        this.CodeParsingEngineUsing = GetSetting.CodeParsingEngineUsing;
                        this.ConjunctionEngineUsing = GetSetting.ConjunctionEngineUsing;
                        this.BaiDuYunApiUsing = GetSetting.BaiDuYunApiUsing;
                        this.ChatGptApiUsing = GetSetting.ChatGptApiUsing;
                        this.DeepSeekApiUsing = GetSetting.DeepSeekApiUsing;
                        this.GoogleYunApiUsing = GetSetting.GoogleYunApiUsing;
                        this.DivCacheEngineUsing = GetSetting.DivCacheEngineUsing;
                        this.SourceLanguage = GetSetting.SourceLanguage;
                        this.TargetLanguage = GetSetting.TargetLanguage;
                        DeFine.SourceLanguage = GetSetting.SourceLanguage;
                        DeFine.TargetLanguage = GetSetting.TargetLanguage;
                        this.CurrentUILanguage = GetSetting.CurrentUILanguage;
                        this.APath = GetSetting.APath;
                        this.BPath = GetSetting.BPath;
                        this.SkyrimPath = GetSetting.SkyrimPath;
                        this.GoogleKey = GetSetting.GoogleKey;
                        this.BaiDuAppID = GetSetting.BaiDuAppID;
                        this.BaiDuSecretKey = GetSetting.BaiDuSecretKey;
                        this.ChatGptKey = GetSetting.ChatGptKey;
                        this.DeepSeekKey = GetSetting.DeepSeekKey;
                        this.TransCount = GetSetting.TransCount;
                        this.PlaySound = GetSetting.PlaySound;
                        this.BackUpPath = GetSetting.BackUpPath;
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

        public void SaveConfig()
        {
            var Options = new JsonSerializerOptions { WriteIndented = true };

            LocalSetting CopySetting = this;
            var GetSettingContent = JsonSerializer.Serialize(CopySetting, Options);

            FileHelper.WriteFile(DeFine.GetFullPath(@"\setting.config"), GetSettingContent, Encoding.UTF8);
        }
    }
}
