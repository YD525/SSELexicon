using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using SSELex.ConvertManager;
using SSELex.TranslateCore;
using static SSELex.TranslateCore.LanguageHelper;

namespace SSELex.TranslateManage
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
        public ChatGptRootobject CallAI(string Msg)
        {
            ChatGptItem NChatGptItem = new ChatGptItem();
            NChatGptItem.model = "gpt-4o-mini";
            NChatGptItem.store = true;
            NChatGptItem.messages = new List<ChatGptMessage>();
            NChatGptItem.messages.Add(new ChatGptMessage("user", Msg));
            return CallAI(NChatGptItem);
        }

        public ChatGptRootobject CallAI(ChatGptItem Item)
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
                Timeout = 3000
            };
            try
            {
                Http.Header.Add("Accept-Encoding", " gzip");
            }
            catch { }

            string GetResult = new HttpHelper().GetHtml(Http).Html;
            try
            {
                return JsonSerializer.Deserialize<ChatGptRootobject>(GetResult);
            }
            catch { return null; }
        }

        public string QuickTrans(string TransSource, Languages FromLang, Languages ToLang)
        {
            List<string> Related = new List<string>();
            if (DeFine.GlobalLocalSetting.UsingContext)
            {
                Related = EngineSelect.AIMemory.FindRelevantTranslations(FromLang, TransSource, 3);
            }

            var GetTransSource = $"Translate the following text from {FromLang} to {ToLang}:\n\n";

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
                            GetStr = ConvertHelper.StringDivision(GetStr, "\"translation\":", "\"}");

                            GetStr = GetStr.Trim();

                            if (GetStr.StartsWith("\""))
                            {
                                GetStr = GetStr.Substring(1);
                            }
                            if (GetStr.EndsWith("\""))
                            {
                                GetStr = GetStr.Substring(0, GetStr.Length - 1);
                            }
                        }
                        catch
                        {
                            return string.Empty;
                        }

                        if (DeFine.CurrentParsingLayer != null)
                        {
                            DeFine.CurrentParsingLayer.SetLog(GetTransSource + "\r\n AI:\r\n" + GetStr);
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
