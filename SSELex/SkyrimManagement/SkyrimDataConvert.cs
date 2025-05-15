using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSELex.ConvertManager;
using System.Windows.Controls;
using SSELex.SkyrimManage;
using SSELex.TranslateManage;
using DynamicData;

namespace SSELex.SkyrimManagement
{
    public class SkyrimDataConvert
    {
        public static YDDictionaryFile GenDictionary(string ModName)
        {
            YDDictionaryFile NYDDictionaryFile = new YDDictionaryFile();
            NYDDictionaryFile.ModName = ModName;
           
            for (int i = 0; i < DeFine.WorkingWin.TransViewList.Rows; i++)
            {
                Grid MainGrid = DeFine.WorkingWin.TransViewList.RealLines[i];

                int HashID = 0;
                string Key = "";
                string SourceText = "";
                string TransText = "";
                string Type = "";
                string ItemType = "";

                DeFine.WorkingWin.TransViewList.GetMainCanvas().Dispatcher.Invoke(new Action(() =>
                {
                    ItemType = ConvertHelper.ObjToStr((MainGrid.Children[1] as Label).Content);
                    Type = ConvertHelper.ObjToStr(MainGrid.ToolTip);
                    HashID = ConvertHelper.ObjToInt(MainGrid.Tag);
                    SourceText = (MainGrid.Children[3] as TextBox).Text.Trim();
                    TransText = ConvertHelper.ObjToStr((MainGrid.Children[4] as TextBox).Text);
                    Key = ConvertHelper.ObjToStr((MainGrid.Children[2] as TextBox).Text);
                }));

                YDDictionary NYDDictionary = new YDDictionary();
                NYDDictionary.Key = Key;
                NYDDictionary.OriginalText = SourceText;
                NYDDictionary.TransText = TransText;

                NYDDictionaryFile.Dictionarys.Add(NYDDictionary);
            }

            return NYDDictionaryFile;
        }
    }
}
