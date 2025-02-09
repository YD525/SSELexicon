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
using System.Windows;
using YDSkyrimToolR.ConvertManager;
using YDSkyrimToolR.UIManage;
/*
* @Author: 约定
* @GitHub: https://github.com/tolove336/YDSkyrimToolR
* @Date: 2025-02-06
*/
/*
* Module referenced from Mutagen
* @Author: Mutagen
* @GitHub: https://github.com/Mutagen-Modding/Mutagen
* @Date: 2025-02-06
*/
namespace YDSkyrimToolR.SkyrimManage
{
    public class EspReader
    {
        public FileSystem GlobalFileSystem = null;

        public SkyrimMod? CurrentReadMod = null;
        
        public Dictionary<int, Quest> Quests = new Dictionary<int, Quest>();
        public Dictionary<int, Faction> Factions = new Dictionary<int, Faction>();
        public Dictionary<int, Perk> Perks = new Dictionary<int, Perk>();
        public Dictionary<int, Weapon> Weapons = new Dictionary<int, Weapon>();
        public Dictionary<int, SoulGem> SoulGems = new Dictionary<int, SoulGem>();
        public Dictionary<int, Armor> Armors = new Dictionary<int, Armor>();
        public Dictionary<int, Key> Keys = new Dictionary<int, Key>();
        public Dictionary<int, Mutagen.Bethesda.Skyrim.Activator> Activators = new Dictionary<int, Mutagen.Bethesda.Skyrim.Activator>();
        public Dictionary<int, MiscItem> MiscItems = new Dictionary<int, MiscItem>();
        public Dictionary<int, Book> Books = new Dictionary<int, Book>();
        public Dictionary<int, Mutagen.Bethesda.Skyrim.Message> Messages = new Dictionary<int, Mutagen.Bethesda.Skyrim.Message>();
        public Dictionary<int, DialogTopic> DialogTopics = new Dictionary<int, DialogTopic>();
        public Dictionary<int, Spell> Spells = new Dictionary<int, Spell>();
        public Dictionary<int, MagicEffect> MagicEffects = new Dictionary<int, MagicEffect>();
        public Dictionary<int, ObjectEffect> ObjectEffects = new Dictionary<int, ObjectEffect>();
        public Dictionary<int, CellBlock> Cells = new Dictionary<int, CellBlock>();
        public Dictionary<int, Container> Containers = new Dictionary<int, Container>();
        
        //...

        public EspReader()
        {
            GlobalFileSystem = new FileSystem();
        }

        public void ClearRam()
        {
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
            UIHelper.ModifyCountCache.Clear();
            UIHelper.ModifyCount = 0;

            ClearRam();
            CurrentReadMod = null;
        }

