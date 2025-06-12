using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mutagen.Bethesda.Plugins;
using SSELex.SkyrimManage;

namespace SSELex.SkyrimManagement
{
    public class KeyGenter
    {
        public static string GenKey(FormKey? Key, string? EditID)
        {
            string MergeKey = "";

            if (Key != null)
            {
                MergeKey = "[" + Crc32Helper.ComputeCrc32(Key.ToString()) + "]";
            }

            if (EditID != null)
            {
                MergeKey += EditID;
            }

            return MergeKey;
        }
    }
}
