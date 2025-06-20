using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using Mutagen.Bethesda.Plugins;
using SSELex.ConvertManager;
using SSELex.SkyrimManage;
using SSELex.SkyrimManagement;
using SSELex.TranslateManage;
using SSELex.UIManagement;
using static PhoenixEngine.SSELexiconBridge.NativeBridge;

namespace SSELex.UIManage
{
    public class SkyrimDataWriter
    {
        public static void WriteAllMemoryData(ref EspReader Writer)
        {
            TranslatorBridge.FormatData();
            ReplaceAllToMemory(ref Writer);
        }

        public static string GetTransData(string EditorID, string SetType)
        {
            string GetKey = SkyrimDataLoader.GenUniqueKey(EditorID, SetType);

            return TranslatorBridge.GetTransData(GetKey);
        }

        public static void ReplaceAllToMemory(ref EspReader Writer)
        {
            SetHazards(ref Writer);
            SetHeadParts(ref Writer);
            SetNpcs(ref Writer);
            SetWorldspaces(ref Writer);
            SetShouts(ref Writer);
            SetTrees(ref Writer);
            SetIngestibles(ref Writer);
            SetRaces(ref Writer);
            SetQuests(ref Writer);
            SetFactions(ref Writer);
            SetPerks(ref Writer);
            SetWeapons(ref Writer);
            SetSoulGems(ref Writer);
            SetArmors(ref Writer);
            SetKeys(ref Writer);
            SetContainers(ref Writer);
            SetActivators(ref Writer);
            SetMiscItems(ref Writer);
            SetBooks(ref Writer);
            SetMessages(ref Writer);
            SetMessageButtons(ref Writer);
            SetDialogTopics(ref Writer);
            SetSpells(ref Writer);
            SetMagicEffects(ref Writer);
            SetObjectEffects(ref Writer);
            SetCells(ref Writer);
        }

        public static void SetHazards(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Hazards.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.Hazards.ElementAt(i).Key;
                    var GetHazardItem = Writer.Hazards[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetHazardItem.FormKey, GetHazardItem.EditorID);

                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetHazardItem.Name = GetTransStr;
                    }

                    Writer.Hazards[GetHashKey] = GetHazardItem;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetHazards loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetHeadParts(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.HeadParts.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.HeadParts.ElementAt(i).Key;
                    var GetHeadPartItem = Writer.HeadParts[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetHeadPartItem.FormKey, GetHeadPartItem.EditorID);

                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetHeadPartItem.Name = GetTransStr;
                    }

                    Writer.HeadParts[GetHashKey] = GetHeadPartItem;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetHeadParts loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetNpcs(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Npcs.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.Npcs.ElementAt(i).Key;
                    var GetNpcItem = Writer.Npcs[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetNpcItem.FormKey, GetNpcItem.EditorID);

                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetNpcItem.Name = GetTransStr;
                    }


                    SetType = "ShortName";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetNpcItem.ShortName = GetTransStr;
                    }

                    Writer.Npcs[GetHashKey] = GetNpcItem;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in Npcs loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetWorldspaces(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Worldspaces.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.Worldspaces.ElementAt(i).Key;
                    var GetWorldspaceItem = Writer.Worldspaces[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetWorldspaceItem.FormKey, GetWorldspaceItem.EditorID);

                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetWorldspaceItem.Name = GetTransStr;
                    }

                    Writer.Worldspaces[GetHashKey] = GetWorldspaceItem;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetWorldspaces loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetShouts(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Shouts.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.Shouts.ElementAt(i).Key;
                    var GetShoutItem = Writer.Shouts[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetShoutItem.FormKey, GetShoutItem.EditorID);

                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetShoutItem.Name = GetTransStr;
                    }

                    Writer.Shouts[GetHashKey] = GetShoutItem;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in Shouts loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetTrees(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Trees.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.Trees.ElementAt(i).Key;
                    var GetTreeItem = Writer.Trees[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetTreeItem.FormKey, GetTreeItem.EditorID);

                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetTreeItem.Name = GetTransStr;
                    }

                    Writer.Trees[GetHashKey] = GetTreeItem;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in Trees loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetIngestibles(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Ingestibles.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.Ingestibles.ElementAt(i).Key;
                    var GetIngestibleItem = Writer.Ingestibles[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetIngestibleItem.FormKey, GetIngestibleItem.EditorID);

                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetIngestibleItem.Name = GetTransStr;
                    }

