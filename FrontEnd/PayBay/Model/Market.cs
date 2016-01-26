using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayBay.ViewModel;

namespace PayBay.Model
{
    public class Market : BaseViewModel
    {
        private string _name;
        private string _image;
        private string _address;
        private string _itemtypes;
        private string _map;

        public string Map
        {
            get { return _map; }
            set
            {
                if (value == _map) return;
                _map = value;
                OnPropertyChanged();
            }
        }

        public string Itemtypes
        {
            get { return _itemtypes; }
            set
            {
                if (value == _itemtypes) return;
                _itemtypes = value;
                OnPropertyChanged();
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (value == _address) return;
                _address = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name= value;
                OnPropertyChanged();
            }
        }

        public string Image
        {
            get { return _image; }
            set
            {
                if (value == _image) return;
                _image = value;
                OnPropertyChanged();
            }
        }

    }
}
