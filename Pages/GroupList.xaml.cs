using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;

namespace WpfApp.Pages
{
    public partial class GroupList : UserControl
    {
        private int pageSize = 5;
        private int currentIndex = 0;

        public GroupList()
        {
            InitializeComponent();
            DataContext = MainWindow.GroupList;
            LoadObjList();
        }

        private void LoadObjList()
        {
            int endIndex = currentIndex + pageSize;
            var SubjectToDisplay = MainWindow.GroupList.Groups.Skip(currentIndex).Take(pageSize);
            GroupListView.ItemsSource = SubjectToDisplay;
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
            Group newSubject = new Group(subjectName);
            MainWindow.GroupList.AddGroup(newSubject);
            AddSubjectForm.Visibility = Visibility.Collapsed;
            SubjectNameTextBox.Text = string.Empty;
            LoadObjList();
        }


    }
}
