﻿using WpfApp.Domain;
using System.DirectoryServices.ActiveDirectory;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using System;
using System.Windows.Controls;
using System.Windows.Media;




namespace WpfApp
{
    public partial class MainWindow : Window
    {
        UsersList list;

        public MainWindow()
        {
            list = new UsersList();
            list.AddUser(new User(@"https://dthezntil550i.cloudfront.net/kg/latest/kg1802132010216500004834729/1280_960/557d644f-12f3-49e1-bb66-23c16400540d.png", "Илья", "Иванов", "Полтава, Незалежности 35", "380665248654"));
            list.AddUser(new User(@"https://www.malls.ru/upload/medialibrary/0a6/https_hypebeast.com_wp_content_blogs.dir_6_files_2023_01_avatar_4_and_5_movies_confirmed_james_cameron_1.jpg", "Константин", "Петров", "Киев, Свободы 35", "380695545654"));
            list.AddUser(new User(@"https://illustrators.ru/uploads/illustration/image/1687137/14.jpg", "Наталья", "Ершова", "Львов, Соборный проспект 35", "380685758654"));
            list.AddUser(new User(@"https://illustrators.ru/uploads/illustration/image/1622361/4.jpg", "Ольга", "Васильева", "Днепр, Набережная 35", "38045789654"));
            InitializeComponent();

            LVMain.ItemsSource = list.Users;
            SearchTextBox.TextChanged += SearchTextBox_TextChanged;
            // Устанавливаем первоначальное значение в текстовые поля
            NameTextBox.Text = "Имя";
            SurnameTextBox.Text = "Фамилия";
            AddressTextBox.Text = "Адрес";
            PhoneTextBox.Text = "Телефон";
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string surname = SurnameTextBox.Text;
            string address = AddressTextBox.Text;
            string phone = PhoneTextBox.Text;

            // Проверка на пустые поля или другие условия

            User newUser = new User("https://illustrators.ru/uploads/illustration/image/1622361/4.jpg", name, surname, address, phone);
            list.AddUser(newUser);
            LVMain.Items.Refresh(); // Обновление списка

            // Очистка полей после добавления
            NameTextBox.Text = "Имя";
            SurnameTextBox.Text = "Фамилия";
            AddressTextBox.Text = "Адрес";
            PhoneTextBox.Text = "Телефон";
        }

        private void LVMain_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (LVMain.SelectedItem is User selectedUser)
            {
                MessageBox.Show($"Номер телефона: {selectedUser.Phone}");
            }
        }

        private void CallButton_Click(object sender, RoutedEventArgs e)
        {
            if (LVMain.SelectedItem is User selectedUser)
            {
                MessageBox.Show($"Имя: {selectedUser.Name}\nНомер телефона: {selectedUser.Phone}", "Позвонить пользователю");
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = SearchTextBox.Text;

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                // Если поле поиска пустое, показываем всех пользователей
                LVMain.ItemsSource = list.Users;
            }
            else
            {
                // поиск или фильтрацию на основе `searchQuery`
                var searchResults = list.Users.Where(user =>
                    user.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    user.Surname.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    user.Address.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    user.Phone.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)
                ).ToList();

                LVMain.ItemsSource = searchResults; // Обновляем список пользователей в LVMain.ItemsSource
            }
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchQuery = SearchTextBox.Text;

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                // Если поле поиска пустое, покажите всех пользователей
                LVMain.ItemsSource = list.Users;
            }
            else
            {
                // Выполните поиск или фильтрацию на основе `searchQuery`
                var searchResults = list.Users.Where(user =>
                    user.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    user.Surname.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    user.Address.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    user.Phone.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)
                ).ToList();

                LVMain.ItemsSource = searchResults; // Обновите список пользователей в LVMain.ItemsSource
            }
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Foreground = Brushes.Black;
                if (textBox.Text == (string)textBox.Tag)
                {
                    textBox.Text = string.Empty;
                }
            }

        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text) || textBox.Text == (string)textBox.Tag)
                {
                    textBox.Foreground = Brushes.Gray;
                    textBox.Text = (string)textBox.Tag;
                }
            }
        }







    }
}
