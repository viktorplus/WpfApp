using System.Collections.Generic;
using System.ComponentModel;

namespace WpfApp.Domain
{
    public class UsersList : INotifyPropertyChanged
    {
        public List<User> Users { get; set; }

        private User selectedUser;
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        public UsersList()
        {
            Users = new List<User>();
        }

        public void AddUser(User user)
        {
            Users?.Add(user);
        }

        public void RemoveUser(User user)
        {
            Users?.Remove(user);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }

}
