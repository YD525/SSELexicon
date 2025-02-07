using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Rendering;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace YDSkyrimToolR.IDEManager
{
    public class UserInputAction
    {
        public static string CurrentTable = "";
        public static string CurrentFunction = "";
        public static bool InputSucess = false;

        private static CompletionWindow _completionWindow;
        public static void TextAreaOnTextEntered(TextCompositionEventArgs e, TextArea TextArea)
        {
            //    string GetLine = DeFine.ActiveIDE.Document.GetText(DeFine.ActiveIDE.Document.Lines[TextArea.Caret.Position.Line - 1]);

            //    if (CurrentTable.Trim().Length > 0)
            //    {
            //        if (e.Text == ".")
            //        {
            //            //if (GetLine.Trim().Replace(" ","").StartsWith("this"))
            //            //{
            //            //    if (CurrentTable.Contains("."))
            //            //    {
            //            //        if (GetLine.Split('.').Length == 2)
            //            //        {
            //            //            List<string> FindKeys = new List<string>();

            //            //            var GetTable = FormHelper.SelectTable(CurrentTable.Split('.')[0], CurrentTable.Split('.')[1]);

            //            //            DeFine.LastTable = GetTable;

            //            //            if (GetTable.Rows.Count > 0)
            //            //            {
            //            //                foreach (var Get in CurrentFunction.Split(','))
            //            //                {
            //            //                    if (Get.Trim().Length > 0)
            //            //                    {

            //            //                        string ProcessKey = Get.Replace(" ", "").Replace("\r\n", "");

            //            //                        foreach (var GetCol in GetTable.Cols)
            //            //                        {
            //            //                            if (!FindKeys.Contains(GetCol.Key))
            //            //                                FindKeys.Add(GetCol.Key);
            //            //                        }

            //            //                    }
            //            //                }
            //            //            }

            //            //            if (FindKeys.Count > 0)
            //            //            {
            //            //                _completionWindow = new CompletionWindow(TextArea);

            //            //                var completionData = _completionWindow.CompletionList.CompletionData;

            //            //                _completionWindow.Width = 600;

            //            //                foreach (var GetKey in FindKeys)
            //            //                    completionData.Add(new CompletionData(GetKey + " "));

            //            //                _completionWindow.Background = new SolidColorBrush(Color.FromRgb(244, 154, 12));
            //            //                _completionWindow.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            //            //                _completionWindow.Show();

            //            //                _completionWindow.Closed += (o, args) => _completionWindow = null;
            //            //            }
            //            //        }
            //            //    }
            //            //}


            //        }
            //    }
            //    else
            //    {
            //        if (e.Text == " ")
            //        {
            //            if (!InputSucess)
            //            {
            //                //_completionWindow = new CompletionWindow(TextArea);

            //                //var completionData = _completionWindow.CompletionList.CompletionData;

            //                //_completionWindow.Width = 600;

            //                //if (!GetLine.Contains("Modify") && !GetLine.Contains("Insert") && !GetLine.Contains("Delete"))
            //                //{
            //                //    completionData.Add(new CompletionData("Modify"));
            //                //    completionData.Add(new CompletionData("Insert"));
            //                //    completionData.Add(new CompletionData("Delete"));
            //                //}

            //                //completionData.Add(new CompletionData(","));

            //                //if (!GetLine.Contains("<"))
            //                //{
            //                //    completionData.Add(new CompletionData("<" + ">"));
            //                //}

            //                //if (DeFine.CurrentTable.Trim().Length > 0&& DeFine.CurrentTable.Contains("."))
            //                //{
            //                //    var GetTable = FormHelper.SelectTable(DeFine.CurrentTable.Split('.')[0], DeFine.CurrentTable.Split('.')[1]);

            //                //    DeFine.LastTable = GetTable;

            //                //    if (!GetLine.Contains("<"))
            //                //    {
            //                //        completionData.Add(new CompletionData("<" + DeFine.CurrentTable + ">"));
            //                //    }

            //                //    foreach (var Get in GetTable.Rows)
            //                //    {
            //                //       completionData.Add(new CompletionData(Get.Key));
            //                //    }

            //                //}

            //                //_completionWindow.Background = new SolidColorBrush(Color.FromRgb(244, 154, 12));
            //                //_completionWindow.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            //                //_completionWindow.Show();

            //                //_completionWindow.Closed += (o, args) => _completionWindow = null;
            //            }

            //        }
            //        else
            //        if (e.Text == "<")
            //        {
            //            UserInputAction.InputSucess = true;
            //        }
            //    }

            //    if (e.Text == "=")
            //    {
            //        string GetKey = GetLine;

            //        if (GetKey.Contains("."))
            //        {
            //            //GetKey = GetKey.Split('.')[1].Trim();

            //            //_completionWindow = new CompletionWindow(TextArea);

            //            //var completionData = _completionWindow.CompletionList.CompletionData;

            //            //_completionWindow.Width = 600;

            //            //completionData.Add(new CompletionData(IDEHelper.GetKeyHelp(GetKey.Replace("=","").Trim())));

            //            //_completionWindow.Background = new SolidColorBrush(Color.FromRgb(244, 154, 12));
            //            //_completionWindow.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            //            //_completionWindow.Show();

            //            //_completionWindow.Closed += (o, args) => _completionWindow = null;

            //        }

            //    }



        }




    }

    public class OffsetColorizer : DocumentColorizingTransformer
    {
        public int StartOffset { get; set; }
        public int EndOffset { get; set; }

        protected override void ColorizeLine(DocumentLine line)
        {
            //if (DeFine.ActiveIDE == null == false)
            //{
            //    if (line.Length == 0)
            //        return;

            //    string CurrentLine = DeFine.ActiveIDE.Document.GetText(line);

            //    if (CurrentLine.Contains("<") && CurrentLine.Contains(">") && CurrentLine.Contains(" "))
            //    {
            //        //var GetOneColors = IDEHelper.CheckLineSign(CurrentLine);

            //        //if (GetOneColors == null == false)
            //        //{
            //        //    foreach (var GetOneColor in GetOneColors)
            //        //        ChangeLinePart(line.Offset + GetOneColor.StartOffset, line.Offset + GetOneColor.StartOffset + GetOneColor.Length, element => element.TextRunProperties.SetForegroundBrush(new SolidColorBrush(GetOneColor.FontColor)));
            //        //}
            //    }
            //    else
            //    {
            //        if (CurrentLine.Contains("{"))
            //        {
            //            //DocumentLine TempLine = line;

            //            //string CheckLine = DeFine.ActiveIDE.Document.GetText(TempLine);

            //            //while (!CheckLine.Contains("<") && !CheckLine.Contains(">"))
            //            //{
            //            //    TempLine = TempLine.PreviousLine;

            //            //    if (TempLine == null)
            //            //    {
            //            //        break;
            //            //    }

            //            //    CheckLine = DeFine.ActiveIDE.Document.GetText(TempLine);
            //            //}

            //            //if (CheckLine.Contains("<") && CheckLine.Contains(">"))
            //            //{
            //            //    UserInputAction.CurrentTable = ConvertHelper.StringDivision(CheckLine, "<", ">");
            //            //}

            //        }
            //        else
            //        if (CurrentLine.Contains("}"))
            //        {
            //            UserInputAction.CurrentTable = "";
            //            UserInputAction.InputSucess = false;
            //        }
            //        else
            //        {
            //            DocumentLine TempLine = line;

            //            string CheckLine = DeFine.ActiveIDE.Document.GetText(TempLine);


            //            while (!CheckLine.Contains("<") && !CheckLine.Contains(">"))
            //            {
            //                TempLine = TempLine.PreviousLine;

            //                if (TempLine == null)
            //                {
            //                    break;
            //                }

            //                CheckLine = DeFine.ActiveIDE.Document.GetText(TempLine);

            //                if (CheckLine.Contains("//"))
            //                {
            //                    CheckLine = CheckLine.Substring(0,CheckLine.IndexOf("//"));
            //                }

            //                if (CheckLine.Contains("}"))
            //                {
            //                    UserInputAction.CurrentTable = "";
            //                    UserInputAction.InputSucess = false;
            //                    break;
            //                }
            //            }

            //            if (CheckLine.Contains("<") && CheckLine.Contains(">"))
            //            {
            //                //UserInputAction.CurrentTable = ConvertHelper.StringDivision(CheckLine, "<", ">");
            //            }
            //        }
            //    }


            //}

        }
    }
}
