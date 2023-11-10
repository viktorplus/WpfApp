using System;
using System.Collections.Generic;
using System.ComponentModel;
using BCrypt.Net;



namespace WpfApp.Domain
{
    public class User : INotifyPropertyChanged
    {
        private string _firstname;
        private string _lastname;
        private string _username;
        private string _password;
        private List<UserRole> _roles;

        public enum UserRole
        {
            Admin,
            Lecturer,
            Student
        }


        public User(string firstName, string lastName, string username, string password, List<UserRole> roles)
        {
            _firstname = firstName;
            _lastname = lastName;
            _username = username;
            _password = HashPassword(password); // Хэшируем пароль при создании
            _roles = roles;

        }
        public string FirstName
        {
            get { return _firstname; }
            set
            {
                _firstname = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get { return _lastname; }
            set
            {
                _lastname = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public List<UserRole> Roles
        {
            get { return _roles; }
            set
            {
                _roles = value;
                OnPropertyChanged(nameof(Roles));
            }
        }

        public void AddRole(UserRole role)
        {
            if (!_roles.Contains(role))
            {
                _roles.Add(role);
                OnPropertyChanged(nameof(Roles));
            }
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, _password); // Проверяем хэшированный пароль
        }

        private string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12); // Генерируем соль
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}