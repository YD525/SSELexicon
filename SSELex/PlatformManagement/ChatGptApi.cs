using System.Net;
using System.Text.Json;
using SSELex.ConvertManager;
using SSELex.RequestManagement;
using SSELex.TranslateCore;
using SSELex.TranslateManage;
using SSELex.UIManagement;
using static SSELex.TranslateManage.TransCore;

namespace SSELex.PlatformManagement
{
    public class ChatGptItem
    {
        public string model { get; set; }
        public bool store { get; set; }
        public List<ChatGptMessage> messages { get; set; }
    }

    public class ChatGptMessage
    {
        public string role { get; set; }
        public string content { get; set; }

        public ChatGptMessage(string role, string content)
        {
            this.role = role;
            this.content = content;
        }
    }

    public class ChatGptApi
    {
        public ChatGptRootobject? CallAI(string Msg)
        {
            int GetCount = Msg.Length; 
            ChatGptItem NChatGptItem = new ChatGptItem();
            NChatGptItem.model = DeFine.GlobalLocalSetting.ChatGptModel;
            NChatGptItem.store = true;
            NChatGptItem.messages = new List<ChatGptMessage>();
            NChatGptItem.messages.Add(new ChatGptMessage("user", Msg));
            var GetResult = CallAI(NChatGptItem);
            return GetResult;
        }

        public ChatGptRootobject? CallAI(ChatGptItem Item)
        {
            string GetJson = JsonSerializer.Serialize(Item);
            WebHeaderCollection Headers = new WebHeaderCollection();
            Headers.Add("Authorization", string.Format("Bearer {0}", DeFine.GlobalLocalSetting.ChatGptKey));
            HttpItem Http = new HttpItem()
            {
                URL = "https://api.openai.com/v1/chat/completions",
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
                DeFine.CurrentDashBoardView.SetLogB("ChatGpt:" + GetResult);
                return JsonSerializer.Deserialize<ChatGptRootobject>(GetResult);
            }
            catch 
            {
                return null; 
            }
        }
        //"Important: When translating, strictly keep any text inside angle brackets (< >) or square brackets ([ ]) unchanged. Do not modify, translate, or remove them.\n\n"
        public string QuickTrans(string TransSource, Languages FromLang, Languages ToLang,bool UseAIMemory,int AIMemoryCountLimit, string Param)
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
                if (GetResult.choices != null)
                {
                    string GetStr = "";
                    if (GetResult.choices.Length > 0)
                    {
                        GetStr = GetResult.choices[0].message.content.Trim();
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
            return string.Empty;
        }



        public class ChatGptRootobject
        {
            public string id { get; set; }
            public string _object { get; set; }
            public int created { get; set; }
            public string model { get; set; }
            public ChatChoice[] choices { get; set; }
            public ChatUsage usage { get; set; }
            public string service_tier { get; set; }
            public string system_fingerprint { get; set; }
        }

        public class ChatUsage
        {
            public int prompt_tokens { get; set; }
            public int completion_tokens { get; set; }
            public int total_tokens { get; set; }
            public ChatPrompt_Tokens_Details prompt_tokens_details { get; set; }
            public ChatCompletion_Tokens_Details completion_tokens_details { get; set; }
        }

        public class ChatPrompt_Tokens_Details
        {
            public int cached_tokens { get; set; }
            public int audio_tokens { get; set; }
        }

        public class ChatCompletion_Tokens_Details
        {
            public int reasoning_tokens { get; set; }
            public int audio_tokens { get; set; }
            public int accepted_prediction_tokens { get; set; }
            public int rejected_prediction_tokens { get; set; }
        }

        public class ChatChoice
        {
            public int index { get; set; }
            public ChatMessage message { get; set; }
            public object logprobs { get; set; }
            public string finish_reason { get; set; }
        }

        public class ChatMessage
        {
            public string role { get; set; }
            public string content { get; set; }
            public object refusal { get; set; }
        }
    }
}
