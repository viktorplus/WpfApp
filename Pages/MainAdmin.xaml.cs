using System.Windows;
using System.Windows.Controls;

namespace WpfApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainAdmin.xaml
    /// </summary>
    public partial class MainAdmin
    {
        Frame MainFrame;
        public MainAdmin(Frame MainFrame)
        {
            InitializeComponent();
            this.MainFrame = MainFrame;
        }
    }
}
