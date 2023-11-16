using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;
using WpfApp.Navigator;
using WpfApp.Pages;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {

        public static User? CurrentUser { get; set; }
        public static Domain.UserList UserList { get; } = new Domain.UserList();
        public static Domain.SubjectList SubjectList { get; } = new Domain.SubjectList();
        public static Domain.BuildingList BuildingList { get; } = new Domain.BuildingList();
        public static Domain.ClassroomList ClassroomList { get; } = new Domain.ClassroomList();
        public static Domain.GroupList GroupList { get; } = new Domain.GroupList();
        public static Domain.SchedulesList ScheduleList1 { get; } = new Domain.SchedulesList();


        public MainWindow()
        {
  
            InitializeComponent();

            //UserList.GenerateUsers();
            //SubjectList.GenerateSubjects();
            //BuildingList.GenerateBuildings();
            //ClassroomList.GenerateClassrooms();
            //GroupList.GenerateGroups();
            //ScheduleList1.GenerateSchedules();

            System.Diagnostics.Debug.WriteLine("Number of schedules in ScheduleList1: " + ScheduleList1.Schedules.Count);
            // Проверка содержимого списка
            foreach (var schedule in MainWindow.ScheduleList1.Schedules)
            {
                System.Diagnostics.Debug.WriteLine(schedule.ToString());
            }

            NavigatorObject.pageSwitcher = this;
            NavigatorObject.Switch(new MainPage());
        }

        //public Action? CloseAction { get; set; }

        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        public void Navigate(UserControl nextPage, object state)
        {
            this.Content = nextPage;
            INavigator? s = nextPage as INavigator;

            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("NextPage is not INavigator! " + nextPage.Name.ToString());
        }
    }
}