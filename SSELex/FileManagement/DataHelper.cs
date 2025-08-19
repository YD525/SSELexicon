
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using SSELex.ConvertManager;
using System.Windows.Forms;

namespace SSELex.SkyrimModManager
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class DataHelper
    {
        public static string ShowSaveFileDialog(string defaultFileName, string filter = "All files (*.*)|*.*")
        {
            using (var saveFileDialog = new System.Windows.Forms.SaveFileDialog())
            {
                saveFileDialog.FileName = defaultFileName;
                saveFileDialog.Filter = filter;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return saveFileDialog.FileName;
                }
                else
                {
                    return null;
                }
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
        public static string ShowFileDialog(string SelectType,string Text)
        {
            try { 
            Microsoft.Win32.OpenFileDialog FileDialog = new Microsoft.Win32.OpenFileDialog();
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
        public static byte[] ReadFile(string Path)
        {
            byte[] Data = null;

            if (File.Exists(Path))
            {
                using (FileStream FS =
                          new FileStream(Path, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader BR = new BinaryReader(FS))
                    {
                        Data = BR.ReadBytes((int)FS.Length);
                    }
                }
            }
            else
            {
                return new byte[0];
            }

            return Data;
        }
        public static void WriteFile(string TargetPath, byte[] Data)
        {
            FileStream FS = new FileStream(TargetPath, FileMode.Create);
            FS.Write(Data, 0, Data.Length);
            FS.Close();
            FS.Dispose();
        }
        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
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
            if (buffer[0] == 0x3c)//UTF-8 without BOM
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
       
        public static void CopyDir(string Path, string TargetPath)
        {
            try
            {
                if (TargetPath[TargetPath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                {
                    TargetPath += System.IO.Path.DirectorySeparatorChar;
                }
                if (!System.IO.Directory.Exists(TargetPath))
                {
                    System.IO.Directory.CreateDirectory(TargetPath);
                }
                string[] FileList = System.IO.Directory.GetFileSystemEntries(Path);
                foreach (string File in FileList)
                {
                    if (System.IO.Directory.Exists(File))
                    {
                        CopyDir(File, TargetPath + System.IO.Path.GetFileName(File));
                    }
                    else
                    {
                        System.IO.File.Copy(File, TargetPath + System.IO.Path.GetFileName(File), true);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
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

   
}
