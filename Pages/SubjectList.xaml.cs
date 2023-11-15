using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;

namespace WpfApp.Pages
{
    public partial class SubjectList : UserControl
    {
        // Размер страницы при пагинации
        private int pageSize = 10;

        // Текущий индекс страницы
        private int currentIndex = 0;

        public SubjectList()
        {
            InitializeComponent();
            //MainWindow.SubjectList.GenerateSubjects();
            DataContext = MainWindow.SubjectList; // Используйте полное имя пространства имен
            SubjectsListView.ItemsSource = MainWindow.SubjectList.GetAllSubjects().Take(pageSize).ToList();
        }

        // Метод для загрузки пользователей в соответствии с текущей ролью и индексом страницы
        private void LoadUsers()
        {
            int endIndex = currentIndex + pageSize;

            // Фильтрация и пагинация пользователей в соответствии с выбранной ролью
            var SubjectToDisplay = MainWindow.SubjectList.GetAllSubjects()
                .Skip(currentIndex)
                .Take(pageSize)
                .ToList();

            // Установка нового источника данных для списка пользователей
            SubjectsListView.ItemsSource = SubjectToDisplay;
        }


        // Обработчик клика по кнопке "Добавить предмет"
        private void AddSubjectButton_Click(object sender, RoutedEventArgs e)
        {
            // Создаем новый предмет
            Subject newSubject = new Subject ("Новый предмет"); 

            // Добавляем предмет в список
            MainWindow.SubjectList.AddSubject(newSubject);
        }


    }
}
