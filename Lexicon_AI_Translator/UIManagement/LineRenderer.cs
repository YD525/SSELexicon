using LexTranslator.UIManage;

namespace LexTranslator.UIManagement
{
    public class LineRenderer
    {
        public static FakeGrid CreatLine(string Type, string EditorID, string Key, string SourceText, string TransText, double Score)
        {
            return UIHelper.CreatFakeLine(Type,Key,SourceText,TransText,Score);
        }
    }
}
