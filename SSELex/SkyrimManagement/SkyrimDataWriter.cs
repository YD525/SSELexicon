using System.Text.RegularExpressions;
using System.Windows.Forms;
using SSELex.ConvertManager;
using SSELex.SkyrimManage;
using SSELex.TranslateManage;

namespace SSELex.UIManage
{
    public class SkyrimDataWriter
    {
        public static string PreFormatStr(string Str)
        {
            Str = Str.Replace("，", ",");
            Str = Str.Replace("。", ".");
            Str = Str.Replace("？", "?");
            Str = Str.Replace("！", "!");
            Str = Str.Replace("；", ";");
            Str = Str.Replace("：", ":");
            Str = Str.Replace("“", "'");
            Str = Str.Replace("”", "'");
            Str = Str.Replace("‘", "'");
            Str = Str.Replace("’", "'");
            Str = Str.Replace("’", "'");
            return Str;
        }

        public static void WriteAllMemoryData(ref EspReader Writer)
        {
            for (int i = 0; i < Translator.TransData.Count; i++)
            {
                var GetHashKey = Translator.TransData.ElementAt(i).Key;
                if (Translator.TransData[GetHashKey].Trim().Length > 0)
                {
                    SetData(GetHashKey, Translator.TransData[GetHashKey].Trim());
                }
            }
            ReplaceAllToMemory(ref Writer);
        }

        public static void SetData(string GetKey, string TransData)
        {
            string NewStr = PreFormatStr(TransData);
            if (Regex.Replace(NewStr, @"\s+", "").Length > 0)
            {
                Translator.TransData[GetKey] = NewStr;
            }
            else
            {
                Translator.TransData[GetKey] = string.Empty;
            }
        }

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

        public static void ReplaceAllToMemory(ref EspReader Writer)
        {
            SetWorldspaces(ref Writer);
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

        public static void SetWorldspaces(ref EspReader Writer)
        {
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
                    GetWorldspaceItem.Name = GetTransStr;
                }

                Writer.Worldspaces[GetHashKey] = GetWorldspaceItem;
            }
        }

