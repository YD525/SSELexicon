using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using SSELex.TranslateManage;
using SSELex.ConvertManager;
using SSELex.SkyrimManage;
using System.Windows.Media.Animation;
using SSELex.TranslateCore;
using System.Windows.Documents;
using System.Windows.Input;
using ICSharpCode.AvalonEdit;
using System.Windows.Media.TextFormatting;
using NexusMods.Paths.Trees;

namespace SSELex.UIManage
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class UIHelper
    {
        public static Grid SelectLine = null;
        public static int ModifyCount = 0;
        public static double DefLineHeight = 42;
        public static SolidColorBrush DefHeightBackground = new SolidColorBrush(Color.FromRgb(15, 15, 15));
        public static SolidColorBrush ExtendHeightBackground = new SolidColorBrush(Color.FromRgb(40, 40, 40));
        public static double DefFontSize = 15;


        public static double MeasureTextWidth(string Text, double FontSize, FontFamily FontFamily)
        {
            var Typeface = new Typeface(FontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);

            var FormattedText = new FormattedText(
                Text,
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                Typeface,
                FontSize,
                Brushes.Black,
                new NumberSubstitution(),
                1);

            return FormattedText.Width;
        }

        public static TextBox FakeTextBox = new TextBox();
        public static FakeGrid CreatFakeLine(string Type, string EditorID, string Key, string SourceText, string TransText, double Score)
        {
            if (DeFine.CurrentSearchStr.Length > 0)
            {
                int Find = 2;
                if (!SourceText.Contains(DeFine.CurrentSearchStr, StringComparison.OrdinalIgnoreCase))
                {
                    Find--;
                }
                if (!TransText.Contains(DeFine.CurrentSearchStr, StringComparison.OrdinalIgnoreCase))
                {
                    Find--;
                }
                if (Find == 0)
                {
                    return null;
                }
            }

            double AutoHeight = DefLineHeight;

            FontFamily FontFamily = FakeTextBox.FontFamily;

            double ActualTextWidth = MeasureTextWidth(SourceText, FakeTextBox.FontSize, FontFamily);

            var GetTextWidthRange = (DeFine.WorkingWin.ActualWidth / 3) - 120;

            if (ActualTextWidth > GetTextWidthRange)
            {
                AutoHeight = 82;
            }

            return new FakeGrid(AutoHeight,Type, EditorID, Key, SourceText, TransText, Score);
        }
        public static Grid CreatLine(FakeGrid Item)
        {
            Item.UPDataThis();
            return CreatLine(Item.Height, Item.Type, Item.EditorID, Item.Key, Item.SourceText, Item.TransText, Item.Score);
        }
        public static SolidColorBrush NoSelect = new SolidColorBrush(Color.FromRgb(30, 30, 30));
        public static SolidColorBrush Select = new SolidColorBrush(Color.FromRgb(68, 147, 228));
        public static Grid CreatLine(double Height,string Type, string EditorID, string Key, string SourceText, string TransText, double Score)
        {
            Grid MainGrid = new Grid();

            MainGrid.Height = Height;
            //Calc FontSize For Auto Height
            //Margin 25
            MainGrid.MouseLeave += MainGrid_MouseLeave;
            MainGrid.PreviewMouseDown += MainGrid_PreviewMouseDown;

            RowDefinition Row1st = new RowDefinition();
            Row1st.Height = new GridLength(1, GridUnitType.Star);
            RowDefinition Row2nd = new RowDefinition();
            Row2nd.Height = new GridLength(2, GridUnitType.Pixel);

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
            Footer.Height = 0.8;
            Footer.Opacity = 0.9;
            Footer.Tag = Key;
            if (!ActiveKey.Equals(Key))
            {
                Footer.Background = NoSelect;
            }
            else
            {
                Footer.Background = Select;
            }

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
            KeyBox.IsHitTestVisible = false;
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
            SourceTextBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            SourceTextBox.FocusVisualStyle = null;

            if (Height == 82)
            {
                MainGrid.Background = ExtendHeightBackground;
            }
            else
            {
                MainGrid.Background = DefHeightBackground;
            }

            if (DeFine.ViewMode == 1)
            {
                SourceTextBox.IsHitTestVisible = false;
                MainGrid.Cursor = Cursors.Hand;
            }

            var FindDictionary = YDDictionaryHelper.CheckDictionary(Key);

            if (FindDictionary != null)
            {
                if (FindDictionary.OriginalText.Trim().Length > 0)
                {
                    SourceTextBox.Text = FindDictionary.OriginalText;
                }
            }
            else
            {
                SourceTextBox.Text = SourceText;
            }

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
            TransTextBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            TransTextBox.FocusVisualStyle = null;
            if (TransText.Trim().Length == 0)
            {
                if (FindDictionary != null)
                {
                    if (FindDictionary.TransText.Trim().Length > 0)
                    {
                        TransTextBox.Text = FindDictionary.TransText;
                        TransTextBox.BorderBrush = new SolidColorBrush(Colors.Green);
                    }
                }
            }
            else
            {
                TransTextBox.Text = TransText;
            }

            TransTextBox.MouseLeave += TransTextBox_MouseLeave;
            TransTextBox.Tag = MainGrid;

            if (Score < 0)
            {
                TransTextBox.Background = new SolidColorBrush(Colors.Red);
                TransTextBox.IsReadOnly = true;
            }
            else
            if (Score < 5)
            {
                TransTextBox.Background = new SolidColorBrush(Colors.IndianRed);
            }

            if (DeFine.ViewMode == 1)
            {
                TransTextBox.IsReadOnly = true;
                TransTextBox.IsHitTestVisible = false;
                TransTextBox.Cursor = System.Windows.Input.Cursors.Hand;
            }

            Grid.SetRow(TransTextBox, 0);
            Grid.SetColumn(TransTextBox, 3);
            MainGrid.Children.Add(TransTextBox);

            return MainGrid;
        }

        private static void MainGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Grid)
            {
                Grid MainGrid = (Grid)sender;
                AutoSetTransData(GetMainGridKey(MainGrid), GetTransText(MainGrid));

                DeFine.WorkingWin.GetStatistics();
            }
        }

        public static void SetSelectLine(string Key)
        {
            if (DeFine.WorkingWin != null)
            {
                //DeFine.WorkingWin.TransViewList.VisibleRows
                for (int i=0;i< DeFine.WorkingWin.TransViewList.VisibleRows.Count;i++)
                {
                    Grid MainGrid = (Grid)DeFine.WorkingWin.TransViewList.VisibleRows[i];
                    Grid GetFooter = (Grid)MainGrid.Children[0];
                    string GetKey = ConvertHelper.ObjToStr(GetFooter.Tag);
                    if (GetKey.Equals(Key))
                    {
                        GetFooter.Background = Select;
                    }
                    else
                    {
                        GetFooter.Background = NoSelect;
                    }
                }
            }
         
        }

        public static string GetMainGridKey(Grid Sender)
        {
            Grid LockerGrid = Sender;
            Grid GetFooter = (Grid)LockerGrid.Children[0];
            string GetKey = ConvertHelper.ObjToStr(GetFooter.Tag);

            return GetKey;
        }

        public static string GetSourceText(Grid Sender)
        {
            Grid LockerGrid = Sender;
            TextBox GetText = (TextBox)LockerGrid.Children[3];
            return GetText.Text;
        }

        public static string GetTransText(Grid Sender)
        {
            Grid LockerGrid = Sender;
            TextBox TransText = (TextBox)LockerGrid.Children[4];
            return TransText.Text;
        }
    

        public static string ActiveType = "";
        public static string ActiveKey = "";
        public static TextBox ActiveTextBox = null;
        private static Grid LastSetGrid = null;
        private static void MainGrid_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is Grid)
            {
                Grid LockerGrid = (Grid)sender;
                if (LockerGrid.Children.Count >= 5)
                {
                    UIHelper.SelectLine = LockerGrid;
                    var MainGrid = LockerGrid;

                    var GetTag = ConvertHelper.ObjToStr((MainGrid.Children[1] as Label).Content);

                    var GetKey = GetMainGridKey(LockerGrid);
                    var SourceText = GetSourceText(LockerGrid);
                    var TransText = GetTransText(LockerGrid);

                    DeFine.WorkingWin.FromStr.Text = SourceText;
                    DeFine.WorkingWin.ToStr.Text = TransText;
                    DeFine.WorkingWin.ToStr.Tag = ConvertHelper.ObjToInt(MainGrid.Tag);

                    ActiveTextBox = (MainGrid.Children[4] as TextBox);
                    ActiveKey = GetKey;
                    ActiveType = GetTag;

                    SetSelectLine(GetKey);

                    if (DeFine.WorkingWin.CurrentTransType == 3)
                    {
                        if (DeFine.GlobalLocalSetting.ShowCode)
                        {
                            try
                            {
                                SelectLineFromIDE(GetKey);
                            }
                            catch { }
                        }
                    }
                }
            }

        }

        private static void TransTextBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var GetTextBox = (sender as TextBox);
            if (GetTextBox.Tag is Grid)
            {
                Grid MainGrid = (Grid)GetTextBox.Tag;

                AutoSetTransData(GetMainGridKey(MainGrid), GetTransText(MainGrid));
            }
        }

        public static void AutoSetTransData(string Key, string Data)
        {
            if (Data.Trim().Length > 0)
            {
                Translator.TransData[Key] = Data;
            }
            else
            {
                if (Translator.TransData.ContainsKey(Key))
                {
                    Translator.TransData.Remove(Key);
                }
            }
        }

        private static CancellationTokenSource AutoCancelSelectIDETrd;
        public static void CancelAutoSelect()
        {
            AutoCancelSelectIDETrd?.Cancel();
        }

        public static void SelectLineFromIDE(string GetKey)
        {
            try
            {
                if (AutoSelectIDETrd != null)
                {
                    CancelAutoSelect();
                }
            }
            catch { }

            AutoCancelSelectIDETrd = new CancellationTokenSource();
            var Token = AutoCancelSelectIDETrd.Token;

            AutoSelectIDETrd = new Thread(() =>
            {
                try
                {
                    string []Params = GetKey.Split(',');
                    if (Params.Length > 1)
                    {
                        Task.Delay(200, Token).Wait(Token);

                        Token.ThrowIfCancellationRequested();

                        foreach (var Item in DeFine.WorkingWin.GlobalPexReader.HeuristicEngine.DStringItems)
                        {
                            if (Item.Key.Contains(","))
                            {
                                if (Item.Key.Equals(Params[0]+","+ Params[1]))
                                {
                                    DeFine.ActiveIDE.Dispatcher.Invoke(() =>
                                    {
                                        int lineOffset = DeFine.ActiveIDE.Document.Text.IndexOf(Item.SourceLine);
                                        if (lineOffset == -1) return;

                                        int relativeOffset = Item.SourceLine.IndexOf("\"" + Item.Str + "\"");
                                        if (relativeOffset == -1) return;

                                        int absoluteOffset = lineOffset + relativeOffset;
                                        DeFine.ActiveIDE.ScrollToLine(DeFine.ActiveIDE.Document.GetLineByOffset(absoluteOffset).LineNumber);
                                        DeFine.ActiveIDE.Select(absoluteOffset, ("\"" + Item.Str + "\"").Length);
                                    });
                                }
                            }
                           
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    
                }
            });

            AutoSelectIDETrd.Start();
        }

        public static bool ExitAny = true;
        public static Thread AutoSelectIDETrd = null;
        public static void AnimateCanvasLeft(Grid targetGrid, double targetLeft, double durationInSeconds = 0.1)
        {
            double currentLeft = Canvas.GetLeft(targetGrid);

            DoubleAnimation animation = new DoubleAnimation
            {
                From = currentLeft,
                To = targetLeft,
                Duration = TimeSpan.FromSeconds(durationInSeconds),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut } 
            };

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            Storyboard.SetTarget(animation, targetGrid);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Canvas.Left)"));

            storyboard.Begin();
        }
    }
}
