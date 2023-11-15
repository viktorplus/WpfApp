using System;
using System.Collections.Generic;
using System.ComponentModel;
using WpfApp.Pages;

namespace WpfApp.Domain
{
    public class SchedulesList : INotifyPropertyChanged
    {
        private List<Schedule> _schedules = new List<Schedule>();

        public List<Schedule> Schedules
        {
            get { return _schedules; }
            set
            {
                _schedules = value;
                OnPropertyChanged(nameof(Schedules));
            }
        }

        public SchedulesList()
        {
            //GenerateSchedules();
        }

        //public void GenerateSchedules()
        //{
        //    // Здесь вы можете добавить логику для генерации расписания
        //    // Пример:
        //    // Добавляем расписание для первой группы в первом здании в первой аудитории
        //    Schedule schedule1 = new Schedule
        //    {
        //        Subject = SubjectList.GetAllSubjects()[0],
        //        Group = GroupList.Groups[0],
        //        Classroom = BuildingList.Buildings[0].Classrooms[0],
        //        Building = BuildingList.Buildings[0],
        //        Date = DateTime.Now.AddDays(1),
        //        Time = new TimeSpan(9, 0, 0),
        //        Lecturer = UserList.GetAllUsers().Find(user => user.Roles.Contains(User.UserRole.Lecturer))
        //    };

        //    // Добавляем расписание для второй группы во втором здании во второй аудитории
        //    Schedule schedule2 = new Schedule
        //    {
        //        Subject = SubjectList.GetAllSubjects()[1],
        //        Group = GroupList.Groups[1],
        //        Classroom = BuildingList.Buildings[1].Classrooms[1],
        //        Building = BuildingList.Buildings[1],
        //        Date = DateTime.Now.AddDays(1),
        //        Time = new TimeSpan(10, 0, 0),
        //        Lecturer = UserList.GetAllUsers().Find(user => user.Roles.Contains(User.UserRole.Lecturer))
        //    };

        //    AddSchedule(schedule1);
        //    AddSchedule(schedule2);
        //}

        public void AddSchedule(Schedule schedule)
        {
            _schedules.Add(schedule);
            OnPropertyChanged(nameof(Schedules));
        }

        public void RemoveSchedule(Schedule schedule)
        {
            _schedules.Remove(schedule);
            OnPropertyChanged(nameof(Schedules));
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
