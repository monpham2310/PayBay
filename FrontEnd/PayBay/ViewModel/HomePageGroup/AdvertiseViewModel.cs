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
using PayBay.Utilities.Helpers;
using PayBay.Utilities.Common;

namespace PayBay.ViewModel.HomePageGroup
{
	public class AdvertiseViewModel : BaseViewModel
	{	   
		private ObservableCollection<AdvertiseItem> _advertiseItemList;
        private ObservableCollection<AdvertiseItem> _saleList;
		private AdvertiseItem _selectedAd;
        private static bool isResponsed = false;

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

        public ObservableCollection<AdvertiseItem> SaleList
        {
            get
            {
                return _saleList;
            }

            set
            {
                _saleList = value;
                OnPropertyChanged();
            }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public AdvertiseViewModel()
		{
            MediateClass.AdvertiseVM = this;
			InitializeProperties();
            InitializeDataFromDB();
            LoadMoreSale(TYPEGET.START);
        }

		/// <summary>
		/// Initialize market
		/// </summary>
		private void InitializeProperties()
		{
            AdvertiseItemList = new ObservableCollection<AdvertiseItem>();
            SaleList = new ObservableCollection<AdvertiseItem>();
        }

        /// <summary>
        /// Get data from service
        /// </summary>
        /// <returns></returns>
        private async void InitializeDataFromDB()
        {
            MobileServiceInvalidOperationException exception = null;
            
            try
            {
                IDictionary<string, string> sale = new Dictionary<string, string>
                {
                    { "required", "true"}
                };
                
                //await ImportData(sale);    
                for(int i=0; i < 7; i++)
                {
                    AdvertiseItem dummyAd = new AdvertiseItem();
                    dummyAd.Image = "ms-appx:///Assets/Icon/MarketIcon.jpg";
                    AdvertiseItemList.Add(dummyAd);
                }            

            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }
            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
        }

        /// <summary>
        /// Get data from database with Json
        /// </summary>
        /// <param name="argument">parameter will get</param>
        /// <param name="type">type product will get : New,Sale,Best sale</param>
        /// <returns></returns>
        private async Task ImportData(IDictionary<string, string> argument)
        {            
            JToken _product = null;
            try
            {
                if (NetworkHelper.Instance.HasInternetConnection)
                {                    
                    _product = await App.MobileService.InvokeApiAsync("SaleInfoes", HttpMethod.Get, argument);
                    JArray results = JArray.Parse(_product.ToString());
                    AdvertiseItemList = results.ToObject<ObservableCollection<AdvertiseItem>>();
                    _selectedAd = AdvertiseItemList[0];
                    _selectedAd.IsSelected = true;
                }                
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message, "Error loading items").ShowAsync();
            }
        }

        public async void LoadMoreSale(TYPEGET type, TYPE isOld=0)
        {
            string lastId = "";
            if (type == TYPEGET.MORE)
            {
                if (SaleList.Count != 0)
                {
                    if (isOld == TYPE.OLD)
                        lastId = SaleList.Min(x => x.SaleId).ToString();
                    else
                        lastId = SaleList.Max(x => x.SaleId).ToString();
                }
            }
            else
                lastId = "-1";
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"id" , lastId},
                {"type" , isOld.ToString()}
            };
            if(lastId != "")
                await SendData(type, isOld, param);
        }

        public async void LoadMoreSale(string title, TYPEGET type, TYPE isOld=0)
        {
            string lastId = "";
            if (type == TYPEGET.MORE)
            {
                if (SaleList.Count != 0)
                {
                    if (isOld == TYPE.OLD)
                        lastId = SaleList.Min(x => x.SaleId).ToString();
                    else
                        lastId = SaleList.Max(x => x.SaleId).ToString();
                }
            }
            else
                lastId = "-1";
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"id" , lastId},
                {"title" , title},
                {"type" , isOld.ToString()}
            };
            if(lastId != "")
                await SendData(type, isOld, param);
        }

        private async Task SendData(TYPEGET typeGet, TYPE type, IDictionary<string,string> param)
        {                     
            try
            {
                if (NetworkHelper.Instance.HasInternetConnection)
                {
                    if (!isResponsed)
                    {
                        isResponsed = true;
                        JToken result = await App.MobileService.InvokeApiAsync("SaleInfoes", HttpMethod.Get, param);
                        JArray sales = JArray.Parse(result.ToString());
                        if (typeGet == TYPEGET.MORE)
                        {
                            ObservableCollection<AdvertiseItem> moreSale = sales.ToObject<ObservableCollection<AdvertiseItem>>();
                            if (type == TYPE.OLD)
                            {
                                foreach (var item in moreSale)
                                {
                                    SaleList.Add(item);
                                }
                            }
                            else {
                                for (int i = 0; i < moreSale.Count; i++)
                                {
                                    SaleList.Insert(i, moreSale[i]);
                                }
                            }
                        }
                        else
                            SaleList = sales.ToObject<ObservableCollection<AdvertiseItem>>();                        
                    }
                }
            }
            catch (Exception ex)
            {                
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }
            finally
            {
                isResponsed = false;
            }
        }
                
    }
}
