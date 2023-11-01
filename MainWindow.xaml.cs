using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private Calculator calculator;

        public MainWindow()
        {
            InitializeComponent();
            calculator = new Calculator();
        }

        private void Digit_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string digit = button.Content.ToString();
            calculator.AppendDigit(digit);
            UpdateDisplay();
        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string operation = button.Content.ToString();
            calculator.SetOperation(operation);
            UpdateDisplay();
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (calculator.HasPendingOperation())
            {
                calculator.Calculate();
                UpdateDisplay();
            }
        }

        private void DecimalPoint_Click(object sender, RoutedEventArgs e)
        {
            calculator.AppendDecimalPoint();
            UpdateDisplay();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            calculator.Clear();
            UpdateDisplay();
        }

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            calculator.ClearAll();
            UpdateDisplay();
        }

        private void RemoveLastDigit_Click(object sender, RoutedEventArgs e)
        {
            calculator.RemoveLastDigit();
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            historyTextBox.Text = calculator.GetHistory();
            currentNumberTextBox.Text = calculator.GetCurrentNumber();
        }
    }
}
