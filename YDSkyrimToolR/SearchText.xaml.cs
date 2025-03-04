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

namespace YDSkyrimToolR
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    /// <summary>
    /// Interaction logic for SearchText.xaml
    /// </summary>
    public partial class SearchText : Window
    {
        public SearchText()
        {
            InitializeComponent();
        }


        int Line = 0;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool IsFind = false;
            for (int i = Line; i < DeFine.ActiveIDE.LineCount; i++)
            {
                var GetLineStr = DeFine.ActiveIDE.Text.Substring(DeFine.ActiveIDE.Document.Lines[i].Offset,DeFine.ActiveIDE.Document.Lines[i].Length);
                if (GetLineStr.Contains(Search.Text))
                {
                    DeFine.ActiveIDE.ScrollToLine(i);
                    int GetOffset = GetLineStr.IndexOf(Search.Text);
                    DeFine.ActiveIDE.Select(DeFine.ActiveIDE.Document.Lines[i].Offset + GetOffset, Search.Text.Length);

                    Line = i + 1;
                    IsFind = true;
                    break;
                }
            }
            if (!IsFind)
            {
                Line = 0;
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Line = 0;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(null,null);
            }
        }
    }
}
