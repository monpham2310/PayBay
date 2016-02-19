using PayBay.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBay.Model
{
    public class Account : BaseViewModel
    {
        private string username;
        private byte[] password;

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

        public byte[] Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public Account(string user, byte[] pwd)
        {
            Username = user;
            Password = pwd;
        }
    }
}
