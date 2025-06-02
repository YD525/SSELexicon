using SSELex.ConvertManager;
using SSELex.PlatformManagement;
using SSELex.TranslateCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSELex.TranslateManage
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/
    public class TransCore
    {
        private static readonly object _SortLock = new object();

        public static void SortByCallCountDescending()
        {
            lock (_SortLock)
            {
                EngineSelects.Sort((a, b) => b.CallCountDown.CompareTo(a.CallCountDown));
            }
        }

        public static List<EngineSelect> EngineSelects = new List<EngineSelect>();

        public static void Init()
        {
            ReloadEngine();
        }

        public static void ReloadEngine()
        {
            EngineSelects.Clear();

            //Baidu support
            if (DeFine.GlobalLocalSetting.BaiDuYunApiUsing)
            {
                if (DeFine.GlobalLocalSetting.BaiDuAppID.Trim().Length > 0)
                {
                    EngineSelects.Add(new EngineSelect(new BaiDuApi(), 1, 2));
                }
            }

            //Google support
            if (DeFine.GlobalLocalSetting.GoogleYunApiUsing)
            {
                if (DeFine.GlobalLocalSetting.GoogleApiKey.Trim().Length > 0)
                {
                    EngineSelects.Add(new EngineSelect(new GoogleTransApi(), 1));
                }
            }

            //ChatGPT support
            if (DeFine.GlobalLocalSetting.ChatGptApiUsing)
            {
                if (DeFine.GlobalLocalSetting.ChatGptKey.Trim().Length > 0)
                {
                    EngineSelects.Add(new EngineSelect(new ChatGptApi(), 6));
                }
            }

            //Gemini support
            if (DeFine.GlobalLocalSetting.GeminiApiUsing)
            {
                if (DeFine.GlobalLocalSetting.GeminiKey.Trim().Length > 0)
                {
                    EngineSelects.Add(new EngineSelect(new GeminiApi(), 6));
                }
            }

            //DeepSeek support
            if (DeFine.GlobalLocalSetting.DeepSeekApiUsing)
            {
                if (DeFine.GlobalLocalSetting.DeepSeekKey.Trim().Length > 0)
                {
                    EngineSelects.Add(new EngineSelect(new DeepSeekApi(), 6));
                }
            }

            //DeepL support
            if (DeFine.GlobalLocalSetting.DeepLApiUsing)
            {
                if (DeFine.GlobalLocalSetting.DeepLKey.Trim().Length > 0)
                {
                    EngineSelects.Add(new EngineSelect(new DeepLApi(), 6));
                }
            }
        }

        public static object SwitchLocker = new object();


        public static string AIParam = "Important: When translating, strictly keep any text inside angle brackets (< >) or square brackets ([ ]) unchanged. Do not modify, translate, or remove them.\n\n";
        /// <summary>
        /// Multithreaded translation entry
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="Target"></param>
        /// <param name="SourceStr"></param>
        /// <returns></returns>
        public string TransAny(string Key,Languages Source, Languages Target,string SourceStr,bool IsBook,ref bool CanAddCache,ref bool CanSleep)
        {
            if (SourceStr == "")
            {
                return "";
            }
            if (Source.Equals(Target))
            {
                return SourceStr;
            }

            string GetCacheStr = CloudDBCache.FindCache(Key);

            if (GetCacheStr.Trim().Length > 0)
            {
                Translator.SendTranslateMsg("Cache From Database", SourceStr, GetCacheStr);
                CanSleep = false;
                return GetCacheStr;
            }

            EngineSelect? CurrentEngine = null;

            while (CurrentEngine == null)
            {
                lock (SwitchLocker)
                {
                    for (int i = 0; i < TransCore.EngineSelects.Count; i++)
                    {
                        if (TransCore.EngineSelects[i].CallCountDown > 0)
                        {
                            TransCore.EngineSelects[i].CallCountDown--;

                            CurrentEngine = TransCore.EngineSelects[i];

                            SortByCallCountDescending();
                        }
                    }
                }

                if (CurrentEngine != null)
                {
                    string GetTrans = "";
                    if (!IsBook)
                    {
                        GetTrans = CurrentEngine.Call(Source, Target, SourceStr, true, DeFine.GlobalLocalSetting.ContextLimit, string.Empty,ref CanAddCache);
                    }
                    else
                    {
                        GetTrans = CurrentEngine.Call(Source, Target, SourceStr, false, 1, AIParam, ref CanAddCache);
                    }

                    if (CanSleep)
                    {
                        CurrentEngine.BeginSleep();
                    }
                    
                    return GetTrans;
                }

                ReloadEngine();
            }
            return string.Empty;
        }

        public class EngineSelect
        {
            public static AITranslationMemory AIMemory = new AITranslationMemory();

            public object Engine = new object();
            public int CallCountDown = 0;
            public int MaxCallCount = 0;

            public int SleepBySec = 0;

            public EngineSelect(object Engine, int MaxCallCount)
            {
                this.Engine = Engine;
                this.MaxCallCount = MaxCallCount;
                this.CallCountDown = this.MaxCallCount;

                this.SleepBySec = 1;
            }

            public EngineSelect(object Engine, int MaxCallCount,int SleepBySec)
            {
                this.Engine = Engine;
                this.MaxCallCount = MaxCallCount;
                this.CallCountDown = this.MaxCallCount;

                this.SleepBySec = SleepBySec;
            }

            public void BeginSleep()
            {
                for (int i = 0; i < SleepBySec; i++)
                {
                    Thread.Sleep(1000);
                }
            }

            public string Call(Languages Source, Languages Target, string SourceStr,bool UseAIMemory,int AIMemoryCountLimit,string Param,ref bool CanAddCache)
            {
                string GetSource = SourceStr.Trim();
                string TransText = string.Empty;

                if (GetSource.Length > 0)
                {
                    if (this.Engine is BaiDuApi)
                    {
                        if (DeFine.GlobalLocalSetting.BaiDuYunApiUsing)
                        {
                            bool Sucess = false;
                            var GetData = ((BaiDuApi)this.Engine).TransStr(GetSource, Source, Target);
                            if (GetData != null)
                            {
                                if (GetData.trans_result != null)
                                {
                                    if (GetData.trans_result.Length > 0)
                                    {
                                        foreach (var GetLine in GetData.trans_result)
                                        {
                                            TransText += GetLine.dst + "\r\n";
                                        }
                                        Translator.SendTranslateMsg("Cloud Engine(BaiDu)", GetSource, TransText);
                                        CanAddCache = false;
                                        Sucess = true;
                                    }
                                }
                            }

                            if (!Sucess)
                            {
                                this.CallCountDown = 0;
                            }
                        }
                        else
                        {
                            this.CallCountDown = 0;
                        }
                    }
                    else
                    if (this.Engine is GoogleTransApi)
                    {
                        if (DeFine.GlobalLocalSetting.GoogleYunApiUsing)
                        {
                            var GetData = ConvertHelper.ObjToStr(((GoogleTransApi)this.Engine).Translate(GetSource, Source, Target));
                            TransText = GetData;
                            Translator.SendTranslateMsg("Cloud Engine(GoogleApi)", GetSource, TransText);

                            if (GetData.Trim().Length == 0)
                            {
                                this.CallCountDown = 0;
                            }
                        }
                        else
                        {
                            this.CallCountDown = 0;
                        }
                    }
                    else
                    if (this.Engine is ChatGptApi)
                    {
                        if (DeFine.GlobalLocalSetting.ChatGptApiUsing)
                        {
                            var GetData = ((ChatGptApi)this.Engine).QuickTrans(GetSource, Source, Target,UseAIMemory,AIMemoryCountLimit, Param).Trim();

                            if (GetData.Trim().Length > 0 && UseAIMemory)
                            {
                                AIMemory.AddTranslation(Source, GetSource, GetData);
                            }
                            TransText = GetData;
                            Translator.SendTranslateMsg("Cloud Engine(ChatGptApi)", GetSource, TransText);

                            if (GetData.Trim().Length == 0)
                            {
                                this.CallCountDown = 0;
                            }
                        }
                        else
                        {
                            this.CallCountDown = 0;
                        }
                    }
                    else
                    if (this.Engine is GeminiApi)
                    {
                        if (DeFine.GlobalLocalSetting.GeminiApiUsing)
                        {
                            var GetData = ((GeminiApi)this.Engine).QuickTrans(GetSource, Source, Target, UseAIMemory, AIMemoryCountLimit, Param).Trim();

                            if (GetData.Trim().Length > 0 && UseAIMemory)
                            {
                                AIMemory.AddTranslation(Source, GetSource, GetData);
                            }
                            TransText = GetData;
                            Translator.SendTranslateMsg("Cloud Engine(GeminiApi)", GetSource, TransText);

                            if (GetData.Trim().Length == 0)
                            {
                                this.CallCountDown = 0;
                            }
                        }
                        else
                        {
                            this.CallCountDown = 0;
                        }
                    }
                    else
                    if (this.Engine is DeepSeekApi)
                    {
                        if (DeFine.GlobalLocalSetting.DeepSeekApiUsing)
                        {
                            var GetData = ((DeepSeekApi)this.Engine).QuickTrans(GetSource, Source, Target, UseAIMemory, AIMemoryCountLimit, Param).Trim();

                            if (GetData.Trim().Length > 0 && UseAIMemory)
                            {
                                AIMemory.AddTranslation(Source, GetSource, GetData);
                            }
                            TransText = GetData;
                            Translator.SendTranslateMsg("Cloud Engine(DeepSeek)", GetSource, TransText);

                            if (GetData.Trim().Length == 0)
                            {
                                this.CallCountDown = 0;
                            }
                        }
                        else
                        {
                            this.CallCountDown = 0;
                        }
                    }
                    else
                    if (this.Engine is DeepLApi)
                    {
                        if (DeFine.GlobalLocalSetting.DeepLApiUsing)
                        {
                            var GetData = ((DeepLApi)this.Engine).QuickTrans(GetSource, Source, Target).Trim();

                            if (GetData.Trim().Length > 0 && UseAIMemory)
                            {
                                AIMemory.AddTranslation(Source, GetSource, GetData);
                            }
                            TransText = GetData;
                            Translator.SendTranslateMsg("Cloud Engine(DeepL)", GetSource, TransText);

                            if (GetData.Trim().Length == 0)
                            {
                                this.CallCountDown = 0;
                            }
                        }
                        else
                        {
                            this.CallCountDown = 0;
                        }
                    }

                    TransText = TransText.Trim();

                    return TransText;
                }

                return string.Empty;
            }
        }
    }
}
