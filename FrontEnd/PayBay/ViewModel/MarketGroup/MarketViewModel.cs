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

namespace PayBay.ViewModel.MarketGroup
{
    public class MarketViewModel : BaseViewModel
    {

        private Market _newMarket;
        private ObservableCollection<Market> _marketItemList;

        #region Property with calling to PropertyChanged
        public Market NewMarket
        {
            get { return _newMarket; }
            set
            {
                if (Equals(value, _newMarket)) return;
                _newMarket = value;
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
            InitializeData();
        }

        /// <summary>
        /// Initialize market
        /// </summary>
        private async void InitializeData()
        {

            MarketItemList = new ObservableCollection<Market>();

            IDictionary<string, string> param = new Dictionary<string, string>
            {
                { "id" , "-1" }
            };

            try {
                JToken result = await App.MobileService.InvokeApiAsync("Markets", HttpMethod.Get, param);
                JArray markets = JArray.Parse(result.ToString());
                MarketItemList = markets.ToObject<ObservableCollection<Market>>();                                
            }
            catch (Exception ex)
            {
                App.MobileService.Dispose();
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }            
        }

        public async Task GetMarketFollowName(string name)
        {
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                { "id" , "-1" },
                { "name" , name }
            };
            try
            {
                JToken result = await App.MobileService.InvokeApiAsync("Markets", HttpMethod.Get, param);
                JArray list = JArray.Parse(result.ToString());

                MarketItemList = list.ToObject<ObservableCollection<Market>>();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }            
        }

        public async void LoadMoreMarket()
        {
            string lastId = MarketItemList[MarketItemList.Count - 1].MarketId.ToString();

            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"id" , lastId}
            };
            try {
                JToken result = await App.MobileService.InvokeApiAsync("Markets", HttpMethod.Get, param);
                JArray markets = JArray.Parse(result.ToString());
                ObservableCollection<Market> moreMarket = markets.ToObject<ObservableCollection<Market>>();

                foreach (var item in moreMarket)
                {
                    MarketItemList.Add(item);
                }                
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }            
        }

        public async void LoadMoreMarket(string name)
        {
            string lastId = MarketItemList[MarketItemList.Count - 1].MarketId.ToString();

            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"id" , lastId},
                { "name" , name }
            };
            try
            {
                JToken result = await App.MobileService.InvokeApiAsync("Markets", HttpMethod.Get, param);
                JArray markets = JArray.Parse(result.ToString());
                ObservableCollection<Market> moreMarket = markets.ToObject<ObservableCollection<Market>>();

                foreach (var item in moreMarket)
                {
                    MarketItemList.Add(item);
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }            
        }

    }
}
