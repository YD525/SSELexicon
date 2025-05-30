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
            en = 0,
            zh = 1,
            jp = 2,
            de = 3,
            kor = 5,
            zh_TW = 6,
            hi = 7,
            ur = 8,
            id = 9,
            it = 10,
            es = 11,
            fr = 12,
            ru = 13,
            vie = 14,
            th = 15,
            pl = 16,
            pt = 17,
            nl = 18
        }
        public BDLanguages MapToBDLanguage(Languages lang)
        {
            return lang switch
            {
                Languages.English => BDLanguages.en,
                Languages.SimplifiedChinese => BDLanguages.zh,
                Languages.TraditionalChinese => BDLanguages.zh_TW,
                Languages.Japanese => BDLanguages.jp,
                Languages.German => BDLanguages.de,
                Languages.Korean => BDLanguages.kor,
                Languages.Italian => BDLanguages.it,
                Languages.Spanish => BDLanguages.es,
                Languages.Hindi => BDLanguages.hi,
                Languages.Urdu => BDLanguages.ur,
                Languages.Indonesian => BDLanguages.id,
                Languages.French => BDLanguages.fr,
                Languages.Russian => BDLanguages.ru,
                Languages.Vietnamese => BDLanguages.vie,
                Languages.Polish => BDLanguages.pl,
                Languages.Brazilian => BDLanguages.pt,
                _ => BDLanguages.en 
            };
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
            BDLanguages Source = MapToBDLanguage(FromLang);
            BDLanguages Target = MapToBDLanguage(ToLang);

            return ConstructGetRequestUrl(
                DeFine.GlobalLocalSetting.BaiDuAppID,
                Query,
                Source.ToString(),
                Target.ToString(),
                new Random(Guid.NewGuid().GetHashCode()).Next(100, 999).ToString(),
                DeFine.GlobalLocalSetting.BaiDuSecretKey
            );
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
                Timeout = DeFine.GlobalRequestTimeOut,
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
