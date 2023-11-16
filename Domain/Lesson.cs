using System;
using System.ComponentModel;

namespace WpfApp.Domain
{
    public class Lesson : INotifyPropertyChanged
    {
        private User _student;
        private bool _isPresent;
        private int _lessonGrade;
        private string _homework;
        private bool _isHomeworkSubmitted;
        private int _homeworkGrade;

        public Lesson (User student, bool isPresent, int lessonGrade, string homework, bool isHomeworkSubmitted, int homeworkGrade)
        {
            Student = student;
            IsPresent = isPresent;
            LessonGrade = lessonGrade;
            Homework = homework;
            IsHomeworkSubmitted = isHomeworkSubmitted;
            HomeworkGrade = homeworkGrade;
        }

        public User Student
        {
            get { return _student; }
            private set
            {
                if (_student != value)
                {
                    _student = value;
                    OnPropertyChanged(nameof(Student));
                }
            }
        }

        public bool IsPresent
        {
            get { return _isPresent; }
            set
            {
                if (_isPresent != value)
                {
                    _isPresent = value;
                    OnPropertyChanged(nameof(IsPresent));
                }
            }
        }

        public int LessonGrade
        {
            get { return _lessonGrade; }
            set
            {
                if (_lessonGrade != value)
                {
                    _lessonGrade = value;
                    OnPropertyChanged(nameof(LessonGrade));
                }
            }
        }

        public string Homework
        {
            get { return _homework; }
            set
            {
                if (_homework != value)
                {
                    _homework = value;
                    OnPropertyChanged(nameof(Homework));
                }
            }
        }

        public bool IsHomeworkSubmitted
        {
            get { return _isHomeworkSubmitted; }
            set
            {
                if (_isHomeworkSubmitted != value)
                {
                    _isHomeworkSubmitted = value;
                    OnPropertyChanged(nameof(IsHomeworkSubmitted));
                }
            }
        }

        public int HomeworkGrade
        {
            get { return _homeworkGrade; }
            set
            {
                if (_homeworkGrade != value)
                {
                    _homeworkGrade = value;
                    OnPropertyChanged(nameof(HomeworkGrade));
                }
            }
        }

        //public Lesson(Schedules schedule, User student)
        //{
        //    Schedule = schedule ?? throw new ArgumentNullException(nameof(schedule));
        //    Student = student ?? throw new ArgumentNullException(nameof(student));
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
