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

        private void SubjectList_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Content = new SubjectList();
        }

        private void Building_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Content = new BuildingList();
        }

        private void Rooms_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Content = new ClassroomList();
        }

        private void Groups_Click(object sender, RoutedEventArgs e)
        {
            AdminFrame.Content = new GroupList();
        }
    }
}
