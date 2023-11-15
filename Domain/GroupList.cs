using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WpfApp.Domain
{
    public class GroupList : INotifyPropertyChanged
    {
        private List<Group> _groups = new List<Group>();

        public List<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        public GroupList()
        {
            GenerateGroups();
        }

        public void GenerateGroups()
        {
            for (int i = 1; i <= 10; i++)
            {
                string groupName = "Group" + i;
                Group group = new Group(groupName);
                AddGroup(group);
            }
            OnPropertyChanged(nameof(Groups));
        }

        public void AddGroup(Group group)
        {
            _groups.Add(group);
            OnPropertyChanged(nameof(Groups));
        }

        public void RemoveGroup(Group group)
        {
            _groups.Remove(group);
            OnPropertyChanged(nameof(Groups));
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
