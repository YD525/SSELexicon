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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using YDSkyrimToolR.ConvertManager;
using YDSkyrimToolR.SkyrimModManager;
using YDSkyrimToolR.TranslateManage;
using YDSkyrimToolR.UIManage;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;

namespace YDSkyrimToolR.SkyrimManage
{
    /*
    * @Author: 约定
    * @GitHub: https://github.com/tolove336/YDSkyrimToolR
    * @Date: 2025-02-06
    */

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

        public List<string> FunctionLinks = new List<string>();

        public StringParam(string DefLine, string EditorID, string SourceText)
        {
            this.Type = "Script";
            this.EditorID = EditorID;
            this.Key = SkyrimDataLoader.GenUniqueKey(this.EditorID, this.Type);

            this.SourceText = SourceText.Substring(1);
            this.SourceText = this.SourceText.Substring(0, this.SourceText.Length - 1);

            this.DefSourceText = SourceText;
            this.TransText = "";
            this.DefLine = DefLine;
        }

        public StringParam(string DefLine, string EditorID, string SourceText,string DefSourceText)
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
            int GetKey = SkyrimDataLoader.GenUniqueKey(this.EditorID, this.Type).GetHashCode();
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
            int GetKey = SkyrimDataLoader.GenUniqueKey(this.EditorID, this.Type).GetHashCode();
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
            CurrentFileName = string.Empty;
            Translator.ClearCache();
            DeFine.CurrentCodeView.Hide();
            StringParams.Clear();
            SafeStringParams.Clear();
            PSCContent = string.Empty;
            TempFilePath = string.Empty;
            CodeLines.Clear();
        }

        public string Execute(string ExePath, string Args)
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
                if (GenFileToPAS())
                {
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
            File.Copy(FilePath, DeFine.GetFullPath(@"Cache\") + GetFileName + "." + GetFileType);
            TempFilePath = DeFine.GetFullPath(@"Cache\") + GetFileName + "." + GetFileType;
            GetFilePath = DeFine.GetFullPath(@"Cache\");

            string TempPath = DeFine.GetFullPath(@"Cache\") + GetFileName + "." + GetFileType;
            Execute(DeFine.GetFullPath(@"Tool\Champollion.exe"), TempPath, DeFine.GetFullPath(@"Cache\"), DeFine.GetFullPath(@"Cache\"));
            //Thread.Sleep(100);
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

        public bool GenFileToPAS()
        {
            string GetFilePath = TempFilePath.Substring(0, TempFilePath.LastIndexOf(@"\"));
            string GetFileFullName = TempFilePath.Substring(TempFilePath.LastIndexOf(@"\") + @"\".Length);
            string GetFileType = GetFileFullName.Split('.')[1];
            string GetFileName = GetFileFullName.Split('.')[0];

            string PapyrusAssembler = DeFine.GetFullPath(@"Tool\") + @"Data\Processors\CreationKit\PapyrusAssembler.exe";
            string GenParam = string.Format("\"{0}\" -D\r\n{1}", GetFileName, DeFine.GetFullPath(@"Tool\Data\Processors\CreationKit\"));

            File.Copy(TempFilePath, DeFine.GetFullPath(@"\") + GetFileName + "." + GetFileType);

            Execute(PapyrusAssembler, GenParam);

            if (File.Exists(DeFine.GetFullPath(GetFileName + "." + "disassemble.pas")))
            {
                if (File.Exists(DeFine.GetFullPath(@"Cache\") + GetFileName + "." + "pas"))
                {
                    File.Delete(DeFine.GetFullPath(@"Cache\") + GetFileName + "." + "pas");
                }
                File.Copy(DeFine.GetFullPath(GetFileName + "." + "disassemble.pas"), DeFine.GetFullPath(@"Cache\") + GetFileName + "." + "pas");
                File.Delete(DeFine.GetFullPath(GetFileName + "." + "disassemble.pas"));
                File.Delete(DeFine.GetFullPath(@"\") + GetFileName + "." + GetFileType);
                if (File.Exists(TempFilePath))
                {
                    if (TempFilePath.Contains(DeFine.GetFullPath(@"Cache\")))
                    {
                        File.Delete(TempFilePath);
                        return true;
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
                    if (GetFunction.Contains(" ")&& GetFunction.Contains("("))
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
                        var SearchLink = TryGetLinks(i, GetOffset, GetString, ref TryGetVariableType, ref TryGetVariableName);
                        StringParam NStringParam = new StringParam(GetLineStr,string.Format("{0}-{1}-{2}",i,GetOffset, GetLength), GetString);
                        NStringParam.FunctionLinks = SearchLink;
                        NStringParam.VariableType = TryGetVariableType;
                        NStringParam.VariableName = TryGetVariableName;
                        StringParams.Add(NStringParam);
                    }
                }
            }

            StartFilter();
        }

        public void StartFilter()
        {
            foreach (var GetParam in this.StringParams)
            {
                if (GetParam.SourceText.Trim().Length == 0)
                {
                    goto QuickPass;
                }
                if (GetParam.FunctionLinks.Count > 0)
                {
                    string GetFunctionName = GetParam.FunctionLinks[GetParam.FunctionLinks.Count - 1].Split("(")[0];
                    string ToLowerFunctionName = GetFunctionName.ToLower();
                    if (ToLowerFunctionName.Contains(".")) ToLowerFunctionName = ToLowerFunctionName.Split('.')[1];
                    if (ToLowerFunctionName.Equals("advanceskill") ||
                      ToLowerFunctionName.Equals("centeroncell") ||
                      ToLowerFunctionName.Equals("centeroncellandwait") ||
                      ToLowerFunctionName.Equals("closeuserlog") ||
                      ToLowerFunctionName.Equals("damageactorvalue") ||
                      ToLowerFunctionName.Equals("damageav") ||
                      ToLowerFunctionName.Equals("forceactorvalue") ||
                      ToLowerFunctionName.Equals("forceav") ||
                      ToLowerFunctionName.Equals("getactorvalue") ||
                      ToLowerFunctionName.Equals("getactorvaluepercentage") ||
                      ToLowerFunctionName.Equals("getaliasbyname") ||
                      ToLowerFunctionName.Equals("getanimationvariablebool") ||
                      ToLowerFunctionName.Equals("getanimationvariablefloat") ||
                      ToLowerFunctionName.Equals("getanimationvariableint") ||
                      ToLowerFunctionName.Equals("getav") ||
                      ToLowerFunctionName.Equals("getavpercentage") ||
                      ToLowerFunctionName.Equals("getbaseactorvalue") ||
                      ToLowerFunctionName.Equals("getbaseav") ||
                      ToLowerFunctionName.Equals("getformfromfile") ||
                      ToLowerFunctionName.Equals("getgamesettingfloat") ||
                      ToLowerFunctionName.Equals("getgamesettingint") ||
                      ToLowerFunctionName.Equals("getgamesettingstring") ||
                      ToLowerFunctionName.Equals("getinibool") ||
                      ToLowerFunctionName.Equals("getinifloat") ||
                      ToLowerFunctionName.Equals("getiniint") ||
                      ToLowerFunctionName.Equals("getinistring") ||
                      ToLowerFunctionName.Equals("getkeyword") ||
                      ToLowerFunctionName.Equals("getmappedkey") ||
                      ToLowerFunctionName.Equals("getmodbyname") ||
                      ToLowerFunctionName.Equals("getnodepositionx") ||
                      ToLowerFunctionName.Equals("getnodepositiony") ||
                      ToLowerFunctionName.Equals("getnodepositionz") ||
                      ToLowerFunctionName.Equals("getnodescale") ||
                      ToLowerFunctionName.Equals("getrace") ||
                      ToLowerFunctionName.Equals("gotostate") ||
                      ToLowerFunctionName.Equals("haskeywordstring") ||
                      ToLowerFunctionName.Equals("hasnode") ||
                      ToLowerFunctionName.Equals("incrementskill") ||
                      ToLowerFunctionName.Equals("incrementskillby") ||
                      ToLowerFunctionName.Equals("incrementstat") ||
                      ToLowerFunctionName.Equals("ismenuopen") ||
                      ToLowerFunctionName.Equals("loadcustomcontent") ||
                      ToLowerFunctionName.Equals("modactorvalue") ||
                      ToLowerFunctionName.Equals("modav") ||
                      ToLowerFunctionName.Equals("movetonode") ||
                      ToLowerFunctionName.Equals("onanimationevent") ||
                      ToLowerFunctionName.Equals("onanimationeventunregistered") ||
                      ToLowerFunctionName.Equals("onmenuclose") ||
                      ToLowerFunctionName.Equals("onmenuopen") ||
                      ToLowerFunctionName.Equals("onstoryincreaseskill") ||
                      ToLowerFunctionName.Equals("ontrackedstatsevent") ||
                      ToLowerFunctionName.Equals("openuserlog") ||
                      ToLowerFunctionName.Equals("playanimation") ||
                      ToLowerFunctionName.Equals("playanimationandwait") ||
                      ToLowerFunctionName.Equals("playbink") ||
                      ToLowerFunctionName.Equals("playermovetoandwait") ||
                      ToLowerFunctionName.Equals("playgamebryoanimation") ||
                      ToLowerFunctionName.Equals("playimpacteffect") ||
                      ToLowerFunctionName.Equals("playsubgraphanimation") ||
                      ToLowerFunctionName.Equals("playsyncedanimationandwaitss") ||
                      ToLowerFunctionName.Equals("playsyncedanimationss") ||
                      ToLowerFunctionName.Equals("playterraineffect") ||
                      ToLowerFunctionName.Equals("querystat") ||
                      ToLowerFunctionName.Equals("registerforanimationevent") ||
                      ToLowerFunctionName.Equals("RegisterForCustomEvent") ||
                      ToLowerFunctionName.Equals("registerforcontrol") ||
                      ToLowerFunctionName.Equals("registerformenu") ||
                      ToLowerFunctionName.Equals("registerformodevent") ||
                      ToLowerFunctionName.Equals("removehavokconstraints") ||
                      ToLowerFunctionName.Equals("requestmodel") ||
                      ToLowerFunctionName.Equals("restoreactorvalue") ||
                      ToLowerFunctionName.Equals("restoreav") ||
                      ToLowerFunctionName.Equals("sendanimationevent") ||
                      ToLowerFunctionName.Equals("sendmodevent") ||
                      ToLowerFunctionName.Equals("setactorvalue") ||
                      ToLowerFunctionName.Equals("setanimationvariablebool") ||
                      ToLowerFunctionName.Equals("setanimationvariablefloat") ||
                      ToLowerFunctionName.Equals("setanimationvariableint") ||
                      ToLowerFunctionName.Equals("setav") ||
                      ToLowerFunctionName.Equals("setgamesettingbool") ||
                      ToLowerFunctionName.Equals("setgamesettingfloat") ||
                      ToLowerFunctionName.Equals("setgamesettingint") ||
                      ToLowerFunctionName.Equals("setgamesettingstring") ||
                      ToLowerFunctionName.Equals("seticonpath") ||
                      ToLowerFunctionName.Equals("setmessageiconpath") ||
                      ToLowerFunctionName.Equals("setmiscstat") ||
                      ToLowerFunctionName.Equals("setmodelpath") ||
                      ToLowerFunctionName.Equals("setnodescale") ||
                      ToLowerFunctionName.Equals("setnodetextureset") ||
                      ToLowerFunctionName.Equals("setnthtexturepath") ||
                      ToLowerFunctionName.Equals("setnthtintmasktexturepath") ||
                      ToLowerFunctionName.Equals("setsubgraphfloatvariable") ||
                      ToLowerFunctionName.Equals("settintmasktexturepath") ||
                      ToLowerFunctionName.Equals("splinetranslatetorefnode") ||
                      ToLowerFunctionName.Equals("startscriptprofiling") ||
                      ToLowerFunctionName.Equals("starttitlesequence") ||
                      ToLowerFunctionName.Equals("stopscriptprofiling") ||
                      ToLowerFunctionName.Equals("syncedanimationandwaitss") ||
                      ToLowerFunctionName.Equals("syncedanimationss") ||
                      ToLowerFunctionName.Equals("unregisterforanimationevent") ||
                      ToLowerFunctionName.Equals("unregisterforcontrol") ||
                      ToLowerFunctionName.Equals("unregisterformenu") ||
                      ToLowerFunctionName.Equals("unregisterformodevent") ||
                      ToLowerFunctionName.Equals("waitforanimationevent") ||
                      ToLowerFunctionName.Equals("setbool") ||
                      ToLowerFunctionName.Equals("setint") ||
                      ToLowerFunctionName.Equals("setfloat") ||
                      ToLowerFunctionName.Equals("setnumber") ||
                      ToLowerFunctionName.Equals("invoke") ||
                      ToLowerFunctionName.Equals("invokebool") ||
                      ToLowerFunctionName.Equals("RegisterForMenuOpenCloseEvent") ||
                      ToLowerFunctionName.Equals("UnRegisterForRemoteEvent") ||
                      ToLowerFunctionName.Equals("RegisterForRemoteEvent") ||
                      ToLowerFunctionName.Equals("UnregisterForTutorialEvent") ||
                      ToLowerFunctionName.Equals("RegisterForTutorialEvent") ||
                      ToLowerFunctionName.Equals("SendCustomEvent") ||
                      ToLowerFunctionName.Equals("ShowAsHelpMessage") ||
                      ToLowerFunctionName.Equals("CastAs") ||
                      ToLowerFunctionName.Equals("AddTextReplacementData") ||
                      ToLowerFunctionName.Equals("CallFunction") ||
                      ToLowerFunctionName.Equals("CallFunctionNoWait") ||
                      ToLowerFunctionName.Equals("CallGlobalFunction") ||
                      ToLowerFunctionName.Equals("CallGlobalFunctionNoWait") ||
                      ToLowerFunctionName.Equals("SetINIBool") ||
                      ToLowerFunctionName.Equals("PlaceAtNode") ||
                      ToLowerFunctionName.Equals("RegisterForTrackedStatsEvent") ||
                      ToLowerFunctionName.Equals("Dismember") ||
                      ToLowerFunctionName.Equals("HasDirectLOS") ||
                      ToLowerFunctionName.Equals("PlayAnimOrIdle")
                        )
                    {
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
                                        var Key = string.Format("{0}-{1}-{2}", GetParam.Key.Split('-')[0], (int)(ConvertHelper.ObjToInt(GetParam.Key.Split('-')[1]) + ((int)GetDefLine.IndexOf(GetLinkText) - GetParam.DefSourceText.Length)), GetLinkText.Length);
                                        this.SafeStringParams.Add(new StringParam(GetDefLine,Key,TempLink,GetLinkText.Trim()));
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
                                        var Key = string.Format("{0}-{1}-{2}", GetParam.Key.Split('-')[0], (int)(ConvertHelper.ObjToInt(GetParam.Key.Split('-')[1]) + ((int)GetDefLine.IndexOf(GetLinkText) - GetParam.DefSourceText.Length)), GetLinkText.Length);
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

                QuickPass:
                int Result = 0;
            }
        }

        public void ProcessCode()
        {
            DeFine.CurrentCodeView.Show();

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

            DeFine.ActiveIDE.Text = RichText;

            SearchAllStr();
        }

        public void SavePexFile(string OutPutPath)
        {
            if (File.Exists(OutPutPath))
            {
                return;
            }

            Dictionary<string, string> LinkTexts = new Dictionary<string, string>();
            foreach (var GetParam in this.StringParams)
            {
                if (GetParam.FunctionLinks.Count > 0)
                {
                    string GetFunctionName = GetParam.FunctionLinks[GetParam.FunctionLinks.Count - 1].Split("(")[0];
                    string ToLowerFunctionName = GetFunctionName.ToLower();

                    if (Translator.TransData.ContainsKey(GetParam.Key.GetHashCode()))
                    {
                        if (!LinkTexts.ContainsKey(GetParam.DefSourceText))
                        LinkTexts.Add(GetParam.DefSourceText, Translator.TransData[GetParam.Key.GetHashCode()]);
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
                                        var Key = string.Format("{0}-{1}-{2}", GetParam.Key.Split('-')[0], (int)(ConvertHelper.ObjToInt(GetParam.Key.Split('-')[1]) + ((int)GetDefLine.IndexOf(GetLinkText) - GetParam.DefSourceText.Length)), GetLinkText.Length);
                                        var SetTKey = SkyrimDataLoader.GenUniqueKey(Key, "Script").GetHashCode();
                                        if (Translator.TransData.ContainsKey(SetTKey))
                                        {
                                            if (!LinkTexts.ContainsKey(GetLinkText.Trim()))
                                            LinkTexts.Add(GetLinkText.Trim(), Translator.TransData[SetTKey]);
                                        }
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
                                        var Key = string.Format("{0}-{1}-{2}", GetParam.Key.Split('-')[0], (int)(ConvertHelper.ObjToInt(GetParam.Key.Split('-')[1]) + ((int)GetDefLine.IndexOf(GetLinkText) - GetParam.DefSourceText.Length)), GetLinkText.Length);
                                        var SetTKey = SkyrimDataLoader.GenUniqueKey(Key, "Script").GetHashCode();
                                        if (Translator.TransData.ContainsKey(SetTKey))
                                        {
                                            if(!LinkTexts.ContainsKey(GetLinkText.Trim()))
                                            LinkTexts.Add(GetLinkText.Trim(), Translator.TransData[SetTKey]);
                                        }
                                        this.SafeStringParams.Add(new StringParam(GetDefLine, Key, TempLink, GetLinkText.Trim()));
                                    }
                                }
                            }
                        }
                    }
                   
                }
            }

            var Encoding = DataHelper.GetFileEncodeType(DeFine.GetFullPath(@"\Cache\"+ CurrentFileName +".pas"));

            string GetFileContent = Encoding.GetString(DataHelper.GetBytesByFilePath(DeFine.GetFullPath(@"\Cache\" + CurrentFileName + ".pas")));
            for (int i = 0; i < LinkTexts.Count; i++)
            {
                string GetKey = LinkTexts.ElementAt(i).Key;
                if (GetFileContent.Contains(GetKey))
                {
                    LinkTexts[GetKey]= SystemDataWriter.PreFormatStr(LinkTexts[GetKey].Replace("\"","'"));
                    GetFileContent = GetFileContent.Replace(GetKey,"\"" +  LinkTexts[GetKey] + "\"");
                }
            }
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

            string FileContent = Encoding.UTF8.GetString(DataHelper.GetBytesByFilePath(DeFine.GetFullPath(@"\EN.bat")));
            string PapyrusAssembler = DeFine.GetFullPath(@"Tool\") + @"Data\Processors\CreationKit\PapyrusAssembler.exe";
            string Param = string.Format(FileContent, PapyrusAssembler, CurrentFileName, DeFine.GetFullPath(@"Tool\Data\Processors\CreationKit\"));
            
            if (File.Exists(DeFine.GetFullPath("Call.bat")))
            {
                File.Delete(DeFine.GetFullPath("Call.bat"));
            }

            FileHelper.WriteFile(DeFine.GetFullPath("Call.bat"), Param, Encoding.ASCII);

            var Result = ExecuteR(DeFine.GetFullPath("Call.bat"), "");

            Thread.Sleep(100);

            if (File.Exists(DeFine.GetFullPath(@"\" + CurrentFileName + ".pex")))
            {
                File.Copy(DeFine.GetFullPath(@"\" + CurrentFileName + ".pex"), OutPutPath);

                File.Delete(DeFine.GetFullPath(@"\" + CurrentFileName + ".pex"));
                File.Delete(DeFine.GetFullPath(@"\" + CurrentFileName + ".pas"));
                File.Delete(DeFine.GetFullPath(@"\Call.bat"));
            }

            Close();
        }
    }


    public class ScriptParser
    {
        private int functionDepth = 0;
        private List<string> externalVariables = new List<string>();
        private List<string> internalVariables = new List<string>();

        public void ProcessScript(string script)
        {
            string[] lines = script.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            foreach (string line in lines)
            {
                var templine = line.Trim();

                // 检查函数定义
                if (templine.StartsWith(" Function"))
                {
                    functionDepth++;
                    continue;
                }

                // 检查函数结束
                else if (templine.StartsWith("EndFunction"))
                {
                    functionDepth--;
                    continue;
                }

                // 处理变量定义
                if (functionDepth > 0)  // 在函数内部
                {
                    if (templine.Contains("="))  // 简单判断变量赋值
                    {
                        internalVariables.Add(templine);
                    }
                }
                else  // 在外部
                {
                    if (templine.Contains("="))  // 简单判断变量赋值
                    {
                        externalVariables.Add(templine);
                    }
                }
            }

            Console.WriteLine("External Variables:");
            Console.WriteLine(string.Join("\n", externalVariables));
            Console.WriteLine("\nInternal Variables:");
            Console.WriteLine(string.Join("\n", internalVariables));
        }
    }

    public class FunctionFinder
    {
        public static void FindLine()
        {
            // 假设你的代码字符串存储在 codeString 变量中
            string codeString = @"
Function Log(String msg, Int level, Bool notify)
Function ecConfigChanged()
Function ecPage(String page)
Function ecStartup()
Int Function ecFlags(Bool disabled)
Function ecCloseToGame()
Function ecFillMode(Bool topDown, Bool leftRight)
Function ecCursor(Int position)
Function ecEmpty(Int count)
Function ecHeader(String label, Bool disabled)
Function ecToggle(String storeKey, String label, Bool default, String desc, Bool disabled, Bool update, Bool saved)
Function ecSlider(String storeKey, String label, Float default, Float min, Float max, Float step, String format, Int prec, String desc, Bool disabled, Bool update, Bool saved)
Function ecText(String storeKey, String label, String default, String desc, Bool disabled, Bool update, Bool saved)
Bool Function ecGetBool(String storeKey)
Int Function ecGetInt(String storeKey)
Float Function ecGetFloat(String storeKey)
String Function ecKey(String S, Int extraOff)
Function ecExport(String fileName) 
";

            // 正则表达式提取方法名、返回值和参数
            string pattern = @"(?:Function\s+)?(\w+)\s+(\w+)\(([^)]*)\)";
            MatchCollection matches = Regex.Matches(codeString, pattern);

            // 转换的结果存储在一个列表中
            List<FunctionType> methodsInfo = new List<FunctionType>();

            foreach (Match match in matches)
            {
                if (match.Groups.Count == 4)
                {
                    string returnType = match.Groups[1].Value;
                    string methodName = match.Groups[2].Value;
                    string paramsString = match.Groups[3].Value;

                    string[] paramList = string.IsNullOrEmpty(paramsString)
                        ? new string[0]
                        : paramsString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    methodsInfo.Add(new FunctionType
                    {
                        MethodName = methodName,
                        ReturnType = returnType,
                        Parameters = Array.ConvertAll(paramList, p => p.Trim())
                    });
                }
            }

            // 打印结果
            foreach (var method in methodsInfo)
            {
                Console.WriteLine(method);
            }
        }
    }

    public class FunctionType
    {
        public string MethodName { get; set; }
        public string ReturnType { get; set; }
        public string[] Parameters { get; set; }

        public override string ToString()
        {
            return $"Method Name: {MethodName}, Return Type: {ReturnType}, Parameters: {string.Join(", ", Parameters)}";
        }
    }

}
