using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {

            if (UsernameTextBox.Text == "user" && PasswordBox.Password == "password")
            {
                ErrorMessageTextBlock.Text = "Вход успешен";
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                ErrorMessageTextBlock.Text = "Ошибка входа. Пожалуйста, проверьте имя пользователя и пароль.";
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            UsernameTextBox.Text = string.Empty;
            PasswordBox.Password = string.Empty;
            ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
        }
    }
}
