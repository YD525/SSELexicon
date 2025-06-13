using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSELex.LanguageDetector
{
    public class TraditionalChineseHelper
    {
        //We should consider collecting all traditional Chinese characters into a database for matching. But... let's not do that for now.
        public static bool ContainsTraditionalChinese(string Input)
        {
            return false;
        }

        public static double GetTraditionalChineseRatio(string Input)
        {
            return 0;
        }
    }
}
