using System.Windows;
using Noggog;
using SSELex.ConvertManager;
using SSELex.SkyrimManage;
using SSELex.TranslateManage;
using SSELex.UIManagement;

namespace SSELex.UIManage
{
    public class SkyrimDataLoader
    {
        public enum ObjSelect
        {
            Null = 99, All = 0, Worldspaces = 1, Quests = 2, Factions = 3, Perks = 5, Weapons = 6, SoulGems = 7, Armors = 8, Keys = 9, Containers = 10, Activators = 11, MiscItems = 12, Books = 13, Messages = 15, DialogTopics = 16, Spells = 17, MagicEffects = 18, ObjectEffects = 19, Cells = 20, Races = 21
        }

        public static List<ObjSelect> QueryParams(EspReader Reader)
        {
            List<ObjSelect> ObjSelects = new List<ObjSelect>();
            if (Reader.Worldspaces.Count > 0)
            {
                ObjSelects.Add(ObjSelect.Worldspaces);
            }

            if (Reader.Races.Count > 0)
            {
                ObjSelects.Add(ObjSelect.Races);
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
             if (Type == ObjSelect.Races)
            {
                LoadRaces(Reader, View);
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
            LoadRaces(Reader, View);
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
                    try
                    {
                        string GetTransStr = "";

                        var GetHashKey = Reader.Worldspaces.ElementAt(i).Key;
                        var GetWorldspaceItem = Reader.Worldspaces[GetHashKey];

                        string AutoKey = SkyrimDataWriter.GetAutoKey(GetWorldspaceItem.EditorID, GetWorldspaceItem.FormKey);

                        var GetName = ConvertHelper.ObjToStr(GetWorldspaceItem.Name);
                        if (GetName.Length > 0)
                        {
                            string SetType = "Name";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Worldspace", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading Worldspace item at index {i}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
        }

        public static void LoadRaces(EspReader Reader, YDListView View)
        {
            if (Reader.Races != null)
                for (int i = 0; i < Reader.Races.Count; i++)
                {
                    try
                    {
                        string GetTransStr = "";

                        var GetHashKey = Reader.Races.ElementAt(i).Key;
                        var GetRaceItem = Reader.Races[GetHashKey];

                        string AutoKey = SkyrimDataWriter.GetAutoKey(GetRaceItem.EditorID, GetRaceItem.FormKey);

                        var GetName = ConvertHelper.ObjToStr(GetRaceItem.Name);
                        if (GetName.Length > 0)
                        {
                            string SetType = "Name";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Race", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                            }));
                        }

                        var GetDescription = ConvertHelper.ObjToStr(GetRaceItem.Description);
                        if (GetDescription.Length > 0)
                        {
                            string SetType = "Description";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Race", GetHashKey, GetUniqueKey, GetDescription, GetTransStr, 999));
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading Race item at index {i}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
        }

        public static void LoadQuests(EspReader Reader, YDListView View)
        {
            if (Reader.Quests != null)
                for (int i = 0; i < Reader.Quests.Count; i++)
                {
                    try
                    {
                        string GetTransStr = "";

                        var GetHashKey = Reader.Quests.ElementAt(i).Key;
                        var GetQuestItem = Reader.Quests[GetHashKey];

                        string AutoKey = SkyrimDataWriter.GetAutoKey(GetQuestItem.EditorID, GetQuestItem.FormKey);

                        var GetName = ConvertHelper.ObjToStr(GetQuestItem.Name);
                        if (GetName.Length > 0)
                        {
                            string SetType = "Name";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Quest", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                            }));
                        }
                        var GetDescription = ConvertHelper.ObjToStr(GetQuestItem.Description);
                        if (GetDescription.Length > 0)
                        {
                            string SetType = "Description";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Quest", GetHashKey, GetUniqueKey, GetDescription, GetTransStr, 999));
                            }));
                        }

                        if (GetQuestItem.Objectives != null)
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
                                            string SetType = string.Format("DisplayText[{0}]", CountObjective);
                                            GetTransStr = TryGetTransData(AutoKey, SetType);
                                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                                            Application.Current.Dispatcher.Invoke(new Action(() =>
                                            {
                                                View.AddRowR(LineRenderer.CreatLine("Quest", GetHashKey, GetUniqueKey, GetDisplayText, GetTransStr, 999));
                                            }));
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show($"Error loading Quest Objective item at index {ir} for Quest {GetHashKey}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                }
                            }

                        if (GetQuestItem.Stages != null)
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
                                                    string SetType = string.Format("Entry[{0}]", CountStage);
                                                    GetTransStr = TryGetTransData(AutoKey, SetType);
                                                    string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                                                    Application.Current.Dispatcher.Invoke(new Action(() =>
                                                    {
                                                        View.AddRowR(LineRenderer.CreatLine("Quest", GetHashKey, GetUniqueKey, GetEntry, GetTransStr, 999));
                                                    }));
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show($"Error loading Quest Log Entry item at index {iii} in Stage {ii} for Quest {GetHashKey}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show($"Error loading Quest Stage item at index {ii} for Quest {GetHashKey}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                }
                            }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading Quest item at index {i}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
        }

        public static void LoadFactions(EspReader Reader, YDListView View)
        {
            if (Reader.Factions != null)
                for (int i = 0; i < Reader.Factions.Count; i++)
                {
                    try
                    {
                        string GetTransStr = "";

                        var GetHashKey = Reader.Factions.ElementAt(i).Key;
                        var GetFactionItem = Reader.Factions[GetHashKey];

                        string AutoKey = SkyrimDataWriter.GetAutoKey(GetFactionItem.EditorID, GetFactionItem.FormKey);

                        var GetName = ConvertHelper.ObjToStr(GetFactionItem.Name);
                        if (GetName.Length > 0)
                        {
                            string SetType = "Name";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Faction", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                            }));
                        }

                        if (GetFactionItem.Ranks != null)
                            if (GetFactionItem.Ranks.Count > 0)
                            {
                                int CountRank = 0;
                                if (GetFactionItem.Ranks != null)
                                    foreach (var GetRank in GetFactionItem.Ranks)
                                    {
                                        try
                                        {
                                            CountRank++;
                                            if (GetRank.Title != null)
                                            {
                                                string GetFemale = ConvertHelper.ObjToStr(GetRank.Title.Female);
                                                string GetMale = ConvertHelper.ObjToStr(GetRank.Title.Male);

                                                if (GetFemale.Trim().Length > 0)
                                                {
                                                    string SetType = string.Format("Female[{0}]", CountRank);
                                                    GetTransStr = TryGetTransData(AutoKey, SetType);
                                                    string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                                                    Application.Current.Dispatcher.Invoke(new Action(() =>
                                                    {
                                                        View.AddRowR(LineRenderer.CreatLine("Faction", GetHashKey, GetUniqueKey, GetFemale, GetTransStr, 999));
                                                    }));
                                                }
                                                if (GetMale.Trim().Length > 0)
                                                {
                                                    string SetType = string.Format("Male[{0}]", CountRank);
                                                    GetTransStr = TryGetTransData(AutoKey, SetType);
                                                    string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                                                    Application.Current.Dispatcher.Invoke(new Action(() =>
                                                    {
                                                        View.AddRowR(LineRenderer.CreatLine("Faction", GetHashKey, GetUniqueKey, GetMale, GetTransStr, 999));
                                                    }));
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show($"Error loading Faction Rank item for Faction {GetHashKey}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        }
                                    }
                            }

                        if (GetFactionItem.Relations != null)
                            if (GetFactionItem.Relations.Count > 0)
                            {
                                // No data processing here, so no specific try-catch needed unless relations processing is added.
                            }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading Faction item at index {i}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
        }

        public static void LoadPerks(EspReader Reader, YDListView View)
        {
            if (Reader.Perks != null)
                for (int i = 0; i < Reader.Perks.Count; i++)
                {
                    try
                    {
                        string GetTransStr = "";

                        var GetHashKey = Reader.Perks.ElementAt(i).Key;
                        var GetPerkItem = Reader.Perks[GetHashKey];

                        string AutoKey = SkyrimDataWriter.GetAutoKey(GetPerkItem.EditorID, GetPerkItem.FormKey);

                        var GetName = ConvertHelper.ObjToStr(GetPerkItem.Name);
                        if (GetName.Length > 0)
                        {
                            string SetType = "Name";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Perk", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                            }));
                        }

                        var GetDescription = ConvertHelper.ObjToStr(GetPerkItem.Description);
                        if (GetDescription.Length > 0)
                        {
                            string SetType = "Description";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Perk", GetHashKey, GetUniqueKey, GetDescription, GetTransStr, 999));
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading Perk item at index {i}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
        }

        public static void LoadWeapons(EspReader Reader, YDListView View)
        {
            if (Reader.Weapons != null)
                for (int i = 0; i < Reader.Weapons.Count; i++)
                {
                    try
                    {
                        string GetTransStr = "";

                        var GetHashKey = Reader.Weapons.ElementAt(i).Key;
                        var GetWeapon = Reader.Weapons[GetHashKey];

                        string AutoKey = SkyrimDataWriter.GetAutoKey(GetWeapon.EditorID, GetWeapon.FormKey);

                        var GetName = ConvertHelper.ObjToStr(GetWeapon.Name);
                        if (GetName.Length > 0)
                        {
                            string SetType = "Name";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Weapon", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                            }));
                        }

                        var GetDescription = ConvertHelper.ObjToStr(GetWeapon.Description);
                        if (GetDescription.Length > 0)
                        {
                            string SetType = "Description";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Weapon", GetHashKey, GetUniqueKey, GetDescription, GetTransStr, 999));
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading Weapon item at index {i}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
        }

        public static void LoadSoulGems(EspReader Reader, YDListView View)
        {
            if (Reader.SoulGems != null)
                for (int i = 0; i < Reader.SoulGems.Count; i++)
                {
                    try
                    {
                        string GetTransStr = "";

                        var GetHashKey = Reader.SoulGems.ElementAt(i).Key;
                        var GetSoulGem = Reader.SoulGems[GetHashKey];

                        string AutoKey = SkyrimDataWriter.GetAutoKey(GetSoulGem.EditorID, GetSoulGem.FormKey);

                        var GetName = ConvertHelper.ObjToStr(GetSoulGem.Name);
                        if (GetName.Length > 0)
                        {
                            string SetType = "Name";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("SoulGem", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading Soul Gem item at index {i}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
        }

        public static void LoadArmors(EspReader Reader, YDListView View)
        {
            if (Reader.Armors != null)
                for (int i = 0; i < Reader.Armors.Count; i++)
                {
                    try
                    {
                        string GetTransStr = "";

                        var GetHashKey = Reader.Armors.ElementAt(i).Key;
                        var GetArmor = Reader.Armors[GetHashKey];

                        string AutoKey = SkyrimDataWriter.GetAutoKey(GetArmor.EditorID, GetArmor.FormKey);

                        var GetName = ConvertHelper.ObjToStr(GetArmor.Name);
                        if (GetName.Length > 0)
                        {
                            string SetType = "Name";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Armor", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                            }));
                        }

                        var GetDescription = ConvertHelper.ObjToStr(GetArmor.Description);
                        if (GetDescription.Length > 0)
                        {
                            string SetType = "Description";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Armor", GetHashKey, GetUniqueKey, GetDescription, GetTransStr, 999));
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading Armor item at index {i}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
        }

        public static void LoadKeys(EspReader Reader, YDListView View)
        {
            if (Reader.Keys != null)
                for (int i = 0; i < Reader.Keys.Count; i++)
                {
                    try
                    {
                        string GetTransStr = "";

                        var GetHashKey = Reader.Keys.ElementAt(i).Key;
                        var GetKey = Reader.Keys[GetHashKey];

                        string AutoKey = SkyrimDataWriter.GetAutoKey(GetKey.EditorID, GetKey.FormKey);

                        var GetName = ConvertHelper.ObjToStr(GetKey.Name);
                        if (GetName.Length > 0)
                        {
                            string SetType = "Name";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Key", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading Key item at index {i}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
        }

        public static void LoadContainers(EspReader Reader, YDListView View)
        {
            if (Reader.Containers != null)
                for (int i = 0; i < Reader.Containers.Count; i++)
                {
                    try
                    {
                        string GetTransStr = "";

                        var GetHashKey = Reader.Containers.ElementAt(i).Key;
                        var GetContainer = Reader.Containers[GetHashKey];

                        string AutoKey = SkyrimDataWriter.GetAutoKey(GetContainer.EditorID, GetContainer.FormKey);

                        var GetName = ConvertHelper.ObjToStr(GetContainer.Name);
                        if (GetName.Length > 0)
                        {
                            string SetType = "Name";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Container", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading Container item at index {i}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
        }

        public static void LoadActivators(EspReader Reader, YDListView View)
        {
            if (Reader.Activators != null)
                for (int i = 0; i < Reader.Activators.Count; i++)
                {
                    try
                    {
                        string GetTransStr = "";

                        var GetHashKey = Reader.Activators.ElementAt(i).Key;
                        var GetActivator = Reader.Activators[GetHashKey];

                        string AutoKey = SkyrimDataWriter.GetAutoKey(GetActivator.EditorID, GetActivator.FormKey);

                        var GetName = ConvertHelper.ObjToStr(GetActivator.Name);
                        if (GetName.Length > 0)
                        {
                            string SetType = "Name";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Activator", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                            }));
                        }

                        var GetActivateTextOverride = ConvertHelper.ObjToStr(GetActivator.ActivateTextOverride);
                        if (GetActivateTextOverride.Trim().Length > 0)
                        {
                            string SetType = "ActivateTextOverride";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Activator", GetHashKey, GetUniqueKey, GetActivateTextOverride, GetTransStr, 999));
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading Activator item at index {i}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
        }

        public static void LoadMiscItems(EspReader Reader, YDListView View)
        {
            if (Reader.MiscItems != null)
                for (int i = 0; i < Reader.MiscItems.Count; i++)
                {
                    try
                    {
                        string GetTransStr = "";

                        var GetHashKey = Reader.MiscItems.ElementAt(i).Key;
                        var GetMiscItem = Reader.MiscItems[GetHashKey];

                        string AutoKey = SkyrimDataWriter.GetAutoKey(GetMiscItem.EditorID, GetMiscItem.FormKey);

                        var GetName = ConvertHelper.ObjToStr(GetMiscItem.Name);
                        if (GetName.Length > 0)
                        {
                            string SetType = "Name";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("MiscItem", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading Misc Item at index {i}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
        }

        public static void LoadBooks(EspReader Reader, YDListView View)
        {
            if (Reader.Books != null)
                for (int i = 0; i < Reader.Books.Count; i++)
                {
                    try
                    {
                        string GetTransStr = "";

                        var GetHashKey = Reader.Books.ElementAt(i).Key;
                        var Books = Reader.Books[GetHashKey];

                        string AutoKey = SkyrimDataWriter.GetAutoKey(Books.EditorID, Books.FormKey);

                        var GetName = ConvertHelper.ObjToStr(Books.Name);
                        if (GetName.Length > 0)
                        {
                            string SetType = "Name";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Book", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                            }));
                        }

                        var GetDescription = ConvertHelper.ObjToStr(Books.Description);
                        if (GetDescription.Length > 0)
                        {
                            string SetType = "Description";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Book", GetHashKey, GetUniqueKey, GetDescription, GetTransStr, 999));
                            }));
                        }

                        var GetBookText = ConvertHelper.ObjToStr(Books.BookText);
                        if (GetBookText.Length > 0)
                        {
                            string SetType = "BookText";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Book", GetHashKey, GetUniqueKey, GetBookText, GetTransStr, 999));
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading Book item at index {i}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
        }

        public static void LoadMessages(EspReader Reader, YDListView View)
        {
            if (Reader.Messages != null)
                for (int i = 0; i < Reader.Messages.Count; i++)
                {
                    try
                    {
                        string GetTransStr = "";

                        var GetHashKey = Reader.Messages.ElementAt(i).Key;
                        var GetMessageItem = Reader.Messages[GetHashKey];

                        string AutoKey = SkyrimDataWriter.GetAutoKey(GetMessageItem.EditorID, GetMessageItem.FormKey);

                        var GetName = ConvertHelper.ObjToStr(GetMessageItem.Name);
                        if (GetName.Length > 0)
                        {
                            string SetType = "Name";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Message", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                            }));
                        }

                        var GetDescription = ConvertHelper.ObjToStr(GetMessageItem.Description);
                        if (GetDescription.Length > 0)
                        {
                            string SetType = "Description";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Message", GetHashKey, GetUniqueKey, GetDescription, GetTransStr, 999));
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading Message item at index {i}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
        }

        public static void LoadMessageButtons(EspReader Reader, YDListView View)
        {
            if (Reader.Messages != null)
                for (int i = 0; i < Reader.Messages.Count; i++)
                {
                    try
                    {
                        string GetTransStr = "";

                        var GetHashKey = Reader.Messages.ElementAt(i).Key;
                        var GetMessageItem = Reader.Messages[GetHashKey];

                        string AutoKey = SkyrimDataWriter.GetAutoKey(GetMessageItem.EditorID, GetMessageItem.FormKey);

                        var GetButtons = GetMessageItem.MenuButtons;
                        if (GetButtons != null)
                        {
                            if (GetButtons.Count > 0)
                            {
                                for (int ir = 0; ir < GetButtons.Count; ir++)
                                {
                                    try
                                    {
                                        var GetButton = ConvertHelper.ObjToStr(GetButtons[ir].Text);
                                        if (GetButton.Length > 0)
                                        {
                                            string SetType = string.Format("Button[{0}]", ir);
                                            GetTransStr = TryGetTransData(AutoKey, SetType);
                                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                                            Application.Current.Dispatcher.Invoke(new Action(() =>
                                            {
                                                View.AddRowR(LineRenderer.CreatLine("Button", GetHashKey, GetUniqueKey, GetButton, GetTransStr, 999));
                                            }));
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show($"Error loading Message Button item at index {ir} for Message {GetHashKey}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error processing Message buttons for item at index {i}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
        }

        public static void LoadDialogTopics(EspReader Reader, YDListView View)
        {
            if (Reader.DialogTopics != null)
                for (int i = 0; i < Reader.DialogTopics.Count; i++)
                {
                    try
                    {
                        string GetTransStr = "";

                        var GetHashKey = Reader.DialogTopics.ElementAt(i).Key;
                        var GetDialogTopicItem = Reader.DialogTopics[GetHashKey];

                        string AutoKey = SkyrimDataWriter.GetAutoKey(GetDialogTopicItem.EditorID, GetDialogTopicItem.FormKey);

                        var GetName = ConvertHelper.ObjToStr(GetDialogTopicItem.Name);
                        if (GetName.Length > 0)
                        {
                            string SetType = "Name";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("DialogTopic", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                            }));
                        }

                        var GetResponses = GetDialogTopicItem.Responses;
                        int ForCount = 0;
                        if (GetResponses != null)
                            foreach (var GetChild in GetResponses)
                            {
                                try
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
                                            View.AddRowR(LineRenderer.CreatLine("DialogTopic", GetHashKey, GetUniqueKey, GetPrompt, GetTransStr, 999));
                                        }));
                                    }

                                    if (GetChild.Responses != null)
                                        foreach (var GetChildA in GetChild.Responses)
                                        {
                                            try
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
                                                        View.AddRowR(LineRenderer.CreatLine("DialogTopic", GetHashKey, GetUniqueKey, GetValue, GetTransStr, 999));
                                                    }));
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show($"Error loading Dialog Topic child response for Dialog Topic {GetHashKey}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                            }
                                        }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Error loading Dialog Topic response for Dialog Topic {GetHashKey}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading Dialog Topic item at index {i}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
        }

        public static void LoadSpells(EspReader Reader, YDListView View)
        {
            if (Reader.Spells != null)
                for (int i = 0; i < Reader.Spells.Count; i++)
                {
                    try
                    {
                        string GetTransStr = "";

                        var GetHashKey = Reader.Spells.ElementAt(i).Key;
                        var GetSpellItem = Reader.Spells[GetHashKey];

                        string AutoKey = SkyrimDataWriter.GetAutoKey(GetSpellItem.EditorID, GetSpellItem.FormKey);

                        var GetName = ConvertHelper.ObjToStr(GetSpellItem.Name);
                        if (GetName.Length > 0)
                        {
                            string SetType = "Name";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Spell", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                            }));
                        }

                        var GetDescription = ConvertHelper.ObjToStr(GetSpellItem.Description);
                        if (GetDescription.Length > 0)
                        {
                            string SetType = "Description";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("Spell", GetHashKey, GetUniqueKey, GetDescription, GetTransStr, 999));
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading Spell item at index {i}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
        }

        public static void LoadObjectEffects(EspReader Reader, YDListView View)
        {
            if (Reader.ObjectEffects != null)
                for (int i = 0; i < Reader.ObjectEffects.Count; i++)
                {
                    try
                    {
                        string GetTransStr = "";

                        var GetHashKey = Reader.ObjectEffects.ElementAt(i).Key;
                        var GetObjectEffect = Reader.ObjectEffects[GetHashKey];

                        string AutoKey = SkyrimDataWriter.GetAutoKey(GetObjectEffect.EditorID, GetObjectEffect.FormKey);

                        var GetName = ConvertHelper.ObjToStr(GetObjectEffect.Name);
                        if (GetName.Length > 0)
                        {
                            string SetType = "Name";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("ObjectEffect", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading Object Effect item at index {i}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
        }

        public static void LoadMagicEffects(EspReader Reader, YDListView View)
        {
            if (Reader.MagicEffects != null)
                for (int i = 0; i < Reader.MagicEffects.Count; i++)
                {
                    try
                    {
                        string GetTransStr = "";

                        var GetHashKey = Reader.MagicEffects.ElementAt(i).Key;
                        var GetMagicEffect = Reader.MagicEffects[GetHashKey];

                        string AutoKey = SkyrimDataWriter.GetAutoKey(GetMagicEffect.EditorID, GetMagicEffect.FormKey);

                        var GetName = ConvertHelper.ObjToStr(GetMagicEffect.Name);
                        if (GetName.Length > 0)
                        {
                            string SetType = "Name";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("MagicEffect", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                            }));
                        }

                        var GetDescription = ConvertHelper.ObjToStr(GetMagicEffect.Description);
                        if (GetDescription.Length > 0)
                        {
                            string SetType = "Description";
                            GetTransStr = TryGetTransData(AutoKey, SetType);
                            string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                View.AddRowR(LineRenderer.CreatLine("MagicEffect", GetHashKey, GetUniqueKey, GetDescription, GetTransStr, 999));
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading Magic Effect item at index {i}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
        }

        public static void LoadCells(EspReader Reader, YDListView View)
        {
            if (Reader.Cells != null)
                for (int i = 0; i < Reader.Cells.Count; i++)
                {
                    try
                    {
                        string GetTransStr = "";

                        var GetHashKey = Reader.Cells.ElementAt(i).Key;
                        var GetCell = Reader.Cells[GetHashKey];
                        int ForID = 0;
                        if (GetCell.SubBlocks != null)
                            foreach (var Get in GetCell.SubBlocks)
                            {
                                try
                                {
                                    ForID++;
                                    if (Get.Cells != null)
                                        foreach (var GetChild in Get.Cells)
                                        {
                                            try
                                            {
                                                ForID++;
                                                var GetName = ConvertHelper.ObjToStr(GetChild.Name);
                                                if (GetName.Length > 0)
                                                {
                                                    string AutoKey = SkyrimDataWriter.GetAutoKey(GetChild.EditorID, GetChild.FormKey);

                                                    string SetType = string.Format("Cell[{0}]", ForID);
                                                    GetTransStr = TryGetTransData(AutoKey, SetType);
                                                    string GetUniqueKey = GenUniqueKey(AutoKey, SetType);

                                                    Application.Current.Dispatcher.Invoke(new Action(() =>
                                                    {
                                                        View.AddRowR(LineRenderer.CreatLine("Cell", GetHashKey, GetUniqueKey, GetName, GetTransStr, 999));
                                                    }));
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show($"Error loading Cell sub-block child cell for Cell {GetHashKey}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                            }
                                        }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Error loading Cell sub-block for Cell {GetHashKey}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading Cell item at index {i}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
        }
    }
}