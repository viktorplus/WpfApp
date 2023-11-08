using System.Windows.Media;

namespace WpfApp
{
    public class SolidColorBrushPair
    {
        public SolidColorBrush Background { get; set; }
        public SolidColorBrush Foreground { get; set; }

        public SolidColorBrushPair(SolidColorBrush background, SolidColorBrush foreground)
        {
            Background = background;
            Foreground = foreground;
        }
    }
}
