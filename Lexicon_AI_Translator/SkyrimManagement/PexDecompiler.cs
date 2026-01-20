using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using static LexTranslator.SkyrimManagement.PexReader;
using LexTranslator.ConvertManager;
using System.Windows.Shapes;
using System;
using static LexTranslator.SkyrimManagement.PexDecompiler;
using LexTranslator.SkyrimManagement;
using System.Reflection.Emit;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace LexTranslator.SkyrimManagement
{
    // This class implements a PEX/PSC Decompiler.
    // Your overall project is licensed under CC BY-NC-ND 4.0,
    // but this specific class/code can be used by SSE Auto Translator (https://github.com/Cutleast/SSE-Auto-Translator)
    // under the MIT License.

    public class PexDecompiler
    {
        #region Extend
        public enum CodeGenStyle
        {
            Null = 0, Papyrus = 1, CSharp = 2, Python = 3
        }

        #endregion

        public static string Version = "1.0.0 Alpha";
        public CodeGenStyle GenStyle = CodeGenStyle.Null;
        public PexReader Reader;
        string CodeSpace = "    ";

        public PexDecompiler(PexReader CurrentReader, CodeGenStyle GenStyle = CodeGenStyle.Papyrus)
        {
            this.Reader = CurrentReader;
            this.GenStyle = GenStyle;
        }

        public string GetJson()
        {
            string GetJson = JsonConvert.SerializeObject(Reader, Formatting.Indented);
            return GetJson;
        }


        public enum ObjType
        {
            Null = 0, Variables = 1, Properties = 2, Functions = 3, DebugInfo = 5
        }
        public object QueryAnyByID(int ID, ref ObjType Type)
        {
            foreach (var GetObj in Reader.Objects)
            {
                foreach (var GetItem in GetObj.Variables)
                {
                    if (GetItem.NameIndex.Equals((ushort)ID))
                    {
                        Type = ObjType.Variables;
                        return GetItem;
                    }
                }

                foreach (var GetItem in GetObj.Properties)
                {
                    if (GetItem.NameIndex.Equals((ushort)ID))
                    {
                        Type = ObjType.Properties;
                        return GetItem;
                    }
                }

                List<PexFunction> Functions = new List<PexFunction>();

                foreach (var GetItem in GetObj.States)
                {
                    foreach (var GetFunc in GetItem.Functions)
                    {
                        if (GetFunc.FunctionNameIndex.Equals((ushort)ID))
                        {
                            Type = ObjType.Functions;
                            Functions.Add(GetFunc);
                        }
                    }
                }

                if (Functions.Count > 0)
                {
                    return Functions;
                }

                foreach (var GetDebugFunc in Reader.DebugInfo.Functions)
                {
                    if (GetDebugFunc.FunctionNameIndex.Equals(ID))
                    {
                        Type = ObjType.DebugInfo;
                        return GetDebugFunc;
                    }
                }
            }

            return null;
        }

        public void AnalyzeClass(List<PexString> TempStrings, ref StringBuilder PscCode)
        {
            if (Reader.Objects.Count > 0)
            {
                string ScriptName = TempStrings[Reader.Objects[0].NameIndex].Value;
                string ParentClass = TempStrings[Reader.Objects[0].ParentClassNameIndex].Value;

                if (this.GenStyle == CodeGenStyle.Papyrus)
                {
                    PscCode.AppendLine(string.Format("ScriptName {0} Extends {1}", ScriptName, ParentClass));
                }
                else
                if (this.GenStyle == CodeGenStyle.CSharp)
                {
                    PscCode.AppendLine("public class  " + ScriptName + " : " + ParentClass + " \n{");
                }
            }
        }

        public void AnalyzeGlobalVariables(List<PexString> TempStrings, ref StringBuilder PscCode)
        {
            if (GenStyle == CodeGenStyle.Papyrus)
            {
                PscCode.AppendLine(";GlobalVariables");
            }
            else
            {
                PscCode.AppendLine(CodeSpace + "//GlobalVariables");
            }
            
            for (int i = 0; i < TempStrings.Count; i++)
            {
                var Item = TempStrings[i];
                ObjType CheckType = ObjType.Null;
                var TempValue = QueryAnyByID(Item.Index, ref CheckType);

                //if (Item.Value.Equals("KeysList"))
                //{ 

                //}

                if (CheckType == ObjType.Variables)
                {
                    PexVariable Variable = TempValue as PexVariable;
                    if (!Item.Value.StartsWith("::"))
                    {
                        string GetVariableType = TempStrings[Variable.TypeNameIndex].Value;
                        string TryGetValue = ConvertHelper.ObjToStr(Variable.DataValue);
                        if (TryGetValue.Length == 0)
                        {
                            if (this.GenStyle == CodeGenStyle.Papyrus)
                            {
                                PscCode.AppendLine(string.Format(GetVariableType + " " + Item.Value));
                            }
                            else
                            if (this.GenStyle == CodeGenStyle.CSharp)
                            {
                                PscCode.AppendLine(string.Format(CodeSpace + GetVariableType + " " + Item.Value + ";"));
                            }
                        }
                        else
                        {
                            if (GetVariableType.ToLower().Equals("string"))
                            {
                                if (this.GenStyle == CodeGenStyle.Papyrus)
                                {
                                    PscCode.AppendLine(string.Format(GetVariableType + " " + Item.Value
                                    + " = " + "\"" + TryGetValue + "\""));
                                }
                                else
                                if (this.GenStyle == CodeGenStyle.CSharp)
                                {
                                    PscCode.AppendLine(string.Format(CodeSpace + GetVariableType + " " + Item.Value
                                    + " = " + "\"" + TryGetValue + "\";"));
                                }
                            }
                            else
                            {
                                if (this.GenStyle == CodeGenStyle.Papyrus)
                                {
                                    PscCode.AppendLine(string.Format(GetVariableType + " " + Item.Value + " = " + TryGetValue));
                                }
                                else
                                if (this.GenStyle == CodeGenStyle.CSharp)
                                {
                                    PscCode.AppendLine(string.Format(CodeSpace + GetVariableType + " " + Item.Value + " = " + TryGetValue + ";"));
                                }
                            }
                        }
                    }
                }
            }
        }

        public void AnalyzeAutoGlobalVariables(List<PexString> TempStrings, ref StringBuilder PscCode)
        {
            if (GenStyle == CodeGenStyle.Papyrus)
            {
                PscCode.AppendLine(";Global Properties");
            }
            else
            {
                PscCode.AppendLine(CodeSpace + "//Global Properties");
            }

            for (int i = 0; i < TempStrings.Count; i++)
            {
                var Item = TempStrings[i];
                ObjType CheckType = ObjType.Null;

                var TempValue = QueryAnyByID(Item.Index, ref CheckType);

                if (CheckType == ObjType.Properties)
                {
                    PexProperty Property = TempValue as PexProperty;

                    string GetVariableType = TempStrings[Property.TypeIndex].Value;
                    string CheckVarName = TempStrings[Property.AutoVarNameIndex].Value;

                    if (CheckVarName.StartsWith("::"))
                    {
                        if (GetVariableType.Equals("globalvariable"))
                        {
                            GetVariableType = "GetVariableType";
                        }

                        string NodeStr = "";

                        if (this.GenStyle == CodeGenStyle.Papyrus)
                        {
                            NodeStr = " ;";
                        }
                        else
                        if (this.GenStyle == CodeGenStyle.CSharp)
                        {
                            NodeStr = " //";
                        }

                        var RealValue = QueryAnyByID(Property.AutoVarNameIndex, ref CheckType);
                        if (CheckType == ObjType.Variables)
                        {
                            string GetRealValue = ConvertHelper.ObjToStr((RealValue as PexVariable).DataValue);
                            if (GetRealValue.Length > 0)
                            {
                                NodeStr += "Value:" + GetRealValue;
                            }
                            else
                            {
                                NodeStr = string.Empty;
                            }
                        }
                        else
                        {
                            NodeStr = string.Empty;
                        }

                        if (this.GenStyle == CodeGenStyle.Papyrus)
                        {
                            PscCode.AppendLine(string.Format(GetVariableType + " Property " + Item.Value + " Auto" + NodeStr));
                        }
                        else
                        if (this.GenStyle == CodeGenStyle.CSharp)
                        {
                            PscCode.AppendLine(CodeSpace + "[Property(Auto = true)]");
                            PscCode.AppendLine(string.Format(CodeSpace + GetVariableType + " " + Item.Value + ";" + NodeStr));
                        }
                    }

                }
            }
        }

        public string GenSpace(int Count)
        {
            string Space = "";
            for (int i = 0; i < Count; i++)
            {
                Space += CodeSpace;
            }
            return Space;
        }

        public void AnalyzeFunction(List<PexString> TempStrings, ref StringBuilder PscCode)
        {
            for (int i = 0; i < TempStrings.Count; i++)
            {
                var Item = TempStrings[i];

                if (Item.Value == "RDO_SetActorAggression")
                { 
                
                }

                ObjType CheckType = ObjType.Null;

                var GetFunc = QueryAnyByID(Item.Index, ref CheckType);
                if (CheckType == ObjType.Functions)
                {
                    List<PexFunction> Function = GetFunc as List<PexFunction>;

                    if (Function.Count == 1)
                    {
                        var GetFunc1st = Function[0];

                        string GenParams = "";

                        string ReturnType = TempStrings[GetFunc1st.ReturnTypeIndex].Value;

                        if (ReturnType == "None")
                        {
                            ReturnType = string.Empty;
                        }
                        else
                        {
                            if (ReturnType.Length > 0)
                            {
                                ReturnType += " ";
                            }
                        }

                        for (int ir = 0; ir < GetFunc1st.NumParams; ir++)
                        {
                            if (GetFunc1st.Parameters.Count > ir)
                            {
                                var GetParam = GetFunc1st.Parameters[ir];
                                string GetType = TempStrings[GetFunc1st.Parameters[ir].TypeIndex].Value;
                                string GetParamName = TempStrings[GetParam.NameIndex].Value;

                                GenParams += string.Format("{0} {1},", GetType, GetParamName);
                            }
                        }

                        if (GenParams.EndsWith(","))
                        {
                            GenParams = GenParams.Substring(0, GenParams.Length - 1);
                        }

                        string GenLine = "";

                        if (GenStyle == CodeGenStyle.Papyrus)
                        {
                            GenLine = string.Format("{0}Function {1}({2})", ReturnType, Item.Value, GenParams);
                        }
                        else
                        if (GenStyle == CodeGenStyle.CSharp)
                        {
                            GenLine = string.Format(CodeSpace + "public {0}{1}({2})\n", ReturnType, Item.Value, GenParams) + CodeSpace + "{";
                        }

                        PscCode.AppendLine(GenLine);

                        int CodeLine = 0;
                        string TempBlock = "";
                        DecompileTracker Tracker = new DecompileTracker(Item.Value);

                        foreach (var GetInstruction in GetFunc1st.Instructions)
                        {
                            string GetOPName = GetInstruction.GetOpcodeName();

                            if (GetOPName == "return")
                            { 
                            
                            }

                            List<int> IntValues = new List<int>();

                            string CurrentLine = "";
                            PexFunction PexFunc = null;

                            foreach (var GetArg in GetInstruction.Arguments)
                            {
                                var GetIndex = ConvertHelper.ObjToInt(GetArg.Value);
                                if (GetIndex > 0)
                                {
                                    string GetValue = "";


                                    IntValues.Add(GetArg.Type);
                                    if (GetArg.Type == 3)
                                    {
                                        
                                    }
                                    else
                                    {
                                        var GetObj = TempStrings[GetIndex];

                                        var ChildType = ObjType.Null;
                                        var GetChildFunc = QueryAnyByID(GetObj.Index, ref ChildType);

                                        if (ChildType == ObjType.Functions)
                                        {
                                            var Functions = GetChildFunc as List<PexFunction>;
                                            if (Functions.Count > 0)
                                            {
                                                PexFunc = Functions[0];
                                            }
                                        }
                                        else
                                        { 
                                        
                                        }

                                        GetValue = GetObj.Value;

                                        ObjType VariableType = ObjType.Null;
                                        var GetVariableTypes = QueryAnyByID(GetObj.Index, ref VariableType);
                                        var NextGet = TempStrings[GetObj.Index];

                                        if (GetValue.Equals("NextIndex"))
                                        { 
                                        
                                        }
                                    }

                                    if (GetArg.Type == 2)
                                    {
                                        CurrentLine += "\"" + GetValue + "\"" + " ";
                                    }
                                    else
                                    {
                                        CurrentLine += GetValue + " ";
                                    }
                                }
                            }

                            CurrentLine = CurrentLine.Trim();
                            TempBlock += GenSpace(2) + Tracker.CheckCode(CodeLine, IntValues, GetOPName, CurrentLine, this.GenStyle) + "\n";
                            CodeLine++;
                        }

                        if (TempBlock.EndsWith("\n"))
                        {
                            TempBlock = TempBlock.Substring(0, TempBlock.Length - "\n".Length);
                        }

                        PscCode.AppendLine(TempBlock);

                        if (GenStyle == CodeGenStyle.Papyrus)
                        {
                            PscCode.AppendLine("EndFunction");
                        }
                        else
                        if (GenStyle == CodeGenStyle.CSharp)
                        {
                            PscCode.AppendLine(CodeSpace + "}\n");
                        }
                    }
                }
            }
        }

        public List<PexString> CurrentStrings = new List<PexString>();
        public string Decompile()
        {
            StringBuilder PscCode = new StringBuilder();
            List<PexString> TempStrings = new List<PexString>();

            TempStrings.AddRange(Reader.StringTable);

            CurrentStrings = TempStrings;

            AnalyzeClass(TempStrings, ref PscCode);

            AnalyzeGlobalVariables(TempStrings, ref PscCode);
            PscCode.Append("\n");
            AnalyzeAutoGlobalVariables(TempStrings, ref PscCode);
            PscCode.Append("\n");
            //Function XXX() EndFunction
            AnalyzeFunction(TempStrings, ref PscCode);


            if (this.GenStyle == CodeGenStyle.CSharp)
            {
                PscCode.AppendLine("}");
            }

            var GetCode = PscCode.ToString();

            return GetCode;
        }

    }
}

