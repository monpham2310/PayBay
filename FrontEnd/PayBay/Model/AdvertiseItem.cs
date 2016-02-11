﻿using PayBay.ViewModel;

namespace PayBay.Model
{
	public class AdvertiseItem: BaseViewModel
	{
        private int _productID;
        private string _productName;
        private byte[] _image;
        private float _unitPrice;
        private int _numberOf;
        private string _unit;
        private int _storeId;
        private string _storeName;
        private int _marketId;
        private string _marketName;
        private float _salePrice;

        public int ProductID
        {
            get
            {
                return _productID;
            }

            set
            {
                _productID = value;
                OnPropertyChanged();
            }
        }

        public string ProductName
        {
            get
            {
                return _productName;
            }

            set
            {
                _productName = value;
                OnPropertyChanged();
            }
        }

        public byte[] Image
        {
            get
            {
                return _image;
            }

            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        public float UnitPrice
        {
            get
            {
                return _unitPrice;
            }

            set
            {
                _unitPrice = value;
                OnPropertyChanged();
            }
        }

        public int NumberOf
        {
            get
            {
                return _numberOf;
            }

            set
            {
                _numberOf = value;
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

        public int MarketId
        {
            get
            {
                return _marketId;
            }

            set
            {
                _marketId = value;
                OnPropertyChanged();
            }
        }

        public string MarketName
        {
            get
            {
                return _marketName;
            }

            set
            {
                _marketName = value;
                OnPropertyChanged();
            }
        }

        public float SalePrice
        {
            get
            {
                return _salePrice;
            }

            set
            {
                _salePrice = value;
                OnPropertyChanged();
            }
        }

    }
}
