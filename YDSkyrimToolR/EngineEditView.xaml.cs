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
using YDSkyrimToolR.TranslateCore;

namespace YDSkyrimToolR
{
    /*
* @Author: YD525
* @GitHub: https://github.com/YD525/YDSkyrimToolR
* @Date: 2025-02-06
*/
    /// <summary>
    /// Interaction logic for EngineEditView.xaml
    /// </summary>
    public partial class EngineEditView : Window
    {
        public EngineEditView()
        {
            InitializeComponent();
        }

        private void StartTestTrans(object sender, RoutedEventArgs e)
        {
            string GetSourceStr = Source.Text.Trim();
            new Thread(() => { 
                List<EngineProcessItem> EngineProcessItems = new List<EngineProcessItem>();
                var GetResult = new WordProcess().ProcessWords(ref EngineProcessItems,GetSourceStr, DeFine.SourceLanguage, DeFine.TargetLanguage);
                this.Dispatcher.Invoke(new Action(() => {
                    TargetStr.Text = GetResult;
                }));
            }).Start();
           
        }
    }
}
