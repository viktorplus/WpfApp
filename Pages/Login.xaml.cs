using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;
using WpfApp.Navigator;

namespace WpfApp.Pages
{
    public partial class Login : UserControl
    {
        private Frame mainFrame;


        public Login(Frame mainFrame)
        {
            InitializeComponent();
            this.mainFrame = mainFrame;

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            if (MainWindow.UserList.ValidateUser(login, password))
            {

                MainWindow.CurrentUser = MainWindow.UserList.GetUserByLogin(login);

                if (MainWindow.CurrentUser.Roles.Contains(User.UserRole.Admin))
                {
                    NavigatorObject.Switch(new MainAdmin());
                }
                //else if (currentUser.Roles.Contains(User.UserRole.Student))
                //{
                //    NavigatorObject.Switch(new MainStudent(currentUser));
                //}
                //else if (currentUser.Roles.Contains(User.UserRole.Lecturer))
                //{
                //    NavigatorObject.Switch(new MainLecture(currentUser));
                //}
                // Другие варианты для других ролей
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль. Попробуйте еще раз.");
            }
        }
    }
}