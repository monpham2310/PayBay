using System.Collections.ObjectModel;
using PayBay.Model;

namespace PayBay.ViewModel.HomePageGroup
{
	public class AdvertiseViewModel: BaseViewModel
	{
		private ObservableCollection<AdvertiseItem> _newMerchandiseItemList;
		private ObservableCollection<AdvertiseItem> _saleMerchandiseItemList;
		private ObservableCollection<AdvertiseItem> _hotMerchandiseItemList;

		#region Property with calling to PropertyChanged
		public ObservableCollection<AdvertiseItem> NewMerchandiseItemList
		{
			get { return _newMerchandiseItemList; }
			set
			{
				if (Equals(value, _newMerchandiseItemList)) return;
				_newMerchandiseItemList = value;
				OnPropertyChanged();
			}
		}

		public ObservableCollection<AdvertiseItem> SaleMerchandiseItemList
		{
			get { return _saleMerchandiseItemList; }
			set
			{
				if (Equals(value, _saleMerchandiseItemList)) return;
				_saleMerchandiseItemList = value;
				OnPropertyChanged();
			}
		}

		public ObservableCollection<AdvertiseItem> HotMerchandiseItemList
		{
			get { return _hotMerchandiseItemList; }
			set
			{
				if (Equals(value, _hotMerchandiseItemList)) return;
				_hotMerchandiseItemList = value;
				OnPropertyChanged();
			}
		}
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public AdvertiseViewModel()
		{
			InitializeData();
		}

		/// <summary>
		/// Initialize function list in splitview control
		/// </summary>
		private void InitializeData()
		{
			NewMerchandiseItemList = new ObservableCollection<AdvertiseItem>();
			SaleMerchandiseItemList = new ObservableCollection<AdvertiseItem>();
			HotMerchandiseItemList = new ObservableCollection<AdvertiseItem>();

			for (var i = 0; i < 10; i++)
			{
				var ad = new AdvertiseItem
				{
					Image = "/Assets/lol.jpg",
					Merchandise = "Mèo nguyên chất - Giảm giá siêu khuyến mãi cực hot",
					Market = "Chơ Phạm Văn Hai - K69"
				};

				_newMerchandiseItemList.Add(ad);
				_saleMerchandiseItemList.Add(ad);
				_hotMerchandiseItemList.Add(ad);
			}
		}
	}
}
