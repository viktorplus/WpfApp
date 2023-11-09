using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Navigator;

namespace WpfApp.Pages
{
    public partial class Login : UserControl
    {
        Frame MainFrame;
        public Login(Frame MainFrame)
        {
            InitializeComponent();

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            //if (MainPage.userList.ValidateUser(login, password))
            //{
                //MainPage.CurrentUser = MainPage.userList.GetUserByLogin(login);

                //MainFrame.Content = new MainAdmin(MainFrame);
                NavigatorObject.Switch(new MainAdmin());


            //}
            //else
            //{
            //    MessageBox.Show("Неправильный логин или пароль. Попробуйте еще раз.");
            //}
        }


    }
}