using System;
using System.Collections.Generic;
using System.ComponentModel;
using BCrypt.Net;
using static WpfApp.Domain.User;

namespace WpfApp.Domain
{
    public class UserList : INotifyPropertyChanged
    {
        private List<User> users;
        private List<UserRole> allRoles;

        public UserList()
        {
            users = new List<User>();
            allRoles = new List<UserRole>();

            // Инициализируем allRoles со всеми значениями перечисления UserRole
            allRoles.AddRange((UserRole[])Enum.GetValues(typeof(UserRole)));

            GenerateUsers();
        }

        public List<UserRole> AllRoles
        {
            get { return allRoles; }
        }

        public List<User> AllUsers
        {
            get { return users; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void GenerateUsers()
        {
            //Random random = new Random();

            for (int i = 1; i <= 20; i++)
            {
                string firstName = "StudentFirstName" + i;
                string lastName = "StudentLastName" + i;
                string username = "student" + i;
                string password = "password" + i;

                List<UserRole> roles = new List<UserRole> { UserRole.Student };
                User student = new User(firstName, lastName, username, password, roles);
                AddUser(student);
            }

            for (int i = 1; i <= 3; i++)
            {
                string firstName = "TeacherFirstName" + i;
                string lastName = "TeacherLastName" + i;
                string username = "teacher" + i;
                string password = "password" + i;

                List<UserRole> roles = new List<UserRole> { UserRole.Lecturer };
                User teacher = new User(firstName, lastName, username, password, roles);
                AddUser(teacher);
            }

            for (int i = 1; i <= 1; i++)
            {
                string firstName = "AdminFirstName" + i;
                string lastName = "AdminLastName" + i;
                string username = "ad" + i;
                string password = "ad" + i;

                List<UserRole> roles = new List<UserRole> { UserRole.Admin };
                User teacher = new User(firstName, lastName, username, password, roles);
                AddUser(teacher);
            }

        }

        public bool ValidateUser(string login, string password)
        {
            foreach (User user in users)
            {
                if (user.Username == login && user.VerifyPassword(password))
                {
                    return true;
                }
            }
            return false;
        }

        public User GetUserByLogin(string login)
        {
            return users.Find(user => user.Username == login);
        }

        public void AddUser(User user)
        {
            users.Add(user);
            user.PropertyChanged += User_PropertyChanged;

            OnPropertyChanged(nameof(AllUsers));
        }

        public void RemoveUser(User user)
        {
            users.Remove(user);
            user.PropertyChanged -= User_PropertyChanged;

            OnPropertyChanged(nameof(AllUsers));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void User_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(AllUsers));
        }
    }
}
