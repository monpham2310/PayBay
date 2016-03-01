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

    public class ProductUnitOrder
    {
        private Product _productUnit;
        private int _orderUnit;
        public ProductUnitOrder(Product product, int order)
        {
            _productUnit = product;
            _orderUnit = order;
        }

        public Product ProductUnit { get { return _productUnit; } }
        public int OrderUnit { get { return _orderUnit; } }

    }

    public class ProductViewModel : BaseViewModel
    {


        private Product _selectedProduct;
        private ObservableCollection<Product> _productsOfStore;
        private ObservableCollection<Product> _productList;
        private static bool isResponsed = false;

        //List for order purpose
        private ObservableCollection<ProductUnitOrder> _productOrderList;
        //

        #region Property with calling to PropertyChanged
        public ObservableCollection<ProductUnitOrder> ProductOrderList
        {
            get { return _productOrderList; }
            set
            {
                if (Equals(value, _productOrderList)) return;
                _productOrderList = value;
                OnPropertyChanged();
            }
        }

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

        public ObservableCollection<Product> ProductsOfStore
        {
            get
            {
                return _productsOfStore;
            }

            set
            {
                _productsOfStore = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public ProductViewModel()
        {
            MediateClass.ProductVM = this;
            InitializeProperties();
            //InitializeData();
        }

        private void InitializeProperties()
        {
            ProductList = new ObservableCollection<Product>();
            ProductOrderList = new ObservableCollection<ProductUnitOrder>();
        }

        private async void InitializeData()
        {
            await LoadMoreProduct(TYPEGET.START);        
        }      

        public async Task LoadMoreProduct(TYPEGET type, TYPE isOld=0)
        {
            string lastId = "";
            if (type == TYPEGET.MORE)
            {
                if (ProductList.Count != 0)
                {
                    if (isOld == TYPE.OLD)
                        lastId = ProductList.Min(x => x.ProductId).ToString();
                    else
                        lastId = ProductList.Max(x => x.ProductId).ToString();
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

        public async void LoadMoreProduct(string name, TYPEGET type, TYPE isOld=0)
        {
            string lastId = "";
            if (type == TYPEGET.MORE)
            {
                if (ProductList.Count != 0)
                {
                    if (isOld == TYPE.OLD)
                        lastId = ProductList.Min(x => x.ProductId).ToString();
                    else
                        lastId = ProductList.Max(x => x.ProductId).ToString();
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
                
        public async void GetProductsOfStore(int storeId, TYPEGET typeGet, TYPE type=0)
        {
            string lastId = "";
            if (typeGet == TYPEGET.MORE)
            {
                if (ProductsOfStore.Count != 0)
                {
                    if (type == TYPE.OLD)
                        lastId = ProductsOfStore.Min(x => x.ProductId).ToString();
                    else
                        lastId = ProductsOfStore.Max(x => x.ProductId).ToString();
                }
            }
            else
                lastId = "-1";
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"storeId" , storeId.ToString()},
                {"productId" , lastId},
                {"type" , type.ToString()}
            };
            if(lastId != "")
                await PassDataOfStore(typeGet, type, param);
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
                        JToken result = await App.MobileService.InvokeApiAsync("Products", HttpMethod.Get, param);
                        JArray products = JArray.Parse(result.ToString());
                        if (typeGet == TYPEGET.MORE)
                        {
                            ObservableCollection<Product> moreProduct = products.ToObject<ObservableCollection<Product>>();
                            if (type == TYPE.OLD)
                            {
                                foreach (var item in moreProduct)
                                {
                                    ProductList.Add(item);
                                }
                            }
                            else {
                                for (int i = 0; i < moreProduct.Count; i++)
                                {
                                    ProductList.Insert(i, moreProduct[i]);
                                }
                            }
                        }
                        else
                            ProductList = products.ToObject<ObservableCollection<Product>>();
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

        private async Task PassDataOfStore(TYPEGET typeGet, TYPE type, IDictionary<string, string> param)
        {
            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    if (!isResponsed)
                    {
                        isResponsed = true;
                        JToken result = await App.MobileService.InvokeApiAsync("Products", HttpMethod.Get, param);
                        JArray response = JArray.Parse(result.ToString());
                        if (typeGet == TYPEGET.START)
                        {
                            ProductsOfStore = response.ToObject<ObservableCollection<Product>>();
                        }
                        else
                        {
                            ObservableCollection<Product> more = response.ToObject<ObservableCollection<Product>>();
                            if (type == TYPE.OLD)
                            {
                                foreach (var item in more)
                                {
                                    ProductsOfStore.Add(item);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < more.Count; i++)
                                {
                                    ProductsOfStore.Insert(i, more[i]);
                                }
                            }
                        }
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
