using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SSELex.FileManagement
{
    public class ExplorerHelper
    {
        public static bool OpenUrl(string Url)
        {
            if (string.IsNullOrWhiteSpace(Url)) return false;

            //Make sure Url is a URL
            if (Uri.TryCreate(Url, UriKind.Absolute, out Uri UriResult) &&
                (UriResult.Scheme == Uri.UriSchemeHttp || UriResult.Scheme == Uri.UriSchemeHttps))
            {
                try
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = UriResult.ToString(),
                        UseShellExecute = true //Let the operating system decide which program to open
                    };

                    Process.Start(psi);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
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
