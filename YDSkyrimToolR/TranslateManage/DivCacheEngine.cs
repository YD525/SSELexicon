using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YDSkyrimToolR.TranslateCore
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

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
