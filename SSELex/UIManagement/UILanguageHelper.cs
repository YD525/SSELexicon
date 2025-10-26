using SharpVectors.Converters;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SSELex.ConvertManager;
using PhoenixEngine.TranslateCore;
using SSELex.SkyrimManage;
using System.IO;

namespace SSELex.UIManage
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class UIItem
    {
        public Dictionary<Languages, string> Contents;
        public Dictionary<Languages, string> ToolTips;
        public object Control;

        public UIItem(object Control, Dictionary<Languages, string> Contents, Dictionary<Languages, string> ToolTips)
        {
            this.Control = Control;
            this.Contents = Contents;
            this.ToolTips = ToolTips;
        }

        public string QueryContent(Languages SetLanguage)
        {
            if (Contents.ContainsKey(SetLanguage))
            {
                return Contents[SetLanguage];
            }
            return string.Empty;
        }

        public string QueryToolTip(Languages SetLanguage)
        {
            if (ToolTips.ContainsKey(SetLanguage))
            {
                return ToolTips[SetLanguage];
            }
            return string.Empty;
        }

        public void Apply(Languages SetLanguage)
        {
            var GetToolTip = QueryToolTip(SetLanguage);
            var GetContent = QueryContent(SetLanguage);

            if (Control is SvgViewbox)
            {
                SvgViewbox GetSvgViewbox = (SvgViewbox)Control;
                GetSvgViewbox.ToolTip = GetToolTip;
            }
            else
            if (Control is ComboBox)
            {
                ComboBox GetComboBox = (ComboBox)Control;
                GetComboBox.ToolTip = GetToolTip;
            }
            else
            if (Control is Button)
            {
                Button GetButton = (Button)Control;
                GetButton.ToolTip = GetToolTip;
                GetButton.Content = GetContent;
            }
            else
            if (Control is TextBox)
            {
                TextBox GetTextBox = (TextBox)Control;
                GetTextBox.ToolTip = GetToolTip;
            }
            else
            if (Control is Label)
            {
                Label GetLabel = (Label)Control;
                GetLabel.ToolTip = GetToolTip;
                GetLabel.Content = GetContent;

            }
            else
            if (Control is Grid)
            {
                Grid GetGrid = (Grid)Control;
                GetGrid.ToolTip = GetToolTip;
            }
            else
            if (Control is StackPanel)
            {
                Grid GetStackPanel = (Grid)Control;
                GetStackPanel.ToolTip = GetToolTip;
            }
        }
    }



    public class UILanguageHelper
    {
        public static List<UIItem> UIItems = new List<UIItem>();

        public static List<Languages> SupportLanguages = new List<Languages>
        {
            Languages.Auto,

            Languages.English,
            Languages.Japanese,
            Languages.German,

            Languages.Portuguese,
            Languages.Brazilian,

            Languages.French,
            Languages.CanadianFrench,

            Languages.SimplifiedChinese,
            Languages.TraditionalChinese,
            
            Languages.Hindi,
            Languages.Indonesian,
            Languages.Italian,
            Languages.Korean,
            Languages.Polish,
            Languages.Russian,
            Languages.Spanish,
            Languages.Turkish,
            Languages.Urdu,
            Languages.Ukrainian,
            Languages.Vietnamese
        };

        public static class VisualTreeHelperExtensions
        {
            public static List<T> GetAllChildren<T>(DependencyObject parent) where T : DependencyObject
            {
                List<T> result = new List<T>();

                if (parent == null)
                    return result;

                int count = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < count; i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                    if (child is T tChild)
                    {
                        result.Add(tChild);
                    }
                    result.AddRange(GetAllChildren<T>(child));
                }

                return result;
            }
        }


        public static void UPDateUI(string Key, string Value)
        {
            switch (Key)
            {
                case "﻿UINormal":
                    {
                        DeFine.WorkingWin.UINormal.Content = Value;
                    }
                break;
                case "UIQuick":
                    {
                        DeFine.WorkingWin.UIQuick.Content = Value;
                    }
                break;
                case "UITypeSelector":
                    {
                        DeFine.WorkingWin.UITypeSelector.Content = Value;
                    }
                break;
                case "LoadFileButton":
                    {
                        DeFine.WorkingWin.LoadFileButton.Content = Value;
                    }
                break;
            }
        }
        public static Dictionary<string, string> UICache = new Dictionary<string, string>();
        public static void ChangeLanguage(Languages SetLanguage)
        {
            UICache.Clear();
            string SetPath = DeFine.GetFullPath(@"\Interface\Translations\SSE Lexicon_" + SetLanguage.ToString().ToUpper() + ".txt");
            MCMReader NewReader = new MCMReader();
            if (File.Exists(SetPath))
            {
                NewReader.LoadMCM(SetPath);

                foreach (var GetMCMItem in NewReader.MCMItems)
                {
                    UPDateUI(GetMCMItem.EditorID,GetMCMItem.SourceText);
                    if (!UICache.ContainsKey(GetMCMItem.EditorID))
                    {
                        UICache.Add(GetMCMItem.EditorID,GetMCMItem.SourceText);
                    }
                }
            }

        }
    }
}
