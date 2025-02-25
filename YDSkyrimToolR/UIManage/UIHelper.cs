using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using Mutagen.Bethesda.Fallout4;
using Mutagen.Bethesda.Skyrim;
using System.Runtime.CompilerServices;
using YDSkyrimToolR.TranslateManage;
using Noggog;
using YDSkyrimToolR.ConvertManager;
using YDSkyrimToolR.SkyrimManage;

namespace YDSkyrimToolR.UIManage
{
    /*
    * @Author: 约定
    * @GitHub: https://github.com/tolove336/YDSkyrimToolR
    * @Date: 2025-02-06
    */
    public class UIHelper
    {
        public static int ModifyCount = 0;
        public static double DefFontSize = 15;
        public static double DefLineHeight = 40;
        public static SolidColorBrush DefHeightBackground = new SolidColorBrush(Color.FromRgb(15, 15, 15));
        public static SolidColorBrush ExtendHeightBackground = new SolidColorBrush(Color.FromRgb(40, 40, 40));

        public static Grid CreatLine(string Type, string EditorID, string Key, string SourceText, string TransText,bool Danger)
        {
            Grid MainGrid = new Grid();

            MainGrid.Tag = Key.GetHashCode().ToString();

            MainGrid.Height = DefLineHeight;
            //Calc FontSize For Auto Height
            //Margin 25

            double TempFontSize = DefFontSize;

            var GetTextWidthRange = (DeFine.WorkingWin.ActualWidth / 3) - 25;


            if (!StrChecker.ContainsChinese(SourceText))
            {
                TempFontSize = 8;
            }
            var GetTextSize = (SourceText.Length * TempFontSize);
            if (GetTextSize > GetTextWidthRange)
            {
                MainGrid.Height = 80;
                MainGrid.Background = ExtendHeightBackground;
            }
            else
            {
                MainGrid.Background = DefHeightBackground;
            }

            if (Danger)
            {
                MainGrid.Background = new SolidColorBrush(Colors.Red);
                MainGrid.ToolTip = "Danger";
            }

            RowDefinition Row1st = new RowDefinition();
            Row1st.Height = new GridLength(1, GridUnitType.Star);
            RowDefinition Row2nd = new RowDefinition();
            Row2nd.Height = new GridLength(1, GridUnitType.Pixel);

            MainGrid.RowDefinitions.Add(Row1st);
            MainGrid.RowDefinitions.Add(Row2nd);

            ColumnDefinition Column1st = new ColumnDefinition();
            Column1st.Width = new GridLength(0.35, GridUnitType.Star);
            //ColumnDefinition Column2nd = new ColumnDefinition();
            //Column2nd.Width = new GridLength(0.3, GridUnitType.Star);
            ColumnDefinition Column3rd = new ColumnDefinition();
            Column3rd.Width = new GridLength(0.5, GridUnitType.Star);
            ColumnDefinition Column5th = new ColumnDefinition();
            Column5th.Width = new GridLength(1, GridUnitType.Star);
            ColumnDefinition Column6th = new ColumnDefinition();
            Column6th.Width = new GridLength(1, GridUnitType.Star);

            MainGrid.ColumnDefinitions.Add(Column1st);
            //MainGrid.ColumnDefinitions.Add(Column2nd);
            MainGrid.ColumnDefinitions.Add(Column3rd);
            MainGrid.ColumnDefinitions.Add(Column5th);
            MainGrid.ColumnDefinitions.Add(Column6th);

            Grid Footer = new Grid();
            Footer.Height = 1;
            Footer.Opacity = 0.9;
            Footer.Background = new SolidColorBrush(Color.FromRgb(30, 30, 30));
            Grid.SetColumn(Footer, 0);
            Grid.SetColumnSpan(Footer, 5);
            Grid.SetRow(Footer, 1);
            MainGrid.Children.Add(Footer);

            Label TypeBox = new Label();
            TypeBox.Height = MainGrid.Height - 10;
            TypeBox.Margin = new Thickness(10, 0, 10, 0);
            TypeBox.FontSize = DefFontSize;
            TypeBox.VerticalAlignment = VerticalAlignment.Center;
            TypeBox.BorderBrush = new SolidColorBrush(Color.FromRgb(76, 76, 76));
            TypeBox.Background = null;
            TypeBox.BorderThickness = new Thickness(1);
            TypeBox.Foreground = new SolidColorBrush(Colors.White);
            TypeBox.VerticalContentAlignment = VerticalAlignment.Center;
            TypeBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            TypeBox.Content = Type;
            Grid.SetRow(TypeBox, 0);
            Grid.SetColumn(TypeBox, 0);
            MainGrid.Children.Add(TypeBox);

            //Label EditorIDBox = new Label();
            //EditorIDBox.Height = 30;
            //EditorIDBox.Margin = new Thickness(10, 0, 10, 0);
            //EditorIDBox.FontSize = DefFontSize;
            //EditorIDBox.VerticalAlignment = VerticalAlignment.Center;
            //EditorIDBox.BorderBrush = new SolidColorBrush(Color.FromRgb(76, 76, 76));
            //EditorIDBox.Background = null;
            //EditorIDBox.BorderThickness = new Thickness(1);
            //EditorIDBox.Foreground = new SolidColorBrush(Colors.White);
            //EditorIDBox.VerticalContentAlignment = VerticalAlignment.Center;
            //EditorIDBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            //EditorIDBox.Content = EditorID;
            //Grid.SetRow(EditorIDBox, 0);
            //Grid.SetColumn(EditorIDBox, 1);
            //MainGrid.Children.Add(EditorIDBox);

            TextBox KeyBox = new TextBox();
            KeyBox.IsReadOnly = true;
            KeyBox.IsTabStop = false;
            KeyBox.Height = MainGrid.Height - 10;
            KeyBox.Margin = new Thickness(10, 0, 10, 0);
            KeyBox.FontSize = DefFontSize;
            KeyBox.VerticalAlignment = VerticalAlignment.Center;
            KeyBox.BorderBrush = new SolidColorBrush(Color.FromRgb(76, 76, 76));
            KeyBox.Background = null;
            KeyBox.BorderThickness = new Thickness(1);
            KeyBox.Foreground = new SolidColorBrush(Colors.White);
            KeyBox.VerticalContentAlignment = VerticalAlignment.Center;
            KeyBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            KeyBox.CaretBrush = new SolidColorBrush(Colors.White);
            KeyBox.AcceptsReturn = true;
            KeyBox.TextWrapping = TextWrapping.Wrap;
            KeyBox.Text = Key;
            Grid.SetRow(KeyBox, 0);
            Grid.SetColumn(KeyBox, 1);
            MainGrid.Children.Add(KeyBox);


            TextBox SourceTextBox = new TextBox();
            SourceTextBox.Height = MainGrid.Height - 10;
            SourceTextBox.IsTabStop = false;
            SourceTextBox.IsReadOnly = true;
            SourceTextBox.Margin = new Thickness(10, 0, 10, 0);
            SourceTextBox.FontSize = DefFontSize;
            SourceTextBox.VerticalAlignment = VerticalAlignment.Center;
            SourceTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(76, 76, 76));
            SourceTextBox.Background = null;
            SourceTextBox.SelectionBrush = new SolidColorBrush(Color.FromRgb(17, 145, 243));
            SourceTextBox.BorderThickness = new Thickness(1);
            SourceTextBox.Foreground = new SolidColorBrush(Colors.White);
            SourceTextBox.VerticalContentAlignment = VerticalAlignment.Center;
            SourceTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            SourceTextBox.CaretBrush = new SolidColorBrush(Colors.White);
            SourceTextBox.AcceptsReturn = true;
            SourceTextBox.TextWrapping = TextWrapping.Wrap;
            SourceTextBox.PreviewMouseDown += SourceTextBox_PreviewMouseDown;
            SourceTextBox.Text = SourceText;
            Grid.SetRow(SourceTextBox, 0);
            Grid.SetColumn(SourceTextBox, 2);
            MainGrid.Children.Add(SourceTextBox);

