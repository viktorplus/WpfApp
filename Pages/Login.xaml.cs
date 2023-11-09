using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;
using WpfApp.Navigator;

namespace WpfApp.Pages
{
    public partial class Login : UserControl
    {
        public static UserList userList;
        public static User CurrentUser { get; set; }
        Frame MainFrame;
        public Login(Frame MainFrame)
        {
            InitializeComponent();

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            //string login = LoginTextBox.Text;
            //string password = PasswordBox.Password;

            //if (userList.ValidateUser(login, password))
            //{
            //    CurrentUser = userList.GetUserByLogin(login);

            NavigatorObject.Switch(new MainAdmin());
            //}
            //else
            //{
            //    MessageBox.Show("Неправильный логин или пароль. Попробуйте еще раз.");
            //}
        }


    }
}