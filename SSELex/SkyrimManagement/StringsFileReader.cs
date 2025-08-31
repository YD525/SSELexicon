using System.IO;
using Cohere;
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

    //STRINGS DLSTRINGS ILSTRINGS
    public class StringsFileReader
    {
        public Dictionary<uint, string> STRINGS = new Dictionary<uint, string>();
        public Dictionary<uint, string> DLSTRINGS = new Dictionary<uint, string>();
        public Dictionary<uint, string> ILSTRINGS = new Dictionary<uint, string>();


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

        public void Close()
        {
            STRINGS.Clear();
            DLSTRINGS.Clear();
            ILSTRINGS.Clear();
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

        //C:\\Users\\52508\\Desktop\\TempFolder\\SkyrimVR strings-16355-1-0\\data\\strings\\

        public void Load(string Path,Languages To)
        {
            Close();

            if (!Path.EndsWith(@"\"))
            {
                Path+= @"\";
            }

            Language ToLang = ToMutagenLang(To);

            //Check whether the module path contains the stringsfile file

            foreach (var GetFile in Directory.GetFiles(Path))
            {
                var FileName = GetFile.Substring(GetFile.LastIndexOf(@"\") + @"\".Length);
                FileName = FileName.ToLower();

                if (FileName.EndsWith(".dlstrings"))
                {
                    var SourceLang = FindLang(GetFile);

                    if(SourceLang == ToLang)
                    {
                        LoadSingleFile(GetFile, StringsSource.DL, SourceLang);
                    }
                }
                if (FileName.EndsWith(".ilstrings"))
                {
                    var SourceLang = FindLang(GetFile);

                    if (SourceLang == ToLang)
                    {
                        LoadSingleFile(GetFile, StringsSource.IL, SourceLang);
                    } 
                }
                if (FileName.EndsWith(".strings"))
                {
                    var SourceLang = FindLang(GetFile);

                    if (SourceLang == ToLang)
                    {
                        LoadSingleFile(GetFile, StringsSource.Normal, SourceLang);
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

            Dictionary<uint, string> TempDictionary = new Dictionary<uint, string>();

            foreach (var GetItem in StringsOverlay)  
            {
                uint GetId = GetItem.Key;
                string GetValue = GetItem.Value;

                if (!TempDictionary.ContainsKey(GetId))
                {
                    TempDictionary.Add(GetId,GetValue);
                }
            }

            if (StringsSource == StringsSource.Normal)
            {
                STRINGS = TempDictionary;
            }
            else
            if (StringsSource == StringsSource.DL)
            {
                DLSTRINGS = TempDictionary;
            }
            else
            if (StringsSource == StringsSource.IL)
            {
                ILSTRINGS = TempDictionary;
            }
        }

        public void Save(string SavePath,string ModName, Languages To)
        {
            Language ToLang = ToMutagenLang(To);

            ModKey ModKey = ModName;

            GameRelease GameType = FindGameType();

            if (STRINGS.Count > 0)
            {
                StringsSource Source = StringsSource.DL;

                using (var Writer = new StringsWriter(GameType,ModKey, SavePath, MutagenEncoding.Default))
                {
                    TranslatedString TransItem = new TranslatedString(ToLang);
                    TransItem.String = "Test";
                    TransItem.Set(ToLang, "Test");

                    //Key?
                    uint key = Writer.Register(TransItem, Source);
                }
            }

            if (DLSTRINGS.Count > 0)
            { 
            
            }

            if (ILSTRINGS.Count > 0)
            {

            }
        }
    }
}