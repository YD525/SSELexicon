using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using ValveKeyValue;
using NexusMods.Paths.Trees.Traits;

namespace YDSkyrimToolR
{
    /*
* @Author: YD525
* @GitHub: https://github.com/YD525/YDSkyrimToolR
* @Date: 2025-02-06
*/
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
            string GetName = "YDSkyrimToolR" + ".IDERule.Lua.xshd";

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

            using (System.IO.Stream s = assembly.GetManifestResourceStream(GetName))
            {
                using (System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(s))
                {
                    var xshd = HighlightingLoader.LoadXshd(reader);

                    TextEditor.SyntaxHighlighting = HighlightingLoader.Load(xshd, HighlightingManager.Instance);
                }
            }

            DeFine.ActiveIDE = TextEditor;
            DeFine.ActiveIDE.TextChanged += ActiveIDE_TextChanged;

            FoldingManager = FoldingManager.Install(TextEditor.TextArea); // 安装折叠管理器

            ReSetFolding();
        }

        private void ActiveIDE_TextChanged(object? sender, EventArgs e)
        {
            ReSetFolding();
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

        private void Close(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
        }

        private void RMouserEffectByEnter(object sender, MouseEventArgs e)
        {
            if (sender is Border)
            {
                Border LockerGrid = sender as Border;
                if (LockerGrid.Child != null)
                {
                    if (LockerGrid.Child is Image)
                    {
                        Image LockerImg = LockerGrid.Child as Image;
                        LockerImg.Opacity = 0.9;
                    }
                    LockerGrid.Background = DeFine.SelectBackGround;
                }
            }
        }

        private void RMouserEffectByLeave(object sender, MouseEventArgs e)
        {
            if (sender is Border)
            {
                Border LockerGrid = sender as Border;
                if (LockerGrid.Child != null)
                {
                    if (LockerGrid.Child is Image)
                    {
                        Image LockerImg = LockerGrid.Child as Image;
                        LockerImg.Opacity = 0.6;
                    }
                    LockerGrid.Background = DeFine.DefBackGround;
                }
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
            List<NewFolding> newFoldings = new List<NewFolding>();
            Stack<int> startOffsets = new Stack<int>();

            int CurrentCharCount = 0;

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

                                startOffsets.Push(I - GetParamOffset + GetParamOffset);
                            }
                        }
                        else
                        {
                            int GetParamOffset = CurrentLine.IndexOf(")");
                            if (GetParamOffset >= 0)
                            {
                                startOffsets.Push((I - GetParamOffset + GetParamOffset));
                            }
                        }
                    }

                    if (CurrentLine.Contains("EndFunction", StringComparison.OrdinalIgnoreCase) && startOffsets.Count > 0)
                    {
                        if (CurrentLine.Replace("\r", "").Replace("\n", "").Length > 0)
                        {
                            int functionStart = startOffsets.Pop();

                            int functionEnd = I; // 结束偏移应为该行的结束位置

                            var folding = new NewFolding(functionStart, functionEnd);
                            newFoldings.Add(folding);
                        }
                    }

                    CurrentLine = "";
                }
            }

            newFoldings.Sort((a, b) => a.StartOffset.CompareTo(b.StartOffset));
            return newFoldings;
        }
    }
}
