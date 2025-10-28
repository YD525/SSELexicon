using System.IO;
using Cohere;
using DynamicData;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Strings;
using Mutagen.Bethesda.Strings.DI;
using PhoenixEngine.EngineManagement;
using PhoenixEngine.TranslateCore;

namespace SSELex.SkyrimManagement
{
    //https://mutagen-modding.github.io/Mutagen/Strings/#simple-string-access

    public class StringItem
    {
        public StringsSource Type;
        public string Key = "";
        public uint ID = 0;
        public string Value = "";

        public StringItem(StringsSource Type,uint ID, string Value)
        {
            this.Type = Type;
            this.ID = ID;
            this.Value = Value;
        }

        public StringItem(StringsSource Type, string Key, uint ID, string Value)
        {
            this.Type = Type;
            this.Key = Key;
            this.ID = ID;
            this.Value = Value; 
        }
    }

    //STRINGS DLSTRINGS ILSTRINGS
    public class StringsFileReader
    {
        public string CurrentModPath = "";
        public Dictionary<uint, StringItem> Strings = new Dictionary<uint, StringItem>();
        public Dictionary<string, uint> KeyBindings = new Dictionary<string, uint>();

        public static Language ToMutagenLang(Languages Lang)
        {
            return Lang switch
            {
                Languages.English => Language.English,
                Languages.German => Language.German,
                Languages.Italian => Language.Italian,
                Languages.Spanish => Language.Spanish,
                Languages.Brazilian => Language.Portuguese_Brazil,
                Languages.SimplifiedChinese => Language.ChineseSimplified,
                Languages.TraditionalChinese => Language.Chinese,
                Languages.Russian => Language.Russian,
                Languages.Japanese => Language.Japanese,
                Languages.Turkish => Language.Turkish,
                Languages.Korean => Language.Korean,
                Languages.French => Language.French,
                Languages.Polish => Language.Polish,
             
                Languages.Hindi or Languages.Urdu or Languages.Indonesian or Languages.Vietnamese
                or Languages.CanadianFrench or Languages.Portuguese or Languages.Ukrainian
                or Languages.Auto => throw new NotSupportedException($"language {Lang} has no corresponding Language enum"),
                _ => throw new NotSupportedException($"language {Lang} is not supported")
            };
        }

        public StringItem? QueryData(string UniqueKey)
        {
            if (KeyBindings.ContainsKey(UniqueKey))
            {
                return Strings[KeyBindings[UniqueKey]];
            }
            return null;
        }

        public void Close()
        {
            Clear();
            CurrentModPath = string.Empty;
        }
        public void Clear()
        {
            KeyBindings.Clear();
            Strings.Clear();
        }

        public Language? FindLang(string FileName)
        {
            FileName = FileName.ToLower();

            foreach (Language Lang in Enum.GetValues(typeof(Language)))
            {
                if (FileName.Contains("_" + Lang.ToString().ToLower() + "."))
                {
                    return Lang;
                }
            }

            return null;
        }

        public void SetModPath(string ModePath)
        {
            CurrentModPath = ModePath;
        }

        public Languages CurrentLang = Languages.Null;

        public string CurrentFileName = "";
        public void LoadStrings(Languages To)
        {
            Clear();
            CurrentLang = To;

            if (CurrentModPath.Length == 0)
            {
                return;
            }

            if (!CurrentModPath.EndsWith(@"\"))
            {
                CurrentModPath += @"\";
            }

            Language ToLang = ToMutagenLang(To);

            //Check whether the module path contains the stringsfile file

            foreach (var GetFile in Directory.GetFiles(CurrentModPath, "*.*", SearchOption.AllDirectories))
            {
                var FileName = GetFile.Substring(GetFile.LastIndexOf(@"\") + @"\".Length);
                FileName = FileName.ToLower();

                if (FileName.EndsWith(".dlstrings"))
                {
                    var SourceLang = FindLang(GetFile);

                    if(SourceLang == ToLang)
                    {
                        LoadSingleFile(GetFile, StringsSource.DL, SourceLang);
                        CurrentFileName = FileName;
                    }
                }
                if (FileName.EndsWith(".ilstrings"))
                {
                    var SourceLang = FindLang(GetFile);

                    if (SourceLang == ToLang)
                    {
                        LoadSingleFile(GetFile, StringsSource.IL, SourceLang);
                        CurrentFileName = FileName;
                    } 
                }
                if (FileName.EndsWith(".strings"))
                {
                    var SourceLang = FindLang(GetFile);

                    if (SourceLang == ToLang)
                    {
                        LoadSingleFile(GetFile, StringsSource.Normal, SourceLang);
                        CurrentFileName = FileName;
                    } 
                }
            }
        }

        public GameRelease FindGameType()
        {
            GameRelease GameType = GameRelease.SkyrimSE;

            if (DeFine.GlobalLocalSetting.GameType == GameNames.SkyrimSE)
            {
                GameType = GameRelease.SkyrimSE;
            }
            else
            if (DeFine.GlobalLocalSetting.GameType == GameNames.SkyrimLE)
            {
                GameType = GameRelease.SkyrimLE;
            }

            return GameType;
        }

        private void LoadSingleFile(string Path, StringsSource StringsSource,Language? To)
        {
            if (To == null)
            {
                return;
            }

            GameRelease GameType = FindGameType();

            var StringsOverlay = new StringsLookupOverlay(Path, StringsSource, MutagenEncoding.GetEncoding(GameType, (Language)To));

            foreach (var GetItem in StringsOverlay)  
            {
                uint GetId = GetItem.Key;
                string GetValue = GetItem.Value;

                Strings.Add(GetId, new StringItem(StringsSource, GetId, GetValue));
            }
        }

        public void UPDateKey(uint? SetID,string Key)
        {
            if (SetID == null)
            {
                return;
            }

            uint ID = (uint)SetID;

            if (Strings.ContainsKey(ID))
            {
                Strings[ID].Key = Key;
                KeyBindings.Add(Key,ID);
            }
        }
    }
}