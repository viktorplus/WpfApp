using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WpfApp.Domain
{
    public class ClassroomList : INotifyPropertyChanged
    {
        private List<Classroom> _classrooms;

        public List<Classroom> Classrooms
        {
            get { return _classrooms; }
            set
            {
                _classrooms = value;
                OnPropertyChanged(nameof(Classrooms));
            }
        }

        public ClassroomList()
        {
            _classrooms = new List<Classroom>();
            GenerateClassrooms();
        }

        public void GenerateClassrooms()
        {
            for (int i = 1; i <= 5; i++)
            {
                string roomNumber = "Room" + i;
                Classroom classroom = new Classroom(roomNumber);
                AddClassroom(classroom);
            }
            OnPropertyChanged(nameof(Classrooms));
        }

        public void AddClassroom(Classroom classroom)
        {
            _classrooms.Add(classroom);
            OnPropertyChanged(nameof(Classrooms));
        }

        public void RemoveClassroom(Classroom classroom)
        {
            _classrooms.Remove(classroom);
            OnPropertyChanged(nameof(Classrooms));
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
