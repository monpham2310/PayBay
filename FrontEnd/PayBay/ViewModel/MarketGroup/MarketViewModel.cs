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
        private static bool isResponsed = false;

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
            LoadMoreMarket(TYPEGET.START);         
        }

        private void InitializeProperties()
        {
            SelectedMarket = new Market();
            MarketItemList = new ObservableCollection<Market>();
        }     
                        
        public async void LoadMoreMarket(TYPEGET type, TYPE isOld = 0)
        {
            string lastId = "";
            if (type == TYPEGET.MORE)
            {
                if (MarketItemList.Count != 0)
                {
                    if (isOld == TYPE.OLD)
                        lastId = MarketItemList.Min(x => x.MarketId).ToString();
                    else
                        lastId = MarketItemList.Max(x => x.MarketId).ToString();
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

        public async void LoadMoreMarket(string name, TYPEGET type, TYPE isOld = 0)
        {
            string lastId = "";
            if (type == TYPEGET.MORE)
            {
                if (MarketItemList.Count != 0)
                {
                    if (isOld == TYPE.OLD)
                        lastId = MarketItemList.Min(x => x.MarketId).ToString();
                    else
                        lastId = MarketItemList.Max(x => x.MarketId).ToString();
                }
            }
            else
                lastId = "-1";
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"id" , lastId},
                {"name" , name},
                {"type" , isOld.ToString()}
            };
            if(lastId != "")
                await SendData(type, isOld, param);
        }

        private async Task SendData(TYPEGET typeGet, TYPE type, IDictionary<string, string> param)
        {
            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    if (!isResponsed)
                    {
                        isResponsed = true;
                        JToken result = await App.MobileService.InvokeApiAsync("Markets", HttpMethod.Get, param);
                        JArray response = JArray.Parse(result.ToString());
                        if (typeGet == TYPEGET.MORE)
                        {
                            ObservableCollection<Market> data = response.ToObject<ObservableCollection<Market>>();
                            if (type == TYPE.OLD)
                            {
                                foreach (var item in data)
                                {
                                    MarketItemList.Add(item);
                                }
                            }
                            else {
                                for (int i = 0; i < data.Count; i++)
                                {
                                    MarketItemList.Insert(i, data[i]);
                                }
                            }
                        }
                        else
                            MarketItemList = response.ToObject<ObservableCollection<Market>>();
                        isResponsed = false;
                    }
                }
            }
            catch (Exception ex)
            {
                isResponsed = false;
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }
        }

    }
}
