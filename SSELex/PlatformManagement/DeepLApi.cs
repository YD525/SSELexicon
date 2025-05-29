using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SSELex.RequestManagement;
using SSELex.TranslateCore;

namespace SSELex.PlatformManagement
{
    public class DeepLItem
    {
        public List<string> text { get; set; }
        public string target_lang { get; set; } = "";
    }

    public class DeepLResult
    {
        public DeepLTranslation[] translations { get; set; }
    }

    public class DeepLTranslation
    {
        public string detected_source_language { get; set; }
        public string text { get; set; }
    }



    public class DeepLApi
    {
        private static string DeepLFreeHost = "https://api-free.deepl.com/v2/translate";
        private static string DeepLHost = "https://api.deepl.com/v2/translate";
        public static string GetLanguageCode(Languages language)
        {
            switch (language)
            {
                case Languages.English:
                    return "EN";
                case Languages.SimplifiedChinese:
                    return "ZH";
                case Languages.Japanese:
                    return "JA";
                case Languages.German:
                    return "DE";
                case Languages.Korean:
                    return "KO";
                case Languages.Turkish:
                    return "TR";
                case Languages.Brazilian: // Should be Portuguese for Brazil
                    return "PT";
                case Languages.Russian:
                    return "RU";
                case Languages.TraditionalChinese:
                    return "ZH-TW";
                case Languages.Italian:
                    return "IT"; 
                case Languages.Spanish:
                    return "ES"; 
                case Languages.Hindi:       
                    return "HI";
                case Languages.Urdu:        
                    return "UR";
                case Languages.Indonesian:
                    return "ID";
                default:
                    throw new ArgumentOutOfRangeException(nameof(language), "Unknown language");
            }
        }
        public string QuickTrans(string TransSource, Languages FromLang, Languages ToLang)
        {
            try
            {
                DeepLItem NDeepLItem = new DeepLItem();
                NDeepLItem.target_lang = GetLanguageCode(ToLang);
                NDeepLItem.text = new List<string>() { TransSource };
                var GetResult = CallAI(NDeepLItem);
                if (GetResult == null)
                {
                    return string.Empty;
                }
                if (GetResult.translations != null)
                {
                    if (GetResult.translations.Length > 0)
                    {
                        return GetResult.translations[0].text;
                    }
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
        public DeepLResult CallAI(DeepLItem Item)
        {
            string GetJson = JsonSerializer.Serialize(Item);
            WebHeaderCollection Headers = new WebHeaderCollection();
            Headers.Add("Authorization", string.Format("DeepL-Auth-Key {0}", DeFine.GlobalLocalSetting.DeepLKey));
            string AutoHost = "";

            if (DeFine.GlobalLocalSetting.IsFreeDeepL)
            {
                AutoHost = DeepLFreeHost;
            }
            else
            {
                AutoHost = DeepLHost;
            }

            HttpItem Http = new HttpItem()
            {
                URL = AutoHost,
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
                DeFine.CurrentLogView.SetLog("DeepL:" + GetResult);
                return JsonSerializer.Deserialize<DeepLResult>(GetResult);
            }
            catch
            {
                return null;
            }
        }
    }
}
