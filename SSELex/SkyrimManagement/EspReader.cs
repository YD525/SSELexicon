using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Binary.Parameters;
using Mutagen.Bethesda.Plugins.Binary.Streams;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Strings.DI;
using Noggog;
using System.IO;
using System.IO.Abstractions;
using System.Windows;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Strings;
using Mutagen.Bethesda.Plugins.Exceptions;
using SSELex.SkyrimManagement;
using static PhoenixEngine.SSELexiconBridge.NativeBridge;
using SSELex.TranslateManage;
using PhoenixEngine.EngineManagement;

namespace SSELex.SkyrimManage
{
    public class EspReader
    {
        public StringsFileReader StringsReader = new StringsFileReader();

        public FileSystem GlobalFileSystem = null;

        public SkyrimMod? CurrentReadMod = null;
        public Dictionary<string, Hazard> Hazards = new Dictionary<string, Hazard>();
        public Dictionary<string, HeadPart> HeadParts = new Dictionary<string, HeadPart>();
        public Dictionary<string, Npc> Npcs = new Dictionary<string, Npc>();
        public Dictionary<string, Worldspace> Worldspaces = new Dictionary<string, Worldspace>();
        public Dictionary<string, Shout> Shouts = new Dictionary<string, Shout>();
        public Dictionary<string, Tree> Trees = new Dictionary<string, Tree>();
        public Dictionary<string, Ingestible> Ingestibles = new Dictionary<string, Ingestible>();
        public Dictionary<string, Race> Races = new Dictionary<string, Race>();
        public Dictionary<string, Quest> Quests = new Dictionary<string, Quest>();
        public Dictionary<string, Faction> Factions = new Dictionary<string, Faction>();
        public Dictionary<string, Perk> Perks = new Dictionary<string, Perk>();
        public Dictionary<string, Weapon> Weapons = new Dictionary<string, Weapon>();
        public Dictionary<string, SoulGem> SoulGems = new Dictionary<string, SoulGem>();
        public Dictionary<string, Armor> Armors = new Dictionary<string, Armor>();
        public Dictionary<string, Key> Keys = new Dictionary<string, Key>();
        public Dictionary<string, Mutagen.Bethesda.Skyrim.Activator> Activators = new Dictionary<string, Mutagen.Bethesda.Skyrim.Activator>();
        public Dictionary<string, MiscItem> MiscItems = new Dictionary<string, MiscItem>();
        public Dictionary<string, Book> Books = new Dictionary<string, Book>();
        public Dictionary<string, Mutagen.Bethesda.Skyrim.Message> Messages = new Dictionary<string, Mutagen.Bethesda.Skyrim.Message>();
        public Dictionary<string, DialogTopic> DialogTopics = new Dictionary<string, DialogTopic>();
        public Dictionary<string, Spell> Spells = new Dictionary<string, Spell>();
        public Dictionary<string, MagicEffect> MagicEffects = new Dictionary<string, MagicEffect>();
        public Dictionary<string, ObjectEffect> ObjectEffects = new Dictionary<string, ObjectEffect>();
        public Dictionary<string, CellBlock> Cells = new Dictionary<string, CellBlock>();
        public Dictionary<string, Container> Containers = new Dictionary<string, Container>();
        public Dictionary<string, Location> Locations = new Dictionary<string, Location>();

        //...

        public EspReader()
        {
            GlobalFileSystem = new FileSystem();
        }

        public void ClearRam()
        {
            Hazards.Clear();
            HeadParts.Clear();
            Npcs.Clear();
            Worldspaces.Clear();
            Shouts.Clear();
            Trees.Clear();
            Ingestibles.Clear();
            Races.Clear();
            Quests.Clear();
            Factions.Clear();
            Perks.Clear();
            Weapons.Clear();
            SoulGems.Clear();
            Armors.Clear();
            Keys.Clear();
            Activators.Clear();
            MiscItems.Clear();
            Books.Clear();
            Messages.Clear();
            DialogTopics.Clear();
            Spells.Clear();
            MagicEffects.Clear();
            ObjectEffects.Clear();
            Cells.Clear();
            Containers.Clear();
            Locations.Clear();
        }

