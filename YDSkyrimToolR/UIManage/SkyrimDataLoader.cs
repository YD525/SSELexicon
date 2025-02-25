using Mutagen.Bethesda.Pex;
using Mutagen.Bethesda.Starfield;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YDSkyrimToolR.ConvertManager;
using YDSkyrimToolR.SkyrimManage;
using YDSkyrimToolR.TranslateManage;

namespace YDSkyrimToolR.UIManage
{
    /*
    * @Author: 约定
    * @GitHub: https://github.com/tolove336/YDSkyrimToolR
    * @Date: 2025-02-06
    */
    public class SkyrimDataLoader
    {
        public enum ObjSelect
        { 
           Null = 99, All = 0, Quests = 1, Factions = 2, Perks = 3, Weapons = 5, SoulGems = 6, Armors = 7, Keys = 8, Containers = 9, Activators = 10, MiscItems = 11, Books = 12, Messages = 13, DialogTopics = 16, Spells = 17, MagicEffects = 18, ObjectEffects = 19, Cells = 20
        }

        public static List<ObjSelect> QueryParams(EspReader Reader)
        {
            List<ObjSelect> ObjSelects = new List<ObjSelect>();
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
            return (EditorID + "(" + SetType.GetHashCode() + ")");
        }

        public static void Load(ObjSelect Type,EspReader Reader, YDListView View)
        {
            if (Type == ObjSelect.All)
            {
                LoadAll(Reader, View);
                return;
            }
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

        public static string TryGetTransData(string EditorID,string SetType)
        {
            int GetHashId = GenUniqueKey(EditorID,SetType).GetHashCode();
            if (Translator.TransData.ContainsKey(GetHashId))
            {
                return Translator.TransData[GetHashId];
            }
            else
            {
                Translator.TransData.Add(GetHashId,string.Empty);
            }
            return string.Empty;
        }

        public static void LoadQuests(EspReader Reader, YDListView View)
        {
            for (int i = 0; i < Reader.Quests.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Quests.ElementAt(i).Key;
                var GetQuestItem = Reader.Quests[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetQuestItem.Name);
                if (GetName.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetQuestItem.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("Quest", GetHashKey.ToString(), GenUniqueKey(GetQuestItem.EditorID,SetType), GetName, GetTransStr,false));
                    }));
                }
                var GetDescription = ConvertHelper.ObjToStr(GetQuestItem.Description);
                if (GetDescription.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Description";
                        GetTransStr = TryGetTransData(GetQuestItem.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("Quest", GetHashKey.ToString(), GenUniqueKey(GetQuestItem.EditorID,SetType).ToString(), GetDescription, GetTransStr,false));
                    }));
                }
            }
        }

        public static void LoadFactions(EspReader Reader, YDListView View)
        {
            for (int i = 0; i < Reader.Factions.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Factions.ElementAt(i).Key;
                var GetFactionItem = Reader.Factions[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetFactionItem.Name);
                if (GetName.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetFactionItem.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("Faction", GetHashKey.ToString(), GenUniqueKey(GetFactionItem.EditorID,SetType).ToString(), GetName, GetTransStr,false));
                    }));
                }

                if (GetFactionItem.Ranks.Count > 0 )
                {
                    int CountRank = 0;
                    foreach (var GetRank in GetFactionItem.Ranks)
                    {
                        CountRank++;
                        string GetFemale = ConvertHelper.ObjToStr(GetRank.Title.Female);
                        string GetMale = ConvertHelper.ObjToStr(GetRank.Title.Male);

                        if (GetFemale.Trim().Length > 0)
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                string SetType = string.Format("Female[{0}]",CountRank);
                                GetTransStr = TryGetTransData(GetFactionItem.EditorID, SetType);
                                View.AddRowR(UIHelper.CreatLine("Faction", GetHashKey.ToString(), GenUniqueKey(GetFactionItem.EditorID,SetType).ToString(), GetFemale, GetTransStr,false));
                            }));
                        }
                        if (GetMale.Trim().Length > 0)
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                string SetType = string.Format("Male[{0}]", CountRank);
                                GetTransStr = TryGetTransData(GetFactionItem.EditorID, SetType);
                                View.AddRowR(UIHelper.CreatLine("Faction", GetHashKey.ToString(), GenUniqueKey(GetFactionItem.EditorID,SetType).ToString(), GetMale, GetTransStr,false));
                            }));
                        }

                    }
                }
                if (GetFactionItem.Relations.Count > 0)
                {

                }
            }
        }

        public static void LoadPerks(EspReader Reader, YDListView View)
        {
            for (int i = 0; i < Reader.Perks.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Perks.ElementAt(i).Key;
                var GetPerkItem = Reader.Perks[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetPerkItem.Name);
                if (GetName.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetPerkItem.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("Perk", GetHashKey.ToString(), GenUniqueKey(GetPerkItem.EditorID,SetType).ToString(), GetName, GetTransStr,false));
                    }));
                }

                var GetDescription = ConvertHelper.ObjToStr(GetPerkItem.Description);
                if (GetDescription.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Description";
                        GetTransStr = TryGetTransData(GetPerkItem.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("Perk", GetHashKey.ToString(), GenUniqueKey(GetPerkItem.EditorID,SetType).ToString(), GetDescription, GetTransStr,false));
                    }));
                }
            }
        }

        public static void LoadWeapons(EspReader Reader, YDListView View)
        {
            for (int i = 0; i < Reader.Weapons.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Weapons.ElementAt(i).Key;
                var GetWeapon = Reader.Weapons[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetWeapon.Name);
                if (GetName.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetWeapon.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("Weapon", GetHashKey.ToString(), GenUniqueKey(GetWeapon.EditorID,SetType).ToString(), GetName, GetTransStr, false));
                    }));
                }

                var GetDescription = ConvertHelper.ObjToStr(GetWeapon.Description);
                if (GetDescription.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Description";
                        GetTransStr = TryGetTransData(GetWeapon.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("Weapon", GetHashKey.ToString(), GenUniqueKey(GetWeapon.EditorID,SetType).ToString(), GetDescription, GetTransStr, false));
                    }));
                }
            }
        }

        public static void LoadSoulGems(EspReader Reader, YDListView View)
        {
            for (int i = 0; i < Reader.SoulGems.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.SoulGems.ElementAt(i).Key;
                var GetSoulGem = Reader.SoulGems[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetSoulGem.Name);
                if (GetName.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetSoulGem.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("SoulGem", GetHashKey.ToString(), GenUniqueKey(GetSoulGem.EditorID,SetType).ToString(), GetName, GetTransStr, false));
                    }));
                }
            }
        }

        public static void LoadArmors(EspReader Reader, YDListView View)
        {
            for (int i = 0; i < Reader.Armors.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Armors.ElementAt(i).Key;
                var GetArmor = Reader.Armors[GetHashKey];            

                var GetName = ConvertHelper.ObjToStr(GetArmor.Name);
                if (GetName.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetArmor.EditorID,SetType);
                        View.AddRowR(UIHelper.CreatLine("Armor", GetHashKey.ToString(), GenUniqueKey(GetArmor.EditorID,SetType).ToString(), GetName, GetTransStr, false));
                    }));
                }

                var GetDescription = ConvertHelper.ObjToStr(GetArmor.Description);
                if (GetDescription.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Description";
                        GetTransStr = TryGetTransData(GetArmor.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("Armor", GetHashKey.ToString(), GenUniqueKey(GetArmor.EditorID,SetType).ToString(), GetDescription, GetTransStr, false));
                    }));
                }
            }
        }

        public static void LoadKeys(EspReader Reader, YDListView View)
        {
            for (int i = 0; i < Reader.Keys.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Keys.ElementAt(i).Key;
                var GetKey = Reader.Keys[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetKey.Name);
                if (GetName.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetKey.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("Key", GetHashKey.ToString(), GenUniqueKey(GetKey.EditorID,SetType).ToString(), GetName, GetTransStr, false));
                    }));
                }
            }
        }

        public static void LoadContainers(EspReader Reader, YDListView View)
        {
            for (int i = 0; i < Reader.Containers.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Containers.ElementAt(i).Key;
                var GetContainer = Reader.Containers[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetContainer.Name);
                if (GetName.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetContainer.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("Container", GetHashKey.ToString(), GenUniqueKey(GetContainer.EditorID,SetType).ToString(), GetName, GetTransStr, false));
                    }));
                }
            }
        }

        public static void LoadActivators(EspReader Reader, YDListView View)
        {
            for (int i = 0; i < Reader.Activators.Count; i++)
            {
                string GetTransStr = "";
                
                var GetHashKey = Reader.Activators.ElementAt(i).Key;
                var GetActivator = Reader.Activators[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetActivator.Name);
                if (GetName.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Name" + GetActivator;
                        GetTransStr = TryGetTransData(GetActivator.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("Activator", GetHashKey.ToString(), GenUniqueKey(GetActivator.EditorID,SetType).ToString(), GetName, GetTransStr, false));
                    }));
                }

                var GetActivateTextOverride = ConvertHelper.ObjToStr(GetActivator.ActivateTextOverride);
                if (GetActivateTextOverride.Trim().Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "ActivateTextOverride";
                        GetTransStr = TryGetTransData(GetActivator.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("Activator", GetHashKey.ToString(), GenUniqueKey(GetActivator.EditorID,SetType).ToString(), GetActivateTextOverride, GetTransStr, false));
                    }));
                }
            }
        }

        public static void LoadMiscItems(EspReader Reader, YDListView View)
        {
            for (int i = 0; i < Reader.MiscItems.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.MiscItems.ElementAt(i).Key;
                var GetMiscItem = Reader.MiscItems[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetMiscItem.Name);
                if (GetName.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetMiscItem.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("MiscItem", GetHashKey.ToString(), GenUniqueKey(GetMiscItem.EditorID,SetType).ToString(), GetName, GetTransStr, false));
                    }));
                }


            }
        }

        public static void LoadBooks(EspReader Reader, YDListView View)
        {
            for (int i = 0; i < Reader.Books.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Books.ElementAt(i).Key;
                var Books = Reader.Books[GetHashKey];

                //GetTransStr = Translator.TransData[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(Books.Name);
                if (GetName.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(Books.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("Book", GetHashKey.ToString(), GenUniqueKey(Books.EditorID,SetType).ToString(), GetName, GetTransStr, false));
                    }));
                }

                var GetDescription = ConvertHelper.ObjToStr(Books.Description);
                if (GetDescription.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Description";
                        GetTransStr = TryGetTransData(Books.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("Book", GetHashKey.ToString(), GenUniqueKey(Books.EditorID,SetType).ToString(), GetDescription, GetTransStr, false));
                    }));
                }

                var GetBookText = ConvertHelper.ObjToStr(Books.BookText);
                if (GetBookText.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "BookText";
                        GetTransStr = TryGetTransData(Books.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("Book", GetHashKey.ToString(), GenUniqueKey(Books.EditorID,SetType).ToString(), GetBookText, GetTransStr, false));
                    }));
                }
            }
        }

        public static void LoadMessages(EspReader Reader, YDListView View)
        {
            for (int i = 0; i < Reader.Messages.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Messages.ElementAt(i).Key;
                var GetMessageItem = Reader.Messages[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetMessageItem.Name);
                if (GetName.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetMessageItem.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("Message", GetHashKey.ToString(), GenUniqueKey(GetMessageItem.EditorID,SetType).ToString(), GetName, GetTransStr, false));
                    }));
                }

                var GetDescription = ConvertHelper.ObjToStr(GetMessageItem.Description);
                if (GetDescription.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Description";
                        GetTransStr = TryGetTransData(GetMessageItem.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("Message", GetHashKey.ToString(), GenUniqueKey(GetMessageItem.EditorID,SetType).ToString(), GetDescription, GetTransStr, false));
                    }));
                }
            }
        }

        public static void LoadMessageButtons(EspReader Reader, YDListView View)
        {
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
                                Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    string SetType = string.Format("Button[{0}]", ir);
                                    GetTransStr = TryGetTransData(GetMessageItem.EditorID, SetType);
                                    View.AddRowR(UIHelper.CreatLine("Button", GetHashKey.ToString(), GenUniqueKey(GetMessageItem.EditorID,SetType).ToString(), GetButton, GetTransStr, false));
                                }));
                            }
                        }
                    }
                }
            }
        }

        public static void LoadDialogTopics(EspReader Reader, YDListView View)
        {
            for (int i = 0; i < Reader.DialogTopics.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.DialogTopics.ElementAt(i).Key;
                var GetDialogTopicItem = Reader.DialogTopics[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetDialogTopicItem.Name);
                if (GetName.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetDialogTopicItem.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("DialogTopic", GetHashKey.ToString(), GenUniqueKey(GetDialogTopicItem.EditorID,SetType).ToString(), GetName, GetTransStr, false));
                    }));
                }

                var GetResponses = GetDialogTopicItem.Responses;
                int ForCount = 0;
                foreach (var GetChild in GetResponses)
                {
                    ForCount++;

                    foreach (var GetChildA in GetChild.Responses)
                    {
                        ForCount++;

                        string GetValue = ConvertHelper.ObjToStr(GetChildA.Text);
                        if (GetValue.Length > 0)
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                string SetType = string.Format("Response[{0}]", ForCount);
                                GetTransStr = TryGetTransData(GetDialogTopicItem.EditorID, SetType);
                                View.AddRowR(UIHelper.CreatLine("DialogTopic", GetHashKey.ToString(), GenUniqueKey(GetDialogTopicItem.EditorID,SetType).ToString(), GetValue, GetTransStr, false));
                            }));
                        }
                    }
                }

            }
        }

        public static void LoadSpells(EspReader Reader, YDListView View)
        {
            for (int i = 0; i < Reader.Spells.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Spells.ElementAt(i).Key;
                var GetSpellItem = Reader.Spells[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetSpellItem.Name);
                if (GetName.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetSpellItem.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("Spell", GetHashKey.ToString(), GenUniqueKey(GetSpellItem.EditorID,SetType).ToString(), GetName, GetTransStr, false));
                    }));
                }
            }
        }

        public static void LoadObjectEffects(EspReader Reader, YDListView View)
        {
            for (int i = 0; i < Reader.ObjectEffects.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.ObjectEffects.ElementAt(i).Key;
                var GetObjectEffect = Reader.ObjectEffects[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetObjectEffect.Name);
                if (GetName.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetObjectEffect.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("ObjectEffect", GetHashKey.ToString(), GenUniqueKey(GetObjectEffect.EditorID,SetType), GetName, GetTransStr, false));
                    }));
                }
            }
        }

        public static void LoadMagicEffects(EspReader Reader, YDListView View)
        {
            for (int i = 0; i < Reader.MagicEffects.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.MagicEffects.ElementAt(i).Key;
                var GetMagicEffect = Reader.MagicEffects[GetHashKey];

                var GetName = ConvertHelper.ObjToStr(GetMagicEffect.Name);
                if (GetName.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Name";
                        GetTransStr = TryGetTransData(GetMagicEffect.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("MagicEffect", GetHashKey.ToString(),GenUniqueKey(GetMagicEffect.EditorID, SetType), GetName, GetTransStr, false));
                    }));
                }

                var GetDescription = ConvertHelper.ObjToStr(GetMagicEffect.Description);
                if (GetDescription.Length > 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        string SetType = "Description";
                        GetTransStr = TryGetTransData(GetMagicEffect.EditorID, SetType);
                        View.AddRowR(UIHelper.CreatLine("MagicEffect", GetHashKey.ToString(), GenUniqueKey(GetMagicEffect.EditorID,SetType), GetDescription, GetTransStr, false));
                    }));
                }
            }
        }

        public static void LoadCells(EspReader Reader, YDListView View)
        {
            for (int i = 0; i < Reader.Cells.Count; i++)
            {
                string GetTransStr = "";

                var GetHashKey = Reader.Cells.ElementAt(i).Key;
                var GetCell = Reader.Cells[GetHashKey];
                int ForID = 0;
                foreach (var Get in GetCell.SubBlocks)
                {
                    ForID++;
                    foreach (var GetChild in Get.Cells)
                    {
                        ForID++;
                        var GetName = ConvertHelper.ObjToStr(GetChild.Name);
                        if (GetName.Length > 0)
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                string SetType = string.Format("Cell[{0}]",ForID);
                                GetTransStr = TryGetTransData(GetChild.EditorID, SetType);
                                View.AddRowR(UIHelper.CreatLine("Cell", GetHashKey.ToString(), GenUniqueKey(GetChild.EditorID,SetType), GetName, GetTransStr, false));
                            }));
                        }
                    }
                }
            }
        }
    }
}
