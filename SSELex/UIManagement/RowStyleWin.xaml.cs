using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using PhoenixEngine.TranslateManage;
using System.Windows.Markup;
using Mutagen.Bethesda.Skyrim;
using SSELex.SkyrimManage;
using System.Windows.Input;
using System.Windows.Media;
using PhoenixEngine.TranslateManagement;
using PhoenixEngine.ConvertManager;
using PhoenixEngine.EngineManagement;
using PhoenixEngine.SSEATComBridge;
using static PhoenixEngine.SSELexiconBridge.NativeBridge;
using System.Windows.Shapes;
using SSELex.TranslateManage;

namespace SSELex.UIManagement
{
    /// <summary>
    /// Interaction logic for RowStyleWin.xaml
    /// </summary>
    public partial class RowStyleWin : Window
    {
        public RowStyleWin()
        {
            InitializeComponent();
            this.Hide();
        }

        public static T CloneElement<T>(T source) where T : UIElement
        {
            if (source == null) return null;

            string xaml = XamlWriter.Save(source);
            StringReader stringReader = new StringReader(xaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);

            return (T)XamlReader.Load(xmlReader);
        }

        public static void SetColor(Grid Grid,int R,int G,int B)
        {
            Color FontColor = Color.FromRgb((byte)R, (byte)G, (byte)B);

            Grid MainGrid = Grid;

            Border MainBorder = (Border)MainGrid.Children[0];

            Grid GetChildGrid = (Grid)MainBorder.Child;

            StackPanel GetStackPanel = (StackPanel)((Grid)GetChildGrid.Children[0]).Children[0];

            Label GetType = (Label)GetStackPanel.Children[1];
            GetType.Foreground = new SolidColorBrush(FontColor);

            Grid GetKeyGrid = (Grid)GetChildGrid.Children[1];
            TextBox GetKey = (TextBox)GetKeyGrid.Children[0];
            GetKey.Foreground = new SolidColorBrush(FontColor);

            Grid GetOriginalGrid = (Grid)GetChildGrid.Children[2];
            TextBox GetOriginal = (TextBox)GetOriginalGrid.Children[0];
            GetOriginal.Foreground = new SolidColorBrush(FontColor);

            Grid GetTranslatedGrid = (Grid)GetChildGrid.Children[3];
            Border GetTranslatedBorder = (Border)GetTranslatedGrid.Children[0];

            TextBox GetTranslated = (TextBox)(GetTranslatedBorder.Child);
            GetTranslated.Foreground = new SolidColorBrush(FontColor);
        }

        public static string GetType(Grid Grid)
        {
            Grid GetDataGrid = ((Grid)((Border)Grid.Children[0]).Child);
            StackPanel GetStackPanel = (StackPanel)(((Grid)GetDataGrid.Children[0]).Children[0]);
            Label GetType = (Label)GetStackPanel.Children[1];

            return ConvertHelper.ObjToStr(GetType.Content);
        }

        public static string GetKey(Grid Grid)
        {
            string GetKey = "";

            Grid.Dispatcher.Invoke(new Action(() => {
                GetKey = ConvertHelper.ObjToStr(((Border)Grid.Children[0]).Tag);
            }));

            return GetKey;
        }

        public static void SetOriginal(Grid Grid,string Text)
        {
            Grid GetDataGrid = ((Grid)((Border)Grid.Children[0]).Child);

            Grid GetOriginalGrid = (Grid)GetDataGrid.Children[2];

            TextBox GetOriginal = (TextBox)GetOriginalGrid.Children[0];

            GetOriginal.Text = Text;
        }

        public static string GetOriginal(Grid Grid)
        {
            Grid GetDataGrid = ((Grid)((Border)Grid.Children[0]).Child);

            Grid GetOriginalGrid = (Grid)GetDataGrid.Children[2];

            TextBox GetOriginal = (TextBox)GetOriginalGrid.Children[0];

            return GetOriginal.Text;
        }

        public static string GetTranslated(Grid Grid)
        {
            Grid GetDataGrid = ((Grid)((Border)Grid.Children[0]).Child);

            Grid GetTranslatedGrid = (Grid)GetDataGrid.Children[3];

            Border GetTranslatedBorder = (Border)GetTranslatedGrid.Children[0];

            TextBox GetTranslated = (TextBox)(GetTranslatedBorder.Child);

            return GetTranslated.Text;
        }

