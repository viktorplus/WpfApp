using System.Windows;
using System.Windows.Controls;

namespace WpfApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        Frame MainFrame;

        public Login(Frame mainFrame)
        {
            InitializeComponent();

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            if (MainPage.userList.ValidateUser(login, password))
            {
                MainPage.CurrentUser = MainPage.userList.GetUserByLogin(login);

                MainFrame.Content = new MainAdmin(MainFrame);
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль. Попробуйте еще раз.");
            }
        }
    }
}