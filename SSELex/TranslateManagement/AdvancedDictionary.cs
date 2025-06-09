using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SSELex.ConvertManager;
using SSELex.TranslateCore;

namespace SSELex.TranslateManagement
{
    public class AdvancedDictionaryItem
    {
        public string TargetModName = "";
        public string Type = "";
        public string Source = "";
        public string Result = "";
        public int From = 0;
        public int To = 0;
        public int ExactMatch = 0;
        public int IgnoreCase = 0;
        public string Regex = "";

        public AdvancedDictionaryItem()
        {

        }
        public AdvancedDictionaryItem(object TargetModName,object Type, object Source, object Result, object From, object To, object ExactMatch, object IgnoreCase, object Regex)
        {
            this.TargetModName = ConvertHelper.ObjToStr(TargetModName);
            this.Type = ConvertHelper.ObjToStr(Type);
            this.Source = ConvertHelper.ObjToStr(Source);
            this.Result = ConvertHelper.ObjToStr(Result);
            this.From = ConvertHelper.ObjToInt(From);
            this.To = ConvertHelper.ObjToInt(To);
            this.ExactMatch = ConvertHelper.ObjToInt(ExactMatch);
            this.IgnoreCase = ConvertHelper.ObjToInt(IgnoreCase);
            this.Regex = ConvertHelper.ObjToStr(Regex);
        }
    }
    public class AdvancedDictionary
    {
        public static void Init()
        {
            string CheckTableSql = "SELECT name FROM sqlite_master WHERE type='table' AND name='AdvancedDictionary';";

            var Result = DeFine.GlobalDB.ExecuteScalar(CheckTableSql);
            if (Result == null || Result == DBNull.Value)
            {
                string CreateTableSql = @"CREATE TABLE [AdvancedDictionary](
  [TargetModName] TEXT, 
  [Type] TEXT, 
  [Source] TEXT, 
  [Result] TEXT, 
  [From] INT, 
  [To] INT, 
  [ExactMatch] INT, 
  [IgnoreCase] INT, 
  [Regex] TEXT);
";

                DeFine.GlobalDB.ExecuteNonQuery(CreateTableSql);
            }
        }

        public static bool IsRegexMatch(string Input, string SetRegex)
        {
            try
            {
                return Regex.IsMatch(Input, SetRegex);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string EscapeSqlString(string S)
        {
            if (string.IsNullOrEmpty(S)) return S;
            S = S.Replace("'", "''");
            S = S.Replace(@"\", @"\\");
            S = S.Replace("%", @"\%");
            S = S.Replace("_", @"\_");
            return S;
        }

        public static List<AdvancedDictionaryItem> Query(string ModName, string Type, Languages From, Languages To, string SourceText)
        {
            List<AdvancedDictionaryItem> AdvancedDictionaryItems = new List<AdvancedDictionaryItem>();
            string SqlOrder = @"
SELECT * FROM AdvancedDictionary
WHERE 
  (
    TargetModName IS NULL
    OR TargetModName = ''
    OR TargetModName = '{0}'
  )
  AND (
    [Type] IS NULL
    OR [Type] = ''
    OR [Type] = '{1}'
  )
  AND [From] = {2}
  AND [To] = {3}
  AND (
    (ExactMatch = 1 AND (
      (IgnoreCase = 1 AND LOWER(Source) = LOWER('{4}'))
      OR (IgnoreCase = 0 AND Source = '{4}')
    ))
    OR
    (ExactMatch = 0 AND (
      (IgnoreCase = 1 AND LOWER('{4}') LIKE '%' || LOWER(Source) || '%')
      OR (IgnoreCase = 0 AND '{4}' LIKE '%' || Source || '%')
    ))
  )
";
            DataTable NTable = DeFine.GlobalDB.ExecuteQuery(string.Format(
                SqlOrder,
                EscapeSqlString(ModName),
                EscapeSqlString(Type),
                (int)From,
                (int)To,
                EscapeSqlString(SourceText)
            ));

            for (int i = 0; i < NTable.Rows.Count; i++)
            {
                var Get = new AdvancedDictionaryItem(
                    NTable.Rows[i]["TargetModName"],
                    NTable.Rows[i]["Type"],
                    NTable.Rows[i]["Source"],
                    NTable.Rows[i]["Result"],
                    NTable.Rows[i]["From"],
                    NTable.Rows[i]["To"],
                    NTable.Rows[i]["ExactMatch"],
                    NTable.Rows[i]["IgnoreCase"],
                    NTable.Rows[i]["Regex"]
                );
                if (Get.Regex.Trim().Length > 0)
                {
                    if (IsRegexMatch(SourceText, Get.Regex))
                    {
                        AdvancedDictionaryItems.Add(Get);
                    }
                }
                else
                {
                    AdvancedDictionaryItems.Add(Get);
                }
            }

            return AdvancedDictionaryItems;
        }
    }
}
