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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GameModel Game { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Game = new GameModel();

        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Key.Left)
            {
                Game.MoveLeft();
            }
            if (e.Key == Key.Right)
            {
                Game.MoveRight();
            }
            if (e.Key == Key.Down)
            {
                Game.MoveDown();
            }
            if (e.Key == Key.Up)
            {
                Game.MoveUp();
            }
        }

    }
}
