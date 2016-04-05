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
using Windows.Storage;

namespace PayBay.ViewModel.MarketGroup
{
    public class KiosViewModel : BaseViewModel
    {
        public static bool isUpdate = false;

        private Kios _selectedStore;
        private ObservableCollection<Kios> _kiosList;
        private ObservableCollection<Kios> _storesOfOwner;
        
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

        public ObservableCollection<Kios> StoresOfOwner
        {
            get
            {
                return _storesOfOwner;
            }

            set
            {
                _storesOfOwner = value;
                OnPropertyChanged();
            }
        }
                
        #endregion

        public KiosViewModel()
        {
            MediateClass.KiotVM = this;
            InitializeProperties();            
            //LoadMoreStore(TYPEGET.START);            
        }
                
        private void InitializeProperties()
        {            
            SelectedStore = new Kios();
            KiosList = new ObservableCollection<Kios>();
            StoresOfOwner = new ObservableCollection<Kios>();
        }
                
        public async void LoadMoreStore(TYPEGET typeGet,TYPE type=0)
        {
            string lastId = "";
            int marketId = MediateClass.MarketVM.SelectedMarket.MarketId;
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
               
        public async void LoadMoreStore(string storeName, TYPEGET typeGet, TYPE type=0)
        {
            string lastId = "";
            int marketId = MediateClass.MarketVM.SelectedMarket.MarketId;
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

        public async Task<bool> InsertStore(Kios store, StorageFile media)
        {
            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    if (store.Image == null || store.Image == "/Assets/LockScreenLogo.scale-200.png")
                    {
                        if (media == null)
                            store.Image = "/Assets/LockScreenLogo.scale-200.png";
                        else
                            store.Image = null;
                    }
                    JToken data = JToken.FromObject(store);
                    JToken result = await App.MobileService.InvokeApiAsync("Stores", data, HttpMethod.Post, null);
                    JObject response = JObject.Parse(result.ToString());

                    if (media != null)
                    {
                        store.Image = response["Image"].ToString();
                        store.SasQuery = response["SasQuery"].ToString();
                        await Functions.Instance.UploadImageToBlob("stores", store.Image, store.SasQuery, media);
                    }                   
                                
                }
                else
                {
                    await new MessageDialog("You have not internet connection!", "Store Register").ShowAsync();
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

        public async Task<bool> UpdateStore(Kios store, StorageFile media)
        {
            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    if (store.Image == null || store.Image == "/Assets/LockScreenLogo.scale-200.png")
                    {
                        if (media == null)
                            store.Image = "/Assets/LockScreenLogo.scale-200.png";
                        else
                            store.Image = null;
                    }
                    JToken data = JToken.FromObject(store);
                    JToken result = await App.MobileService.InvokeApiAsync("Stores", data, HttpMethod.Put, null);
                    JObject response = JObject.Parse(result.ToString());

                    SelectedStore = response.ToObject<Kios>();
                    if (media != null)
                    {
                        await Functions.Instance.UploadImageToBlob("stores", SelectedStore.Image, SelectedStore.SasQuery, media);
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

        public async void GetStoresOfOwner()
        {
            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    JArray result = new JArray();
                    int ownerId = MediateClass.UserVM.UserInfo.UserId;
                    IDictionary<string, string> param = new Dictionary<string, string>
                    {
                        {"ownerId" , ownerId.ToString()}
                    };
                    var response = await App.MobileService.InvokeApiAsync("Stores", HttpMethod.Get, param);
                    result = JArray.Parse(response.ToString());
                    StoresOfOwner = result.ToObject<ObservableCollection<Kios>>();
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Get Store").ShowAsync();
            }
        }

        public async Task<bool> DeleteStore()
        {
            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    JObject result = new JObject();
                    IDictionary<string, string> param = new Dictionary<string, string>
                    {
                        {"storeId", SelectedStore.StoreId.ToString()}
                    };
                    var response = await App.MobileService.InvokeApiAsync("Stores", HttpMethod.Delete, param);
                    result = JObject.Parse(response.ToString());
                    Kios store = result.ToObject<Kios>();
                    if (store.Image != null && store.SasQuery != null)
                        await Functions.Instance.DeleteImageInBlob("stores", store.Image, store.SasQuery);
                    SelectedStore = null;
                }
                else
                {
                    await new MessageDialog("You have not internet connection!", "Insert Product").ShowAsync();
                    return false;
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Delete Store").ShowAsync();
                return false;
            }
            return true;
        }

    }
}
