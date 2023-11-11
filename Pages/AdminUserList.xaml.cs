using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using WpfApp.Domain;
using static WpfApp.Domain.User;

namespace WpfApp.Pages
{
    public partial class AdminUserList : UserControl, INotifyPropertyChanged
    {
        private List<UserRole> allRoles;
        private int pageSize = 10;
        private int currentIndex = 0;

        private UserRole selectedRole;
        public UserRole SelectedRole
        {
            get { return selectedRole; }
            set
            {
                selectedRole = value;
                currentIndex = 0; // Сбросим индекс при изменении роли
                LoadUsers();
                OnPropertyChanged(nameof(SelectedRole));
            }
        }

        public AdminUserList()
        {
            InitializeComponent();
            allRoles = MainWindow.UserList.AllRoles;
            SelectedRole = UserRole.Student;
            UsersListView.ItemsSource = MainWindow.UserList.AllUsers.Take(pageSize).ToList();
            RoleComboBox.ItemsSource = allRoles;
            DataContext = this;

            // Добавляем обработчик события для сортировки по заголовку колонки
            AddGridViewColumnHeaderClickEvent();
        }

        private void LoadUsers()
        {
            int endIndex = currentIndex + pageSize;
            var usersToDisplay = MainWindow.UserList.AllUsers
                .Where(user => user.Roles.Contains(SelectedRole))
                .Skip(currentIndex)
                .Take(pageSize)
                .ToList();

            UsersListView.ItemsSource = usersToDisplay;
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

        private void NextPageButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            currentIndex += pageSize;
            LoadUsers();
        }

        private void PreviousPageButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            currentIndex = System.Math.Max(currentIndex - pageSize, 0);
            LoadUsers();
        }

        private void GridViewColumnHeader_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Реализуйте логику сортировки userList (теперь AllUsers), если требуется
        }

        private void UsersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Обработка изменений в выделении списка
            // Добавьте код для показа кнопок удаления, изменения и добавления
            // на основе выбранной строки
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
