using System;
using System.Globalization;
using System.Windows.Data;

namespace SIDISTraceChecker.Converters
{
    internal class DiameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter.ToString() == "2nd" ? (double)value * 1.2 : (double)value * 1.4;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
