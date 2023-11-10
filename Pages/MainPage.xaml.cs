using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;

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

        private void About_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content.GetType() != typeof(Info))
            {
                MainFrame.Content = new Info(MainFrame);
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