using System;
using System.ComponentModel;

namespace WpfApp.Domain
{
    public class Schedules : INotifyPropertyChanged
    {
        private Subject _subject;
        private Group _group;
        private Classroom _classroom;
        private Building _building;
        private DateTime _date;
        private TimeSpan _time;
        private User _lecturer;

        public Subject Subject
        {
            get { return _subject; }
            set
            {
                _subject = value;
                OnPropertyChanged(nameof(Subject));
            }
        }

        public Group Group
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChanged(nameof(Group));
            }
        }

        public Classroom Classroom
        {
            get { return _classroom; }
            set
            {
                _classroom = value;
                OnPropertyChanged(nameof(Classroom));
            }
        }

        public Building Building
        {
            get { return _building; }
            set
            {
                _building = value;
                OnPropertyChanged(nameof(Building));
            }
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public TimeSpan Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        public User Lecturer
        {
            get { return _lecturer; }
            set
            {
                _lecturer = value;
                OnPropertyChanged(nameof(Lecturer));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public override string ToString()
        {
            return $"{Subject} - {Group} - {Classroom} - {Building} - {Date} - {Time} - {Lecturer}";
        }
    }
}
