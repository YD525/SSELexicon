using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YDSkyrimToolR.ConvertManager;

namespace YDSkyrimToolR.SqlManager
{
    /*
    * @Author: 约定
    * @GitHub: https://github.com/tolove336/YDSkyrimToolR
    * @Date: 2025-02-06
    */
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
