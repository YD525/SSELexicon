using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YDSkyrimToolR.SkyrimModManager;

namespace YDSkyrimTools.SkyrimModManager
{
    public  class ModHelper
    {

     
        /// <summary>
        /// 对比两个目录的文件差异
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static bool CheckModStruct(STreeItem A,STreeItem B,ref List<ErrorReport> Errors)
        {
            //if (A.Files.GetHashCode() != B.Files.GetHashCode())
            //{
            //    DeFine.WorkWin.ComparePercent.Dispatcher.Invoke(new Action(() => {
            //        DeFine.WorkWin.ComparePercent.Maximum = B.Files.Count;
            //    }));

            //    int CurrentCount = 0;

            //    foreach (var Get in B.Files)
            //    {
            //        string TargetFile = A.MainPath + Get.GetFilePath(B.MainPath);

            //        if (File.Exists(TargetFile))
            //        {
            //            if (!FileHelper.CompareFileContent(TargetFile, Get.GetFilePath()))
            //            {
            //                Errors.Add(new ErrorReport("FileChange",TargetFile));
            //            }
            //        }
            //        else
            //        {
            //            Errors.Add(new ErrorReport("FileLost",TargetFile));
            //        }

            //        CurrentCount++;

            //        DeFine.WorkWin.ComparePercent.Dispatcher.Invoke(new Action(() => {
            //            DeFine.WorkWin.ComparePercent.Value = CurrentCount;
            //        }));
            //    }



            //    if (Errors.Count > 0)
            //    {
            //        return false;
            //    }
            //    else
            //    {
            //        return true;
            //    }
            //}

            //return true;

            return false;
        }
    }


    public class ErrorReport
    {
        public string Error = "";
        public string FilePath = "";

        public ErrorReport(string Error, string FilePath)
        {
            this.Error = Error;
            this.FilePath = FilePath;
        }
    }

  
}
