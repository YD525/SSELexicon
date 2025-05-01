using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web;
using SSELex.RequestManagement;
using SSELex.TranslateCore;

namespace SSELex.PlatformManagement
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class BaiDuApi
    {
        public enum BDLanguages
        {
            en=0, zh = 1, jp=2, de=3, kor = 5, zh_TW = 6
        }

        public string GenerateSignature(string appId, string query, string salt, string secretKey)
        {
            string rawString = appId + query + salt + secretKey;
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(rawString);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public BaiduTransResult? TransStr(string Query,Languages FromLang, Languages ToLang)
        {
            BDLanguages Source = BDLanguages.en;
            BDLanguages Target = BDLanguages.zh;

            if (FromLang == Languages.English)
                Source = BDLanguages.en;
            else if (FromLang == Languages.SimplifiedChinese)
                Source = BDLanguages.zh;
            else if (FromLang == Languages.TraditionalChinese)
                Source = BDLanguages.zh_TW;
            else if (FromLang == Languages.Japanese)
                Source = BDLanguages.jp;
            else if (FromLang == Languages.German)
                Source = BDLanguages.de;
            else if (FromLang == Languages.Korean)
                Source = BDLanguages.kor;

            if (ToLang == Languages.English)
                Target = BDLanguages.en;
            else if (ToLang == Languages.SimplifiedChinese)
                Target = BDLanguages.zh;
            else if (ToLang == Languages.TraditionalChinese)
                Target = BDLanguages.zh_TW;
            else if (ToLang == Languages.Japanese)
                Target = BDLanguages.jp;
            else if (ToLang == Languages.German)
                Target = BDLanguages.de;
            else if (ToLang == Languages.Korean)
                Target = BDLanguages.kor;

            return ConstructGetRequestUrl(DeFine.GlobalLocalSetting.BaiDuAppID, Query, Source.ToString(), Target.ToString(), new Random(Guid.NewGuid().GetHashCode()).Next(100, 999).ToString(), DeFine.GlobalLocalSetting.BaiDuSecretKey);
        }

        public string Host = "https://fanyi-api.baidu.com";

        public BaiduTransResult? ConstructGetRequestUrl(string AppId, string Query, string FromLang, string ToLang, string Salt, string SecretKey)
        {
            string Sign = GenerateSignature(AppId, Query, Salt, SecretKey);
            string EncodedQuery = HttpUtility.UrlEncode(Query, Encoding.UTF8);

            string Param = $"?appid={AppId}&q={EncodedQuery}&from={FromLang}&to={ToLang}&salt={Salt}&sign={Sign}";

            string Url = Host + "/api/trans/vip/translate";
            WebHeaderCollection Headers = new WebHeaderCollection();

            HttpItem Http = new HttpItem()
            {
                URL = Url + Param,
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/132.0.0.0 Safari/537.36",
                Method = "Get",
                Header = Headers,
                Accept = "*/*",
                Postdata = "",
                Cookie = "",
                ContentType = "application/json; charset=utf-8",
                Timeout = 3000,
                ProxyIp = ProxyCenter.GlobalProxyIP
            };
            try
            {
                Http.Header.Add("Accept-Encoding", " gzip");
            }
            catch { }

            var GetResult = new HttpHelper().GetHtml(Http).Html;

            DeFine.CurrentLogView.SetLog("Baidu:" + GetResult);

            if (GetResult != null)
            {
                try 
                {
                    return JsonSerializer.Deserialize<BaiduTransResult>(GetResult);
                }
                catch 
                {
                    return null;
                }
            }

            return null;
        }
    }

    public class BaiduTransResult
    {
        public string from { get; set; }
        public string to { get; set; }
        public BaiduTrans_Result[] trans_result { get; set; }
    }

    public class BaiduTrans_Result
    {
        public string src { get; set; }
        public string dst { get; set; }
    }

}
