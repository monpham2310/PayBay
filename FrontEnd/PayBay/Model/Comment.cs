using PayBay.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBay.Model
{
    public class Comment : BaseViewModel
    {
        private int _id;
        private DateTime _commentDate;
        private int _storeId;
        private int _userId;
        private string _username;
        private string _avatar;
        private string _content;
        private double _rated;

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

        public DateTime CommentDate
        {
            get
            {
                return _commentDate;
            }

            set
            {
                _commentDate = value;
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

        public double Rated
        {
            get
            {
                return _rated;
            }

            set
            {
                _rated = value;
                OnPropertyChanged();
            }
        }
    }
}
