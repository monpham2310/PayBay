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
        private int _userID;
        private int _ownerID;
        private string _userName;
        private string _avatar;
        private DateTime _inboxDate;
        private string _content;

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

        public int UserID
        {
            get
            {
                return _userID;
            }

            set
            {
                _userID = value;
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

        public DateTime InboxDate
        {
            get
            {
                return _inboxDate;
            }
            set
            {
                if (value == null)
                    value = DateTime.Now;
                _inboxDate = value;
                OnPropertyChanged();
            }
        }

        public int OwnerID
        {
            get
            {
                return _ownerID;
            }

            set
            {
                _ownerID = value;
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

        public MessageInbox()
        {

        }

        public MessageInbox(int userId, int ownerId)
        {
            MessageId = -1;
            UserID = userId;
            OwnerID = ownerId;
        }
    }
}
