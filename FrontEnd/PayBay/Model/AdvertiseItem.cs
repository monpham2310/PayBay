using PayBay.ViewModel;

namespace PayBay.Model
{
	public class AdvertiseItem: BaseViewModel
	{
		private string _image;
		private string _merchandise;
		private string _market;

		public string Image
		{
			get { return _image; }
			set
			{
				if (value == _image) return;
				_image = value;
				OnPropertyChanged();
			}
		}

		public string Merchandise
		{
			get { return _merchandise; }
			set
			{
				if (value == _merchandise) return;
				_merchandise = value;
				OnPropertyChanged();
			}
		}

		public string Market
		{
			get { return _market; }
			set
			{
				if (value == _market) return;
				_market = value;
				OnPropertyChanged();
			}
		}

	}
}
