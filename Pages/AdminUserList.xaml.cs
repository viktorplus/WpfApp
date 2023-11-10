using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using WpfApp.Domain;
using static WpfApp.Domain.User;

namespace WpfApp.Pages
{
    public partial class AdminUserList : UserControl
    {
        private ObservableCollection<User> userList;
        private List<User> allUsers;
        private List<UserRole> allRoles;
        private int pageSize = 10;

        private UserRole selectedRole;
        public UserRole SelectedRole
        {
            get { return selectedRole; }
            set
            {
                selectedRole = value;
                LoadUsers(0);
            }
        }

        public AdminUserList()
        {
            InitializeComponent();
            userList = new ObservableCollection<User>();
            allUsers = new UserList().AllUsers;
            allRoles = new UserList().AllRoles;
            SelectedRole = UserRole.Student;
            UsersListView.ItemsSource = userList;

            // Используем ItemTemplate для отображения содержимого в ComboBox
            RoleComboBox.ItemsSource = allRoles;
            RoleComboBox.SelectedItem = SelectedRole;
            DataContext = this;

        }

        private void LoadUsers(int startIndex)
        {
            userList.Clear();
            int endIndex = startIndex + pageSize;

            var usersToDisplay = allUsers.Where(user => user.Roles.Contains(SelectedRole)).ToList();

            for (int i = startIndex; i < endIndex && i < usersToDisplay.Count; i++)
            {
                userList.Add(usersToDisplay[i]);
            }
        }

        private void NextPageButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int startIndex = userList.Count;
            LoadUsers(startIndex);
        }

        private void PreviousPageButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int startIndex = System.Math.Max(userList.Count - pageSize * 2, 0);
            LoadUsers(startIndex);
        }

        private void RoleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Обновляем выбранную роль
            SelectedRole = (UserRole)RoleComboBox.SelectedItem;
        }
    }
}
