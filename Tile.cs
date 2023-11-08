
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    public class Tiles: INotifyPropertyChanged
    {
        public int _num { get; set; }
        public bool _merged { get; set; }

        public Tiles (int num, bool merged) 
        {
            _merged = merged;
            _num = num;
        }

        public int Num
        {
            get { return _num; }
            set
            {
                _num = value;
                OnPropertyChanged(nameof(Num));
            }
        }
        public bool Merged 
        {
            get { return _merged; }
            set
            {
                _merged = value;
                OnPropertyChanged(nameof(Merged));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
