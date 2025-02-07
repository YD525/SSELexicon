
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YDSkyrimToolR;
using YDSkyrimToolR.ConvertManager;
using YDSkyrimToolR.SkyrimModManager;

namespace YDSkyrimTools.SkyrimModManager
{
    public class ModOrganizerHelper
    {

        public static List<string> AllCheckModStr = new List<string>();
        public static Thread BackupThread = null;//备份主线程
        public static void StartBackupService(System.Windows.Controls.ProgressBar OneBar, System.Windows.Controls.Label OneLab, System.Windows.Controls.TextBox OneTextBox)
        {
            if (BackupThread == null)
            {
                BackupThread = new Thread(() => {

                    AllCheckModStr.Clear();

                    OneTextBox.Dispatcher.Invoke(new Action(() => {
                        OneTextBox.Text = "执行Mod扫描";
                    }));

                    string GetNeedBackUPPath = DeFine.GlobalLocalSetting.ModOrganizerConfig.mod_directory + @"\";
                    string GetTargetPath = (DeFine.GlobalLocalSetting.BackUpPath + @"\").Trim();

                    if (!Directory.Exists(GetTargetPath))
                    {
                        BackupThread = null;
                        return;
                    }
                    if (GetTargetPath.Trim().Length == 0)
                    {
                        BackupThread = null;
                        return;
                    }

                    List<string> NeedBackMods = new List<string>();

                    var GetSourceMods = Directory.GetDirectories(GetNeedBackUPPath);

                    OneBar.Dispatcher.Invoke(new Action(() => {
                        OneBar.Minimum = 0;
                        OneBar.Maximum = GetSourceMods.Length;
                    }));

                    int ProcessCount = 0;

                    foreach (var GetDir in GetSourceMods)
                    {
                        ProcessCount++;

                        string GetDirName = GetDir.Substring(GetDir.LastIndexOf(@"\")+ @"\".Length);

                        OneLab.Dispatcher.Invoke(new Action(() => {
                            OneLab.Content = GetDirName;
                        }));

                        OneBar.Dispatcher.Invoke(new Action(() => {
                            OneBar.Value = ProcessCount;
                        }));

                        if (Directory.Exists(GetTargetPath + GetDirName))
                        {
                            //需要对比文件差异
                            List<File_Fold_info> Foldinfo1 = new List<File_Fold_info>();
                            List<File_Fold_info> Foldinfo2 = new List<File_Fold_info>();

                            Search(ref Foldinfo1, GetDir);

                            Search(ref Foldinfo2, GetTargetPath + GetDirName);

                            List<string> lack = new List<string>(), surplus = new List<string>(), samename = new List<string>();

                            RecursionCompare(ref lack, ref surplus, ref samename, Foldinfo1, Foldinfo2, Foldinfo1[0], Foldinfo2[0]);

                            if (lack.Count > 0 || surplus.Count > 0 || samename.Count > 0)
                            {
                                OneTextBox.Dispatcher.Invoke(new Action(() => {
                                    OneTextBox.Text += "\r\n";
                                    OneTextBox.ScrollToEnd();
                                }));

                                if (!NeedBackMods.Contains(GetDirName))
                                {
                                    NeedBackMods.Add(GetDirName);
                                }
                            }

                            for (int i = 0; i < lack.Count; i++)
                            {
                                OneTextBox.Dispatcher.Invoke(new Action(() => {
                                    OneTextBox.Text += "\r\n" + lack[i] + "缺少文件";
                                    OneTextBox.ScrollToEnd();
                                }));
                                if (i > 25)
                                {
                                    OneTextBox.Dispatcher.Invoke(new Action(() => {
                                        OneTextBox.Text += "\r\n" + "忽略更多文件...";
                                        OneTextBox.ScrollToEnd();
                                    }));
                                    break;
                                }
                            }
                            for (int i = 0; i < surplus.Count; i++)
                            {
                                OneTextBox.Dispatcher.Invoke(new Action(() => {
                                    OneTextBox.Text += "\r\n" + surplus[i] + "多余文件";
                                    OneTextBox.ScrollToEnd();
                                }));
                                if (i > 25)
                                {
                                    OneTextBox.Dispatcher.Invoke(new Action(() => {
                                        OneTextBox.Text += "\r\n" + "忽略更多文件...";
                                        OneTextBox.ScrollToEnd();
                                    }));
                                    break;
                                }
                            }
                            for (int i = 0; i < samename.Count; i++)
                            {
                                OneTextBox.Dispatcher.Invoke(new Action(() => {
                                    OneTextBox.Text += "\r\n" + samename[i] + "同名文件但特征码不同";
                                    OneTextBox.ScrollToEnd();
                                }));
                                if (i > 25)
                                {
                                    OneTextBox.Dispatcher.Invoke(new Action(() => {
                                        OneTextBox.Text += "\r\n" + "忽略更多文件...";
                                        OneTextBox.ScrollToEnd();
                                    }));
                                    break;
                                }
                            }
                        }
                        else
                        {
                            //缺少mod无需对比直接备份
                            NeedBackMods.Add(GetDirName);
                        }
                    }

                    OneTextBox.Dispatcher.Invoke(new Action(() => {
                        OneTextBox.Text += "\r\n";
                        OneTextBox.ScrollToEnd();
                    }));

                    foreach (var GetChangeMod in NeedBackMods)
                    {
                        OneTextBox.Dispatcher.Invoke(new Action(() => {
                            OneTextBox.Text += "\r\n" + GetChangeMod + "发生变化 需要重新备份";
                            OneTextBox.ScrollToEnd();
                        }));
                    }
                  
                    OneLab.Dispatcher.Invoke(new Action(() => {
                        OneLab.Content = "检测完成";
                    }));

                    BackupThread = null;
                });
                BackupThread.Start();
            }
        }
        public static void ClearOverWrite()
        {
            string GetMo2Path = DeFine.GlobalLocalSetting.ModOrganizerConfig.Mo2Path + @"\" + @"Mods\overwrite";

            string CalcCharGenPath = DeFine.GlobalLocalSetting.ModOrganizerConfig.Mo2Path + @"\" + @"Mods\overwrite\SKSE\Plugins\CharGen\Presets";

            if (Directory.Exists(CalcCharGenPath))
            {
                foreach (var GetFile in Directory.GetFiles(CalcCharGenPath))
                {
                    string GetFileName = GetFile.Substring(GetFile.LastIndexOf(@"\") + @"\".Length);

                    string GetTargetFile = DeFine.GetFullPath(DeFine.BackupPath) + @"\" + @"CharGen\" + GetFileName;

                    if (File.Exists(GetTargetFile))
                    {
                        File.Delete(GetTargetFile);
                    }

                    //转移捏脸文件
                    File.Copy(GetFile, GetTargetFile);
                }
            }

            List<string> NeedDeleteDirectories = new List<string>();//检测后需要删除的目录

            if (Directory.Exists(GetMo2Path))
            {
                foreach (var Get in Directory.GetDirectories(GetMo2Path))
                {
                    string GetFileName = Get.Substring(Get.LastIndexOf(@"\") + @"\".Length);
                    if (!GetFileName.Equals("meshes"))
                    {
                        NeedDeleteDirectories.Add(Get);
                    }
                }

                foreach (var GetDir in NeedDeleteDirectories)
                {
                    DataHelper.DeleteDirectory(GetDir);
                }
            }

            //创建捏脸预设路径
            Directory.CreateDirectory(CalcCharGenPath);
            //我在给他还原回去233
            foreach (var Get in DataHelper.GetAllFile(DeFine.GetFullPath(DeFine.BackupPath) + @"\" + @"CharGen\"))
            {
                File.Copy(Get.FilePath, CalcCharGenPath + @"\" + Get.FileName);
            }
        }

        public static List<string> CheckSkyrimNeeds(MoConfigItem Config)
        {
            string GamePath = Config.gamePath;

            List<string> Msgs = new List<string>();
            if (Directory.Exists(GamePath))
            {
                var Plate = Environment.Is64BitOperatingSystem ? "x64" : "x86";

                if (Plate == "x64")
                {
                    string SkyrimPath = GamePath + @"\" + "SkyrimSE.exe";

                    if (File.Exists(SkyrimPath))
                    {
                        FileVersionInfo SkyrimInFo = FileVersionInfo.GetVersionInfo(SkyrimPath);

                        string SkyrimVersion = SkyrimInFo.ProductVersion.Replace(",", ".").Replace(" ", "");

                        if (SkyrimVersion.EndsWith(".0"))
                        {
                            SkyrimVersion = SkyrimVersion.Substring(0,SkyrimVersion.LastIndexOf(".0"));
                        }

                        Msgs.Add("SkyrimSE-已安装! Version:" + SkyrimVersion);
                    }
                    else
                    {
                        Msgs.Add("这个不是您的重制版安装路径!");
                    }


                    string SksePath = GamePath + @"\" + "skse64_loader.exe";

                    if (File.Exists(SksePath))
                    {
                        FileVersionInfo SkseInFo = FileVersionInfo.GetVersionInfo(SksePath);

                        string SkseVersion = SkseInFo.ProductVersion.Replace(",", ".").Replace(" ", "");
                        if (SkseVersion.StartsWith("0."))
                        {
                            SkseVersion = SkseVersion.Substring("0.".Length);
                        }

                        Msgs.Add("Skse64-已安装! Version:" + SkseVersion);
                    }
                    else
                    {
                        Msgs.Add("建议你安装Skse:" + "https://skse.silverlock.org/");
                    }

                    string ENBSeries = GamePath + @"\" + "ENB.config";

                    if (File.Exists(ENBSeries))
                    {
                        string GetContent = Encoding.UTF8.GetString(DataHelper.GetBytesByFilePath(ENBSeries));
                        string GetEnbDxDllLocation = ConvertHelper.StringDivision(GetContent, @"CurrentPath':'".Replace("'", "\""), "\"");

                        FileVersionInfo ENBInFo = FileVersionInfo.GetVersionInfo(GamePath + @"\" + GetEnbDxDllLocation);
                        string ENBVersion = ENBInFo.ProductVersion.Replace(",", ".").Replace(" ", "");

                        if (ENBVersion.StartsWith("0."))
                        {
                            ENBVersion = ENBVersion.Substring("0.".Length);
                        }

                        Msgs.Add("Enb-已安装! DxVersion:" + ENBVersion);
                    }
                    else
                    {
                        Msgs.Add("建议你安装Enb:" + "http://enbdev.com/");
                    }

                    if (File.Exists(Config.FnisPath))
                    {
                        FileVersionInfo FnisInFo = FileVersionInfo.GetVersionInfo(Config.FnisPath);

                        Msgs.Add("Fnis-已安装! Version:" + FnisInFo.FileVersion);
                    }
                    else
                    {
                        Msgs.Add("缺少或未配置Fnis组件 请安装Fnis!");
                    }

                    if (File.Exists(Config.BodySlidePath))
                    {
                        FileVersionInfo BodySlideInFo = FileVersionInfo.GetVersionInfo(Config.BodySlidePath);

                        Msgs.Add("BS-已安装! Version:" + BodySlideInFo.FileVersion);
                    }
                    else
                    {
                        Msgs.Add("缺少或未配置BodySlide组件 请安装BodySlide!");
                    }

                    if (File.Exists(Config.NemesisPath))
                    {
                        Msgs.Add("Nemesis-已安装!");
                    }
                    else
                    {
                        Msgs.Add("建议你安装 Nemesis组件.");
                    }
                }
                else
                {
                   
                  Msgs.Add("建议您重装系统到64位系统");
                    
                }
            }

            return Msgs;
        }

        public static bool CheckMo2(string SourcePath)
        {
            if (SourcePath.Length == 0) return false;
            if (Directory.Exists(SourcePath))
            {
                string TargetProgram = SourcePath += @"\ModOrganizer.exe";
                if (File.Exists(TargetProgram))
                {
                    return true;
                }
            }
            return false;
        }


        public static byte[] TextToByteArray(string Text)
        {

            return null;
        }

        public static HashAlgorithm GlobalHash = HashAlgorithm.Create("SHA1");

        public static string FileHash(string path)
        {
            if (path.ToLower().EndsWith(".dll"))
            {
                return BitConverter.ToString(GlobalHash.ComputeHash(File.ReadAllBytes(path)));
            }
            if (path.ToLower().EndsWith(".txt"))
            {
                return BitConverter.ToString(GlobalHash.ComputeHash(File.ReadAllBytes(path)));
            }
            if (path.ToLower().EndsWith(".esl"))
            {
                return BitConverter.ToString(GlobalHash.ComputeHash(File.ReadAllBytes(path)));
            }
            if (path.ToLower().EndsWith(".esm"))
            {
                return BitConverter.ToString(GlobalHash.ComputeHash(File.ReadAllBytes(path)));
            }
            if (path.ToLower().EndsWith(".esp"))
            {
                return BitConverter.ToString(GlobalHash.ComputeHash(File.ReadAllBytes(path)));
            }
            if (path.ToLower().EndsWith(".pex"))
            {
                return BitConverter.ToString(GlobalHash.ComputeHash(File.ReadAllBytes(path)));
            }

            return "0";
        }

        public static void Search(ref List<File_Fold_info> data, string path)//开始搜索，生成目录数据，并且把头目录准备好
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);//获取头文件夹信息

            File_Fold_info file_Fold_Info = new File_Fold_info();
            file_Fold_Info.name = directoryInfo.Name;
            file_Fold_Info.fullname = directoryInfo.FullName;
            file_Fold_Info.type = 1;
            file_Fold_Info.fatherindex = -1;

            if (directoryInfo.GetDirectories().Length != 0 || directoryInfo.GetFiles().Length != 0)
                file_Fold_Info.childer = new List<int>();
            data.Add(file_Fold_Info);

            RecursionFile(ref data, directoryInfo, data.Count - 1);

        }
        public static void RecursionFile(ref List<File_Fold_info> data, DirectoryInfo directoryInfo, int parentdirectorindex)
        {

            DirectoryInfo[] dir_info = directoryInfo.GetDirectories();//先搜索文件夹，因为childer的分配在文件夹搜索中。
            for (int i = 0; i < dir_info.Length; i++)
            {
                File_Fold_info file_Fold_Info = new File_Fold_info();
                file_Fold_Info.name = dir_info[i].Name;
                file_Fold_Info.fullname = dir_info[i].FullName;
                file_Fold_Info.type = 1;
                file_Fold_Info.fatherindex = parentdirectorindex;
                //为下一级的递归分配childer空间
                if (dir_info[i].GetDirectories().Length != 0 || dir_info[i].GetFiles().Length != 0)
                {
                    file_Fold_Info.childer = new List<int>();
                    data.Add(file_Fold_Info);
                    int dir_index = data.Count - 1;
                    data[parentdirectorindex].childer.Add(dir_index);
                    //以当前文件夹为头，开始新的递归
                    RecursionFile(ref data, dir_info[i], dir_index);
                }
                else //没有子级就不递归了
                {
                    data.Add(file_Fold_Info);
                    int dir_index = data.Count - 1;
                    data[parentdirectorindex].childer.Add(dir_index);
                }


            }
            FileInfo[] file_info = directoryInfo.GetFiles();
            for (int i = 0; i < file_info.Length; i++)//文件处理
            {
                File_Fold_info file_Fold_Info = new File_Fold_info();
                file_Fold_Info.name = file_info[i].Name;
                file_Fold_Info.fullname = file_info[i].FullName;
                file_Fold_Info.type = 0;
                file_Fold_Info.hash = FileHash(file_info[i].FullName);
                file_Fold_Info.fatherindex = parentdirectorindex;
                data.Add(file_Fold_Info);
                data[parentdirectorindex].childer.Add(data.Count - 1);
            }
        }
        /// <summary>
        /// 比较函数
        /// </summary>
        /// <param name="lack">缺少的</param>
        /// <param name="surplus">多余的</param>
        /// <param name="samename">同名的</param>
        /// <param name="reference">参考数据</param>
        /// <param name="compared">比较数据</param>
        /// <param name="referenceinfo">从哪个参考数据文件夹开始</param>
        /// <param name="compareinfo">从哪个比较数据文件夹开始</param>
        static void RecursionCompare(ref List<string> lack, ref List<string> surplus, ref List<string> samename, List<File_Fold_info> reference, List<File_Fold_info> compared, File_Fold_info referenceinfo, File_Fold_info compareinfo)
        {
            File_Fold_info[] referenceChilder;
            if (referenceinfo.childer != null)
            {
                referenceChilder = new File_Fold_info[referenceinfo.childer.Count];//制造参考子级的数租，方便后面计算

                for (int i = 0; i < referenceinfo.childer.Count; i++)
                {
                    referenceChilder[i] = reference[referenceinfo.childer[i]];//making
                }
            }
            else
            {
                referenceChilder = new File_Fold_info[0];
            }

            File_Fold_info[] compareChilder;
            if (compareinfo.childer != null)
            {
                compareChilder = new File_Fold_info[compareinfo.childer.Count];//制造比较者子级的数组，方便比较


                for (int i = 0; i < compareinfo.childer.Count; i++)
                {
                    compareChilder[i] = compared[compareinfo.childer[i]];//making
                }
            }
            else
            {
                compareChilder = new File_Fold_info[0];
            }
            for (int i = 0; i < referenceChilder.Length; i++)
            {//循环进行比较
                for (int j = 0; j < compareChilder.Length; j++)
                {
                    if (referenceChilder[i].type == 0)//如果参考中是文件类型
                    {
                        if (compareChilder[j].type == 0 && compareChilder[j].name == referenceChilder[i].name)//比较中也是文件类型，并且名字相同
                            if (compareChilder[j].hash != referenceChilder[i].hash)//但是hash值不一样，说明是不同的文件，只是同名了
                            {
                                samename.Add(compareChilder[j].fullname);//在同名数组中添加该信息
                            }
                            else//不然就是同一个文件
                            {
                                //在参考和比较数组中清除这个文件的信息，结构体无法为null
                                compareChilder[j] = new File_Fold_info() { type = -1 };
                                referenceChilder[i] = new File_Fold_info() { type = -1 };
                            }
                    }
                    else//不然就是文件夹类型
                    {
                        if (compareChilder[j].type == 1 && compareChilder[j].name == referenceChilder[i].name)
                        {//比较中也是文件夹类型，并且名字相同
                         //那么就进行下一轮的比较
                            RecursionCompare(ref lack, ref surplus, ref samename, reference, compared, referenceChilder[i], compareChilder[j]);
                            //在参考和比较数组中清除这个文件夹的信息，结构体无法为null
                            compareChilder[j] = new File_Fold_info() { type = -1 };
                            referenceChilder[i] = new File_Fold_info() { type = -1 };
                        }
                    }
                }

            }
            //这样子以来，参考数组中剩下的部分就是比较数组没有的
            for (int i = 0; i < referenceChilder.Length; i++)
            {
                if (referenceChilder[i].type != -1)
                    lack.Add(referenceChilder[i].fullname);
            }
            //比较数组中剩下的就是多余的。
            for (int i = 0; i < compareChilder.Length; i++)
            {
                if (compareChilder[i].type != -1)
                    surplus.Add(compareChilder[i].fullname);
            }
        }

        public static void ScanMod()
        {
            List<File_Fold_info> foldinfo = new List<File_Fold_info>();
            List<File_Fold_info> foldinfo2 = new List<File_Fold_info>();
            Search(ref foldinfo, @"C:\Users\52508\Desktop\A");
            Search(ref foldinfo2, @"C:\Users\52508\Desktop\B");
            List<string> lack = new List<string>(), surplus = new List<string>(), samename = new List<string>();

            RecursionCompare(ref lack, ref surplus, ref samename, foldinfo, foldinfo2, foldinfo[0], foldinfo2[0]);
            Console.WriteLine("缺少：");
            for (int i = 0; i < lack.Count; i++)
            {
                Console.WriteLine(lack[i]);
            }
            Console.WriteLine("多余：");
            for (int i = 0; i < surplus.Count; i++)
            {
                Console.WriteLine(surplus[i]);
            }
            Console.WriteLine("同名：");
            for (int i = 0; i < samename.Count; i++)
            {
                Console.WriteLine(samename[i]);
            }
        }


        public static MoConfigItem ReadMo2Config(string SourcePath)
        {
            MoConfigItem Config = new MoConfigItem();
            if (Directory.Exists(SourcePath))
            {
                Config.Mo2Path = SourcePath;
                SourcePath += @"\ModOrganizer.ini";
                var GetData = DataHelper.GetBytesByFilePath(SourcePath);
                string DataContent = Encoding.UTF8.GetString(GetData);

                foreach (var GetLine in DataContent.Split(new char[2] { '\r', '\n' }))
                {
                    if (GetLine.Trim().Length > 0)
                    {
                        if (GetLine.Contains("="))
                        {
                            string GetCaption = GetLine.Split('=')[0];
                            string GetContent = GetLine.Split('=')[1];

                            if (GetCaption.Equals("gamePath"))
                            {
                                if (GetContent.Contains("@ByteArray("))
                                {
                                    Config.gamePath = ConvertHelper.StringDivision(GetContent, "@ByteArray(", ")");
                                }
                                else
                                {
                                    Config.gamePath = GetContent;
                                }
                            }

                            if (GetCaption.Equals("mod_directory"))
                            {
                                if (GetContent.Contains("@ByteArray("))
                                {
                                    Config.mod_directory = ConvertHelper.StringDivision(GetContent, "@ByteArray(", ")");
                                }
                                else
                                {
                                    Config.mod_directory = GetContent;
                                }
                            }

                            if (GetCaption.Equals("overwrite_directory"))
                            {
                                if (GetContent.Contains("@ByteArray("))
                                {
                                    Config.overwrite_directory = ConvertHelper.StringDivision(GetContent, "@ByteArray(", ")");
                                }
                                else
                                {
                                    Config.overwrite_directory = GetContent;
                                }
                            }

                            if (GetCaption.Equals("profiles_directory"))
                            {
                                if (GetContent.Contains("@ByteArray("))
                                {
                                    Config.profiles_directory = ConvertHelper.StringDivision(GetContent, "@ByteArray(", ")");
                                }
                                else
                                {
                                    Config.profiles_directory = GetContent;
                                }
                            }

                            if (GetCaption.Equals("MainWindow_modList_index"))
                            {
                                foreach (var GetModName in GetContent.Split(','))
                                {
                                    Config.Mods.Add(GetModName);
                                }
                            }

                            //检测BS
                            if (GetContent.Contains("BodySlide.exe"))
                            {
                                Config.BodySlidePath = GetContent;
                            }

                            //检测Fnis
                            if (GetContent.Contains("GenerateFNISforUsers.exe"))
                            {
                                Config.FnisPath = GetContent;
                            }

                            //检测Nemesis
                            if (GetContent.Contains("Nemesis Unlimited Behavior Engine.exe"))
                            {
                                Config.NemesisPath = GetContent;
                            }
                        }
                    }
                }
            }

            Config.gamePath = Config.gamePath.Replace(@"\\", @"\").Replace("/", @"\");
            Config.mod_directory = Config.mod_directory.Replace(@"\\", @"\").Replace("/", @"\");
            Config.profiles_directory = Config.profiles_directory.Replace(@"\\", @"\").Replace("/", @"\");
            Config.overwrite_directory = Config.overwrite_directory.Replace(@"\\", @"\").Replace("/", @"\");


            foreach (var GetFolder in Directory.GetDirectories(Config.profiles_directory))
            {
                ModLoader NewModLoader = new ModLoader();
                string GetLoadName = GetFolder.Substring(GetFolder.LastIndexOf(@"\") + @"\".Length);
                NewModLoader.LoadName = GetLoadName;
                string ModListPath = GetFolder + @"\modlist.txt";
                if (File.Exists(ModListPath))
                {
                    foreach (var GetLine in Encoding.UTF8.GetString(DataHelper.GetBytesByFilePath(ModListPath)).Split(new char[2] { '\r', '\n' }))
                    {
                        if (GetLine.Trim().Length > 0)
                        {
                            if (!GetLine.StartsWith("# "))
                            {
                                NewModLoader.Mods.Add(new ModItem(GetLine));
                            }
                        }
                    }
                }
                Config.ModLoaders.Add(NewModLoader);
            }

            return Config;
        }

    }

    public class ModItem
    {
        public string ModName = "";
        public bool IsEnable = false;

        public ModItem(string Value)
        {
            if (Value != null)
            {
                if (Value.StartsWith("+"))
                {
                    this.IsEnable = true;
                    Value = Value.Substring(1);
                }
                else
                {
                    this.IsEnable = false;
                    Value = Value.Substring(1);
                }

                this.ModName = Value;
            }
        }
    }
    public class ModLoader
    {
        public string LoadName = "";
        public List<ModItem> Mods = new List<ModItem>();
    }

    public class MoConfigItem
    {
        public string Mo2Path = "";
        public string gamePath = "";
        public string mod_directory = "";
        public string overwrite_directory = "";
        public string profiles_directory = "";

        public string FnisPath = "";
        public string BodySlidePath = "";
        public string NemesisPath = "";

        public List<string> Mods = new List<string>();
        public List<ModLoader> ModLoaders = new List<ModLoader>();

        public List<string> GetEnableMods(string LoadName)
        {
            List<string> ReturnMods = new List<string>();

            foreach (var Get in this.ModLoaders)
            {
                if (Get.LoadName.Equals(LoadName))
                {
                    foreach (var GetModItem in Get.Mods)
                    {
                        if (GetModItem.IsEnable)
                        {
                            ReturnMods.Add(GetModItem.ModName);
                        }
                    }
                }
            }

            return ReturnMods;
        }

        public List<string> GetDisableMods(string LoadName)
        {
            List<string> ReturnMods = new List<string>();

            foreach (var Get in this.ModLoaders)
            {
                if (Get.LoadName.Equals(LoadName))
                {
                    foreach (var GetModItem in Get.Mods)
                    {
                        if (!GetModItem.IsEnable)
                        {
                            ReturnMods.Add(GetModItem.ModName);
                        }
                    }
                }
            }

            return ReturnMods;
        }
    }

    public struct File_Fold_info
    {
        public string hash;//文件的hash值，用于比较内容
        public string name;//文件或者的名字
        public string fullname;//文件或者文件夹的绝对路径
        /// <summary>
        /// 目录节点的类型
        /// 0 file. 1 fold
        /// </summary>
        public int type;
        //父节点的index
        public int fatherindex;
        //所有子级的index
        public List<int> childer;
    }





}
