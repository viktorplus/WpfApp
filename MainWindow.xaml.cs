using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        List<char> chars = new() { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', '[', ']', };
        List<char> chars2 = new() { 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', ';', '\'' };
        List<char> chars3 = new() { 'z', 'x', 'c', 'v', 'b', 'n', 'm', ',', '.', '/' };
        List<char> chars4 = new() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-', '=' };

        List<Button> buttons = new ();
        string? enteredText;
        private int correctKeyPresses = 0;    // количество правильных нажатий
        private int incorrectKeyPresses = 0;  // количество неправильных нажатий


        public MainWindow()
        {
            InitializeComponent();
            CreateButtons(SPBase, chars);
            CreateButtons(SPBase2, chars2);
            CreateButtons(SPBase3, chars3);
            CreateButtons(SPBase4, chars4);

        }
        //добавление кнопок в каждую StackPanel отдельно со стилем
        private void CreateButtons(StackPanel stackPanel, List<char> charsList)
        {
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.HorizontalAlignment = HorizontalAlignment.Stretch;

            for (int i = 0; i < charsList.Count; i++)
            {
                var b = new Button
                {
                    Content = charsList[i].ToString(),
                    Width = 50,
                    Height = 50,
                    Style = (Style)FindResource("MaterialDesignPaperButton")
                };
                stackPanel.Children.Add(b);

                // Добавить кнопку в общий список всех кнопок buttons
                buttons.Add(b);
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            enteredText = tbText.Text.ToLower();
            tbText.Text = enteredText;
        }

        private void tbText_KeyDown(object sender, KeyEventArgs e)
        {
            char sign;
            string key = e.Key.ToString();
            if (key.Length == 2 && key[0] == 'D')
            {
                sign = key[1]; // Удалить первый символ для цифр
            }
            else if (key == "Space")
            {
                sign = ' ';
            }

            else
            {
                sign = char.ToLower(key[0]);
            }
            // Вывод информации о значениях переменных в окно вывода
            //Debug.WriteLine("Key Pressed: " + key);
            //Debug.WriteLine("First Character in TextBox: " + tbText.Text.First());

            if (sign == tbText.Text.First())
            {
                HighlightButtonOk(sign.ToString());
                StringBuilder stringBuilder = new (tbText.Text);
                stringBuilder.Remove(0, 1);
                tbText.Text = stringBuilder.ToString();
                tbText.UpdateLayout();
                correctKeyPresses++; // увеличиваем счетчик правильных нажатий
            }
            else
            {
                HighlightButtonFalse(sign.ToString());
                incorrectKeyPresses++; // увеличиваем счетчик неправильных нажатий
            }
            UpdateKeyPressStatistics();

        }
        private void UpdateKeyPressStatistics()
        {
            int totalKeyPresses = correctKeyPresses + incorrectKeyPresses;
            double percentageCorrect = totalKeyPresses == 0 ? 100.0 : (correctKeyPresses * 100.0) / totalKeyPresses;

            // Обновляем TextBlock для отображения статистики
            // Создайте TextBlock с именем, например, statsTextBlock в вашем XAML и свяжите его с этим кодом.
            statsTextBlock.Text = $"Правильно: {correctKeyPresses}, Неправильно: {incorrectKeyPresses}, " +
                                 $"Процент правильных: {percentageCorrect:F2}%";
        }
        private void HighlightButtonOk(string sign)
        {
            foreach (var button in buttons)
            {
                if (button.Content.ToString() == sign)
                {
                    button.Background = new SolidColorBrush(Color.FromRgb(0x76, 0xff, 0x03));
                }
            }
        }
        private void HighlightButtonFalse(string sign)
        {
            foreach (var button in buttons)
            {
                if (button.Content.ToString() == sign)
                {
                    button.Background = new SolidColorBrush(Colors.IndianRed);
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
            string key;
            string sign = e.Key.ToString();
            if (sign.Length == 2 && sign[0] == 'D')
            {
                key = sign[1].ToString(); // Удалить первый символ для цифр
            }
            else key = sign[0].ToString();

            UnhighlightButtons(key);
        }

        private void UnhighlightButtons(string key)
        {
            foreach (var button in buttons)
            {
                if (button.Content.ToString() == char.ToLower(key[0]).ToString())
                {
                    button.Background = new SolidColorBrush(Color.FromRgb(0x30, 0x30, 0x30));
                    button.UpdateLayout();
                }
            }
        }

    }
}