            TextBox TransTextBox = new TextBox();
            TransTextBox.Height = MainGrid.Height - 10;
            TransTextBox.Margin = new Thickness(10, 0, 10, 0);
            TransTextBox.FontSize = DefFontSize;
            TransTextBox.VerticalAlignment = VerticalAlignment.Center;
            TransTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(87, 87, 87));
            if (TransText.Length > 0)
            {
                TransTextBox.BorderBrush = new SolidColorBrush(Colors.Green);
                if (UIHelper.ModifyCount < DeFine.WorkingWin.MaxTransCount)
                {
                    ModifyCount++;
                }
            }
            TransTextBox.Background = null;
            TransTextBox.SelectionBrush = new SolidColorBrush(Color.FromRgb(17, 145, 243));
            TransTextBox.BorderThickness = new Thickness(1);
            TransTextBox.Foreground = new SolidColorBrush(Colors.White);
            TransTextBox.VerticalContentAlignment = VerticalAlignment.Center;
            TransTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            TransTextBox.CaretBrush = new SolidColorBrush(Colors.White);
            TransTextBox.AcceptsReturn = true;
            TransTextBox.TextWrapping = TextWrapping.Wrap;
            TransTextBox.Text = TransText;
            TransTextBox.MouseEnter += TransTextBox_MouseEnter;
            TransTextBox.MouseLeave += TransTextBox_MouseLeave;
            TransTextBox.LostFocus += TransTextBox_LostFocus;
            TransTextBox.Tag = MainGrid;

            TransTextBox.MouseLeave += TransTextBox_MouseLeave1; ;
            Grid.SetRow(TransTextBox, 0);
            Grid.SetColumn(TransTextBox, 3);
            MainGrid.Children.Add(TransTextBox);

