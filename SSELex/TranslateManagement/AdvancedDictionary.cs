using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Mutagen.Bethesda.Skyrim;
using SSELex.ConvertManager;
using SSELex.SqlManager;
using SSELex.TranslateCore;

namespace SSELex.TranslateManagement
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/PhoenixEngine
    public class AdvancedDictionaryItem
    {
        public int Rowid = 0;
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
        public AdvancedDictionaryItem(object TargetModName, object Type, object Source, object Result, object From, object To, object ExactMatch, object IgnoreCase, object Regex)
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
        public AdvancedDictionaryItem(object Rowid,object TargetModName, object Type, object Source, object Result, object From, object To, object ExactMatch, object IgnoreCase, object Regex)
        {
            this.Rowid = ConvertHelper.ObjToInt(Rowid);
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

        public static string GetSourceByRowid(int Rowid)
        {
            string SqlOrder = "Select [Source] From AdvancedDictionary Where Rowid = {0}";
            return ConvertHelper.ObjToStr(DeFine.GlobalDB.ExecuteScalar(string.Format(SqlOrder,Rowid)));
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
SELECT Rowid,* FROM AdvancedDictionary
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
                    NTable.Rows[i]["Rowid"],
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
                    if (IsRegexMatch(SourceText,System.Web.HttpUtility.HtmlDecode(Get.Regex)))
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

        public static bool CheckSame(AdvancedDictionaryItem item)
        {
            string CheckSql = $@"
SELECT COUNT(*) FROM AdvancedDictionary 
WHERE 
[TargetModName] = '{EscapeSqlString(item.TargetModName)}' AND
[Type] = '{EscapeSqlString(item.Type)}' AND
[Source] = '{EscapeSqlString(item.Source)}' AND
[Result] = '{EscapeSqlString(item.Result)}' AND
[From] = {item.From} AND
[To] = {item.To}";

            int Count = Convert.ToInt32(DeFine.GlobalDB.ExecuteScalar(CheckSql));
            return Count > 0;
        }


        public static bool AddItem(AdvancedDictionaryItem Item)
        {
            if (!CheckSame(Item))
            {
                string sql = $@"INSERT INTO AdvancedDictionary 
([TargetModName], [Type], [Source], [Result], [From], [To], [ExactMatch], [IgnoreCase], [Regex])
VALUES (
'{EscapeSqlString(Item.TargetModName)}',
'{EscapeSqlString(Item.Type)}',
'{EscapeSqlString(Item.Source)}',
'{EscapeSqlString(Item.Result)}',
{Item.From},
{Item.To},
{Item.ExactMatch},
{Item.IgnoreCase},
'{System.Web.HttpUtility.HtmlEncode(Item.Regex)}'
)";
                int State = DeFine.GlobalDB.ExecuteNonQuery(sql);
                if (State != 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public static void DeleteItem(AdvancedDictionaryItem item)
        {
            string sql = $@"DELETE FROM AdvancedDictionary WHERE 
TargetModName = '{EscapeSqlString(item.TargetModName)}' AND
Type = '{EscapeSqlString(item.Type)}' AND
Source = '{EscapeSqlString(item.Source)}' AND
Result = '{EscapeSqlString(item.Result)}' AND
[From] = {item.From} AND
[To] = {item.To} AND
ExactMatch = {item.ExactMatch} AND
IgnoreCase = {item.IgnoreCase} AND
Regex = '{System.Web.HttpUtility.HtmlEncode(item.Regex)}'";
            DeFine.GlobalDB.ExecuteNonQuery(sql);
        }

        public static PageItem<List<AdvancedDictionaryItem>> QueryByPage(int From, int To, int PageNo)
        {
            string Where = $"WHERE [From] = {From} And [To] = {To}";

            int MaxPage = PageHelper.GetPageCount("AdvancedDictionary", Where);

            DataTable NTable = PageHelper.GetTablePageData("AdvancedDictionary", PageNo, DeFine.DefPageSize, Where);

            List<AdvancedDictionaryItem> Items = new List<AdvancedDictionaryItem>();
            for (int i = 0; i < NTable.Rows.Count; i++)
            {
                DataRow Row = NTable.Rows[i];
                Items.Add(new AdvancedDictionaryItem(
                    Row["Rowid"],
                    Row["TargetModName"],
                    Row["Type"],
                    Row["Source"],
                    Row["Result"],
                    Row["From"],
                    Row["To"],
                    Row["ExactMatch"],
                    Row["IgnoreCase"],
                    Row["Regex"]
                ));
            }

            return new PageItem<List<AdvancedDictionaryItem>>(Items, PageNo, MaxPage);
        }

        public static PageItem<List<AdvancedDictionaryItem>> QueryByPage(string SourceText,int From,int To, int PageNo)
        {
            string Where = $"WHERE Source = '{EscapeSqlString(SourceText)}' And [From] = {From} And [To] = {To}";

            int MaxPage = PageHelper.GetPageCount("AdvancedDictionary", Where);

            DataTable NTable = PageHelper.GetTablePageData("AdvancedDictionary", PageNo, DeFine.DefPageSize, Where);

            List<AdvancedDictionaryItem> Items = new List<AdvancedDictionaryItem>();
            for (int i = 0; i < NTable.Rows.Count; i++)
            {
                DataRow Row = NTable.Rows[i];
                Items.Add(new AdvancedDictionaryItem(
                    Row["TargetModName"],
                    Row["Type"],
                    Row["Source"],
                    Row["Result"],
                    Row["From"],
                    Row["To"],
                    Row["ExactMatch"],
                    Row["IgnoreCase"],
                    Row["Regex"]
                ));
            }

            return new PageItem<List<AdvancedDictionaryItem>>(Items, PageNo, MaxPage);
        }

        public static bool DeleteByRowid(int Rowid)
        {
            string SqlOrder = "Delete From AdvancedDictionary Where Rowid = {0}";
            int State = DeFine.GlobalDB.ExecuteNonQuery(string.Format(SqlOrder,Rowid));
            if (State != 0)
            {
                return true;
            }
            return false;
        }

    }
}
