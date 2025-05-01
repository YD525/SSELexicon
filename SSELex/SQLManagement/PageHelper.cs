using System.Data;
using SSELex.ConvertManager;

namespace SSELex.SqlManager
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class PageHelper
    {

        public static int GetPageCount(string TableName, string Where)
        {
            int GetCount = ConvertHelper.ObjToInt(DeFine.GlobalDB.ExecuteScalar(string.Format("Select Count(*) From {0} ", TableName) + Where));
            int PageCount = GetCount / DeFine.DefPageSize;
            if (GetCount % DeFine.DefPageSize > 0)
            {
                PageCount++;
            }
            return PageCount;
        }

        public static DataTable GetTablePageData(string TableName, int PageNo, int Count, string Where = "")
        {
            if (Where.Trim().Length > 0)
            {
                Where += " ";
            }
            string SqlOrder = string.Format("Select Rowid,* From {0} ", TableName) + Where + string.Format("Order BY Rowid Desc Limit (({0}-1)*{1}),{1};", PageNo, Count);
            return DeFine.GlobalDB.ExecuteQuery(SqlOrder);
        }

    }

    public class PageItem<T> where T : new()
    {
        public T CurrentPage = new T();
        public int PageNo = 0;
        public int MaxPage = 0;

        public PageItem(T Source, int PageNo, int MaxPage)
        {
            this.CurrentPage = Source;
            this.PageNo = PageNo;
            this.MaxPage = MaxPage;
        }
    }
}
