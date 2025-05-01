using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Masters;
using Mutagen.Bethesda.Plugins.Records;
using NexusMods.Paths.Trees;
using NexusMods.Paths.Trees.Traits;
using OneOf.Types;
using SSELex.ConvertManager;
using SSELex.SkyrimManage;
using SSELex.TranslateManage;
using SSELex.UIManage;

namespace SSELex.SkyrimManagement
{
    public class DSDItem
    {
        public string editor_id { get; set; } = "";
        public string form_id { get; set; } = "";
        public int index { get; set; } = 1;
        public string type { get; set; } = "";
        public string original { get; set; } = "";
        public string @string { get; set; } = "";
    }

    public class DSDFile
    {
        public List<DSDItem> DSDItems = new List<DSDItem>();
    }
    public class SkyrimDataDSDConvert
    {
        public static string GetTransData(string EditorID, string SetType)
        {
            string GetKey = SkyrimDataLoader.GenUniqueKey(EditorID, SetType);
            if (Translator.TransData.ContainsKey(GetKey))
            {
                return Translator.TransData[GetKey];
            }
            else
            {
                return string.Empty;
            }
        }


        public static List<DSDItem> EspExportAllByDSD(EspReader Reader)
        {
            List<DSDItem> Data = new List<DSDItem>();
            Data.AddRange(GetWorldspaces(Reader));
            Data.AddRange(GetQuests(Reader));
            Data.AddRange(GetFactions(Reader));
            Data.AddRange(GetPerks(Reader));
            Data.AddRange(GetWeapons(Reader));
            Data.AddRange(GetSoulGems(Reader));
            Data.AddRange(GetArmors(Reader));
            Data.AddRange(GetKeys(Reader));
            Data.AddRange(GetContainers(Reader));
            Data.AddRange(GetActivators(Reader));
            Data.AddRange(GetMiscItems(Reader));
            Data.AddRange(GetBooks(Reader));
            Data.AddRange(GetMessages(Reader));
            Data.AddRange(GetMessageButtons(Reader));
            Data.AddRange(GetDialogTopics(Reader));
            Data.AddRange(GetSpells(Reader));
            Data.AddRange(GetMagicEffects(Reader));
            Data.AddRange(GetObjectEffects(Reader));
            Data.AddRange(GetCells(Reader));
            return Data;
        }

        public static string IntToHex(int num)
        {
            return (num & 0xFF).ToString("X2");
        }
        public static int AutoNumber = 0;

        public static string GenFormID(FormKey Key)
        {
            //An example FormKey string might be 123456:Skyrim.esm
            //The numbers are the last 6 digits of a FormID(with no mod index), followed by the string name of the mod that originally defined it
            string GetKey = Key.ToString();
            if (GetKey.Contains(":"))
            {
                string GetFormID = GetKey.Split(':')[0];
                string GetModName = GetKey.Split(":")[1];
                //The first two digits are the index
                return GenFormID(GetFormID, GetModName);
            }
            return "";
        }
        public static string GenFormID(string FormID,string ModName)
        {
            string PaddedID = FormID.PadLeft(8, '0');
            return $"{PaddedID}|{ModName}";
        }

        public static List<DSDItem> GetWorldspaces(EspReader Writer)
        {
            List<DSDItem> DSDItems = new List<DSDItem>();
            string SetType = "";
            for (int i = 0; i < Writer.Worldspaces.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Writer.Worldspaces.ElementAt(i).Key;
                var GetWorldspaceItem = Writer.Worldspaces[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetWorldspaceItem.Name);

                SetType = "Name";
                GetTransStr = GetTransData(GetWorldspaceItem.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = GetWorldspaceItem.EditorID;
                    NDSDItem.form_id = GenFormID(GetWorldspaceItem.FormKey);
                    NDSDItem.type = "WRLD FULL";
                    NDSDItem.original = GetName;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //GetWorldspaceItem.Name = GetTransStr;
                }
            }
            return DSDItems;
        }

