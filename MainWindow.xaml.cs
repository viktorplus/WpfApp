using WpfApp.Domain;
using System.DirectoryServices.ActiveDirectory;
using System.Windows;
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
            list.AddUser(new User(@"https://illustrators.ru/uploads/illustration/image/1622361/4.jpg", "Ольга", "Васильева", "Днепр, Набережная 35", "38045789654"));
            InitializeComponent();

            LVMain.ItemsSource = list.Users;
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
            NameTextBox.Text = "";
            SurnameTextBox.Text = "";
            AddressTextBox.Text = "";
            PhoneTextBox.Text = "";
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
                // код для обработки пустого запроса
            }
            else
            {
                // Выполните поиск или фильтрацию на основе `searchQuery`
                // и обновите список пользователей в LVMain.ItemsSource
            }
        }

    }
}
