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
using System.Text.RegularExpressions;

namespace YDSkyrimToolR.TranslateManage
{
    /*
* @Author: YD525
* @GitHub: https://github.com/YD525/YDSkyrimToolR
* @Date: 2025-02-06
*/
    public class GoogleHelper
    {
        public string ToLanguageCode(Languages language)
        {
            return language switch
            {
                Languages.English => "en",
                Languages.Chinese => "zh-CN",
                Languages.Japanese => "ja",
                Languages.German => "de",
                Languages.Korean => "ko",
                Languages.Turkish => "tr",
                Languages.Brazilian => "pt-BR",
                _ => throw new ArgumentOutOfRangeException(nameof(language), "Unsupported language")
            };
        }

        public string ApiKey = "";

        public string FreeTransStr(string ENText,Languages FromLang, Languages ToLang)
        {
            return TransStr(ToLanguageCode(FromLang), ToLanguageCode(ToLang), ENText);
        }

        public bool IsMd5Format(string input)
        {
            return Regex.IsMatch(input, "^[a-f0-9]{32}$");
        }

        public string RestoreUnicode(string input)
        {
            // 使用正则表达式来查找所有的 \uXXXX 格式的 Unicode 转义字符
            return Regex.Replace(input, @"\\u([0-9a-fA-F]{4})", match =>
            {
                // 将转义字符还原成实际的字符
                return char.ConvertFromUtf32(int.Parse(match.Groups[1].Value, System.Globalization.NumberStyles.HexNumber));
            });
        }

        public static object LockerTrans = new object();
        public string TransStr(string FromLang,string ToLang, string ENText)
        {
            lock (GoogleHelper.LockerTrans)
            {
                string Url = string.Format("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}", FromLang, ToLang, System.Web.HttpUtility.UrlEncode(ENText));
                HttpItem Http = new HttpItem()
                {
                    URL = Url,
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/132.0.0.0 Safari/537.36",
                    Method = "Get",
                    KeepAlive = true,
                    ProxyIp = "127.0.0.1:7890",
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

                var outerArray = GetResult.Split(new string[] { "],[" }, StringSplitOptions.None);

                foreach (var item in outerArray)
                {
                    var innerItem = item.Trim(new char[] { '[', ']', '\n', ' ' });
                    var Array = innerItem.Split(',');
                    
                    if (Array.Length >= 0)
                    {
                        if (Array[0] != "null")
                        {
                            string GetStr = Array[0].Trim('"', ',').Replace("\r\n", "") + "\r\n";
                            if (!IsMd5Format(GetStr.Replace("\r\n","").Replace("\n","")))
                            {
                                RichText += RestoreUnicode(GetStr);
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (RichText.Trim().Length > 0)
                {
                    if (RichText.Contains("The SSL connection could not be established"))
                    {
                        Thread.Sleep(3000);
                        return null;
                    }
                    if (!RichText.ToLower().Contains("The operation has timed out.".ToLower()))
                    {
                        Thread.Sleep(1000);
                        if (RichText.EndsWith("\r\n"))
                        {
                            RichText = RichText.Substring(0, RichText.Length - "\r\n".Length);
                        }
                        return RichText;
                    }
                    else
                    { 
                    
                    }

                    Thread.Sleep(3000);
                    return null;

                }
                Thread.Sleep(3000);
                return null;
            }
        }
    }
}
