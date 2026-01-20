using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using static LexTranslator.SkyrimManagement.PexReader;
using LexTranslator.ConvertManager;
using System.Windows.Shapes;
using System;

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

        public void AnalyzeFunction(List<PexString> TempStrings, ref StringBuilder PscCode)
        {
            for (int i = 0; i < TempStrings.Count; i++)
            {
                var Item = TempStrings[i];

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

                        string TempBlock = "";

                        foreach (var GetInstruction in GetFunc1st.Instructions)
                        {
                            string GetOPName = GetInstruction.GetOpcodeName();

                            TempBlock += GetOPName + " ";

                            string CurrentLine = GetOPName + " ";
                            PexFunction PexFunc = null;

                            foreach (var GetArg in GetInstruction.Arguments)
                            {
                                var GetIndex = ConvertHelper.ObjToInt(GetArg.Value);
                                if (GetIndex > 0)
                                {
                                    if (GetOPName == "callmethod" || GetOPName == "callparent" || GetOPName == "callstatic")
                                    { 
                                    
                                    }

                                    string GetValue = "";

                                    if (GetArg.Type == 3)
                                    {
                                        GetValue = GetIndex.ToString();
                                    }
                                    else
                                    {
                                        var GetObj = TempStrings[GetIndex];

                                        if (GetObj.Value == "Notification")
                                        { 
                                        
                                        }

                                        var ChildType = ObjType.Null;
                                        var GetChildFunc = QueryAnyByID(GetObj.Index, ref ChildType);

                                        if (ChildType == ObjType.Functions)
                                        {
                                            PexFunc = GetChildFunc as PexFunction;
                                        }
                                        else
                                        { 
                                        
                                        }

                                        GetValue = GetObj.Value;
                                    }

                                    if (GetArg.Type == 2)
                                    {
                                        TempBlock += "\"" + GetValue + "\"" + " ";
                                        CurrentLine += "\"" + GetValue + "\"" + " ";
                                    }
                                    else
                                    {
                                        TempBlock += GetValue + " ";
                                        CurrentLine += GetValue + " ";
                                    }
                                }
                            }

                            if (GetOPName == "callmethod" || GetOPName == "callparent" || GetOPName == "callstatic")
                            {
                                AssemblyHelper.ReconstructFunctionCall(PexFunc, GetOPName,CurrentLine);
                            }

                            TempBlock += "\n";
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

        public string Decompile()
        {
            StringBuilder PscCode = new StringBuilder();
            List<PexString> TempStrings = new List<PexString>();

            TempStrings.AddRange(Reader.StringTable);

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

public class AssemblyHelper
{
    public static string ReconstructFunctionCall(PexFunction Func, string Type,string Line)
    {
        Line = Line.Trim();
        string TempLine = Line;

        switch (Type)
        {
            case "callmethod":
                {
                    TempLine = TempLine.Substring(Type.Length).Trim();
                    string []GenParam = TempLine.Split(' ');
                   
                }
            break;
            case "callparent":
                { 
                }
            break;
            case "callstatic":
                { 
                }
            break;
        }
        return string.Empty;
    
    }

}