using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WpfApp.Domain
{
    public class BuildingList : INotifyPropertyChanged
    {
        private List<Building> _buildings;

        public List<Building> Buildings
        {
            get { return _buildings; }
            set
            {
                _buildings = value;
                OnPropertyChanged(nameof(Buildings));
            }
        }

        public BuildingList()
        {
            _buildings = new List<Building>();
            GenerateBuildings();
            OnPropertyChanged(nameof(Buildings));
        }
        public List<Building> GetAllSubjects()
        {
            return _buildings;
        }

        public void GenerateBuildings()
        {
            for (int i = 1; i <= 2; i++)
            {
                string buildingNumber = "Building" + i;
                Building building = new Building(buildingNumber);
                AddBuilding(building);
            }
            OnPropertyChanged(nameof(Buildings));
        }

        public void AddBuilding(Building building)
        {
            _buildings.Add(building);
            OnPropertyChanged(nameof(Buildings));
        }

        public void RemoveBuilding(Building building)
        {
            _buildings.Remove(building);
            OnPropertyChanged(nameof(Buildings));
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
