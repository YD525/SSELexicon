using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSELex.SkyrimManagement
{
    public enum StringsFileType
    { 
       Null = 0,Strings = 1,IL = 2,DL = 3
    }
    public class StringItem
    {
        public uint ID = 0;
        public StringsFileType Type;
        public string Value = "";
    }
    public class StringsFileReader
    {
        public Dictionary<uint,StringItem> Strings = new Dictionary<uint, StringItem>();
        public string CurrentFileName = "";

        public StringItem QueryData(string Key)
        {
            return null;
        }
    }
}