public class Function
{
    public string ParentFunction = "";
    public int CodeLine = 0;
    public string SelfVariable = "";
}

public class TVariable
{
    public string Tag = "";
    public string VariableName = "";
    public int CodeLine = 0;
}

public class CastLink
{
    public List<string> Links = new List<string>();
    public int CodeLine = 0;

    public void AddLinks(List<string> SetLinks)
    {
        foreach (var GetLink in SetLinks)
        {
            if (!this.Links.Contains(GetLink))
            {
                this.Links.Add(GetLink);
            }
        }
    }
    public bool Find(string Name)
    {
        foreach (var Get in Links)
        {
            if (Get.Equals(Name))
            {
                return true;
            }
        }
        return false;
    }
}

public class TProp
{
    public List<string> Fronts = new List<string>();
    public string PropName = "";
    public string LinkVariable = "";
    public int CodeLine = 0;
    public int IsGetOrSet = 0;
    public bool Self = false;
}

public class TFunction
{
    public List<string> Fronts = new List<string>();
    public string FunctionName = "";
    public List<string> Params = new List<string>();

    public int CodeLine = 0;

    public bool Self = false;

    public string LinkVariable = "";
}

public class TStrcat
{
    public string LinkVariable = "";

    public string Value = "";

    public string MergeStr = "";

