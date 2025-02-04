using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YDSkyrimToolR.TranslateManage
{
    public class Translator
    {
        public static bool StopAny = false;

        public static Dictionary<int, string> TransData = new Dictionary<int, string>();

        public static void ClearCache()
        {
            TransData.Clear();
        }
    }
}
