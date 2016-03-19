using PayBay.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBay.Model
{
    public class MessageInbox : BaseViewModel
    {
        private int _messageId;
        private int _userId;
        private int _ownerId;
        private string _userName;
        private string _avatar;
        private DateTime _recentDate;

        public int MessageId
        {
            get
            {
                return _messageId;
            }

            set
            {
                _messageId = value;
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

        public DateTime RecentDate
        {
            get
            {
                return _recentDate;
            }
            set
            {
                if (value == null)
                    value = DateTime.Now;
                _recentDate = value;
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

        public MessageInbox()
        {

        }

        public MessageInbox(int userId, int ownerId)
        {
            MessageId = -1;
            UserId = userId;
            OwnerId = ownerId;
        }
    }
}
