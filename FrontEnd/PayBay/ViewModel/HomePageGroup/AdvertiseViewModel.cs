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

namespace PayBay.ViewModel.HomePageGroup
{
	public class AdvertiseViewModel : BaseViewModel
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
			//InitializeData();
            InitializeDataFromDB();
        }

		/// <summary>
		/// Initialize market
		/// </summary>
		private void InitializeData()
		{
			_advertiseItemList = new ObservableCollection<AdvertiseItem>();

			for (var i = 0; i < 6; i++)
			{
				AdvertiseItem ad = new AdvertiseItem();
				ad.Image = "/Assets/lol.jpg";
				ad.IsSelected = false;
				_advertiseItemList.Add(ad);
			}

			_selectedAd = _advertiseItemList[0];
			_selectedAd.IsSelected = true;
		}

        /// <summary>
        /// Get data from service
        /// </summary>
        /// <returns></returns>
        private async void InitializeDataFromDB()
        {
            MobileServiceInvalidOperationException exception = null;
            AdvertiseItemList = new ObservableCollection<AdvertiseItem>();

            try
            {
                IDictionary<string, string> sale = new Dictionary<string, string>
                {
                    { "required", "true"}
                };
                
                await ImportData(sale);                

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

            _product = await App.MobileService.InvokeApiAsync("SaleInfoes", HttpMethod.Get, argument);

            JArray results = JArray.Parse(_product.ToString());

            AdvertiseItemList = results.ToObject<ObservableCollection<AdvertiseItem>>();
            _selectedAd = AdvertiseItemList[0];
            _selectedAd.IsSelected = true;
        }

    }
}
