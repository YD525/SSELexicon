using System.Diagnostics;
using System.IO;
using System.Text;
using SSELex.ConvertManager;
using SSELex.SkyrimModManager;
using SSELex.TranslateManage;

namespace SSELex.SkyrimManage
{

    public class TempPathWrapper
    {
        private readonly List<string> TempPaths = new();
        public string TempRoot = Path.Combine(Path.GetTempPath(), "Champollion_" + Guid.NewGuid().ToString("N"));
        public string CopyToTemp(string OriginalPath)
        {
            if (!File.Exists(OriginalPath) && !Directory.Exists(OriginalPath))
                throw new FileNotFoundException("Path not found: " + OriginalPath);

            if (File.Exists(OriginalPath))
            {
                Directory.CreateDirectory(TempRoot);
                string DestFile = Path.Combine(TempRoot, Path.GetFileName(OriginalPath));
                File.Copy(OriginalPath, DestFile, true);
                TempPaths.Add(DestFile);
                return DestFile;
            }
            else
            {
                CopyDirectory(OriginalPath, TempRoot);
                TempPaths.Add(TempRoot);
                return TempRoot;
            }
        }

        private void CopyDirectory(string sourceDir, string destDir)
        {
            Directory.CreateDirectory(destDir);

            foreach (var dir in Directory.GetDirectories(sourceDir, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dir.Replace(sourceDir, destDir));
            }

            foreach (var file in Directory.GetFiles(sourceDir, "*.*", SearchOption.AllDirectories))
            {
                string destFile = file.Replace(sourceDir, destDir);
                File.Copy(file, destFile, true);
            }
        }
        public void Clean()
        {
            foreach (var Path in TempPaths)
            {
                try
                {
                    if (File.Exists(Path))
                        File.Delete(Path);
                    else if (Directory.Exists(Path))
                        Directory.Delete(Path, true);
                }
                catch { /* 忽略异常 */ }
            }
            TempPaths.Clear();
        }
    }

    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class StringParam
    {
        public string DefLine = "";
        public string Type = "";
        public string EditorID = "";
        public string Key = "";
        public string SourceText = "";
        public string TransText = "";
        public double TranslationSafetyScore = 0;
        public int LineID = 0;


        public string GetTextIfTransR()
        {
            string GetKey = this.Key;
            if (Translator.TransData.ContainsKey(GetKey))
            {
                this.TransText = Translator.TransData[GetKey];
                if (this.TransText.Length > 0)
                {
                    return this.TransText;
                }
                else
                {
                    return "";
                }
            }

            return "";
        }
    }

    public class PexReader
    {
        //E:\ModOrganizer\MO2\Mods\mods
        public string PSCContent = "";
        public string TempFilePath = "";
        public List<string> CodeLines = new List<string>();

        public HeuristicCore HeuristicEngine = null;

        public List<StringParam> Strings = new List<StringParam>();

        public PexReader()
        {

        }
        public void Close()
        {
            if (HeuristicEngine != null)
            {
                HeuristicEngine.DStringItems.Clear();
            }

            Strings.Clear();
            HeuristicEngine = null;
            CurrentFileName = string.Empty;
            Translator.ClearCache();
            DeFine.HideCodeView();
            PSCContent = string.Empty;
            TempFilePath = string.Empty;
            CodeLines.Clear();
        }

