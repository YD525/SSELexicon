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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts.Wpf;
using LiveCharts;
using System.ComponentModel;

namespace SSELex
{
    /// <summary>
    /// Interaction logic for OnlyValueTooltip.xaml
    /// </summary>
    public partial class OnlyValueTooltip : UserControl, IChartTooltip, INotifyPropertyChanged
    {
        public OnlyValueTooltip()
        {
            InitializeComponent();
            DataContext = this;
        }

        private TooltipData DataBackingField;
        public TooltipData Data
        {
            get => DataBackingField;
            set
            {
                DataBackingField = value;
                OnPropertyChanged(nameof(Data));

                if (DataBackingField == null || DataBackingField.Points.Count == 0)
                    return;

                var Point = DataBackingField.Points[0];

                int Index = (int)Point.ChartPoint.Key;

                string[] PlatformNames = new string[] { "ChatGPT", "Gemini", "Cohere","DeepSeek", "DeepL","Baichuan", "LocalAI", "Google"};

                string PlatformName = (Index >= 0 && Index < PlatformNames.Length)
                    ? PlatformNames[Index]
                    : "Unknown";

                ValueText.Text = $"{PlatformName}: {Point.ChartPoint.Y} words";
            }
        }

        public TooltipSelectionMode? SelectionMode { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