        public static List<DSDItem> GetQuests(EspReader Reader)
        {
            List<DSDItem> DSDItems = new List<DSDItem>();
            string SetType = "";
            for (int i = 0; i < Reader.Quests.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Quests.ElementAt(i).Key;
                var GetQuestItem = Reader.Quests[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetQuestItem.Name);

                SetType = "Name";
                GetTransStr = GetTransData(GetQuestItem.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = GetQuestItem.EditorID;
                    NDSDItem.form_id = GenFormID(GetQuestItem.FormKey);
                    NDSDItem.type = "QUST FULL";
                    NDSDItem.original = GetName;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //GetQuestItem.Name = GetTransStr;
                }

                var GetDescription = ConvertHelper.ObjToStr(GetQuestItem.Description);

                SetType = "Description";
                GetTransStr = GetTransData(GetQuestItem.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = GetQuestItem.EditorID;
                    NDSDItem.form_id = GenFormID(GetQuestItem.FormKey);
                    NDSDItem.type = "QUST DESC";
                    NDSDItem.original = GetDescription;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //GetQuestItem.Description = GetTransStr;
                }

                if (GetQuestItem.Objectives.Count > 0)
                {
                    int CountObjective = 0;
                    for (int ir = 0; ir < GetQuestItem.Objectives.Count; ir++)
                    {
                        CountObjective++;
                        var GetObjectiveItem = GetQuestItem.Objectives[ir];
                        var GetDisplayText = ConvertHelper.ObjToStr(GetObjectiveItem.DisplayText);
                        if (GetDisplayText.Length > 0)
                        {
                            SetType = string.Format("DisplayText[{0}]", CountObjective);
                            GetTransStr = GetTransData(GetQuestItem.EditorID, SetType);
                            if (GetTransStr.Trim().Length > 0)
                            {
                                DSDItem NDSDItem = new DSDItem();
                                NDSDItem.editor_id = GetQuestItem.EditorID;
                                NDSDItem.form_id = GenFormID(GetQuestItem.FormKey);
                                NDSDItem.type = "QUST NNAM";
                                NDSDItem.original = GetDisplayText;
                                NDSDItem.@string = GetTransStr;
                                DSDItems.Add(NDSDItem);
                                //GetQuestItem.Objectives[ir].DisplayText = GetTransStr;
                            }
                        }
                    }
                }

                if (GetQuestItem.Stages.Count > 0)
                {
                    int CountStage = 0;
                    for (int ii = 0; ii < GetQuestItem.Stages.Count; ii++)
                    {
                        CountStage++;
                        for (int iii = 0; iii < GetQuestItem.Stages[ii].LogEntries.Count; iii++)
                        {
                            CountStage++;
                            var GetLogEntrieItem = GetQuestItem.Stages[ii].LogEntries[iii];

                            var GetEntry = ConvertHelper.ObjToStr(GetLogEntrieItem.Entry);
                            if (GetEntry.Length > 0)
                            {
                                SetType = string.Format("Entry[{0}]", CountStage);
                                GetTransStr = GetTransData(GetQuestItem.EditorID, SetType);
                                if (GetTransStr.Trim().Length > 0)
                                {

                                    DSDItem NDSDItem = new DSDItem();
                                    NDSDItem.editor_id = GetQuestItem.EditorID;
                                    NDSDItem.form_id = GenFormID(GetQuestItem.FormKey);
                                    NDSDItem.type = "QUST CNAM";
                                    NDSDItem.original = GetEntry;
                                    NDSDItem.@string = GetTransStr;
                                    DSDItems.Add(NDSDItem);
                                    //GetQuestItem.Stages[ii].LogEntries[iii].Entry = GetTransStr;
                                }
                            }
                        }
                    }
                }
            }
            return DSDItems;
        }

