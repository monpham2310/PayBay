using PayBay.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBay.Model
{
    public class UserInfo : BaseViewModel
    {
        private int _userId;
        private DateTime _birthday;
        private string _email;
        private string _phone;
        private bool _gender;
        private string _address;
        private string _avatar;
        private int _typeId;
        private string _username;
        private byte[] _pass;
        private string _sasQuery;          

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
                
        public DateTime Birthday
        {
            get
            {
                return _birthday;
            }

            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
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

        public bool Gender
        {
            get
            {
                return _gender;
            }

            set
            {
                _gender = value;
                OnPropertyChanged();
            }
        }

        public string Address
        {
            get
            {
                return _address;
            }

            set
            {
                _address = value;
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

        public int TypeId
        {
            get
            {
                return _typeId;
            }

            set
            {
                _typeId = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public byte[] Pass
        {
            get
            {
                return _pass;
            }

            set
            {
                _pass = value;
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
    }
}
