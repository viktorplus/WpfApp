using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;

namespace WpfApp.Pages
{
    public partial class MakeSchedule : UserControl
    {
        public MakeSchedule()
        {
            InitializeComponent();
            LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            // Загружаем значения для выпадающих списков
            SubjectComboBox.ItemsSource = MainWindow.SubjectList.Subjects;
            GroupComboBox.ItemsSource = MainWindow.GroupList.Groups;
            ClassroomComboBox.ItemsSource = MainWindow.ClassroomList.Classrooms;
            BuildingComboBox.ItemsSource = MainWindow.BuildingList.Buildings;

            // Добавьте дополнительные строки для остальных списков (BuildingList, LecturerList и т.д.)
        }

        private void CreateSchedule_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранные значения из выпадающих списков
            var selectedSubject = (Subject)SubjectComboBox.SelectedItem;
            var selectedGroup = (Group)GroupComboBox.SelectedItem;
            var selectedClassroom = (Classroom)ClassroomComboBox.SelectedItem;
            var selectedBuilding = (Building)BuildingComboBox.SelectedItem;

            // Получаем выбранную дату
            var selectedDate = DatePicker.SelectedDate ?? DateTime.Now; // Если дата не выбрана, используем текущую

            // Получаем введенное пользователем время
            if (TimeTextBox.Text.Length == 0 || !TimeSpan.TryParse(TimeTextBox.Text, out var selectedTime))
            {
                MessageBox.Show("Please enter a valid time.");
                return;
            }

            // Получаем выбранного лектора
            var lecturerList = MainWindow.UserList.LecturerList; // Предположим, у вас есть LecturerList в UserList
            var selectedLecturer = lecturerList.FirstOrDefault();

            // Создаем новый объект Schedules
            var newSchedule = new Schedules(
                subject: selectedSubject,
                group: selectedGroup,
                classroom: selectedClassroom,
                building: selectedBuilding,
                date: selectedDate,
                time: selectedTime,
                lecturer: selectedLecturer
            );

            // Добавляем новое расписание в список
            MainWindow.ScheduleList1.AddSchedule(newSchedule);

            // Очищаем значения в выпадающих списках и элементах выбора даты/времени
            SubjectComboBox.SelectedIndex = -1;
            GroupComboBox.SelectedIndex = -1;
            ClassroomComboBox.SelectedIndex = -1;
            BuildingComboBox.SelectedIndex = -1;
            DatePicker.SelectedDate = DateTime.Now; // Или другую дату по умолчанию
            TimeTextBox.Text = string.Empty; // Очищаем TextBox

            MessageBox.Show("Schedule created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
