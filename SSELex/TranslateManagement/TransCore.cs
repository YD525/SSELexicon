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
        public static void Shuffle<T>(IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
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
                    EngineSelects.Add(new EngineSelect(new BaiDuApi(), 1));
                }
            }

            //Google support
            if (DeFine.GlobalLocalSetting.GoogleYunApiUsing)
            {
                if (DeFine.GlobalLocalSetting.GoogleApiKey.Trim().Length == 0)
                {
                    if (ConvertHelper.ObjToStr(new GoogleHelper().FreeTransStr("Test", DeFine.SourceLanguage, DeFine.TargetLanguage)).Length > 0)
                    {
                        EngineSelects.Add(new EngineSelect(new GoogleHelper(), 1));
                    }
                }
                else
                {
                    EngineSelects.Add(new EngineSelect(new GoogleTransApi(), 2));
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

            Shuffle<EngineSelect>(EngineSelects);
        }

        public string TransAny(Languages Source, Languages Target,string SourceStr)
        {
            if (SourceStr == "")
            {
                return "";
            }

            string GetCacheStr = TranslateDBCache.FindCache(SourceStr, (int)Source, (int)Target);

            if (GetCacheStr.Trim().Length > 0)
            {
                Translator.SendTranslateMsg("Cache From Database", SourceStr, GetCacheStr);
                return GetCacheStr;
            }

            NextSet:

            int LimitCount = 0;
            for (int i = 0; i < TransCore.EngineSelects.Count; i++)
            {
                if (TransCore.EngineSelects[i].CurrentCallCount < TransCore.EngineSelects[i].MaxUseCount)
                {
                    string GetTrans = TransCore.EngineSelects[i].Call(Source, Target, SourceStr);
                    if (GetTrans.Trim().Length > 0)
                    {
                        return GetTrans;
                    }
                }
                else
                {
                    LimitCount++;
                }
            }

            if (LimitCount == TransCore.EngineSelects.Count)
            {
                Thread.Sleep(5);
                ReloadEngine();
                goto NextSet;
            }

            return string.Empty;
        }

        public class EngineSelect
        {
            public static AITranslationMemory AIMemory = new AITranslationMemory();

            public object Engine = new object();
            public int CurrentCallCount = 0;
            public int MaxUseCount = 0;

            public EngineSelect(object Engine, int MaxUseCount)
            {
                this.Engine = Engine;
                this.MaxUseCount = MaxUseCount;
            }
            public void ProcessEmptyEndLine(ref string TransText)
            {
                if (TransText.EndsWith("\r\n"))
                {
                    TransText = TransText.Substring(0, TransText.Length - "\r\n".Length);
                }
                else
                if (TransText.EndsWith("\n"))
                {
                    TransText = TransText.Substring(0, TransText.Length - "\n".Length);
                }
            }
            public string Call(Languages Source, Languages Target, string SourceStr)
            {
                string GetSource = SourceStr.Trim();
                string TransText = string.Empty;

                if (GetSource.Length > 0)
                {
                    if (Source.Equals(Target))
                    {
                        return TransText;
                    }

                    if (this.Engine is BaiDuApi)
                    {
                        if (DeFine.GlobalLocalSetting.BaiDuYunApiUsing)
                        {
                            if (this.CurrentCallCount < this.MaxUseCount)
                            {
                                var GetData = (this.Engine as BaiDuApi).TransStr(GetSource, Source, Target);
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

                                            this.CurrentCallCount++;
                                            Translator.SendTranslateMsg("Cloud Engine(BaiDu)", GetSource, TransText);
                                        }
                                        else
                                        {
                                            this.CurrentCallCount = this.MaxUseCount;
                                        }
                                    }
                                    else
                                    {
                                        this.CurrentCallCount = this.MaxUseCount;
                                    }
                                }
                                else
                                {
                                    this.CurrentCallCount = this.MaxUseCount;
                                }
                            }
                        }
                        else
                        {
                            this.CurrentCallCount = this.MaxUseCount;
                        }
                    }
                    else
                    if (this.Engine is GoogleHelper)
                    {
                        if (DeFine.GlobalLocalSetting.GoogleYunApiUsing)
                        {
                            var GetData = ConvertHelper.ObjToStr((this.Engine as GoogleHelper).FreeTransStr(GetSource, Source, Target));

                            TransText = GetData;
                            this.CurrentCallCount++;
                            Translator.SendTranslateMsg("Cloud Engine(Google)", GetSource, TransText);
                        }
                        else
                        {
                            this.CurrentCallCount = this.MaxUseCount;
                        }
                    }
                    else
                    if (this.Engine is GoogleTransApi)
                    {
                        if (DeFine.GlobalLocalSetting.GoogleYunApiUsing)
                        {
                            var GetData = ConvertHelper.ObjToStr((this.Engine as GoogleTransApi).Translate(GetSource, Source, Target));

                            TransText = GetData;
                            this.CurrentCallCount++;
                            Translator.SendTranslateMsg("Cloud Engine(GoogleApi)", GetSource, TransText);
                        }
                        else
                        {
                            this.CurrentCallCount = this.MaxUseCount;
                        }
                    }
                    else
                    if (this.Engine is ChatGptApi)
                    {
                        if (DeFine.GlobalLocalSetting.ChatGptApiUsing)
                        {
                            var GetData = (this.Engine as ChatGptApi).QuickTrans(GetSource, Source, Target).Trim();

                            if (GetData.Trim().Length > 0)
                            {
                                AIMemory.AddTranslation(Source, GetSource, GetData);
                            }

                            TransText = GetData;
                            this.CurrentCallCount++;
                            Translator.SendTranslateMsg("Cloud Engine(ChatGptApi)", GetSource, TransText);
                        }
                        else
                        {
                            this.CurrentCallCount = this.MaxUseCount;
                        }
                    }
                    else
                    if (this.Engine is DeepSeekApi)
                    {
                        if (DeFine.GlobalLocalSetting.DeepSeekApiUsing)
                        {
                            var GetData = (this.Engine as DeepSeekApi).QuickTrans(GetSource, Source, Target).Trim();

                            if (GetData.Trim().Length > 0)
                            {
                                AIMemory.AddTranslation(Source, GetSource, GetData);
                            }

                            TransText = GetData;
                            this.CurrentCallCount++;
                            Translator.SendTranslateMsg("Cloud Engine(DeepSeek)", GetSource, TransText);
                        }
                        else
                        {
                            this.CurrentCallCount = this.MaxUseCount;
                        }
                    }
                    else
                    if (this.Engine is DeepLApi)
                    {
                        if (DeFine.GlobalLocalSetting.DeepLApiUsing)
                        {
                            var GetData = (this.Engine as DeepLApi).QuickTrans(GetSource, Source, Target).Trim();

                            if (GetData.Trim().Length > 0)
                            {
                                AIMemory.AddTranslation(Source, GetSource, GetData);
                            }

                            TransText = GetData;
                            this.CurrentCallCount++;
                            Translator.SendTranslateMsg("Cloud Engine(DeepL)", GetSource, TransText);
                        }
                    }

                    TransText = TransText.Trim();

                    ProcessEmptyEndLine(ref TransText);

                    if (TransText.Length > 0)
                    {
                        TranslateDBCache.AddCache(SourceStr, (int)Source, (int)Target, TransText);
                    }

                    return TransText;
                }

                return string.Empty;
            }
        }
    }
}
