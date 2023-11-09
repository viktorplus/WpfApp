using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Domain
{
    public class UserList
    {
        private List<User> users;

        public UserList()
        {
            users = new List<User>();

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
        }

        public void RemoveUser(User user)
        {
            users.Remove(user);
        }

        public List<User> GetAllUsers()
        {
            return users;
        }
    }
}