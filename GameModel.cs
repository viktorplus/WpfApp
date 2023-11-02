using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    public class GameModel : INotifyPropertyChanged
    {
        public Tile[,] Tiles { get; set; }
        public int Score { get; set; }
        public bool IsGameOver { get; set; }


        public void MoveLeft() { /* Логика для движения влево */ }
        public void MoveRight() { /* Логика для движения вправо */ }
        public void MoveUp() { /* Логика для движения вверх */ }
        public void MoveDown() { /* Логика для движения вниз */ }
        public void Reset() { }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
