using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YDSkyrimToolR.ConvertManager;

namespace YDSkyrimToolR.TranslateManage
{
    public class GoogleHelper
    {
        public string ApiKey = "";

        public string FreeTransStr(string ENText)
        {
            string Url = string.Format("https://translate.googleapis.com/translate_a/single?client=gtx&sl=en&tl=zh&dt=t&q={0}",System.Web.HttpUtility.UrlEncode(ENText));
            HttpItem Http = new HttpItem()
            {
                URL = Url,
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/132.0.0.0 Safari/537.36",
                Method = "Get",
                KeepAlive = true,
                Accept = "application/json, text/plain; q=0.9, text/html;q=0.8,",
                Postdata = "",
                Cookie = "",
                ContentType = "application/json",
                Timeout = 3000
            };
            try
            {
                Http.Header.Add("Accept-Encoding", " gzip");
            }
            catch { }

            var GetResult = new HttpHelper().GetHtml(Http).Html;

            if (GetResult.Contains("[") && GetResult.Contains("\""))
            {
                return ConvertHelper.StringDivision(GetResult,"\"", "\"").Trim();
            }

            return null;
        }
    }
}