                    Writer.Ingestibles[GetHashKey] = GetIngestibleItem;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in Ingestibles loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetRaces(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Races.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.Races.ElementAt(i).Key;
                    var GetRaceItem = Writer.Races[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetRaceItem.FormKey, GetRaceItem.EditorID);

                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetRaceItem.Name = GetTransStr;
                    }

                    SetType = "Description";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetRaceItem.Description = GetTransStr;
                    }

                    Writer.Races[GetHashKey] = GetRaceItem;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetRaces loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetQuests(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Quests.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.Quests.ElementAt(i).Key;
                    var GetQuestItem = Writer.Quests[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetQuestItem.FormKey, GetQuestItem.EditorID);

                    var GetName = ConvertHelper.ObjToStr(GetQuestItem.Name);

                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetQuestItem.Name = GetTransStr;
                    }

                    SetType = "Description";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetQuestItem.Description = GetTransStr;
                    }

                    if (GetQuestItem.Objectives.Count > 0)
                    {
                        int CountObjective = 0;
                        for (int ir = 0; ir < GetQuestItem.Objectives.Count; ir++)
                        {
                            try
                            {
                                CountObjective++;
                                var GetObjectiveItem = GetQuestItem.Objectives[ir];
                                var GetDisplayText = ConvertHelper.ObjToStr(GetObjectiveItem.DisplayText);
                                if (GetDisplayText.Length > 0)
                                {
                                    SetType = string.Format("DisplayText[{0}]", CountObjective);
                                    GetTransStr = GetTransData(AutoKey, SetType);
                                    if (GetTransStr.Trim().Length > 0)
                                    {
                                        GetQuestItem.Objectives[ir].DisplayText = GetTransStr;
                                    }
                                }
                            }
                            catch (System.Exception ex)
                            {
                                System.Console.WriteLine($"Error in SetQuests objectives loop at index {ir}: {ex.Message}");
                            }
                        }
                    }

                    if (GetQuestItem.Stages.Count > 0)
                    {
                        int CountStage = 0;
                        for (int ii = 0; ii < GetQuestItem.Stages.Count; ii++)
                        {
                            try
                            {
                                CountStage++;
                                for (int iii = 0; iii < GetQuestItem.Stages[ii].LogEntries.Count; iii++)
                                {
                                    try
                                    {
                                        CountStage++;
                                        var GetLogEntrieItem = GetQuestItem.Stages[ii].LogEntries[iii];

                                        var GetEntry = ConvertHelper.ObjToStr(GetLogEntrieItem.Entry);
                                        if (GetEntry.Length > 0)
                                        {
                                            SetType = string.Format("Entry[{0}]", CountStage);
                                            GetTransStr = GetTransData(AutoKey, SetType);
                                            if (GetTransStr.Trim().Length > 0)
                                            {
                                                GetQuestItem.Stages[ii].LogEntries[iii].Entry = GetTransStr;
                                            }
                                        }
                                    }
                                    catch (System.Exception ex)
                                    {
                                        System.Console.WriteLine($"Error in SetQuests log entries loop at index {iii}: {ex.Message}");
                                    }
                                }
                            }
                            catch (System.Exception ex)
                            {
                                System.Console.WriteLine($"Error in SetQuests stages loop at index {ii}: {ex.Message}");
                            }
                        }
                    }

                    Writer.Quests[GetHashKey] = GetQuestItem;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetQuests main loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetFactions(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Factions.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.Factions.ElementAt(i).Key;
                    var GetFactionItem = Writer.Factions[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetFactionItem.FormKey, GetFactionItem.EditorID);

                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetFactionItem.Name = GetTransStr;
                    }

                    if (GetFactionItem.Ranks.Count > 0)
                    {
                        int CountRank = 0;
                        if (GetFactionItem.Ranks != null)
                            for (int ii = 0; ii < GetFactionItem.Ranks.Count; ii++)
                            {
                                try
                                {
                                    CountRank++;
                                    if (GetFactionItem.Ranks[ii].Title != null)
                                    {
                                        string GetFemale = ConvertHelper.ObjToStr(GetFactionItem.Ranks[ii].Title.Female);
                                        string GetMale = ConvertHelper.ObjToStr(GetFactionItem.Ranks[ii].Title.Male);

                                        SetType = string.Format("Female[{0}]", CountRank);
                                        GetTransStr = GetTransData(AutoKey, SetType);
                                        if (GetTransStr.Trim().Length > 0)
                                        {
                                            GetFactionItem.Ranks[ii].Title.Female = GetTransStr;
                                        }

                                        SetType = string.Format("Male[{0}]", CountRank);
                                        GetTransStr = GetTransData(AutoKey, SetType);
                                        if (GetTransStr.Trim().Length > 0)
                                        {
                                            GetFactionItem.Ranks[ii].Title.Male = GetTransStr;
                                        }
                                    }
                                }
                                catch (System.Exception ex)
                                {
                                    System.Console.WriteLine($"Error in SetFactions ranks loop at index {ii}: {ex.Message}");
                                }
                            }
                    }
                    Writer.Factions[GetHashKey] = GetFactionItem;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetFactions main loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetPerks(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Perks.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.Perks.ElementAt(i).Key;
                    var GetPerkItem = Writer.Perks[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetPerkItem.FormKey, GetPerkItem.EditorID);

                    var GetName = ConvertHelper.ObjToStr(GetPerkItem.Name);
                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetPerkItem.Name = GetTransStr;
                    }

                    SetType = "Description";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetPerkItem.Description = GetTransStr;
                    }

                    Writer.Perks[GetHashKey] = GetPerkItem;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetPerks loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetWeapons(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Weapons.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.Weapons.ElementAt(i).Key;
                    var GetWeapon = Writer.Weapons[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetWeapon.FormKey, GetWeapon.EditorID);

                    var GetName = ConvertHelper.ObjToStr(GetWeapon.Name);
                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetWeapon.Name = GetTransStr;
                    }

                    var GetDescription = ConvertHelper.ObjToStr(GetWeapon.Description);
                    SetType = "Description";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetWeapon.Description = GetTransStr;
                    }

                    Writer.Weapons[GetHashKey] = GetWeapon;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetWeapons loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetSoulGems(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.SoulGems.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.SoulGems.ElementAt(i).Key;
                    var GetSoulGem = Writer.SoulGems[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetSoulGem.FormKey, GetSoulGem.EditorID);

                    var GetName = ConvertHelper.ObjToStr(GetSoulGem.Name);
                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetSoulGem.Name = GetTransStr;
                    }

                    Writer.SoulGems[GetHashKey] = GetSoulGem;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetSoulGems loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetArmors(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Armors.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.Armors.ElementAt(i).Key;
                    var GetArmor = Writer.Armors[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetArmor.FormKey, GetArmor.EditorID);

                    var GetName = ConvertHelper.ObjToStr(GetArmor.Name);
                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetArmor.Name = GetTransStr;
                    }

                    var GetDescription = ConvertHelper.ObjToStr(GetArmor.Description);
                    SetType = "Description";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetArmor.Description = GetTransStr;
                    }

                    Writer.Armors[GetHashKey] = GetArmor;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetArmors loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetKeys(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Keys.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.Keys.ElementAt(i).Key;
                    var GetKey = Writer.Keys[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetKey.FormKey, GetKey.EditorID);

                    var GetName = ConvertHelper.ObjToStr(GetKey.Name);
                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetKey.Name = GetTransStr;
                    }

                    Writer.Keys[GetHashKey] = GetKey;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetKeys loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetContainers(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Containers.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.Containers.ElementAt(i).Key;
                    var GetContainer = Writer.Containers[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetContainer.FormKey, GetContainer.EditorID);

                    var GetName = ConvertHelper.ObjToStr(GetContainer.Name);
                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetContainer.Name = GetTransStr;
                    }

                    Writer.Containers[GetHashKey] = GetContainer;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetContainers loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetActivators(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Activators.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.Activators.ElementAt(i).Key;
                    var GetActivator = Writer.Activators[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetActivator.FormKey, GetActivator.EditorID);

                    var GetName = ConvertHelper.ObjToStr(GetActivator.Name);
                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetActivator.Name = GetTransStr;
                    }

                    var GetActivateTextOverride = ConvertHelper.ObjToStr(GetActivator.ActivateTextOverride);
                    if (GetActivateTextOverride.Trim().Length > 0)
                    {
                        SetType = "ActivateTextOverride";
                        GetTransStr = GetTransData(AutoKey, SetType);
                        if (GetTransStr.Length > 0)
                        {
                            GetActivator.ActivateTextOverride = GetTransStr;
                        }
                    }
                    Writer.Activators[GetHashKey] = GetActivator;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetActivators loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetMiscItems(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.MiscItems.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.MiscItems.ElementAt(i).Key;
                    var GetMiscItem = Writer.MiscItems[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetMiscItem.FormKey, GetMiscItem.EditorID);

                    var GetName = ConvertHelper.ObjToStr(GetMiscItem.Name);
                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetMiscItem.Name = GetTransStr;
                    }
                    Writer.MiscItems[GetHashKey] = GetMiscItem;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetMiscItems loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetBooks(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Books.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.Books.ElementAt(i).Key;
                    var Books = Writer.Books[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(Books.FormKey, Books.EditorID);

                    var GetName = ConvertHelper.ObjToStr(Books.Name);
                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        Books.Name = GetTransStr;
                    }

                    var GetDescription = ConvertHelper.ObjToStr(Books.Description);
                    SetType = "Description";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        Books.Description = GetTransStr;
                    }

                    var GetBookText = ConvertHelper.ObjToStr(Books.BookText);
                    SetType = "BookText";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        Books.BookText = GetTransStr;
                    }

                    Writer.Books[GetHashKey] = Books;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetBooks loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetMessages(ref EspReader Writer)
        {
            string SetType = "Name";
            for (int i = 0; i < Writer.Messages.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.Messages.ElementAt(i).Key;
                    var GetMessageItem = Writer.Messages[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetMessageItem.FormKey, GetMessageItem.EditorID);

                    var GetName = ConvertHelper.ObjToStr(GetMessageItem.Name);
                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetMessageItem.Name = GetTransStr;
                    }

                    var GetDescription = ConvertHelper.ObjToStr(GetMessageItem.Description);
                    SetType = "Description";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetMessageItem.Description = GetTransStr;
                    }
                    Writer.Messages[GetHashKey] = GetMessageItem;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetMessages loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetMessageButtons(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Messages.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.Messages.ElementAt(i).Key;
                    var GetMessageItem = Writer.Messages[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetMessageItem.FormKey, GetMessageItem.EditorID);

                    if (GetMessageItem.MenuButtons != null)
                    {
                        if (GetMessageItem.MenuButtons.Count > 0)
                        {
                            for (int ir = 0; ir < GetMessageItem.MenuButtons.Count; ir++)
                            {
                                try
                                {
                                    var GetButton = ConvertHelper.ObjToStr(GetMessageItem.MenuButtons[ir].Text);
                                    SetType = string.Format("Button[{0}]", ir);
                                    GetTransStr = GetTransData(AutoKey, SetType);
                                    if (GetTransStr.Length > 0)
                                    {
                                        GetMessageItem.MenuButtons[ir].Text = GetTransStr;
                                    }
                                }
                                catch (System.Exception ex)
                                {
                                    System.Console.WriteLine($"Error in SetMessageButtons inner loop at index {ir}: {ex.Message}");
                                }
                            }
                        }
                    }

                    Writer.Messages[GetHashKey] = GetMessageItem;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetMessageButtons main loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetDialogTopics(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.DialogTopics.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.DialogTopics.ElementAt(i).Key;
                    var GetDialogTopicItem = Writer.DialogTopics[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetDialogTopicItem.FormKey, GetDialogTopicItem.EditorID);

                    var GetName = ConvertHelper.ObjToStr(GetDialogTopicItem.Name);
                    if (GetName.Length > 0)
                    {
                        SetType = "Name";
                        GetTransStr = GetTransData(AutoKey, SetType);
                        if (GetTransStr.Length > 0)
                        {
                            GetDialogTopicItem.Name = GetTransStr;
                        }
                    }

                    int ForCount = 0;
                    if (GetDialogTopicItem.Responses != null)
                        for (int ii = 0; ii < GetDialogTopicItem.Responses.Count; ii++)
                        {
                            try
                            {
                                ForCount++;
                                string GetPrompt = ConvertHelper.ObjToStr(GetDialogTopicItem.Responses[ii].Prompt);
                                if (GetPrompt.Length > 0)
                                {
                                    SetType = string.Format("ResponsePrompt[{0}]", ForCount);
                                    GetTransStr = GetTransData(AutoKey, SetType);
                                    if (GetTransStr.Length > 0)
                                    {
                                        GetDialogTopicItem.Responses[ii].Prompt = GetTransStr;
                                    }
                                }
                                if (GetDialogTopicItem.Responses[ii].Responses != null)
                                    for (int iii = 0; iii < GetDialogTopicItem.Responses[ii].Responses.Count; iii++)
                                    {
                                        try
                                        {
                                            ForCount++;

                                            string GetValue = ConvertHelper.ObjToStr(GetDialogTopicItem.Responses[ii].Responses[iii].Text);
                                            SetType = string.Format("Response[{0}]", ForCount);
                                            GetTransStr = GetTransData(AutoKey, SetType);
                                            if (GetTransStr.Length > 0)
                                            {
                                                GetDialogTopicItem.Responses[ii].Responses[iii].Text = GetTransStr;
                                            }
                                        }
                                        catch (System.Exception ex)
                                        {
                                            System.Console.WriteLine($"Error in SetDialogTopics innermost loop at index {iii}: {ex.Message}");
                                        }
                                    }
                            }
                            catch (System.Exception ex)
                            {
                                System.Console.WriteLine($"Error in SetDialogTopics inner loop at index {ii}: {ex.Message}");
                            }
                        }
                    Writer.DialogTopics[GetHashKey] = GetDialogTopicItem;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetDialogTopics main loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetSpells(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Spells.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.Spells.ElementAt(i).Key;
                    var GetSpellItem = Writer.Spells[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetSpellItem.FormKey, GetSpellItem.EditorID);

                    var GetName = ConvertHelper.ObjToStr(GetSpellItem.Name);
                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetSpellItem.Name = GetTransStr;
                    }

                    var GetDescription = ConvertHelper.ObjToStr(GetSpellItem.Description);
                    SetType = "Description";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetSpellItem.Description = GetTransStr;
                    }

                    Writer.Spells[GetHashKey] = GetSpellItem;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetSpells loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetObjectEffects(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.ObjectEffects.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.ObjectEffects.ElementAt(i).Key;
                    var GetObjectEffect = Writer.ObjectEffects[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetObjectEffect.FormKey, GetObjectEffect.EditorID);

                    var GetName = ConvertHelper.ObjToStr(GetObjectEffect.Name);
                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetObjectEffect.Name = GetTransStr;
                    }
                    Writer.ObjectEffects[GetHashKey] = GetObjectEffect;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetObjectEffects loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetMagicEffects(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.MagicEffects.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.MagicEffects.ElementAt(i).Key;
                    var GetMagicEffect = Writer.MagicEffects[GetHashKey];

                    string AutoKey = KeyGenerator.GenKey(GetMagicEffect.FormKey, GetMagicEffect.EditorID);

                    var GetName = ConvertHelper.ObjToStr(GetMagicEffect.Name);
                    SetType = "Name";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetMagicEffect.Name = GetTransStr;
                    }

                    var GetDescription = ConvertHelper.ObjToStr(GetMagicEffect.Description);
                    SetType = "Description";
                    GetTransStr = GetTransData(AutoKey, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetMagicEffect.Description = GetTransStr;
                    }
                    Writer.MagicEffects[GetHashKey] = GetMagicEffect;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetMagicEffects loop at index {i}: {ex.Message}");
                }
            }
        }

        public static void SetCells(ref EspReader Writer)
        {
            for (int i = 0; i < Writer.Cells.Count; i++)
            {
                try
                {
                    string GetTransStr = "";

                    var GetHashKey = Writer.Cells.ElementAt(i).Key;
                    var GetCell = Writer.Cells[GetHashKey];
                    int ForID = 0;

                    if (GetCell.SubBlocks != null)
                        for (int ii = 0; ii < GetCell.SubBlocks.Count; ii++)
                        {
                            try
                            {
                                ForID++;
                                if (GetCell.SubBlocks[ii].Cells != null)
                                    for (int iii = 0; iii < GetCell.SubBlocks[ii].Cells.Count; iii++)
                                    {
                                        try
                                        {
                                            ForID++;
                                            var GetName = ConvertHelper.ObjToStr(GetCell.SubBlocks[ii].Cells[iii].Name);
                                            string SetType = string.Format("Cell[{0}]", ForID);
                                            string AutoKey = KeyGenerator.GenKey(GetCell.SubBlocks[ii].Cells[iii].FormKey, GetCell.SubBlocks[ii].Cells[iii].EditorID);
                                            GetTransStr = GetTransData(AutoKey, SetType);
                                            if (GetTransStr.Length > 0)
                                            {
                                                GetCell.SubBlocks[ii].Cells[iii].Name = GetTransStr;
                                            }
                                        }
                                        catch (System.Exception ex)
                                        {
                                            System.Console.WriteLine($"Error in SetCells innermost loop at index {iii}: {ex.Message}");
                                        }
                                    }
                            }
                            catch (System.Exception ex)
                            {
                                System.Console.WriteLine($"Error in SetCells inner loop at index {ii}: {ex.Message}");
                            }
                        }
                    Writer.Cells[GetHashKey] = GetCell;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Error in SetCells main loop at index {i}: {ex.Message}");
                }
            }
        }
    }
}