        public static List<DSDItem> GetFactions(EspReader Reader)
        {
            List<DSDItem> DSDItems = new List<DSDItem>();
            string SetType = "";
            for (int i = 0; i < Reader.Factions.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Factions.ElementAt(i).Key;
                var GetFactionItem = Reader.Factions[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetFactionItem.Name);

                SetType = "Name";
                GetTransStr = GetTransData(GetFactionItem.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = GetFactionItem.EditorID;
                    NDSDItem.form_id = GenFormID(GetFactionItem.FormKey);
                    NDSDItem.type = "FACT FULL";
                    NDSDItem.original = GetName;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //GetFactionItem.Name = GetTransStr;
                }

                if (GetFactionItem.Ranks.Count > 0)
                {
                    int CountRank = 0;
                    if (GetFactionItem.Ranks != null)
                        for (int ii = 0; ii < GetFactionItem.Ranks.Count; ii++)
                        {
                            CountRank++;
                            string GetFemale = ConvertHelper.ObjToStr(GetFactionItem.Ranks[ii].Title.Female);
                            string GetMale = ConvertHelper.ObjToStr(GetFactionItem.Ranks[ii].Title.Male);

                            SetType = string.Format("Female[{0}]", CountRank);
                            GetTransStr = GetTransData(GetFactionItem.EditorID, SetType);
                            if (GetTransStr.Trim().Length > 0)
                            {
                                DSDItem NDSDItem = new DSDItem();
                                NDSDItem.editor_id = GetFactionItem.EditorID;
                                NDSDItem.form_id = GenFormID(GetFactionItem.FormKey);
                                NDSDItem.type = "FACT FNAM";
                                NDSDItem.original = GetFemale;
                                NDSDItem.@string = GetTransStr;
                                DSDItems.Add(NDSDItem);
                                //GetFactionItem.Ranks[ii].Title.Female = GetTransStr;
                            }

                            SetType = string.Format("Male[{0}]", CountRank);
                            GetTransStr = GetTransData(GetFactionItem.EditorID, SetType);
                            if (GetTransStr.Trim().Length > 0)
                            {
                                DSDItem NDSDItem = new DSDItem();
                                NDSDItem.editor_id = GetFactionItem.EditorID;
                                NDSDItem.form_id = GenFormID(GetFactionItem.FormKey);
                                NDSDItem.type = "FACT MNAM";
                                NDSDItem.original = GetMale;
                                NDSDItem.@string = GetTransStr;
                                DSDItems.Add(NDSDItem);
                                //GetFactionItem.Ranks[ii].Title.Male = GetTransStr;
                            }
                        }
                }
            }
            return DSDItems;
        }

        public static List<DSDItem> GetPerks(EspReader Reader)
        {
            List<DSDItem> DSDItems = new List<DSDItem>();
            string SetType = "";
            for (int i = 0; i < Reader.Perks.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Perks.ElementAt(i).Key;
                var GetPerkItem = Reader.Perks[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetPerkItem.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetPerkItem.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = GetPerkItem.EditorID;
                    NDSDItem.form_id = GenFormID(GetPerkItem.FormKey);
                    NDSDItem.type = "PERK FULL";
                    NDSDItem.original = GetName;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //GetPerkItem.Name = GetTransStr;
                }

                var GetDescription = ConvertHelper.ObjToStr(GetPerkItem.Description);
                SetType = "Description";
                GetTransStr = GetTransData(GetPerkItem.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = GetPerkItem.EditorID;
                    NDSDItem.form_id = GenFormID(GetPerkItem.FormKey);
                    NDSDItem.type = "PERK DESC";
                    NDSDItem.original = GetDescription;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //GetPerkItem.Description = GetTransStr;
                }
            }
            return DSDItems;
        }

        public static List<DSDItem> GetWeapons(EspReader Reader)
        {
            List<DSDItem> DSDItems = new List<DSDItem>();
            string SetType = "";
            for (int i = 0; i < Reader.Weapons.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Weapons.ElementAt(i).Key;
                var GetWeapon = Reader.Weapons[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetWeapon.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetWeapon.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = GetWeapon.EditorID;
                    NDSDItem.form_id = GenFormID(GetWeapon.FormKey);
                    NDSDItem.type = "WEAP FULL";
                    NDSDItem.original = GetName;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //GetWeapon.Name = GetTransStr;
                }

                var GetDescription = ConvertHelper.ObjToStr(GetWeapon.Description);
                SetType = "Description";
                GetTransStr = GetTransData(GetWeapon.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = GetWeapon.EditorID;
                    NDSDItem.form_id = GenFormID(GetWeapon.FormKey);
                    NDSDItem.type = "WEAP DESC";
                    NDSDItem.original = GetDescription;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //GetWeapon.Description = GetTransStr;
                }
            }
            return DSDItems;
        }

