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
                FakeGrid MainGrid = DeFine.WorkingWin.TransViewList.RealLines[i];

                string Key = MainGrid.Key;
                string SourceText = MainGrid.SourceText;
                string TransText = MainGrid.TransText;

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
