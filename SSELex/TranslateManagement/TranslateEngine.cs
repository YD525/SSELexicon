using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSELex.SkyrimManage;
using System.Windows.Media;

namespace SSELex.TranslateManagement
{
    public class TranslateEngine
    {
        public static void Start()
        {
            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                FakeGrid GetFakeGrid = DeFine.WorkingWin.TransViewList.RealLines[i];
                GetFakeGrid.UPDataThis();
                
               
            }


        }
    }
}
