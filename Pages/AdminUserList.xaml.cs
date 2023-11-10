using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using WpfApp.Domain;

namespace WpfApp.Pages
{
    public partial class AdminUserList : UserControl
    {
        private ObservableCollection<User> userList;
        private List<User> allUsers; // Предположим, что у вас есть экземпляр класса UserList
        private int pageSize = 10; // Количество отображаемых элементов на странице

        public AdminUserList()
        {
            InitializeComponent();
            userList = new ObservableCollection<User>();
            allUsers = new UserList().AllUsers; // Получаем всех пользователей из UserList

            // Загрузка первых пользователей (замените на свою логику)
            LoadUsers(0);
            UsersListView.ItemsSource = userList;
        }

        private void LoadUsers(int startIndex)
        {
            userList.Clear(); // Очищаем предыдущие записи

            // Логика загрузки пользователей (замените на свою логику)
            int endIndex = startIndex + pageSize;

            for (int i = startIndex; i < endIndex && i < allUsers.Count; i++)
            {
                userList.Add(allUsers[i]);
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            int startIndex = userList.Count;
            LoadUsers(startIndex);
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            int startIndex = Math.Max(userList.Count - pageSize * 2, 0);
            LoadUsers(startIndex);
        }

    }
}
