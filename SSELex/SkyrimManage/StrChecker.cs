using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSELex.SkyrimManage
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

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
