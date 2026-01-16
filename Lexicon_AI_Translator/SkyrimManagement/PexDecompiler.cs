using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

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
            if (Reader.Objects.Count > 0)
            {
                string ScriptName = Reader.StringTable[Reader.Objects[0].NameIndex].Value;
                string ParentClass = Reader.StringTable[Reader.Objects[0].ParentClassNameIndex].Value;
            }

            foreach (var GetItem in Reader.StringTable)
            { 
                
            }
            return string.Empty;
        }


    }
}