using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;

namespace WpfApp.Pages
{
    public partial class BuildingList : UserControl
    {
        // Размер страницы при пагинации
        private int pageSize = 5;

        // Текущий индекс страницы
        private int currentIndex = 0;

        public BuildingList()
        {
            InitializeComponent();
            //MainWindow.SubjectList.GenerateSubjects();
            DataContext = MainWindow.BuildingList; // Используйте полное имя пространства имен
            BuildingsListView.ItemsSource = MainWindow.BuildingList.GetAllSubjects().Take(pageSize).ToList();
        }

        // Метод для загрузки пользователей в соответствии с текущей ролью и индексом страницы
        private void LoadObjList()
        {
            int endIndex = currentIndex + pageSize;

            // Фильтрация
            var SubjectToDisplay = MainWindow.BuildingList.GetAllSubjects()
                .Skip(currentIndex)
                .Take(pageSize)
                .ToList();

            // Установка нового источника данных для списка пользователей
            BuildingsListView.ItemsSource = SubjectToDisplay;
        }

        // Обработчик события для переключения на следующую страницу
        private void NextPageButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            currentIndex += pageSize;
            LoadObjList();
        }

        // Обработчик события для переключения на предыдущую страницу
        private void PreviousPageButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            currentIndex = System.Math.Max(currentIndex - pageSize, 0);
            LoadObjList();
        }

        private void ShowAddSubjectFormButton_Click(object sender, RoutedEventArgs e)
        {
            // Показываем форму для добавления предмета
            AddSubjectForm.Visibility = Visibility.Visible;
        }

        private void AddToSubjectListButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем текст из TextBox
            string subjectName = SubjectNameTextBox.Text;

            // Создаем новый предмет
            Building newSubject = new Building(subjectName);

            // Добавляем предмет в список
            MainWindow.BuildingList.AddBuilding(newSubject);

            // Скрываем форму для добавления предмета
            AddSubjectForm.Visibility = Visibility.Collapsed;

            // Очищаем TextBox
            SubjectNameTextBox.Text = string.Empty;
            LoadObjList();
        }


    }
}
