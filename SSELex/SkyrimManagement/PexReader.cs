using DynamicData;
using Mutagen.Bethesda.Skyrim;
using Noggog;
using Reloaded.Memory.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using SSELex.ConvertManager;
using SSELex.SkyrimModManager;
using SSELex.TranslateManage;
using SSELex.UIManage;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;

namespace SSELex.SkyrimManage
{
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
        public string DefSourceText = "";
        public string TransText = "";
        public string VariableType = "";
        public string VariableName = "";
        public bool Danger = false;
        public int LineID = 0;

        public List<string> FunctionLinks = new List<string>();

        public StringParam(string DefLine, int LineID, int Offset, int Length, int SourceLineID, string SourceText)
        {
            this.Type = "Script";

            this.EditorID = string.Format("{0}-{1}-{2}-{3}", LineID, Offset, Length, SourceLineID);
            this.Key = SkyrimDataLoader.GenUniqueKey(this.EditorID, this.Type);

            this.SourceText = SourceText.Substring(1);
            this.SourceText = this.SourceText.Substring(0, this.SourceText.Length - 1);

            this.DefSourceText = SourceText;
            this.TransText = "";
            this.DefLine = DefLine;
        }

        public StringParam(string DefLine, string EditorID, string SourceText, string DefSourceText)
        {
            this.Type = "Script";
            this.EditorID = EditorID;
            this.Key = SkyrimDataLoader.GenUniqueKey(this.EditorID, this.Type);

            if (this.SourceText.StartsWith("\"") && this.SourceText.EndsWith("\""))
            {
                this.SourceText = SourceText.Substring(1);
                this.SourceText = this.SourceText.Substring(0, this.SourceText.Length - 1);
            }
            else
            {
                this.SourceText = SourceText;
            }

            this.DefSourceText = DefSourceText;
            this.TransText = "";
            this.DefLine = DefLine;
        }

        public string GetTextIfTrans()
        {
            if (this.TransText.Trim().Length > 0)
            {
                return this.TransText;
            }
            string GetKey = SkyrimDataLoader.GenUniqueKey(this.EditorID, this.Type);
            if (Translator.TransData.ContainsKey(GetKey))
            {
                this.TransText = Translator.TransData[GetKey];
                if (this.TransText.Length > 0)
                {
                    return this.TransText;
                }
                else
                {
                    return this.SourceText;
                }
            }

            return this.SourceText;
        }

        public string GetTextIfTransR()
        {
            string GetKey = SkyrimDataLoader.GenUniqueKey(this.EditorID, this.Type);
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
        public FunctionFinder ?ScriptParser = new FunctionFinder();
        public List<StringParam> StringParams = new List<StringParam>();
        public List<StringParam> SafeStringParams = new List<StringParam>();
        //E:\ModOrganizer\MO2\Mods\mods
        public string PSCContent = "";
        public string TempFilePath = "";
        public List<string> CodeLines = new List<string>();

        public PexReader()
        {

        }
        public void Close()
        {
            ScriptParser = null;
            CurrentFileName = string.Empty;
            Translator.ClearCache();
            DeFine.HideCodeView();
            StringParams.Clear();
            SafeStringParams.Clear();
            PSCContent = string.Empty;
            TempFilePath = string.Empty;
            CodeLines.Clear();
        }

        public string Execute(string ExePath, string Args,ref string OutPutMsg)
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

        public string ExecuteR(string ExePath, string Args)
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

        public string ExecuteRR(string ExePath,string StartPath, string Args)
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
            string GenParam = string.Format(" {0} -p {1} -a {2} -c -t", SourceFilePath, OutPutDirectory, AssemblyDirectory);
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
        }

