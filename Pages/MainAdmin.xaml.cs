using System.Windows;
using System.Windows.Controls;
using WpfApp.Navigator;

namespace WpfApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainAdmin.xaml
    /// </summary>
    public partial class MainAdmin : UserControl
    {
        Frame MainFrame;
        public MainAdmin()
        {
            InitializeComponent();
        }

        public MainAdmin(Frame mainFrame)
        {
            MainFrame = mainFrame;
        }
    }
}