        private void ToRam()
        {
            ClearRam();

            if (CurrentReadMod != null)
            {
                foreach (var Get in this.CurrentReadMod.Quests.ToList())
                {
                    if (Get.EditorID != null)
                    {
                        int CreatHashID = Get.EditorID.GetHashCode();
                        Quests.Add(CreatHashID, Get);
                    }
                }

                foreach (var Get in this.CurrentReadMod.Factions.ToList())
                {
                    if (Get.EditorID != null)
                    {
                        int CreatHashID = Get.EditorID.GetHashCode();
                        Factions.Add(CreatHashID, Get);
                    }
                }

                foreach (var Get in this.CurrentReadMod.Perks.ToList())
                {
                    if (Get.EditorID != null)
                    {
                        int CreatHashID = Get.EditorID.GetHashCode();
                        Perks.Add(CreatHashID, Get);
                    }
                }

                foreach (var Get in this.CurrentReadMod.Weapons.ToList())
                {
                    if (Get.EditorID != null)
                    {
                        int CreatHashID = Get.EditorID.GetHashCode();
                        Weapons.Add(CreatHashID, Get);
                    }
                }

                foreach (var Get in this.CurrentReadMod.SoulGems.ToList())
                {
                    if (Get.EditorID != null)
                    {
                        int CreatHashID = Get.EditorID.GetHashCode();
                        SoulGems.Add(CreatHashID, Get);
                    }
                }

                foreach (var Get in this.CurrentReadMod.Armors.ToList())
                {
                    if (Get.EditorID != null)
                    {
                        int CreatHashID = Get.EditorID.GetHashCode();
                        Armors.Add(CreatHashID, Get);
                    }
                }

                foreach (var Get in this.CurrentReadMod.Keys.ToList())
                {
                    if (Get.EditorID != null)
                    {
                        int CreatHashID = Get.EditorID.GetHashCode();
                        Keys.Add(CreatHashID, Get);
                    }
                }

                foreach (var Get in this.CurrentReadMod.Containers.ToList())
                {
                    if (Get.EditorID != null)
                    {
                        int CreatHashID = Get.EditorID.GetHashCode();
                        Containers.Add(CreatHashID, Get);
                    }
                }

                foreach (var Get in this.CurrentReadMod.Activators.ToList())
                {
                    if (Get.EditorID != null)
                    {
                        int CreatHashID = Get.EditorID.GetHashCode();
                        Activators.Add(CreatHashID, Get);
                    }    
                }

                foreach (var Get in this.CurrentReadMod.MiscItems.ToList())
                {
                    if (Get.EditorID != null)
                    {
                        int CreatHashID = Get.EditorID.GetHashCode();
                        MiscItems.Add(CreatHashID, Get);
                    }
                }

                foreach (var Get in this.CurrentReadMod.Books.ToList())
                {
                    if (Get.EditorID != null)
                    {
                        int CreatHashID = Get.EditorID.GetHashCode();
                        Books.Add(CreatHashID, Get);
                    } 
                }

                foreach (var Get in this.CurrentReadMod.Messages.ToList())
                {
                    if (Get.EditorID != null)
                    {
                        int CreatHashID = Get.EditorID.GetHashCode();
                        Messages.Add(CreatHashID, Get);
                    }
                }

                foreach (var Get in this.CurrentReadMod.DialogTopics.ToList())
                {
                    if (Get.EditorID != null)
                    {
                        int CreatHashID = Get.EditorID.GetHashCode();
                        DialogTopics.Add(CreatHashID, Get);
                    }
                }

                foreach (var Get in this.CurrentReadMod.Spells.ToList())
                {
                    if (Get.EditorID != null)
                    {
                        int CreatHashID = Get.EditorID.GetHashCode();
                        Spells.Add(CreatHashID, Get);
                    }
                }

                foreach (var Get in this.CurrentReadMod.MagicEffects.ToList())
                {
                    if (Get.EditorID != null)
                    {
                        int CreatHashID = Get.EditorID.GetHashCode();
                        MagicEffects.Add(CreatHashID, Get);
                    }
                }

                foreach (var Get in this.CurrentReadMod.ObjectEffects.ToList())
                {
                    if (Get.EditorID != null)
                    {
                        int CreatHashID = Get.EditorID.GetHashCode();
                        ObjectEffects.Add(CreatHashID, Get);
                    }
                }

                foreach (var Get in this.CurrentReadMod.Cells.ToList())
                {
                    Cells.Add(Get.GetHashCode(), Get);
                }

            }
        }

        public SkyrimMod? DefReadMod(string FilePath)
        {
           return ReadMod(FilePath, Mutagen.Bethesda.Strings.Language.ChineseSimplified);
        }
        public SkyrimMod? ReadMod(string FilePath, Mutagen.Bethesda.Strings.Language SetLanguage)
        {
            if (File.Exists(FilePath) && (FilePath.ToLower().EndsWith(".esp") || FilePath.ToLower().EndsWith(".esm") || FilePath.ToLower().EndsWith(".esl")))
            {
                YDSkyrimToolR.TranslateManage.Translator.ClearCache();
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

        public bool DefSaveMod(SkyrimMod SourceMod, string OutPutPath)
        {
            return SaveMod(SourceMod, OutPutPath, new EncodingBundle(MutagenEncoding._utf8_1256, MutagenEncoding._utf8_1256));
        }

        private void ApplyRam()
        {
            if (CurrentReadMod == null)
            {
                return;
            }

            List<Armor> Armors = new List<Armor>();

            for (int i = 0; i < this.Armors.Count; i++)
            { 
               var GetKey = this.Armors.ElementAt(i).Key;
               var ToItem = this.Armors[GetKey];
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

                foreach (var Item in SourceMod.EnumerateMajorRecords())
                {
                    Item.IsCompressed = false;
                }

                Task.Run(async () =>
                {
                    try { 
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
                    catch(Exception Ex) 
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
