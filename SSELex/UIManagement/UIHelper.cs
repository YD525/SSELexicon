using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using SSELex.TranslateManage;
using SSELex.ConvertManager;
using SSELex.SkyrimManage;
using System.Windows.Media.Animation;
using System.Windows.Input;
using System.Windows.Shapes;
using static SSELex.TranslateManage.TranslatorExtend;

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
        public static FakeGrid CreatFakeLine(string Type,string Key, string SourceText, string TransText, double Score)
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

            var GetTextWidthRange = (DeFine.WorkingWin.ActualWidth / 3) - 135;

            if (ActualTextWidth > GetTextWidthRange)
            {
                AutoHeight = 82;
            }

            return new FakeGrid(AutoHeight, Type, Key, SourceText, TransText, Score);
        }
        public static Grid CreatLine(FakeGrid Item)
        {
            return CreatLine(Item.Height, Item.Type,Item.Key, Item.SourceText, Item.TransText, Item.Score);
        }
        public static SolidColorBrush NoSelect = new SolidColorBrush(Color.FromRgb(30, 30, 30));
        public static SolidColorBrush Select = new SolidColorBrush(Color.FromRgb(68, 147, 228));
        public static Grid CreatLine(double Height, string Type,string Key, string SourceText, string TransText, double Score)
        {
            Grid MainGrid = DeFine.RowStyleWin.CreatLine(Height, new PhoenixEngine.TranslateManage.TranslationUnit(DeFine.CurrentModName, Key, Type, SourceText, TransText));
            return MainGrid;
        }

        public static string GetMainGridKey(Grid Sender)
        {
            Grid LockerGrid = Sender;
            Border MainBorder = (Border)LockerGrid.Children[0];
            string GetKey = ConvertHelper.ObjToStr(MainBorder.Tag);

            return GetKey;
        }

        public static string GetOriginalText(Grid Sender)
        {
            Grid LockerGrid = Sender;
            TextBox GetText = (TextBox)LockerGrid.Children[3];
            return ConvertHelper.ObjToStr(GetText.Tag);
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

        public static Ellipse GetTransState(Grid Sender)
        {
            Grid LockerGrid = Sender;
            Ellipse TransState = (Ellipse)LockerGrid.Children[5];
            return TransState;
        }

        public static string ActiveType = "";
        public static string ActiveKey = "";
        public static TextBox ActiveTextBox = null;
        private static Grid LastSetGrid = null;
        //private static void MainGrid_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    if (sender is Grid)
        //    {
        //        Grid LockerGrid = (Grid)sender;
        //        if (LockerGrid.Children.Count >= 5)
        //        {
        //            UIHelper.SelectLine = LockerGrid;
        //            var MainGrid = LockerGrid;

        //            var GetTag = ConvertHelper.ObjToStr((MainGrid.Children[1] as Label).Content);

        //            var GetKey = GetMainGridKey(LockerGrid);
        //            var SourceText = GetSourceText(LockerGrid);
        //            var TransText = GetTransText(LockerGrid);

        //            //DeFine.WorkingWin.FromStr.Text = SourceText;
        //            //DeFine.WorkingWin.ToStr.Text = TransText;
        //            //DeFine.WorkingWin.ToStr.Tag = ConvertHelper.ObjToInt(MainGrid.Tag);
        //            //DeFine.WorkingWin.ReplaceKeyBox.Text = string.Empty;
        //            //DeFine.WorkingWin.ReplaceValueBox.Text = string.Empty;

        //            ActiveTextBox = (MainGrid.Children[4] as TextBox);
        //            ActiveKey = GetKey;
        //            ActiveType = GetTag;

        //            SetSelectLine(GetKey);

        //            if (DeFine.WorkingWin.CurrentTransType == 3)
        //            {
        //                if (DeFine.GlobalLocalSetting.ShowCode)
        //                {
        //                    try
        //                    {
        //                        SelectLineFromIDE(GetKey);
        //                    }
        //                    catch { }
        //                }
        //                if (DeFine.WorkingWin.GlobalPexReader != null)
        //                {
        //                    if (DeFine.WorkingWin.GlobalPexReader.HeuristicEngine != null)
        //                    {
        //                        try
        //                        {
        //                            string RichText = "";
        //                            string CreatRealKey = GetKey;
        //                            if (CreatRealKey.Contains(","))
        //                            {
        //                                CreatRealKey = CreatRealKey.Substring(0, CreatRealKey.LastIndexOf(","));
        //                            }
        //                            var FindFrist = DeFine.WorkingWin.GlobalPexReader.HeuristicEngine.DStringItems.FirstOrDefault(X => X.Key == CreatRealKey);
        //                            if (FindFrist != null)
        //                            {
        //                                RichText += FindFrist.ParentFunctionName + "->" + "\r\n";
        //                                RichText += FindFrist.SourceLine + "\r\n";
        //                                RichText += "\r\n";
        //                                foreach (var Get in FindFrist.TranslationScoreDetails)
        //                                {
        //                                    RichText += Get.Reason + "(" + Get.Value + ")" + "\r\n";
        //                                }
        //                                RichText += "\r\n";
        //                                RichText += FindFrist.Feature;
        //                                DeFine.CurrentDashBoardView.SetLogB(RichText);
        //                            }
        //                        }
        //                        catch { }
        //                    }
        //                }

        //            }
        //        }
        //    }

        //}

        private static void TransTextBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var GetTextBox = (sender as TextBox);
            if (GetTextBox.Tag is Grid)
            {
                Grid MainGrid = (Grid)GetTextBox.Tag;
                TextBox GetTransTextBox = (TextBox)MainGrid.Children[4];
                string GetKey = GetMainGridKey(MainGrid);
                string SourceText = GetSourceText(MainGrid);

                var GetResult = SetTransData(GetKey, SourceText, GetTransTextBox.Text);
                GetTransTextBox.BorderBrush = new SolidColorBrush(GetResult.Color);

                Ellipse GetState = GetTransState(MainGrid);
                if (GetOriginalText(MainGrid).Equals(GetTransText(MainGrid)))
                {
                    GetState.Fill = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    GetState.Fill = new SolidColorBrush(Colors.BlanchedAlmond);
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
                    string[] Params = GetKey.Split(',');
                    if (Params.Length > 1)
                    {
                        Task.Delay(200, Token).Wait(Token);

                        Token.ThrowIfCancellationRequested();

                        foreach (var Item in DeFine.WorkingWin.GlobalPexReader.HeuristicEngine.DStringItems)
                        {
                            if (Item.Key.Contains(","))
                            {
                                if (Item.Key.Equals(Params[0] + "," + Params[1]))
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
