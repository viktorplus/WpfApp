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
            allRoles = MainWindow.UserList.AllRoles;
            SelectedRole = UserRole.Student;
            UsersListView.ItemsSource = userList;
            RoleComboBox.ItemsSource = allRoles;
            DataContext = this;

            // Добавляем обработчик события для сортировки по заголовку колонки
            AddGridViewColumnHeaderClickEvent();
        }

        private void LoadUsers(int startIndex)
        {
            userList.Clear();
            int endIndex = startIndex + pageSize;

            var usersToDisplay = MainWindow.UserList.AllUsers
                .Where(user => user.Roles.Contains(SelectedRole))
                .ToList();

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
            SelectedRole = (UserRole)RoleComboBox.SelectedItem;
        }

        private void AddGridViewColumnHeaderClickEvent()
        {
            foreach (var column in ((GridView)UsersListView.View).Columns)
            {
                var columnHeader = column.Header as GridViewColumnHeader;
                if (columnHeader != null)
                {
                    columnHeader.Click += GridViewColumnHeader_Click;
                }
            }
        }

        private void GridViewColumnHeader_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GridViewColumnHeader column = (GridViewColumnHeader)e.OriginalSource;
            string columnName = column.Content.ToString();

            // Добавьте код сортировки по выбранной колонке
            // Например, используйте LINQ для сортировки userList
        }

        private void UsersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Обработка изменений в выделении списка
            // Добавьте код для показа кнопок удаления, изменения и добавления
            // на основе выбранной строки
        }
    }
}
