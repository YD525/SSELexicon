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

            string xaml = XamlWriter.Save(source); // 序列化成 XAML 字符串
            StringReader stringReader = new StringReader(xaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);

            return (T)XamlReader.Load(xmlReader);  // 反序列化成新对象
        }

        public Grid CreatLine(double Height, TranslationUnit Item)
        {
            Grid MainGrid = CloneElement(LineGrid);
            MainGrid.Height = Height;

            Border MainBorder = (Border)MainGrid.Children[0];
            MainBorder.Tag = Item.Key;

            Grid GetChildGrid = (Grid)MainBorder.Child;

            StackPanel GetStackPanel = (StackPanel)((Grid)GetChildGrid.Children[0]).Children[0];
            Label GetType = (Label)GetStackPanel.Children[1];
            GetType.Content = Item.Type;

            Grid GetKeyGrid = (Grid)GetChildGrid.Children[1];
            TextBox GetKey = (TextBox)GetKeyGrid.Children[0];
            GetKey.Text = Item.Key;

            Grid GetOriginalGrid = (Grid)GetChildGrid.Children[2];
            TextBox GetOriginal = (TextBox)GetOriginalGrid.Children[0];
            GetOriginal.Text = Item.SourceText;

            Grid GetTranslatedGrid = (Grid)GetChildGrid.Children[3];
            TextBox GetTranslated = (TextBox)(((Border)GetTranslatedGrid.Children[0]).Child);
            GetTranslated.Text = Item.TransText;

            return MainGrid;
        }

      
    }
}
