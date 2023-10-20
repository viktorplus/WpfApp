using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        List<char> chars = new List<char>() { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', '[', ']', };
        List<char> chars2 = new List<char>() { 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', ';', '\'' };
        List<char> chars3 = new List<char>() { 'z', 'x', 'c', 'v', 'b', 'n', 'm', ',', '.', '/' };
        List<char> chars4 = new List<char>() { '`', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-', '=' };

        List<Button> buttons = new List<Button>();

        public MainWindow()
        {
            InitializeComponent();
            CreateButtons(SPBase, chars);
            CreateButtons(SPBase2, chars2);
            CreateButtons(SPBase3, chars3);
            CreateButtons(SPBase4, chars4);

            foreach (var item in SPBase.Children)
            {
                if (item is Button button)
                {
                    buttons.Add(button);
                }
            }
            foreach (var item in SPBase2.Children)
            {
                if (item is Button button)
                {
                    buttons.Add(button);
                }
            }
            foreach (var item in SPBase3.Children)
            {
                if (item is Button button)
                {
                    buttons.Add(button);
                }
            }
            foreach (var item in SPBase4.Children)
            {
                if (item is Button button)
                {
                    buttons.Add(button);
                }
            }
        }

        private void CreateButtons(StackPanel stackPanel, List<char> charsList)
        {
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
            for (int i = 0; i < charsList.Count; i++)
            {
                var b = new Button() { Content = charsList[i].ToString(), Width = 50, Height = 50 };
                b.Style = (Style)FindResource("MaterialDesignPaperButton");
                stackPanel.Children.Add(b);
            }
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
                HighlightButton(sign.ToString());
                StringBuilder stringBuilder = new StringBuilder(tbText.Text);
                stringBuilder.Remove(0, 1);
                tbText.Text = stringBuilder.ToString();
                tbText.UpdateLayout();
            }
        }

        private void HighlightButton(string key)
        {
            foreach (var button in buttons)
            {
                if (button.Content.ToString() == key)
                {
                    button.Background = new SolidColorBrush(Color.FromRgb(0x76, 0xff, 0x03));
                }
            }
        }

        private void UnhighlightButtons()
        {
            foreach (var button in buttons)
            {
                button.Background = new SolidColorBrush(Color.FromRgb(0x30, 0x30, 0x30));
            }
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            UnhighlightButton(SPBase, e.Key.ToString());
            UnhighlightButton(SPBase2, e.Key.ToString());
            UnhighlightButton(SPBase3, e.Key.ToString());
            UnhighlightButton(SPBase4, e.Key.ToString());
        }

        private void UnhighlightButton(StackPanel stackPanel, string key)
        {
            foreach (var item in stackPanel.Children)
            {
                if (item is Button)
                {
                    if ((item as Button).Content.ToString() == char.ToLower(key[0]).ToString())
                    {
                        (item as Button).Background = new SolidColorBrush(Color.FromRgb(0x30, 0x30, 0xff));
                        (item as Button).UpdateLayout();
                    }
                }
            }
        }
    }
}
