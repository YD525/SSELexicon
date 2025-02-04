using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YDSkyrimToolR.SkyrimManage
{
    public class StrChecker()
    {
        public static bool ContainsChinese(string str)
        {
            foreach (char c in str)
            {
                if ((c >= '\u4e00' && c <= '\u9fa5') || (c >= '\u3400' && c <= '\u4dbf') || (c >= '\u9fc3' && c <= '\u9fff'))
                {
                    return true;
                }
            }
            return false;
        }


    }
}
