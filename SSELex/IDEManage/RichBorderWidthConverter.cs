using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SSELex
{
    public class RichBorderWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double rich_h = (double)values[0];
            double border_h = (double)values[1];
            double border_w = (double)values[2];
            if (rich_h + 2 > border_h)
            {
                return border_w - 20;
            }
            else
            {
                return border_w;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
