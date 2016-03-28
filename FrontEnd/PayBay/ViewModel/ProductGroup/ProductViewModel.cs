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
using PayBay.Model.RequestBody;
using Windows.Storage;

namespace PayBay.ViewModel.ProductGroup
{
    public class ProductViewModel : BaseViewModel
    {
        private Product _selectedProduct;
        private ObservableCollection<Product> _productsOfStore;
        private ObservableCollection<Product> _productList;
        private ObservableCollection<Product> _productsOfStoreOwner;

        public static bool isUpdate = false;
        private static bool isResponsed = false;

        //List for order purpose
        private ObservableCollection<Product> _productOrderList;
        //
        
        #region Property with calling to PropertyChanged
        public ObservableCollection<Product> ProductOrderList
        {
            get { return _productOrderList; }
            set
            {                                
                _productOrderList = value;
                OnPropertyChanged();
            }
        }

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {                
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

        public ObservableCollection<Product> ProductsOfStoreOwner
        {
            get
            {
                return _productsOfStoreOwner;
            }

            set
            {
                _productsOfStoreOwner = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public ProductViewModel()
        {
            MediateClass.ProductVM = this;
            InitializeProperties();
            //LoadMoreProduct(TYPEGET.START);        
        }

        private void InitializeProperties()
        {
            ProductList = new ObservableCollection<Product>();
            ProductOrderList = new ObservableCollection<Product>();
        }                 

        public async void LoadMoreProduct(TYPEGET type, TYPE isOld=0)
        {
            string lastId = "-1";
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
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"id" , lastId},
                {"type" , isOld.ToString()}
            };
            await SendData(type, isOld, param);         
        }

        public async void LoadMoreProduct(string name, TYPEGET type, TYPE isOld=0)
        {
            string lastId = "-1";
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
            string lastId = "-1";
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
                            else
                            {
                                for (int i = 0; i < moreProduct.Count; i++)
                                {
                                    ProductList.Insert(i, moreProduct[i]);
                                }
                            }
                        }
                        else
                            ProductList = products.ToObject<ObservableCollection<Product>>();                        
                    }
                }
            }
            catch (Exception ex)
            {                
                await new MessageDialog(ex.Message.ToString(), "Load Product").ShowAsync();
            }
            finally
            {
                isResponsed = false;
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
                        ObservableCollection<Product> more = response.ToObject<ObservableCollection<Product>>();
                        if (typeGet == TYPEGET.START)
                        {
                            ProductsOfStore = more;
                        }
                        else
                        {                            
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
                    }
                }
            }
            catch (Exception ex)
            {                
                await new MessageDialog(ex.Message.ToString(), "Load Product!").ShowAsync();
            }
            finally
            {
                isResponsed = false;
            }
        }

        public async void LoadProductsOfStoreOwner(TYPEGET typeGet, TYPE type = TYPE.OLD)
        {
            try
            {
                JArray result = new JArray();
                int lastId = -1;
                if (typeGet == TYPEGET.MORE)
                {
                    if (ProductsOfStoreOwner.Count != 0)
                    {
                        if (type == TYPE.OLD)
                            lastId = ProductsOfStoreOwner.Min(x => x.ProductId);
                        else
                            lastId = ProductsOfStoreOwner.Max(x => x.ProductId);
                    }
                }
                int ownerId = MediateClass.UserVM.UserInfo.UserId;
                ProductInfo proInfo = new ProductInfo(ownerId, lastId, type);
                JToken body = JToken.FromObject(proInfo);

                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {                    
                    var response = await App.MobileService.InvokeApiAsync("Products", body, HttpMethod.Get, null);
                    result = JArray.Parse(response.ToString());
                    ObservableCollection<Product> more = result.ToObject<ObservableCollection<Product>>();
                    if (typeGet == TYPEGET.START)
                    {
                        ProductsOfStoreOwner = more;
                    }
                    else
                    {
                        if(type == TYPE.OLD)
                        {
                            foreach (var item in more)
                            {
                                ProductsOfStoreOwner.Add(item);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < more.Count; i++)
                            {
                                ProductsOfStoreOwner.Insert(i, more[i]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Load Product").ShowAsync();
            }
        }

        public async Task<bool> InsertProduct(Product product, StorageFile media)
        {
            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    if (product.Image == null || product.Image == "/Assets/LockScreenLogo.scale-200.png")
                    {
                        if (media == null)
                            product.Image = "/Assets/LockScreenLogo.scale-200.png";
                        else
                            product.Image = null;
                    }
                    JToken data = JToken.FromObject(product);
                    JToken result = await App.MobileService.InvokeApiAsync("Products", data, HttpMethod.Post, null);

                    JObject response = JObject.Parse(result.ToString());
                    if (media != null)
                    {
                        product.Image = response["Image"].ToString();
                        product.SasQuery = response["SasQuery"].ToString();
                        bool check = await Functions.Instance.UploadImageToBlob("products", product.Image, product.SasQuery, media);
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

        public async Task<bool> UpdateProduct(Product product, StorageFile media)
        {
            try
            {
                if (product.Image == null || product.Image == "/Assets/LockScreenLogo.scale-200.png")
                {
                    if (media == null)
                        product.Image = "/Assets/LockScreenLogo.scale-200.png";
                    else
                        product.Image = null;
                }
                JToken data = JToken.FromObject(product);
                JToken result = await App.MobileService.InvokeApiAsync("Products", data, HttpMethod.Put, null);
                JObject response = JObject.Parse(result.ToString());

                SelectedProduct = response.ToObject<Product>();
                if (media != null)
                {
                    await Functions.Instance.UploadImageToBlob("products", SelectedProduct.Image, SelectedProduct.SasQuery, media);
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteProduct()
        {
            try
            {
                JObject result = new JObject();
                IDictionary<string, string> param = new Dictionary<string, string>
                {
                    {"productId", SelectedProduct.ProductId.ToString()}
                };
                var response = await App.MobileService.InvokeApiAsync("Products", HttpMethod.Delete, param);
                result = JObject.Parse(response.ToString());
                if (result["ErrCode"].ToString() == "0")
                {
                    return false;
                }
                await Functions.Instance.DeleteImageInBlob("products", SelectedProduct.Image, SelectedProduct.SasQuery);
                SelectedProduct = null;
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Delete Product").ShowAsync();
                return false;
            }
            return true;
        }

    }
}
