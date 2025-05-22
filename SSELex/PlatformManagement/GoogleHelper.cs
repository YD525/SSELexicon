using SSELex.RequestManagement;
using SSELex.TranslateCore;
using System.Text.RegularExpressions;

namespace SSELex.PlatformManagement
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class GoogleHelper
    {
        public string ToLanguageCode(Languages Language)
        {
            return Language switch
            {
                Languages.English => "en",
                Languages.SimplifiedChinese => "zh-CN",
                Languages.TraditionalChinese => "zh-TW",
                Languages.Japanese => "ja",
                Languages.German => "de",
                Languages.Korean => "ko",
                Languages.Turkish => "tr",
                Languages.Brazilian => "pt-BR",
                Languages.Russian => "ru",
                Languages.Italian => "it",     
                Languages.Spanish => "es",    
                Languages.Hindi => "hi",           
                Languages.Urdu => "ur",           
                Languages.Indonesian => "id", 
                _ => throw new ArgumentOutOfRangeException(nameof(Language), "Unsupported language")
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
            // Use regular expressions to find all Unicode escape sequences of the form \uXXXX
            return Regex.Replace(input, @"\\u([0-9a-fA-F]{4})", match =>
            {
                //Restore escape characters to actual characters
                return char.ConvertFromUtf32(int.Parse(match.Groups[1].Value, System.Globalization.NumberStyles.HexNumber));
            });
        }

        public static object LockerTrans = new object();
        public string TransStr(string FromLang,string ToLang, string ENText)
        {
            lock (LockerTrans)
            {
                string Url = string.Format("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}", FromLang, ToLang, System.Web.HttpUtility.UrlEncode(ENText));
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
                    Timeout = 3000,
                    ProxyIp = ProxyCenter.GlobalProxyIP
                };
                try
                {
                    Http.Header.Add("Accept-Encoding", " gzip");
                }
                catch { }

                var GetResult = new HttpHelper().GetHtml(Http).Html;

                DeFine.CurrentLogView.SetLog("Google:" + GetResult);

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
                        if (!RichText.ToLower().Contains("No connection could be made because the target ".ToLower()))
                        {
                            Thread.Sleep(1000);
                            if (RichText.EndsWith("\r\n"))
                            {
                                RichText = RichText.Substring(0, RichText.Length - "\r\n".Length);
                            }
                            return RichText;
                        }
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
