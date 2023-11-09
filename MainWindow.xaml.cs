using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Navigator;
using WpfApp.Pages;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        //public static UserList userList;
        //public static User CurrentUser { get; set; }

        public MainWindow()
        {

            //userList = new UserList();
      

            InitializeComponent();

            NavigatorObject.pageSwitcher = this;
            NavigatorObject.Switch(new MainPage());
        }

        //public Action? CloseAction { get; set; }

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
    }
}