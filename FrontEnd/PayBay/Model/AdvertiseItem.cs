using PayBay.ViewModel;
using System;

namespace PayBay.Model
{
	public class AdvertiseItem : BaseViewModel
	{
		private string _image;
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

		//private int _productID;
		//private string _productName;
		//private string _image;
		//private float _unitPrice;
		//private int _numberOf;
		//private string _unit;
		//private int _storeId;
		//private DateTime _importDate;
		//private float _salePrice;
		//private string _sasQuery;
		//private string _storeName;
		//private int _marketId;
		//private string _marketName;

		//public int ProductID
		//{
		//	get
		//	{
		//		return _productID;
		//	}

		//	set
		//	{
		//		_productID = value;
		//		OnPropertyChanged();
		//	}
		//}

		//public string ProductName
		//{
		//	get
		//	{
		//		return _productName;
		//	}

		//	set
		//	{
		//		_productName = value;
		//		OnPropertyChanged();
		//	}
		//}

		//public string Image
		//{
		//	get
		//	{
		//		return _image;
		//	}

		//	set
		//	{
		//		_image = value;
		//		OnPropertyChanged();
		//	}
		//}

		//public float UnitPrice
		//{
		//	get
		//	{
		//		return _unitPrice;
		//	}

		//	set
		//	{
		//		_unitPrice = value;
		//		OnPropertyChanged();
		//	}
		//}

		//public int NumberOf
		//{
		//	get
		//	{
		//		return _numberOf;
		//	}

		//	set
		//	{
		//		_numberOf = value;
		//		OnPropertyChanged();
		//	}
		//}

		//public string Unit
		//{
		//	get
		//	{
		//		return _unit;
		//	}

		//	set
		//	{
		//		_unit = value;
		//		OnPropertyChanged();
		//	}
		//}

		//public int StoreId
		//{
		//	get
		//	{
		//		return _storeId;
		//	}

		//	set
		//	{
		//		_storeId = value;
		//		OnPropertyChanged();
		//	}
		//}

		//public float SalePrice
		//{
		//	get
		//	{
		//		return _salePrice;
		//	}

		//	set
		//	{
		//		_salePrice = value;
		//		OnPropertyChanged();
		//	}
		//}

		//public string SasQuery
		//{
		//	get
		//	{
		//		return _sasQuery;
		//	}

		//	set
		//	{
		//		_sasQuery = value;
		//	}
		//}

		//public DateTime ImportDate
		//{
		//	get
		//	{
		//		return _importDate;
		//	}

		//	set
		//	{
		//		_importDate = value;
		//	}
		//}

		//public string StoreName
		//{
		//	get
		//	{
		//		return _storeName;
		//	}

		//	set
		//	{
		//		_storeName = value;
		//	}
		//}

		//public int MarketId
		//{
		//	get
		//	{
		//		return _marketId;
		//	}

		//	set
		//	{
		//		_marketId = value;
		//	}
		//}

		//public string MarketName
		//{
		//	get
		//	{
		//		return _marketName;
		//	}

		//	set
		//	{
		//		_marketName = value;
		//	}
		//}
	}
}
