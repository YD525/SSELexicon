using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SSELex.ConvertManager;
using SSELex.RequestManagement;
using SSELex.TranslateCore;
using SSELex.TranslateManage;
using SSELex.UIManagement;
using static SSELex.TranslateManage.TransCore;

namespace SSELex.PlatformManagement
{

    public class GeminiItem
    {
        public List<GeminiContent> contents { get; set; } = new List<GeminiContent>();
    }

    public class GeminiContent
    {
        public List<GeminiPart> parts { get; set; } = new List<GeminiPart>();
    }

    public class GeminiPart
    {
        public string text { get; set; }
    }


    public class GeminiRootobject
    {
        public GeminiCandidate[] candidates { get; set; }
        public GeminiUsagemetadata usageMetadata { get; set; }
        public string modelVersion { get; set; }
    }

    public class GeminiUsagemetadata
    {
        public int promptTokenCount { get; set; }
        public int candidatesTokenCount { get; set; }
        public int totalTokenCount { get; set; }
        public GeminiPrompttokensdetail[] promptTokensDetails { get; set; }
        public GeminiCandidatestokensdetail[] candidatesTokensDetails { get; set; }
    }

    public class GeminiPrompttokensdetail
    {
        public string modality { get; set; }
        public int tokenCount { get; set; }
    }

    public class GeminiCandidatestokensdetail
    {
        public string modality { get; set; }
        public int tokenCount { get; set; }
    }

    public class GeminiCandidate
    {
        public GeminiContent content { get; set; }
        public string finishReason { get; set; }
        public float avgLogprobs { get; set; }
    }

    public class GeminiRContent
    {
        public GeminiRPart[] parts { get; set; }
        public string role { get; set; }
    }

    public class GeminiRPart
    {
        public string text { get; set; }
    }

    public class GeminiApi
    {
        //"Important: When translating, strictly keep any text inside angle brackets (< >) or square brackets ([ ]) unchanged. Do not modify, translate, or remove them.\n\n"
        public string QuickTrans(string TransSource, Languages FromLang, Languages ToLang, bool UseAIMemory, int AIMemoryCountLimit, string Param)
        {
            List<string> Related = new List<string>();
            if (DeFine.GlobalLocalSetting.UsingContext && UseAIMemory)
            {
                Related = EngineSelect.AIMemory.FindRelevantTranslations(FromLang, TransSource, AIMemoryCountLimit);
            }

            var GetTransSource = $"Translate the following text from {LanguageHelper.ToLanguageCode(FromLang)} to {LanguageHelper.ToLanguageCode(ToLang)}:\n\n";

            if (Param.Trim().Length > 0)
            {
                GetTransSource += Param;
            }

            if (ConvertHelper.ObjToStr(DeFine.GlobalLocalSetting.UserCustomAIPrompt).Trim().Length > 0)
            {
                GetTransSource += DeFine.GlobalLocalSetting.UserCustomAIPrompt + "\n\n";
            }

            if (Related.Count > 0)
            {
                GetTransSource += "Previous related translations:\n";
                foreach (var related in Related)
                {
                    GetTransSource += $"- {related}\n";
                }
                GetTransSource += "\n";
            }

            GetTransSource += $"\"\"\"\n{TransSource}\n\"\"\"\n\n";
            GetTransSource += "Respond in JSON format: {\"translation\": \"<translated_text>\"}";

            if (GetTransSource.EndsWith("\n"))
            {
                GetTransSource = GetTransSource.Substring(0, GetTransSource.Length - 1);
            }

            var GetResult = CallAI(GetTransSource);
            if (GetResult != null)
            {
                try
                {
                    if (GetResult.candidates != null)
                    {
                        string GetStr = "";
                        if (GetResult.candidates.Length > 0)
                        {
                            if (GetResult.candidates[0].content.parts.Count > 0)
                            {
                                GetStr = GetResult.candidates[0].content.parts[0].text.Trim();
                            }
                        }
                        if (GetStr.Trim().Length > 0)
                        {
                            try
                            {
                                GetStr = JsonGeter.GetValue(GetStr);
                            }
                            catch
                            {
                                return string.Empty;
                            }

                            if (DeFine.CurrentDashBoardView != null)
                            {
                                DeFine.CurrentDashBoardView.SetLogB(GetTransSource + "\r\n\r\n AI:\r\n" + GetStr);
                            }

                            if (GetStr.Trim().Equals("<translated_text>"))
                            {
                                return string.Empty;
                            }

                            return GetStr;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                }
                catch { return string.Empty; }
            }
            return string.Empty;
        }

        public GeminiRootobject? CallAI(string Msg)
        {
            int GetCount = Msg.Length;
            GeminiItem NGeminiItem = new GeminiItem();
            NGeminiItem.contents.Add(new GeminiContent());
            NGeminiItem.contents[0].parts.Add(new GeminiPart());
            NGeminiItem.contents[0].parts[0].text = Msg;
            var GetResult = CallAI(NGeminiItem);
            return GetResult;
        }

        public GeminiRootobject? CallAI(GeminiItem Item)
        {
            string GetJson = JsonSerializer.Serialize(Item);
            WebHeaderCollection Headers = new WebHeaderCollection();
            HttpItem Http = new HttpItem()
            {
                URL = $"https://generativelanguage.googleapis.com/v1beta/models/{DeFine.GlobalLocalSetting.GeminiModel}:generateContent?key={DeFine.GlobalLocalSetting.GeminiKey}",
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/132.0.0.0 Safari/537.36",
                Method = "Post",
                Header = Headers,
                Accept = "*/*",
                Postdata = GetJson,
                Cookie = "",
                ContentType = "application/json",
                Timeout = DeFine.GlobalRequestTimeOut,
                ProxyIp = ProxyCenter.GlobalProxyIP
            };
            try
            {
                Http.Header.Add("Accept-Encoding", " gzip");
            }
            catch { }

            string GetResult = new HttpHelper().GetHtml(Http).Html;
            try
            {
                DeFine.CurrentDashBoardView.SetLogB("Gemini:" + GetResult);
                return JsonSerializer.Deserialize<GeminiRootobject>(GetResult);
            }
            catch
            {
                return null;
            }
        }
    }
}
