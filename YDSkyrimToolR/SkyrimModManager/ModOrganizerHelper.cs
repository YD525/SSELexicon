
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
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class ModOrganizerHelper
    {
        public static byte[] GetBytesByFilePath(string strFile)
        {
            byte[] photo_byte = null;

            if (File.Exists(strFile))
            {
                using (FileStream fs =
                          new FileStream(strFile, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        photo_byte = br.ReadBytes((int)fs.Length);
                    }
                }
            }
            else
            {
                return new byte[0];
            }

            return photo_byte;
        }

        public static string StringDivision(string Message, string Left, string Right)
        {
            if (Message.Contains(Left) && Message.Contains(Right))
            {
                string GetLeftString = Message.Substring(Message.IndexOf(Left) + Left.Length);
                string GetRightString = GetLeftString.Substring(0, GetLeftString.IndexOf(Right));
                return GetRightString;
            }
            else
            {
                return string.Empty;
            }
        }

        public static MoConfigItem ReadMo2Config(string SourcePath)
        {
            MoConfigItem Config = new MoConfigItem();
            if (File.Exists(SourcePath))
            {
                Config.Mo2Path = SourcePath.Substring(0, SourcePath.LastIndexOf(@"\")) + @"\";

                Console.WriteLine(SourcePath);
                var GetData = GetBytesByFilePath(SourcePath);
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
                                    Config.gamePath = StringDivision(GetContent, "@ByteArray(", ")");
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
                                    Config.mod_directory = StringDivision(GetContent, "@ByteArray(", ")");
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
                                    Config.overwrite_directory = StringDivision(GetContent, "@ByteArray(", ")");
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
                                    Config.profiles_directory = StringDivision(GetContent, "@ByteArray(", ")");
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

            if (Directory.Exists(Config.profiles_directory))
            {
                foreach (var GetFolder in Directory.GetDirectories(Config.profiles_directory))
                {
                    ModLoader NewModLoader = new ModLoader();
                    string GetLoadName = GetFolder.Substring(GetFolder.LastIndexOf(@"\") + @"\".Length);
                    NewModLoader.LoadName = GetLoadName;
                    string ModListPath = GetFolder + @"\modlist.txt";
                    if (File.Exists(ModListPath))
                    {
                        foreach (var GetLine in Encoding.UTF8.GetString(GetBytesByFilePath(ModListPath)).Split(new char[2] { '\r', '\n' }))
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
