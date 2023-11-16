using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp.Domain;

namespace WpfApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainLecture.xaml
    /// </summary>
    public partial class MainLecture
    {
        public MainLecture()
        {
            InitializeComponent();
            LectureFrame.Content = new ScheduleList();
        }

        private void Schedules_Click(object sender, RoutedEventArgs e)
        {
            LectureFrame.Content = new ScheduleList();

        }
    }
}
