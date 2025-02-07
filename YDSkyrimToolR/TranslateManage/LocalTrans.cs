using Mutagen.Bethesda.Skyrim;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ValveKeyValue;
using YDSkyrimToolR.ConvertManager;
using YDSkyrimToolR.SkyrimModManager;
using YDSkyrimToolR.UIManage;

namespace YDSkyrimToolR.TranslateManage
{

    public class COCA
    {
        public string order { get; set; }
        public string word { get; set; }
        public string explain { get; set; }
        public string sentence { get; set; }
        public string trans { get; set; }
        public string phonetic { get; set; }
    }

  
    public class LocalTrans
    {
        public static Dictionary<string,COCA> COCAs = new Dictionary<string, COCA>();
        public static void Init()
        {
            var GetContent = FileHelper.ReadFileByStr(DeFine.GetFullPath(@"\LocalDB\coca.json"), Encoding.UTF8);
            var GetCocaData = JsonSerializer.Deserialize<List<COCA>>(GetContent);
            foreach (var GetItem in GetCocaData)
            {
                if (!COCAs.ContainsKey(GetItem.word.ToLower()))
                {
                    COCAs.Add(GetItem.word.ToLower(),GetItem);
                }
            }
        }

        public static string ToNormalStyle(string OneWord)
        {
            string Pattern = @"([A-Z]{2,})(?=[A-Z][a-z])";
            return Regex.Replace(OneWord, Pattern, "$1 ");
        }
        public static string SearchLocalData(string NewWord)
        {
            var OneWord = SystemDataWriter.PreFormatStr(ToNormalStyle(NewWord));
            string AutoChar = "";
            if (OneWord.EndsWith("."))
            {
                AutoChar = ".";
            }
            if (OneWord.EndsWith(","))
            {
                AutoChar = ",";
            }
            if (OneWord.EndsWith("?"))
            {
                AutoChar = "?";
            }
            if (OneWord.EndsWith("!"))
            {
                AutoChar = "!";
            }

            string RichText = "";
            foreach (var WordR in OneWord.Split(' '))
            {
                if (WordR.Trim().Length > 0)
                {
                    string Word = WordR.Trim();

                    bool FindIs = false;
                    if(COCAs.ContainsKey(Word.ToLower()))
                    {
                        var GetCoca = COCAs[Word.ToLower()];

                        if (GetCoca.word.Equals(Word.ToLower()))
                        {
                            if (GetCoca.explain.Contains("."))
                            {
                                string GetStr = GetCoca.explain.Split('.')[1];

                                var GetParams = GetStr.Split('；');
                                if (GetParams.Length > 0)
                                {
                                    if (GetParams[0].Contains("，"))
                                    {
                                        GetParams[0] = GetParams[0].Split('，')[0];
                                    }
                                    if (GetParams[0].Contains(","))
                                    {
                                        GetParams[0] = GetParams[0].Split(',')[0];
                                    }

                                    if (GetParams[0].Contains("<"))
                                    {
                                        GetParams[0] = GetParams[0].Split('<')[0];
                                    }
                                    RichText += GetParams[0].Trim() + "";
                                    FindIs = true;
                                }
                            }
                        }
                    }
                    if (!FindIs)
                    {
                        RichText += Word;
                    }
                }
                   
            }

            if (RichText.Trim().Length == 0)
            {
                return null;
            }
            return RichText.Trim();
        }
    }
}
