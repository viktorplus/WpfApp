using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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
using WpfApp;
using WpfApp.Domain;
using WpfApp.Navigator;

namespace WpfApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            MainFrame.Content = new Login(MainFrame);
        }

        private void MainPage_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content.GetType() != typeof(Login))
            {
                MainFrame.Content = new Login(MainFrame);
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content.GetType() != typeof(Login))
            {
                MainFrame.Content = new Login(MainFrame);
            }
        }
    }

}