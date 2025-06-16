using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SSELex.UIManagement
{
    public enum PlatformType
    {
        Null = 0, ChatGpt = 1, DeepSeek = 2, Gemini = 3, DeepL = 5, GoogleApi = 7, Baichuan = 8, Cohere = 9, LMLocalAI = 10
    }

    public class QueryPlatformItem
    {
        public PlatformType Type;
        public int FontUsageCount = 0;

        public QueryPlatformItem(PlatformType Type, int FontUsageCount)
        {
            this.Type = Type;
            this.FontUsageCount = FontUsageCount;
        }
    }
    public class DashBoardService
    {
        public static int Interval = 1000;

        public static bool LockerStartListenService = false;
        public static void StartListenService(bool Check, Action OneFunc)
        {
            if (!LockerStartListenService)
            {
                LockerStartListenService = true;

                new Thread(() =>
                {

                    while (LockerStartListenService)
                    {
                        Thread.Sleep(DashBoardService.Interval);

                        try
                        {
                            OneFunc.Invoke();
                        }
                        catch { }
                    }
                }).Start();
            }
            else
            {
                LockerStartListenService = false;
            }
        }

        public static Dictionary<PlatformType, int> FontUsageCounts = new Dictionary<PlatformType, int>();

        private static readonly object _Lock = new object();
        public static void SetUsage(PlatformType Type, int Count)
        {
            try
            {
                lock (_Lock)
                {
                    if (Type == PlatformType.Null)
                    {
                        return;
                    }

                    if (FontUsageCounts.ContainsKey(Type))
                    {
                        FontUsageCounts[Type] += Count;
                    }
                    else
                    {
                        FontUsageCounts.Add(Type, Count);
                    }
                }
            }
            catch { }
        }
        public static List<QueryPlatformItem> QueryData()
        {
            try
            {
                List<QueryPlatformItem> QueryPlatformItems = new List<QueryPlatformItem>();
                for (int i = 0; i < FontUsageCounts.Count; i++)
                {
                    var GetItem = FontUsageCounts.ElementAt(i);
                    if (GetItem.Value > 0)
                    {
                        QueryPlatformItems.Add(new QueryPlatformItem(GetItem.Key, GetItem.Value));
                    }
                }
                return QueryPlatformItems;
            }
            catch
            {
                return new List<QueryPlatformItem>();
            }
        }

        public static void Clear()
        {
            FontUsageCounts.Clear();
            DeltasHistory.Clear();
        }

        public static int GetTotalUsageCount()
        {
            try
            {
                int Total = 0;
                for (int i = 0; i < FontUsageCounts.Count; i++)
                {
                    var GetKey = FontUsageCounts.ElementAt(i).Key;

                    if (FontUsageCounts[GetKey] > 0)
                    {
                        Total += FontUsageCounts[GetKey];
                    }
                }
                return Total;
            }
            catch
            {
                return 0;
            }
        }

        private static int LastTotalCount = 0;
        private static Queue<int> DeltasHistory = new Queue<int>();
        private const int WindowSize = 5;
        public static int GetCurrentSecondUsage()
        {
            try
            {
                int CurrentTotal = GetTotalUsageCount();
                int CurrentDelta = CurrentTotal - LastTotalCount;

                if (CurrentDelta < 0) CurrentDelta = 0;

                DeltasHistory.Enqueue(CurrentDelta);
                if (DeltasHistory.Count > WindowSize)
                {
                    DeltasHistory.Dequeue();
                }

                LastTotalCount = CurrentTotal;

                if (DeltasHistory.Count == 0) return 0;
                return (int)DeltasHistory.Average();
            }
            catch
            {
                return 0;
            }
        }

        public static void Init()
        {
            LastTotalCount = 0;
            FontUsageCounts.Clear();
        }
    }
}
