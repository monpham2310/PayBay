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
using Windows.Storage;

namespace PayBay.ViewModel.MarketGroup
{
    public class MarketViewModel : BaseViewModel
    {

        private Market _selectedMarket;
        private ObservableCollection<Market> _marketItemList;

        public static bool isUpdate = false;
        private static bool isResponsed = false;
        private static bool isSuggested = false;

        private ObservableCollection<Market> _allMarket;

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

        public ObservableCollection<Market> AllMarket
        {
            get
            {
                return _allMarket;
            }

            set
            {
                _allMarket = value;
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
            //GetAllMarket();             
        }

        private void InitializeProperties()
        {
            SelectedMarket = new Market();
            MarketItemList = new ObservableCollection<Market>();
            AllMarket = new ObservableCollection<Market>();
        }     
                  
        public async void GetAllMarket()
        {
            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    var response = await App.MobileService.InvokeApiAsync("Markets", HttpMethod.Get, null);
                    var result = JArray.Parse(response.ToString());
                    AllMarket = result.ToObject<ObservableCollection<Market>>();
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }
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

        public async Task<bool> InsertMarket(Market market, StorageFile media)
        {
            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    if (market.Image == null || market.Image == "/Assets/LockScreenLogo.scale-200.png")
                    {
                        if (media == null)
                            market.Image = "/Assets/LockScreenLogo.scale-200.png";
                        else
                            market.Image = null;
                    }
                    JToken data = JToken.FromObject(market);
                    JToken result = await App.MobileService.InvokeApiAsync("Markets", data, HttpMethod.Post, null);
                    JObject response = JObject.Parse(result.ToString());

                    if (media != null)
                    {
                        market.Image = response["Image"].ToString();
                        market.SasQuery = response["SasQuery"].ToString();
                        await Functions.Instance.UploadImageToBlob("markets", market.Image, market.SasQuery, media);
                    }

                }
                else
                {
                    await new MessageDialog("You have not internet connection!", "Add Market").ShowAsync();
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

        public async Task<bool> UpdateMarket(Market market, StorageFile media)
        {
            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    if (market.Image == null || market.Image == "/Assets/LockScreenLogo.scale-200.png")
                    {
                        if (media == null)
                            market.Image = "/Assets/LockScreenLogo.scale-200.png";
                        else
                            market.Image = null;
                    }
                    JToken data = JToken.FromObject(market);
                    JToken result = await App.MobileService.InvokeApiAsync("Markets", data, HttpMethod.Put, null);
                    JObject response = JObject.Parse(result.ToString());

                    SelectedMarket = response.ToObject<Market>();
                    if (media != null)
                    {
                        await Functions.Instance.UploadImageToBlob("markets", SelectedMarket.Image, SelectedMarket.SasQuery, media);
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

        public async Task<bool> DeleteMarket()
        {
            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    JObject result = new JObject();
                    IDictionary<string, string> param = new Dictionary<string, string>
                    {
                        {"marketId", SelectedMarket.MarketId.ToString()}
                    };
                    var response = await App.MobileService.InvokeApiAsync("Markets", HttpMethod.Delete, param);
                    result = JObject.Parse(response.ToString());
                    Market market = result.ToObject<Market>();
                    if (market.Image != null && market.SasQuery != null)
                        await Functions.Instance.DeleteImageInBlob("markets", market.Image, market.SasQuery);
                    SelectedMarket = null;
                }
                else
                {
                    await new MessageDialog("You have not internet connection!", "Insert Product").ShowAsync();
                    return false;
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Delete Market").ShowAsync();
                return false;
            }
            return true;
        }

        public async void SuggestedMarketList()
        {
            Coordinate coor = await Functions.TrackLocationOfUser();
            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    if (coor.Latitute != 0 && coor.Longitute != 0)
                    {
                        if (!isSuggested)
                        {
                            isSuggested = true;
                            IDictionary<string, string> param = new Dictionary<string, string>
                            {
                                {"latitute" , coor.Latitute.ToString()},
                                {"longitute" , coor.Longitute.ToString()},
                            };

                            var result = await App.MobileService.InvokeApiAsync("Markets", HttpMethod.Get, param);
                            JArray responed = JArray.Parse(result.ToString());
                            MarketItemList = responed.ToObject<ObservableCollection<Market>>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Suggest Market").ShowAsync();
            }
            finally
            {
                isSuggested = false;
            }
        }

    }
}
