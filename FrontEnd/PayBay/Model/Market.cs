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
        private int _marketId;
        private string _marketName;
        private string _image;
        private string _address;
        private string _sasQuery;
        private string _phone;
        private float _longitute;
        private float _latitute;
        private TimeSpan _openTime;
        private TimeSpan _closeTime;
                
        public string SasQuery
        {
            get { return _sasQuery; }
            set
            {
                if (value == _sasQuery) return;
                _sasQuery = value;
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

        public string MarketName
        {
            get { return _marketName; }
            set
            {
                if (value == _marketName) return;
                _marketName= value;
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

        public string Phone
        {
            get { return _phone; }
            set
            {
                if (value == _phone) return;
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

        public float Longitute
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

        public float Latitute
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
