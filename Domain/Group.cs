using System.ComponentModel;

namespace WpfApp.Domain
{
    public class Group : INotifyPropertyChanged
    {
        private string _groupName;

        public Group(string groupName)
        {
            _groupName = groupName;
        }

        public string GroupName
        {
            get { return _groupName; }
            set
            {
                if (_groupName != value)
                {
                    _groupName = value;
                    OnPropertyChanged(nameof(GroupName));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