        public static List<DSDItem> GetSoulGems(EspReader Reader)
        {
            List<DSDItem> DSDItems = new List<DSDItem>();
            string SetType = "";
            for (int i = 0; i < Reader.SoulGems.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.SoulGems.ElementAt(i).Key;
                var GetSoulGem = Reader.SoulGems[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetSoulGem.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetSoulGem.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = GetSoulGem.EditorID;
                    NDSDItem.form_id = GenFormID(GetSoulGem.FormKey);
                    NDSDItem.type = "SLGM FULL";
                    NDSDItem.original = GetName;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //GetSoulGem.Name = GetTransStr;
                }
            }
            return DSDItems;
        }

        public static List<DSDItem> GetArmors(EspReader Reader)
        {
            List<DSDItem> DSDItems = new List<DSDItem>();
            string SetType = "";
            for (int i = 0; i < Reader.Armors.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Armors.ElementAt(i).Key;
                var GetArmor = Reader.Armors[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetArmor.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetArmor.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = GetArmor.EditorID;
                    NDSDItem.form_id = GenFormID(GetArmor.FormKey);
                    NDSDItem.type = "ARMO FULL";
                    NDSDItem.original = GetName;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //GetArmor.Name = GetTransStr;
                }

                var GetDescription = ConvertHelper.ObjToStr(GetArmor.Description);
                SetType = "Description";
                GetTransStr = GetTransData(GetArmor.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = GetArmor.EditorID;
                    NDSDItem.form_id = GenFormID(GetArmor.FormKey);
                    NDSDItem.type = "ARMO DESC";
                    NDSDItem.original = GetDescription;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //GetArmor.Description = GetTransStr;
                }
            }
            return DSDItems;
        }

        public static List<DSDItem> GetKeys(EspReader Reader)
        {
            List<DSDItem> DSDItems = new List<DSDItem>();
            string SetType = "";
            for (int i = 0; i < Reader.Keys.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Keys.ElementAt(i).Key;
                var GetKey = Reader.Keys[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetKey.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetKey.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = GetKey.EditorID;
                    NDSDItem.form_id = GenFormID(GetKey.FormKey);
                    NDSDItem.type = "KEYM FULL";
                    NDSDItem.original = GetName;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //GetKey.Name = GetTransStr;
                }
            }
            return DSDItems;
        }

        public static List<DSDItem> GetContainers(EspReader Reader)
        {
            List<DSDItem> DSDItems = new List<DSDItem>();
            string SetType = "";
            for (int i = 0; i < Reader.Containers.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Containers.ElementAt(i).Key;
                var GetContainer = Reader.Containers[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetContainer.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetContainer.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = GetContainer.EditorID;
                    NDSDItem.form_id = GenFormID(GetContainer.FormKey);
                    NDSDItem.type = "CONT FULL";
                    NDSDItem.original = GetName;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //GetContainer.Name = GetTransStr;
                }
            }
            return DSDItems;
        }

        public static List<DSDItem> GetActivators(EspReader Reader)
        {
            List<DSDItem> DSDItems = new List<DSDItem>();
            string SetType = "";
            for (int i = 0; i < Reader.Activators.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Activators.ElementAt(i).Key;
                var GetActivator = Reader.Activators[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetActivator.Name);
                SetType = "Name" + GetActivator;
                GetTransStr = GetTransData(GetActivator.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = GetActivator.EditorID;
                    NDSDItem.form_id = GenFormID(GetActivator.FormKey);
                    NDSDItem.type = "ACTI FULL";
                    NDSDItem.original = GetName;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //GetActivator.Name = GetTransStr;
                }

                var GetActivateTextOverride = ConvertHelper.ObjToStr(GetActivator.ActivateTextOverride);
                if (GetActivateTextOverride.Trim().Length > 0)
                {
                    SetType = "ActivateTextOverride";
                    GetTransStr = GetTransData(GetActivator.EditorID, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        DSDItem NDSDItem = new DSDItem();
                        NDSDItem.editor_id = GetActivator.EditorID;
                        NDSDItem.form_id = GenFormID(GetActivator.FormKey);
                        NDSDItem.type = "ACTI RNAM";
                        NDSDItem.original = GetActivateTextOverride;
                        NDSDItem.@string = GetTransStr;
                        DSDItems.Add(NDSDItem);
                        //GetActivator.ActivateTextOverride = GetTransStr;
                    }
                }
            }
            return DSDItems;
        }

