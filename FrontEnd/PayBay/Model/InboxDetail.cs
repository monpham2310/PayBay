using PayBay.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBay.Model
{
    public class InboxDetail : BaseViewModel
    {
        private int _userId;
        private string _userName;
        private string _avatar;
        private string _content;
        private DateTime _inboxDate;

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

        public string Content
        {
            get
            {
                return _content;
            }

            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        public DateTime InboxDate
        {
            get
            {
                return _inboxDate;
            }

            set
            {
                _inboxDate = value;
                OnPropertyChanged();
            }
        }
    }
}
