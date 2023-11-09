using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;


namespace WpfApp
{
    public class NumberToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int number = (int)value;

            if (number == 0)
            {
                return Brushes.Transparent; // Плитка с нулевым значением
            }
            else
            {
                var background = Brushes.LightGray;

                if (number == 2)
                {
                    background = Brushes.LightGray;
                }
                else if (number == 4)
                {
                    background = Brushes.LightBlue;
                }
                else if (number == 8)
                {
                    background = Brushes.LightGreen;
                }
                else if (number == 16)
                {
                    background = Brushes.Orange;
                }
                else if (number == 32)
                {
                    background = Brushes.Yellow;
                }
                else if (number == 64)
                {
                    background = Brushes.Red;
                }
                else if (number == 128)
                {
                    background = Brushes.Purple;
                }
                else if (number == 256)
                {
                    background = Brushes.Pink;
                }
                else if (number == 512)
                {
                    background = Brushes.Brown;
                }
                else if (number == 1024)
                {
                    background = Brushes.Magenta;
                }
                else if (number == 2048)
                {
                    background = Brushes.Cyan;
                }

                return background;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}