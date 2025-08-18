using System.IO;
using System.Net.Http;
using System.Net;
using System.Text;
using System.IO.Compression;
using PhoenixEngine.EngineManagement;
using System.Security.Cryptography;
using SSELex.SkyrimModManager;
using System.Windows;

namespace SSELex.FileManagement
{
    public class ToolDownloader
    {
        public static string ChampollionMd5Sign = "bab15b1f4c45b41fbb024bd61087dab6";//v1.3.2
        public static string PapyrusAssemblerMD5Sign = "55a426bda1af9101ad5359f276805ab6";
        public static string ScriptCompileMD5Sign = "9774f28bb11963ca3fb06797bbbc33ec";
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
                {
                    HttpClient.Timeout = TimeSpan.FromSeconds(20);

                    using (var Response = HttpClient.GetAsync(Url).GetAwaiter().GetResult())
                    {
                        Response.EnsureSuccessStatusCode();
                        using (var FS = new FileStream(TempFile, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            Response.Content.CopyToAsync(FS).GetAwaiter().GetResult();
                        }
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

        public static bool? SCanToolPath()
        {
            //Deleting files from unknown sources
            //\SSE Lexicon\Tool

            // Only delete .exe files in the Tool folder that are NOT in the whitelist.
            // This ensures that any other programs or user-added executables in the Tool folder remain untouched.

            List<string> WhiteList = new List<string>() { "antlr.runtime.dll", "Antlr3.Runtime.dll", "Antlr3.Utility.dll",
                        "PapyrusAssembler.exe","Champollion.exe",
                        "PCompiler.dll", "ScriptCompile.bat", "StringTemplate.dll", "TESV_Papyrus_Flags.flg" };

            int OtherFileCount = 0;

            foreach (var GetFile in DataHelper.GetAllFile(DeFine.GetFullPath(@"Tool\")))
            {
                if (!WhiteList.Contains(GetFile.FileName))
                {
                    MessageBox.Show("Unknown file:" + GetFile.FilePath);
                    OtherFileCount++;
                }
                else
                if (GetFile.FileName.Equals("PapyrusAssembler.exe"))
                {
                    string SetPapyrusAssembler = GetFile.FilePath;
                    string GetPapyrusAssemblerMD5 = GetMD5(DataHelper.GetBytesByFilePath(SetPapyrusAssembler));

                    if (!GetPapyrusAssemblerMD5.Equals(PapyrusAssemblerMD5Sign))
                    {
                        //if (File.Exists(GetFile.FilePath))
                        //{
                        //    File.Delete(GetFile.FilePath);
                        //}

                        MessageBox.Show("The tool directory cannot be recognized. Please delete the installation directory and download the program again.");
                        DeFine.CloseAny();
                        return null;
                    }
                }
                else
                if (GetFile.FileName.Equals("Champollion.exe"))
                {
                    string SetChampollion = GetFile.FilePath;
                    string GetChampollionMD5 = GetMD5(DataHelper.GetBytesByFilePath(SetChampollion));

                    if (!GetChampollionMD5.Equals(ChampollionMd5Sign))
                    {
                        if (GetFile.FilePath.Equals(DeFine.GetFullPath(@"Tool\Champollion.exe")))
                        {
                            if (File.Exists(GetFile.FilePath))
                            {
                                File.Delete(GetFile.FilePath);
                            }
                        }

                        MessageBox.Show("Champollion signature verification failed.");
                        return false;
                    }
                }
                else
                if (GetFile.FileName.Equals("ScriptCompile.bat"))
                {
                    string SetScriptCompile = GetFile.FilePath;
                    string GetScriptCompileMD5 = GetMD5(DataHelper.GetBytesByFilePath(SetScriptCompile));

                    if (!GetScriptCompileMD5.Equals(ScriptCompileMD5Sign))
                    {
                        //if (File.Exists(GetFile.FilePath))
                        //{
                        //    File.Delete(GetFile.FilePath);
                        //}

                        MessageBox.Show("The tool directory cannot be recognized. Please delete the installation directory and download the program again.");
                        DeFine.CloseAny();
                        return null;
                    }
                }
            }

            if (OtherFileCount == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static bool DownloadChampollion()
        {
            WebProxy? SetProxy = null;

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
                    if (SCanToolPath() == true)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
