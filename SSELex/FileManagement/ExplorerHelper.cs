using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSELex.FileManagement
{
    public class ExplorerHelper
    {
        public static bool OpenUrl(string Url)
        {
            try
            {
                var PSI = new ProcessStartInfo
                {
                    FileName = Url,
                    UseShellExecute = true  //Let the operating system decide which program to open
                };

                Process.Start(PSI);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool SelectFile(string FilePath)
        {
            try
            {
                var PSI = new ProcessStartInfo
                {
                    FileName = "explorer.exe",
                    Arguments = "/select,\"" + FilePath + "\""
                };
                Process.Start(PSI);

                return true;
            }
            catch(Exception e) 
            {
                return false;
            }
        }
    }
}
