using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using static LexTranslator.SkyrimManagement.PexReader;
using System.ComponentModel.DataAnnotations;
using LexTranslator.ConvertManager;

namespace LexTranslator.SkyrimManagement
{
    public class PexDecompiler
    {
        public PexReader Reader;

        public PexDecompiler(PexReader CurrentReader)
        { 
            Reader = CurrentReader;
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

        public string Decompile()
        {
            StringBuilder PscCode = new StringBuilder();
            List<PexString> TempStrings = new List<PexString>();

            TempStrings.AddRange(Reader.StringTable);

            if (Reader.Objects.Count > 0)
            {
                string ScriptName = TempStrings[Reader.Objects[0].NameIndex].Value;
                string ParentClass = TempStrings[Reader.Objects[0].ParentClassNameIndex].Value;
                PscCode.AppendLine(string.Format("ScriptName {0} Extends {1}",ScriptName,ParentClass));
            }

            //First, Find Global Variables
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
                            PscCode.AppendLine(string.Format(GetVariableType + " " + Item.Value));
                        }
                        else
                        {
                            if (GetVariableType.ToLower().Equals("string"))
                            {
                                PscCode.AppendLine(string.Format(GetVariableType + " " + Item.Value 
                                + " = " + "\"" + TryGetValue + "\""));
                            }
                            else
                            {
                                PscCode.AppendLine(string.Format(GetVariableType + " " + Item.Value + " = " + TryGetValue));
                            }
                               
                        }
                        
                    }
                }
            }

            var GetCode = PscCode.ToString();


            return string.Empty;
        }


    }
}