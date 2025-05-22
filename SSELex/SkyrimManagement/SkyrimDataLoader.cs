using System.Windows;
using Noggog;
using SSELex.ConvertManager;
using SSELex.SkyrimManage;
using SSELex.TranslateManage;

namespace SSELex.UIManage
{
    public class SkyrimDataLoader
    {
        public enum ObjSelect
        {
            Null = 99, All = 0, Worldspaces = 1, Quests = 2, Factions = 3, Perks = 5, Weapons = 6, SoulGems = 7, Armors = 8, Keys = 9, Containers = 10, Activators = 11, MiscItems = 12, Books = 13, Messages = 15, DialogTopics = 16, Spells = 17, MagicEffects = 18, ObjectEffects = 19, Cells = 20
        }

        public static List<ObjSelect> QueryParams(EspReader Reader)
        {
            List<ObjSelect> ObjSelects = new List<ObjSelect>();
            if (Reader.Worldspaces.Count > 0)
            {
                ObjSelects.Add(ObjSelect.Worldspaces);
            }

            if (Reader.Quests.Count > 0)
            {
                ObjSelects.Add(ObjSelect.Quests);
            }

            if (Reader.Factions.Count > 0)
            {
                ObjSelects.Add(ObjSelect.Factions);
            }

            if (Reader.Perks.Count > 0)
            {
                ObjSelects.Add(ObjSelect.Perks);
            }

            if (Reader.Weapons.Count > 0)
            {
                ObjSelects.Add(ObjSelect.Weapons);
            }

            if (Reader.SoulGems.Count > 0)
            {
                ObjSelects.Add(ObjSelect.SoulGems);
            }

            if (Reader.Armors.Count > 0)
            {
                ObjSelects.Add(ObjSelect.Armors);
            }

            if (Reader.Keys.Count > 0)
            {
                ObjSelects.Add(ObjSelect.Keys);
            }

            if (Reader.Containers.Count > 0)
            {
                ObjSelects.Add(ObjSelect.Containers);
            }

            if (Reader.Activators.Count > 0)
            {
                ObjSelects.Add(ObjSelect.Activators);
            }

            if (Reader.MiscItems.Count > 0)
            {
                ObjSelects.Add(ObjSelect.MiscItems);
            }

            if (Reader.Books.Count > 0)
            {
                ObjSelects.Add(ObjSelect.Books);
            }

            if (Reader.Messages.Count > 0)
            {
                ObjSelects.Add(ObjSelect.Messages);
            }

            if (Reader.DialogTopics.Count > 0)
            {
                ObjSelects.Add(ObjSelect.DialogTopics);
            }

            if (Reader.Spells.Count > 0)
            {
                ObjSelects.Add(ObjSelect.Spells);
            }

            if (Reader.MagicEffects.Count > 0)
            {
                ObjSelects.Add(ObjSelect.MagicEffects);
            }

            if (Reader.ObjectEffects.Count > 0)
            {
                ObjSelects.Add(ObjSelect.ObjectEffects);
            }

            if (Reader.Cells.Count > 0)
            {
                ObjSelects.Add(ObjSelect.Cells);
            }

            ObjSelects.Add(ObjSelect.All);

            return ObjSelects;
        }

        public static string GenUniqueKey(string EditorID, string SetType)
        {
            return (EditorID + "(" + SetType + ")");
        }

