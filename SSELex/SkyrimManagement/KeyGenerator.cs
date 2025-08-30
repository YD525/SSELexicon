using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using SSELex.TranslateManagement;

namespace SSELex.SkyrimManagement
{
    public class KeyGenerator
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

        public static string GenKey(CellBlock? Item)
        {
            if (Item != null)
            {
                try 
                { 
                    return "[" + Item.BlockNumber + "->" + Item.GroupType.ToString() + "]";
                }
                catch 
                {
                    return Item.GetHashCode().ToString();
                }
            }

            return string.Empty;
        }
    }
}
