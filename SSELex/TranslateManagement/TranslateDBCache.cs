using System.Data;
using System.Windows.Forms;
using SSELex.ConvertManager;

namespace SSELex.TranslateCore
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class TranslateDBCache
    {
        public static bool DeleteCache(string Text)
        {
            try
            {
                string SqlOrder = "Delete From CloudTranslation Where [ModName] = '{0}' And [Text] = '{1}' And [From] = {2} And [To] = {3}";

                int State = DeFine.GlobalDB.ExecuteNonQuery(string.Format(SqlOrder,DeFine.CurrentModName, System.Web.HttpUtility.HtmlEncode(Text),(int)LanguageHelper.DetectLanguageByLine(Text),(int)DeFine.TargetLanguage));

                if (State!=0)
                {
                    return true;
                }

                return false;
            }
            catch { return false; }
        }
        public static string FindCache(string Text)
        {
            return FindCache(DeFine.CurrentModName,Text,(int)LanguageHelper.DetectLanguageByLine(Text),(int)DeFine.TargetLanguage);
        }
        public static string FindCache(string ModName,string Text,int From,int To)
        {
            try { 
            string SqlOrder = "Select Result From CloudTranslation Where [ModName] = '{0}' And [Text] = '{1}' And [From] = {2} And [To] = {3}";

            string GetResult = ConvertHelper.ObjToStr(DeFine.GlobalDB.ExecuteScalar(string.Format(SqlOrder, ModName, System.Web.HttpUtility.HtmlEncode(Text),From,To)));

            if (GetResult.Trim().Length > 0)
            {
                return System.Web.HttpUtility.HtmlDecode(GetResult);
            }

            return string.Empty;
            }
            catch { return string.Empty; }
        }

        public static bool AddCache(string ModName, string Text, int From, int To,string Result)
        {
            try {
            int GetRowID = ConvertHelper.ObjToInt(DeFine.GlobalDB.ExecuteScalar(String.Format("Select Rowid From CloudTranslation Where [ModName] = '{0}' And [Text] = '{1}' And [From] = {2} And [To] = {3}",ModName, System.Web.HttpUtility.HtmlEncode(Text),From,To)));

            if (GetRowID < 0)
            {
                string SqlOrder = "Insert Into CloudTranslation([ModName],[Text],[From],[To],[Result])Values('{0}','{1}',{2},{3},'{4}')";

                int State = DeFine.GlobalDB.ExecuteNonQuery(string.Format(SqlOrder,ModName,System.Web.HttpUtility.HtmlEncode(Text), From, To, System.Web.HttpUtility.HtmlEncode(Result)));

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


        public static string FindCacheAndID(string ModName,string Text,ref int ID, int From, int To)
        {
            try { 
            string SqlOrder = "Select Rowid,Result From CloudTranslation Where [ModName] = '{0}' And [Text] = '{1}' And [From] = {2} And [To] = {3}";

            DataTable GetResult = DeFine.GlobalDB.ExecuteQuery(string.Format(SqlOrder,ModName,System.Web.HttpUtility.HtmlEncode(Text), From, To));

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