        public static void SetTranslated(Grid Grid,string Translated)
        {
            Grid SetDataGrid = ((Grid)((Border)Grid.Children[0]).Child);

            Grid SetTranslatedGrid = (Grid)SetDataGrid.Children[3];

            Border SetTranslatedBorder = (Border)SetTranslatedGrid.Children[0];

            TextBox SetTranslated = (TextBox)(SetTranslatedBorder.Child);

            SetTranslated.Text = Translated;
        }

        public static TextBox GetTranslatedTextBoxHandle(Grid Grid)
        {
            Grid SetDataGrid = ((Grid)((Border)Grid.Children[0]).Child);

            Grid SetTranslatedGrid = (Grid)SetDataGrid.Children[3];

            Border SetTranslatedBorder = (Border)SetTranslatedGrid.Children[0];

            TextBox SetTranslated = (TextBox)(SetTranslatedBorder.Child);

            return SetTranslated;
        }

        public static List<string> RecordModifyStates = new List<string>();

        public Grid CreatLine(double Height, TranslationUnit Item)
        {
            bool IsModify = false;

            var QueryTranslated = TranslatorBridge.QueryTransData(Item.Key, Item.SourceText);

            if (QueryTranslated != null)
            {
                Item.TransText = QueryTranslated.TransText;
            }

            var FindDictionary = YDDictionaryHelper.CheckDictionary(Item.Key);

            if (FindDictionary != null)
            {
                if (FindDictionary.OriginalText.Trim().Length > 0)
                {
                    if (Item.SourceText != FindDictionary.OriginalText)
                    {
                        Item.SourceText = FindDictionary.OriginalText;

                        //11 116 209
                        IsModify = true;

                        if (!RecordModifyStates.Contains(Item.Key))
                        {
                            RecordModifyStates.Add(Item.Key);
                        }
                    }
                }
            }

            Color FontColor = Colors.White;

            var QueryColor = FontColorFinder.FindColor(Engine.GetModName(), Item.Key);

            if (QueryColor != null)
            {
                FontColor = Color.FromRgb((byte)QueryColor.R, (byte)QueryColor.G, (byte)QueryColor.B);
            }

            Grid MainGrid = CloneElement(LineGrid);
            MainGrid.Height = Height;

            Border MainBorder = (Border)MainGrid.Children[0];
            MainBorder.Tag = Item.Key;

            Grid GetChildGrid = (Grid)MainBorder.Child;

            StackPanel GetStackPanel = (StackPanel)((Grid)GetChildGrid.Children[0]).Children[0];

            Ellipse State = (Ellipse)GetStackPanel.Children[0];

            if (IsModify || RecordModifyStates.Contains(Item.Key))
            {
                State.Fill = new SolidColorBrush(Color.FromRgb(11, 116, 209));
            }

            Label GetType = (Label)GetStackPanel.Children[1];
            GetType.Content = Item.Type;
            GetType.Foreground = new SolidColorBrush(FontColor);

            Grid GetKeyGrid = (Grid)GetChildGrid.Children[1];
            TextBox GetKey = (TextBox)GetKeyGrid.Children[0];
            GetKey.Text = Item.Key;
            GetKey.Foreground = new SolidColorBrush(FontColor);
            GetKey.PreviewMouseWheel += OnePreviewMouseWheel;

            Grid GetOriginalGrid = (Grid)GetChildGrid.Children[2];
            TextBox GetOriginal = (TextBox)GetOriginalGrid.Children[0];
            GetOriginal.Text = Item.SourceText;
            GetOriginal.Foreground = new SolidColorBrush(FontColor);
            GetOriginal.PreviewMouseWheel += OnePreviewMouseWheel;

            Grid GetTranslatedGrid = (Grid)GetChildGrid.Children[3];
            Border GetTranslatedBorder = (Border)GetTranslatedGrid.Children[0];

            TextBox GetTranslated = (TextBox)(GetTranslatedBorder.Child);

            GetTranslated.Text = Item.TransText;
            GetTranslated.Foreground = new SolidColorBrush(FontColor);
            GetTranslated.PreviewMouseWheel += OnePreviewMouseWheel;

            GetTranslated.MouseLeave += GetTranslated_MouseLeave;
            GetTranslated.LostFocus += GetTranslated_LostFocus;

            GetTranslated.Tag = Item.Key;

            if (DeFine.GlobalLocalSetting.ViewMode == "Normal")
            {
                MainGrid.Cursor = Cursors.Hand;
                GetKey.Cursor = Cursors.Hand;
                GetOriginal.Cursor = Cursors.Hand;
                GetTranslated.Cursor = Cursors.Hand;
                GetTranslatedBorder.Background = null;
                GetTranslatedBorder.Cursor = null;

                GetTranslated.IsReadOnly = true;
                GetTranslated.HorizontalContentAlignment = HorizontalAlignment.Center;
                GetTranslated.VerticalContentAlignment = VerticalAlignment.Center;
            }
            else
            {
                MainGrid.Cursor = Cursors.Hand;
                GetKey.Cursor = Cursors.Hand;
                GetOriginal.Cursor = Cursors.Hand;
                GetTranslated.Cursor = Cursors.IBeam;
                GetTranslated.IsReadOnly = false;
                GetTranslated.HorizontalAlignment = HorizontalAlignment.Stretch;
                GetTranslated.VerticalContentAlignment = VerticalAlignment.Center;
            }

            if (Item.Score < 5)
            {
                GetKey.Foreground = new SolidColorBrush(Colors.Red);
                GetOriginal.Foreground = new SolidColorBrush(Colors.Red);
                GetTranslated.Foreground = new SolidColorBrush(Colors.Red);
            }
            if (Item.Score < 0)
            {
                GetKey.Foreground = new SolidColorBrush(Colors.Red);
                GetOriginal.Foreground = new SolidColorBrush(Colors.Red);
                GetTranslatedBorder.Visibility = Visibility.Collapsed;

                GetTranslated.IsReadOnly = true;
            }

            return MainGrid;
        }


