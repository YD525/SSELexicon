using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSELex.UIManage;

namespace SSELex.UIManagement
{
    public class LineRenderer
    {
        public static FakeGrid CreatLine(string Type, string EditorID, string Key, string SourceText, string TransText, double Score)
        {
            return UIHelper.CreatFakeLine(Type,EditorID,Key,SourceText,TransText,Score);
        }
    }
}
