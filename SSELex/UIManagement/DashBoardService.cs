using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LiveCharts.Defaults;
using System.Timers;
using PhoenixEngine.TranslateManage;
using static SSELex.MainGui;

namespace SSELex.UIManagement
{

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
        public class SpeedMonitor
        {
            private DashBoardViewModel CurrentModel;

            // Store Number Of Characters Processed Each Second
            private Queue<int> RecentCounts = new Queue<int>();

            // Timer Interval In Milliseconds
            private const int IntervalMs = 1000;

            // Timer
            private System.Timers.Timer Timer;

            // Current Second Count
            public int CurrentSecondCount { get; private set; } = 0;

            // Average Speed Over Recent Seconds
            public double AverageSpeed { get; private set; } = 0.0;

            // Number of seconds to calculate average
            private const int AverageWindowSeconds = 30;

            public SpeedMonitor(DashBoardViewModel Model)
            {
                CurrentModel = Model;

                // Initialize Timer
                Timer = new System.Timers.Timer(IntervalMs);
                Timer.Elapsed += Timer_Elapsed;
                Timer.Start();
            }

            private int ChartMaxPoints = 60;

            /// <summary>
            /// Add New Character Count For The Current Second
            /// </summary>
            /// <param name="Count">Number Of Characters Processed</param>
            public void AddCount(int Count)
            {
                // Add To Current Second Count
                CurrentSecondCount += Count;

                // Update Chart With Current Consumption
                if (CurrentModel.FontUsageSeries.Count > 0)
                {
                    // Add Current Second Value To Chart
                    CurrentModel.FontUsageSeries[0].Values.Add(new ObservableValue(CurrentSecondCount));

                    // Remove Oldest Value To Maintain Chart Length
                    while (CurrentModel.FontUsageSeries[0].Values.Count > ChartMaxPoints)
                        CurrentModel.FontUsageSeries[0].Values.RemoveAt(0);
                }
            }

            /// <summary>
            /// Timer Elapsed Event, Updates Average Speed And Resets Current Second Count
            /// </summary>
            private void Timer_Elapsed(object Sender, ElapsedEventArgs E)
            {
                lock (RecentCounts)
                {
                    // Enqueue Current Second Count
                    RecentCounts.Enqueue(CurrentSecondCount);

                    // Keep Only Last AverageWindowSeconds For Average Calculation
                    while (RecentCounts.Count > AverageWindowSeconds)
                        RecentCounts.Dequeue();

                    // Calculate Average Speed
                    AverageSpeed = RecentCounts.Count > 0 ? (double)SumQueue(RecentCounts) / RecentCounts.Count : 0.0;

                    // Reset Current Second Count
                    CurrentSecondCount = 0;
                }
            }

            /// <summary>
            /// Calculate Sum Of Queue Values
            /// </summary>
            private int SumQueue(Queue<int> Queue)
            {
                int Sum = 0;
                foreach (var Val in Queue)
                    Sum += Val;
                return Sum;
            }
        }

        /// <summary>
        /// Estimate Token Count For A Text String (Approximation For Cohere Or Other AI)
        /// </summary>
        /// <param name="text">Input Text To Be Sent To AI</param>
        /// <returns>Estimated Token Count</returns>
        public static int EstimateTokenCount(string Text)
        {
            // Return 0 If Input Text Is Null Or Empty
            if (string.IsNullOrEmpty(Text))
                return 0;

            // Approximate: 1 Token ≈ 4 Characters (Common Approximation For English)
            int EstimatedTokens = (int)Math.Ceiling(Text.Length / 4.0);

            // Return Estimated Token Count
            return EstimatedTokens;
        }

        public static void TokenStatistics(PlatformType Platform,string SendStr, string Json)
        {
            var TryGetToken = ExtractTotalTokens(Json);

            if (Platform == PlatformType.Cohere && TryGetToken == 0)
            {
                TryGetToken = EstimateTokenCount(SendStr);
            }

            if (TryGetToken > 0)
            {
                switch (Platform)
                {
                    case PlatformType.ChatGpt:
                        {
                            DeFine.GlobalLocalSetting.ChatGPTTokenUsage += TryGetToken;
                        }
                        break;
                    case PlatformType.Gemini:
                        {
                            DeFine.GlobalLocalSetting.GeminiTokenUsage += TryGetToken;
                        }
                        break;
                    case PlatformType.Cohere:
                        {
                            DeFine.GlobalLocalSetting.CohereTokenUsage += TryGetToken;
                        }
                        break;
                    case PlatformType.DeepSeek:
                        {
                            DeFine.GlobalLocalSetting.DeepSeekTokenUsage += TryGetToken;
                        }
                        break;
                    case PlatformType.Baichuan:
                        {
                            DeFine.GlobalLocalSetting.BaichuanTokenUsage += TryGetToken;
                        }
                        break;
                    case PlatformType.LMLocalAI:
                        {
                            DeFine.GlobalLocalSetting.LocalAITokenUsage += TryGetToken;
                        }
                        break;
                }

                UPLoadView(SendStr);
            }
        }

        /// <summary>
        /// Extract Total Token Count From Any AI JSON Text (Fuzzy Match total_tokens / totalTokenCount etc.)
        /// </summary>
        /// <param name="json">Original JSON String</param>
        /// <returns>Token Count, Return 0 If Not Found</returns>
        public static int ExtractTotalTokens(string Json)
        {
            // Return 0 If Input String Is Null Or Empty
            if (string.IsNullOrEmpty(Json))
                return 0;

            // Fuzzy Match total_tokens Or totalTokenCount, Ignore Case
            var Match = Regex.Match(Json, @"(?i)""total[_]?tokens?""\s*:\s*(\d+)|""totalTokenCount""\s*:\s*(\d+)");
            if (Match.Success)
            {
                // Get Matched Value From Correct Group
                string Value = Match.Groups[1].Success ? Match.Groups[1].Value : Match.Groups[2].Value;

                // Parse String To Int And Return If Success
                if (int.TryParse(Value, out int Tokens))
                    return Tokens;
            }

            // Return 0 If No Match Found
            return 0;
        }

        public static void UPLoadView(string SendStr)
        {
            if (DeFine.CanUpdateChart)
            {
                DeFine.WorkingWin.Dispatcher.BeginInvoke(new Action(() => {

                    DeFine.WorkingWin.UPDateChart(SendStr);

                }));
            }
        }
    }
}
