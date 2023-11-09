using System.Windows.Controls;

namespace WpfApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для Info.xaml
    /// </summary>
    public partial class Info : UserControl
    {
        Frame MainFrame;
        public Info(Frame mainFrame)
        {
            InitializeComponent();
            this.MainFrame = mainFrame;
        }
    }
}
