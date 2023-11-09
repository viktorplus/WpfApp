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
        }
           private void AdminInfo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AdminUserList_Click(object sender, RoutedEventArgs e)
        {
            if (AdminFrame.Content.GetType() != typeof(Info))
            {
                AdminFrame.Content = new Info(AdminFrame);
            }
        }
    }
}
