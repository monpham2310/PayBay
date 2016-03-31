using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using PayBay.ViewModel;

namespace PayBay.Model
{
    public class Kios : BaseViewModel
    {
        private int _storeId;
        private string _storeName;        
        private string _kiotNo;
        private string _image;
        private string _phone;
        private int _marketId;
        private int _ownerId;
        private string _userName;
        private string _avatar;  
        private string _sasQuery;
        private float _rate;
        private double _acceptDiscount;
        private TimeSpan _openTime;
        private TimeSpan _closeTime;

        public string Image
        {
            get { return _image; }
            set
            {
                //if (value == null)
                //{
                //    value = "/Assets/LockScreenLogo.scale-200.png";
                //}
                _image = value;
                OnPropertyChanged();
            }
        }

        public string StoreName
        {
            get { return _storeName; }
            set
            {
                if (value == _storeName) return;
                _storeName = value;
                OnPropertyChanged();
            }
        }

        public string KiotNo
        {
            get { return _kiotNo; }
            set
            {
                if (value == _kiotNo) return;
                _kiotNo = value;
                OnPropertyChanged();
            }
        }

        public float Rate
        {
            get { return _rate; }
            set
            {
                if (value == _rate) return;
                _rate = value;
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

        public string Phone
        {
            get
            {
                return _phone;
            }

            set
            {
                _phone = value;
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

        public int OwnerId
        {
            get
            {
                return _ownerId;
            }

            set
            {
                _ownerId = value;
                OnPropertyChanged();
            }
        }

        public string Username
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

        public string Avatar
        {
            get
            {
                return _avatar;
            }

            set
            {
                _avatar = value;
                OnPropertyChanged();
            }
        }

        public double AcceptDiscount
        {
            get
            {
                return _acceptDiscount;
            }

            set
            {
                _acceptDiscount = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan OpenTime
        {
            get
            {
                return _openTime;
            }

            set
            {
                _openTime = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan CloseTime
        {
            get
            {
                return _closeTime;
            }

            set
            {
                _closeTime = value;
                OnPropertyChanged();
            }
        }
    }
}
