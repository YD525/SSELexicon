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
using SSELex.UIManage;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Strings;
using Mutagen.Bethesda.Plugins.Exceptions;
using SSELex.SkyrimManagement;
using PhoenixEngine.TranslateCore;
using PhoenixEngine.SSELexiconBridge;
using static PhoenixEngine.SSELexiconBridge.NativeBridge;
using SSELex.UIManagement;
using SSELex.TranslateManage;

namespace SSELex.SkyrimManage
{
    public class EspReader
    {
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
        }

        public void Close()
        {
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
                    Hazards.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.HeadParts.ToList())
                {
                    HeadParts.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Npcs.ToList())
                {
                    Npcs.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Worldspaces.ToList())
                {
                    Worldspaces.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Shouts.ToList())
                {
                    Shouts.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Trees.ToList())
                {
                    Trees.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Ingestibles.ToList())
                {
                    Ingestibles.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Races.ToList())
                {
                    Races.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Quests.ToList())
                {
                    Quests.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Factions.ToList())
                {
                    Factions.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Perks.ToList())
                {
                    Perks.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Weapons.ToList())
                {
                    Weapons.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.SoulGems.ToList())
                {
                    SoulGems.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Armors.ToList())
                {
                    Armors.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Keys.ToList())
                {
                    Keys.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Containers.ToList())
                {
                    Containers.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Activators.ToList())
                {
                    Activators.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.MiscItems.ToList())
                {
                    MiscItems.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Books.ToList())
                {
                    Books.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Messages.ToList())
                {
                    Messages.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.DialogTopics.ToList())
                {
                    DialogTopics.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Spells.ToList())
                {
                    Spells.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.MagicEffects.ToList())
                {
                    MagicEffects.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.ObjectEffects.ToList())
                {
                    ObjectEffects.Add(KeyGenerator.GenKey(Get.FormKey, Get.EditorID), Get);
                }

