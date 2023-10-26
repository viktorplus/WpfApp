using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Domain
{
    public class User : INotifyPropertyChanged
    {
        private string? _photo;
        private string? _name;
        private string? _surname;
        private string? _address;
        private string? _phone;

        public User(string photo, string name, string surname, string address, string phone)
        {
            Photo = string.IsNullOrEmpty(photo) ? "https://cdn.icon-icons.com/icons2/2630/PNG/512/diversity_avatar_people_beard_man_icon_159105.png" : photo;
            Name = name;
            Surname = surname;
            Address = address;
            Phone = phone;
        }

        public string? Photo
        {
            get { return _photo; }
            set
            {
                _photo = value;
                OnPropertyChanged(nameof(Photo));
            }
        }
        public string? Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }


        public string? Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public string? Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public string? Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }


}
