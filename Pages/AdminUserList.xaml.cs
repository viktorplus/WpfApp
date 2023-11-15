using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp.Domain;
using static WpfApp.Domain.User;

namespace WpfApp.Pages
{
    public partial class AdminUserList : UserControl, INotifyPropertyChanged
    {
        // Команда для изменения роли пользователя
        public ICommand ChangeRoleCommand { get; set; }

        // Размер страницы при пагинации
        private int pageSize = 5;

        // Текущий индекс страницы
        private int currentIndex = 0;

        // Выбранная роль для отображения пользователей
        private UserRole selectedRole;

        // Свойство для отслеживания изменений выбранной роли
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

        // Конструктор класса
        public AdminUserList()
        {
            InitializeComponent();
            //SelectedRole = UserRole.Student;
            UsersListView.ItemsSource = MainWindow.UserList.AllUsers.Take(pageSize).ToList();
            RoleButtonsContainer.ItemsSource = MainWindow.UserList.AllRoles;

            // Инициализация команды для изменения роли
            ChangeRoleCommand = new RelayCommand(ChangeRole);
            DataContext = this;

            // Добавляем обработчик события для сортировки по заголовку колонки
            AddGridViewColumnHeaderClickEvent();
        }

        // Метод для изменения роли пользователя
        private void ChangeRole(object parameter)
        {
            UserRole selectedRole = (UserRole)parameter;
            SelectedRole = selectedRole;
            currentIndex = 0; // Сбросим индекс при изменении роли
            LoadUsers();
        }

        // Метод для загрузки пользователей в соответствии с текущей ролью и индексом страницы
        private void LoadUsers()
        {
            int endIndex = currentIndex + pageSize;

            // Фильтрация и пагинация пользователей в соответствии с выбранной ролью
            var usersToDisplay = MainWindow.UserList.AllUsers
                .Where(user => user.Roles.Contains(SelectedRole))
                .Skip(currentIndex)
                .Take(pageSize)
                .ToList();

            // Установка нового источника данных для списка пользователей
            UsersListView.ItemsSource = usersToDisplay;
        }

        // Метод для добавления обработчика события сортировки по заголовку колонки
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

        // Обработчик события для переключения на следующую страницу
        private void NextPageButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            currentIndex += pageSize;
            LoadUsers();
        }

        // Обработчик события для переключения на предыдущую страницу
        private void PreviousPageButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            currentIndex = System.Math.Max(currentIndex - pageSize, 0);
            LoadUsers();
        }

        // Обработчик события для сортировки по заголовку колонки
        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (GridViewColumnHeader)e.OriginalSource;
            string columnName = column.Content.ToString();

            var sortedUsers = MainWindow.UserList.AllUsers;

            // сортировка в зависимости от выбранной колонки
            switch (columnName)
            {
                case "Имя":
                    sortedUsers = sortedUsers.OrderBy(user => user.FirstName).ToList();
                    break;
                case "Фамилия":
                    sortedUsers = sortedUsers.OrderBy(user => user.LastName).ToList();
                    break;
                case "Логин":
                    sortedUsers = sortedUsers.OrderBy(user => user.Username).ToList();
                    break;
                    // Добавьте дополнительные кейсы для других колонок, если необходимо
            }

            // Обновить источник данных для UsersListView
            UsersListView.ItemsSource = sortedUsers;
        }


        // Обработчик события изменения выделения в списке пользователей
        private void UsersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
