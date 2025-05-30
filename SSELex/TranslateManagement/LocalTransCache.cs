using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SSELex.ConvertManager;
using SSELex.TranslateCore;

namespace SSELex.TranslateManagement
{
    public class LocalTransItem
    {
        public string ModName = "";
        public string Key = "";
        public string Text = "";
        public int From = 0;
        public int To = 0;
        public string Result = "";
        public int Index = 0;
        public int ColorSet = 0;

        public LocalTransItem(string Key,string FromText,string ToText)
        {
            this.ModName = DeFine.CurrentModName;
            this.Key = Key;
            this.Text = FromText;
            this.From = (int)LanguageHelper.DetectLanguageByLine(FromText);
            this.To = (int)DeFine.TargetLanguage;
            this.Result = ToText;
            this.Index = 0;
            this.ColorSet = ConvertHelper.ObjToInt(ColorSet);
        }

        public LocalTransItem(object ModName, object Text, object From, object To, object Result)
        {
            this.ModName = ConvertHelper.ObjToStr(ModName);
            this.Text = ConvertHelper.ObjToStr(Text);
            this.From = ConvertHelper.ObjToInt(From);
            this.To = ConvertHelper.ObjToInt(To);
            this.Result = ConvertHelper.ObjToStr(Result);
            this.Index = 0; 
            this.ColorSet = ConvertHelper.ObjToInt(ColorSet);
        }
    }
    public class LocalTransCache
    {
        public static bool DeleteCacheByModName()
        {
            try
            {
                string SqlOrder = "Delete From LocalTranslation Where [ModName] = '{0}'";

                int State = DeFine.GlobalDB.ExecuteNonQuery(string.Format(SqlOrder, DeFine.CurrentModName));

                if (State != 0)
                {
                    return true;
                }

                return false;
            }
            catch { return false; }
        }

        public static bool DeleteCacheByResult(string Key,string ResultText)
        {
            try
            {
                string SqlOrder = "Delete From LocalTranslation Where [ModName] = '{0}' And [Key] = '{1}' And [Result] = '{2}'";

                int State = DeFine.GlobalDB.ExecuteNonQuery(string.Format(SqlOrder, DeFine.CurrentModName,Key,System.Web.HttpUtility.HtmlEncode(ResultText)));

                if (State != 0)
                {
                    return true;
                }

                return false;
            }
            catch { return false; }
        }

        public static bool DeleteCache(string Key,string Text)
        {
            try
            {
                string SqlOrder = "Delete From LocalTranslation Where [ModName] = '{0}' And [Key] = '{1}' And [Text] = '{2}' And [From] = {3} And [To] = {4}";

                int State = DeFine.GlobalDB.ExecuteNonQuery(string.Format(SqlOrder, DeFine.CurrentModName,Key,System.Web.HttpUtility.HtmlEncode(Text), (int)LanguageHelper.DetectLanguageByLine(Text), (int)DeFine.TargetLanguage));

                if (State!=0)
                {
                    return true;
                }

                return false;
            }
            catch { return false; }
        }
        public static bool QFindCache(string Key,string Text)
        {
            try
            {
                string SqlOrder = "Select Rowid From LocalTranslation Where [ModName] = '{0}' And [Key] = '{1}' And [Text] = '{2}' And [From] = {3} And [To] = {4}";

                int GetRowid = ConvertHelper.ObjToInt(DeFine.GlobalDB.ExecuteScalar(string.Format(SqlOrder,DeFine.CurrentModName,Key,System.Web.HttpUtility.HtmlEncode(Text), (int)LanguageHelper.DetectLanguageByLine(Text), (int)DeFine.TargetLanguage)));

                if (GetRowid>0)
                {
                    return true;
                }

                return false;
            }
            catch { return false; }
        }

        public static string GetCacheText(string Key,string Text)
        {
            try
            {
                string SqlOrder = "Select Result From LocalTranslation Where [ModName] = '{0}' And[Key] = '{1}' And [Text] = '{2}' And [From] = {3} And [To] = {4}";

                string GetText = ConvertHelper.ObjToStr(DeFine.GlobalDB.ExecuteScalar(string.Format(SqlOrder, DeFine.CurrentModName,Key,System.Web.HttpUtility.HtmlEncode(Text), (int)LanguageHelper.DetectLanguageByLine(Text), (int)DeFine.TargetLanguage)));

                if (GetText.Trim().Length > 0)
                {
                    return System.Web.HttpUtility.HtmlDecode(GetText);
                }

                return string.Empty;
            }
            catch { return string.Empty; }
        }

        public static string FindCache(string ModName,string Key, string Text, int From, int To)
        {
            try
            {
                string SqlOrder = "Select Result From LocalTranslation Where [ModName] = '{0}' And [Key] = '{1}' And [Text] = '{2}' And [From] = {3} And [To] = {4}";

                string GetResult = ConvertHelper.ObjToStr(DeFine.GlobalDB.ExecuteScalar(string.Format(SqlOrder,ModName,Key,System.Web.HttpUtility.HtmlEncode(Text), From, To)));

                if (GetResult.Trim().Length > 0)
                {
                    return System.Web.HttpUtility.HtmlDecode(GetResult);
                }

                return string.Empty;
            }
            catch { return string.Empty; }
        }    

        public static bool UPDateLocalTransItem(LocalTransItem Item)
        {
            if (Item.ModName.Trim().Length == 0)
            {
                return false;
            }

            string FindCache = TranslateDBCache.FindCache(Item.ModName, Item.Text, Item.From, Item.To);

            if (FindCache.Trim().Length > 0)
            {
                if (FindCache.Equals(Item.Result))
                {
                    DeleteCache(Item.Key,Item.Text);
                    return false;
                }
            }

            int GetRowID = ConvertHelper.ObjToInt(DeFine.GlobalDB.ExecuteScalar(String.Format("Select Rowid From LocalTranslation Where [ModName] = '{0}' And [Key] = '{1}' And [Text] = '{2}' And [From] = {3} And [To] = {4}", Item.ModName,Item.Key,System.Web.HttpUtility.HtmlEncode(Item.Text), Item.From, Item.To)));

            if (GetRowID < 0 && Item.Result.Trim().Length > 0)
            {
                string SqlOrder = "Insert Into LocalTranslation([ModName],[Key],[Text],[From],[To],[Result],[Index],[ColorSet])Values('{0}','{1}','{2}',{3},{4},'{5}',{6},{7})";
                int State = DeFine.GlobalDB.ExecuteNonQuery(string.Format(SqlOrder,
                    Item.ModName,
                    Item.Key,
                    System.Web.HttpUtility.HtmlEncode(Item.Text),
                    Item.From,
                    Item.To,
                    System.Web.HttpUtility.HtmlEncode(Item.Result),
                    Item.Index,
                    Item.ColorSet
                    ));
                if (State != 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                string SqlOrder = "UPDate LocalTranslation Set [Result] = '{1}',[Index] = {2},[ColorSet] = {3} Where Rowid = {0}";
                int State = DeFine.GlobalDB.ExecuteNonQuery(string.Format(SqlOrder,GetRowID,System.Web.HttpUtility.HtmlEncode(Item.Result),Item.Index,Item.ColorSet));
                if (State != 0)
                {
                    return true;
                }
                return false;
            }  
        }

    }
}
