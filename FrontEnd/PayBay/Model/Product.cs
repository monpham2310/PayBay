using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayBay.ViewModel;

namespace PayBay.Model
{
    public class Product : BaseViewModel
    {
        private string _name;
        private string _image;
        private string _price;
        private string _storename;
        private string _saleprice;

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

        public string Price
        {
            get { return _price; }
            set
            {
                if (value == _price) return;
                _price = value;
                OnPropertyChanged();
            }
        }

        public string StoreName
        {
            get { return _storename; }
            set
            {
                if (value == _storename) return;
                _storename = value;
                OnPropertyChanged();
            }
        }

        public string SalePrice
        {
            get { return _saleprice; }
            set
            {
                if (value == _saleprice) return;
                _saleprice = value;
                OnPropertyChanged();
            }
        }


    }
}
