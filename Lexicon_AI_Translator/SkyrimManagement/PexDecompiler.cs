using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using static LexTranslator.SkyrimManagement.PexReader;

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

        public string Decompile()
        {
            StringBuilder PscCode = new StringBuilder();
            List<PexReader.PexString> TempStrings = new List<PexReader.PexString>();

            TempStrings.AddRange(Reader.StringTable);

            List<PexString> WaitDeleteStrings = new List<PexString>();

            if (Reader.Objects.Count > 0)
            {
                var NameIndexObj = TempStrings[Reader.Objects[0].NameIndex];
                var ParentClassNameObj = TempStrings[Reader.Objects[0].ParentClassNameIndex];

                string ScriptName = NameIndexObj.Value;
                string ParentClass = ParentClassNameObj.Value;
                PscCode.AppendLine(string.Format("ScriptName {0} Extends {1}",ScriptName,ParentClass));

                WaitDeleteStrings.Add(NameIndexObj);
                WaitDeleteStrings.Add(ParentClassNameObj);
            }

            foreach (var GetWaitDelete in WaitDeleteStrings)
            {
                TempStrings.Remove(GetWaitDelete);
            }

            //First, Find Global Variables
            for (int i = 0; i < TempStrings.Count; i++)
            { 
               
            }

            return string.Empty;
        }


    }
}