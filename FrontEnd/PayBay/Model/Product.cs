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
        private int _stt;
        private int _productId;
        private string _productName;
        private string _image;
        private float _unitPrice;
        private int _numberOf;
        private string _unit;
        private int _storeId;
        private DateTime _importDate;
        private float _salePrice;
        private string _sasQuery;
        private string _storeName;

        public string ProductName
        {
            get { return _productName; }
            set
            {
                if (value == _productName) return;
                _productName = value;
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

        public float UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                if (value == _unitPrice) return;
                _unitPrice = value;
                OnPropertyChanged();
            }
        }

        public int NumberOf
        {
            get { return _numberOf; }
            set
            {
                if (value == _numberOf) return;
                _numberOf = value;
                OnPropertyChanged();
            }
        }

        public float SalePrice
        {
            get { return _salePrice; }
            set
            {
                if (value == _salePrice) return;
                _salePrice = value;
                OnPropertyChanged();
            }
        }

        public int ProductId
        {
            get
            {
                return _productId;
            }

            set
            {
                _productId = value;
                OnPropertyChanged();
            }
        }

        public string Unit
        {
            get
            {
                return _unit;
            }

            set
            {
                _unit = value;
                OnPropertyChanged();
            }
        }

        public int StoreId
        {
            get
            {
                return _storeId;
            }

            set
            {
                _storeId = value;
                OnPropertyChanged();
            }
        }

        public DateTime ImportDate
        {
            get
            {
                return _importDate;
            }

            set
            {
                _importDate = value;
                OnPropertyChanged();
            }
        }

        public string SasQuery
        {
            get
            {
                return _sasQuery;
            }

            set
            {
                _sasQuery = value;
                OnPropertyChanged();
            }
        }

        public string StoreName
        {
            get
            {
                return _storeName;
            }

            set
            {
                _storeName = value;
                OnPropertyChanged();
            }
        }

        public int Stt
        {
            get
            {
                return _stt;
            }

            set
            {
                _stt = value;
                OnPropertyChanged();
            }
        }
    }
}