        public static void Load(ObjSelect Type, EspReader Reader, YDListView View)
        {
            if (Type == ObjSelect.All)
            {
                LoadAll(Reader, View);
                return;
            }

            if (Type == ObjSelect.Worldspaces)
            {
                LoadWorldspaces(Reader, View);
            }
            else
            if (Type == ObjSelect.Quests)
            {
                LoadQuests(Reader, View);
            }
            else
            if (Type == ObjSelect.Factions)
            {
                LoadFactions(Reader, View);
            }
            else
            if (Type == ObjSelect.Perks)
            {
                LoadPerks(Reader, View);
            }
            else
            if (Type == ObjSelect.Weapons)
            {
                LoadWeapons(Reader, View);
            }
            else
            if (Type == ObjSelect.SoulGems)
            {
                LoadSoulGems(Reader, View);
            }
            else
            if (Type == ObjSelect.Armors)
            {
                LoadArmors(Reader, View);
            }
            else
            if (Type == ObjSelect.Keys)
            {
                LoadKeys(Reader, View);
            }
            else
            if (Type == ObjSelect.Containers)
            {
                LoadContainers(Reader, View);
            }
            else
            if (Type == ObjSelect.Activators)
            {
                LoadActivators(Reader, View);
            }
            else
            if (Type == ObjSelect.MiscItems)
            {
                LoadMiscItems(Reader, View);
            }
            else
            if (Type == ObjSelect.Books)
            {
                LoadBooks(Reader, View);
            }
            else
            if (Type == ObjSelect.Messages)
            {
                LoadMessages(Reader, View);
                LoadMessageButtons(Reader, View);
            }
            else
            if (Type == ObjSelect.DialogTopics)
            {
                LoadDialogTopics(Reader, View);
            }
            else
            if (Type == ObjSelect.Spells)
            {
                LoadSpells(Reader, View);
            }
            else
            if (Type == ObjSelect.MagicEffects)
            {
                LoadMagicEffects(Reader, View);
            }
            else
            if (Type == ObjSelect.ObjectEffects)
            {
                LoadObjectEffects(Reader, View);
            }
            else
            if (Type == ObjSelect.Cells)
            {
                LoadCells(Reader, View);
            }
        }

        public static void LoadAll(EspReader Reader, YDListView View)
        {
            LoadWorldspaces(Reader, View);
            LoadQuests(Reader, View);
            LoadFactions(Reader, View);
            LoadPerks(Reader, View);
            LoadWeapons(Reader, View);
            LoadSoulGems(Reader, View);
            LoadArmors(Reader, View);
            LoadKeys(Reader, View);
            LoadContainers(Reader, View);
            LoadActivators(Reader, View);
            LoadMiscItems(Reader, View);
            LoadBooks(Reader, View);
            LoadMessages(Reader, View);
            LoadMessageButtons(Reader, View);
            LoadDialogTopics(Reader, View);
            LoadSpells(Reader, View);
            LoadMagicEffects(Reader, View);
            LoadObjectEffects(Reader, View);
            LoadCells(Reader, View);
        }

        public static string TryGetTransData(string EditorID, string SetType)
        {
            string GetKey = GenUniqueKey(EditorID, SetType);
            if (Translator.TransData.ContainsKey(GetKey))
            {
                return Translator.TransData[GetKey];
            }
            else
            {
                Translator.TransData.Add(GetKey, string.Empty);
            }
            return string.Empty;
        }

