using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Navigator;

namespace WpfApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        Frame MainFrame;
        public Login(Frame MainFrame)
        {
            InitializeComponent();
            this.MainFrame = MainFrame;

        }
        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        public void Navigate(UserControl nextPage, object state)
        {
            this.Content = nextPage;
            INavigator? s = nextPage as INavigator;

            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("NextPage is not INavigator! " + nextPage.Name.ToString());
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            if (MainPage.userList.ValidateUser(login, password))
            {
                MainPage.CurrentUser = MainPage.userList.GetUserByLogin(login);

                //MainFrame.Content = new MainAdmin(MainFrame);
                NavigatorObject.Switch(new MainAdmin());


            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль. Попробуйте еще раз.");
            }
        }


    }
}