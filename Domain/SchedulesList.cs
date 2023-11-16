﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using WpfApp.Pages;

namespace WpfApp.Domain
{
    public class SchedulesList : INotifyPropertyChanged
    {
        private List<Schedules> _schedules;

        public List<Schedules> Schedules
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
            _schedules = new List<Schedules>();
            GenerateSchedules();
        }

        public void GenerateSchedules()
        {
            // Добавляем расписание для первой группы в первом здании в первой аудитории
            Schedules schedule1 = new Schedules
            {
                Subject = MainWindow.SubjectList.Subjects[0],
                Group = MainWindow.GroupList.Groups[0],
                Classroom = MainWindow.ClassroomList.Classrooms[0],
                Building = MainWindow.BuildingList.Buildings[0],
                Date = DateTime.Now.AddDays(1),
                Time = new TimeSpan(9, 0, 0),
                Lecturer = MainWindow.UserList.AllUsers.Find(user => user.Roles.Contains(User.UserRole.Lecturer))
            };


            // Добавляем расписание для второй группы во втором здании во второй аудитории
            Schedules schedule2 = new Schedules
            {
                Subject = MainWindow.SubjectList.Subjects[1],
                Group = MainWindow.GroupList.Groups[1],
                Classroom = MainWindow.ClassroomList.Classrooms[1],
                Building = MainWindow.BuildingList.Buildings[0],
                Date = DateTime.Now.AddDays(1),
                Time = new TimeSpan(10, 0, 0),
                Lecturer = MainWindow.UserList.AllUsers.Find(user => user.Roles.Contains(User.UserRole.Lecturer))
            };

            AddSchedule(schedule1);
            AddSchedule(schedule2);
        }

        public void AddSchedule(Schedules schedule)
        {
            _schedules.Add(schedule);
            OnPropertyChanged(nameof(Schedules));
        }

        public void RemoveSchedule(Schedules schedule)
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
