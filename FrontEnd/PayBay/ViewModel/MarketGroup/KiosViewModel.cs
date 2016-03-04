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
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using PayBay.Utilities.Common;

namespace PayBay.ViewModel.MarketGroup
{
    public class KiosViewModel : BaseViewModel
    {
        private Kios _selectedStore;
        private ObservableCollection<Kios> _kiosList;
        
        private static bool isResponsed = false;
        
        #region Property with calling to PropertyChanged
        public ObservableCollection<Kios> KiosList
        {
            get { return _kiosList; }
            set
            {
                if (Equals(value, _kiosList)) return;
                _kiosList = value;
                OnPropertyChanged();
            }
        }
                                
        public Kios SelectedStore
        {
            get
            {
                return _selectedStore;
            }

            set
            {
                _selectedStore = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public KiosViewModel()
        {
            MediateClass.KiotVM = this;
            InitializeProperties();
            InitializeData();
                        
        }

        //Initialize Fake Data
        //private void InitFakeData()
        //{
        //    _fakeKios = new Kios();
        //    ObservableCollection<Product> fakeProductList = new ObservableCollection<Product>();
        //    for (int i=0; i < 5; i++)
        //    {
        //        Product fakeProduct = new Product();
        //        fakeProduct.ProductName = "Ba Con Soi";
        //        fakeProductList.Add(fakeProduct);
        //    }
        //    _fakeKios.ProductList = fakeProductList;
        //}

        private void InitializeProperties()
        {            
            SelectedStore = new Kios();
            KiosList = new ObservableCollection<Kios>();
        }

        public void InitializeData()
        {
            int marketID = MediateClass.MarketVM.SelectedMarket.MarketId;
            LoadMoreStore(marketID, TYPEGET.START);
        }

        public async void LoadMoreStore(int marketId, TYPEGET typeGet,TYPE type=0)
        {
            string lastId = "";
            if (typeGet == TYPEGET.MORE)
            {
                if (KiosList.Count != 0)
                {
                    if (type == TYPE.OLD)
                        lastId = KiosList.Min(x => x.StoreId).ToString();
                    else
                        lastId = KiosList.Max(x => x.StoreId).ToString();
                }
            }
            else
                lastId = "-1";
                  
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"marketId", marketId.ToString()},
                {"storeId" , lastId},
                {"type" , type.ToString()}
            };
            if(lastId != "")
                await SendData(typeGet, type, param);
        }
               
        public async void LoadMoreStore(int marketId, string storeName, TYPEGET typeGet, TYPE type=0)
        {
            string lastId = "";
            if (typeGet == TYPEGET.MORE)
            {
                if (KiosList.Count != 0)
                {
                    if (type == TYPE.OLD)
                        lastId = KiosList.Min(x => x.StoreId).ToString();
                    else
                        lastId = KiosList.Max(x => x.StoreId).ToString();
                }
            }
            else
                lastId = "-1";
            
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"name" , storeName},
                {"markId" , marketId.ToString()},
                {"storeId" , lastId},
                {"type" , type.ToString()}
            };
            if(lastId != "")
                await SendData(typeGet, type, param);
        }

        private async Task SendData(TYPEGET typeGet, TYPE type, IDictionary<string,string> param)
        {
            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    if (!isResponsed)
                    {
                        isResponsed = true;
                        JToken result = await App.MobileService.InvokeApiAsync("Stores", HttpMethod.Get, param);
                        JArray response = JArray.Parse(result.ToString());
                        if (typeGet == TYPEGET.MORE)
                        {
                            ObservableCollection<Kios> kiosLst = response.ToObject<ObservableCollection<Kios>>();
                            if (type == TYPE.OLD)
                            {
                                foreach (var item in kiosLst)
                                {
                                    KiosList.Add(item);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < kiosLst.Count; i++)
                                {
                                    KiosList.Insert(i, kiosLst[i]);
                                }
                            }
                        }
                        else
                            KiosList = response.ToObject<ObservableCollection<Kios>>();                        
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
