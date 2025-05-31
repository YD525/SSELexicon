using System.IO;
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
    public class HeuristicCore
    {
        public static string Version = "1.3Alpha";
        public List<ParsingItem> ParsingItems = new();
        public FunctionFinder CurrentFunctionFinder = new FunctionFinder();
        public static List<string> SafePapyrusFuncs = new List<string>() { "SetInfoText", "NotifyActor", "Warn", "messageBox", "messagebox", "Messagebox", "Log", "NotifyPlayer", "NotifyNPC", "Message", "ecSlider", "ecToggle", "TextUpdate" };

        //https://ck.uesp.net/wiki/Category:Papyrus Game Api Doc

        public static List<string> DangerPapyrusFuncs = new List<string>
        {
            "DamageActorValue",//Game Api https://ck.uesp.net/wiki/DamageActorValue
            "AttrDrain",//Game Api ?? maybe SkSE
            "RandomExpressionByTag",//SexLab Api
            "SendDeviceEvent",//DD Api
            "SendModEvent",//Game Api https://ck.uesp.net/wiki/SendModEvent
            "UnSetFormValue",//storageutil Api
            "AnimSwitchKeyword",//Game Api ?? maybe SkSE 
            "StartThirdPersonAnimation",//Game Api ?? maybe SkSE 
            "SendAnimationEvent",//Game Api https://ck.uesp.net/wiki/SendAnimationEvent_-_Debug
            "SetInstanceVolume",//Game Sound Api https://ck.uesp.net/wiki/SetInstanceVolume_-_Sound
            "Create",//Game ModEvent Api https://ck.uesp.net/wiki/ModEvent_Script

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
        };
        public static List<MethodParam> MethodSafeParams = new List<MethodParam>()
        {
           new MethodParam("NotifyPlayer",0),//DD Api
           new MethodParam("NotifyNPC",0),//DD Api
           new MethodParam("AddSliderOption",0),//SkyUI Api
           new MethodParam("AddSliderOptionST",1),//SkyUI Api
           new MethodParam("AddTextOption",0),//SkyUI Api
           new MethodParam("AddTextOptionST",1),//SkyUI Api
           new MethodParam("AddToggleOption",0),//SkyUI Api
           new MethodParam("AddToggleOptionST",1),//SkyUI Api
           new MethodParam("SetGameSettingString",1)//?? Api
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

        public static int FindParameterPosition(string CodeLine, string ParamToFind)
        {
            int Start = CodeLine.IndexOf('(');
            int End = CodeLine.LastIndexOf(')');
            if (Start < 0 || End < 0 || End <= Start)
                return -1;

            string ParamsStr = CodeLine.Substring(Start + 1, End - Start - 1).Trim();

            var Parameters = SplitParameters(ParamsStr);

            for (int i = 0; i < Parameters.Count; i++)
            {
                if (Parameters[i].Trim() == ParamToFind)
                    return i;
            }
            return -1;
        }

        public static List<string> SplitParameters(string Input)
        {
            List<string> Result = new List<string>();
            bool InQuotes = false;
            char QuoteChar = '\0';
            int LastPos = 0;

            for (int i = 0; i < Input.Length; i++)
            {
                char C = Input[i];
                if (C == '"' || C == '\'')
                {
                    if (!InQuotes)
                    {
                        InQuotes = true;
                        QuoteChar = C;
                    }
                    else if (QuoteChar == C)
                    {
                        InQuotes = false;
                    }
                }
                else if (C == ',' && !InQuotes)
                {
                    Result.Add(Input.Substring(LastPos, i - LastPos).Trim());
                    LastPos = i + 1;
                }
            }

            if (LastPos < Input.Length)
            {
                Result.Add(Input.Substring(LastPos).Trim());
            }
            return Result;
        }

        public bool IsSafeParam(string FunctionName, int Index)
        {
            return MethodSafeParams.Any(P => P.FunctionName == FunctionName && P.Index == Index);
        }

        public void DetectFunction(string SourceLine, string FunctionName, int Offset, string SourceStr)
        {
            bool IsNativeFunction = false;

            var Details = DStringItems[Offset].TranslationScoreDetails;

            void AddIfNotExists(string defLine, string reason, double value)
            {
                if (!Details.Any(d => d.DefLine == defLine && d.Reason == reason && d.Value == value))
                {
                    Details.Add(new TranslationScoreDetail(defLine, reason, value));
                }
            }

            int GetIndex = FindParameterPosition(SourceLine, "\"" + SourceStr + "\"");
            DStringItems[Offset].Feature += "[" + GetIndex.ToString() + "]";

            if (IsSafeParam(FunctionName, GetIndex))
            {
                AddIfNotExists(SourceLine, $"Detected safe function '{FunctionName}'", 20);
                IsNativeFunction = true;
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

        public bool CheckExists(int Offset, string Reason, string SourceLine)
        {
            bool AlreadyAdded = DStringItems[Offset].TranslationScoreDetails
            .Any(D => D.Reason == Reason && D.DefLine == SourceLine);
            return AlreadyAdded;
        }

        private static readonly HashSet<string> ValidExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
        ".dsd", ".txt", ".json", ".esl", ".esm", ".esp", ".pex",".dds"
        };

        public static bool LooksLikePath(string Input)
        {
            if (string.IsNullOrWhiteSpace(Input))
                return false;

            string Ext = Path.GetExtension(Input);
            return !string.IsNullOrEmpty(Ext) && ValidExtensions.Contains(Ext);
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
                foreach (var CodeLine in GetFunc.Body.Split(new char[2] { '\r', '\n' }))
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
                    else
                    {
                        foreach (var GetStr in FindStrs)
                        {
                            DStringItem NDStringItem = new DStringItem();
                            NDStringItem.Str = GetStr;
                            NDStringItem.SourceLine = CodeLine;
                            NDStringItem.ParentFunctionName = GetFunc.MethodName;
                            NDStringItem.TranslationSafetyScore = 0;
                            NDStringItem.LineID = -1;
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

                if (DStringItems[i].Str.Trim().StartsWith("$"))
                {
                    DStringItems[i].Feature += "Is MCM Link";

                    var DetailMessage = "These are the entries/terminologies used in MCM.";
                    if (!CheckExists(i, DetailMessage, SourceLine))
                    {
                        DStringItems[i].TranslationScoreDetails.Add(
                        new TranslationScoreDetail(
                        SourceLine,
                        DetailMessage,
                        -20
                        ));
                    }
                }
                else
                if (LooksLikePath(DStringItems[i].Str.Trim()))
                {
                    DStringItems[i].Feature += "Is LooksLikePath";

                    var DetailMessage = "String looks like a file path (based on extension).";
                    if (!CheckExists(i, DetailMessage, SourceLine))
                    {
                        DStringItems[i].TranslationScoreDetails.Add(
                        new TranslationScoreDetail(
                        SourceLine,
                        DetailMessage,
                        -10
                        ));
                    }
                }

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
                            DetectFunction(SourceLine, GetFunctionName, i, DStringItems[i].Str);
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
                                    foreach (var GetLine in GetFunc.Body.Split(new char[2] { '\r', '\n' }))
                                    {
                                        if (GetLine.Trim().Length > 0)
                                        {
                                            GetFullCode += GetLine + "\r\n";
                                            var OuterMethodOrReturn = GetOuterMethodOrReturn(GetLine, GetVariableName);
                                            if (OuterMethodOrReturn.Trim().Length > 0)
                                            {
                                                if (OuterMethodOrReturn.ToLower().Equals("return"))
                                                {
                                                    DStringItems[i].Feature += "Return>";
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
                                                    DetectFunction(GetLine, TryGetFunctionName, i, DStringItems[i].Str);
                                                    FindVariableLocation = true;
                                                }
                                            }
                                        }

                                        if (!FindVariableLocation)
                                        {
                                            DStringItems[i].Feature += "NotFind>";

                                            var DetailMessage = "Unresolved variable reference — likely a class variable or out-of-scope. Conservative penalty applied.";
                                            if (!CheckExists(i, DetailMessage, SourceLine))
                                            {
                                                DStringItems[i].TranslationScoreDetails.Add(
                                                new TranslationScoreDetail(
                                                SourceLine,
                                                DetailMessage,
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
                                foreach (var GetLine in GetFunc.Body.Split(new char[2] { '\r', '\n' }))
                                {
                                    if (GetLine.Trim().Length > 0)
                                    {
                                        GetFullCode += GetLine + "\r\n";
                                        var OuterMethodOrReturn = GetOuterMethodOrReturn(GetLine, GetVariableName);
                                        if (OuterMethodOrReturn.Trim().Length > 0)
                                        {
                                            if (OuterMethodOrReturn.ToLower().Equals("return"))
                                            {
                                                DStringItems[i].Feature += "Return>";
                                                //Need DeepSearch
                                                //[V2 Enhancement] Planned: Implement function chain resolution for deep variable/method tracing.
                                            }
                                            else
                                            {
                                                DStringItems[i].Feature += "Is Function>";
                                                //Is Function
                                                string TryGetFunctionName = OuterMethodOrReturn;
                                                DStringItems[i].Feature += TryGetFunctionName + ">";

                                                DetectFunction(GetLine, TryGetFunctionName, i, DStringItems[i].Str);
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
                    if (SourceLine.Contains("UnSetFormValue"))
                    {

                    }
                    //Is Function
                    DStringItems[i].Feature += "Is Function>";
                    DStringItems[i].ItemType = DStringItemType.FunctionCall;
                    string TryGetFunctionName = ExtractOuterMethodNameByValue(SourceLine, DStringItems[i].Str);
                    DStringItems[i].Feature += TryGetFunctionName + ">";
                    DStringItems[i].IdentifierName = TryGetFunctionName;

                    DetectFunction(SourceLine, TryGetFunctionName, i, DStringItems[i].Str);
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

            this.DStringItems = DStringItems.OrderByDescending(Item => Item.TranslationSafetyScore).ToList();
        }
    }

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

        public void FindContent(string content)
        {
            GlobalVariables = new VariablesFinder().ProcessScript(content);

            string pattern = @"(?i)(?:(\w+)\s+)?(Function|Event)\s+(\w+)\s*\(([^)]*)\)[\r\n]+(.*?)^\s*End(Function|Event)";
            var matches = Regex.Matches(content, pattern, RegexOptions.Singleline | RegexOptions.Multiline);

            List<FunctionType> methodsInfo = new List<FunctionType>();

            foreach (Match match in matches)
            {
                if (match.Groups.Count >= 7)
                {
                    string returnType = match.Groups[1].Success ? match.Groups[1].Value.Trim() : "";
                    string funcType = match.Groups[2].Value.Trim();
                    string methodName = match.Groups[3].Value.Trim();
                    string parameters = match.Groups[4].Value.Trim();
                    string body = match.Groups[5].Value.Trim();

                    string[] paramList = string.IsNullOrEmpty(parameters)
                        ? Array.Empty<string>()
                        : parameters.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    methodsInfo.Add(new FunctionType
                    {
                        MethodName = methodName,
                        ReturnType = returnType,
                        Parameters = paramList.Select(p => p.Trim()).ToList(),
                        Body = body,
                        IsEvent = funcType.Equals("Event", StringComparison.OrdinalIgnoreCase)
                    });
                }
            }

            Functions = methodsInfo;
        }


    }
    public class FunctionType
    {
        public string MethodName = "";
        public string ReturnType = "";
        public List<string> Parameters = new List<string>();
        public string Body = "";
        public bool IsEvent = true;
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
    public class MethodParam
    {
        public string FunctionName = "";
        public int Index = 0;

        public MethodParam(string FunctionName, int Index)
        {
            this.FunctionName = FunctionName;
            this.Index = Index;
        }
    }
}