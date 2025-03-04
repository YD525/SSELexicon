using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace YDSkyrimToolR.SQLManager
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class ConvertT
    {
        public static T ConverToObject<T>(object asobject)
        {
            //创建实体对象实例

            var t = Activator.CreateInstance<T>();

            if (asobject != null)

            {

                Type type = asobject.GetType();

                //遍历实体对象属性

                foreach (var info in typeof(T).GetProperties())

                {

                    object obj = null;

                    //取得object对象中此属性的值

                    var val = type.GetProperty(info.Name)?.GetValue(asobject);

                    if (val != null)

                    {

                        //非泛型

                        if (!info.PropertyType.IsGenericType)

                            obj = Convert.ChangeType(val, info.PropertyType);

                        else//泛型nullable<>

                        {

                            Type generictypedefinition = info.PropertyType.GetGenericTypeDefinition();

                            if (generictypedefinition == typeof(Nullable<>))

                            {

                                obj = Convert.ChangeType(val, Nullable.GetUnderlyingType(info.PropertyType));

                            }

                        }

                        info.SetValue(t, obj, null);

                    }

                }

            }

            return t;

        }

    }

    public class SqlCore<T> where T : new()
    {
        public object LockerDB = new object();
        public T SQLHelper;
        public string ConnectStr = "";
        public SqlType ThisType = SqlType.Null;
        public SqlCore(string ConnectStr)
        {
            this.SQLHelper = new T();

            if (SQLHelper is SQLiteHelper)
            {
                (this.SQLHelper as SQLiteHelper).OpenSql(ConnectStr);
                this.ThisType = SqlType.SQLite;
            }
            

            this.ConnectStr = ConnectStr;
        }

       
        public int ExecuteNonQuery(string SqlOrder)
        {
            lock (LockerDB)
            {
                int State = -9;
                switch (this.ThisType)
                {
                    case SqlType.SQLite:
                        {
                            State = (SQLHelper as SQLiteHelper).ExecuteNonQuery(SqlOrder);
                        }
                        break;
                    case SqlType.SqlServer:
                        {
                            
                        }
                        break;
                    case SqlType.MySql:
                        {
                        }
                        break;
                }
                return State;
            }
        }
        public DataTable ExecuteQuery(string SqlOrder)
        {
            lock (LockerDB)
            {
                DataTable Table = null;

                switch (this.ThisType)
                {
                    case SqlType.SQLite:
                        {
                            Table = (SQLHelper as SQLiteHelper).ExecuteDataTable(SqlOrder);
                        }
                        break;
                    case SqlType.SqlServer:
                        {
                           
                        }
                        break;
                    case SqlType.MySql:
                        {
                        }
                        break;
                }

                return Table;
            }
        }

        //public T1 ExecuteScalar<T1>(string SqlOrder)
        //{
        //    object Result = null;

        //    switch (this.ThisType)
        //    {
        //        case SqlType.SQLite:
        //            {
        //                Result = (SQLHelper as SQLiteHelper).ExecuteScalar(SqlOrder);
        //            }
        //            break;
        //        case SqlType.SqlServer:
        //            {
        //                Result = (SQLHelper as SqlServerHelper).ExecuteScalar(SqlOrder);
        //            }
        //            break;
        //        case SqlType.MySql:
        //            {
        //            }
        //            break;
        //    }

        //    try
        //    {
        //        return ConvertT.ConverToObject<T1>(Result);
        //    }
        //    catch { return default(T1); }
        //}

        public static object GetPropertyValue(object info, string field)
        {
            if (info == null) return null;
            Type t = info.GetType();
            IEnumerable<System.Reflection.PropertyInfo> property = from pi in t.GetProperties() where pi.Name.ToLower() == field.ToLower() select pi;
            return property.First().GetValue(info, null);
        }


        public object ExecuteScalar(string SqlOrder)
        {
            lock (LockerDB)
            {
                object Result = null;

                switch (this.ThisType)
                {
                    case SqlType.SQLite:
                        {
                            Result = (SQLHelper as SQLiteHelper).ExecuteScalar(SqlOrder);
                        }
                        break;
                    case SqlType.SqlServer:
                        {
                           
                        }
                        break;
                    case SqlType.MySql:
                        {
                        }
                        break;
                }

                return Result;
            }
        }

    }


    public enum SqlType
    {
        Null = 0, SQLite = 1, SqlServer = 2, MySql = 3, Access = 4, Oracle = 5,
    }
}
