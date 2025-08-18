using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using PhoenixEngine.EngineManagement;
using System.Security.Cryptography;
using SSELex.SkyrimModManager;

namespace SSELex.TranslateManagement
{
    public class ToolDownloader
    {
        public static string ChampollionMd5Sign = "bab15b1f4c45b41fbb024bd61087dab6";
        public static string PapyrusAssemblerMD5Sign = "55a426bda1af9101ad5359f276805ab6";
        public static string GetMD5(byte[] Data)
        {
            using (var Md5 = MD5.Create())
            {
                byte[] Hash = Md5.ComputeHash(Data);
                StringBuilder Sb = new StringBuilder();
                foreach (byte B in Hash)
                {
                    Sb.Append(B.ToString("x2"));
                }
                return Sb.ToString();
            }
        }

        //https://github.com/Orvid/Champollion/releases/download/v1.3.2/Champollion.v1.3.2.zip

        public static void DownloadAndExtract(string Url, string DestinationFolder, IWebProxy? Proxy = null)
        {
            string TempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".zip");

            try
            {
                var Handler = new HttpClientHandler();
                if (Proxy != null)
                {
                    Handler.Proxy = Proxy;
                    Handler.UseProxy = true;
                }

                using (var HttpClient = new HttpClient(Handler))
                using (var Response = HttpClient.GetAsync(Url).GetAwaiter().GetResult())
                {
                    Response.EnsureSuccessStatusCode();
                    using (var FS = new FileStream(TempFile, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        Response.Content.CopyToAsync(FS).GetAwaiter().GetResult();
                    }
                }

                if (!Directory.Exists(DestinationFolder))
                    Directory.CreateDirectory(DestinationFolder);

                ZipFile.ExtractToDirectory(TempFile, DestinationFolder, true);
            }
            finally
            {
                if (File.Exists(TempFile))
                    File.Delete(TempFile);
            }
        }


        public static bool DownloadChampollion()
        {
            WebProxy SetProxy = null;

            if (EngineConfig.ProxyIP.Trim().Length > 0)
            {
                SetProxy = new WebProxy(EngineConfig.ProxyIP);
            }

            string SetFolder = DeFine.GetFullPath(@"Tool\");

            DownloadAndExtract(
                "https://github.com/Orvid/Champollion/releases/download/v1.3.2/Champollion.v1.3.2.zip",
                 SetFolder,
                 SetProxy
                );

            string SetFilePath = DeFine.GetFullPath(@"Tool\Champollion.exe");
            if (File.Exists(SetFilePath))
            {
                byte[] ReadFileData = DataHelper.GetBytesByFilePath(SetFilePath);

                string CurrentMD5 = GetMD5(ReadFileData);

                // Verify the downloaded file's MD5 signature to ensure its integrity and authenticity.
                // Purpose: Protect users from potentially malicious files if the remote repository 
                // (e.g., GitHub) updates or tampers with the file.
                if (CurrentMD5.Equals(ChampollionMd5Sign))
                {
                    return true;
                }
                else
                {
                    //Deleting files from unknown sources
                    foreach (var GetFile in DataHelper.GetAllFile(DeFine.GetFullPath(@"Tool\"),new List<string>() { ".exe" }))
                    {
                        if (GetFile.FileName != "PapyrusAssembler.exe")
                        {
                            if (File.Exists(GetFile.FilePath))
                            {
                                File.Delete(GetFile.FilePath);
                            }
                        }
                        else
                        { 
                            string SetPapyrusAssembler  = GetFile.FilePath;
                            string GetPapyrusAssemblerMD5 = GetMD5(DataHelper.GetBytesByFilePath(SetPapyrusAssembler));

                            if (!GetPapyrusAssemblerMD5.Equals(PapyrusAssemblerMD5Sign))
                            {
                                if (File.Exists(GetFile.FilePath))
                                {
                                    File.Delete(GetFile.FilePath);
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}
