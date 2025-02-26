using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using YDSkyrimToolR.ConvertManager;
using static YDSkyrimToolR.TranslateManage.BaiDuApi;
using YDSkyrimToolR.TranslateCore;

namespace YDSkyrimToolR.TranslateManage
{

    public class GoogleHelper
    {
        public string ApiKey = "";

        public string FreeTransStr(string ENText,Languages FromLang, Languages ToLang)
        {
            BDLanguages Source = BDLanguages.en;
            BDLanguages Target = BDLanguages.zh;

            if (FromLang == Languages.English)
            {
                Source = BDLanguages.en;
            }
            if (FromLang == Languages.Chinese)
            {
                Source = BDLanguages.zh;
            }
            if (FromLang == Languages.Japanese)
            {
                Source = BDLanguages.jp;
            }
            if (FromLang == Languages.German)
            {
                Source = BDLanguages.de;
            }
            if (FromLang == Languages.Korean)
            {
                Source = BDLanguages.kor;
            }

            if (ToLang == Languages.English)
            {
                Target = BDLanguages.en;
            }
            if (ToLang == Languages.Chinese)
            {
                Target = BDLanguages.zh;
            }
            if (ToLang == Languages.Japanese)
            {
                Target = BDLanguages.jp;
            }
            if (ToLang == Languages.German)
            {
                Target = BDLanguages.de;
            }
            if (ToLang == Languages.Korean)
            {
                Target = BDLanguages.kor;
            }

            return TransStr(Source.ToString(), Target.ToString(), ENText);
        }
        public string TransStr(string FromLang,string ToLang, string ENText)
        {
            string Url = string.Format("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",FromLang,ToLang,System.Web.HttpUtility.UrlEncode(ENText));
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

            GetResult = GetResult.Trim(new char[] { '[', ']', '\n', ' ' });

            string RichText = "";
            // 分离外层数组
            var outerArray = GetResult.Split(new string[] { "],[" }, StringSplitOptions.None);

            foreach (var item in outerArray)
            {
                // 移除内层数组的开头和结尾的方括号
                var innerItem = item.Trim(new char[] { '[', ']', '\n', ' ' });
                var Array = innerItem.Split(',');
                // 输出中文和英文文本
                if (Array.Length>=10)
                {
                    RichText += Array[0].Trim('"', ',').Replace("\r\n","") +"\r\n";
                }
            }
            if (RichText.Trim().Length > 0)
            {
                if (!RichText.Contains("timeout"))
                {
                    return RichText;
                }
                return null;

            }
            return null;
        }
    }
}
