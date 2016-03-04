using PayBay.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBay.Model
{
    public class Bill : BaseViewModel
    {
        private int _billId;
        private DateTime _createdDate;
        private int _storeId;
        private string _storeName;
        private double _totalPrice;
        private double _reducedPrice = 0;
        private int _userId;
        private string _userName;
        private string _note;
        private string _shipMethod;
        private string _tradeTerm;
        private string _agreeredShippingDate;
        private DateTime _shipDate;

        private double _oldPrice = 0;

        #region
        public int BillId
        {
            get
            {
                return _billId;
            }

            set
            {
                _billId = value;
                OnPropertyChanged();
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }

            set
            {
                _createdDate = value;
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

        public double TotalPrice
        {
            get
            {
                return _totalPrice;
            }

            set
            {
                _totalPrice = value;                
                OnPropertyChanged();
            }
        }

        public double ReducedPrice
        {
            get
            {
                return _reducedPrice;
            }

            set
            {
                _reducedPrice = value;
                TotalPrice = (TotalPrice + (_oldPrice - TotalPrice)) - _reducedPrice;
                OnPropertyChanged();
            }
        }

        public int UserId
        {
            get
            {
                return _userId;
            }

            set
            {
                _userId = value;
                OnPropertyChanged();
            }
        }

        public string ShipMethod
        {
            get
            {
                return _shipMethod;
            }

            set
            {
                _shipMethod = value;
                OnPropertyChanged();
            }
        }

        public string Note
        {
            get
            {
                return _note;
            }

            set
            {
                _note = value;
                OnPropertyChanged();
            }
        }

        public string TradeTerm
        {
            get
            {
                return _tradeTerm;
            }

            set
            {
                _tradeTerm = value;
                OnPropertyChanged();
            }
        }

        public string AgreeredShippingDate
        {
            get
            {
                return _agreeredShippingDate;
            }

            set
            {
                _agreeredShippingDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime ShipDate
        {
            get
            {
                return _shipDate;
            }

            set
            {
                _shipDate = value;
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

        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public Bill() { }
        
        public Bill(DateTime createDate, int storeId, string storeName, double totalPrice,
                        double reducePrice, int userId, string userName)
        {
            _createdDate = createDate;
            _storeId = storeId;
            _totalPrice = totalPrice;
            _reducedPrice = reducePrice;
            _userId = userId;
            _storeName = storeName;
            _userName = userName;

            _oldPrice = _totalPrice;
        }
    }
}