        private void GetTranslated_LostFocus(object sender, RoutedEventArgs e)
        {
            SaveText((TextBox)sender);
        }

        private void GetTranslated_MouseLeave(object sender, MouseEventArgs e)
        {
            SaveText((TextBox)sender);
        }

        public void SaveText(TextBox Text)
        {
            if (DeFine.GlobalLocalSetting.ViewMode != "Normal")
            {
                if (DeFine.WorkingWin != null)
                {
                    if (DeFine.WorkingWin.TransViewList != null)
                    {
                        int GetSelectionStart = Text.SelectionStart;
                        int GetSelectionLength = Text.SelectionLength;

                        if (Text.Text.Length > 0)
                        {
                            Text.Text = Translator.FormatStr(Text.Text);
                        }

                        string GetKey = ConvertHelper.ObjToStr(Text.Tag);
                        var GetTarget = DeFine.WorkingWin.TransViewList.KeyToFakeGrid(GetKey);

                        if (GetTarget != null)
                        {
                            TranslatorBridge.SetTransData(GetKey, GetTarget.SourceText, Text.Text);
                            bool IsCloud = false;
                            GetTarget.SyncData(ref IsCloud);
                            TranslatorExtend.SetTranslatorHistoryCache(GetKey, Text.Text, IsCloud);
                        }

                        GetSelectionStart = Math.Min(GetSelectionStart, Text.Text.Length);
                        GetSelectionLength = Math.Min(GetSelectionLength, Text.Text.Length - GetSelectionStart);

                        Text.SelectionStart = GetSelectionStart;
                        Text.SelectionLength = GetSelectionLength;
                    }
                }
            }
        }

        public static Thread AutoSelectIDETrd = null;
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
                                        int LineOffset = DeFine.ActiveIDE.Document.Text.IndexOf(Item.SourceLine);
                                        if (LineOffset == -1) return;

                                        int RelativeOffset = Item.SourceLine.IndexOf("\"" + Item.Str + "\"");
                                        if (RelativeOffset == -1) return;

                                        int AbsoluteOffset = LineOffset + RelativeOffset;
                                        DeFine.ActiveIDE.ScrollToLine(DeFine.ActiveIDE.Document.GetLineByOffset(AbsoluteOffset).LineNumber);
                                        DeFine.ActiveIDE.Select(AbsoluteOffset, ("\"" + Item.Str + "\"").Length);
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

        public void OnePreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var TextBox = sender as TextBox;
            var Parent = VisualTreeHelper.GetParent(TextBox);

            while (Parent != null && !(Parent is ScrollViewer))
            {
                Parent = VisualTreeHelper.GetParent(Parent);
            }

            if (Parent is ScrollViewer ScrollViewer)
            {
                ScrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset - e.Delta);
                e.Handled = true; 
            }
        }
    }
}
