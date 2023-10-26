using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WpfApp.Domain
{
    public class UsersList : INotifyPropertyChanged
    {
        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

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
            Users = new ObservableCollection<User>();
        }

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public void RemoveUser(User user)
        {
            Users.Remove(user);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}




//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;

//namespace WpfApp.Domain
//{
//    public class UsersList : INotifyPropertyChanged
//    {
//        private List<User> users;
//        public List<User> Users
//        {
//            get { return users; }
//            set
//            {
//                users = value;
//                OnPropertyChanged(nameof(Users));
//            }
//        }

//        private User selectedUser;
//        public User SelectedUser
//        {
//            get { return selectedUser; }
//            set
//            {
//                selectedUser = value;
//                OnPropertyChanged(nameof(SelectedUser));
//            }
//        }

//        public UsersList()
//        {
//            Users = new List<User>();
//        }

//        public void AddUser(User user)
//        {
//            Users.Add(user);
//            OnPropertyChanged(nameof(Users));
//        }

//        public void RemoveUser(User user)
//        {
//            Users.Remove(user);
//            OnPropertyChanged(nameof(Users));
//        }

//        public event PropertyChangedEventHandler PropertyChanged;
//        protected void OnPropertyChanged(string PropertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
//        }
//    }
//}