        public void Close()
        {
            StringsReader.Close();
            TranslatorExtend.ClearTranslatorHistoryCache();

            ClearRam();
            CurrentReadMod = null;
        }

        private void ToRam()
        {
            ClearRam();

            if (CurrentReadMod != null)
            {
                foreach (var Get in this.CurrentReadMod.Hazards.ToList())
                {
                    if (Get != null)
                        Hazards.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.HeadParts.ToList())
                {
                    if (Get != null)
                        HeadParts.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Npcs.ToList())
                {
                    if (Get != null)
                        Npcs.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Worldspaces.ToList())
                {
                    if (Get != null)
                        Worldspaces.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Shouts.ToList())
                {
                    if (Get != null)
                        Shouts.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Trees.ToList())
                {
                    if (Get != null)
                        Trees.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Ingestibles.ToList())
                {
                    if (Get != null)
                        Ingestibles.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Races.ToList())
                {
                    if (Get != null)
                        Races.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Quests.ToList())
                {
                    if (Get != null)
                        Quests.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Factions.ToList())
                {
                    if (Get != null)
                        Factions.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Perks.ToList())
                {
                    if (Get != null)
                        Perks.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Weapons.ToList())
                {
                    if (Get != null)
                        Weapons.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.SoulGems.ToList())
                {
                    if (Get != null)
                        SoulGems.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Armors.ToList())
                {
                    if (Get != null)
                        Armors.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Keys.ToList())
                {
                    if (Get != null)
                        Keys.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Containers.ToList())
                {
                    if (Get != null)
                        Containers.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Activators.ToList())
                {
                    if (Get != null)
                        Activators.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.MiscItems.ToList())
                {
                    if (Get != null)
                        MiscItems.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Books.ToList())
                {
                    if (Get != null)
                        Books.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Messages.ToList())
                {
                    if (Get != null)
                        Messages.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.DialogTopics.ToList())
                {
                    if (Get != null)
                        DialogTopics.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Spells.ToList())
                {
                    if (Get != null)
                        Spells.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.MagicEffects.ToList())
                {
                    if (Get != null)
                        MagicEffects.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.ObjectEffects.ToList())
                {
                    if (Get != null)
                        ObjectEffects.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Cells.ToList())
                {
                    if (Get != null)
                        Cells.Add(KeyGenerator.GenKey(Get), Get);
                }

                foreach (var Get in this.CurrentReadMod.Locations.ToList())
                {
                    if (Get != null)
                        Locations.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }
            }
        }

        public SkyrimMod? DefReadMod(string FilePath)
        {
            TranslatorExtend.ClearTranslatorHistoryCache();
            return ReadMod(FilePath);
        }

        public enum EncodingTypes
        {
            UTF8_1256 = 0, UTF8_1252 = 1, UTF8_1250 = 2, UTF8_1253 = 3, UTF8 = 5
        }

