using PayBay.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBay.Model
{
    public class GuestMail : BaseViewModel
    {
        private string email;
        private string pass;
        private string title;
        private string content;

        public GuestMail(string email, string pass, string title, string content)
        {
            this.email = email;
            this.pass = pass;
            this.title = title;
            this.content = content;
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public string Pass
        {
            get
            {
                return pass;
            }

            set
            {
                pass = value;
                OnPropertyChanged();
            }
        }
                
        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public string Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
                OnPropertyChanged();
            }
        }
    }             
}
