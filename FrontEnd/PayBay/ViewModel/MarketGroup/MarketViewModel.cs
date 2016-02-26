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
using PayBay.Utilities.Common;

namespace PayBay.ViewModel.MarketGroup
{
    public class MarketViewModel : BaseViewModel
    {

        private Market _selectedMarket;
        private ObservableCollection<Market> _marketItemList;

        #region Property with calling to PropertyChanged
        public Market SelectedMarket
        {
            get { return _selectedMarket; }
            set
            {
                if (Equals(value, _selectedMarket)) return;
                _selectedMarket = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Market> MarketItemList
        {
            get
            {
                return _marketItemList;
            }

            set
            {
                if (Equals(value, _marketItemList)) return;
                _marketItemList = value;
                OnPropertyChanged();
            }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public MarketViewModel()
        {
            MediateClass.MarketVM = this;
            InitializeProperties();
            InitializeData();
        }

        private void InitializeProperties()
        {
            SelectedMarket = new Market();
            MarketItemList = new ObservableCollection<Market>();
        }
                
        /// <summary>
        /// Initialize market
        /// </summary>
        private async void InitializeData()
        {
            await LoadMoreMarket(TYPEGET.START);
        }
                
        public async Task LoadMoreMarket(TYPEGET type)
        {
            string lastId = "";
            if (type == TYPEGET.MORE)
                lastId = MarketItemList[MarketItemList.Count - 1].MarketId.ToString();
            else
                lastId = "-1";
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"id" , lastId}
            };
            try {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    JToken result = await App.MobileService.InvokeApiAsync("Markets", HttpMethod.Get, param);
                    JArray markets = JArray.Parse(result.ToString());
                    if (type == TYPEGET.MORE)
                    {
                        ObservableCollection<Market> moreMarket = markets.ToObject<ObservableCollection<Market>>();

                        foreach (var item in moreMarket)
                        {
                            MarketItemList.Add(item);
                        }
                    }
                    else
                        MarketItemList = markets.ToObject<ObservableCollection<Market>>();
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }            
        }

        public async Task LoadMoreMarket(string name, TYPEGET type)
        {
            string lastId = "";
            if (type == TYPEGET.MORE)
                lastId = MarketItemList[MarketItemList.Count - 1].MarketId.ToString();
            else
                lastId = "-1";
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"id" , lastId},
                { "name" , name }
            };
            try
            {
                JToken result = await App.MobileService.InvokeApiAsync("Markets", HttpMethod.Get, param);
                JArray markets = JArray.Parse(result.ToString());
                if (type == TYPEGET.MORE)
                {
                    ObservableCollection<Market> moreMarket = markets.ToObject<ObservableCollection<Market>>();

                    foreach (var item in moreMarket)
                    {
                        MarketItemList.Add(item);
                    }
                }
                else
                    MarketItemList = markets.ToObject<ObservableCollection<Market>>();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }            
        }

    }
}
