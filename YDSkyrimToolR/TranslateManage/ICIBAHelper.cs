
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using YDSkyrimToolR.ConvertManager;

namespace YDSkyrimToolR.TranslateManage
{
    public class ICIBAHelper
    {
        public string FreeTransStr(string ENText)
        {
            string GetEncodeStr = System.Web.HttpUtility.UrlEncode(ENText);
            string Url = string.Format("https://www.iciba.com/_next/data/NW4LYCQTPmcJk1wISQx_s/word.json?w={0}", GetEncodeStr);
            WebHeaderCollection Headers = new WebHeaderCollection();
            Headers.Add("x-nextjs-data", "1");
            Headers.Add("sec-ch-ua", "\"Not A(Brand\";v=\"8\", \"Chromium\";v=\"132\", \"Google Chrome\";v=\"132\"");
            Headers.Add("sec-ch-ua-platform", "Windows");
            Headers.Add("Sec-Fetch-Site", "same-origin");
            Headers.Add("Sec-Fetch-Mode", "cors");
            Headers.Add("Sec-Fetch-Dest", "empty");

            HttpItem Http = new HttpItem()
            {
                URL = Url,
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/132.0.0.0 Safari/537.36",
                Method = "Get",
                Header = Headers,
                KeepAlive = true,
                Accept = "*/*",
                Referer = string.Format("https://www.iciba.com/word?w={0}", GetEncodeStr),
                Cookie = "",
                ContentType = "application/json",
                Timeout = 2000,
            };
            try
            {
                Http.Header.Add("Accept-Encoding", "gzip,deflate,br,zstd");
            }
            catch { }

            var GetResult = new HttpHelper().GetHtml(Http).Html;

            try {
                if (GetResult.Contains("translate_result\":\""))
                {
                    var GetStr = ConvertHelper.StringDivision(GetResult, "translate_result\":\"", "\",\"");
                    return GetStr.Trim();
                }
                if (GetResult.Contains("mean\":\""))
                {
                    var GetStr = ConvertHelper.StringDivision(GetResult, "mean\":\"", "\",\"");
                    if (GetStr.Contains("；"))
                    {
                        GetStr = GetStr.Split("；")[0].Trim();
                    }
                    return GetStr.Trim();
                }
            }
            catch { }
            return null;
        }
    }
}
