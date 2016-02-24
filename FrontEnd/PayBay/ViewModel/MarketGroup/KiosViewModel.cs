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
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using PayBay.Utilities.Common;

namespace PayBay.ViewModel.MarketGroup
{
    public class KiosViewModel : BaseViewModel
    {
        private ObservableCollection<Kios> _kiosList;
        private int _marketID;

        private Kios _fakeKios;

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

        public int MarketID
        {
            get
            {
                return _marketID;
            }

            set
            {
                _marketID = value;
                OnPropertyChanged();
            }
        }

        public Kios FakeKios
        {
            get
            {
                return _fakeKios;
            }
            set
            {
                _fakeKios = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public KiosViewModel()
        {
            MediateClass.KiotVM = this;
            InitializeProperties();
            InitializeData();

            //Fake Data
            InitFakeData();
        }

        //Initialize Fake Data
        private void InitFakeData()
        {
            _fakeKios = new Kios();
            ObservableCollection<Product> fakeProductList = new ObservableCollection<Product>();
            for (int i=0; i < 5; i++)
            {
                Product fakeProduct = new Product();
                fakeProduct.ProductName = "Ba Con Soi";
                fakeProductList.Add(fakeProduct);
            }
            _fakeKios.ProductList = fakeProductList;
        }

        private void InitializeProperties()
        {
            MarketID = -1;
            KiosList = new ObservableCollection<Kios>();
        }

        public async void InitializeData()
        {            
            await LoadMoreStore(Functions.TYPEGET.START);
        }

        public async Task LoadMoreStore(Functions.TYPEGET type)
        {
            int lastId = -1;
            if(type == Functions.TYPEGET.MORE)
                lastId = KiosList[KiosList.Count - 1].StoreId;
            MarketID = MediateClass.MarketVM.SelectedMarket.MarketId;        
            IDictionary<string, string> market = new Dictionary<string, string>
            {
                {"marketId", MarketID.ToString()},
                {"storeId" , lastId.ToString()}
            };
            try
            {
                JToken result = await App.MobileService.InvokeApiAsync("Stores", HttpMethod.Get, market);
                JArray response = JArray.Parse(result.ToString());
                if (type == Functions.TYPEGET.MORE)
                {
                    ObservableCollection<Kios> kiosLst = response.ToObject<ObservableCollection<Kios>>();

                    foreach (var item in kiosLst)
                    {
                        KiosList.Add(item);
                    }
                }
                else
                    KiosList = response.ToObject<ObservableCollection<Kios>>();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }
        }
               
        public async Task LoadMoreStore(string storeName, Functions.TYPEGET type)
        {
            int lastId = -1;
            if (type == Functions.TYPEGET.MORE)
                lastId = KiosList[KiosList.Count - 1].StoreId;
            MarketID = MediateClass.MarketVM.SelectedMarket.MarketId;
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"name" , storeName},
                {"markId" , MarketID.ToString()},
                {"storeId" , "-1"}
            };
            try
            {
                JToken result = await App.MobileService.InvokeApiAsync("Stores", HttpMethod.Post, param);
                JArray response = JArray.Parse(result.ToString());
                if (type == Functions.TYPEGET.MORE)
                {
                    ObservableCollection<Kios> kiosLst = response.ToObject<ObservableCollection<Kios>>();

                    foreach (var item in kiosLst)
                    {
                        KiosList.Add(item);
                    }
                }
                else
                    KiosList = response.ToObject<ObservableCollection<Kios>>();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }
        }

    }
}
