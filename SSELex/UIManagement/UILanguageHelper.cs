using SharpVectors.Converters;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SSELex.ConvertManager;
using SSELex.TranslateCore;

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
            Languages.SimplifiedChinese,
            Languages.Korean,
            Languages.Turkish,
            Languages.Brazilian,
            Languages.Russian,
            Languages.TraditionalChinese,
            Languages.Italian,
            Languages.Spanish,
            Languages.Hindi,
            Languages.Urdu,
            Languages.Indonesian,
            Languages.French,
            Languages.Vietnamese,
            Languages.Polish
        };

        public static string DBQueryStr(Languages Type, string Key, string Name)
        {
            string SqlOrder = "Select Str From UILanguages Where Key = '{0}' And Name = '{1}' And LangType = '{2}'";
            string GetResult = ConvertHelper.ObjToStr(DeFine.GlobalDB.ExecuteScalar(string.Format(SqlOrder, Key, Name, Type.ToString())));
            if (GetResult.Contains(">"))
            {
                GetResult = GetResult.Split('>')[0];
            }
            return ConvertHelper.ObjToStr(GetResult);
        }

        public static string SearchStateChangeStr(string Name, int ID)
        {
            string SqlOrder = "Select Str From UILanguages Where Name = '{0}' And LangType = '{1}'";
            string GetResult = ConvertHelper.ObjToStr(DeFine.GlobalDB.ExecuteScalar(string.Format(SqlOrder, Name, DeFine.GlobalLocalSetting.CurrentUILanguage.ToString())));
            if (GetResult.Contains(">"))
            {
                GetResult = GetResult.Split('>')[ID];
            }
            return ConvertHelper.ObjToStr(GetResult);
        }

        public static Dictionary<Languages, string> QueryLanguageStr(string Name, string Key)
        {
            Dictionary<Languages, string> SetHashMap = new Dictionary<Languages, string>();

            foreach (var Get in SupportLanguages)
            {
                SetHashMap.Add(Get, DBQueryStr(Get, Key, Name));
            }
            return SetHashMap;
        }

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


        public static void ChangeLanguage(Languages SetLanguage)
        {
            foreach (var GetLabel in VisualTreeHelperExtensions.GetAllChildren<ComboBox>(DeFine.WorkingWin))
            {
                string GetStr = DBQueryStr(SetLanguage, "ToolTips", GetLabel.Name);
                if (GetStr.Trim().Length > 0)
                {
                    GetLabel.ToolTip = GetStr;
                }
            }
            foreach (var GetLabel in VisualTreeHelperExtensions.GetAllChildren<SvgViewbox>(DeFine.WorkingWin))
            {
                string GetStr = DBQueryStr(SetLanguage, "ToolTips", GetLabel.Name);
                if (GetStr.Trim().Length > 0)
                {
                    GetLabel.ToolTip = GetStr;
                }
            }
            foreach (var GetLabel in VisualTreeHelperExtensions.GetAllChildren<Border>(DeFine.WorkingWin))
            {
                string GetStr = DBQueryStr(SetLanguage, "ToolTips", GetLabel.Name);
                if (GetStr.Trim().Length > 0)
                {
                    GetLabel.ToolTip = GetStr;
                }
            }
            foreach (var GetLabel in VisualTreeHelperExtensions.GetAllChildren<Label>(DeFine.WorkingWin))
            {
                string GetStr = DBQueryStr(SetLanguage, "ToolTips", GetLabel.Name);
                if (GetStr.Trim().Length > 0)
                {
                    GetLabel.ToolTip = GetStr;
                }
                GetStr = DBQueryStr(SetLanguage, "Content", GetLabel.Name);
                if (GetStr.Trim().Length > 0)
                {
                    GetLabel.Content = GetStr;
                }
            }
            foreach (var GetLabel in VisualTreeHelperExtensions.GetAllChildren<Button>(DeFine.WorkingWin))
            {
                string GetStr = DBQueryStr(SetLanguage, "ToolTips", GetLabel.Name);
                if (GetStr.Trim().Length > 0)
                {
                    GetLabel.ToolTip = GetStr;
                }
                GetStr = DBQueryStr(SetLanguage, "Content", GetLabel.Name);
                if (GetStr.Trim().Length > 0)
                {
                    GetLabel.Content = GetStr;
                }
            }
            foreach (var GetLabel in VisualTreeHelperExtensions.GetAllChildren<TextBox>(DeFine.WorkingWin))
            {
                string GetStr = DBQueryStr(SetLanguage, "ToolTips", GetLabel.Name);
                if (GetStr.Trim().Length > 0)
                {
                    GetLabel.ToolTip = GetStr;
                }
            }
        }
    }
}
