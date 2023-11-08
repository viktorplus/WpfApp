using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;


namespace WpfApp
{
    public class NumberToColorConverter : IMultiValueConverter
    {


        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Проверка наличия нужного числа значений и преобразование их
            if (values.Length >= 1 && values[0] is int)
            {
                int number = (int)values[0];

                if (number == 0)
                {
                    SolidColorBrush background = Brushes.Transparent;
                    SolidColorBrush foreground = Brushes.White; // Белый цвет текста для 0
                    return new SolidColorBrushPair(background, foreground);
                }
                else
                {
                    SolidColorBrush background = Brushes.LightGray; // Цвет фона для других значений
                    SolidColorBrush foreground = Brushes.Black; // Черный цвет текста для других значений
                    return new SolidColorBrushPair(background, foreground);
                }
            }
            // Если параметры не соответствуют ожиданиям, вы можете вернуть, например, значения по умолчанию или обработать ошибку.
            return new SolidColorBrushPair(Brushes.Transparent, Brushes.Black);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
