using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using static LexTranslator.SkyrimManagement.PexReader;
using LexTranslator.ConvertManager;

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
          Null=0,Papyrus = 1,CSharp = 2,Python= 3
        }

        #endregion

        public static string Version = "1.0.0 Alpha";
        public CodeGenStyle GenStyle = CodeGenStyle.Null;
        public PexReader Reader;

        public PexDecompiler(PexReader CurrentReader,CodeGenStyle GenStyle = CodeGenStyle.Papyrus)
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
           Null=0,Variables = 1, Properties = 2
        }
        public object QueryAnyByID(int ID,ref ObjType Type)
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
                    PscCode.AppendLine(string.Format("public class  {0} : {1} \n {", ScriptName, ParentClass));
                }
            }
        }

        public void AnalyzeGlobalVariables(List<PexString> TempStrings,ref StringBuilder PscCode)
        {
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
                                PscCode.AppendLine(string.Format(GetVariableType + " " + Item.Value + ";"));
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
                                    PscCode.AppendLine(string.Format(GetVariableType + " " + Item.Value
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
                                    PscCode.AppendLine(string.Format(GetVariableType + " " + Item.Value + " = " + TryGetValue + ";"));
                                }
                                   
                            }

                        }

                    }
                }
            }
        }

        public void AnalyzeAutoGlobalVariables(List<PexString> TempStrings, ref StringBuilder PscCode)
        {
            for (int i = 0; i < TempStrings.Count; i++)
            {
                var Item = TempStrings[i];
                ObjType CheckType = ObjType.Null;

                var TempValue = QueryAnyByID(Item.Index, ref CheckType);

                if (Item.Value.Equals("GoldTheftPercentage"))
                { 
                
                }

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

                        var RealValue = QueryAnyByID(Property.AutoVarNameIndex, ref CheckType);
                        if (CheckType == ObjType.Variables)
                        {
                            string GetRealValue = ConvertHelper.ObjToStr((RealValue as PexVariable).DataValue);
                        }

                        if (this.GenStyle == CodeGenStyle.Papyrus)
                        {
                            PscCode.AppendLine(string.Format(GetVariableType + " Property " + Item.Value + " Auto"));
                        }
                        else
                        if (this.GenStyle == CodeGenStyle.CSharp)
                        {
                            PscCode.AppendLine("[Property(Auto = true)]");
                            PscCode.AppendLine(string.Format(GetVariableType + " " + Item.Value + ";"));
                        }
                    }
                   
                }
            }
        }
        public void AnalyzeFunction(List<PexString> TempStrings, ref StringBuilder PscCode)
        {

        }

        public string Decompile()
        {
            StringBuilder PscCode = new StringBuilder();
            List<PexString> TempStrings = new List<PexString>();

            TempStrings.AddRange(Reader.StringTable);

            AnalyzeClass(TempStrings, ref PscCode);

            //First, Find Global Variables
            AnalyzeGlobalVariables(TempStrings, ref PscCode);

            //Float Property xxx = 50.0 Auto hidden
            AnalyzeAutoGlobalVariables(TempStrings, ref PscCode);

            //Function XXX() EndFunction
            AnalyzeFunction(TempStrings, ref PscCode);


            if (this.GenStyle == CodeGenStyle.CSharp)
            {
                PscCode.AppendLine("}");
            }

            var GetCode = PscCode.ToString();

            return string.Empty;
        }


    }
}