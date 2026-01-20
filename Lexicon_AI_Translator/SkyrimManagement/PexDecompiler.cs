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

                        foreach (var GetInstruction in GetFunc1st.Instructions)
                        {
                            string GetOPName = GetInstruction.GetOpcodeName();

                            if (GetOPName == "return")
                            { 
                            
                            }

                            List<int> IntValues = new List<int>();

                            string CurrentLine = "";
                            PexFunction PexFunc = null;
                            DecompileTracker Tracker = new DecompileTracker(Item.Value);

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

public class TFunction
{
    public List<string> Fronts = new List<string>();
    public string FunctionName = "";
    public List<string> Params = new List<string>();

    public string LinkVariable = "";
}
public class DecompileTracker
{
    public string FuncName = "";
    public List<TVariable> PexVariables = new List<TVariable>();
    public List<TFunction> PexFunctions = new List<TFunction>();

    public DecompileTracker(string FuncName)
    {
        this.FuncName = FuncName;
    }
    public string CheckCode(int CodeLine, List<int> IntValues,string OPCode,string Line,CodeGenStyle GenStyle)
    {
        if (OPCode == "callmethod" || OPCode == "callparent" || OPCode == "callstatic")
        {
            return OPCode + " " + Line;
        }
        else
        if (OPCode == "assign")
        {
            string[] GetParam = Line.Split(new[] { "::" }, StringSplitOptions.None);

            if (GetParam.Length == 2)
            {
                TVariable NTVariable = new TVariable();
                NTVariable.Tag = GetParam[1];
                NTVariable.VariableName = GetParam[0];

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
                    return ReturnLine;
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
        {
            return OPCode + " " + Line;
        }
    }
}
