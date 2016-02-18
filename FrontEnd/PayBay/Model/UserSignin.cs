using PayBay.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBay.Model
{
    public class UserSignin : BaseViewModel
    {
        private string avatar;
        private string username;

        public UserSignin(string ava, string user)
        {
            Avatar = ava;
            Username = user;
        }

        public string Avatar
        {
            get
            {
                return avatar;
            }

            set
            {
                avatar = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
                OnPropertyChanged();
            }
        }
    }
}
