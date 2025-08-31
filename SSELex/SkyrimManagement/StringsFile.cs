using System.IO;
using Mutagen.Bethesda.Strings;
using Mutagen.Bethesda.Strings.DI;

namespace SSELex.SkyrimManagement
{
    //https://mutagen-modding.github.io/Mutagen/Strings/#simple-string-access

    //STRINGS DLSTRINGS ILSTRINGS
    public class StringsFile
    {
        public Dictionary<uint, string> STRINGS = new Dictionary<uint, string>();
        public Dictionary<uint, string> DLSTRINGS = new Dictionary<uint, string>();
        public Dictionary<uint, string> ILSTRINGS = new Dictionary<uint, string>();


        public void Close()
        {
            STRINGS.Clear();
            DLSTRINGS.Clear();
            ILSTRINGS.Clear();
        }

        public void Load(string Path)
        {
            if (!Path.EndsWith(@"\"))
            {
                Path+= @"\";
            }

            foreach (var GetFile in Directory.GetFiles(Path))
            {
                if (GetFile.ToLower().EndsWith(".dlstrings"))
                {
                    LoadSingleFile(GetFile, StringsSource.DL);
                }
                if (GetFile.ToLower().EndsWith(".ilstrings"))
                {
                    LoadSingleFile(GetFile, StringsSource.IL);
                }
                if (GetFile.ToLower().EndsWith(".strings"))
                {
                    LoadSingleFile(GetFile, StringsSource.Normal);
                }
            }
        }


        //C:\\Users\\52508\\Desktop\\TempFolder\\SkyrimVR strings-16355-1-0\\data\\strings\\skyrimvr_polish.dlstrings
        public void LoadSingleFile(string Path, StringsSource StringsSource)
        {
            Close();

            var StringsOverlay = new StringsLookupOverlay(Path, StringsSource, MutagenEncoding.GetEncoding(Mutagen.Bethesda.GameRelease.SkyrimSE,Language.Polish));

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

            if (StringsSource == StringsSource.DL)
            {
                DLSTRINGS = TempDictionary;
            }

            if (StringsSource == StringsSource.IL)
            {
                ILSTRINGS = TempDictionary;
            }
        }

        public void Save(string path)
        {

        }
    }
}