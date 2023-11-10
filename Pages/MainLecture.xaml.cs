using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp.Domain;

namespace WpfApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainLecture.xaml
    /// </summary>
    public partial class MainLecture
    {

        private UserList userList;
        private User currentUser;
        public MainLecture(User currentUser)
        {
            InitializeComponent();
            this.userList = userList;
            this.currentUser = currentUser;
            //LectureFrame.Content = new AdminUserList(userList, currentUser);
        }
    }
}
