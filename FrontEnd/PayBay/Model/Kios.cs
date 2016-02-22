using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayBay.ViewModel;

namespace PayBay.Model
{
    public class Kios : BaseViewModel
    {
        private string _avatar;
        private string _name;
        private string _kiosname;
        private float _rating;

        public string Avatar
        {
            get { return _avatar; }
            set
            {
                if (value == _avatar) return;
                _avatar = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Kiosname
        {
            get { return _kiosname; }
            set
            {
                if (value == _kiosname) return;
                _kiosname = value;
                OnPropertyChanged();
            }
        }

        public float Rating
        {
            get { return _rating; }
            set
            {
                if (value == _rating) return;
                _rating = value;
                OnPropertyChanged();
            }
        }
    }
}