        public void LoadPexFile(string FilePath)
        {
            Close();
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
                string OutPutMsg = string.Empty;
                if (GenFileToPAS(FilePath,ref OutPutMsg))
                {
                    if (OutPutMsg.Trim().Length == 0)
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
            string GetPscFile = GetFilePath + @"\" + GetFileName + ".psc";
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

        public bool GenFileToPAS(string FilePath,ref string OutPutMsg)
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
                string GenParam = string.Format("\"{0}\" -D\n{1}", GetFileName, GetWorkPath + @"\");

                if (File.Exists(DeFine.GetFullPath(@"\") + GetFileName + "." + GetFileType))
                {
                    File.Delete(DeFine.GetFullPath(@"\") + GetFileName + "." + GetFileType);
                }

                File.Copy(TempFilePath, DeFine.GetFullPath(@"\") + GetFileName + "." + GetFileType);

                Execute(PapyrusAssembler, GenParam,ref OutPutMsg);

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
                        if (TempFilePath.Contains(DeFine.GetFullPath(@"Cache\")))
                        {
                            File.Delete(TempFilePath);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public List<string> TryGetLinks(int StartLineID, int Offset, string Str, ref string TryGetVariableType, ref string TryGetVariableName)
        {
            List<string> Links = new List<string>();

            string GetLine = DeFine.ActiveIDE.Text.Substring(DeFine.ActiveIDE.Document.Lines[StartLineID].Offset, DeFine.ActiveIDE.Document.Lines[StartLineID].Length);
            if (GetLine.Contains("="))
            {
                string GetLeftStr = GetLine.Split('=')[0].Trim();
                if (GetLeftStr.Contains(" "))
                {
                    TryGetVariableType = GetLeftStr.Split(' ')[0];
                    TryGetVariableName = GetLeftStr.Split(' ')[1];
                }
                else
                if (!GetLeftStr.Contains(".")&&!GetLeftStr.Contains("(") && !GetLeftStr.Contains("'") && !GetLeftStr.Contains("\""))
                {
                    TryGetVariableName = GetLeftStr.Trim();
                }
            }

            if ((Offset - 1) >= 0)
            {
                if (GetLine.Contains("(") && GetLine.Contains(")"))
                {
                    string GetFunction = "";
                    if (GetLine.Contains("="))
                    {
                        TryGetVariableName = GetLine.Split(' ')[0];
                        GetFunction = GetLine.Substring(GetLine.IndexOf("=") + "=".Length);
                    }
                    else
                    {
                        GetFunction = GetLine;
                    }
                    GetFunction = GetFunction.Trim();
                    if (GetFunction.Contains(" ") && GetFunction.Contains("("))
                    {
                        string GetLeft = "";
                        string GetStartStr = GetFunction.Substring(0, GetFunction.IndexOf("("));
                        int LeftCutCount = 0;

                        for (int i = GetStartStr.Length; i > 0; i--)
                        {
                            string GetChar = GetFunction.Substring(i, 1);
                            if (GetChar == " ")
                            {
                                break;
                            }
                            else
                            {
                                LeftCutCount++;
                            }
                        }
                        if (LeftCutCount >= 0)
                        {
                            GetLeft = GetStartStr.Substring(GetStartStr.Length - LeftCutCount) + "(";
                        }

                        string GetLeftString = GetFunction.Substring(GetFunction.IndexOf("(") + "(".Length);
                        string GetRightString = GetLeftString.Substring(0, GetLeftString.LastIndexOf(")"));


                        string FullFunction = GetLeft + "(" + GetRightString + ")";
                        FullFunction = FullFunction.Replace("((", "(").Replace("))", ")");

                        string GetValue = ConvertHelper.StringDivision(FullFunction, "(", ")");
                        foreach (var GetParam in GetValue.Split(','))
                        {
                            if (GetParam.Trim().Length > 0)
                            {
                                Links.Add(GetParam);
                            }
                        }
                        Links.Add(FullFunction);
                    }
                }
            }

            return Links;
        }

        public void SearchAllStr()
        {
            DeFine.CurrentCodeView.Dispatcher.Invoke(new Action(() => {
                for (int i = 0; i < DeFine.ActiveIDE.LineCount; i++)
                {
                    var GetLineStr = DeFine.ActiveIDE.Text.Substring(DeFine.ActiveIDE.Document.Lines[i].Offset, DeFine.ActiveIDE.Document.Lines[i].Length);
                    if (GetLineStr.Contains("\""))
                    {
                        if (GetLineStr.Split('\"').Length > 2)
                        {
                            string GetString = "\"" + ConvertHelper.StringDivision(GetLineStr, "\"", "\"") + "\"";
                            int GetOffset = DeFine.ActiveIDE.Document.Lines[i].Offset + GetLineStr.IndexOf(GetString);
                            int GetLength = GetString.Length;
                            string TryGetVariableType = "";
                            string TryGetVariableName = "";

                            int GetDefLineID = 0;
                            if (GetLineStr.Contains("#DEBUG_LINE_NO:"))
                            {
                                var GetCutIDStr = GetLineStr.Substring(GetLineStr.IndexOf("#DEBUG_LINE_NO:") + "#DEBUG_LINE_NO:".Length).Trim();
                                GetDefLineID = ConvertHelper.ObjToInt(GetCutIDStr);
                            }

                            var SearchLink = TryGetLinks(i, GetOffset, GetLineStr, ref TryGetVariableType, ref TryGetVariableName);

                            StringParam NStringParam = new StringParam(GetLineStr, GetDefLineID, GetOffset, GetLength, i, GetString);
                            NStringParam.FunctionLinks = SearchLink;
                            NStringParam.VariableType = TryGetVariableType;
                            NStringParam.VariableName = TryGetVariableName;
                            NStringParam.LineID = GetDefLineID;
                            StringParams.Add(NStringParam);
                        }
                    }
                }

                StartFilter();
            }));
        }

        public bool CheckCanTransVariableName(string VariableName)
        {
            List<string> VariableNames = new List<string>() { "MessageStr", "Message", "msg", "Msg", "Str", "str" };
            foreach (var Get in VariableNames)
            {
                if (Get.Equals(VariableName))
                {
                    return true;
                }
            }
            return false;
        }

        public void StartFilter()
        {
            for (int i = 0; i < this.StringParams.Count; i++)
            {
                var GetParam = this.StringParams[i];
                int GetDefLineID = 0;

                if (GetParam.DefLine.Contains("#DEBUG_LINE_NO:"))
                {
                    var GetCutIDStr = GetParam.DefLine.Substring(GetParam.DefLine.IndexOf("#DEBUG_LINE_NO:") + "#DEBUG_LINE_NO:".Length).Trim();
                    GetDefLineID = ConvertHelper.ObjToInt(GetCutIDStr);
                    GetParam.LineID = GetDefLineID;
                }
                if (GetParam.SourceText.Trim().Length == 0)
                {
                    goto QuickPass;
                }

                if (CheckCanTransVariableName(GetParam.VariableName))
                { 
                    this.SafeStringParams.Add(GetParam);
                    goto QuickPass;
                }

                if (GetParam.FunctionLinks.Count > 0)
                {
                    string GetFunctionName = GetParam.FunctionLinks[GetParam.FunctionLinks.Count - 1].Split("(")[0];
                    string ToLowerFunctionName = GetFunctionName.ToLower();
                    if (ToLowerFunctionName.Contains(".")) ToLowerFunctionName = ToLowerFunctionName.Split('.')[1];
                    if (ToLowerFunctionName.Equals("advanceskill".ToLower()) ||
                      ToLowerFunctionName.Equals("centeroncell".ToLower()) ||
                      ToLowerFunctionName.Equals("centeroncellandwait".ToLower()) ||
                      ToLowerFunctionName.Equals("closeuserlog".ToLower()) ||
                      ToLowerFunctionName.Equals("damageactorvalue".ToLower()) ||
                      ToLowerFunctionName.Equals("damageav".ToLower()) ||
                      ToLowerFunctionName.Equals("forceactorvalue".ToLower()) ||
                      ToLowerFunctionName.Equals("forceav".ToLower()) ||
                      ToLowerFunctionName.Equals("getactorvalue".ToLower()) ||
                      ToLowerFunctionName.Equals("getactorvaluepercentage".ToLower()) ||
                      ToLowerFunctionName.Equals("getaliasbyname".ToLower()) ||
                      ToLowerFunctionName.Equals("getanimationvariablebool".ToLower()) ||
                      ToLowerFunctionName.Equals("getanimationvariablefloat".ToLower()) ||
                      ToLowerFunctionName.Equals("getanimationvariableint".ToLower()) ||
                      ToLowerFunctionName.Equals("getav".ToLower()) ||
                      ToLowerFunctionName.Equals("getavpercentage".ToLower()) ||
                      ToLowerFunctionName.Equals("getbaseactorvalue".ToLower()) ||
                      ToLowerFunctionName.Equals("getbaseav".ToLower()) ||
                      ToLowerFunctionName.Equals("getformfromfile".ToLower()) ||
                      ToLowerFunctionName.Equals("getgamesettingfloat".ToLower()) ||
                      ToLowerFunctionName.Equals("getgamesettingint".ToLower()) ||
                      ToLowerFunctionName.Equals("getgamesettingstring".ToLower()) ||
                      ToLowerFunctionName.Equals("getinibool".ToLower()) ||
                      ToLowerFunctionName.Equals("getinifloat".ToLower()) ||
                      ToLowerFunctionName.Equals("getiniint".ToLower()) ||
                      ToLowerFunctionName.Equals("getinistring".ToLower()) ||
                      ToLowerFunctionName.Equals("getkeyword".ToLower()) ||
                      ToLowerFunctionName.Equals("getmappedkey".ToLower()) ||
                      ToLowerFunctionName.Equals("getmodbyname".ToLower()) ||
                      ToLowerFunctionName.Equals("getnodepositionx".ToLower()) ||
                      ToLowerFunctionName.Equals("getnodepositiony".ToLower()) ||
                      ToLowerFunctionName.Equals("getnodepositionz".ToLower()) ||
                      ToLowerFunctionName.Equals("getnodescale".ToLower()) ||
                      ToLowerFunctionName.Equals("getrace".ToLower()) ||
                      ToLowerFunctionName.Equals("gotostate".ToLower()) ||
                      ToLowerFunctionName.Equals("haskeywordstring".ToLower()) ||
                      ToLowerFunctionName.Equals("hasnode".ToLower()) ||
                      ToLowerFunctionName.Equals("incrementskill".ToLower()) ||
                      ToLowerFunctionName.Equals("incrementskillby".ToLower()) ||
                      ToLowerFunctionName.Equals("incrementstat".ToLower()) ||
                      ToLowerFunctionName.Equals("ismenuopen".ToLower()) ||
                      ToLowerFunctionName.Equals("loadcustomcontent".ToLower()) ||
                      ToLowerFunctionName.Equals("modactorvalue".ToLower()) ||
                      ToLowerFunctionName.Equals("modav".ToLower()) ||
                      ToLowerFunctionName.Equals("movetonode".ToLower()) ||
                      ToLowerFunctionName.Equals("onanimationevent".ToLower()) ||
                      ToLowerFunctionName.Equals("onanimationeventunregistered".ToLower()) ||
                      ToLowerFunctionName.Equals("onmenuclose".ToLower()) ||
                      ToLowerFunctionName.Equals("onmenuopen".ToLower()) ||
                      ToLowerFunctionName.Equals("onstoryincreaseskill".ToLower()) ||
                      ToLowerFunctionName.Equals("ontrackedstatsevent".ToLower()) ||
                      ToLowerFunctionName.Equals("openuserlog".ToLower()) ||
                      ToLowerFunctionName.Equals("playanimation".ToLower()) ||
                      ToLowerFunctionName.Equals("playanimationandwait".ToLower()) ||
                      ToLowerFunctionName.Equals("playbink".ToLower()) ||
                      ToLowerFunctionName.Equals("playermovetoandwait".ToLower()) ||
                      ToLowerFunctionName.Equals("playgamebryoanimation".ToLower()) ||
                      ToLowerFunctionName.Equals("playimpacteffect".ToLower()) ||
                      ToLowerFunctionName.Equals("playsubgraphanimation".ToLower()) ||
                      ToLowerFunctionName.Equals("playsyncedanimationandwaitss".ToLower()) ||
                      ToLowerFunctionName.Equals("playsyncedanimationss".ToLower()) ||
                      ToLowerFunctionName.Equals("playterraineffect".ToLower()) ||
                      ToLowerFunctionName.Equals("querystat".ToLower()) ||
                      ToLowerFunctionName.Equals("registerforanimationevent".ToLower()) ||
                      ToLowerFunctionName.Equals("RegisterForCustomEvent".ToLower()) ||
                      ToLowerFunctionName.Equals("registerforcontrol".ToLower()) ||
                      ToLowerFunctionName.Equals("registerformenu".ToLower()) ||
                      ToLowerFunctionName.Equals("registerformodevent".ToLower()) ||
                      ToLowerFunctionName.Equals("removehavokconstraints".ToLower()) ||
                      ToLowerFunctionName.Equals("requestmodel".ToLower()) ||
                      ToLowerFunctionName.Equals("restoreactorvalue".ToLower()) ||
                      ToLowerFunctionName.Equals("restoreav".ToLower()) ||
                      ToLowerFunctionName.Equals("sendanimationevent".ToLower()) ||
                      ToLowerFunctionName.Equals("sendmodevent".ToLower()) ||
                      ToLowerFunctionName.Equals("setactorvalue".ToLower()) ||
                      ToLowerFunctionName.Equals("setanimationvariablebool".ToLower()) ||
                      ToLowerFunctionName.Equals("setanimationvariablefloat".ToLower()) ||
                      ToLowerFunctionName.Equals("setanimationvariableint".ToLower()) ||
                      ToLowerFunctionName.Equals("setav".ToLower()) ||
                      ToLowerFunctionName.Equals("setgamesettingbool".ToLower()) ||
                      ToLowerFunctionName.Equals("setgamesettingfloat".ToLower()) ||
                      ToLowerFunctionName.Equals("setgamesettingint".ToLower()) ||
                      ToLowerFunctionName.Equals("setgamesettingstring".ToLower()) ||
                      ToLowerFunctionName.Equals("seticonpath".ToLower()) ||
                      ToLowerFunctionName.Equals("setmessageiconpath".ToLower()) ||
                      ToLowerFunctionName.Equals("setmiscstat".ToLower()) ||
                      ToLowerFunctionName.Equals("setmodelpath".ToLower()) ||
                      ToLowerFunctionName.Equals("setnodescale".ToLower()) ||
                      ToLowerFunctionName.Equals("setnodetextureset".ToLower()) ||
                      ToLowerFunctionName.Equals("setnthtexturepath".ToLower()) ||
                      ToLowerFunctionName.Equals("setnthtintmasktexturepath".ToLower()) ||
                      ToLowerFunctionName.Equals("setsubgraphfloatvariable".ToLower()) ||
                      ToLowerFunctionName.Equals("settintmasktexturepath".ToLower()) ||
                      ToLowerFunctionName.Equals("splinetranslatetorefnode".ToLower()) ||
                      ToLowerFunctionName.Equals("startscriptprofiling".ToLower()) ||
                      ToLowerFunctionName.Equals("starttitlesequence".ToLower()) ||
                      ToLowerFunctionName.Equals("stopscriptprofiling".ToLower()) ||
                      ToLowerFunctionName.Equals("syncedanimationandwaitss".ToLower()) ||
                      ToLowerFunctionName.Equals("syncedanimationss".ToLower()) ||
                      ToLowerFunctionName.Equals("unregisterforanimationevent".ToLower()) ||
                      ToLowerFunctionName.Equals("unregisterforcontrol".ToLower()) ||
                      ToLowerFunctionName.Equals("unregisterformenu".ToLower()) ||
                      ToLowerFunctionName.Equals("unregisterformodevent".ToLower()) ||
                      ToLowerFunctionName.Equals("waitforanimationevent".ToLower()) ||
                      ToLowerFunctionName.Equals("setbool".ToLower()) ||
                      ToLowerFunctionName.Equals("setint".ToLower()) ||
                      ToLowerFunctionName.Equals("setfloat".ToLower()) ||
                      ToLowerFunctionName.Equals("setnumber".ToLower()) ||
                      ToLowerFunctionName.Equals("invoke".ToLower()) ||
                      ToLowerFunctionName.Equals("invokebool".ToLower()) ||
                      ToLowerFunctionName.Equals("RegisterForMenuOpenCloseEvent".ToLower()) ||
                      ToLowerFunctionName.Equals("UnRegisterForRemoteEvent".ToLower()) ||
                      ToLowerFunctionName.Equals("RegisterForRemoteEvent".ToLower()) ||
                      ToLowerFunctionName.Equals("UnregisterForTutorialEvent".ToLower()) ||
                      ToLowerFunctionName.Equals("RegisterForTutorialEvent".ToLower()) ||
                      ToLowerFunctionName.Equals("SendCustomEvent".ToLower()) ||
                      ToLowerFunctionName.Equals("ShowAsHelpMessage".ToLower()) ||
                      ToLowerFunctionName.Equals("CastAs".ToLower()) ||
                      ToLowerFunctionName.Equals("AddTextReplacementData".ToLower()) ||
                      ToLowerFunctionName.Equals("CallFunction".ToLower()) ||
                      ToLowerFunctionName.Equals("CallFunctionNoWait".ToLower()) ||
                      ToLowerFunctionName.Equals("CallGlobalFunction".ToLower()) ||
                      ToLowerFunctionName.Equals("CallGlobalFunctionNoWait".ToLower()) ||
                      ToLowerFunctionName.Equals("SetINIBool".ToLower()) ||
                      ToLowerFunctionName.Equals("PlaceAtNode".ToLower()) ||
                      ToLowerFunctionName.Equals("RegisterForTrackedStatsEvent".ToLower()) ||
                      ToLowerFunctionName.Equals("Dismember".ToLower()) ||
                      ToLowerFunctionName.Equals("HasDirectLOS".ToLower()) ||
                      ToLowerFunctionName.Equals("PlayAnimOrIdle".ToLower())
                        )
                    {
                        goto QuickPass;
                    }
                    if (GetFunctionName.Contains("NotifyPlayer"))
                    {
                        this.SafeStringParams.Add(GetParam);
                        goto QuickPass;
                    }
                    if (GetFunctionName.Contains("Message"))
                    {
                        this.SafeStringParams.Add(GetParam);
                        goto QuickPass;
                    }
                    if (GetFunctionName.Contains("Log"))
                    {
                        this.SafeStringParams.Add(GetParam);
                        goto QuickPass;
                    }
                    if (GetFunctionName.Contains("ecSlider"))
                    {
                        int Count = 0;
                        foreach (var GetLinkText in GetParam.FunctionLinks)
                        {
                            Count++;
                            string TempLink = GetLinkText.Trim();

                            if (TempLink.StartsWith("\"") && TempLink.EndsWith("\""))
                            {
                                TempLink = TempLink.Substring(1);
                                TempLink = TempLink.Substring(0, TempLink.Length - 1);
                                if (GetParam.DefSourceText != GetLinkText)
                                {
                                    if (GetLinkText.Trim().StartsWith("\"") && TempLink.Trim().Length > 0)
                                    {
                                        string GetDefLine = GetParam.DefLine;
                                        var Key = string.Format("{0}-{1}-{2}-{3}", GetParam.Key.Split('-')[0], (int)(ConvertHelper.ObjToInt(GetParam.Key.Split('-')[1]) + ((int)GetDefLine.IndexOf(GetLinkText) - GetParam.DefSourceText.Length)), GetLinkText.Length, Count);
                                        this.SafeStringParams.Add(new StringParam(GetDefLine, Key, TempLink, GetLinkText.Trim()));
                                    }
                                }
                            }
                        }
                    }
                    if (GetFunctionName.Contains("ecToggle"))
                    {
                        int Count = 0;
                        foreach (var GetLinkText in GetParam.FunctionLinks)
                        {
                            Count++;
                            string TempLink = GetLinkText.Trim();

                            if (TempLink.StartsWith("\"") && TempLink.EndsWith("\""))
                            {
                                TempLink = TempLink.Substring(1);
                                TempLink = TempLink.Substring(0, TempLink.Length - 1);
                                if (GetParam.DefSourceText != GetLinkText)
                                {
                                    if (GetLinkText.Trim().StartsWith("\"") && TempLink.Trim().Length > 0)
                                    {
                                        string GetDefLine = GetParam.DefLine;
                                        var Key = string.Format("{0}-{1}-{2}-{3}", GetParam.Key.Split('-')[0], (int)(ConvertHelper.ObjToInt(GetParam.Key.Split('-')[1]) + ((int)GetDefLine.IndexOf(GetLinkText) - GetParam.DefSourceText.Length)), GetLinkText.Length, Count);
                                        this.SafeStringParams.Add(new StringParam(GetDefLine, Key, TempLink, GetLinkText.Trim()));
                                    }
                                }
                            }
                        }
                    }
                    if (GetFunctionName.Contains("TextUpdate"))
                    {
                        this.SafeStringParams.Add(GetParam);
                        goto QuickPass;
                    }
                }

                if (GetParam.SourceText.Contains(".") && (GetParam.SourceText.Contains(" ") || GetParam.SourceText.Contains(",")))
                {
                    if (GetParam.SourceText.ToLower().Contains(".esm") || GetParam.SourceText.ToLower().Contains(".esp") || GetParam.SourceText.ToLower().Contains(".pex") || GetParam.SourceText.ToLower().Contains(".psc"))
                    {

                    }
                    else
                    {
                        if (GetParam.SourceText.Length > 6)
                        {
                            this.SafeStringParams.Add(GetParam);
                            goto QuickPass;
                        }
                    }
                }

                if (GetParam.SourceText.Contains("\n"))
                {
                    if (GetParam.SourceText.Length > 10)
                    {
                        this.SafeStringParams.Add(GetParam);
                        goto QuickPass;
                    }
                }

                GetParam.Danger = true;
                this.SafeStringParams.Add(GetParam);

                QuickPass:
                int Result = 0;
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

            //ADD TO Code View
            string RichText = "";
            foreach (var GetLine in this.CodeLines)
            {
                RichText += GetLine.Trim() + "\r\n";
            }

            ScriptParser = new FunctionFinder();
            ScriptParser.FindContent(RichText);

            if (DeFine.CurrentCodeView != null)
            {
                DeFine.CurrentCodeView.Dispatcher.Invoke(new Action(() => {
                    DeFine.ActiveIDE.Text = RichText;
                    DeFine.CurrentCodeView.ReSetFolding();
                }));
            }

            SearchAllStr();
        }

        public class LinkValue
        {
            public string Value = "";
            public int DefLineID = 0;
            public LinkValue(string Value, int DefLineID)
            {
                this.Value = Value;
                this.DefLineID = DefLineID;
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

        public void SavePexFile(string OutPutPath)
        {
            ScriptParser = null;
            Dictionary<string, LinkValue> LinkTexts = new Dictionary<string, LinkValue>();
            foreach (var GetParam in this.StringParams)
            {
                if (Translator.TransData.ContainsKey(GetParam.Key))
                {
                    if (!LinkTexts.ContainsKey(GetParam.DefSourceText))
                        LinkTexts.Add(GetParam.DefSourceText, new LinkValue(Translator.TransData[GetParam.Key], GetParam.LineID));
                }
            }

            foreach (var GetParam in this.SafeStringParams)
            {
                if (Translator.TransData.ContainsKey(GetParam.Key))
                {
                    if (!LinkTexts.ContainsKey(GetParam.DefSourceText))
                        LinkTexts.Add(GetParam.DefSourceText, new LinkValue(Translator.TransData[GetParam.Key], GetParam.LineID));
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
                    GetLinkValue.Value = SkyrimDataWriter.PreFormatStr(GetLinkValue.Value.Replace("\"", "'"));
                    LinkTexts[GetKey] = GetLinkValue;

                    for (int ir = 0; ir < Lines.Count; ir++)
                    {
                        string GetLine = Lines[ir];
                        if (GetLine.Contains("@line") && GetLinkValue.Value.Trim().Length > 0)
                        {
                            string CheckID = GetLine.Substring(GetLine.IndexOf("@line") + "@line".Length).Trim();
                            if (CheckID.Equals(GetLinkValue.DefLineID.ToString()))
                            {
                                if (Lines[ir].Contains(GetKey))
                                {
                                    Lines[ir] = Lines[ir].Replace(GetKey, "\"" + GetLinkValue.Value + "\"");
                                }
                            }
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

                File.Copy(DeFine.GetFullPath(@"\" + CurrentFileName + ".pas"), GetWorkPath + CurrentFileName + ".pas");

                if (File.Exists(DeFine.GetFullPath(@"\" + CurrentFileName + ".pas")))
                {
                    File.Delete(DeFine.GetFullPath(@"\" + CurrentFileName + ".pas"));
                }

                var Result = ExecuteRR(CompilerPath, GetWorkPath,"\"" + CurrentFileName + "\"");

                if (File.Exists(GetWorkPath + CurrentFileName + ".pas"))
                {
                    File.Delete(GetWorkPath + CurrentFileName + ".pas");
                }

                string SetOutPutFile = GetWorkPath + CurrentFileName + ".pex";

                if (File.Exists(SetOutPutFile))
                {
                    File.Copy(SetOutPutFile,OutPutPath);
                }

                if (File.Exists(SetOutPutFile))
                {
                    File.Delete(SetOutPutFile);
                }

                Close();
            }
        }
    }


    public class VariablesFinder
    {
        private int FunctionDepth = 0;

        public List<string> ProcessScript(string Script)
        {
            List<string> GlobalVariables = new List<string>();

            string[] Lines = Script.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            foreach (string Line in Lines)
            {
                var Templine = Line.Trim();

                if (Templine.Trim().ToLower().StartsWith("function"))
                {
                    FunctionDepth++;
                    continue;
                }

                else if (Templine.Trim().ToLower().StartsWith("endfunction"))
                {
                    FunctionDepth--;
                    continue;
                }

                if (FunctionDepth <= 0)  
                {
                    if (Templine.Contains("=") || Templine.Contains(" Auto"))
                    {
                        GlobalVariables.Add(Templine);
                    }
                }
            }
            return GlobalVariables;
        }
    }

    public class FunctionFinder
    {
        public List<string> GlobalVariables = new List<string>();
        public List<FunctionType> Functions = new List<FunctionType>();

        public void FindContent(string Content)
        {
            this.GlobalVariables = new VariablesFinder().ProcessScript(Content);
            string CodeString = Content;

            string Pattern = @"^(.*\b(?:Function\s+)?(\w+)\s+(\w+)\(([^)]*)\).*)$";
            MatchCollection matches = Regex.Matches(CodeString, Pattern, RegexOptions.Multiline);

            List<FunctionType> MethodsInfo = new List<FunctionType>();

            foreach (Match Match in matches)
            {
                if (Match.Groups.Count > 3)
                {
                    string GetType = Match.Groups[0].Value;
                    string ReturnType = "";

                    if (GetType.Contains(" "))
                    {
                        GetType = GetType.Split(' ')[0];
                        if (GetType.ToLower() != "function")
                        {
                            ReturnType = GetType;
                        }
                    }

                    string MethodName = Match.Groups[3].Value;
                    string ParamsString = Match.Groups[3+1].Value;

                    string[] ParamList = string.IsNullOrEmpty(ParamsString)
                        ? new string[0]
                        : ParamsString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    MethodsInfo.Add(new FunctionType
                    {
                        MethodName = MethodName,
                        ReturnType = ReturnType,
                        Parameters = (Array.ConvertAll(ParamList, P => P.Trim())).ToList()
                    });
                }
            }

            Functions = MethodsInfo;
            ReadFunctionContent(Content);
        }

        public void ReadFunctionContent(string Content)
        {
            List<string>Lines = Content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();
            for (int i = 0; i < Functions.Count; i++)
            {
                int LineID = 0;
                foreach (var GetLine in Lines)
                {
                    if (GetLine.Trim().Contains("Function") && GetLine.Contains(Functions[i].MethodName+"("))
                    {
                        string GetFunctionContent = ConvertHelper.StringDivision(Content,GetLine,"EndFunction");

                        List<string>TempLines = GetFunctionContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();
                        Functions[i].Codes.Clear();
                        Functions[i].Codes.AddRange(TempLines);
                        foreach (var GetTempLine in TempLines)
                        {
                            string TempLine = GetTempLine.Trim();
                            if (TempLine.Contains("=") && !TempLine.StartsWith("If") && !TempLine.StartsWith("if") && !TempLine.StartsWith("IF"))
                            {
                                Variable NVariable = new Variable();

                                var GetParams = TempLine.Split('=')[0].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                if (GetParams.Count > 1)
                                {
                                    NVariable.Type = GetParams[0];
                                    NVariable.Name = GetParams[1];
                                }
                                else
                                {
                                    NVariable.Name = GetParams[0];
                                }

                                if (!HeuristicCore.IsIF(TempLine))
                                {
                                    var GetValues = TempLine.Split('=')[1].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                    if (GetValues.Count > 0)
                                    {
                                        NVariable.Value = GetValues[0];
                                        if (NVariable.Value.Trim().StartsWith("\"") && NVariable.Value.Trim().EndsWith("\""))
                                        {
                                            NVariable.IsConst = true;
                                        }
                                        else
                                        {
                                            NVariable.IsConst = false;
                                        }

                                        foreach (var GetValue in GetValues)
                                        {
                                            if (GetValue.Trim().StartsWith("#DEBUG_LINE_NO:"))
                                            {
                                                NVariable.DEBUGLineID = GetValue;
                                            }
                                        }
                                    }

                                    Functions[i].Variables.Add(NVariable);
                                }
                            }
                            else
                            if (TempLine.Contains("(") && TempLine.Replace(" ","").Contains(");"))
                            {
                                CallItem NCallItem = new CallItem();
                                NCallItem.Call = TempLine;
                                if (NCallItem.Call.Contains("#DEBUG_LINE_NO:"))
                                {
                                    NCallItem.Call = NCallItem.Call.Substring(0, NCallItem.Call.IndexOf("#DEBUG_LINE_NO:"));
                                    NCallItem.DEBUGLineID = TempLine.Substring(TempLine.IndexOf("#DEBUG_LINE_NO:"));
                                }
                                Functions[i].Calls.Add(NCallItem);
                            }
                        }
                    }
                    LineID++;
                }
            }
        }
    }

    public class FunctionType
    {
        public string MethodName = "";
        public string ReturnType = "";
        public List<string> Parameters = new List<string>();
        public List<Variable> Variables = new List<Variable>();
        public List<CallItem> Calls = new List<CallItem>();
        public List<string> Codes = new List<string>();
    }
    
    public class Variable
    {
        public string Type = "";
        public string Name = "";
        public bool IsConst = false;
        public string Value = "";
        public string DEBUGLineID = "";
    }

    public class CallItem
    {
        public string Call = "";
        public string DEBUGLineID = "";
    }
}