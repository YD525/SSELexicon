using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
* @Author: 约定
* @GitHub: https://github.com/tolove336/YDSkyrimToolR
* @Date: 2025-02-06
*/
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
