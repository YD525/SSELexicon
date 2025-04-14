
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using SSELex.ConvertManager;

namespace SSELex.SkyrimModManager
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class DataHelper
    {
        public static void DeleteDirectory(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                DirectoryInfo[] childs = dir.GetDirectories();
                foreach (DirectoryInfo child in childs)
                {
                    child.Delete(true);
                }
                dir.Delete(true);
            }
        }
  
        public static string GetPathAndFileName(ref string SourcePath)
        {
            string FileName = SourcePath.Substring(SourcePath.LastIndexOf(@"\") + @"\".Length);
            SourcePath = SourcePath.Substring(0, SourcePath.LastIndexOf(@"\") + @"\".Length);
            return FileName;
        }

        public static bool TryMoveFile(string SourcePath, string VirtualPath)
        {
            string GetFullPath = VirtualPath;

            string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            AppData = AppData.Substring(0, AppData.LastIndexOf(@"\") + @"\".Length);

            if (GetFullPath.StartsWith(@"Roaming\"))
            {
                GetFullPath = AppData + GetFullPath;
            }
            else
           if (GetFullPath.StartsWith(@"Local\"))
            {
                GetFullPath = AppData + GetFullPath;
            }
            else
           if (GetFullPath.StartsWith(@"LocalLow\"))
            {
                GetFullPath = AppData + GetFullPath;
            }
            else
            {
                GetFullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + GetFullPath;
            }

            string GetTargetPath = GetFullPath.Substring(0, GetFullPath.LastIndexOf(@"\") + @"\".Length);
            string GetTargetName = GetFullPath.Substring(GetFullPath.LastIndexOf(@"\") + @"\".Length);

            GetTargetName = GetTargetName.Replace("\r\n", "");

            if (!Directory.Exists(GetTargetPath))
            {
                Directory.CreateDirectory(GetTargetPath);
            }

            try
            {
                if (File.Exists(GetTargetPath + GetTargetName))
                {
                    File.Delete(GetTargetPath + GetTargetName);
                }


                File.Copy(SourcePath, GetTargetPath + GetTargetName);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public static string TryFindSavePath(string Path, ref string NameRule)
        {
            int Offset = 0;
            string ThisPath = "";

            foreach (var Get in Path.Split('\\'))
            {
                int NumberOffset = 0;
                for (int i = 0; i < Get.Length; i++)
                {
                    int Number = 0;

                    if (int.TryParse(Get.Substring(i, 1), out Number))
                    {
                        NumberOffset++;
                    }
                }

                if (NumberOffset >= 7)
                {
                    NameRule = Get;
                    break;
                }
                else
                {
                    ThisPath += Get + @"\";
                    Offset++;
                }
            }

            return ThisPath;
        }
      
        public static double DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan A = new TimeSpan(DateTime1.Ticks);
            TimeSpan B = new TimeSpan(DateTime2.Ticks);
            TimeSpan TS = A - B;
            return TS.TotalMilliseconds;
        }
        
        public static void WriteFile(string TargetPath, byte[] Data)
        {
            FileStream FS = new FileStream(TargetPath, FileMode.Create);
            FS.Write(Data, 0, Data.Length);
            FS.Close();
            FS.Dispose();
        }
        
        public static string ShowFileDialog(string SelectType,string Text)
        {
            try { 
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.Filter = SelectType;
            FileDialog.Title = Text;
            if (FileDialog.ShowDialog() == true)
            {
                return FileDialog.FileName;
            }
            return string.Empty;
            }
            catch { return string.Empty; }
        }

        public static List<string> ReadFile(string filepath, Encoding EnCoding)
        {
            try
            {
                StreamReader rd = new StreamReader(filepath, EnCoding);

                StringBuilder sb = new StringBuilder();
                while (!rd.EndOfStream)
                {
                    string dqstr = rd.ReadLine();
                    sb = sb.Append(dqstr + "\r\n");
                }

                rd.Close();
                rd.Dispose();
                return StrToList(sb.ToString());
            }
            catch
            {
                return new List<string>();
            }
        }

        public static string ReadFileByStr(string filepath, Encoding EnCoding)
        {
            try
            {
                StreamReader rd = new StreamReader(filepath, Encoding.UTF8);

                StringBuilder sb = new StringBuilder();
                while (!rd.EndOfStream)
                {
                    string dqstr = rd.ReadLine();
                    sb = sb.Append(dqstr + "\r\n");
                }

                rd.Close();
                rd.Dispose();
                return sb.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
       
        public static byte[] StrToStream(string Str)
        {
            return Encoding.UTF8.GetBytes(Str);
        }

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

        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
       
        public static void WriteFile(string targetpath, string text, Encoding encodingtype)
        {
            try
            {
                StreamWriter FileWriter = new StreamWriter(targetpath, false, encodingtype);
                FileWriter.Write(text);
                FileWriter.Close();
                FileWriter.Dispose();
                GC.Collect();
            }
            catch { }
        }
      
        public static void WriteFileAppend(string targetpath, string text, Encoding encodingtype)
        {
            try
            {
                StreamWriter FileWriter = new StreamWriter(targetpath, true, encodingtype);
                FileWriter.Write(text);
                FileWriter.Close();
                FileWriter.Dispose();
                GC.Collect();
            }
            catch { }
        }
        
        public static bool CreatFile(string targetpath)
        {
            bool iserror = false;
            if (File.Exists(targetpath) == false)
            {
                File.Create(targetpath);
            }
            else
            {
                iserror = true;
            }
            return iserror;
        }

        public static System.Text.Encoding GetFileEncodeType(string filename)
        {
            System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            Byte[] buffer = br.ReadBytes(2);
            if (buffer[0] >= 0xEF)
            {
                if (buffer[0] == 0xEF && buffer[1] == 0xBB)
                {
                    fs.Close();
                    br.Close();
                    return System.Text.Encoding.UTF8;
                }
                else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
                {
                    fs.Close();
                    br.Close();
                    return System.Text.Encoding.BigEndianUnicode;
                }
                else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
                {
                    fs.Close();
                    br.Close();
                    return System.Text.Encoding.Unicode;
                }
                else
                {
                    fs.Close();
                    br.Close();
                    return System.Text.Encoding.Default;
                }
            }
            if (buffer[0] == 0x3c)//utf-8无bom格式
            {
                fs.Close();
                br.Close();
                return System.Text.Encoding.UTF8;
            }

            else
            {
                fs.Close();
                br.Close();
                return System.Text.Encoding.Default;
            }
            fs.Close();
            br.Close();
        }

        public static void CopyDir(string srcPath, string aimPath)
        {
            try
            {
                if (aimPath[aimPath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                {
                    aimPath += System.IO.Path.DirectorySeparatorChar;
                }
                if (!System.IO.Directory.Exists(aimPath))
                {
                    System.IO.Directory.CreateDirectory(aimPath);
                }
                string[] fileList = System.IO.Directory.GetFileSystemEntries(srcPath);
                foreach (string file in fileList)
                {
                    if (System.IO.Directory.Exists(file))
                    {
                        CopyDir(file, aimPath + System.IO.Path.GetFileName(file));
                    }
                    else
                    {
                        System.IO.File.Copy(file, aimPath + System.IO.Path.GetFileName(file), true);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static string ListToSrt(List<string> obj)
        {
            string RichText = "";
            if (obj.Count == 1)
            {
                return obj[0];
            }
            else
            {
                RichText = string.Join("\r\n", obj.ToArray());
                return RichText;
            }

        }
       
        public static bool StrToBool(string obj)
        {
            bool NextBool = false;
            bool.TryParse(obj, out NextBool);
            return NextBool;
        }
       
        public static List<string> StrToList(string obj)
        {
            List<string> AllLine = new List<string>();
            if (obj.Contains("\r\n"))
            {
                AllLine = obj.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            else
            {
                AllLine.Add(obj);
            }
            return AllLine;
        }
      
        public static int StrToInt(string Number)
        {
            int NNumber = 0;
            int.TryParse(Number, out NNumber);
            return NNumber;
        }

        public static void DeleteFolder(string strPath)
        {
            if (Directory.GetDirectories(strPath).Length > 0)
            {
                foreach (string fl in Directory.GetDirectories(strPath))
                {
                    Directory.Delete(fl, true);
                }
            }
            if (Directory.GetFiles(strPath).Length > 0)
            {
                foreach (string f in Directory.GetFiles(strPath))
                {
                    System.IO.File.Delete(f);
                }
            }
            Directory.Delete(strPath, true);
        }

        public static List<FileInformation> GetAllFile(string filepath, List<string> filetype = null)
        {
            DirectoryAllFiles.FileList.Clear();
            List<FileInformation> list = DirectoryAllFiles.GetAllFiles(new System.IO.DirectoryInfo(filepath));
            List<FileInformation> nlist = new List<FileInformation>();

            if (filetype == null == false)
            {
                nlist.AddRange(list);
                foreach (var autoget in list)
                {
                    if (filetype.Contains(autoget.Filetype) == false)
                    {
                        nlist.Remove(autoget);
                    }
                }
             
                return nlist;
            }
            
            return list;
        }
    }

    public class DirectoryAllFiles
    {
        public static List<FileInformation> FileList = new List<FileInformation>();
        public static List<FileInformation> GetAllFiles(DirectoryInfo dir)
        {

            List<FileInfo> allFile = new List<FileInfo>(); ;

            try
            {
                allFile = dir.GetFiles().ToList();
            }
            catch { }

            foreach (FileInfo fi in allFile)
            {
                FileList.Add(new FileInformation { FileName = fi.Name, FilePath = fi.FullName, Filetype = fi.Extension });
            }

            List<DirectoryInfo> allDir = new List<DirectoryInfo>();

            try
            {
                allDir = dir.GetDirectories().ToList();
            }
            catch { }

            foreach (DirectoryInfo d in allDir)
            {
                GetAllFiles(d);
            }
            return FileList;
        }
    }
    
    public class DirectoryAllFilesCode
    {
        public static List<FileInformation> FileList = new List<FileInformation>();
        public static List<FileInformation> GetAllFiles(DirectoryInfo dir, string dqpath)
        {
            FileInfo[] allFile = dir.GetFiles();
            foreach (FileInfo fi in allFile)
            {
                List<string> AllCode = new List<string>();
                AllCode = DataHelper.ReadFile(fi.FullName, Encoding.UTF8);
                FileList.Add(new FileInformation { FileName = fi.Name, FilePath = fi.FullName.Replace(dqpath, ""), Filetype = fi.Extension, FileCode = AllCode });
            }
            DirectoryInfo[] allDir = dir.GetDirectories();
            foreach (DirectoryInfo d in allDir)
            {
                GetAllFiles(d, dqpath);
            }
            return FileList;
        }
    }

    public class FileInformation
    {
        public string Filetype = "";
        public string FileName = "";
        public string FilePath = "";

        public List<string> FileCode = new List<string>();
    }

    public class DataItem : IComparable<DataItem>
    {
        public string DataPath = "";

        public long LocalTime = 0;

        public long Size = 0;

        public string VirtualPath = "";

        public string DataName = "";

        public int CompareTo(DataItem Other)
        {
            if (this.LocalTime != Other.LocalTime)
            {
                return this.LocalTime.CompareTo(Other.LocalTime);
            }

            else return 0;
        }

        public DataItem(string Content)
        {
            string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            AppData = AppData.Substring(0, AppData.LastIndexOf(@"\") + @"\".Length);

            string GetPath = ConvertHelper.StringDivision(Content, "\"", "\"");

            long Number = 0;

            if (long.TryParse(GetPath, out Number))
            {
                Content = Content.Substring(("\"" + Number + "\"").Length);
                GetPath = ConvertHelper.StringDivision(Content, "\"", "\"");
            }

            string GetInFo = Content.Substring(Content.IndexOf(GetPath) + (GetPath + "\"").Length);

            if (GetInFo.Contains("{")) GetInFo = GetInFo.Split('{')[1];

            foreach (var GetLine in GetInFo.Split(new char[2] { '\r', '\n' }))
            {
                if (GetLine.Contains("\""))
                {
                    string Name = "";
                    string Value = "";

                    foreach (var GetParam in GetLine.Split('"'))
                    {
                        if (GetParam.Trim().Replace(" ", "").Length > 0)
                        {
                            if (Name == "")
                            {
                                Name = GetParam;
                            }
                            else
                            {
                                Value = GetParam;
                                break;
                            }
                        }
                    }

                    if (Name == "size")
                    {
                        this.Size = ConvertHelper.ObjToLong(Value);
                    }
                    if (Name == "localtime")
                    {
                        this.LocalTime = ConvertHelper.ObjToLong(Value);
                    }
                }
            }

            string NewAppPath = AppData + @"Local\" + GetPath.Replace("/", @"\");

            if (File.Exists(NewAppPath))
            {
                this.DataPath = NewAppPath;
                this.DataName = NewAppPath.Substring(this.DataPath.LastIndexOf(@"\") + @"\".Length);
                VirtualPath = @"Local\" + GetPath.Replace("/", @"\");
            }

            NewAppPath = AppData + @"Roaming\" + GetPath.Replace("/", @"\");

            if (File.Exists(NewAppPath))
            {
                this.DataPath = NewAppPath;
                this.DataName = NewAppPath.Substring(this.DataPath.LastIndexOf(@"\") + @"\".Length);
                VirtualPath = @"Roaming\" + GetPath.Replace("/", @"\");
            }

            NewAppPath = AppData + @"LocalLow\" + GetPath.Replace("/", @"\");

            if (File.Exists(NewAppPath))
            {
                this.DataPath = NewAppPath;
                this.DataName = NewAppPath.Substring(this.DataPath.LastIndexOf(@"\") + @"\".Length);
                VirtualPath = @"LocalLow\" + GetPath.Replace("/", @"\");
            }

            NewAppPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + GetPath.Replace("/", @"\");
            if (File.Exists(NewAppPath))
            {
                this.DataPath = NewAppPath;
                this.DataName = NewAppPath.Substring(this.DataPath.LastIndexOf(@"\") + @"\".Length);
                VirtualPath = @"\" + GetPath.Replace("/", @"\");
            }
        }
    }

    public class DataArray : IComparable<DataArray>
    {
        public string DataHash = "";

        public List<DataItem> Datas = new List<DataItem>();

        public long ArrayLocalTime = 0;

        public string WritePath = "";
        public string RuleName = "";

        public int CompareTo(DataArray Other)
        {
            if (this.ArrayLocalTime != Other.ArrayLocalTime)
            {
                return this.ArrayLocalTime.CompareTo(Other.ArrayLocalTime);
            }

            else return 0;
        }


        public DataArray(List<DataItem> Datas)
        {
            this.Datas = Datas;

            long MaxValue = 0;

            DataItem NewIndexData = null;

            if (this.Datas.Count > 0)
            {
                foreach (var Get in this.Datas)
                {
                    if (Get.LocalTime > MaxValue)
                    {
                        MaxValue = Get.LocalTime;
                        NewIndexData = Get;
                    }
                }
            }

            if (NewIndexData == null == false)
            {
                this.ArrayLocalTime = NewIndexData.LocalTime;
                this.WritePath = DataHelper.TryFindSavePath(NewIndexData.DataPath, ref this.RuleName);
            }

            this.DataHash = GetHash();
        }

        public int CopyToPath(string TargetPath)
        {
            if (!TargetPath.EndsWith(@"\"))
            {
                TargetPath += @"\";
            }

            int SucessCount = 0;

            if (Directory.Exists(TargetPath))
            {
                foreach (var GetFile in Datas)
                {
                    if (File.Exists(GetFile.DataPath))
                    {
                        SucessCount++;


                        if (File.Exists(TargetPath + SucessCount + ".Data"))
                        {
                            File.Delete(TargetPath + SucessCount + ".Data");
                        }

                        File.Copy(GetFile.DataPath, TargetPath + SucessCount + ".Data");

                        FileStream NewStream = new FileStream(TargetPath + SucessCount + ".InFo", FileMode.Create, FileAccess.Write);//创建写入文件 
                        StreamWriter NewWriter = new StreamWriter(NewStream);

                        NewWriter.WriteLine(GetFile.VirtualPath);//开始写入值

                        NewWriter.Close();
                        NewStream.Close();
                    }
                }
            }

            return SucessCount;
        }

        public string GetHash()
        {
            return Datas.GetHashCode().ToString().Replace("-", "A");
        }
    }

    public class GameDataControl
    {
        public List<DataArray> Datas = new List<DataArray>();
    }
}