            return MainGrid;
        }

        private static void TransTextBox_MouseLeave1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var GetTextBox = (sender as TextBox);
            var GetKey = ConvertHelper.ObjToInt((GetTextBox.Tag as Grid).Tag);
            if (GetTextBox.Text.Length > 0)
            {
                Translator.TransData[GetKey] = GetTextBox.Text;
            }
        }

        public static Dictionary<int, int> ModifyCountCache = new Dictionary<int, int>();

        private static void TransTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var GetKey = ConvertHelper.ObjToInt(((sender as TextBox).Tag as Grid).Tag);

            if ((sender as TextBox).Text.Length > 0)
            {
                Translator.TransData[GetKey] = (sender as TextBox).Text;

                if ((sender as TextBox).BorderBrush != new SolidColorBrush(Colors.BlueViolet))
                (sender as TextBox).BorderBrush = new SolidColorBrush(Colors.Green);

                int GetGridModifyCount = 0;

                if (!ModifyCountCache.ContainsKey(GetKey))
                {
                    ModifyCountCache.Add(GetKey, 1);
                    GetGridModifyCount = 1;
                }

                if (GetGridModifyCount > 0)
                {
                    if (UIHelper.ModifyCount < DeFine.WorkingWin.MaxTransCount)
                    {
                        UIHelper.ModifyCount++;
                    }

                    if (DeFine.WorkingWin != null)
                    {
                        DeFine.WorkingWin.GetStatistics();
                    }
                }
                else
                {

                }
            }
            else
            {
                if (ModifyCountCache.ContainsKey(GetKey))
                {
                    ModifyCountCache.Remove(GetKey);
                    UIHelper.ModifyCount--;
                    DeFine.WorkingWin.GetStatistics();
                }

                (sender as TextBox).BorderBrush = new SolidColorBrush(Color.FromRgb(87, 87, 87));
            }
        }

        private static void SourceTextBox_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is TextBox)
            {
                (sender as TextBox).SelectAll();
                Clipboard.SetText((sender as TextBox).Text.Trim());
            }
        }

        private static void TransTextBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is TextBox)
            {
                var GetKey = ConvertHelper.ObjToInt(((sender as TextBox).Tag as Grid).Tag);
                var GetToolTipStr = ConvertHelper.ObjToStr((sender as TextBox).Tag as Grid);
                var LockerGrid = ((sender as TextBox).Tag as Grid);
                (sender as TextBox).Foreground = new SolidColorBrush(Colors.White);
                (LockerGrid.Children[3] as TextBox).Foreground = new SolidColorBrush(Colors.White);

                if (LockerGrid.Height != DefLineHeight)
                {
                    if(GetToolTipStr.Length == 0)
                    LockerGrid.Background = ExtendHeightBackground;
                }
                else
                {
                    if (GetToolTipStr.Length == 0)
                    LockerGrid.Background = DefHeightBackground;
                }

                if ((sender as TextBox).Text.Length > 0)
                {
                    if ((sender as TextBox).BorderBrush != new SolidColorBrush(Colors.BlueViolet))
                    (sender as TextBox).BorderBrush = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    (sender as TextBox).BorderBrush = new SolidColorBrush(Color.FromRgb(87, 87, 87));
                }

                if ((sender as TextBox).Text.Length == 0)
                {
                    if (ModifyCountCache.ContainsKey(GetKey))
                    {
                        ModifyCountCache.Remove(GetKey);
                        UIHelper.ModifyCount--;
                        DeFine.WorkingWin.GetStatistics();
                    }
                }
                string Text = (sender as TextBox).Text;
                if (Text.Trim().Length > 0)
                {
                    Translator.TransData[GetKey] = Text;
                }
            }
        }

        private static void TransTextBox_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is TextBox)
            {
                var LockerGrid = ((sender as TextBox).Tag as Grid);
                var GetToolTipStr = ConvertHelper.ObjToStr(((sender as TextBox).Tag as Grid));
                (sender as TextBox).Foreground = new SolidColorBrush(Colors.Orange);
                (LockerGrid.Children[3] as TextBox).Foreground = new SolidColorBrush(Colors.Yellow);

                if(GetToolTipStr.Length==0)
                LockerGrid.Background = new SolidColorBrush(Color.FromRgb(55, 55, 55));

                if (DeFine.WorkingWin.CurrentTransType == 3)
                {
                    try
                    {
                        string GetKey = ConvertHelper.ObjToStr((LockerGrid.Children[2] as TextBox).Text);
                        if (GetKey.Contains("-"))
                        {
                            DeFine.ActiveIDE.ScrollToLine(ConvertHelper.ObjToInt(GetKey.Split('-')[0]));
                            int GetEnd = 0;

                            DeFine.ActiveIDE.Select(ConvertHelper.ObjToInt(GetKey.Split('-')[1]), ConvertHelper.ObjToInt(GetKey.Split('-')[2].Split('(')[0]));
                        }
                    }
                    catch { }
                }

                DeFine.DefTransTool.QuickSearchStr((LockerGrid.Children[3] as TextBox).Text);
            }
        }
    }
}
