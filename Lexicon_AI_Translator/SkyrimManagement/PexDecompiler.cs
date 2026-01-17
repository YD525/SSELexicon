using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using static LexTranslator.SkyrimManagement.PexReader;
using LexTranslator.ConvertManager;
using System.Windows.Shapes;
using System;
using System.Linq;

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
        public enum OpCode
        {
            NOP = 0,
            IADD = 1,
            FADD = 2,
            ISUB = 3,
            FSUB = 4,
            IMUL = 5,
            FMUL = 6,
            IDIV = 7,
            FDIV = 8,
            IMOD = 9,
            NOT = 10,
            INEG = 11,
            FNEG = 12,
            ASSIGN = 13,
            CAST = 14,
            CMP_EQ = 15,
            CMP_LT = 16,
            CMP_LTE = 17,
            CMP_GT = 18,
            CMP_GTE = 19,
            JMP = 20,      
            JMPT = 21,     
            JMPF = 22,     
            CALLMETHOD = 23,
            CALLPARENT = 24,
            CALLSTATIC = 25,
            RETURN = 26,
            STRCAT = 27,
            PROPGET = 28,
            PROPSET = 29,
            ARRAY_CREATE = 30,
            ARRAY_LENGTH = 31,
            ARRAY_GETELEMENT = 32,
            ARRAY_SETELEMENT = 33
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

 
        private enum BlockType
        {
            Sequential,
            IfThen,
            IfElse,
            While,
            Return
        }

        private string DecompileFunction(PexFunction Func, List<PexString> Strings, int Indent = 1)
        {
            StringBuilder Code = new StringBuilder();
            string IndentStr = new string(' ', Indent * 4);

            var Instructions = Func.Instructions.ToList();
            int i = 0;

            while (i < Instructions.Count)
            {
                var Instruction = Instructions[i];
                OpCode Opcode = (OpCode)Instruction.Opcode;

                switch (Opcode)
                {
                    case OpCode.JMPF: 
                        {
                            // JMPF condition offset
                            var Condition = GetArgumentValue(Instruction.Arguments[0], Strings);
                            int JumpOffset = ConvertHelper.ObjToInt(Instruction.Arguments[1].Value);
                            int JumpTarget = i + JumpOffset;

                            bool HasElse = false;
                            int ElseStart = -1;
                            int EndIfIndex = JumpTarget;

                            for (int j = i + 1; j < JumpTarget && j < Instructions.Count; j++)
                            {
                                if ((OpCode)Instructions[j].Opcode == OpCode.JMP)
                                {
                                    HasElse = true;
                                    ElseStart = JumpTarget;
                                    int elseJumpOffset = ConvertHelper.ObjToInt(Instructions[j].Arguments[0].Value);
                                    EndIfIndex = j + elseJumpOffset;
                                    break;
                                }
                            }

                            Code.AppendLine($"{IndentStr}If {Condition}");

                            i++;
                            while (i < (HasElse ? ElseStart - 1 : JumpTarget) && i < Instructions.Count)
                            {
                                Code.Append(DecompileInstruction(Instructions[i], Strings, Indent + 1));
                                i++;
                            }

                            if (HasElse)
                            {
                                Code.AppendLine($"{IndentStr}Else");
                                i++; 

                                while (i < EndIfIndex && i < Instructions.Count)
                                {
                                    Code.Append(DecompileInstruction(Instructions[i], Strings, Indent + 1));
                                    i++;
                                }
                            }

                            Code.AppendLine($"{IndentStr}EndIf");
                            continue;
                        }

                    case OpCode.JMPT: 
                        {
                            var Condition = GetArgumentValue(Instruction.Arguments[0], Strings);
                            int JumpOffset = ConvertHelper.ObjToInt(Instruction.Arguments[1].Value);
                            int JumpTarget = i + JumpOffset;

                            Code.AppendLine($"{IndentStr}If {Condition}");

                            i++;
                            while (i < JumpTarget && i < Instructions.Count)
                            {
                                Code.Append(DecompileInstruction(Instructions[i], Strings, Indent + 1));
                                i++;
                            }

                            Code.AppendLine($"{IndentStr}EndIf");
                            continue;
                        }

                    case OpCode.JMP:
                        i++;
                        continue;

                    case OpCode.RETURN:
                        {
                            if (Instruction.Arguments.Count > 0)
                            {
                                var ReturnValue = GetArgumentValue(Instruction.Arguments[0], Strings);
                                Code.AppendLine($"{IndentStr}Return {ReturnValue}");
                            }
                            else
                            {
                                Code.AppendLine($"{IndentStr}Return");
                            }
                            i++;
                            continue;
                        }

                    default:
                        Code.Append(DecompileInstruction(Instruction, Strings, Indent));
                        i++;
                        break;
                }
            }

            return Code.ToString();
        }

        private string DecompileInstruction(PexInstruction Instruction, List<PexString> Strings, int Indent)
        {
            string IndentStr = new string(' ', Indent * 4);
            OpCode Opcode = (OpCode)Instruction.Opcode;

            switch (Opcode)
            {
                case OpCode.ASSIGN:
                    {
                        var Dest = GetArgumentValue(Instruction.Arguments[0], Strings);
                        var Src = GetArgumentValue(Instruction.Arguments[1], Strings);
                        return $"{IndentStr}{Dest} = {Src}\n";
                    }

                case OpCode.CALLMETHOD:
                case OpCode.CALLSTATIC:
                    {
                        string Call = "";
                        foreach (var Arg in Instruction.Arguments)
                        {
                            Call += GetArgumentValue(Arg, Strings) + " ";
                        }
                        return $"{IndentStr}{Call.Trim()}\n";
                    }

                default:
                    {
                        string Line = $"{IndentStr} {Opcode}";
                        foreach (var Arg in Instruction.Arguments)
                        {
                            Line += " " + GetArgumentValue(Arg, Strings);
                        }
                        return Line + "\n";
                    }
            }
        }


        private string GetArgumentValue(PexInstructionArgument Arg, List<PexString> Strings)
        {
            int Index = ConvertHelper.ObjToInt(Arg.Value);

            switch (Arg.Type)
            {
                case 1:
                    {
                        return Strings[Index].Value;
                    }
                case 2:
                    {
                        return "\"" + Strings[Index].Value + "\"";
                    }
                case 3:
                    {
                        return ConvertHelper.ObjToStr(Index);
                    }
                case 4:
                    {
                        return ConvertHelper.ObjToStr(Arg.Value);
                    }
                default:
                    {
                        return ConvertHelper.ObjToStr(Arg.Value);
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
                        else if (GenStyle == CodeGenStyle.CSharp)
                        {
                            GenLine = string.Format(CodeSpace + "public {0}{1}({2})\n", ReturnType, Item.Value, GenParams) + CodeSpace + "{";
                        }

                        PscCode.AppendLine(GenLine);

                        if (Item.Value == "IsModFound")
                        { 
                        
                        }

                        string FunctionBody = DecompileFunction(GetFunc1st, TempStrings);

                        PscCode.Append(FunctionBody);

                        if (GenStyle == CodeGenStyle.Papyrus)
                        {
                            PscCode.AppendLine("EndFunction\n");
                        }
                        else if (GenStyle == CodeGenStyle.CSharp)
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