using System.Windows;
using System.Windows.Controls;
using WpfApp.Navigator;

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
            //AdminFrame.Content = new AdminUserList(AdminFrame);
        }

        private void AdminUserList_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AdminInfo_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