        #region ExecuteFunc
        public string Execute(string ExePath, string Args, ref string OutPutMsg)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = ExePath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                Arguments = Args
            };

            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;
                    process.Start();
                    process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                    process.StartInfo.StandardErrorEncoding = Encoding.UTF8;
                    string output = process.StandardOutput.ReadToEnd();
                    OutPutMsg = output.Trim();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    return output;
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public string ExecuteRR(string ExePath, string StartPath, string Args)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = ExePath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = StartPath,
                CreateNoWindow = true,
                Arguments = Args
            };

            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    return output;
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public void Execute(string ExePath, string SourceFilePath, string AssemblyDirectory, string OutPutDirectory)
        {
            TempPathWrapper NTempPathWrapper = new TempPathWrapper();
            string TempFilePath = NTempPathWrapper.CopyToTemp(SourceFilePath);
            string GenParam = string.Format("{0} -p {1} -a {2} -c -t", TempFilePath, NTempPathWrapper.TempRoot, NTempPathWrapper.TempRoot);
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = ExePath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                Arguments = GenParam
            };

            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    Console.WriteLine(output);
                    if (!string.IsNullOrEmpty(error))
                    {
                        Console.WriteLine("Error: " + error);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while executing: " + ex.Message);
            }

            if (File.Exists(TempFilePath))
            {
                var GetInFo = new FileInfo(TempFilePath);
                string GetFileName = GetInFo.Name;
                string GetFilePath = ConvertHelper.ObjToStr(GetInFo.DirectoryName) + @"\";

                if (GetFileName.Contains("."))
                {
                    GetFileName = GetFileName.Substring(0, GetFileName.LastIndexOf("."));
                }

                try
                {
                    if (File.Exists(GetFilePath + GetFileName + ".pas"))
                    {
                        File.Copy(GetFilePath + GetFileName + ".pas", OutPutDirectory + GetFileName + ".pas");
                    }

                    if (File.Exists(GetFilePath + GetFileName + ".psc"))
                    {
                        File.Copy(GetFilePath + GetFileName + ".psc", OutPutDirectory + GetFileName + ".psc");
                    }
                }
                catch { }

                try
                {
                    File.Delete(GetFilePath + GetFileName + ".pas");
                    File.Delete(GetFilePath + GetFileName + ".psc");
                    File.Delete(GetFilePath + GetFileName + ".pex");

                    //CleanTempFile
                    Directory.Delete(NTempPathWrapper.TempRoot);
                }
                catch (Exception Ex)
                {
                }
            }

        }

        #endregion
        public void LoadPexFile(string FilePath)
        {
            Close();

            HeuristicEngine = new HeuristicCore();

            foreach (var GetFile in DataHelper.GetAllFile(DeFine.GetFullPath(@"Cache\")))
            {
                if (File.Exists(GetFile.FilePath) && GetFile.FilePath.Contains(DeFine.GetFullPath(@"Cache\")))
                {
                    File.Delete(GetFile.FilePath);
                }
            }
            this.CodeLines.Clear();
            this.PSCContent = string.Empty;
            if (DeCodeFileToUsing(FilePath))
            {
                bool HaveError = false;
                if (GenFileToPAS(FilePath, ref HaveError))
                {
                    if (HaveError)
                    {
                        new Thread(() =>
                        {
                            Thread.Sleep(500);
                            System.Windows.MessageBox.Show("To help you roll back the operation.Reason:Decompilation failed");
                            DeFine.WorkingWin.CancelAny();
                        }).Start();
                        return;
                    }
                    ProcessCode();
                }
            }
        }
        public string CurrentFileName = "";
        public bool DeCodeFileToUsing(string FilePath)
        {
            if (!File.Exists(FilePath)) return false;
            string GetFilePath = FilePath.Substring(0, FilePath.LastIndexOf(@"\"));
            string GetFileFullName = FilePath.Substring(FilePath.LastIndexOf(@"\") + @"\".Length);
            string GetFileType = GetFileFullName.Split('.')[1];
            string GetFileName = GetFileFullName.Split('.')[0];
            CurrentFileName = GetFileName;

            if (File.Exists(DeFine.GetFullPath(@"Cache\") + GetFileName + "." + GetFileType))
            {
                File.Delete(DeFine.GetFullPath(@"Cache\") + GetFileName + "." + GetFileType);
            }

            File.Copy(FilePath, DeFine.GetFullPath(@"Cache\") + GetFileName + "." + GetFileType);
            TempFilePath = DeFine.GetFullPath(@"Cache\") + GetFileName + "." + GetFileType;
            GetFilePath = DeFine.GetFullPath(@"Cache\");

            string TempPath = DeFine.GetFullPath(@"Cache\") + GetFileName + "." + GetFileType;
            Execute(DeFine.GetFullPath(@"Tool\Champollion.exe"), TempPath, DeFine.GetFullPath(@"Cache\"), DeFine.GetFullPath(@"Cache\"));
            Thread.Sleep(500);
            string GetPscFile = GetFilePath + GetFileName + ".psc";
            if (File.Exists(GetPscFile))
            {
                var FileData = DataHelper.GetBytesByFilePath(GetPscFile);
                this.PSCContent = DataHelper.GetFileEncodeType(GetPscFile).GetString(FileData);

                if (File.Exists(GetPscFile))
                {
                    File.Delete(GetPscFile);
                }

                if (File.Exists(GetFilePath + @"\" + GetFileName + ".pas"))
                {
                    File.Delete(GetFilePath + @"\" + GetFileName + ".pas");
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GenFileToPAS(string FilePath, ref bool HaveError)
        {
            string CompilerPath = "";
            if (SkyrimHelper.FindPapyrusCompilerPath(ref CompilerPath))
            {
                string GetFilePath = TempFilePath.Substring(0, TempFilePath.LastIndexOf(@"\"));
                string GetFileFullName = TempFilePath.Substring(TempFilePath.LastIndexOf(@"\") + @"\".Length);
                string GetFileType = GetFileFullName.Split('.')[1];
                string GetFileName = GetFileFullName.Split('.')[0];

                string PapyrusAssembler = CompilerPath;
                string GetWorkPath = FilePath.Substring(0, FilePath.LastIndexOf(@"\"));
                string GenParam = string.Format("\"{0}\" -D -Q", GetFileName);
                //string GenParam = string.Format("\"{0}\" -D\n\"{1}\"", GetFileName, GetWorkPath + @"\");

                if (File.Exists(DeFine.GetFullPath(@"\") + GetFileName + "." + GetFileType))
                {
                    File.Delete(DeFine.GetFullPath(@"\") + GetFileName + "." + GetFileType);
                }

                File.Copy(TempFilePath, DeFine.GetFullPath(@"\") + GetFileName + "." + GetFileType);

                string OutPutMsg = "";

                Execute(PapyrusAssembler, GenParam, ref OutPutMsg);

                Thread.Sleep(500);

                string SetDisassemblePath = DeFine.GetFullPath(GetFileName + "." + "disassemble.pas");

                if (File.Exists(SetDisassemblePath))
                {
                    string SetCachePas = DeFine.GetFullPath(@"Cache\") + GetFileName + "." + "pas";
                    if (File.Exists(SetCachePas))
                    {
                        File.Delete(SetCachePas);
                    }
                    File.Copy(SetDisassemblePath, SetCachePas);
                    if (File.Exists(SetDisassemblePath))
                    {
                        File.Delete(SetDisassemblePath);
                    }
                    string SetSource = DeFine.GetFullPath(GetFileName + "." + GetFileType);
                    if (File.Exists(SetSource))
                    {
                        File.Delete(SetSource);
                    }
                    if (File.Exists(TempFilePath))
                    {
                        if (TempFilePath.Contains(DeFine.GetFullPath(@"\Cache\")))
                        {
                            File.Delete(TempFilePath);

                            HaveError = false;
                            return true;
                        }
                    }
                }
            }
            HaveError = true;
            return false;
        }

        public class StringValue
        {
            public string Key = "";
            public int DefID = 0;
            public string SourceStr = "";

            public StringValue(string key, int defID, string sourceStr)
            {
                Key = key;
                DefID = defID;
                SourceStr = sourceStr;
            }
        }

        public void ProcessCode()
        {
            DeFine.ShowCodeView();

            foreach (var GetLine in this.PSCContent.Split('\n'))
            {
                if (!GetLine.Trim().StartsWith(";"))
                {
                    this.CodeLines.Add(GetLine.Trim());
                }
            }

            string RichText = "";
            foreach (var GetLine in this.CodeLines)
            {
                RichText += GetLine.Trim() + "\r\n";
            }

            if (DeFine.CurrentCodeView != null)
            {
                DeFine.CurrentCodeView.Dispatcher.Invoke(new Action(() =>
                {
                    DeFine.ActiveIDE.Text = RichText;
                    DeFine.CurrentCodeView.ReSetFolding();
                }));
            }

            HeuristicEngine.AnalyzeCodeLine(this.CodeLines);
            for (int i = 0; i < HeuristicEngine.DStringItems.Count; i++)
            {
                if ((HeuristicEngine.DStringItems[i].LineID > 0 || HeuristicEngine.DStringItems[i].LineID == -1) && HeuristicEngine.DStringItems[i].Str.Trim().Length > 0)
                {
                    StringParam NStringParam = new StringParam();
                    NStringParam.DefLine = HeuristicEngine.DStringItems[i].SourceLine;
                    NStringParam.SourceText = HeuristicEngine.DStringItems[i].Str;
                    NStringParam.Key = HeuristicEngine.DStringItems[i].Key + ",Score:" + HeuristicEngine.DStringItems[i].TranslationSafetyScore;
                    NStringParam.TranslationSafetyScore = HeuristicEngine.DStringItems[i].TranslationSafetyScore;
                    NStringParam.EditorID = HeuristicEngine.DStringItems[i].Feature;
                    NStringParam.Type = "Papyrus";
                    NStringParam.LineID = HeuristicEngine.DStringItems[i].LineID;
                    Strings.Add(NStringParam);
                }
            }
        }

        public class LinkValue
        {
            public string Value = "";
            public int DefLineID = 0;
            public string DefKey = "";
            public LinkValue(string Value, int DefLineID,string DefKey)
            {
                this.Value = Value;
                this.DefLineID = DefLineID;
                this.DefKey = DefKey;
            }
        }

        public string GetSourceStr(List<string> Lines)
        {
            string RichText = string.Empty;
            foreach (var Get in Lines)
            {
                if (Get.Trim().Length > 0)
                {
                    RichText += (Get + "\n");
                }
                else
                {
                    RichText += "\n";
                }
            }
            if (RichText.EndsWith("\n"))
            {
                RichText = RichText.Substring(0, RichText.Length - 1);
            }
            return RichText;
        }

        public void CheckBatSafe(string BatPath, string CheckStr)
        {
        NextCheck:
            if (!File.Exists(BatPath))
            {
                DataHelper.WriteFile(BatPath, Encoding.UTF8.GetBytes(CheckStr));
            }
            else
            {
                string CheckFileContent = DataHelper.ReadFileByStr(BatPath, Encoding.UTF8);
                if (CheckFileContent != CheckStr)
                {
                    //Ensure bat file security
                    File.Delete(BatPath);
                    goto NextCheck;
                }
            }
        }

        public bool SavePexFile(string OutPutPath)
        {
            bool Sucess = false;
            Dictionary<string, LinkValue> LinkTexts = new Dictionary<string, LinkValue>();
            foreach (var GetParam in this.Strings)
            {
                if (Translator.TransData.ContainsKey(GetParam.Key))
                {
                    if (!LinkTexts.ContainsKey(GetParam.SourceText))
                        LinkTexts.Add(GetParam.SourceText, new LinkValue(Translator.TransData[GetParam.Key], GetParam.LineID, GetParam.Key));
                }
            }

            var Encoding = DataHelper.GetFileEncodeType(DeFine.GetFullPath(@"\Cache\" + CurrentFileName + ".pas"));

            string GetFileContent = Encoding.GetString(DataHelper.GetBytesByFilePath(DeFine.GetFullPath(@"\Cache\" + CurrentFileName + ".pas")));

            List<string> Lines = GetFileContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();

            for (int i = 0; i < LinkTexts.Count; i++)
            {
                string GetKey = LinkTexts.ElementAt(i).Key;
                if (GetFileContent.Contains(GetKey))
                {
                    var GetLinkValue = LinkTexts[GetKey];
                    string NewStr = GetLinkValue.Value;
                    TranslationPreprocessor.NormalizePunctuation(ref NewStr);
                    GetLinkValue.Value = NewStr;
                    LinkTexts[GetKey] = GetLinkValue;

                    for (int ir = 0; ir < Lines.Count; ir++)
                    {
                        try
                        {
                            string GetLine = Lines[ir];
                            if (GetLine.Contains("@line") && GetLinkValue.Value.Trim().Length > 0)
                            {
                                string CheckID = GetLine.Substring(GetLine.IndexOf("@line") + "@line".Length).Trim();
                                if (CheckID.Equals(GetLinkValue.DefLineID.ToString()))
                                {
                                    if (Lines[ir].Contains(GetKey))
                                    {
                                        Lines[ir] = Lines[ir].Replace("\"" + GetKey + "\"", "\"" + GetLinkValue.Value + "\"");
                                    }
                                }
                                else
                                {
                                    if (Lines[ir].Contains(GetKey))
                                    {
                                        string QueryKey = LinkTexts[GetKey].DefKey;
                                        QueryKey = QueryKey.Substring(0,QueryKey.LastIndexOf(","));

                                        foreach (var GetItem in HeuristicEngine.DStringItems)
                                        {
                                            if (GetItem.Key.Equals(QueryKey))
                                            {
                                                int CheckFunc = 0;
                                                foreach (var GetParam in GetItem.Feature.Split('>'))
                                                {
                                                    if (GetParam.Trim().Length > 0)
                                                    {
                                                        if (GetLine.Contains(GetParam.Trim()))
                                                        {
                                                            CheckFunc++;
                                                        }
                                                    }
                                                }
                                                if (CheckFunc > 0)
                                                {
                                                    Lines[ir] = Lines[ir].Replace("\"" + GetKey + "\"", "\"" + GetLinkValue.Value + "\"");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception Ex)
                        {

                        }
                    }
                }
            }

            GetFileContent = string.Empty;

            GetFileContent = GetSourceStr(Lines);

            string SetPath = DeFine.GetFullPath(@"\Cache\" + CurrentFileName + ".pas");

            if (File.Exists(SetPath))
            {
                File.Delete(SetPath);
            }

            if (File.Exists(DeFine.GetFullPath(@"\") + CurrentFileName + ".pas"))
            {
                File.Delete(DeFine.GetFullPath(@"\") + CurrentFileName + ".pas");
            }

            DataHelper.WriteFile(DeFine.GetFullPath(@"\") + CurrentFileName + ".pas", Encoding.GetBytes(GetFileContent));

            string CompilerPath = "";
            if (SkyrimHelper.FindPapyrusCompilerPath(ref CompilerPath))
            {
                if (File.Exists(OutPutPath + ".backup"))
                {
                    File.Delete(OutPutPath + ".backup");
                }
                File.Copy(OutPutPath, OutPutPath + ".backup");
                File.Delete(OutPutPath);

                string GetWorkPath = CompilerPath.Substring(0, CompilerPath.LastIndexOf(@"\") + @"\".Length);

                if (File.Exists(GetWorkPath + CurrentFileName + ".pas"))
                {
                    File.Delete(GetWorkPath + CurrentFileName + ".pas");
                }

                File.Copy(DeFine.GetFullPath(@"\" + CurrentFileName + ".pas"), GetWorkPath + CurrentFileName + ".pas");

                if (File.Exists(DeFine.GetFullPath(@"\" + CurrentFileName + ".pas")))
                {
                    File.Delete(DeFine.GetFullPath(@"\" + CurrentFileName + ".pas"));
                }

                var Result = ExecuteRR(CompilerPath, GetWorkPath, "\"" + CurrentFileName + "\"");

                if (File.Exists(GetWorkPath + CurrentFileName + ".pas"))
                {
                    File.Delete(GetWorkPath + CurrentFileName + ".pas");
                }

                string SetOutPutFile = GetWorkPath + CurrentFileName + ".pex";

                if (File.Exists(SetOutPutFile))
                {
                    File.Copy(SetOutPutFile, OutPutPath);
                    Sucess = true;
                }
                else
                {
                    try
                    {
                        File.Copy(OutPutPath + ".backup", OutPutPath);
                    }
                    catch { }
                }

                if (File.Exists(SetOutPutFile))
                {
                    File.Delete(SetOutPutFile);
                }

                Close();
            }
            return Sucess;
        }
    }



    public class CallItem
    {
        public string Call = "";
        public string DEBUGLineID = "";
    }
}