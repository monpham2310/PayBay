﻿using System.Collections.ObjectModel;
using System.Linq;
using PayBay.Model;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Net.Http;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using System.IO;

namespace PayBay.ViewModel.HomePageGroup
{
	public class AdvertiseViewModel: BaseViewModel
	{
		private ObservableCollection<AdvertiseItem> _advertiseItemList;
		private AdvertiseItem _selectedAd;

		#region Property with calling to PropertyChanged
		public ObservableCollection<AdvertiseItem> AdvertiseItemList
		{
			get
			{
				return _advertiseItemList;
			}

			set
			{
				if (Equals(value, _advertiseItemList)) return;
				_advertiseItemList = value;
				OnPropertyChanged();
			}
		}

		public AdvertiseItem SelectedAd
		{
			get
			{
				return _selectedAd;
			}
			
			set
			{
				foreach (var ad in this.AdvertiseItemList.Where(x => x.IsSelected))
				{
					ad.IsSelected = false;
				}

				if (value != null)
				{
					value.IsSelected = true;
				}

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
		/// Initialize market
		/// </summary>
		private void InitializeData()
		{
			_advertiseItemList = new ObservableCollection<AdvertiseItem>();

			for (var i = 0; i < 5; i++)
			{
				AdvertiseItem ad = new AdvertiseItem();
				ad.Image = "/Assets/lol.jpg";
				ad.IsSelected = false;
				_advertiseItemList.Add(ad);
			}

			_selectedAd = _advertiseItemList[0];
			_selectedAd.IsSelected = true;
		}

		//private ObservableCollection<AdvertiseItem> _newMerchandiseItemList;
		//private ObservableCollection<AdvertiseItem> _saleMerchandiseItemList;
		//private ObservableCollection<AdvertiseItem> _hotMerchandiseItemList;

		//      private enum TypeMechandises
		//      {
		//          NEW = 1,
		//          SALE = 2,
		//          BESTSALE = 3
		//      }

		//#region Property with calling to PropertyChanged
		//public ObservableCollection<AdvertiseItem> NewMerchandiseItemList
		//{
		//	get { return _newMerchandiseItemList; }
		//	set
		//	{
		//		if (Equals(value, _newMerchandiseItemList)) return;
		//		_newMerchandiseItemList = value;
		//		OnPropertyChanged();
		//	}
		//}

		//public ObservableCollection<AdvertiseItem> SaleMerchandiseItemList
		//{
		//	get { return _saleMerchandiseItemList; }
		//	set
		//	{
		//		if (Equals(value, _saleMerchandiseItemList)) return;
		//		_saleMerchandiseItemList = value;
		//		OnPropertyChanged();
		//	}
		//}

		//public ObservableCollection<AdvertiseItem> HotMerchandiseItemList
		//{
		//	get { return _hotMerchandiseItemList; }
		//	set
		//	{
		//		if (Equals(value, _hotMerchandiseItemList)) return;
		//		_hotMerchandiseItemList = value;
		//		OnPropertyChanged();
		//	}
		//}
		//#endregion

		///// <summary>
		///// Constructor
		///// </summary>
		//public AdvertiseViewModel()
		//{
		//	InitializeData();
		//}

		///// <summary>
		///// Get data from service
		///// </summary>
		///// <returns></returns>
		//private async void InitializeData()
		//{
		//	MobileServiceInvalidOperationException exception = null;

		//	NewMerchandiseItemList = new ObservableCollection<AdvertiseItem>();
		//	SaleMerchandiseItemList = new ObservableCollection<AdvertiseItem>();
		//	HotMerchandiseItemList = new ObservableCollection<AdvertiseItem>();

		//	try
		//	{
		//		IDictionary<string, string> newMechandises = new Dictionary<string, string>
		//			  {
		//				  { "typeProduct", "1"}
		//			  };

		//		IDictionary<string, string> saleMechandise = new Dictionary<string, string>
		//			  {
		//				  { "typeProduct", "2"}
		//			  };

		//		IDictionary<string, string> hotMechandise = new Dictionary<string, string>
		//			  {
		//				  { "typeProduct", "3"}
		//			  };

		//		await ImportData(newMechandises, TypeMechandises.NEW);
		//		await ImportData(saleMechandise, TypeMechandises.SALE);
		//		await ImportData(hotMechandise, TypeMechandises.BESTSALE);

		//	}
		//	catch (MobileServiceInvalidOperationException e)
		//	{
		//		exception = e;
		//	}
		//	if (exception != null)
		//	{
		//		await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
		//	}
		//}

		///// <summary>
		///// Get data from database with Json
		///// </summary>
		///// <param name="argument">parameter will get</param>
		///// <param name="type">type product will get : New,Sale,Best sale</param>
		///// <returns></returns>
		//private async Task ImportData(IDictionary<string, string> argument, TypeMechandises type)
		//{
		//	JToken _product = null;
		//	_product = await App.MobileService.InvokeApiAsync("Products", HttpMethod.Get, argument);

		//	JArray results = JArray.Parse(_product.ToString());
		//	foreach (var result in results)
		//	{
		//		AdvertiseItem temp = new AdvertiseItem();
		//		temp.ProductID = (int)result["ProductId"];
		//		temp.ProductName = (string)result["ProductName"];
		//		temp.Image = (string)result["Image"];
		//		temp.UnitPrice = (float)result["UnitPrice"];
		//		temp.Unit = (string)result["Unit"];
		//		temp.StoreId = (int)result["StoreID"];
		//		temp.StoreName = (string)result["StoreName"];
		//		temp.MarketId = (int)result["MarketID"];
		//		temp.MarketName = (string)result["MarketName"];
		//		temp.SalePrice = (float)result["SalePrice"];
		//		switch (type)
		//		{
		//			case TypeMechandises.NEW:
		//			_newMerchandiseItemList.Add(temp);
		//			break;
		//			case TypeMechandises.SALE:
		//			_saleMerchandiseItemList.Add(temp);
		//			break;
		//			default:
		//			_hotMerchandiseItemList.Add(temp);
		//			break;
		//		}

		//	}
		//}

	}
}
