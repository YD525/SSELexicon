using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YDSkyrimToolR.TranslateCore
{
    /*
* @Author: YD525
* @GitHub: https://github.com/YD525/YDSkyrimToolR
* @Date: 2025-02-06
*/
    public class DivTranslateEngine
    {
        public static Dictionary<string, string> CardCaches = new Dictionary<string, string>();

        public static void AddCardItem(string Text, string Result)
        {
            if (CardCaches.ContainsKey(Text))
            {
                CardCaches[Text] = Result;
            }
            else
            {
                CardCaches.Add(Text, Result);
            }
        }

        public static void UsingCacheEngine(ref string Str)
        {
            foreach (var Get in CardCaches)
            {
                Str = Str.Replace(Get.Key, Get.Value);
            }
        }
    }
}
