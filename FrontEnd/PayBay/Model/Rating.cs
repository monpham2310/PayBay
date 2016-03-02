using PayBay.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBay.Model
{
    public class Rating : BaseViewModel
    {
        private int _id;
        private int _userId;
        private int _storeId;
        private double _rateOfUser;

        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
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

        public double RateOfUser
        {
            get
            {
                return _rateOfUser;
            }

            set
            {
                _rateOfUser = value;
                OnPropertyChanged();
            }
        }

        public Rating() { }

        public Rating(int user, int store, double rated)
        {
            _userId = user;
            _storeId = store;
            _rateOfUser = rated;
        }

    }
}
