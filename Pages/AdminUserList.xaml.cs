using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using WpfApp.Domain;
using System.Linq;

public partial class AdminUserList : UserControl
{
    private UserList userList;
    private ObservableCollection<User> users;

    public AdminUserList()
    {
        InitializeComponent();
        userList = new UserList();
        LoadUsers(50);
    }

    private void LoadUsers(int count)
    {
        List<User> loadedUsers = userList.GetAllUsers().Take(count).ToList();

        if (users == null)
        {
            users = new ObservableCollection<User>(loadedUsers);
            userListView.ItemsSource = users;
        }
        else
        {
            foreach (User user in loadedUsers)
            {
                users.Add(user);
            }
        }
    }


    private void LoadNextUsers_Click(object sender, RoutedEventArgs e)
    {
        LoadUsers(50);
    }
}
