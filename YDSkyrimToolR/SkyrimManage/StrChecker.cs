using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YDSkyrimToolR.SkyrimManage
{
    /*
* @Author: YD525
* @GitHub: https://github.com/YD525/YDSkyrimToolR
* @Date: 2025-02-06
*/
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
