using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media.TextFormatting;
using SSELex.ConvertManager;

namespace SSELex.SkyrimManage
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    // Some logic in this file was developed with the assistance of ChatGPT, an AI model by OpenAI.
    // This code aims to parse and analyze conditional expressions in Papyrus scripts,
    // particularly to trace whether string values are passed through functions or are part of variable chains.

    public static class Crc32Helper
    {
        private static readonly uint[] Table;

        static Crc32Helper()
        {
            uint poly = 0xEDB88320;
            Table = new uint[256];
            for (uint i = 0; i < Table.Length; ++i)
            {
                uint temp = i;
                for (int j = 0; j < 8; ++j)
                {
                    if ((temp & 1) == 1)
                    {
                        temp = (temp >> 1) ^ poly;
                    }
                    else
                    {
                        temp >>= 1;
                    }
                }
                Table[i] = temp;
            }
        }

        public static string ComputeCrc32(string input)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input);
            uint crc = 0xFFFFFFFF;

            foreach (byte b in bytes)
            {
                byte index = (byte)((crc & 0xFF) ^ b);
                crc = (crc >> 8) ^ Table[index];
            }

            crc ^= 0xFFFFFFFF;
            return crc.ToString("X8"); 
        }
    }
    public static class ConditionHelper
    {
        // Given a condition line and a target string, find the variable or method related to that string.
        // Example:
        //   Line = If Anim != None && Anim.Name != "DDZapArmbDoggy01" ; #DEBUG_LINE_NO:86
        //   Target = DDZapArmbDoggy01
        // Returns: "Anim.Name" (IsMethod = false)
        public static (string Name, bool IsMethod) FindVariableOrMethodForString(string line, string targetString)
        {
            if (string.IsNullOrEmpty(line) || string.IsNullOrEmpty(targetString))
                return (string.Empty, false);

            string trimmedLine = line.Trim();
            if (trimmedLine.StartsWith("If ", StringComparison.OrdinalIgnoreCase))
                trimmedLine = trimmedLine.Substring(3).Trim();

            int commentIndex = trimmedLine.IndexOf(';');
            if (commentIndex >= 0)
                trimmedLine = trimmedLine.Substring(0, commentIndex).Trim();

            string quotedTarget = $"\"{targetString}\"";
            int foundIndex = trimmedLine.IndexOf(quotedTarget, StringComparison.Ordinal);
            if (foundIndex == -1)
                return (string.Empty, false);

            // Step backward to find the variable or method left of '!=' or '='
            int opIndex = trimmedLine.LastIndexOf("!=", foundIndex);
            if (opIndex == -1)
                opIndex = trimmedLine.LastIndexOf("=", foundIndex);
            if (opIndex == -1)
                return (string.Empty, false);

            // Move left from operator to find the full expression
            int end = opIndex - 1;
            while (end >= 0 && char.IsWhiteSpace(trimmedLine[end])) end--;
            if (end < 0) return (string.Empty, false);

            int start = end;
            while (start >= 0 && (char.IsLetterOrDigit(trimmedLine[start]) || trimmedLine[start] == '_' || trimmedLine[start] == '.'))
                start--;
            start++;

            if (start > end) return (string.Empty, false);

            string candidate = trimmedLine.Substring(start, end - start + 1);

            // Detect method
            bool isMethod = false;
            int lookahead = end + 1;
            while (lookahead < trimmedLine.Length && char.IsWhiteSpace(trimmedLine[lookahead]))
                lookahead++;
            if (lookahead < trimmedLine.Length && trimmedLine[lookahead] == '(')
                isMethod = true;

            return (candidate, isMethod);
        }

    }




    public class VariablesFinder
    {
        private int FunctionDepth = 0;

        public List<string> GlobalVariables = new List<string>();
        public List<string> ProcessScript(string Script)
        {
            GlobalVariables.Clear();

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
                    string ParamsString = Match.Groups[3 + 1].Value;

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
            List<string> Lines = Content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();
            for (int i = 0; i < Functions.Count; i++)
            {
                int LineID = 0;
                foreach (var GetLine in Lines)
                {
                    if (GetLine.Trim().Contains("Function") && GetLine.Contains(Functions[i].MethodName + "("))
                    {
                        string GetFunctionContent = ConvertHelper.StringDivision(Content, GetLine, "EndFunction");

                        List<string> TempLines = GetFunctionContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();
                        Functions[i].Codes.Clear();
                        Functions[i].Codes.AddRange(TempLines);
                        foreach (var GetTempLine in TempLines)
                        {
                            string TempLine = GetTempLine.Trim();
                            if (TempLine.Contains("=") && !TempLine.StartsWith("If") && !TempLine.StartsWith("if") && !TempLine.StartsWith("IF"))
                            {
                                TrackedVariable NVariable = new TrackedVariable();

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

                                if (!HeuristicCore.IsIf(TempLine))
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
                            if (TempLine.Contains("(") && TempLine.Replace(" ", "").Contains(");"))
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
        public List<TrackedVariable> Variables = new List<TrackedVariable>();
        public List<CallItem> Calls = new List<CallItem>();
        public List<string> Codes = new List<string>();
    }
    public class FunctionLink
    {
        public string FunctionName = string.Empty;
        public string VariableName = string.Empty;
        public FunctionLink Next;
    }
    public class TrackedVariable
    {
        public string Name;
        public string Type;
        public bool IsConst = false;
        public string Value;
        public string DEBUGLineID = "";
    }
    public class ParsingItem
    {
        public string MainFunctionName = string.Empty;
        public string FindLine = string.Empty;
        public bool IsFinalCall = false;
        public int DebugLineId = 0;
        public int Point = 0;
        public List<TrackedVariable> VariableLinks = new();
        public FunctionLink FuncLinks = new();
    }

    public class TranslationScoreDetail
    {
        public string DefLine = "";
        public string Reason = "";
        public double Value = 0;

        public TranslationScoreDetail(string DefLine, string Reason, double Value)
        {
            this.DefLine = DefLine;
            this.Reason = Reason;
            this.Value = Value;
        }
    }
    public enum DStringItemType
    {
        Unknown,
        IfCondition,
        FunctionCall,
        VariableAssignment
    }

    public class DStringItem
    {
        public string Key = "";
        public string Str = "";
        public int LineID = 0;
        public string SourceLine = "";
        public string ParentFunctionName = "";
        public double TranslationSafetyScore = 0;
        public string IdentifierName = "";
        public DStringItemType ItemType = DStringItemType.Unknown;
        public List<TranslationScoreDetail> TranslationScoreDetails = new List<TranslationScoreDetail>();
        public string Feature = "";
    }
    public class HeuristicCore
    {
        public static string Version = "1.0Alpha";
        public List<ParsingItem> ParsingItems = new();
        public FunctionFinder CurrentFunctionFinder = new FunctionFinder();
        public static List<string> SafePapyrusFuncs = new List<string>() { "SetInfoText", "NotifyActor", "Warn", "messageBox", "messagebox", "Messagebox","Log", "NotifyPlayer", "Message", "ecSlider", "ecToggle", "TextUpdate" };
        public static List<string> DangerPapyrusFuncs = new List<string>
{
    "AddTag",
    "AddPositionStage",
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
        public static List<string> UserDefinedSafeFuncs = new List<string>() {
    "Notify", "notify",
    "Log", "log",
    "Msg", "msg", "MSG",
    "Message", "message",
    "ShowMessage", "showMessage", "ShowMsg", "showMsg",
    "Display", "display",
    "Print", "print",
    "Alert", "alert",
    "Popup", "popup", "PopUp", "popUp",
    "Toast", "toast",
    "DebugLog", "debugLog", "Debug", "debug",
    "WriteLine", "writeline", "Write", "write",
    "ShowNotification", "showNotification", "Notification", "notification",
    "ShowInfo", "showInfo", "Info", "info",
    "ShowText", "showText",
    "ConsoleLog", "consoleLog",
    "Output", "output",
    "Error","error"
};


        public List<DStringItem> DStringItems = new List<DStringItem>();
        public static string FormatLine(string Line)
        {
            Line = Line.Trim();
            if (Line.StartsWith("Self.", StringComparison.OrdinalIgnoreCase))
            {
                return Line.Substring("Self.".Length);
            }
            if (Line.StartsWith("This.", StringComparison.OrdinalIgnoreCase))
            {
                return Line.Substring("This.".Length);
            }
            return Line;
        }

        public static bool IsFunction(string Line)
        {
            if (!IsIf(Line))
            {
                if (Regex.IsMatch(Line, @"^\s*(if|for|while|switch|return|catch|throw|try|do)\b"))
                    return false;

                return Regex.IsMatch(Line, @"\b\w+\s*\(.*\)");
            }
            return false;
        }

        public static bool IsIf(string Line)
        {
            string TrimmedLine = Line.TrimStart();

            return TrimmedLine.StartsWith("If ", StringComparison.OrdinalIgnoreCase)
                || TrimmedLine.StartsWith("ElseIf ", StringComparison.OrdinalIgnoreCase)
                || TrimmedLine.StartsWith("While ", StringComparison.OrdinalIgnoreCase);
        }

        public static string MatchIfCondition(string Input)
        {
            string Pattern = @"^\s*If\s+(\w+)\s*(==|!=|>=|<=|>|<)?\s*";
            var Match = Regex.Match(Input, Pattern);
            return Match.Success ? Match.Groups[1].Value : string.Empty;
        }

        public static string GetParentVariable(string VariableName, List<TrackedVariable> Variables, ref List<TrackedVariable> VariableLinks)
        {
            foreach (var Variable in Variables)
            {
                if (Variable.Name.Equals(VariableName, StringComparison.OrdinalIgnoreCase))
                {
                    if (Variable.Type.Equals("string", StringComparison.OrdinalIgnoreCase))
                    {
                        VariableLinks.Add(Variable);
                        return Variable.Value.Trim().StartsWith("\"")
                            ? Variable.Name
                            : GetParentVariable(Variable.Value, Variables, ref VariableLinks);
                    }
                }
            }

            foreach (var Variable in Variables)
            {
                if (Variable.Name.Equals(VariableName, StringComparison.OrdinalIgnoreCase)
                    && Variable.Type.Equals("string", StringComparison.OrdinalIgnoreCase))
                {
                    VariableLinks.Add(Variable);
                    return Variable.Name;
                }
            }

            return string.Empty;
        }

        public void DeepSearchFunctions(
            PexReader Reader,
            string SourceText,
            string TargetFunctionName,
            FunctionType FromFunc,
            ref List<TrackedVariable> VariableLinks,
            ref List<FunctionLink> FuncLinks)
        {
            foreach (var Function in CurrentFunctionFinder.Functions)
            {
                if (FromFunc.MethodName == Function.MethodName)
                    continue;

                foreach (var Line in Function.Codes)
                {
                    if (Line.Contains(FromFunc.MethodName + "("))
                    {
                        bool NotSelfCall = !(Line.ToLower().Contains("." + FromFunc.MethodName.ToLower() + "(") &&
                                             !Line.ToLower().Contains("self." + FromFunc.MethodName.ToLower() + "("));

                        if (NotSelfCall)
                        {
                            FunctionLink NewLink = new()
                            {
                                FunctionName = Function.MethodName,
                                VariableName = TargetFunctionName
                            };
                            FuncLinks.Add(NewLink);

                            // 递归继续向上找调用者
                            DeepSearchFunctions(Reader, SourceText, Function.MethodName, Function, ref VariableLinks, ref FuncLinks);
                        }
                    }
                }
            }
        }

        public void DeepSearchVariable(
            PexReader Reader,
            string SourceText,
            string VariableName,
            FunctionType FromFunc,
            ref List<TrackedVariable> VariableLinks,
            ref List<FunctionLink> FuncLinks)
        {
            string Resolved = GetParentVariable(VariableName, FromFunc.Variables, ref VariableLinks);
            if (!string.IsNullOrEmpty(Resolved))
            {
                foreach (var Link in VariableLinks)
                {
                    if (Link.Name == Resolved && !Link.Value.Trim().StartsWith("\""))
                    {
                        VariableName = Link.Value;
                        return;
                    }
                }
            }

            foreach (var Param in FromFunc.Parameters)
            {
                if (Param.Contains(" " + VariableName))
                {
                    FunctionLink NewLink = new()
                    {
                        FunctionName = FromFunc.MethodName,
                        VariableName = VariableName
                    };
                    FuncLinks.Add(NewLink);

                    DeepSearchFunctions(Reader, SourceText, FromFunc.MethodName, FromFunc, ref VariableLinks, ref FuncLinks);
                }
            }
        }

        public static List<string> ExtractValidStrings(string Line)
        {
            var Results = new List<string>();

            string TrimmedLine = Line.Trim();
            if (TrimmedLine.StartsWith(";") || TrimmedLine.StartsWith("//"))
                return Results;

            if (!(Line.Contains("(") || Line.Contains("=")))
                return Results;

            var Matches = Regex.Matches(Line, "\"([^\"]*)\"");
            foreach (Match Match in Matches)
            {
                if (Match.Success && Match.Groups.Count > 1)
                {
                    if (Match.Groups[1].Value.Trim().Length > 0)
                    {
                        Results.Add(Match.Groups[1].Value);
                    }
                }
            }

            return Results;
        }

        public static bool ContainsDebugLineNoAtEnd(string Line)
        {
            return Regex.IsMatch(Line, @"#DEBUG_LINE_NO:\d+\s*$");
        }

        public static int GetDebugLineNo(string Line)
        {
            string Trimmed = Line.TrimEnd();
            var Match = Regex.Match(Trimmed, @"#DEBUG_LINE_NO:(\d+)$");
            if (Match.Success && int.TryParse(Match.Groups[1].Value, out int LineNo))
            {
                return LineNo;
            }
            return -1;
        }

        public static string ExtractOuterMethodNameByValue(string Line, string TargetLiteral)
        {
            int Index = Line.IndexOf(TargetLiteral);
            if (Index == -1) return string.Empty;

            Stack<int> ParenStack = new Stack<int>();
            List<(int Open, int Close)> ParenPairs = new List<(int, int)>();

            for (int I = 0; I < Line.Length; I++)
            {
                char C = Line[I];
                if (C == '(') ParenStack.Push(I);
                else if (C == ')')
                {
                    if (ParenStack.Count > 0)
                    {
                        int Open = ParenStack.Pop();
                        int Close = I;
                        ParenPairs.Add((Open, Close));
                    }
                }
            }

            ParenPairs.Sort((a, b) => (a.Open == b.Open) ? b.Close.CompareTo(a.Close) : a.Open.CompareTo(b.Open));

            (int Open, int Close)? TargetParen = null;
            foreach (var Pair in ParenPairs)
            {
                if (Index > Pair.Open && Index < Pair.Close)
                {
                    TargetParen = Pair;
                    break;
                }
            }

            if (TargetParen.HasValue)
            {
                int NameEnd = TargetParen.Value.Open;
                int NameStart = NameEnd - 1;

                while (NameStart >= 0 && (char.IsLetterOrDigit(Line[NameStart]) || Line[NameStart] == '_' || Line[NameStart] == '.'))
                    NameStart--;

                NameStart++;

                string FullName = Line.Substring(NameStart, NameEnd - NameStart).Trim();

                int DotIndex = FullName.LastIndexOf('.');
                return DotIndex >= 0 ? FullName.Substring(DotIndex + 1) : FullName;
            }
            else
            {
                if (ParenPairs.Count > 0)
                {
                    var OuterParen = ParenPairs.OrderBy(p => p.Open).First();
                    int NameEnd = OuterParen.Open;
                    int NameStart = NameEnd - 1;

                    while (NameStart >= 0 && (char.IsLetterOrDigit(Line[NameStart]) || Line[NameStart] == '_' || Line[NameStart] == '.'))
                        NameStart--;

                    NameStart++;

                    string FullName = Line.Substring(NameStart, NameEnd - NameStart).Trim();

                    int DotIndex = FullName.LastIndexOf('.');
                    return DotIndex >= 0 ? FullName.Substring(DotIndex + 1) : FullName;
                }
            }

            return string.Empty;
        }
        public static string ExtractAssignedVariable(string Line)
        {
            var Match = Regex.Match(Line.Trim(), @"^(?:[A-Za-z_][A-Za-z0-9_]*\s+)?([A-Za-z_][A-Za-z0-9_]*)\s*(\+=|-=|=)");
            return Match.Success ? Match.Groups[1].Value : string.Empty;
        }

        public static string GetOuterMethodOrReturn(string Line, string VariableName)
        {
            if (Line.TrimStart().StartsWith("return", StringComparison.OrdinalIgnoreCase) &&
                Line.IndexOf(VariableName, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "Return";
            }

            int Index = Line.IndexOf(VariableName, StringComparison.Ordinal);
            if (Index == -1) return string.Empty;

            Stack<int> ParenStack = new Stack<int>();
            List<(int Open, int Close)> ParenPairs = new List<(int, int)>();

            for (int i = 0; i < Line.Length; i++)
            {
                char C = Line[i];
                if (C == '(') ParenStack.Push(i);
                else if (C == ')')
                {
                    if (ParenStack.Count > 0)
                    {
                        int Open = ParenStack.Pop();
                        int Close = i;
                        ParenPairs.Add((Open, Close));
                    }
                }
            }

            ParenPairs.Sort((a, b) => (a.Open == b.Open) ? b.Close.CompareTo(a.Close) : a.Open.CompareTo(b.Open));

            foreach (var Pair in ParenPairs)
            {
                if (Index > Pair.Open && Index < Pair.Close)
                {
                    int NameEnd = Pair.Open;
                    int NameStart = NameEnd - 1;

                    while (NameStart >= 0 && (char.IsLetterOrDigit(Line[NameStart]) || Line[NameStart] == '_' || Line[NameStart] == '.'))
                        NameStart--;

                    NameStart++;

                    string FullName = Line.Substring(NameStart, NameEnd - NameStart).Trim();

                    int DotIndex = FullName.LastIndexOf('.');
                    return DotIndex >= 0 ? FullName.Substring(DotIndex + 1) : FullName;
                }
            }

            return string.Empty;
        }

        public void DetectFunction(string SourceLine, string FunctionName, int Offset)
        {
            bool IsNativeFunction = false;

            var details = DStringItems[Offset].TranslationScoreDetails;

            void AddIfNotExists(string defLine, string reason, double value)
            {
                if (!details.Any(d => d.DefLine == defLine && d.Reason == reason && d.Value == value))
                {
                    details.Add(new TranslationScoreDetail(defLine, reason, value));
                }
            }

            if (SafePapyrusFuncs.Contains(FunctionName))
            {
                AddIfNotExists(SourceLine, $"Detected safe function '{FunctionName}'", 10);
                IsNativeFunction = true;
            }

            if (DangerPapyrusFuncs.Contains(FunctionName))
            {
                AddIfNotExists(SourceLine, $"Detected danger function '{FunctionName}'", -20);
                IsNativeFunction = true;
            }

            if (!IsNativeFunction)
            {
                if (UserDefinedSafeFuncs.Contains(FunctionName))
                {
                    AddIfNotExists(SourceLine, $"Determined safe function by name '{FunctionName}'", 5);
                }
                else
                {
                    string lower = FunctionName.ToLower();
                    if (lower.StartsWith("is") || lower.StartsWith("get") || lower.StartsWith("set"))
                    {
                        AddIfNotExists(SourceLine, $"Function '{FunctionName}' may be related to state check or key-value access (e.g., get/set/is). Translation may alter logic.", -20);
                    }
                    else
                    {
                        // TODO: Need DeepSearch
                        //[V2 Enhancement] Planned: Implement function chain resolution for deep variable/method tracing.
                    }
                }
            }
        }

       

        public void AnalyzeCodeLine(List<string> Lines)
        {
            DStringItems.Clear();

            string RichText = "";
            foreach (var GetLine in Lines)
            {
                RichText += GetLine + "\r\n";
            }
            CurrentFunctionFinder.FindContent(RichText);

            foreach (var GetFunc in CurrentFunctionFinder.Functions)
            {
                foreach (var CodeLine in GetFunc.Codes)
                {
                    List<string> FindStrs = ExtractValidStrings(CodeLine);
                    if (FindStrs.Count > 0 && ContainsDebugLineNoAtEnd(CodeLine))
                    {
                        foreach (var GetStr in FindStrs)
                        {
                            DStringItem NDStringItem = new DStringItem();
                            NDStringItem.Str = GetStr;
                            NDStringItem.SourceLine = CodeLine;
                            NDStringItem.ParentFunctionName = GetFunc.MethodName;
                            NDStringItem.TranslationSafetyScore = 0;
                            NDStringItem.LineID = GetDebugLineNo(CodeLine);
                            DStringItems.Add(NDStringItem);
                        }
                    }
                }
            }

            for (int i = 0; i < DStringItems.Count; i++)
            {
                string SourceLine = DStringItems[i].SourceLine;
                string FormattedLine = FormatLine(SourceLine);
                DStringItems[i].Feature += DStringItems[i].ParentFunctionName + ">";

                if (!IsFunction(FormattedLine))
                {
                    if (IsIf(FormattedLine))
                    {
                        //Is IF
                        DStringItems[i].Feature += "Is IF>";
                        DStringItems[i].ItemType = DStringItemType.IfCondition;
                        string IfVar = MatchIfCondition(FormattedLine);

                        var Result = ConditionHelper.FindVariableOrMethodForString(SourceLine, DStringItems[i].Str);
                        if (Result.IsMethod)
                        {
                            DStringItems[i].Feature += "IsMethod True>";
                            string GetFunctionName = Result.Name;
                            DStringItems[i].Feature += GetFunctionName + ">";
                            DetectFunction(SourceLine, GetFunctionName, i);
                        }
                        else
                        {
                            DStringItems[i].Feature += "IsMethod False>";
                            string GetVariableName = Result.Name;
                            DStringItems[i].Feature += GetVariableName + ">";
                            string GetFullCode = "";
                            foreach (var GetFunc in CurrentFunctionFinder.Functions)
                            {
                                if (GetFunc.MethodName.Equals(DStringItems[i].ParentFunctionName))
                                {
                                    bool FindVariableLocation = false;
                                    foreach (var GetLine in GetFunc.Codes)
                                    {
                                        if (GetLine.Trim().Length > 0)
                                        {
                                            GetFullCode += GetLine + "\r\n";
                                            var OuterMethodOrReturn = GetOuterMethodOrReturn(GetLine, GetVariableName);
                                            if (OuterMethodOrReturn.Trim().Length > 0)
                                            {
                                                if (OuterMethodOrReturn.ToLower().Equals("return"))
                                                {
                                                    DStringItems[i].Feature += "DeepSearch>";
                                                    //Need DeepSearch
                                                    //[V2 Enhancement] Planned: Implement function chain resolution for deep variable/method tracing.
                                                    FindVariableLocation = true;
                                                }
                                                else
                                                {
                                                    DStringItems[i].Feature += "Is Function>";
                                                    //Is Function
                                                    string TryGetFunctionName = OuterMethodOrReturn;
                                                    DStringItems[i].Feature += TryGetFunctionName + ">";
                                                    DetectFunction(GetLine, TryGetFunctionName, i);
                                                    FindVariableLocation = true;
                                                }
                                            }
                                        }

                                        if (!FindVariableLocation)
                                        {
                                            DStringItems[i].Feature += "NotFind>";

                                            var DetailMessage = "Unresolved variable reference — likely a class variable or out-of-scope. Conservative penalty applied.";
                                            bool AlreadyAdded = DStringItems[i].TranslationScoreDetails
                                                .Any(d => d.Reason == DetailMessage && d.DefLine == SourceLine);
                                            if (!AlreadyAdded)
                                            {
                                               DStringItems[i].TranslationScoreDetails.Add(
                                               new TranslationScoreDetail(
                                               SourceLine,
                                               "Unresolved variable reference — likely a class variable or out-of-scope. Conservative penalty applied.",
                                               -20
                                               ));
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //Is Variable set 
                        DStringItems[i].Feature += "Is Variable set>";
                        DStringItems[i].ItemType = DStringItemType.VariableAssignment;
                        var GetVariableName = ExtractAssignedVariable(SourceLine);

                        DStringItems[i].Feature += GetVariableName + ">";
                        string GetFullCode = "";
                        foreach (var GetFunc in CurrentFunctionFinder.Functions)
                        {
                            if (GetFunc.MethodName.Equals(DStringItems[i].ParentFunctionName))
                            {
                                foreach (var GetLine in GetFunc.Codes)
                                {
                                    if (GetLine.Trim().Length > 0)
                                    {
                                        GetFullCode += GetLine + "\r\n";
                                        var OuterMethodOrReturn = GetOuterMethodOrReturn(GetLine, GetVariableName);
                                        if (OuterMethodOrReturn.Trim().Length > 0)
                                        {
                                            if (OuterMethodOrReturn.ToLower().Equals("return"))
                                            {
                                                DStringItems[i].Feature += "DeepSearch>";
                                                //Need DeepSearch
                                                //[V2 Enhancement] Planned: Implement function chain resolution for deep variable/method tracing.
                                            }
                                            else
                                            {
                                                DStringItems[i].Feature += "Is Function>";
                                                //Is Function
                                                string TryGetFunctionName = OuterMethodOrReturn;
                                                DStringItems[i].Feature += TryGetFunctionName + ">";

                                                DetectFunction(GetLine, TryGetFunctionName, i);
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
                else
                {
                    //Is Function
                    DStringItems[i].Feature += "Is Function>";
                    DStringItems[i].ItemType = DStringItemType.FunctionCall;
                    string TryGetFunctionName = ExtractOuterMethodNameByValue(SourceLine, DStringItems[i].Str);
                    DStringItems[i].Feature += TryGetFunctionName + ">";
                    DStringItems[i].IdentifierName = TryGetFunctionName;

                    DetectFunction(SourceLine, TryGetFunctionName, i);
                }
            }

            for (int i = 0; i < DStringItems.Count; i++)
            {
                foreach (var GetValue in DStringItems[i].TranslationScoreDetails)
                {
                    DStringItems[i].TranslationSafetyScore += GetValue.Value;
                }

                DStringItems[i].Key = Crc32Helper.ComputeCrc32(DStringItems[i].Feature) + "," + i.ToString();
            }
        }
    }
}