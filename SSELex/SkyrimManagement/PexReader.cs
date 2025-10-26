using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Noggog;
using PhoenixEngine.TranslateManage;
using SSELex.ConvertManager;
using SSELex.SkyrimModManager;
using SSELex.TranslateManage;
using SSELex.UIManagement;
using static PhoenixEngine.SSELexiconBridge.NativeBridge;

namespace SSELex.SkyrimManage
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/SSELexicon
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
    }

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
            if (this.TransText.Length == 0)
            {
                string GetKey = this.Key;
                var GetResult = TranslatorBridge.GetTranslatorCache(GetKey);
                if (GetResult != null)
                {
                    this.TransText = GetResult;
                    if (this.TransText.Length > 0)
                    {
                        return this.TransText;
                    }
                    else
                    {
                        return "";
                    }
                }
            }

            return "";
        }
    }

    public class PathInFo
    {
        public string FilePath = "";
        public string FileFullName = "";

        public string FileName = "";
        public string FileType = "";
        public PathInFo(string Path)
        {
            this.FilePath = Path.Substring(0, Path.LastIndexOf(@"\"));
            this.FileFullName = Path.Substring(Path.LastIndexOf(@"\") + @"\".Length);

            this.FileName = this.FileFullName.Substring(0, this.FileFullName.LastIndexOf("."));
            this.FileType = this.FileFullName.Substring(this.FileFullName.LastIndexOf(".") + ".".Length);
        }
    }

    public class PexReader
    {
        public string PSCContent = "";
        public string TempFilePath = "";
        public List<string> CodeLines = new List<string>();

        public PapyrusHeurCore HeuristicEngine = null;

        public List<StringParam> Strings = new List<StringParam>();

        public PexReader()
        {

        }
        public void Close()
        {
            TranslatorExtend.ClearTranslatorHistoryCache();
            if (HeuristicEngine != null)
            {
                HeuristicEngine.DStringItems.Clear();
            }

            Strings.Clear();
            HeuristicEngine = null;
            CurrentFileName = string.Empty;
            TranslatorBridge.ClearCache();
            DeFine.HideCodeView();
            PSCContent = string.Empty;
            TempFilePath = string.Empty;
            CodeLines.Clear();
        }


        public static void ForceDelete(string FilePath)
        {
            if (!File.Exists(FilePath))
                return;

            try
            {
                File.SetAttributes(FilePath, FileAttributes.Normal);

                File.Delete(FilePath);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region ExecuteFunc
        public string CallPapyrusAssemblerByReturn(string ExePath, string Args, ref string OutPutMsg)
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

        public string CallPapyrusAssembler(string ExePath, string StartPath, string Args)
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

        public void CallChampollion(string ExePath, string SourceFilePath, string AssemblyDirectory, string OutPutDirectory)
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
                    //Clean up the remaining files generated by Champollion

                    //Clean up all middle-tier files
                    ForceDelete(GetFilePath + GetFileName + ".pas");
                    ForceDelete(GetFilePath + GetFileName + ".psc");
                    ForceDelete(GetFilePath + GetFileName + ".pex");

                    //Cleaning up temporary files
                    if (NTempPathWrapper.TempRoot.Length > 0)
                    {
                        Directory.Delete(NTempPathWrapper.TempRoot);
                    }
                }
                catch (Exception Ex)
                {
                }
            }

        }

        #endregion
        public void LoadPexFile(string FilePath)
        {
            TranslatorExtend.ClearTranslatorHistoryCache();
            Close();

            HeuristicEngine = new PapyrusHeurCore();

            foreach (var GetFile in DataHelper.GetAllFile(DeFine.GetFullPath(@"Cache\")))
            {
                if (File.Exists(GetFile.FilePath) && GetFile.FilePath.Contains(DeFine.GetFullPath(@"Cache\")))
                {
                    ForceDelete(GetFile.FilePath);
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

        public void WaitChampollion()
        {
            while (Process.GetProcessesByName("Champollion").Length > 0)
            {
                Thread.Sleep(100);
            }
        }

        public string CurrentFileName = "";
        public bool DeCodeFileToUsing(string FilePath)
        {
            if (!File.Exists(FilePath)) return false;

            PathInFo PathInFo = new PathInFo(FilePath);

            CurrentFileName = PathInFo.FileName;

            if (File.Exists(DeFine.GetFullPath(@"Cache\") + PathInFo.FileName + "." + PathInFo.FileType))
            {
                ForceDelete(DeFine.GetFullPath(@"Cache\") + PathInFo.FileName + "." + PathInFo.FileType);
            }

            File.Copy(FilePath, DeFine.GetFullPath(@"Cache\") + PathInFo.FileName + "." + PathInFo.FileType);
            TempFilePath = DeFine.GetFullPath(@"Cache\") + PathInFo.FileName + "." + PathInFo.FileType;

            string TempPath = DeFine.GetFullPath(@"Cache\") + PathInFo.FileName + "." + PathInFo.FileType;
            CallChampollion(DeFine.GetFullPath(@"Tool\Champollion.exe"), TempPath, DeFine.GetFullPath(@"Cache\"), DeFine.GetFullPath(@"Cache\"));

            //Wait for Champollion decompilation to complete
            WaitChampollion();

            string CurrentPath = DeFine.GetFullPath(@"Cache\");

            string GetPscFile = CurrentPath + PathInFo.FileName + ".psc";

            if (File.Exists(GetPscFile))
            {
                var FileData = DataHelper.ReadFile(GetPscFile);
                this.PSCContent = DataHelper.GetFileEncodeType(GetPscFile).GetString(FileData);

                if (File.Exists(GetPscFile))
                {
                    ForceDelete(GetPscFile);
                }

                if (File.Exists(CurrentPath + PathInFo.FileName + ".pas"))
                {
                    ForceDelete(CurrentPath + PathInFo.FileName + ".pas");
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
                PathInFo TempPathInFo = new PathInFo(TempFilePath);

                string PapyrusAssembler = CompilerPath;
                string GetWorkPath = FilePath.Substring(0, FilePath.LastIndexOf(@"\"));
                string GenParam = string.Format("\"{0}\" -D -Q", TempPathInFo.FileName);
                //string GenParam = string.Format("\"{0}\" -D\n\"{1}\"", GetFileName, GetWorkPath + @"\");

                if (File.Exists(DeFine.GetFullPath(@"\") + TempPathInFo.FileName + "." + TempPathInFo.FileType))
                {
                    ForceDelete(DeFine.GetFullPath(@"\") + TempPathInFo.FileName + "." + TempPathInFo.FileType);
                }

                File.Copy(TempFilePath, DeFine.GetFullPath(@"\") + TempPathInFo.FileName + "." + TempPathInFo.FileType);

                string OutPutMsg = "";

                CallPapyrusAssemblerByReturn(PapyrusAssembler, GenParam, ref OutPutMsg);

                Thread.Sleep(500);

                string SetDisassemblePath = DeFine.GetFullPath(TempPathInFo.FileName + "." + "disassemble.pas");

                if (File.Exists(SetDisassemblePath))
                {
                    string SetCachePas = DeFine.GetFullPath(@"Cache\") + TempPathInFo.FileName + "." + "pas";
                    if (File.Exists(SetCachePas))
                    {
                        ForceDelete(SetCachePas);
                    }
                    File.Copy(SetDisassemblePath, SetCachePas);
                    if (File.Exists(SetDisassemblePath))
                    {
                        ForceDelete(SetDisassemblePath);
                    }
                    string SetSource = DeFine.GetFullPath(TempPathInFo.FileName + "." + TempPathInFo.FileType);
                    if (File.Exists(SetSource))
                    {
                        ForceDelete(SetSource);
                    }
                    if (File.Exists(TempFilePath))
                    {
                        if (TempFilePath.Contains(DeFine.GetFullPath(@"\Cache\")))
                        {
                            ForceDelete(TempFilePath);

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
                if (DeFine.GlobalLocalSetting.ShowCode)
                {
                    double GetHeight = DeFine.WorkingWin.Height;
                    double GetTop = DeFine.WorkingWin.Top;
                    double GetLeft = DeFine.WorkingWin.Left;

                    DeFine.CurrentCodeView.Dispatcher.Invoke(new Action(() =>
                    {
                        DeFine.CurrentCodeView.Height = GetHeight;
                        DeFine.CurrentCodeView.Show();
                        DeFine.ActiveIDE.Text = RichText;
                        DeFine.CurrentCodeView.ReSetFolding();
                        DeFine.CurrentCodeView.Left = GetLeft - DeFine.CurrentCodeView.Width;
                        DeFine.CurrentCodeView.Top = GetTop;
                    }));
                }
            }

            HeuristicEngine.AnalyzeCodeLine(this.CodeLines);
            for (int i = 0; i < HeuristicEngine.DStringItems.Count; i++)
            {
                if ((HeuristicEngine.DStringItems[i].LineID > 0 || HeuristicEngine.DStringItems[i].LineID == -1) && HeuristicEngine.DStringItems[i].Str.Trim().Length > 0)
                {
                    StringParam NStringParam = new StringParam();
                    NStringParam.DefLine = HeuristicEngine.DStringItems[i].SourceLine;
                    NStringParam.SourceText = HeuristicEngine.DStringItems[i].Str;
                    NStringParam.Key = HeuristicEngine.DStringItems[i].Key;
                    NStringParam.TranslationSafetyScore = HeuristicEngine.DStringItems[i].TranslationSafetyScore;
                    NStringParam.EditorID = HeuristicEngine.DStringItems[i].Feature;
                    NStringParam.Type = "Papyrus";
                    NStringParam.LineID = HeuristicEngine.DStringItems[i].LineID;
                    Strings.Add(NStringParam);
                }
            }
            GC.Collect();
        }

        public class LinkValue
        {
            public string Original = "";
            public string Translated = "";
            public int DefLineID = 0;
            public string DefKey = "";
            public LinkValue(string Original, string Translated, int DefLineID,string DefKey)
            {
                this.Original = Original;
                this.Translated = Translated;
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

        public bool SavePexFile(string OutPutPath)
        {
            TranslatorExtend.ClearTranslatorHistoryCache();
            bool Sucess = false;

            List<LinkValue> LinkTexts = new List<LinkValue>();

            foreach (var GetParam in this.Strings)
            {
                if (Translator.TransData.ContainsKey(GetParam.Key))
                {
                    LinkTexts.Add(new LinkValue(GetParam.SourceText, Translator.TransData[GetParam.Key], GetParam.LineID, GetParam.Key));
                }
            }

            var Encoding = DataHelper.GetFileEncodeType(DeFine.GetFullPath(@"\Cache\" + CurrentFileName + ".pas"));

            string GetFileContent = Encoding.GetString(DataHelper.ReadFile(DeFine.GetFullPath(@"\Cache\" + CurrentFileName + ".pas")));

            List<string> Lines = GetFileContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();

            for (int i = 0; i < LinkTexts.Count; i++)
            {
                string GetOriginal = LinkTexts[i].Original;

                if (GetFileContent.Contains(GetOriginal))
                {
                    string GetTranslated = LinkTexts[i].Translated;

                    TranslationPreprocessor.NormalizePunctuation(ref GetTranslated);

                    for (int ir = 0; ir < Lines.Count; ir++)
                    {
                        try
                        {
                            string GetLine = Lines[ir];
                            if (GetLine.Contains("@line"))
                            {
                                string CheckID = GetLine.Substring(GetLine.IndexOf("@line") + "@line".Length).Trim();

                                if (CheckID.Equals(LinkTexts[i].DefLineID.ToString()))
                                {
                                    if (Lines[ir].Contains(GetOriginal))
                                    {
                                        Lines[ir] = Lines[ir].Replace("\"" + GetOriginal + "\"", "\"" + GetTranslated + "\"");
                                    }
                                }
                                else
                                {
                                    //Some methods are missing the "@line" field. The specific reason is unclear. Here, you need to check whether the method name is consistent.

                                    if (Lines[ir].Contains(GetOriginal))
                                    {
                                        //Get the original key
                                        string QueryKey = LinkTexts[i].DefKey;

                                        QueryKey = QueryKey.Substring(0,QueryKey.LastIndexOf(","));

                                        foreach (var GetItem in HeuristicEngine.DStringItems)
                                        {
                                            if (GetItem.Key.Equals(QueryKey))
                                            {
                                                //Here the details should be structured. This is some of the feature files analyzed. I use ">" to split them. In fact, it is mainly to help establish a unique key. But I was lazy and directly used it to get the method name.
                                                int CheckFunc = 0;
                                                foreach (var GetParam in GetItem.Feature.Split('>'))
                                                {
                                                    if (GetParam.Trim().Length > 0)
                                                    {
                                                        //GetLine =	CallMethod AddToggleOption self ::temp8 "Message" True 0  ;@line 62
                                                        //GetParam = AddToggleOption
                                                        if (GetLine.Contains(GetParam.Trim()))
                                                        {
                                                            CheckFunc++;
                                                        }
                                                    }
                                                }

                                                if (CheckFunc > 0)
                                                {
                                                    int Count = Regex.Matches(Lines[ir], Regex.Escape("\"" + GetOriginal + "\"")).Count;

                                                    //To prevent the method from having two identical parameters, you can actually determine the position of the parameter. This is currently simplified. However, there are problems. I wrote it this way because I was lazy.
                                                    if (Count < 2)
                                                    {
                                                        Lines[ir] = Lines[ir].Replace("\"" + GetOriginal + "\"", "\"" + GetTranslated + "\"");
                                                    }
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
                ForceDelete(SetPath);
            }

            if (File.Exists(DeFine.GetFullPath(@"\") + CurrentFileName + ".pas"))
            {
                ForceDelete(DeFine.GetFullPath(@"\") + CurrentFileName + ".pas");
            }

            DataHelper.WriteFile(DeFine.GetFullPath(@"\") + CurrentFileName + ".pas", Encoding.GetBytes(GetFileContent));

            string CompilerPath = "";
            if (SkyrimHelper.FindPapyrusCompilerPath(ref CompilerPath))
            {
                if (!File.Exists(OutPutPath + ".backup"))
                {
                    File.Copy(OutPutPath, OutPutPath + ".backup");
                }
                
                ForceDelete(OutPutPath);

                string GetWorkPath = CompilerPath.Substring(0, CompilerPath.LastIndexOf(@"\") + @"\".Length);

                if (File.Exists(GetWorkPath + CurrentFileName + ".pas"))
                {
                    ForceDelete(GetWorkPath + CurrentFileName + ".pas");
                }

                File.Copy(DeFine.GetFullPath(@"\" + CurrentFileName + ".pas"), GetWorkPath + CurrentFileName + ".pas");

                if (File.Exists(DeFine.GetFullPath(@"\" + CurrentFileName + ".pas")))
                {
                    ForceDelete(DeFine.GetFullPath(@"\" + CurrentFileName + ".pas"));
                }

                var Result = CallPapyrusAssembler(CompilerPath, GetWorkPath, "\"" + CurrentFileName + "\"");

                if (File.Exists(GetWorkPath + CurrentFileName + ".pas"))
                {
                    ForceDelete(GetWorkPath + CurrentFileName + ".pas");
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
                    ForceDelete(SetOutPutFile);
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