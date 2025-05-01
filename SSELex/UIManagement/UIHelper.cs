using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using SSELex.TranslateManage;
using SSELex.ConvertManager;
using SSELex.SkyrimManage;
using System.Windows.Media.Animation;
using SSELex.TranslateCore;
using System.Windows.Documents;

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
        public static double DefFontSize = 15;
        public static double DefLineHeight = 40;
        public static SolidColorBrush DefHeightBackground = new SolidColorBrush(Color.FromRgb(15, 15, 15));
        public static SolidColorBrush ExtendHeightBackground = new SolidColorBrush(Color.FromRgb(40, 40, 40));


        public static Grid CreatLine(string Type, string EditorID, string Key, string SourceText, string TransText, bool Danger)
        {
            if (DeFine.CurrentSearchStr.Length > 0)
            {
                if (!SourceText.Contains(DeFine.CurrentSearchStr))
                {
                    return null;
                }
            }

            Grid MainGrid = new Grid();

            MainGrid.Tag = Key.ToString();

            MainGrid.Height = DefLineHeight;
            //Calc FontSize For Auto Height
            //Margin 25

            MainGrid.PreviewMouseDown += MainGrid_PreviewMouseDown;

            double TempFontSize = DefFontSize;

            var GetTextWidthRange = (DeFine.WorkingWin.ActualWidth / 3) - 25;

            var GetLang = LanguageHelper.DetectLanguageByLine(SourceText);
            if (GetLang != Languages.SimplifiedChinese && GetLang != Languages.TraditionalChinese)
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
            Footer.Height = 0.8;
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
            SourceTextBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            SourceTextBox.PreviewMouseDown += SourceTextBox_PreviewMouseDown;

            var FindDictionary = YDDictionaryHelper.CheckDictionary(Key);

            if (FindDictionary != null)
            {
                if (FindDictionary.OriginalText.Trim().Length > 0)
                {
                    SourceTextBox.Text = FindDictionary.OriginalText;
                    if (FindDictionary.TransText.Trim().Length > 0)
                    {
                        UIHelper.ModifyCount++;
                    }
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
            TransTextBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

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

            TransTextBox.MouseEnter += TransTextBox_MouseEnter;
            TransTextBox.MouseLeave += TransTextBox_MouseLeave;
            TransTextBox.LostFocus += TransTextBox_LostFocus;
            TransTextBox.Tag = MainGrid;

            TransTextBox.MouseLeave += TransTextBox_MouseLeave1;

            if (DeFine.ViewMode == 1)
            {
                TransTextBox.IsReadOnly = true;
                TransTextBox.Cursor = System.Windows.Input.Cursors.Hand;
            }

            Grid.SetRow(TransTextBox, 0);
            Grid.SetColumn(TransTextBox, 3);
            MainGrid.Children.Add(TransTextBox);

            return MainGrid;
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
                    var GetTilp = ConvertHelper.ObjToStr(MainGrid.ToolTip);
                    var GetTag = ConvertHelper.ObjToStr((MainGrid.Children[1] as Label).Content);
                    var GetKey = ConvertHelper.ObjToStr((MainGrid.Children[2] as TextBox).Text);
                    var GetSourceText = (MainGrid.Children[3] as TextBox).Text.Trim();
                    var TransText = ConvertHelper.ObjToStr((MainGrid.Children[4] as TextBox).Text);
                    DeFine.WorkingWin.FromStr.Text = GetSourceText;
                    DeFine.WorkingWin.ToStr.Text = TransText;
                    DeFine.WorkingWin.ToStr.Tag = ConvertHelper.ObjToInt(MainGrid.Tag);

                    ActiveTextBox = (MainGrid.Children[4] as TextBox);
                    ActiveKey = GetKey;
                    ActiveType = GetTag;
                    try
                    {
                        if (LastSetGrid != null)
                        {
                            if (LastSetGrid.Children.Count > 0)
                            {
                                if (LastSetGrid.Children[0] is Grid)
                                {
                                    Grid GetFooter = ((Grid)LastSetGrid.Children[0]);
                                    GetFooter.Opacity = 0.9;
                                    GetFooter.Background = new SolidColorBrush(Color.FromRgb(30, 30, 30));
                                }
                            }
                        }

                        if (LockerGrid.Children.Count > 0)
                        {
                            if (LockerGrid.Children[0] is Grid)
                            {
                                Grid GetFooter = ((Grid)LockerGrid.Children[0]);
                                GetFooter.Opacity = 1;
                                GetFooter.Background = new SolidColorBrush(Colors.Yellow);

                                LastSetGrid = LockerGrid;
                            }
                        }
                    }
                    catch { }
                }
            }

        }

        private static void TransTextBox_MouseLeave1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var GetTextBox = (sender as TextBox);
            var GetKey = ConvertHelper.ObjToStr((GetTextBox.Tag as Grid).Tag);

            AutoSetTransData(GetKey, GetTextBox.Text);
        }

        public static Dictionary<string, int> ModifyCountCache = new Dictionary<string, int>();

        private static void TransTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var GetKey = ConvertHelper.ObjToStr(((sender as TextBox).Tag as Grid).Tag);

            if ((sender as TextBox).Text.Length > 0)
            {
                AutoSetTransData(GetKey, (sender as TextBox).Text);

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

        private static void SourceTextBox_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private static void TransTextBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is TextBox)
            {
                var GetKey = ConvertHelper.ObjToStr(((sender as TextBox).Tag as Grid).Tag);
                var GetToolTipStr = ConvertHelper.ObjToStr((sender as TextBox).Tag as Grid);
                var LockerGrid = ((sender as TextBox).Tag as Grid);
                (sender as TextBox).Foreground = new SolidColorBrush(Colors.White);
                (LockerGrid.Children[3] as TextBox).Foreground = new SolidColorBrush(Colors.White);

                if (LockerGrid.Height != DefLineHeight)
                {
                    if (GetToolTipStr.Length == 0)
                        LockerGrid.Background = ExtendHeightBackground;
                }
                else
                {
                    if (GetToolTipStr.Length == 0)
                        LockerGrid.Background = DefHeightBackground;
                }

                if ((sender as TextBox).Text.Length > 0)
                {
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
                    AutoSetTransData(GetKey, Text);
                }
            }
        }

        private static CancellationTokenSource AutoCancelSelectIDETrd;
        public static void CancelAutoSelect()
        {
            AutoCancelSelectIDETrd?.Cancel();
        }

        public static bool ExitAny = true;
        public static Thread AutoSelectIDETrd = null;
        private static void TransTextBox_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is TextBox)
            {
                var LockerGrid = ((sender as TextBox).Tag as Grid);
                var GetToolTipStr = ConvertHelper.ObjToStr(((sender as TextBox).Tag as Grid));
                (sender as TextBox).Foreground = new SolidColorBrush(Colors.Orange);
                (LockerGrid.Children[3] as TextBox).Foreground = new SolidColorBrush(Colors.Yellow);

                if (GetToolTipStr.Length == 0)
                    LockerGrid.Background = new SolidColorBrush(Color.FromRgb(55, 55, 55));

                if (DeFine.WorkingWin.CurrentTransType == 3)
                {
                    if (DeFine.GlobalLocalSetting.ShowCode)
                    {
                        try
                        {
                            string GetKey = ConvertHelper.ObjToStr((LockerGrid.Children[2] as TextBox).Text);
                            if (GetKey.Contains("-"))
                            {
                                var GetStr = GetKey.Split('-')[3];
                                if (GetStr.Contains("("))
                                {
                                    GetStr = GetStr.Split("(")[0];
                                }
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
                                        // 可取消的等待（替代 Thread.Sleep）
                                        Task.Delay(200, Token).Wait(Token);

                                        Token.ThrowIfCancellationRequested();

                                        DeFine.ActiveIDE.Dispatcher.Invoke(() =>
                                        {
                                            DeFine.ActiveIDE.ScrollToLine(ConvertHelper.ObjToInt(GetStr));
                                            DeFine.ActiveIDE.Select(ConvertHelper.ObjToInt(GetKey.Split('-')[1]), ConvertHelper.ObjToInt(GetKey.Split('-')[2].Split('(')[0]));
                                        });
                                    }
                                    catch (OperationCanceledException)
                                    {
                                        // 取消时静默处理
                                    }
                                });

                                AutoSelectIDETrd.Start();
                            }
                        }
                        catch { }
                    }
                }
            }
        }

        public static void AnimateCanvasLeft(Grid targetGrid, double targetLeft, double durationInSeconds = 0.1)
        {
            // 获取当前 Grid 的左边距
            double currentLeft = Canvas.GetLeft(targetGrid);

            // 创建一个 DoubleAnimation 动画，目标值是目标 Left 值
            DoubleAnimation animation = new DoubleAnimation
            {
                From = currentLeft,
                To = targetLeft,
                Duration = TimeSpan.FromSeconds(durationInSeconds),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut } // 可选：平滑的缓动
            };

            // 使用 Storyboard 来应用动画
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            Storyboard.SetTarget(animation, targetGrid);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Canvas.Left)"));

            // 启动动画
            storyboard.Begin();
        }
    }
}