        public static List<DSDItem> GetMiscItems(EspReader Reader)
        {
            List<DSDItem> DSDItems = new List<DSDItem>();
            string SetType = "";
            for (int i = 0; i < Reader.MiscItems.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.MiscItems.ElementAt(i).Key;
                var GetMiscItem = Reader.MiscItems[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetMiscItem.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetMiscItem.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = GetMiscItem.EditorID;
                    NDSDItem.form_id = GenFormID(GetMiscItem.FormKey);
                    NDSDItem.type = "MISC FULL";
                    NDSDItem.original = GetName;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //GetMiscItem.Name = GetTransStr;
                }
            }
            return DSDItems;
        }

        public static List<DSDItem> GetBooks(EspReader Reader)
        {
            List<DSDItem> DSDItems = new List<DSDItem>();
            string SetType = "";
            for (int i = 0; i < Reader.Books.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Books.ElementAt(i).Key;
                var Books = Reader.Books[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(Books.Name);
                SetType = "Name";
                GetTransStr = GetTransData(Books.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = Books.EditorID;
                    NDSDItem.form_id = GenFormID(Books.FormKey);
                    NDSDItem.type = "BOOK FULL";
                    NDSDItem.original = GetName;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //Books.Name = GetTransStr;
                }

                var GetDescription = ConvertHelper.ObjToStr(Books.Description);
                SetType = "Description";
                GetTransStr = GetTransData(Books.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = Books.EditorID;
                    NDSDItem.form_id = GenFormID(Books.FormKey);
                    NDSDItem.type = "BOOK CNAM";
                    NDSDItem.original = GetDescription;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //Books.Description = GetTransStr;
                }

                var GetBookText = ConvertHelper.ObjToStr(Books.BookText);
                SetType = "BookText";
                GetTransStr = GetTransData(Books.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = Books.EditorID;
                    NDSDItem.form_id = GenFormID(Books.FormKey);
                    NDSDItem.type = "BOOK DESC";
                    NDSDItem.original = GetBookText;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //Books.BookText = GetTransStr;
                }
            }
            return DSDItems;
        }

        public static List<DSDItem> GetMessages(EspReader Reader)
        {
            List<DSDItem> DSDItems = new List<DSDItem>();
            string SetType = "Name";
            for (int i = 0; i < Reader.Messages.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Messages.ElementAt(i).Key;
                var GetMessageItem = Reader.Messages[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetMessageItem.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetMessageItem.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = GetMessageItem.EditorID;
                    NDSDItem.form_id = GenFormID(GetMessageItem.FormKey);
                    NDSDItem.type = "MESG FULL";
                    NDSDItem.original = GetName;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //GetMessageItem.Name = GetTransStr;
                }

                var GetDescription = ConvertHelper.ObjToStr(GetMessageItem.Description);
                SetType = "Description";
                GetTransStr = GetTransData(GetMessageItem.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = GetMessageItem.EditorID;
                    NDSDItem.form_id = GenFormID(GetMessageItem.FormKey);
                    NDSDItem.type = "MESG DESC";
                    NDSDItem.original = GetDescription;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //GetMessageItem.Description = GetTransStr;
                }
            }
            return DSDItems;
        }

        public static List<DSDItem> GetMessageButtons(EspReader Reader)
        {
            List<DSDItem> DSDItems = new List<DSDItem>();
            string SetType = "";
            for (int i = 0; i < Reader.Messages.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Messages.ElementAt(i).Key;
                var GetMessageItem = Reader.Messages[GetHashKey];

                if (GetMessageItem.MenuButtons != null)
                {
                    if (GetMessageItem.MenuButtons.Count > 0)
                    {
                        for (int ir = 0; ir < GetMessageItem.MenuButtons.Count; ir++)
                        {
                            var GetButton = ConvertHelper.ObjToStr(GetMessageItem.MenuButtons[ir].Text);
                            SetType = string.Format("Button[{0}]", ir);
                            GetTransStr = GetTransData(GetMessageItem.EditorID, SetType);
                            if (GetTransStr.Length > 0)
                            {
                                //??????????????????????????
                                DSDItem NDSDItem = new DSDItem();
                                NDSDItem.editor_id = GetMessageItem.EditorID;
                                NDSDItem.form_id = GenFormID(GetMessageItem.FormKey);
                                NDSDItem.type = "MESG ITXT";
                                NDSDItem.original = GetButton;
                                NDSDItem.@string = GetTransStr;
                                DSDItems.Add(NDSDItem);
                                //GetMessageItem.MenuButtons[ir].Text = GetTransStr;
                            }
                        }
                    }
                }
            }
            return DSDItems;
        }

        public static List<DSDItem> GetDialogTopics(EspReader Reader)
        {
            List<DSDItem> DSDItems = new List<DSDItem>();
            string SetType = "";
            for (int i = 0; i < Reader.DialogTopics.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.DialogTopics.ElementAt(i).Key;
                var GetDialogTopicItem = Reader.DialogTopics[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetDialogTopicItem.Name);
                if (GetName.Length > 0)
                {
                    SetType = "Name";
                    GetTransStr = GetTransData(GetDialogTopicItem.EditorID, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        DSDItem NDSDItem = new DSDItem();
                        NDSDItem.editor_id = GetDialogTopicItem.EditorID;
                        NDSDItem.form_id = GenFormID(GetDialogTopicItem.FormKey);
                        NDSDItem.type = "DIAL FULL";
                        NDSDItem.original = GetName;
                        NDSDItem.@string = GetTransStr;
                        DSDItems.Add(NDSDItem);
                        //GetDialogTopicItem.Name = GetTransStr;
                    }
                }

                int ForCount = 0;
                if (GetDialogTopicItem.Responses != null)
                    for (int ii = 0; ii < GetDialogTopicItem.Responses.Count; ii++)
                    {
                        ForCount++;
                        if (GetDialogTopicItem.Responses[ii].Responses != null)
                            for (int iii = 0; iii < GetDialogTopicItem.Responses[ii].Responses.Count; iii++)
                            {
                                ForCount++;

                                string GetValue = ConvertHelper.ObjToStr(GetDialogTopicItem.Responses[ii].Responses[iii].Text);
                                SetType = string.Format("Response[{0}]", ForCount);
                                GetTransStr = GetTransData(GetDialogTopicItem.EditorID, SetType);
                                if (GetTransStr.Length > 0)
                                {
                                    //??????? DIAL INFO NAM1 or INFO NAM1...
                                    DSDItem NDSDItem = new DSDItem();
                                    NDSDItem.editor_id = GetDialogTopicItem.EditorID;
                                    NDSDItem.form_id = GenFormID(GetDialogTopicItem.FormKey);
                                    NDSDItem.type = "INFO NAM1";
                                    NDSDItem.original = GetValue;
                                    NDSDItem.@string = GetTransStr;
                                    DSDItems.Add(NDSDItem);
                                    //GetDialogTopicItem.Responses[ii].Responses[iii].Text = GetTransStr;
                                }
                            }
                    }
            }
            return DSDItems;
        }

        public static List<DSDItem> GetSpells(EspReader Reader)
        {
            List<DSDItem> DSDItems = new List<DSDItem>();
            string SetType = "";
            for (int i = 0; i < Reader.Spells.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Spells.ElementAt(i).Key;
                var GetSpellItem = Reader.Spells[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetSpellItem.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetSpellItem.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = GetSpellItem.EditorID;
                    NDSDItem.form_id = GenFormID(GetSpellItem.FormKey);
                    NDSDItem.type = "SPEL FULL";
                    NDSDItem.original = GetName;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //GetSpellItem.Name = GetTransStr;
                }
            }
            return DSDItems;
        }

        //ENCH
        public static List<DSDItem> GetObjectEffects(EspReader Reader)
        {
            List<DSDItem> DSDItems = new List<DSDItem>();
            string SetType = "";
            for (int i = 0; i < Reader.ObjectEffects.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.ObjectEffects.ElementAt(i).Key;
                var GetObjectEffect = Reader.ObjectEffects[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetObjectEffect.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetObjectEffect.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    //??????????????? Not Found ENCH
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = GetObjectEffect.EditorID;
                    NDSDItem.form_id = GenFormID(GetObjectEffect.FormKey);
                    NDSDItem.type = "ENCH FULL";
                    NDSDItem.original = GetName;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //GetObjectEffect.Name = GetTransStr;
                }
            }
            return DSDItems;
        }

        public static List<DSDItem> GetMagicEffects(EspReader Reader)
        {
            List<DSDItem> DSDItems = new List<DSDItem>();
            string SetType = "";
            for (int i = 0; i < Reader.MagicEffects.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.MagicEffects.ElementAt(i).Key;
                var GetMagicEffect = Reader.MagicEffects[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetMagicEffect.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetMagicEffect.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = GetMagicEffect.EditorID;
                    NDSDItem.form_id = GenFormID(GetMagicEffect.FormKey);
                    NDSDItem.type = "MGEF FULL";
                    NDSDItem.original = GetName;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //GetMagicEffect.Name = GetTransStr;
                }

                var GetDescription = ConvertHelper.ObjToStr(GetMagicEffect.Description);
                SetType = "Description";
                GetTransStr = GetTransData(GetMagicEffect.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    DSDItem NDSDItem = new DSDItem();
                    NDSDItem.editor_id = GetMagicEffect.EditorID;
                    NDSDItem.form_id = GenFormID(GetMagicEffect.FormKey);
                    NDSDItem.type = "MGEF DNAM";
                    NDSDItem.original = GetDescription;
                    NDSDItem.@string = GetTransStr;
                    DSDItems.Add(NDSDItem);
                    //GetMagicEffect.Description = GetTransStr;
                }
            }
            return DSDItems;
        }

        public static List<DSDItem> GetCells(EspReader Reader)
        {
            List<DSDItem> DSDItems = new List<DSDItem>();
            for (int i = 0; i < Reader.Cells.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Cells.ElementAt(i).Key;
                var GetCell = Reader.Cells[GetHashKey];
                int ForID = 0;

                if (GetCell.SubBlocks != null)
                    for (int ii = 0; ii < GetCell.SubBlocks.Count; ii++)
                    {
                        ForID++;
                        if (GetCell.SubBlocks[ii].Cells != null)
                            for (int iii = 0; iii < GetCell.SubBlocks[ii].Cells.Count; iii++)
                            {
                                ForID++;
                                var GetName = ConvertHelper.ObjToStr(GetCell.SubBlocks[ii].Cells[iii].Name);
                                string SetType = string.Format("Cell[{0}]", ForID);
                                GetTransStr = GetTransData(GetCell.SubBlocks[ii].Cells[iii].EditorID, SetType);
                                if (GetTransStr.Length > 0)
                                {
                                    DSDItem NDSDItem = new DSDItem();
                                    NDSDItem.editor_id = GetCell.SubBlocks[ii].Cells[iii].EditorID;
                                    NDSDItem.form_id = GenFormID(GetCell.SubBlocks[ii].Cells[iii].FormKey);
                                    NDSDItem.type = "CELL FULL";
                                    NDSDItem.original = GetName;
                                    NDSDItem.@string = GetTransStr;
                                    DSDItems.Add(NDSDItem);
                                    //GetCell.SubBlocks[ii].Cells[iii].Name = GetTransStr;
                                }
                            }
                    }
            }
            return DSDItems;
        }
    }
}
