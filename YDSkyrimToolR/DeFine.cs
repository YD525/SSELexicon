using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace YDSkyrimToolR
{
    /*
    * @Author: 约定
    * @GitHub: https://github.com/tolove336/YDSkyrimToolR
    * @Date: 2025-02-06
    */
    public class DeFine
    {
        public static string Version = "3.1.1 Alpha";
        public static MainWindow WorkingWin = null;

        public static void Init(MainWindow Work)
        {
            WorkingWin = Work;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
    }
}
