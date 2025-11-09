using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Highlighting;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit;

namespace SSELex
{
    /// <summary>
    /// Interaction logic for CodeView.xaml
    /// </summary>
    public partial class CodeView : Window
    {
        public CodeView()
        {
            InitializeComponent();
        }

        public FoldingManager FoldingManager = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string GetName = "SSELex" + ".IDERule.Lua.xshd";

            System.Reflection.Assembly Assembly = System.Reflection.Assembly.GetExecutingAssembly();

            using (System.IO.Stream Resource = Assembly.GetManifestResourceStream(GetName))
            {
                using (System.Xml.XmlTextReader Reader = new System.Xml.XmlTextReader(Resource))
                {
                    var Xshd = HighlightingLoader.LoadXshd(Reader);

                    TextEditor.SyntaxHighlighting = HighlightingLoader.Load(Xshd, HighlightingManager.Instance);
                }
            }

            DeFine.ActiveIDE = TextEditor;

            FoldingManager = FoldingManager.Install(TextEditor.TextArea); // Install the folder
        }

        public void ReSetFolding()
        {
            FunctionFoldingStrategy NBraceFoldingStrategy = new FunctionFoldingStrategy();
            NBraceFoldingStrategy.UpdateFoldings(FoldingManager, TextEditor.Document);
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public bool IsLeftMouseDown = false;

        private void WinHead_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                IsLeftMouseDown = true;
            }

            if (IsLeftMouseDown)
            {
                try
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        this.DragMove();
                    }));

                    IsLeftMouseDown = false;
                }
                catch { }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.F)
            {
                SearchText NSearchText = new SearchText();
                NSearchText.Owner = this;
                NSearchText.Show();
            }
        }

        private void Close_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
        }
    }

    public class FunctionFoldingStrategy
    {
        public void UpdateFoldings(FoldingManager foldingManager, TextDocument Document)
        {
            var newFoldings = CreateNewFoldings(Document);
            foldingManager.UpdateFoldings(newFoldings, 0);
        }

        private List<NewFolding> CreateNewFoldings(ITextSource Document)
        {
            List<NewFolding> NewFoldings = new List<NewFolding>();
            Stack<int> StartOffsets = new Stack<int>();
            string CurrentLine = "";

            for (int I = 0; I < Document.TextLength; I++)
            {
                char Line = Document.GetCharAt(I);
                CurrentLine += Line.ToString();

                if (Line == '\n' || Line == '\r')
                {
                    if (CurrentLine.Contains("Function", StringComparison.OrdinalIgnoreCase))
                    {
                        int GetFunctionOffset = CurrentLine.IndexOf("Function");
                        if (GetFunctionOffset > 0)
                        {
                            if (CurrentLine.Substring(GetFunctionOffset - 1, 1).Trim().Length > 0 == false)
                            {
                                int GetParamOffset = CurrentLine.IndexOf(")");

                                StartOffsets.Push(I - GetParamOffset + GetParamOffset);
                            }
                        }
                        else
                        {
                            int GetParamOffset = CurrentLine.IndexOf(")");
                            if (GetParamOffset >= 0)
                            {
                                StartOffsets.Push((I - GetParamOffset + GetParamOffset));
                            }
                        }
                    }

                    if (CurrentLine.Contains("EndFunction", StringComparison.OrdinalIgnoreCase) && StartOffsets.Count > 0)
                    {
                        if (CurrentLine.Replace("\r", "").Replace("\n", "").Length > 0)
                        {
                            int FunctionStart = StartOffsets.Pop();

                            int FunctionEnd = I; //The ending offset should be the end position of the line

                            var Folding = new NewFolding(FunctionStart, FunctionEnd);
                            NewFoldings.Add(Folding);
                        }
                    }

                    CurrentLine = "";
                }
            }

            NewFoldings.Sort((a, b) => a.StartOffset.CompareTo(b.StartOffset));
            return NewFoldings;
        }
    }
}
