using System.Collections.ObjectModel;
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
using PayBay.Model.RequestBody;
using Windows.Storage;

namespace PayBay.ViewModel.HomePageGroup
{
	public class AdvertiseViewModel : BaseViewModel
	{	   
		private ObservableCollection<AdvertiseItem> _advertiseItemList;
        //private ObservableCollection<AdvertiseItem> _dummyAdvertiseList;
        //private ObservableCollection<AdvertiseItem> _dummyAdvertiseList2;
        private ObservableCollection<AdvertiseItem> _saleList;
        private ObservableCollection<AdvertiseItem> _saleOfStoreOwner;

        private AdvertiseItem _selectedSale;
		private AdvertiseItem _selectedAd;

        public static bool isUpdate = false;
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

        public ObservableCollection<AdvertiseItem> SaleOfStoreOwner
        {
            get
            {
                return _saleOfStoreOwner;
            }

            set
            {
                _saleOfStoreOwner = value;
                OnPropertyChanged();
            }
        }


        public AdvertiseItem SelectedSale
        {
            get
            {
                return _selectedSale;
            }

            set
            {
                _selectedSale = value;
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
            //InitializeDummyAds();
            //InitializeDataFromDB();
            //LoadMoreSale(TYPEGET.START);
        }

        //private void InitializeDummyAds()
        //{
        //    DummyAdvertiseList = new ObservableCollection<AdvertiseItem>();
        //    DummyAdvertiseList2 = new ObservableCollection<AdvertiseItem>();
        //    for (int i=0; i < 20; i++)
        //    {
        //        AdvertiseItem ad = new AdvertiseItem();
        //        ad.Image = "ms-appx:///Assets/Icon/FruitIcon.png";
        //        DummyAdvertiseList.Add(ad);
        //    }

        //    AdvertiseItem ad2 = new AdvertiseItem();
        //    ad2.Image = "ms-appx:///Assets/Icon/WelcomePaybay.png";
        //    DummyAdvertiseList2.Add(ad2);
        //}

		/// <summary>
		/// Initialize market
		/// </summary>
		private void InitializeProperties()
		{
            AdvertiseItemList = new ObservableCollection<AdvertiseItem>();
            SaleList = new ObservableCollection<AdvertiseItem>();
            SelectedAd = new AdvertiseItem();
            SelectedSale = new AdvertiseItem();
        }

        /// <summary>
        /// Get data from service
        /// </summary>
        /// <returns></returns>
        public async void InitializeDataFromDB()
        {
            MobileServiceInvalidOperationException exception = null;
                        
            try
            {
                IDictionary<string, string> sale = new Dictionary<string, string>
                {
                    { "required", "true"}
                };
                
                await ImportData(sale);    
                //for(int i=0; i < 7; i++)
                //{
                //    AdvertiseItem dummyAd = new AdvertiseItem();
                //    dummyAd.Image = "ms-appx:///Assets/Icon/MarketIcon.jpg";
                //    AdvertiseItemList.Add(dummyAd);
                //}            

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

        public async void GetSaleOfOwner(TYPEGET typeGet, TYPE type = TYPE.OLD)
        {
            try
            {
                JArray result = new JArray();
                int lastId = -1;
                if (typeGet == TYPEGET.MORE)
                {
                    if (SaleOfStoreOwner.Count != 0)
                    {
                        if (type == TYPE.OLD)
                            lastId = SaleOfStoreOwner.Min(x => x.SaleId);
                        else
                            lastId = SaleOfStoreOwner.Max(x => x.SaleId);
                    }
                }
                int ownerId = MediateClass.UserVM.UserInfo.UserId;
                SaleItem saleInfo = new SaleItem(lastId, ownerId, type);
                JToken body = JToken.FromObject(saleInfo);

                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    var response = await App.MobileService.InvokeApiAsync("SaleInfoes", body, HttpMethod.Get, null);
                    result = JArray.Parse(response.ToString());
                    ObservableCollection<AdvertiseItem> more = result.ToObject<ObservableCollection<AdvertiseItem>>();
                    if (typeGet == TYPEGET.START)
                    {
                        SaleOfStoreOwner = more;
                    }
                    else
                    {
                        if (type == TYPE.OLD)
                        {
                            foreach (var item in more)
                            {
                                SaleOfStoreOwner.Add(item);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < more.Count; i++)
                            {
                                SaleOfStoreOwner.Insert(i, more[i]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Load Sale").ShowAsync();
            }
        }

        public async Task<bool> InsertSale(AdvertiseItem sale, StorageFile media)
        {
            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    if (sale.Image == null || sale.Image == "/Assets/LockScreenLogo.scale-200.png")
                    {
                        if (media == null)
                            sale.Image = "/Assets/LockScreenLogo.scale-200.png";
                        else
                            sale.Image = null;
                    }
                    JToken data = JToken.FromObject(sale);
                    JToken result = await App.MobileService.InvokeApiAsync("SaleInfoes", data, HttpMethod.Post, null);

                    JObject response = JObject.Parse(result.ToString());
                    if (media != null)
                    {
                        sale.Image = response["Image"].ToString();
                        sale.SasQuery = response["SasQuery"].ToString();
                        bool check = await Functions.Instance.UploadImageToBlob("sales", sale.Image, sale.SasQuery, media);
                    }
                }
                else
                {
                    await new MessageDialog("You have not internet connection!", "Insert Sale").ShowAsync();
                    return false;
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateSale(AdvertiseItem sale, StorageFile media)
        {
            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    if (sale.Image == null || sale.Image == "/Assets/LockScreenLogo.scale-200.png")
                    {
                        if (media == null)
                            sale.Image = "/Assets/LockScreenLogo.scale-200.png";
                        else
                            sale.Image = null;
                    }
                    JToken data = JToken.FromObject(sale);
                    JToken result = await App.MobileService.InvokeApiAsync("SaleInfoes", data, HttpMethod.Put, null);
                    JObject response = JObject.Parse(result.ToString());

                    SelectedSale = response.ToObject<AdvertiseItem>();
                    if (media != null)
                    {
                        await Functions.Instance.UploadImageToBlob("sales", SelectedSale.Image, SelectedSale.SasQuery, media);
                    }
                }
                else
                {
                    await new MessageDialog("You have not internet connection!", "Insert Product").ShowAsync();
                    return false;
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteSale()
        {
            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    JObject result = new JObject();
                    IDictionary<string, string> param = new Dictionary<string, string>
                    {
                        {"saleId", SelectedSale.SaleId.ToString()}
                    };
                    var response = await App.MobileService.InvokeApiAsync("SaleInfoes", HttpMethod.Delete, param);
                    result = JObject.Parse(response.ToString());
                    if (result["ErrCode"].ToString() == "0")
                    {
                        return false;
                    }
                    await Functions.Instance.DeleteImageInBlob("sales", SelectedSale.Image, SelectedSale.SasQuery);
                    SelectedSale = null;
                }
                else
                {
                    await new MessageDialog("You have not internet connection!", "Insert Product").ShowAsync();
                    return false;
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Delete Sale").ShowAsync();
                return false;
            }
            return true;
        }

    }
}
