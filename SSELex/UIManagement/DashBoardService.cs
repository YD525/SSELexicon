using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSELex.UIManagement
{
    public enum PlatformType
    { 
        Null = 0, ChatGpt = 1,DeepSeek = 2,Gemini = 3,DeepL = 5,BaiduApi = 6,GoogleApi = 7
    }

    public class QueryPlatformItem
    {
        public PlatformType Type;
        public int FontUsageCount = 0;

        public QueryPlatformItem(PlatformType Type,int FontUsageCount)
        {
            this.Type = Type;
            this.FontUsageCount = FontUsageCount;
        }
    }
    public class DashBoardService
    {
        public static int Interval = 1000;

        public static bool LockerStartListenService = false;
        public static void StartListenService(bool Check,Action OneFunc)
        {
            if (!LockerStartListenService)
            {
                LockerStartListenService = true;

                new Thread(() =>
                {

                    while (LockerStartListenService)
                    {
                        Thread.Sleep(DashBoardService.Interval);

                        OneFunc.Invoke();
                    }
                }).Start();
            }
            else
            {
                LockerStartListenService = false;
            }
        }

        public static Dictionary<PlatformType, int> FontUsageCounts = new Dictionary<PlatformType, int>();
        public static void SetUsage(PlatformType Type,int Count)
        {
            if (FontUsageCounts.ContainsKey(Type))
            {
                FontUsageCounts[Type] += Count;
            }
            else
            {
                FontUsageCounts.Add(Type, Count);
            }
        }
        public static List<QueryPlatformItem> QueryData()
        {
            List<QueryPlatformItem> QueryPlatformItems = new List<QueryPlatformItem>();
            for (int i = 0; i < FontUsageCounts.Count; i++)
            {
                var GetItem = FontUsageCounts.ElementAt(i);
                if (GetItem.Value > 0)
                {
                    QueryPlatformItems.Add(new QueryPlatformItem(GetItem.Key,GetItem.Value));
                }
            }
            return QueryPlatformItems;
        }
        public static int GetAndClearUsageCount()
        {
            int Total = 0;
            for (int i = 0; i < FontUsageCounts.Count; i++)
            {
                var GetKey = FontUsageCounts.ElementAt(i).Key;

                if (FontUsageCounts[GetKey] > 0)
                {
                    Total += FontUsageCounts[GetKey];
                    FontUsageCounts[GetKey] = 0;
                }
            }
            return Total;
        }
        public static int GetAndClearUsageCount(PlatformType Type)
        {
            if (FontUsageCounts.ContainsKey(Type))
            {
                int GetLength = FontUsageCounts[Type];
                FontUsageCounts[Type] = 0;
                return GetLength;
            }
            else
            {
                return 0;
            }
        }
    }
}
