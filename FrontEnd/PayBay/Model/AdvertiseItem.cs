using PayBay.ViewModel;
using System;

namespace PayBay.Model
{
	public class AdvertiseItem : BaseViewModel
	{
        private int _id;
        private string _title;
        private string _describes;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _storeId;
        private string _storeName;
        private bool _isRequired;
        private string _image;
        private string _sasQuery;
        private bool _isSelected = false;

		public string Image
		{
			get
			{
				return _image;
			}

			set
			{
				_image = value;
				OnPropertyChanged();
			}
		}

		public bool IsSelected
		{
			get
			{
				return _isSelected;
			}

			set
			{
				_isSelected = value;
				OnPropertyChanged();
			}
		}

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

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Describes
        {
            get
            {
                return _describes;
            }

            set
            {
                _describes = value;
                OnPropertyChanged();
            }
        }

        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }

            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }

            set
            {
                _endDate = value;
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

        public string StoreName
        {
            get
            {
                return _storeName;
            }

            set
            {
                _storeName = value;
                OnPropertyChanged();
            }
        }

        public bool IsRequired
        {
            get
            {
                return _isRequired;
            }

            set
            {
                _isRequired = value;
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
