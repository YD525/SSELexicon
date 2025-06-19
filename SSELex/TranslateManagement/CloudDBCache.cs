using System.Data;
using SSELex.ConvertManager;

namespace SSELex.TranslateCore
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/PhoenixEngine

    public class CloudDBCache
    {
        public static bool DeleteCache(string Key)
        {
            try
            {
                string SqlOrder = "Delete From CloudTranslation Where [ModName] = '{0}' And [Key] = '{1}' And [To] = {2}";

                int State = DeFine.GlobalDB.ExecuteNonQuery(string.Format(SqlOrder,DeFine.CurrentModName,Key,(int)DeFine.TargetLanguage));

                if (State!=0)
                {
                    return true;
                }

                return false;
            }
            catch { return false; }
        }
        public static string FindCache(string Key)
        {
            return FindCache(DeFine.CurrentModName,Key);
        }
        public static string FindCache(string ModName,string Key)
        {
            try { 
            string SqlOrder = "Select Result From CloudTranslation Where [ModName] = '{0}' And [Key] = '{1}' And [To] = {2}";

            string GetResult = ConvertHelper.ObjToStr(DeFine.GlobalDB.ExecuteScalar(string.Format(SqlOrder, ModName, Key,(int)DeFine.TargetLanguage)));

            if (GetResult.Trim().Length > 0)
            {
                return System.Web.HttpUtility.HtmlDecode(GetResult);
            }

            return string.Empty;
            }
            catch { return string.Empty; }
        }

        public static bool AddCache(string ModName, string Key, int To,string Result)
        {
            if (ModName.Trim().Length == 0)
            {
               return false;
            }
            try {
            int GetRowID = ConvertHelper.ObjToInt(DeFine.GlobalDB.ExecuteScalar(String.Format("Select Rowid From CloudTranslation Where [ModName] = '{0}' And [Key] = '{1}' And [To] = {2}",ModName,Key,To)));

            if (GetRowID < 0)
            {
                string SqlOrder = "Insert Into CloudTranslation([ModName],[Key],[To],[Result])Values('{0}','{1}',{2},'{3}')";

                int State = DeFine.GlobalDB.ExecuteNonQuery(string.Format(SqlOrder,ModName,Key, To, System.Web.HttpUtility.HtmlEncode(Result)));

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


        public static string FindCacheAndID(string ModName,string Key, int To,ref int ID)
        {
            try { 
            string SqlOrder = "Select Rowid,Result From CloudTranslation Where [ModName] = '{0}' And [Key] = '{1}' And [To] = {2}";

            DataTable GetResult = DeFine.GlobalDB.ExecuteQuery(string.Format(SqlOrder,ModName,Key,To));

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
