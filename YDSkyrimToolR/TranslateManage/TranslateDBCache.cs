
using System;
using System.Data;
using System.Security.Policy;
using System.Windows.Forms;
using YDSkyrimToolR.ConvertManager;

namespace YDSkyrimToolR.TranslateCore
{
    public class TranslateDBCache
    {
        public static string FindCache(string Text,int From,int To)
        {
            try { 
            string SqlOrder = "Select Result From CloudTranslation Where [Text] = '{0}' And [From] = {1} And [To] = {2}";

            string GetResult = ConvertHelper.ObjToStr(DeFine.GlobalDB.ExecuteScalar(string.Format(SqlOrder, System.Web.HttpUtility.HtmlEncode(Text), From,To)));

            if (GetResult.Trim().Length > 0)
            {
                return System.Web.HttpUtility.HtmlDecode(GetResult);
            }

            return string.Empty;
            }
            catch { return string.Empty; }
        }

        public static bool AddCache(string Text, int From, int To,string Result)
        {
            try {
            int GetRowID = ConvertHelper.ObjToInt(DeFine.GlobalDB.ExecuteScalar(String.Format("Select Rowid From CloudTranslation Where [Text] = '{0}' And [From] = {1} And [To] = {2}", System.Web.HttpUtility.HtmlEncode(Text), From,To)));

            if (GetRowID < 0)
            {
                string SqlOrder = "Insert Into CloudTranslation([Text],[From],[To],[Result])Values('{0}',{1},{2},'{3}')";

                int State = DeFine.GlobalDB.ExecuteNonQuery(string.Format(SqlOrder, System.Web.HttpUtility.HtmlEncode(Text), From, To, System.Web.HttpUtility.HtmlEncode(Result)));

                if (State != 0)
                {
                    return true;
                }

                return false;
            }

            return false;
            }
            catch { return false; }
        }


        public static string FindCacheAndID(string Text,ref int ID, int From, int To)
        {
            try { 
            string SqlOrder = "Select Rowid,Result From CloudTranslation Where [Text] = '{0}' And [From] = {1} And [To] = {2}";

            DataTable GetResult = DeFine.GlobalDB.ExecuteQuery(string.Format(SqlOrder, System.Web.HttpUtility.HtmlEncode(Text), From, To));

            if (GetResult.Rows.Count > 0)
            {
                string GetStr = System.Web.HttpUtility.HtmlDecode(ConvertHelper.ObjToStr(GetResult.Rows[0]["Result"]));
                ID = ConvertHelper.ObjToInt(GetResult.Rows[0]["Rowid"]);
                return GetStr;
            }

            return string.Empty;
            }
            catch {return string.Empty; }
        }

        public static bool DeleteCacheByID(int Rowid)
        {
            try {
            string SqlOrder = "Delete From CloudTranslation Where Rowid = {0}";
            int State = DeFine.GlobalDB.ExecuteNonQuery(string.Format(SqlOrder,Rowid));
            if (State != 0)
            {
                return true;
            }
            return false;
            }
            catch { return false; }
        }
    }
}
