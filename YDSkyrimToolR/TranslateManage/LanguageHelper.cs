using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using System.Net.Sockets;
using YDSkyrimToolR;
using YDSkyrimToolR.TranslateManage;
using YDSkyrimToolR.ConvertManager;
using ICSharpCode.AvalonEdit.Highlighting;
using ValveKeyValue;
using YDSkyrimToolR.SkyrimManage;
using static YDSkyrimToolR.TranslateCore.LanguageHelper;
using Mutagen.Bethesda.Starfield;

namespace YDSkyrimToolR.TranslateCore
{
    public class LanguageHelper
    {
        public static void Shuffle<T>(IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static List<EngineSelect> EngineSelects = new List<EngineSelect>();

        public static void Init()
        {
            ReloadEngine();
        }

        public static void ReloadEngine()
        {
            EngineSelects.Clear();

            EngineSelects.Add(new EngineSelect(new BaiDuApi(), 6));
            EngineSelects.Add(new EngineSelect(new ICIBAHelper(), 3));
            EngineSelects.Add(new EngineSelect(new GoogleHelper(), 1));
            Shuffle<EngineSelect>(EngineSelects);
        }

        public enum LanguageType
        {
            zh,//中文    
            en,//英文    
            ja,//日文    
            ko,//韩文    
            fr,//法文    
            es,//西班牙文
            pt,//葡萄牙文
            it,//意大利文
            ru,//俄文    
            vi,//越南文    
            de,//德文    
            ar,//阿拉伯文
            id,//印尼文    
            auto,//自动识别
        }

        public bool IsEnglishChar(string strValue)
        {
            string[] Chars = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

            string TempContent = strValue.ToLower();
            foreach (var Get in Chars)
            {
                if (TempContent.Contains(Get.ToLower()))
                {
                    return true;
                }
            }

            return false;
        }

        public static string ProcessCodeSign(string Line,WordTProcess ProcessItems)
        {
            return Line;

            List<string> AllCode = new List<string>();

            foreach (var Get in ProcessItems.AllLine)
            {
                if (Get.Code.Trim().Length > 0)
                {
                    AllCode.Add(Get.Code);
                }
            }

            string TempSign = "";
            int Offset = -1;

            string NewLine = "";

            for (int i = 0; i < Line.Length; i++)
            {
                string GetChar = Line.Substring(i, 1);

                if (GetChar == "_")
                {
                    TempSign += GetChar;

                    if (TempSign.Length == 3)
                    {
                        Offset++;

                        TempSign = "";

                        if (Offset < AllCode.Count)
                        {
                            NewLine += AllCode[Offset];
                        }
                    }
                }
                else
                {
                    NewLine += GetChar;
                    TempSign = "";
                }
            }

            return NewLine;
        }

        public static string OptimizeString(string Str)
        {
            return Str.Replace("（", "(").Replace("）", ")").Replace("【", "[").Replace("】", "]").Replace("一个一个", "一个").Replace("一个一对", "一对").Replace("一个一套", "一套").Replace("一个一双", "一双");
        }

        public string EnglishToCN(string Str)
        {
            if (Str == "")
            {
                return "";
            }

            string GetCacheStr = TranslateDBCache.FindCache(Str, 0, 1);

            if (GetCacheStr.Trim().Length > 0)
            {
                WordProcess.SendTranslateMsg("从数据库缓存", Str, GetCacheStr);
                return GetCacheStr;
            }

            NextSet:

            int LimitCount = 0;
            for (int i = 0; i < LanguageHelper.EngineSelects.Count; i++)
            {
                if (LanguageHelper.EngineSelects[i].CurrentCallCount < LanguageHelper.EngineSelects[i].MaxUseCount)
                {
                    string GetTrans = LanguageHelper.EngineSelects[i].Call(Str);
                    if (GetTrans.Trim().Length > 0)
                    {
                        return GetTrans;
                    }
                }
                else
                {
                    LimitCount++;
                }
            }

            if (LimitCount == LanguageHelper.EngineSelects.Count)
            {
                ReloadEngine();
                goto NextSet;
            }
            return string.Empty;
        }

        public class EngineSelect
        {
            public object Engine = new object();
            public int CurrentCallCount = 0;
            public int MaxUseCount = 0;

            public EngineSelect(object engine, int maxUseCount)
            {
                Engine = engine;
                MaxUseCount = maxUseCount;
            }

            public string Call(string SourceStr)
            {
                WordTProcess OneProcess = new WordTProcess(SourceStr);

                string TransLine = "";

                foreach (var Get in OneProcess.AllLine)
                {
                    TransLine += Get.TextMsg + Get.Char;
                }

                if (TransLine.Length > 0)
                {
                    string GetSource = TransLine;

                    GetSource = LanguageHelper.ProcessCodeSign(GetSource, OneProcess);

                    GetSource = OneProcess.StartContent + GetSource;

                    if (GetSource.Trim().Length == 0)
                    {
                        return "";
                    }

                    string SetTransLine = "";

                    if (this.Engine is BaiDuApi)
                    {
                        if (DeFine.BaiDuYunApiUsing)
                        {
                            if (this.CurrentCallCount < this.MaxUseCount)
                            {
                                var GetData = (this.Engine as BaiDuApi).TransStr(GetSource, "en", "zh");
                                if (GetData != null)
                                {
                                    if (GetData.trans_result != null)
                                    {
                                        if (GetData.trans_result.Length > 0)
                                        {
                                            SetTransLine = GetData.trans_result[0].dst;
                                            this.CurrentCallCount++;
                                            WordProcess.SendTranslateMsg("翻译引擎(百度云)", GetSource, SetTransLine);
                                        }
                                        else
                                        {
                                            this.CurrentCallCount = this.MaxUseCount;
                                        }
                                    }
                                    else
                                    {
                                        this.CurrentCallCount = this.MaxUseCount;
                                    }
                                }
                                else
                                {
                                    this.CurrentCallCount = this.MaxUseCount;
                                }
                            }
                        }
                        else
                        {
                            this.CurrentCallCount = this.MaxUseCount;
                        }
                    }
                    else
                    if (this.Engine is ICIBAHelper)
                    {
                        if (DeFine.ICIBAApiUsing)
                        {
                            var GetData = ConvertHelper.ObjToStr((this.Engine as ICIBAHelper).FreeTransStr(GetSource));
                            if (GetData.Trim().Length > 0)
                            {
                                SetTransLine = GetData;
                                this.CurrentCallCount++;
                                WordProcess.SendTranslateMsg("翻译引擎(爱词霸)", GetSource, SetTransLine);
                            }
                            else
                            {
                                this.CurrentCallCount = this.MaxUseCount;
                            }
                        }
                        else
                        {
                            this.CurrentCallCount = this.MaxUseCount;
                        }
                    }
                    else
                    if (this.Engine is GoogleHelper)
                    {
                        if (DeFine.GoogleYunApiUsing)
                        {
                            var GetData = ConvertHelper.ObjToStr((this.Engine as GoogleHelper).FreeTransStr(GetSource));
                            if (StrChecker.ContainsChinese(GetData))
                            {
                                SetTransLine = GetData;
                                this.CurrentCallCount++;
                                WordProcess.SendTranslateMsg("翻译引擎(谷歌)", GetSource, SetTransLine);
                            }
                            else
                            {
                                this.CurrentCallCount = this.MaxUseCount;
                            }
                        }
                        else
                        {
                            this.CurrentCallCount = this.MaxUseCount;
                        }
                    }

                    if (new WordProcess().HasChinese(SetTransLine))
                    {
                        TranslateDBCache.AddCache(GetSource, 0, 1, SetTransLine);
                    }

                    return SetTransLine.Trim();
                }

                return string.Empty;
            }
        }
       

        public string EncryptString(string str)
        {
            MD5 md5 = MD5.Create();
            // 将字符串转换成字节数组
            byte[] byteOld = Encoding.UTF8.GetBytes(str);
            // 调用加密方法
            byte[] byteNew = md5.ComputeHash(byteOld);
            // 将加密结果转换为字符串
            StringBuilder sb = new StringBuilder();
            foreach (byte b in byteNew)
            {
                // 将字节转换成16进制表示的字符串，
                sb.Append(b.ToString("x2"));
            }
            // 返回加密的字符串
            return sb.ToString();
        }

        public List<string> AllEng = new List<string>() {
            "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
        };
    }

