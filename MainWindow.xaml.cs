using WpfApp.Domain;
using System.Linq;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

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
            list.AddUser(new User(@"https://cdn.icon-icons.com/icons2/2630/PNG/512/diversity_avatar_people_beard_man_icon_159105.png", "Ольга", "Васильева", "Днепр, Набережная 35", "38045789654"));
            InitializeComponent();

            LVMain.ItemsSource = list.Users;
            SearchTextBox.TextChanged += SearchTextBox_TextChanged;

            // Устанавливаем первоначальное значение в текстовые поля
            PhotoTextBox.Text = "Ссылка на фото";
            NameTextBox.Text = "Имя";
            SurnameTextBox.Text = "Фамилия";
            AddressTextBox.Text = "Адрес";
            PhoneTextBox.Text = "Телефон";
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            string photo = PhotoTextBox.Text;
            string name = NameTextBox.Text;
            string surname = SurnameTextBox.Text;

            if (string.IsNullOrEmpty(photo) || (photo == "Ссылка на фото"))
            {
                // Если ссылка на изображение не указана, используйте изображение по умолчанию
                photo = "https://cdn.icon-icons.com/icons2/2630/PNG/512/diversity_avatar_people_beard_man_icon_159105.png";
            }

            // Проверка наличия пользователя с такими именем и фамилией
            if (list.Users.Any(user => user.Name == name && user.Surname == surname))
            {
                MessageBox.Show("Пользователь с таким именем и фамилией уже существует.");
            }
            else
            {
                string address = AddressTextBox.Text;
                string phone = PhoneTextBox.Text;

                User newUser = new User(photo, name, surname, address, phone);

                list.AddUser(newUser);
                //LVMain.Items.Refresh(); // Обновление списка

                // Очистка полей после добавления
                PhotoTextBox.Text = "Ссылка на фото";
                NameTextBox.Text = "Имя";
                SurnameTextBox.Text = "Фамилия";
                AddressTextBox.Text = "Адрес";
                PhoneTextBox.Text = "Телефон";
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (LVMain.SelectedItem is User selectedUser)
            {
                string name = NameTextBox.Text;
                string surname = SurnameTextBox.Text;
                string address = AddressTextBox.Text;
                string phone = PhoneTextBox.Text;

                // Проверка наличия пользователя с такими именем и фамилией, исключая текущего выбранного пользователя
                if (list.Users.Any(user => user != selectedUser && user.Name == name && user.Surname == surname))
                {
                    MessageBox.Show("Пользователь с таким именем и фамилией уже существует.");
                }
                else
                {
                    // Обновите данные выбранного пользователя
                    selectedUser.Name = name;
                    selectedUser.Surname = surname;
                    selectedUser.Address = address;
                    selectedUser.Phone = phone;
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (LVMain.SelectedItem is User selectedUser)
            {
                list.RemoveUser(selectedUser);
                //LVMain.Items.Refresh(); // Обновление списка
            }
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

                LVMain.ItemsSource = searchResults;
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

        private void LVMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LVMain.SelectedItem is User selectedUser)
            {
                // Заполните текстовые поля данными из выбранного контакта
                NameTextBox.Text = selectedUser.Name;
                SurnameTextBox.Text = selectedUser.Surname;
                AddressTextBox.Text = selectedUser.Address;
                PhoneTextBox.Text = selectedUser.Phone;
            }
        }
    }
}
