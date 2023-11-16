using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;

namespace WpfApp.Pages
{
    public partial class ScheduleList : UserControl
    {
        private int pageSize = 5;
        private int currentIndex = 0;

        public ScheduleList()
        {
            InitializeComponent();
            DataContext = MainWindow.ScheduleList1;
            LoadObjList();
        }

        private void LoadObjList()
        {
            int endIndex = currentIndex + pageSize;

            if (MainWindow.CurrentUser != null)
            {
                if (MainWindow.CurrentUser.Roles.Contains(User.UserRole.Admin))
                {
                    // Администратор видит все расписания
                    var schedulesToDisplay = MainWindow.ScheduleList1.Schedules
                        .Skip(currentIndex)
                        .Take(pageSize)
                        .ToList();

                    ScheduleListView.ItemsSource = schedulesToDisplay;
                    ScheduleListView.Items.Refresh();
                }
                else if (MainWindow.CurrentUser.Roles.Contains(User.UserRole.Lecturer))
                {
                    // Преподаватель видит только расписания, где он указан в качестве преподавателя
                    var lecturerSchedules = MainWindow.ScheduleList1.Schedules
                        .Where(schedule => schedule.Lecturer == MainWindow.CurrentUser)
                        .Skip(currentIndex)
                        .Take(pageSize)
                        .ToList();

                    ScheduleListView.ItemsSource = lecturerSchedules;
                    ScheduleListView.Items.Refresh();
                }
                else if (MainWindow.CurrentUser.Roles.Contains(User.UserRole.Student))
                {
                    // Студент видит только расписания своей группы
                    var studentSchedules = MainWindow.ScheduleList1.Schedules
                        .Where(schedule => schedule.Group == MainWindow.CurrentUser.Group)
                        .Skip(currentIndex)
                        .Take(pageSize)
                        .ToList();

                    ScheduleListView.ItemsSource = studentSchedules;
                    ScheduleListView.Items.Refresh();
                }
            }
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



    }
}
