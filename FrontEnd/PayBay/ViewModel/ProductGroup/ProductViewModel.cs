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

namespace PayBay.ViewModel.ProductGroup
{
    public class ProductViewModel : BaseViewModel
    {
        private Product _newProduct;
        private ObservableCollection<Product> _productList;

        #region Property with calling to PropertyChanged
        public Product NewProduct
        {
            get { return _newProduct; }
            set
            {
                if (Equals(value, _newProduct)) return;
                _newProduct = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Product> ProductList
        {
            get
            {
                return _productList;
            }

            set
            {
                if (Equals(value, _productList)) return;
                _productList = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public ProductViewModel()
        {
            InitializeData();
        }

        private async void InitializeData()
        {
            ProductList = new ObservableCollection<Product>();

            IDictionary<string, string> param = new Dictionary<string, string>
            {
                { "id" , "-1" }
            };

            try
            {
                JToken result = await App.MobileService.InvokeApiAsync("Products", HttpMethod.Get, param);
                JArray products = JArray.Parse(result.ToString());
                ProductList = products.ToObject<ObservableCollection<Product>>();
            }
            catch (Exception ex)
            {                
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }            
        }

        public async Task GetProductFollowName(string name)
        {            
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                { "id" , "-1" },
                { "name" , name }
            };
            try {
                JToken result = await App.MobileService.InvokeApiAsync("Products", HttpMethod.Get, param);
                JArray list = JArray.Parse(result.ToString());

                ProductList = list.ToObject<ObservableCollection<Product>>();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }            
        }

        public async void LoadMoreProduct()
        {
            string lastId = ProductList[ProductList.Count - 1].ProductId.ToString();

            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"id" , lastId}                
            };
            try
            {
                JToken result = await App.MobileService.InvokeApiAsync("Products", HttpMethod.Get, param);
                JArray markets = JArray.Parse(result.ToString());
                ObservableCollection<Product> moreProduct = markets.ToObject<ObservableCollection<Product>>();

                foreach (var item in moreProduct)
                {
                    ProductList.Add(item);
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }            
        }

        public async void LoadMoreProduct(string name)
        {
            string lastId = ProductList[ProductList.Count - 1].ProductId.ToString();

            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"id" , lastId},
                { "name" , name }
            };
            try
            {
                JToken result = await App.MobileService.InvokeApiAsync("Products", HttpMethod.Get, param);
                JArray markets = JArray.Parse(result.ToString());
                ObservableCollection<Product> moreProduct = markets.ToObject<ObservableCollection<Product>>();

                foreach (var item in moreProduct)
                {
                    ProductList.Add(item);
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }            
        }

    }
}
