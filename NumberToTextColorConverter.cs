using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfApp
{
    public class NumberToTextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int number = (int)value;

            if (number == 0)
            {
                return Brushes.White; // Белый цвет текста для плиток с нулевым значением
            }
            else
            {
                return Brushes.Black; // Черный цвет текста для непустых плиток
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
