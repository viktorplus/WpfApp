using System.ComponentModel;

namespace WpfApp.Domain
{
    public class Building : INotifyPropertyChanged
    {
        private string _buildingNumber;

        public string BuildingNumber
        {
            get { return _buildingNumber; }
            set
            {
                if (_buildingNumber != value)
                {
                    _buildingNumber = value;
                    OnPropertyChanged(nameof(BuildingNumber));
                }
            }
        }

        public Building(string buildingNumber)
        {
            BuildingNumber = buildingNumber;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return BuildingNumber;
        }
    }
}
