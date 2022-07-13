using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SIDISTraceChecker.Converters
{
    internal class BooleanToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Brushes.Red : Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
