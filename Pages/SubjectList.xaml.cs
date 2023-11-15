using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;

namespace WpfApp.Pages
{
    public partial class SubjectList : UserControl
    {
        private int pageSize = 5;

        private int currentIndex = 0;

        public SubjectList()
        {
            InitializeComponent();
            DataContext = MainWindow.SubjectList; 
            LoadUsers();
        }
        private void LoadUsers()
        {
            int endIndex = currentIndex + pageSize;
            var SubjectToDisplay = MainWindow.SubjectList.Subjects.Skip(currentIndex).Take(pageSize);
            SubjectsListView.ItemsSource = SubjectToDisplay;
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

        private void ShowAddSubjectFormButton_Click(object sender, RoutedEventArgs e)
        {
            AddSubjectForm.Visibility = Visibility.Visible;
        }

        private void AddToSubjectListButton_Click(object sender, RoutedEventArgs e)
        {
            string subjectName = SubjectNameTextBox.Text;
            Subject newSubject = new Subject(subjectName);
            MainWindow.SubjectList.AddSubject(newSubject);
            AddSubjectForm.Visibility = Visibility.Collapsed;
            SubjectNameTextBox.Text = string.Empty;
        }


    }
}
