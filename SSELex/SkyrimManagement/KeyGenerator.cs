using Mutagen.Bethesda.Plugins;
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
    }
}
