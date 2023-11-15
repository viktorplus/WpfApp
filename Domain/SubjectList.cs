using System;
using System.Collections.Generic;
using System.ComponentModel;
using static WpfApp.Domain.User;

namespace WpfApp.Domain
{
    public class SubjectList : INotifyPropertyChanged
    {
        private List<Subject> subjects = new List<Subject>();

        public SubjectList()
        {
            subjects = new List<Subject>();
            GenerateSubjects();
        }
        public List<Subject> Subjects
        {
            get { return subjects; }
            set
            {
                subjects = value;
                OnPropertyChanged(nameof(Subjects));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void GenerateSubjects()
        {
            for (int i = 1; i <= 20; i++)
            {
                string Name = "Subject" + i;
                Subject Subj = new Subject(Name);
                AddSubject(Subj);
            }
            OnPropertyChanged(nameof(Subjects)); 

        }
        public void AddSubject(Subject subject)
        {
            subjects.Add(subject);
            OnPropertyChanged(nameof(Subjects));
        }

        public void RemoveSubject(Subject subject)
        {
            subjects.Remove(subject);
            OnPropertyChanged(nameof(Subjects));
        }

        public List<Subject> GetAllSubjects()
        {
            return subjects;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
