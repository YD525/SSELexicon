using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Binary.Parameters;
using Mutagen.Bethesda.Plugins.Binary.Streams;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Strings.DI;
using Noggog;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mutagen.Bethesda.Starfield.PhotoModeFeature;

namespace YDSkyrimToolR.SkyrimManage
{
    public class EspReader
    {
        public FileSystem GlobalFileSystem = null;

        public SkyrimMod? CurrentReadMod = null;

        public Dictionary<int,Armor> Armor = new Dictionary<int,Armor>();
        //...

        public EspReader()
        {
            GlobalFileSystem = new FileSystem();
        }

        private void ClearRam()
        {
            Armor.Clear();
        }

        private void ToRam()
        {
            ClearRam();

            if (CurrentReadMod != null)
            {
                foreach (var GetArmor in this.CurrentReadMod.Armors.ToList())
                {
                    int CreatHashID = (GetArmor.EditorID + GetArmor.FormKey).GetHashCode();
                    Armor.Add(CreatHashID, GetArmor);
                }
            }
        }

        public SkyrimMod? ReadMod(string FilePath, Mutagen.Bethesda.Strings.Language SetLanguage)
        {
            if (File.Exists(FilePath) && (FilePath.ToLower().EndsWith(".esp") || FilePath.ToLower().EndsWith(".esm")))
            {
                Cache<IModMasterStyledGetter, ModKey>? FlagsLookup = null;

                var SetParam = new BinaryReadParameters()
                {
                    StringsParam = new Mutagen.Bethesda.Strings.StringsReadParameters()
                    {
                        TargetLanguage = Mutagen.Bethesda.Strings.Language.ChineseSimplified
                    },
                    FileSystem = GlobalFileSystem,
                    MasterFlagsLookup = FlagsLookup
                };

                CurrentReadMod = SkyrimMod
                .CreateFromBinary(FilePath, SkyrimRelease.SkyrimSE, SetParam);

                ToRam();

                return CurrentReadMod;
            }

            return null;
        }

        public bool DefSaveMod(SkyrimMod SourceMod, string OutPutPath)
        {
            return SaveEspEsmFile(SourceMod, OutPutPath, new EncodingBundle(MutagenEncoding._utf8_1256, MutagenEncoding._utf8_1256));
        }

        private void ApplyRam()
        {
            if (CurrentReadMod == null)
            {
                return;
            }

            List<Armor> Armors = new List<Armor>();

            for (int i = 0; i < this.Armor.Count; i++)
            { 
               var GetKey = this.Armor.ElementAt(i).Key;
               var ToItem = this.Armor[GetKey];
               Armors.Add(ToItem);
            }

            if(Armors.Count>0 && Armors.Count.Equals(CurrentReadMod.Armors.Count))
            CurrentReadMod.Armors.Set(Armors);

        }

        public bool SaveMod(SkyrimMod SourceMod, string OutPutPath, EncodingBundle SetEncodingBundle)
        {
            if (CurrentReadMod == null)
            {
                return false;
            }
            if (File.Exists(OutPutPath))
            {
                return false;
            }

            try
            {
                ApplyRam();
                Task.Run(async () =>
                {
                    await SourceMod.BeginWrite.ToPath("C:\\Users\\Administrator\\Desktop\\TestPy\\AA\\3.esm")
                   .WithLoadOrderFromHeaderMasters()
                   .WithDefaultDataFolder()
                   .WithEmbeddedEncodings(SetEncodingBundle)
                   .WithFileSystem(GlobalFileSystem)
                   .WithRecordCount(RecordCountOption.Iterate)
                   .WithModKeySync(ModKeyOption.CorrectToPath)
                   .WithMastersListContent(MastersListContentOption.NoCheck)
                   .WithMastersListOrdering(MastersListOrderingOption.NoCheck)
                   .NoFormIDUniquenessCheck()
                   .NoFormIDCompactnessCheck()
                   .NoCheckIfLowerRangeDisallowed()
                   .NoNullFormIDStandardization()
                   .WriteAsync();

                }).Wait();

                return true;
            }
            catch { return false; }
        }

    }
}
