using PayBay.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBay.Model
{
    public class Coordinate : BaseViewModel
    {
        private double _latitute;
        private double _longitute;

        public double Latitute
        {
            get
            {
                return _latitute;
            }

            set
            {
                _latitute = value;
                OnPropertyChanged();
            }
        }

        public double Longitute
        {
            get
            {
                return _longitute;
            }

            set
            {
                _longitute = value;
                OnPropertyChanged();
            }
        }

        public Coordinate()
        {
            _latitute = 0;
            _longitute = 0;
        }
    }
}
