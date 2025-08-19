using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SSELex.SkyrimModManager
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class FileHelper
    {
        public static void WriteFile(string targetpath, string text, Encoding encodingtype)
        {
            try
            {
                StreamWriter FileWriter = new StreamWriter(targetpath, false, encodingtype);
                FileWriter.Write(text);
                FileWriter.Close();
                FileWriter.Dispose();
            }
            catch { }

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

        public static STreeItem GetPathStruct(string FromPath)
        {
            if (!FromPath.EndsWith(@"\"))
            {
                FromPath += @"\";
            }

            if (Directory.Exists(FromPath))
            {
                OneEnum NOneEnum = new OneEnum();
                NOneEnum.STreeItems.MainPath = FromPath;
                NOneEnum.EnumAny(FromPath);
                return NOneEnum.STreeItems;
            }

            return null;
        }

        public class OneEnum
        {
            public STreeItem STreeItems = new STreeItem();

            public void EnumAny(string FromPath)
            {
                foreach (var GetChildFile in Directory.GetFiles(FromPath))
                {
                    STreeItems.Files.Add(new FileItem(GetChildFile));
                }
                foreach (var GetChildDir in Directory.GetDirectories(FromPath))
                {
                    EnumAny(GetChildDir);
                }
            }
        }

    }


    public enum SourceType
    { 
      Null=0, IsFile = 1,IsPath = 2
    }

    public class FileItem : IComparable<FileItem>
    {
        public string FilePath;

        public int CompareTo(FileItem I)
        {
            if (string.Compare(this.FilePath, I.FilePath, StringComparison.Ordinal) > 0) return 1;
            else if (string.Compare(this.FilePath, I.FilePath, StringComparison.Ordinal) < 0) return -1;
            else return 0;
        }

        public FileItem(string FilePath)
        {
            this.FilePath = FilePath;
        }
        public string GetFilePath()
        {
            return this.FilePath;
        }

        public string GetFilePath(string MainPath)
        {
            return (this.FilePath).Substring(MainPath.Length);
        }
    }
   
    public class STreeItem 
    {
        public string MainPath;
        public List<FileItem> Files = new List<FileItem>();
    }
}
