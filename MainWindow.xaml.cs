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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int pos = 0;
        List<char> chars = new List<char>() { 'w', 'l', 'o', 'h', 'd', 'r' };
        public MainWindow()
        {
            InitializeComponent();
            CreateButtons();


        }
        private void CreateButtons()
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
            for (int i = 0; i < chars.Count; i++)
            {
                var b = new Button() { Content = chars[i], Width = 50, Height = 50 };
                b.Style = (Style)FindResource("MaterialDesignPaperButton");
                stackPanel.Children.Add(b);
            }
            SPBase.Children.Add(stackPanel);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbText_KeyDown(object sender, KeyEventArgs e)
        {

            char sign;
            string key = e.Key.ToString();
            if (key == "Space")
            {
                sign = ' ';
            }
            else
            {
                sign = char.ToLower(key[0]);
            }

            if (sign == tbText.Text.First())
            {
                foreach (var item in SPBase.Children)
                {
                    if (item is StackPanel)
                    {
                        foreach (var item2 in (item as StackPanel).Children)
                        {
                            if (item2 is Button)
                            {
                                if ((item2 as Button).Content.ToString() == char.ToLower(key[0]).ToString())
                                {
                                    (item2 as Button).Background = new SolidColorBrush(Color.FromRgb(0x76, 0xff, 0x03));  //                                    (item2 as Button).Background = new SolidColorBrush(Colors.Red);
                                    break;
                                }
                            }
                        }
                    }
                }

                StringBuilder stringBuilder = new StringBuilder(tbText.Text);
                stringBuilder.Remove(0, 1);
                tbText.Text = stringBuilder.ToString();
                tbText.UpdateLayout();
            }

        }




        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            foreach (var item in SPBase.Children)
            {
                if (item is StackPanel)
                {
                    foreach (var item2 in (item as StackPanel).Children)
                    {
                        if (item2 is Button)
                        {
                            if ((item2 as Button).Content.ToString() == char.ToLower(e.Key.ToString()[0]).ToString())
                            {
                                (item2 as Button).Background = new SolidColorBrush(Color.FromRgb(0x30, 0x30, 0x30));
                                (item2 as Button).UpdateLayout();
                            }
                        }
                    }
                }
            }

        }
    }
}