    public bool IsLeft = false;

    public int CodeLine = 0;
}
public class DecompileTracker
{
    public string FuncName = "";
    public List<TVariable> PexVariables = new List<TVariable>();
    public List<TFunction> PexFunctions = new List<TFunction>();
    public List<CastLink> CastLinks = new List<CastLink>();
    public List<TProp> Props = new List<TProp>();
    public List<TStrcat> Strcats = new List<TStrcat>();

    public string QueryVariables(string TempName)
    {
        TempName = TempName.Trim();
        foreach (var Get in this.CastLinks)
        {
            if (Get.Find(TempName))
            {
                foreach (var GetLinkValue in Get.Links)
                {
                    foreach (var GetVariable in PexVariables)
                    {
                        if (GetVariable.Tag.Equals(GetLinkValue))
                        {
                            return GetVariable.VariableName.Trim();
                        }
                    }
                }
                break;
            }
        }

        foreach (var GetVariable in PexVariables)
        {
            if (GetVariable.Tag.Equals(TempName))
            {
                return GetVariable.VariableName.Trim();
            }
        }

        return string.Empty;
    }
    public DecompileTracker(string FuncName)
    {
        this.FuncName = FuncName;
    }
    public List<string> CreatParams(string Line)
    {
        List<string> Params = new List<string>();
        foreach (var Get in Line.Split(new[] { "::" }, StringSplitOptions.None))
        {
            if (Get.Trim().Length > 0)
            {
                Params.Add(Get);
            }
        }
        return Params;
    }
    public string CheckCode(int CodeLine, List<int> IntValues,string OPCode,string Line,CodeGenStyle GenStyle)
    {
        List<string> GetParams = CreatParams(Line);

        if (OPCode == "callmethod" || OPCode == "callparent" || OPCode == "callstatic")
        {
            TFunction NTFunction = new TFunction();
            NTFunction.CodeLine = CodeLine;

            bool NoParams = false;

            string FristValue = "";

            List<string> AParams = new List<string>();

            if (GetParams.Count >= 2)
            {
                GetParams[1] = GetParams[1].Trim();

                bool Nonevar = false;
                if (GetParams[1].Contains(" "))
                {
                    var GetStaticValue = GetParams[1].Split(' ');

                    for (int i = 0; i < GetStaticValue.Length; i++)
                    {
                        if (GetStaticValue[i] == "nonevar")
                        {
                            Nonevar = true;
                        }
                        else
                        if (GetStaticValue[i].Trim().Length > 0)
                        {
                            AParams.Add(GetStaticValue[i]);
                        }
                    }
                }
                if (Nonevar)
                {
                    GetParams[1] = "nonevar";
                }
                if (GetParams[1] == "nonevar")
                {
                    NoParams = true;
                }
            }

            if (GetParams.Count > 0)
            {
                FristValue = GetParams[0].Trim();
            }

            if (GetParams.Count > 2)
            {
                for (int i = 2; i < GetParams.Count; i++)
                {
                    GetParams[i] = GetParams[i].Trim();

                    if (GetParams[i].Contains(" "))
                    {
                        foreach (var Get in GetParams[i].Split(' '))
                        {
                            if (Get.Trim().Length > 0)
                            {
                                AParams.Add(Get);
                            }
                        }
                    }
                    else
                    {
                        AParams.Add(GetParams[i]);
                    }
                }
            }

            var NextParams = FristValue.Split(' ');
            List<string> Fronts = new List<string>();
            List<string> Params = new List<string>();

            if (AParams.Count > 0)
            {
                Params.AddRange(AParams);
            }

            if (FristValue.Contains(" "))
            {
                for (int i = 1; i < NextParams.Length; i++)
                {
                    if (NextParams[i] != "self")
                    {
                        Fronts.Add(NextParams[i]);
                    }
                }
            }

            if (NoParams)
            {
                if (NextParams.Length >= 2)
                {
                    if (NextParams[1].Equals("self"))
                    {
                        NTFunction.Self = true;
                        NTFunction.FunctionName = NextParams[0];

                        NTFunction.Fronts = Fronts;
                        NTFunction.Params = Params;

                        PexFunctions.Add(NTFunction);
                    }
                }
                else
                if (NextParams.Length > 0)
                {
                    NTFunction.FunctionName = NextParams[0];

                    NTFunction.Fronts = Fronts;
                    NTFunction.Params = Params;

                    PexFunctions.Add(NTFunction);
                }
            }
            else
            {
                if (NextParams.Length >= 2)
                {
                    if (NextParams[1].Equals("self"))
                    {
                        NTFunction.Self = true;
                        NTFunction.FunctionName = NextParams[0];

                        NTFunction.Fronts = Fronts;
                        NTFunction.Params = Params;

                        PexFunctions.Add(NTFunction);
                    }
                }
                else
                if (NextParams.Length > 0)
                {
                    NTFunction.FunctionName = NextParams[0];

                    NTFunction.Fronts = Fronts;
                    NTFunction.Params = Params;

                    PexFunctions.Add(NTFunction);
                }
            }

            return "//" + OPCode + " " + Line;
        }
        else
        if (OPCode == "assign")
        {
            if (GetParams.Count == 2)
            {
                TVariable NTVariable = new TVariable();
                NTVariable.Tag = GetParams[1];
                NTVariable.VariableName = GetParams[0];

                PexVariables.Add(NTVariable);
                return "//" + OPCode + " " + Line;
            }
            else
            {
                if (Line.Split(' ').Length == 2)
                {
                    string ReturnLine = Line.Split(' ')[0] + " = " + Line.Split(' ')[1];
                    if (GenStyle == CodeGenStyle.CSharp)
                    {
                        ReturnLine += ";";
                    }
                    return "//" + ReturnLine;
                }
                else
                {
                    TVariable NTVariable = new TVariable();
                    NTVariable.VariableName = Line;

                    PexVariables.Add(NTVariable);

                    return "//" + OPCode + " " + Line;
                }
            }
        }
        else
        if (OPCode == "cast")
        {
            bool FindLink = false;
            int SetOffset = -1;

            for (int i = 0; i < this.CastLinks.Count; i++)
            {
                foreach (var CheckLink in GetParams)
                {
                    if (this.CastLinks[i].Find(CheckLink))
                    {
                        SetOffset = i;
                        FindLink = true;
                        goto SetLink;
                    }
                }
            }

        SetLink:
            if (FindLink)
            {
                this.CastLinks[SetOffset].AddLinks(GetParams);
            }
            else
            {
                CastLink NCastLink = new CastLink();
                NCastLink.CodeLine = CodeLine;
                NCastLink.AddLinks(GetParams);

                this.CastLinks.Add(NCastLink);
            }

            return "//" + OPCode + " " + Line;
        }
        else
        if (OPCode == "propget" || OPCode == "propset")
        {
            TProp NTProp = new TProp();
            NTProp.CodeLine = CodeLine;

            if (OPCode == "propset")
            {
                NTProp.IsGetOrSet = 1;
            }

            if (GetParams.Count == 2)
            {
                string GetPropName = GetParams[0];
                GetPropName = GetPropName.Trim();
                if (GetPropName.Contains(" "))
                {
                    var GetPropNames = GetPropName.Split(' ');
                    if (GetPropNames.Length >= 2)
                    {
                        NTProp.PropName = GetPropNames[0];

                        if (GetPropNames[1] == "self")
                        {
                            NTProp.Self = true;
                        }
                        else
                        {

                        }
                    }
                    else
                    {

                    }

                    NTProp.LinkVariable = GetParams[1];
                }
                else
                {
                    NTProp.PropName = GetPropName;
                    NTProp.LinkVariable = GetParams[1];
                }
            }
            else
            {
                List<string> GetFronts = new List<string>();

                foreach (var Get in GetParams)
                {
                    if (Get.Trim() != GetParams[GetParams.Count - 1].Trim() && Get.Trim() != GetParams[0].Trim())
                    {
                        GetFronts.Add(Get.Trim());
                    }
                }

                NTProp.PropName = GetParams[0].Trim();
                NTProp.LinkVariable = GetParams[GetParams.Count - 1].Trim();
                NTProp.Fronts = GetFronts;
            }
            return "//" + OPCode + " " + Line;
        }
        else
        if (OPCode == "strcat")
        {
            if (GetParams.Count > 0)
            {
                string StrValue = "";
                List<string> StrcatParams = new List<string>();

                if (GetParams.Count > 0 && GetParams.Count < 2)
                {
                    foreach (var Get in GetParams[0].Split(' '))
                    {
                        if (Get.Trim().StartsWith("\""))
                        {
                            StrValue = Get.Trim();
                        }
                        else
                        if (Get.Trim().Length > 0)
                        {
                            StrcatParams.Add(Get.Trim());
                        }
                    }
                }
                else
                if (GetParams.Count >= 2)
                {
                    StrcatParams = GetParams;
                }

                TStrcat NTStrcat = new TStrcat();
                NTStrcat.CodeLine = CodeLine;
                NTStrcat.LinkVariable = StrcatParams[0].Trim();
                if (StrValue.Trim().Length > 0)
                {
                    NTStrcat.Value = StrcatParams[StrcatParams.Count - 1];
                    NTStrcat.MergeStr = StrValue;
                }
                else
                {
                    NTStrcat.MergeStr = StrcatParams[StrcatParams.Count - 1];
                }

                NTStrcat.MergeStr = NTStrcat.MergeStr.Trim();
                if (NTStrcat.MergeStr.StartsWith(NTStrcat.LinkVariable))
                {
                    NTStrcat.MergeStr = NTStrcat.MergeStr.Substring(NTStrcat.LinkVariable.Length).Trim();
                    NTStrcat.IsLeft = true;
                }
                if (NTStrcat.MergeStr.EndsWith(NTStrcat.LinkVariable))
                {
                    NTStrcat.MergeStr = NTStrcat.MergeStr.Substring(0, NTStrcat.MergeStr.Length - NTStrcat.LinkVariable.Length).Trim();
                    NTStrcat.IsLeft = false;
                }
                Strcats.Add(NTStrcat);
            }

            return "//" + OPCode + " " + Line;
        }
        else
        {
            return OPCode + " " + Line;
        }
    }
}
