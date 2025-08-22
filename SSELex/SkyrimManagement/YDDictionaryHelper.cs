using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using SSELex.SkyrimModManager;

namespace SSELex.SkyrimManage
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class YDDictionaryFile
    {
        public string ModName { get; set; } = "";
        public List<YDDictionary> Dictionarys { get; set; } = new List<YDDictionary>();
    }
    public class YDDictionary
    {
        public string Key { get; set; } = "";
        public string OriginalText { get; set; } = "";

        public YDDictionary()
        { 
        
        }

        public YDDictionary(string Key, string OriginalText)
        { 
           this.Key = Key;
           this.OriginalText = OriginalText;
        }

        public YDDictionary(YDDictionary Item)
        {
            this.Key = Item.Key;
            this.OriginalText = Item.OriginalText;
        }
    }
    public class YDDictionaryHelper
    {
        public static YDDictionaryFile CurrentFile = null;
        public static Dictionary<string, YDDictionary> Dictionarys = new Dictionary<string, YDDictionary>();

        public static void Close()
        {
            CurrentFile = new YDDictionaryFile();
            Dictionarys.Clear();
            CurrentModName = string.Empty;
        }

        public static void CreatDictionary()
        {
            string ModName = CurrentModName;
            string SetPath = DeFine.GetFullPath(@"Librarys\" + ModName) + ".Json";

            CurrentFile = new YDDictionaryFile();

            foreach (var Get in Dictionarys)
            {
                CurrentFile.ModName = ModName;
                CurrentFile.Dictionarys.Add(Get.Value);
            }

            if (File.Exists(SetPath))
            {
                File.Delete(SetPath);
            }

            var Options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };

            string GetJson = JsonSerializer.Serialize(CurrentFile, Options);

            DataHelper.WriteFile(SetPath,Encoding.UTF8.GetBytes(GetJson));
        }

        public static string CurrentModName = string.Empty;
        public static void ReadDictionary(string ModName)
        {
            CurrentModName = ModName;
            Dictionarys.Clear();

            string SetPath = DeFine.GetFullPath(@"Librarys\" + ModName) + ".Json";
            if (File.Exists(SetPath))
            {
                string GetData = Encoding.UTF8.GetString(DataHelper.ReadFile(SetPath));
                var GetClass = JsonSerializer.Deserialize<YDDictionaryFile>(GetData);
                if (GetClass != null)
                {
                    CurrentFile = GetClass;

                    foreach (var Get in CurrentFile.Dictionarys)
                    {
                        Dictionarys.Add(Get.Key,new YDDictionary(Get));
                    }
                }
            }
        }

        public static YDDictionary CheckDictionary(string Key)
        {
            if (Dictionarys.ContainsKey(Key))
            { 
               return Dictionarys[Key];
            }

            return null;
        }

        public static int UPDateTransText(string Key,string OriginalText)
        {   
            if (Dictionarys.ContainsKey(Key))
            {
                Dictionarys[Key].OriginalText = OriginalText;
                return 1;
            }
            else
            {
                Dictionarys.Add(Key,new YDDictionary(Key,OriginalText));
                return 2;
            }
        }
    }
}
