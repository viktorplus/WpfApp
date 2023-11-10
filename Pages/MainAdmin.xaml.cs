using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;


namespace WpfApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainAdmin.xaml
    /// </summary>
    public partial class MainAdmin : UserControl
    {

        public MainAdmin()
        {
            InitializeComponent();
            AdminFrame.Content = new AdminUserList();
        }

        private void AdminUserList_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Content = new AdminUserList();
        }

        private void AdminInfo_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