        public IMutagenEncoding QueryEncoding()
        {
            var AutoEncoding = MutagenEncoding._utf8;

            if (DeFine.GlobalLocalSetting.FileEncoding == EncodingTypes.UTF8_1256)
            {
                AutoEncoding = MutagenEncoding._utf8_1256;
            }
            else
            if (DeFine.GlobalLocalSetting.FileEncoding == EncodingTypes.UTF8_1252)
            {
                AutoEncoding = MutagenEncoding._utf8_1252;
            }
            else
            if (DeFine.GlobalLocalSetting.FileEncoding == EncodingTypes.UTF8_1250)
            {
                AutoEncoding = MutagenEncoding._utf8_1250;
            }
            else
            if (DeFine.GlobalLocalSetting.FileEncoding == EncodingTypes.UTF8_1253)
            {
                AutoEncoding = MutagenEncoding._utf8_1253;
            }
            else
            if (DeFine.GlobalLocalSetting.FileEncoding == EncodingTypes.UTF8)
            {
                AutoEncoding = MutagenEncoding._utf8;
            }

            return AutoEncoding;
        }
        public SkyrimMod? ReadMod(string FilePath)
        {
            string GetPath = FilePath.Substring(0, FilePath.LastIndexOf(@"\"));
            StringsReader.SetModPath(GetPath);
            StringsReader.LoadStrings(Engine.To);

            if (File.Exists(FilePath) && (FilePath.ToLower().EndsWith(".esp") || FilePath.ToLower().EndsWith(".esm") || FilePath.ToLower().EndsWith(".esl")))
            {
                TranslatorBridge.ClearCache();
                Cache<IModMasterStyledGetter, ModKey>? FlagsLookup = null;

                var AutoEncoding = QueryEncoding();

                var SetParam = new BinaryReadParameters()
                {
                    StringsParam = new Mutagen.Bethesda.Strings.StringsReadParameters()
                    {
                        NonTranslatedEncodingOverride = AutoEncoding,
                        NonLocalizedEncodingOverride = AutoEncoding
                    },
                    FileSystem = GlobalFileSystem,
                    MasterFlagsLookup = FlagsLookup,
                    
                };
                try
                {
                    //var Mask = new GroupMask(false);
                    //var Mask = new GroupMask(true);
                    //Mask.AddonNodes = false;

                    //DNAM...

                    if (DeFine.GlobalLocalSetting.GameType == GameNames.SkyrimSE)
                    {
                        CurrentReadMod = SkyrimMod
                       .CreateFromBinary(FilePath, SkyrimRelease.SkyrimSE, SetParam);
                    }
                    else
                    if (DeFine.GlobalLocalSetting.GameType == GameNames.SkyrimLE)
                    {
                        CurrentReadMod = SkyrimMod
                        .CreateFromBinary(FilePath, SkyrimRelease.SkyrimLE, SetParam);
                    }
                }
                catch (RecordException rex)
                {
                    GC.Collect();
                }

                ToRam();
               
                //foreach (var Item in CurrentReadMod.EnumerateMajorRecords())
                //{
                //    //LinkType
                //    if (Item.FormKey.ToString().Contains("87F928"))
                //    {

                //    }
                //}

                return CurrentReadMod;
            }

            return null;
        }

        public bool DefSaveMod(SkyrimMod SourceMod, string OutPutPath)
        {
            var AutoEncoding = QueryEncoding();

            //IMutagenEncoding GetTargetLang = null;

            //if (DeFine.GlobalLocalSetting.SkyrimType == SkyrimType.SkyrimSE)
            //{
            //    GetTargetLang = MutagenEncoding.GetEncoding(GameRelease.SkyrimSE, GetLang(DeFine.GlobalLocalSetting.TargetLanguage));
            //}
            //else
            //{
            //    GetTargetLang = MutagenEncoding.GetEncoding(GameRelease.SkyrimLE, GetLang(DeFine.GlobalLocalSetting.TargetLanguage));
            //}

            return SaveMod(SourceMod, OutPutPath, new EncodingBundle(AutoEncoding, AutoEncoding));
        }

        private void ApplyRam()
        {
            if (CurrentReadMod == null)
            {
                return;
            }

            //List<Armor> Armors = new List<Armor>();

            //for (int i = 0; i < this.Armors.Count; i++)
            //{ 
            //   var GetKey = this.Armors.ElementAt(i).Key;
            //   var ToItem = this.Armors[GetKey];
            //   Armors.Add(ToItem);
            //}

            //if(Armors.Count>0 && Armors.Count.Equals(CurrentReadMod.Armors.Count))
            //CurrentReadMod.Armors.Set(Armors);

        }

        public bool SaveMod(SkyrimMod SourceMod, string OutPutPath, EncodingBundle SetEncodingBundle)
        {
            TranslatorExtend.ClearTranslatorHistoryCache();

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

                if (!DeFine.GlobalLocalSetting.AutoCompress)
                {
                    foreach (var Item in SourceMod.EnumerateMajorRecords())
                    {
                        Item.IsCompressed = false;
                    }
                }

                Task.Run(async () =>
                {
                    try
                    {
                        await SourceMod.BeginWrite.ToPath(OutPutPath)
                       .WithLoadOrderFromHeaderMasters()
                       .WithNoDataFolder()
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
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }).Wait();

                return true;
            }
            catch { return false; }
        }

    }
}
