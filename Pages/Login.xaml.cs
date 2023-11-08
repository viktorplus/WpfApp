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
using WpfApp;
using WpfApp.Domain;

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
            MainFrame = mainFrame;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            //string login = LoginTextBox.Text;
            //string password = PasswordBox.Password;

            //if (MainWindow.userList.ValidateUser(login, password))
            //{
            //    MainWindow.CurrentUser = MainWindow.userList.GetUserByLogin(login);

            //    MainFrame.Content = new MainGoodsList(MainFrame);
            //}
            //else
            //{
            //    MessageBox.Show("Неправильный логин или пароль. Попробуйте еще раз.");
            //}
        }
    }
}