    public class CharLineExtend
    {
        public string TextMsg = "";
        public string Char = "";
        public string Code = "";

        public CharLineExtend(string Msg, string Char,string Code)
        {
            this.TextMsg = Msg;
            this.Char = Char;
            this.Code = Code;
        }
    }

    public class WordTProcess
    {
        public string Content = "";
        public string StartContent = "";

        public List<CharLineExtend> AllLine = new List<CharLineExtend>();

        public WordTProcess(string Content)
        {
            this.Content = Content;

            this.Content = this.Content.Replace("(", "（");
            this.Content = this.Content.Replace(")", "）");
         

            this.Content = this.Content.Replace("（（", "☯（");
            this.Content = this.Content.Replace("））", "）㊣");
            this.Content = this.Content.Replace("（ （", "☯（");
            this.Content = this.Content.Replace("） ）", "）㊣");
            this.Content = this.Content.Replace("（  （", "☯（");
            this.Content = this.Content.Replace("）  ）", "）㊣");


            bool IsCode = false;
            string CodeStr = "";
            string RichText = "";

            for (int i = 0; i < this.Content.Length; i++)
            {
                string GetChar = this.Content.Substring(i, 1);

                if (GetChar == "）" || GetChar == ")")
                {
                    IsCode = false;

                    if (CodeStr.Trim().Length > 0)
                    {
                        AllLine.Add(new CharLineExtend(string.Empty, string.Empty, CodeStr));
                        CodeStr = "";
                    }

                    GetChar = "";
                }
                else
                if (IsCode)
                {
                    CodeStr += GetChar;
                    GetChar = "";
                }
                else
                if (GetChar == "（" || GetChar == "(")
                {
                    IsCode = true;

                    if (RichText.Trim().Length > 0)
                    {
                        AllLine.Add(new CharLineExtend(RichText, string.Empty, string.Empty));
                        RichText = "";
                    }
                    else
                    {
                        RichText = "";
                    }

                    GetChar = "";
                }
               

                if (GetChar == "-")
                {
                    AllLine.Add(new CharLineExtend(RichText,GetChar,CodeStr));
                    RichText = "";
                    GetChar = "";
                }
                else
                if (GetChar == ".")
                {
                    AllLine.Add(new CharLineExtend(RichText, GetChar,CodeStr));
                    RichText = "";
                    GetChar = "";
                }
                else
                if (GetChar == ",")
                {
                    AllLine.Add(new CharLineExtend(RichText, GetChar,CodeStr));
                    RichText = "";
                    GetChar = "";
                }
                else
                if (GetChar == ";")
                {
                    AllLine.Add(new CharLineExtend(RichText, GetChar,CodeStr));
                    RichText = "";
                    GetChar = "";
                }
                else
                {
                    RichText += GetChar;
                }
            }

            if (RichText.Trim().Length > 0)
            {
                AllLine.Add(new CharLineExtend(RichText, string.Empty,CodeStr));
                RichText = "";
            }

            for (int i = 0; i < AllLine.Count; i++)
            {
                if (AllLine[i].Code.Trim().Length > 0)
                {
                    if (ConvertHelper.ObjToInt(AllLine[i].Code.Trim()) > 0)
                    {
                        AllLine[i].TextMsg = "（" + AllLine[i].Code + "）";
                        AllLine[i].Code = "（" + AllLine[i].Code + "）";
                    }
                    else
                    {
                        AllLine[i].TextMsg = "（" + AllLine[i].Code + "）";
                        AllLine[i].Code = "";
                    }
                }
            }

            for (int i = 0; i < AllLine.Count; i++)
            {
                if (AllLine[i].TextMsg.Contains("☯") || AllLine[i].TextMsg.Contains("㊣"))
                {
                    AllLine[i].TextMsg = AllLine[i].TextMsg.Replace("☯", "(").Replace("㊣", ")");
                }
            }
        }
    }

    public class QuickSearch
    {
        public string Key = "";
        public string Value = "";

        public QuickSearch(string Key, string Value)
        {
            this.Key = Key;
            this.Value = Value;
        }

    }

}