                foreach (var Get in this.CurrentReadMod.Cells.ToList())
                {
                    if (Get != null)
                    {
                        Cells.Add(Get.GetHashCode().ToString(), Get);
                    }
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
                //    if (Item.FormKey.ToString().Contains("000809"))
                //    {

                //    }
                //}

                //foreach (var Get in this.CurrentReadMod.Keywords.ToList())
                //{

                //}

                //foreach (var Item in CurrentReadMod.EnumerateMajorRecords())
                //{
                //    if (Item.EditorID != null)
                //    {
                //        if (Item.EditorID.Contains("zadc_BondagePole"))
                //        { 

                //        }

                //        if (Item is ActionRecord)
                //        {

                //        }
                //        if (Item is TextureSet)
                //        {

                //        }
                //        if (Item is Global)
                //        {

                //        }
                //        if (Item is Class)
                //        {

                //        }
                //        if (Item is Faction)
                //        {

                //        }
                //        if (Item is HeadPart)
                //        {

                //        }
                //        if (Item is Hair)
                //        {

                //        }
                //        if (Item is Eyes)
                //        {

                //        }
                //        if (Item is Race)
                //        {

                //        }
                //        if (Item is SoundMarker)
                //        {

                //        }
                //        if (Item is AcousticSpace)
                //        {

                //        }
                //        if (Item is MagicEffect)
                //        {

                //        }
                //        if (Item is LandscapeTexture)
                //        {

                //        }
                //        if (Item is ObjectEffect)
                //        {

                //        }
                //        if (Item is Spell)
                //        {

                //        }
                //        if (Item is Scroll)
                //        {

                //        }
                //        if (Item is TalkingActivator)
                //        {

                //        }
                //        if (Item is Door)
                //        {

                //        }
                //        if (Item is Ingredient)
                //        {

                //        }
                //        if (Item is Light)
                //        {

                //        }
                //        if (Item is AlchemicalApparatus)
                //        {

                //        }
                //        if (Item is Static)
                //        {

                //        }
                //        if (Item is MoveableStatic)
                //        {

                //        }
                //        if (Item is Grass)
                //        {

                //        }
                //        if (Item is Tree)
                //        {

                //        }
                //        if (Item is Flora)
                //        {

                //        }
                //        if (Item is Furniture)
                //        {

                //        }
                //        if (Item is Weapon)
                //        {

                //        }
                //        if (Item is Ammunition)
                //        {

                //        }
                //        if (Item is Npc)
                //        {

                //        }
                //        if (Item is LeveledNpc)
                //        {

                //        }
                //        if (Item is Ingestible)
                //        {

                //        }
                //        if (Item is IdleMarker)
                //        {

                //        }
                //        if (Item is ConstructibleObject)
                //        {

                //        }
                //        if (Item is Projectile)
                //        {

                //        }
                //        if (Item is Hazard)
                //        {

                //        }
                //        if (Item is LeveledItem)
                //        {

                //        }
                //        if (Item is Weather)
                //        {

                //        }
                //        if (Item is Climate)
                //        {

                //        }
                //        if (Item is ShaderParticleGeometry)
                //        {

                //        }
                //        if (Item is VisualEffect)
                //        {

                //        }
                //        if (Item is Region)
                //        {

                //        }
                //        if (Item is NavigationMeshInfoMap)
                //        {

                //        }
                //        if (Item is CellBlock)
                //        {

                //        }
                //        if (Item is Worldspace)
                //        {

                //        }
                //        if (Item is IdleAnimation)
                //        {

                //        }
                //        if (Item is Package)
                //        {

                //        }
                //        if (Item is CombatStyle)
                //        {

                //        }
                //        if (Item is LoadScreen)
                //        {

                //        }
                //        if (Item is LeveledSpell)
                //        {

                //        }
                //        if (Item is AnimatedObject)
                //        {

                //        }
                //        if (Item is Water)
                //        {

                //        }
                //        if (Item is EffectShader)
                //        {

                //        }
                //        if (Item is Explosion)
                //        {

                //        }
                //        if (Item is Debris)
                //        {

                //        }
                //        if (Item is ImageSpace)
                //        {

                //        }
                //        if (Item is ImageSpaceAdapter)
                //        {

                //        }
                //        if (Item is FormList)
                //        {

                //        }
                //        if (Item is BodyPartData)
                //        {

                //        }
                //        if (Item is AddonNode)
                //        {

                //        }
                //        if (Item is ActorValueInformation)
                //        {

                //        }
                //        if (Item is CameraShot)
                //        {

                //        }
                //        if (Item is CameraPath)
                //        {

                //        }
                //        if (Item is VoiceType)
                //        {

                //        }
                //        if (Item is MaterialType)
                //        {

                //        }
                //        if (Item is Impact)
                //        {

                //        }
                //        if (Item is ImpactDataSet)
                //        {

                //        }
                //        if (Item is ArmorAddon)
                //        {

                //        }
                //        if (Item is EncounterZone)
                //        {

                //        }
                //        if (Item is Location)
                //        {

                //        }
                //        if (Item is DefaultObjectManager)
                //        {

                //        }
                //        if (Item is LightingTemplate)
                //        {

                //        }
                //        if (Item is MusicType)
                //        {

                //        }
                //        if (Item is Footstep)
                //        {

                //        }
                //        if (Item is FootstepSet)
                //        {

                //        }
                //        if (Item is StoryManagerBranchNode)
                //        {

                //        }
                //        if (Item is StoryManagerQuestNode)
                //        {

                //        }
                //        if (Item is StoryManagerEventNode)
                //        {

                //        }
                //        if (Item is DialogBranch)
                //        {

                //        }
                //        if (Item is MusicTrack)
                //        {

                //        }
                //        if (Item is DialogView)
                //        {

                //        }
                //        if (Item is WordOfPower)
                //        {

                //        }
                //        if (Item is Shout)
                //        {

                //        }
                //        if (Item is EquipType)
                //        {

                //        }
                //        if (Item is Relationship)
                //        {

                //        }
                //        if (Item is Scene)
                //        {

                //        }
                //        if (Item is AssociationType)
                //        {

                //        }
                //        if (Item is Outfit)
                //        {

                //        }
                //        if (Item is ArtObject)
                //        {

                //        }
                //        if (Item is MaterialObject)
                //        {

                //        }
                //        if (Item is MovementType)
                //        {

                //        }
                //        if (Item is SoundDescriptor)
                //        {

                //        }
                //        if (Item is DualCastData)
                //        {

                //        }
                //        if (Item is SoundCategory)
                //        {

                //        }
                //        if (Item is SoundOutputModel)
                //        {

                //        }
                //        if (Item is CollisionLayer)
                //        {

                //        }
                //        if (Item is ColorRecord)
                //        {

                //        }
                //        if (Item is ReverbParameters)
                //        {

                //        }
                //        if (Item is VolumetricLighting)
                //        {

                //        }
                //        if (Item is LensFlare)
                //        {

                //        }
                //    }
                //}

                return CurrentReadMod;
            }

            return null;
        }

        public Language GetLang(Languages Lang)
        {
            if (Lang == Languages.SimplifiedChinese)
            {
                return Language.ChineseSimplified;
            }

            if (Lang == Languages.TraditionalChinese)
            {
                return Language.Chinese;
            }

            if (Lang == Languages.English)
            {
                return Language.English;
            }

            if (Lang == Languages.Turkish)
            {
                return Language.Turkish;
            }

            if (Lang == Languages.Japanese)
            {
                return Language.Japanese;
            }

            if (Lang == Languages.Brazilian)
            {
                return Language.Portuguese_Brazil;
            }

            if (Lang == Languages.Korean)
            {
                return Language.Korean;
            }

            if (Lang == Languages.Russian)
            {
                return Language.Russian;
            }

            if (Lang == Languages.German)
            {
                return Language.German;
            }

            return Language.English;
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