        public static void SetQuests(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Quests.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Writer.Quests.ElementAt(i).Key;
                var GetQuestItem = Writer.Quests[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetQuestItem.Name);

                SetType = "Name";
                GetTransStr = GetTransData(GetQuestItem.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    GetQuestItem.Name = GetTransStr;
                }

                var GetDescription = ConvertHelper.ObjToStr(GetQuestItem.Description);

                SetType = "Description";
                GetTransStr = GetTransData(GetQuestItem.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    GetQuestItem.Description = GetTransStr;
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
                                GetQuestItem.Objectives[ir].DisplayText = GetTransStr;
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
                                    GetQuestItem.Stages[ii].LogEntries[iii].Entry = GetTransStr;
                                }
                            }
                        }
                    }
                }

                Writer.Quests[GetHashKey] = GetQuestItem;
            }
        }

        public static void SetFactions(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Factions.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Writer.Factions.ElementAt(i).Key;
                var GetFactionItem = Writer.Factions[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetFactionItem.Name);

                SetType = "Name";
                GetTransStr = GetTransData(GetFactionItem.EditorID, SetType);
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
                            CountRank++;
                            string GetFemale = ConvertHelper.ObjToStr(GetFactionItem.Ranks[ii].Title.Female);
                            string GetMale = ConvertHelper.ObjToStr(GetFactionItem.Ranks[ii].Title.Male);

                            SetType = string.Format("Female[{0}]", CountRank);
                            GetTransStr = GetTransData(GetFactionItem.EditorID, SetType);
                            if (GetTransStr.Trim().Length > 0)
                            {
                                GetFactionItem.Ranks[ii].Title.Female = GetTransStr;
                            }

                            SetType = string.Format("Male[{0}]", CountRank);
                            GetTransStr = GetTransData(GetFactionItem.EditorID, SetType);
                            if (GetTransStr.Trim().Length > 0)
                            {
                                GetFactionItem.Ranks[ii].Title.Male = GetTransStr;
                            }
                        }
                }
                Writer.Factions[GetHashKey] = GetFactionItem;
            }
        }

        public static void SetPerks(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Perks.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Writer.Perks.ElementAt(i).Key;
                var GetPerkItem = Writer.Perks[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetPerkItem.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetPerkItem.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    GetPerkItem.Name = GetTransStr;
                }

                var GetDescription = ConvertHelper.ObjToStr(GetPerkItem.Description);
                SetType = "Description";
                GetTransStr = GetTransData(GetPerkItem.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    GetPerkItem.Description = GetTransStr;
                }

                Writer.Perks[GetHashKey] = GetPerkItem;
            }
        }

        public static void SetWeapons(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Weapons.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Writer.Weapons.ElementAt(i).Key;
                var GetWeapon = Writer.Weapons[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetWeapon.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetWeapon.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    GetWeapon.Name = GetTransStr;
                }

                var GetDescription = ConvertHelper.ObjToStr(GetWeapon.Description);
                SetType = "Description";
                GetTransStr = GetTransData(GetWeapon.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    GetWeapon.Description = GetTransStr;
                }

                Writer.Weapons[GetHashKey] = GetWeapon;
            }
        }

        public static void SetSoulGems(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.SoulGems.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Writer.SoulGems.ElementAt(i).Key;
                var GetSoulGem = Writer.SoulGems[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetSoulGem.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetSoulGem.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    GetSoulGem.Name = GetTransStr;
                }

                Writer.SoulGems[GetHashKey] = GetSoulGem;
            }
        }

        public static void SetArmors(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Armors.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Writer.Armors.ElementAt(i).Key;
                var GetArmor = Writer.Armors[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetArmor.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetArmor.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    GetArmor.Name = GetTransStr;
                }

                var GetDescription = ConvertHelper.ObjToStr(GetArmor.Description);
                SetType = "Description";
                GetTransStr = GetTransData(GetArmor.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    GetArmor.Description = GetTransStr;
                }

                Writer.Armors[GetHashKey] = GetArmor;
            }
        }

        public static void SetKeys(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Keys.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Writer.Keys.ElementAt(i).Key;
                var GetKey = Writer.Keys[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetKey.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetKey.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    GetKey.Name = GetTransStr;
                }

                Writer.Keys[GetHashKey] = GetKey;
            }
        }

        public static void SetContainers(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Containers.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Writer.Containers.ElementAt(i).Key;
                var GetContainer = Writer.Containers[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetContainer.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetContainer.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    GetContainer.Name = GetTransStr;
                }

                Writer.Containers[GetHashKey] = GetContainer;
            }
        }

        public static void SetActivators(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Activators.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Writer.Activators.ElementAt(i).Key;
                var GetActivator = Writer.Activators[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetActivator.Name);
                SetType = "Name" + GetActivator;
                GetTransStr = GetTransData(GetActivator.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    GetActivator.Name = GetTransStr;
                }

                var GetActivateTextOverride = ConvertHelper.ObjToStr(GetActivator.ActivateTextOverride);
                if (GetActivateTextOverride.Trim().Length > 0)
                {
                    SetType = "ActivateTextOverride";
                    GetTransStr = GetTransData(GetActivator.EditorID, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetActivator.ActivateTextOverride = GetTransStr;
                    }
                }
                Writer.Activators[GetHashKey] = GetActivator;
            }
        }

        public static void SetMiscItems(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.MiscItems.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Writer.MiscItems.ElementAt(i).Key;
                var GetMiscItem = Writer.MiscItems[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetMiscItem.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetMiscItem.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    GetMiscItem.Name = GetTransStr;
                }
                Writer.MiscItems[GetHashKey] = GetMiscItem;
            }
        }

        public static void SetBooks(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Books.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Writer.Books.ElementAt(i).Key;
                var Books = Writer.Books[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(Books.Name);
                SetType = "Name";
                GetTransStr = GetTransData(Books.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    Books.Name = GetTransStr;
                }

                var GetDescription = ConvertHelper.ObjToStr(Books.Description);
                SetType = "Description";
                GetTransStr = GetTransData(Books.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    Books.Description = GetTransStr;
                }

                var GetBookText = ConvertHelper.ObjToStr(Books.BookText);
                SetType = "BookText";
                GetTransStr = GetTransData(Books.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    Books.BookText = GetTransStr;
                }

                Writer.Books[GetHashKey] = Books;
            }
        }

        public static void SetMessages(ref EspReader Writer)
        {
            string SetType = "Name";
            for (int i = 0; i < Writer.Messages.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Writer.Messages.ElementAt(i).Key;
                var GetMessageItem = Writer.Messages[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetMessageItem.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetMessageItem.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    GetMessageItem.Name = GetTransStr;
                }

                var GetDescription = ConvertHelper.ObjToStr(GetMessageItem.Description);
                SetType = "Description";
                GetTransStr = GetTransData(GetMessageItem.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    GetMessageItem.Description = GetTransStr;
                }
                Writer.Messages[GetHashKey] = GetMessageItem;
            }
        }

        public static void SetMessageButtons(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Messages.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Writer.Messages.ElementAt(i).Key;
                var GetMessageItem = Writer.Messages[GetHashKey];

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
                                GetMessageItem.MenuButtons[ir].Text = GetTransStr;
                            }
                        }
                    }
                }

                Writer.Messages[GetHashKey] = GetMessageItem;
            }
        }

        public static void SetDialogTopics(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.DialogTopics.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Writer.DialogTopics.ElementAt(i).Key;
                var GetDialogTopicItem = Writer.DialogTopics[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetDialogTopicItem.Name);
                if (GetName.Length > 0)
                {
                    SetType = "Name";
                    GetTransStr = GetTransData(GetDialogTopicItem.EditorID, SetType);
                    if (GetTransStr.Length > 0)
                    {
                        GetDialogTopicItem.Name = GetTransStr;
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
                                    GetDialogTopicItem.Responses[ii].Responses[iii].Text = GetTransStr;
                                }
                            }
                    }
                Writer.DialogTopics[GetHashKey] = GetDialogTopicItem;
            }
        }

        public static void SetSpells(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.Spells.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Writer.Spells.ElementAt(i).Key;
                var GetSpellItem = Writer.Spells[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetSpellItem.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetSpellItem.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    GetSpellItem.Name = GetTransStr;
                }
                Writer.Spells[GetHashKey] = GetSpellItem;
            }
        }

        public static void SetObjectEffects(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.ObjectEffects.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Writer.ObjectEffects.ElementAt(i).Key;
                var GetObjectEffect = Writer.ObjectEffects[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetObjectEffect.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetObjectEffect.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    GetObjectEffect.Name = GetTransStr;
                }
                Writer.ObjectEffects[GetHashKey] = GetObjectEffect;
            }
        }

        public static void SetMagicEffects(ref EspReader Writer)
        {
            string SetType = "";
            for (int i = 0; i < Writer.MagicEffects.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Writer.MagicEffects.ElementAt(i).Key;
                var GetMagicEffect = Writer.MagicEffects[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetMagicEffect.Name);
                SetType = "Name";
                GetTransStr = GetTransData(GetMagicEffect.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    GetMagicEffect.Name = GetTransStr;
                }

                var GetDescription = ConvertHelper.ObjToStr(GetMagicEffect.Description);
                SetType = "Description";
                GetTransStr = GetTransData(GetMagicEffect.EditorID, SetType);
                if (GetTransStr.Length > 0)
                {
                    GetMagicEffect.Description = GetTransStr;
                }
                Writer.MagicEffects[GetHashKey] = GetMagicEffect;
            }
        }

        public static void SetCells(ref EspReader Writer)
        {
            for (int i = 0; i < Writer.Cells.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Writer.Cells.ElementAt(i).Key;
                var GetCell = Writer.Cells[GetHashKey];
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
                                    GetCell.SubBlocks[ii].Cells[iii].Name = GetTransStr;
                                }
                            }
                    }
                Writer.Cells[GetHashKey] = GetCell;
            }
        }
    }
}
