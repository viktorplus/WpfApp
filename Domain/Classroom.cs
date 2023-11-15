using System.ComponentModel;

namespace WpfApp.Domain
{
    public class Classroom : INotifyPropertyChanged
    {
        private string _roomNumber;

        public string RoomNumber
        {
            get { return _roomNumber; }
            set
            {
                if (_roomNumber != value)
                {
                    _roomNumber = value;
                    OnPropertyChanged(nameof(RoomNumber));
                }
            }
        }

        public Classroom(string roomNumber)
        {
            RoomNumber = roomNumber;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return RoomNumber;
        }
    }
}
