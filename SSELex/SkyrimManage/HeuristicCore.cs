using System.Text.RegularExpressions;

namespace SSELex.SkyrimManage
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class FunctionLink
    {
        public string FunctionName = "";
        public FunctionLink Next;
    }
    public class ParsingItem
    {
        public string MainFunctionName = "";
        public string FindLine = "";
        public bool IsFinalCall = false;
        public int DebugLineID = 0;
        public int Point = 0;
        public List<Variable> VariableLinks = new List<Variable>();
        public FunctionLink FuncLinks = new FunctionLink();
    }
    public class HeuristicCore
    {
        public static List<ParsingItem> ParsingItems = new List<ParsingItem>();
        public static List<string> SafePapyrusFuncs = new List<string>() { "Log" , "NotifyPlayer", "Message", "ecSlider", "ecToggle", "TextUpdate" };
        public static List<string> DangerPapyrusFuncs = new List<string>
{
    "advanceskill",
    "centeroncell",
    "centeroncellandwait",
    "closeuserlog",
    "damageactorvalue",
    "damageav",
    "forceactorvalue",
    "forceav",
    "getactorvalue",
    "getactorvaluepercentage",
    "getaliasbyname",
    "getanimationvariablebool",
    "getanimationvariablefloat",
    "getanimationvariableint",
    "getav",
    "getavpercentage",
    "getbaseactorvalue",
    "getbaseav",
    "getformfromfile",
    "getgamesettingfloat",
    "getgamesettingint",
    "getgamesettingstring",
    "getinibool",
    "getinifloat",
    "getiniint",
    "getinistring",
    "getkeyword",
    "getmappedkey",
    "getmodbyname",
    "getnodepositionx",
    "getnodepositiony",
    "getnodepositionz",
    "getnodescale",
    "getrace",
    "gotostate",
    "haskeywordstring",
    "hasnode",
    "incrementskill",
    "incrementskillby",
    "incrementstat",
    "ismenuopen",
    "loadcustomcontent",
    "modactorvalue",
    "modav",
    "movetonode",
    "onanimationevent",
    "onanimationeventunregistered",
    "onmenuclose",
    "onmenuopen",
    "onstoryincreaseskill",
    "ontrackedstatsevent",
    "openuserlog",
    "playanimation",
    "playanimationandwait",
    "playbink",
    "playermovetoandwait",
    "playgamebryoanimation",
    "playimpacteffect",
    "playsubgraphanimation",
    "playsyncedanimationandwaitss",
    "playsyncedanimationss",
    "playterraineffect",
    "querystat",
    "registerforanimationevent",
    "RegisterForCustomEvent",
    "registerforcontrol",
    "registerformenu",
    "registerformodevent",
    "removehavokconstraints",
    "requestmodel",
    "restoreactorvalue",
    "restoreav",
    "sendanimationevent",
    "sendmodevent",
    "setactorvalue",
    "setanimationvariablebool",
    "setanimationvariablefloat",
    "setanimationvariableint",
    "setav",
    "setgamesettingbool",
    "setgamesettingfloat",
    "setgamesettingint",
    "setgamesettingstring",
    "seticonpath",
    "setmessageiconpath",
    "setmiscstat",
    "setmodelpath",
    "setnodescale",
    "setnodetextureset",
    "setnthtexturepath",
    "setnthtintmasktexturepath",
    "setsubgraphfloatvariable",
    "settintmasktexturepath",
    "splinetranslatetorefnode",
    "startscriptprofiling",
    "starttitlesequence",
    "stopscriptprofiling",
    "syncedanimationandwaitss",
    "syncedanimationss",
    "unregisterforanimationevent",
    "unregisterforcontrol",
    "unregisterformenu",
    "unregisterformodevent",
    "waitforanimationevent",
    "setbool",
    "setint",
    "setfloat",
    "setnumber",
    "invoke",
    "invokebool",
    "RegisterForMenuOpenCloseEvent",
    "UnRegisterForRemoteEvent",
    "RegisterForRemoteEvent",
    "UnregisterForTutorialEvent",
    "RegisterForTutorialEvent",
    "SendCustomEvent",
    "ShowAsHelpMessage",
    "CastAs",
    "AddTextReplacementData",
    "CallFunction",
    "CallFunctionNoWait",
    "CallGlobalFunction",
    "CallGlobalFunctionNoWait",
    "SetINIBool",
    "PlaceAtNode",
    "RegisterForTrackedStatsEvent",
    "Dismember",
    "HasDirectLOS",
    "PlayAnimOrIdle"
};

        public static string FormatLine(string Line)
        {
            Line = Line.Trim();
            if (Line.ToLower().StartsWith("Self."))
            {
                Line = Line.Substring("Self.".Length);
            }
            else
            if (Line.ToLower().StartsWith("this."))
            {
                Line = Line.Substring("this.".Length);
            }
            return Line;
        }

        public static bool IsFunction(string Line)
        {
            if (Regex.IsMatch(Line, @"^\s*(if|for|while|switch|return|catch|throw|try|do)\b"))
                return false;

            return Regex.IsMatch(Line, @"\b\w+\s*\(.*\)");
        }

        public static bool IsIF(string Line)
        {
            if (Line.Trim().ToLower().StartsWith("if ")|| Line.Trim().ToLower().StartsWith("elseif ") || Line.Trim().ToLower().StartsWith("while "))
            {
                return true;
            }

            return false;
        }

        public static string MatchIfCondition(string input)
        {
            string pattern = @"^\s*If\s+(\w+)\s*(==|!=|>=|<=|>|<)?\s*";
            var match = Regex.Match(input, pattern);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetParentVariable(string VariableName, List<Variable> Variables,ref List<Variable> VariableLinks)
        {
            foreach (var GetVariable in Variables)
            {
                if (GetVariable.Name.Equals(VariableName))
                {
                    if (GetVariable.Type.ToLower() == "string")
                    {
                        VariableLinks.Add(GetVariable);

                        if (GetVariable.Value.Trim().StartsWith("\""))
                        {
                            return GetVariable.Name;
                        }
                        else
                        {
                            return GetParentVariable(GetVariable.Value, Variables,ref VariableLinks);
                        }
                    }
                }
            }

            foreach (var GetVariable in Variables)
            {
                if (GetVariable.Name.Equals(VariableName))
                {
                    if (GetVariable.Type.ToLower() == "string")
                    {
                        VariableLinks.Add(GetVariable);

                        return GetVariable.Name;
                    }
                }
            }

            return string.Empty;
        }

        public static void DeepSearchFunctions(PexReader OneReader, string DefSourceText, string FuncName, FunctionType FromFunc, ref List<Variable> VariableLinks, ref List<FunctionLink> FuncLinks)
        {
            //NeedDeepSearch
            foreach (var FunctionItem in OneReader.ScriptParser.Functions)
            {
                if (FromFunc.MethodName != FunctionItem.MethodName)
                {
                    //inline Search
                    foreach (var GetLine in FunctionItem.Codes)
                    {
                        if (GetLine.Contains(FromFunc.MethodName + "("))
                        {
                            if ((GetLine.ToLower().Contains("." + FromFunc.MethodName + "(") && !GetLine.ToLower().Contains("self." + FromFunc.MethodName + "(")) == false)
                            {
                                FunctionLink NFunctionLink = new FunctionLink();
                                NFunctionLink.FunctionName = FunctionItem.MethodName;

                                //需要检测参数传值是不是最终层

                                //if (!GetLine.Contains(DefSourceText))
                                //{
                                //    FuncLinks.Add(NFunctionLink);
                                //}
                                //else
                                //{ 
                                //    //LinkSearchNeed

                                //}
                            }
                        }
                    }
                }
            }
        }

        public static void DeepSearchVariable(PexReader OneReader,string DefSourceText, string VariableName,FunctionType FromFunc,ref List<Variable> VariableLinks,ref List<FunctionLink> FuncLinks)
        {
            var TryGetParentVariable = GetParentVariable(VariableName, FromFunc.Variables, ref VariableLinks);
            if (TryGetParentVariable.Trim().Length > 0)
            {
                foreach (var GetLinkItem in VariableLinks)
                {
                    if (GetLinkItem.Name.Equals(TryGetParentVariable))
                    {
                        if (!GetLinkItem.Value.Trim().StartsWith("\""))
                        {
                            VariableName = GetLinkItem.Value;
                            return;
                        }
                    }
                }
            }

            foreach (var GetParam in FromFunc.Parameters)
            {
                if (GetParam.Contains(" " + VariableName))
                {
                    DeepSearchFunctions(OneReader, DefSourceText, FromFunc.MethodName, FromFunc, ref VariableLinks, ref FuncLinks);
                }
            }
        }

        public static void CheckPexParams(PexReader OneReader)
        {
            for(int i=0;i<OneReader.SafeStringParams.Count;i++)
            {
                var GetItem = OneReader.SafeStringParams[i];

                string GetKey = GetItem.Key;
                int GetLineID = GetItem.LineID;

                if (OneReader.ScriptParser != null)
                {
                    foreach (var GetGlobalVariable in OneReader.ScriptParser.GlobalVariables)
                    {
                        if (GetGlobalVariable.Contains(GetItem.DefSourceText))
                        {
                            OneReader.SafeStringParams[i].Danger = true;
                        }
                    }

                    foreach (var GetFunc in OneReader.ScriptParser.Functions)
                    {
                        foreach (var GetFLine in GetFunc.Codes)
                        {
                            if (GetFLine.Contains(GetItem.DefSourceText) && GetFLine.Contains($"#DEBUG_LINE_NO:{GetLineID}"))
                            {
                                string GetLine = FormatLine(GetFLine);
                                if (IsFunction(GetLine))
                                {

                                }
                                else
                                {
                                    List<Variable> VariableLinks = new List<Variable>();
                                    List<FunctionLink> FuncLinks = new List<FunctionLink>();
                                    if (IsIF(GetLine))
                                    {
                                        var GetIFVariableName = MatchIfCondition(GetLine);
                                        //全部一视同仁
                                        DeepSearchVariable(OneReader,GetItem.DefSourceText,GetIFVariableName,GetFunc,ref VariableLinks,ref FuncLinks);

                                        if (GetLine.Replace(" ", "").Contains("=" + GetItem.DefSourceText))
                                        {
                                            OneReader.SafeStringParams[i].Danger = true;
                                        }
                                        else
                                        { 
                                        
                                        }
                                    }
                                    else
                                    { 
                                    
                                    }
                                }
                            }
                        }
                    }
                }
               
            }
        }
    }
}
