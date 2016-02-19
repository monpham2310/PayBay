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
        private string mail;
        private byte[] password;

        public string Email
        {
            get
            {
                return mail;
            }

            set
            {
                mail = value;
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

        public Account(string email, byte[] pwd)
        {
            Email = email;
            Password = pwd;
        }
    }
}
