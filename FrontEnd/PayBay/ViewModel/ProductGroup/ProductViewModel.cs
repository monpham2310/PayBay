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

namespace PayBay.ViewModel.ProductGroup
{
    public class ProductViewModel : BaseViewModel
    {
        private Product _selectedProduct;
        private ObservableCollection<Product> _productList;
                
        #region Property with calling to PropertyChanged
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                if (Equals(value, _selectedProduct)) return;
                _selectedProduct = value;
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
            InitializeProperties();
            InitializeData();
        }

        private void InitializeProperties()
        {
            ProductList = new ObservableCollection<Product>();
        }

        private async void InitializeData()
        {
            await LoadMoreProduct(Functions.TYPEGET.START);
        }      

        public async Task LoadMoreProduct(Functions.TYPEGET type)
        {
            string lastId = "";
            if (type == Functions.TYPEGET.MORE)
                lastId = ProductList[ProductList.Count - 1].ProductId.ToString();
            else
                lastId = "-1";
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"id" , lastId}                
            };
            try
            {
                JToken result = await App.MobileService.InvokeApiAsync("Products", HttpMethod.Get, param);
                JArray products = JArray.Parse(result.ToString());
                if (type == Functions.TYPEGET.MORE)
                {
                    ObservableCollection<Product> moreProduct = products.ToObject<ObservableCollection<Product>>();

                    foreach (var item in moreProduct)
                    {
                        ProductList.Add(item);
                    }
                }
                else
                    ProductList = products.ToObject<ObservableCollection<Product>>();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }            
        }

        public async Task LoadMoreProduct(string name, Functions.TYPEGET type)
        {
            string lastId = "";
            if (type == Functions.TYPEGET.MORE)
                lastId = ProductList[ProductList.Count - 1].ProductId.ToString();
            else
                lastId = "-1";
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"id" , lastId},
                { "name" , name }
            };
            try
            {
                JToken result = await App.MobileService.InvokeApiAsync("Products", HttpMethod.Get, param);
                JArray products = JArray.Parse(result.ToString());
                if (type == Functions.TYPEGET.MORE)
                {
                    ObservableCollection<Product> moreProduct = products.ToObject<ObservableCollection<Product>>();

                    foreach (var item in moreProduct)
                    {
                        ProductList.Add(item);
                    }
                }
                else
                    ProductList = products.ToObject<ObservableCollection<Product>>();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }            
        }

    }
}