        public static void LoadWorldspaces(EspReader Reader, YDListView View)
        {
            if (Reader.Worldspaces != null)
                for (int i = 0; i < Reader.Worldspaces.Count; i++)
                {
                    string GetTransStr = "";

                    var GetHashKey = Reader.Worldspaces.ElementAt(i).Key;
                    var GetWorldspaceItem = Reader.Worldspaces[GetHashKey];

                    var GetName = ConvertHelper.ObjToStr(GetWorldspaceItem.Name);
                    if (GetName.Length > 0)
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetWorldspaceItem.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetWorldspaceItem.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("Worldspace", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                        }));
                    }
                }
        }

        public static void LoadQuests(EspReader Reader, YDListView View)
        {
            if (Reader.Quests != null)
                for (int i = 0; i < Reader.Quests.Count; i++)
                {
                    string GetTransStr = "";

                    var GetHashKey = Reader.Quests.ElementAt(i).Key;
                    var GetQuestItem = Reader.Quests[GetHashKey];

                    var GetName = ConvertHelper.ObjToStr(GetQuestItem.Name);
                    if (GetName.Length > 0)
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetQuestItem.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetQuestItem.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("Quest", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                        }));
                    }
                    var GetDescription = ConvertHelper.ObjToStr(GetQuestItem.Description);
                    if (GetDescription.Length > 0)
                    {
                        string SetType = "Description";
                        GetTransStr = TryGetTransData(GetQuestItem.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetQuestItem.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("Quest", GetHashKey, GetUniqueKey, GetDescription, GetTransStr, 999));
                        }));
                    }

                    if (GetQuestItem.Objectives != null)
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
                                    string SetType = string.Format("DisplayText[{0}]", CountObjective);
                                    GetTransStr = TryGetTransData(GetQuestItem.EditorID, SetType);
                                    string GetUniqueKey = GenUniqueKey(GetQuestItem.EditorID, SetType);

                                    Application.Current.Dispatcher.Invoke(new Action(() =>
                                    {
                                        View.AddRowR(UIHelper.CreatLine("Quest", GetHashKey, GetUniqueKey, GetDisplayText, GetTransStr, 999));
                                    }));
                                }
                            }
                        }

                    if (GetQuestItem.Stages != null)
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
                                        string SetType = string.Format("Entry[{0}]", CountStage);
                                        GetTransStr = TryGetTransData(GetQuestItem.EditorID, SetType);
                                        string GetUniqueKey = GenUniqueKey(GetQuestItem.EditorID, SetType);

                                        Application.Current.Dispatcher.Invoke(new Action(() =>
                                        {
                                            View.AddRowR(UIHelper.CreatLine("Quest", GetHashKey, GetUniqueKey, GetEntry, GetTransStr, 999));
                                        }));
                                    }
                                }
                            }
                        }
                }
        }

        public static void LoadFactions(EspReader Reader, YDListView View)
        {
            if (Reader.Factions != null)
                for (int i = 0; i < Reader.Factions.Count; i++)
                {
                    string GetTransStr = "";

                    var GetHashKey = Reader.Factions.ElementAt(i).Key;
                    var GetFactionItem = Reader.Factions[GetHashKey];

                    var GetName = ConvertHelper.ObjToStr(GetFactionItem.Name);
                    if (GetName.Length > 0)
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetFactionItem.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetFactionItem.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("Faction", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                        }));
                    }

                    if (GetFactionItem.Ranks != null)
                        if (GetFactionItem.Ranks.Count > 0)
                        {
                            int CountRank = 0;
                            if (GetFactionItem.Ranks != null)
                                foreach (var GetRank in GetFactionItem.Ranks)
                                {
                                    CountRank++;
                                    if (GetRank.Title != null)
                                    {
                                        string GetFemale = ConvertHelper.ObjToStr(GetRank.Title.Female);
                                        string GetMale = ConvertHelper.ObjToStr(GetRank.Title.Male);

                                        if (GetFemale.Trim().Length > 0)
                                        {
                                            string SetType = string.Format("Female[{0}]", CountRank);
                                            GetTransStr = TryGetTransData(GetFactionItem.EditorID, SetType);
                                            string GetUniqueKey = GenUniqueKey(GetFactionItem.EditorID, SetType);

                                            Application.Current.Dispatcher.Invoke(new Action(() =>
                                            {
                                                View.AddRowR(UIHelper.CreatLine("Faction", GetHashKey, GetUniqueKey, GetFemale, GetTransStr, 999));
                                            }));
                                        }
                                        if (GetMale.Trim().Length > 0)
                                        {
                                            string SetType = string.Format("Male[{0}]", CountRank);
                                            GetTransStr = TryGetTransData(GetFactionItem.EditorID, SetType);
                                            string GetUniqueKey = GenUniqueKey(GetFactionItem.EditorID, SetType);

                                            Application.Current.Dispatcher.Invoke(new Action(() =>
                                            {
                                                View.AddRowR(UIHelper.CreatLine("Faction", GetHashKey, GetUniqueKey, GetMale, GetTransStr, 999));
                                            }));
                                        }
                                    }
                                }
                        }

                    if (GetFactionItem.Relations != null)
                        if (GetFactionItem.Relations.Count > 0)
                        {

                        }
                }
        }

        public static void LoadPerks(EspReader Reader, YDListView View)
        {
            if (Reader.Perks != null)
                for (int i = 0; i < Reader.Perks.Count; i++)
                {
                    string GetTransStr = "";

                    var GetHashKey = Reader.Perks.ElementAt(i).Key;
                    var GetPerkItem = Reader.Perks[GetHashKey];

                    var GetName = ConvertHelper.ObjToStr(GetPerkItem.Name);
                    if (GetName.Length > 0)
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetPerkItem.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetPerkItem.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("Perk", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                        }));
                    }

                    var GetDescription = ConvertHelper.ObjToStr(GetPerkItem.Description);
                    if (GetDescription.Length > 0)
                    {
                        string SetType = "Description";
                        GetTransStr = TryGetTransData(GetPerkItem.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetPerkItem.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("Perk", GetHashKey, GetUniqueKey, GetDescription, GetTransStr, 999));
                        }));
                    }
                }
        }

        public static void LoadWeapons(EspReader Reader, YDListView View)
        {
            if (Reader.Weapons != null)
                for (int i = 0; i < Reader.Weapons.Count; i++)
                {
                    string GetTransStr = "";

                    var GetHashKey = Reader.Weapons.ElementAt(i).Key;
                    var GetWeapon = Reader.Weapons[GetHashKey];

                    var GetName = ConvertHelper.ObjToStr(GetWeapon.Name);
                    if (GetName.Length > 0)
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetWeapon.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetWeapon.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("Weapon", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                        }));
                    }

                    var GetDescription = ConvertHelper.ObjToStr(GetWeapon.Description);
                    if (GetDescription.Length > 0)
                    {
                        string SetType = "Description";
                        GetTransStr = TryGetTransData(GetWeapon.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetWeapon.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("Weapon", GetHashKey, GetUniqueKey, GetDescription, GetTransStr, 999));
                        }));
                    }
                }
        }

        public static void LoadSoulGems(EspReader Reader, YDListView View)
        {
            if (Reader.SoulGems != null)
                for (int i = 0; i < Reader.SoulGems.Count; i++)
                {
                    string GetTransStr = "";

                    var GetHashKey = Reader.SoulGems.ElementAt(i).Key;
                    var GetSoulGem = Reader.SoulGems[GetHashKey];

                    var GetName = ConvertHelper.ObjToStr(GetSoulGem.Name);
                    if (GetName.Length > 0)
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetSoulGem.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetSoulGem.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("SoulGem", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                        }));
                    }
                }
        }

        public static void LoadArmors(EspReader Reader, YDListView View)
        {
            if (Reader.Armors != null)
                for (int i = 0; i < Reader.Armors.Count; i++)
                {
                    string GetTransStr = "";

                    var GetHashKey = Reader.Armors.ElementAt(i).Key;
                    var GetArmor = Reader.Armors[GetHashKey];

                    var GetName = ConvertHelper.ObjToStr(GetArmor.Name);
                    if (GetName.Length > 0)
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetArmor.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetArmor.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("Armor", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                        }));
                    }

                    var GetDescription = ConvertHelper.ObjToStr(GetArmor.Description);
                    if (GetDescription.Length > 0)
                    {
                        string SetType = "Description";
                        GetTransStr = TryGetTransData(GetArmor.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetArmor.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("Armor", GetHashKey, GetUniqueKey, GetDescription, GetTransStr, 999));
                        }));
                    }
                }
        }

        public static void LoadKeys(EspReader Reader, YDListView View)
        {
            if (Reader.Keys != null)
                for (int i = 0; i < Reader.Keys.Count; i++)
                {
                    string GetTransStr = "";

                    var GetHashKey = Reader.Keys.ElementAt(i).Key;
                    var GetKey = Reader.Keys[GetHashKey];

                    var GetName = ConvertHelper.ObjToStr(GetKey.Name);
                    if (GetName.Length > 0)
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetKey.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetKey.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("Key", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                        }));
                    }
                }
        }

        public static void LoadContainers(EspReader Reader, YDListView View)
        {
            if (Reader.Containers != null)
                for (int i = 0; i < Reader.Containers.Count; i++)
                {
                    string GetTransStr = "";

                    var GetHashKey = Reader.Containers.ElementAt(i).Key;
                    var GetContainer = Reader.Containers[GetHashKey];

                    var GetName = ConvertHelper.ObjToStr(GetContainer.Name);
                    if (GetName.Length > 0)
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetContainer.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetContainer.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("Container", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                        }));
                    }
                }
        }

        public static void LoadActivators(EspReader Reader, YDListView View)
        {
            if (Reader.Activators != null)
                for (int i = 0; i < Reader.Activators.Count; i++)
                {
                    string GetTransStr = "";

                    var GetHashKey = Reader.Activators.ElementAt(i).Key;
                    var GetActivator = Reader.Activators[GetHashKey];

                    var GetName = ConvertHelper.ObjToStr(GetActivator.Name);
                    if (GetName.Length > 0)
                    {
                        string SetType = "Name" + GetActivator;
                        GetTransStr = TryGetTransData(GetActivator.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetActivator.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("Activator", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                        }));
                    }

                    var GetActivateTextOverride = ConvertHelper.ObjToStr(GetActivator.ActivateTextOverride);
                    if (GetActivateTextOverride.Trim().Length > 0)
                    {
                        string SetType = "ActivateTextOverride";
                        GetTransStr = TryGetTransData(GetActivator.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetActivator.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("Activator", GetHashKey, GetUniqueKey, GetActivateTextOverride, GetTransStr, 999));
                        }));
                    }
                }
        }

        public static void LoadMiscItems(EspReader Reader, YDListView View)
        {
            if (Reader.MiscItems != null)
                for (int i = 0; i < Reader.MiscItems.Count; i++)
                {
                    string GetTransStr = "";

                    var GetHashKey = Reader.MiscItems.ElementAt(i).Key;
                    var GetMiscItem = Reader.MiscItems[GetHashKey];

                    var GetName = ConvertHelper.ObjToStr(GetMiscItem.Name);
                    if (GetName.Length > 0)
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetMiscItem.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetMiscItem.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("MiscItem", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                        }));
                    }


                }
        }

        public static void LoadBooks(EspReader Reader, YDListView View)
        {
            if (Reader.Books != null)
                for (int i = 0; i < Reader.Books.Count; i++)
                {
                    string GetTransStr = "";

                    var GetHashKey = Reader.Books.ElementAt(i).Key;
                    var Books = Reader.Books[GetHashKey];

                    //GetTransStr = Translator.TransData[GetHashKey];

                    var GetName = ConvertHelper.ObjToStr(Books.Name);
                    if (GetName.Length > 0)
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(Books.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(Books.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("Book", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                        }));
                    }

                    var GetDescription = ConvertHelper.ObjToStr(Books.Description);
                    if (GetDescription.Length > 0)
                    {
                        string SetType = "Description";
                        GetTransStr = TryGetTransData(Books.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(Books.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("Book", GetHashKey, GetUniqueKey, GetDescription, GetTransStr, 999));
                        }));
                    }

                    var GetBookText = ConvertHelper.ObjToStr(Books.BookText);
                    if (GetBookText.Length > 0)
                    {
                        string SetType = "BookText";
                        GetTransStr = TryGetTransData(Books.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(Books.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("Book", GetHashKey, GetUniqueKey, GetBookText, GetTransStr, 999));
                        }));
                    }
                }
        }

        public static void LoadMessages(EspReader Reader, YDListView View)
        {
            if (Reader.Messages != null)
                for (int i = 0; i < Reader.Messages.Count; i++)
                {
                    string GetTransStr = "";

                    var GetHashKey = Reader.Messages.ElementAt(i).Key;
                    var GetMessageItem = Reader.Messages[GetHashKey];

                    var GetName = ConvertHelper.ObjToStr(GetMessageItem.Name);
                    if (GetName.Length > 0)
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetMessageItem.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetMessageItem.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("Message", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                        }));
                    }

                    var GetDescription = ConvertHelper.ObjToStr(GetMessageItem.Description);
                    if (GetDescription.Length > 0)
                    {
                        string SetType = "Description";
                        GetTransStr = TryGetTransData(GetMessageItem.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetMessageItem.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("Message", GetHashKey, GetUniqueKey, GetDescription, GetTransStr, 999));
                        }));
                    }
                }
        }

        public static void LoadMessageButtons(EspReader Reader, YDListView View)
        {
            if (Reader.Messages != null)
                for (int i = 0; i < Reader.Messages.Count; i++)
                {
                    string GetTransStr = "";

                    var GetHashKey = Reader.Messages.ElementAt(i).Key;
                    var GetMessageItem = Reader.Messages[GetHashKey];

                    var GetButtons = GetMessageItem.MenuButtons;
                    if (GetButtons != null)
                    {
                        if (GetButtons.Count > 0)
                        {
                            for (int ir = 0; ir < GetButtons.Count; ir++)
                            {
                                var GetButton = ConvertHelper.ObjToStr(GetButtons[ir].Text);
                                if (GetButton.Length > 0)
                                {
                                    string SetType = string.Format("Button[{0}]", ir);
                                    GetTransStr = TryGetTransData(GetMessageItem.EditorID, SetType);
                                    string GetUniqueKey = GenUniqueKey(GetMessageItem.EditorID, SetType);

                                    Application.Current.Dispatcher.Invoke(new Action(() =>
                                    {
                                        View.AddRowR(UIHelper.CreatLine("Button", GetHashKey, GetUniqueKey, GetButton, GetTransStr, 999));
                                    }));
                                }
                            }
                        }
                    }
                }
        }

        public static void LoadDialogTopics(EspReader Reader, YDListView View)
        {
            if (Reader.DialogTopics != null)
                for (int i = 0; i < Reader.DialogTopics.Count; i++)
                {
                    string GetTransStr = "";

                    var GetHashKey = Reader.DialogTopics.ElementAt(i).Key;
                    var GetDialogTopicItem = Reader.DialogTopics[GetHashKey];

                    string AutoKey = "";
                    if (GetDialogTopicItem.EditorID != null)
                    {
                        AutoKey = GetDialogTopicItem.EditorID;
                    }
                    else
                    {
                        if (GetDialogTopicItem.FormKey != null)
                        {
                            AutoKey = GetDialogTopicItem.FormKey.ToString();
                        }
                    }

                    var GetName = ConvertHelper.ObjToStr(GetDialogTopicItem.Name);
                    if (GetName.Length > 0)
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(AutoKey, SetType);
                        string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("DialogTopic", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                        }));
                    }

                    var GetResponses = GetDialogTopicItem.Responses;
                    int ForCount = 0;
                    if (GetResponses != null)
                        foreach (var GetChild in GetResponses)
                        {
                            ForCount++;
                            string GetPrompt = ConvertHelper.ObjToStr(GetChild.Prompt);
                            if (GetPrompt.Length > 0)
                            {
                                string SetType = string.Format("ResponsePrompt[{0}]", ForCount);
                                GetTransStr = TryGetTransData(AutoKey, SetType);
                                string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    View.AddRowR(UIHelper.CreatLine("DialogTopic", GetHashKey, GetUniqueKey, GetPrompt, GetTransStr, 999));
                                }));
                            }

                            if (GetChild.Responses != null)
                                foreach (var GetChildA in GetChild.Responses)
                                {
                                    ForCount++;

                                    string GetValue = ConvertHelper.ObjToStr(GetChildA.Text);
                                    if (GetValue.Length > 0)
                                    {
                                        string SetType = string.Format("Response[{0}]", ForCount);
                                        GetTransStr = TryGetTransData(AutoKey, SetType);
                                        string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                                        Application.Current.Dispatcher.Invoke(new Action(() =>
                                        {
                                            View.AddRowR(UIHelper.CreatLine("DialogTopic", GetHashKey, GetUniqueKey, GetValue, GetTransStr, 999));
                                        }));
                                    }
                                }
                        }

                }
        }

        public static void LoadSpells(EspReader Reader, YDListView View)
        {
            if (Reader.Spells != null)
                for (int i = 0; i < Reader.Spells.Count; i++)
                {
                    string GetTransStr = "";

                    var GetHashKey = Reader.Spells.ElementAt(i).Key;
                    var GetSpellItem = Reader.Spells[GetHashKey];

                    var GetName = ConvertHelper.ObjToStr(GetSpellItem.Name);
                    if (GetName.Length > 0)
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetSpellItem.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetSpellItem.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("Spell", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                        }));
                    }
                }
        }

        public static void LoadObjectEffects(EspReader Reader, YDListView View)
        {
            if (Reader.ObjectEffects != null)
                for (int i = 0; i < Reader.ObjectEffects.Count; i++)
                {
                    string GetTransStr = "";

                    var GetHashKey = Reader.ObjectEffects.ElementAt(i).Key;
                    var GetObjectEffect = Reader.ObjectEffects[GetHashKey];

                    var GetName = ConvertHelper.ObjToStr(GetObjectEffect.Name);
                    if (GetName.Length > 0)
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetObjectEffect.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetObjectEffect.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("ObjectEffect", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                        }));
                    }
                }
        }

        public static void LoadMagicEffects(EspReader Reader, YDListView View)
        {
            if (Reader.MagicEffects != null)
                for (int i = 0; i < Reader.MagicEffects.Count; i++)
                {
                    string GetTransStr = "";

                    var GetHashKey = Reader.MagicEffects.ElementAt(i).Key;
                    var GetMagicEffect = Reader.MagicEffects[GetHashKey];

                    var GetName = ConvertHelper.ObjToStr(GetMagicEffect.Name);
                    if (GetName.Length > 0)
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetMagicEffect.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetMagicEffect.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("MagicEffect", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                        }));
                    }

                    var GetDescription = ConvertHelper.ObjToStr(GetMagicEffect.Description);
                    if (GetDescription.Length > 0)
                    {
                        string SetType = "Description";
                        GetTransStr = TryGetTransData(GetMagicEffect.EditorID, SetType);
                        string GetUniqueKey = GenUniqueKey(GetMagicEffect.EditorID, SetType);

                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            View.AddRowR(UIHelper.CreatLine("MagicEffect", GetHashKey, GetUniqueKey, GetDescription, GetTransStr, 999));
                        }));
                    }
                }
        }

        public static void LoadCells(EspReader Reader, YDListView View)
        {
            if (Reader.Cells != null)
                for (int i = 0; i < Reader.Cells.Count; i++)
                {
                    string GetTransStr = "";

                    var GetHashKey = Reader.Cells.ElementAt(i).Key;
                    var GetCell = Reader.Cells[GetHashKey];
                    int ForID = 0;
                    if (GetCell.SubBlocks != null)
                        foreach (var Get in GetCell.SubBlocks)
                        {
                            ForID++;
                            if (Get.Cells != null)
                                foreach (var GetChild in Get.Cells)
                                {
                                    ForID++;
                                    var GetName = ConvertHelper.ObjToStr(GetChild.Name);
                                    if (GetName.Length > 0)
                                    {
                                        string SetType = string.Format("Cell[{0}]", ForID);
                                        GetTransStr = TryGetTransData(GetChild.EditorID, SetType);
                                        string GetUniqueKey = GenUniqueKey(GetChild.EditorID, SetType);

                                        Application.Current.Dispatcher.Invoke(new Action(() =>
                                        {
                                            View.AddRowR(UIHelper.CreatLine("Cell", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                                        }));
                                    }
                                }
                        }
                }
        }
    }
}
