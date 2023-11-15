using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;

namespace WpfApp.Pages
{
    public partial class ClassroomList : UserControl
    {
        private int pageSize = 5;
        private int currentIndex = 0;

        public ClassroomList()
        {
            InitializeComponent();
            DataContext = MainWindow.ClassroomList;
            LoadObjList();
        }

        private void LoadObjList()
        {
            int endIndex = currentIndex + pageSize;
            var SubjectToDisplay = MainWindow.ClassroomList.Classrooms.Skip(currentIndex).Take(pageSize);
            ClassroomListView.ItemsSource = SubjectToDisplay;
        }

        private void NextPageButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            currentIndex += pageSize;
            LoadObjList();
        }

        private void PreviousPageButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            currentIndex = System.Math.Max(currentIndex - pageSize, 0);
            LoadObjList();
        }

        private void ShowAddSubjectFormButton_Click(object sender, RoutedEventArgs e)
        {
            AddSubjectForm.Visibility = Visibility.Visible;
        }

        private void AddToSubjectListButton_Click(object sender, RoutedEventArgs e)
        {
            string subjectName = SubjectNameTextBox.Text;
            Classroom newSubject = new Classroom(subjectName);
            MainWindow.ClassroomList.AddClassroom(newSubject);
            AddSubjectForm.Visibility = Visibility.Collapsed;
            SubjectNameTextBox.Text = string.Empty;
            LoadObjList();
        }


    }
}
