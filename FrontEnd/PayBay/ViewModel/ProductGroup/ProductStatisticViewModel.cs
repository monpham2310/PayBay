using Newtonsoft.Json.Linq;
using PayBay.Model;
using PayBay.Utilities.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace PayBay.ViewModel.ProductGroup
{
    public class ProductStatisticViewModel : BaseViewModel
    {
        private ObservableCollection<Product> _newProductList;
        private ObservableCollection<ProductStatistic> _bestProductList;

        public ObservableCollection<Product> NewProductList
        {
            get
            {
                return _newProductList;
            }

            set
            {
                _newProductList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ProductStatistic> BestProductList
        {
            get
            {
                return _bestProductList;
            }

            set
            {
                _bestProductList = value;
                OnPropertyChanged();
            }
        }

        public ProductStatisticViewModel()
        {
            MediateClass.ProStatisticVm = this;
            NewProductList = new ObservableCollection<Product>();
            BestProductList = new ObservableCollection<ProductStatistic>();
        }

        public async void GetNewProductList(TYPEGET typeGet, TYPE type = TYPE.OLD)
        {
            try
            {
                JArray result = new JArray();
                int lastId = -1;
                if (typeGet == TYPEGET.MORE)
                {
                    if (NewProductList.Count != 0)
                    {
                        if (type == TYPE.OLD)
                            lastId = NewProductList.Min(x => x.ProductId);
                        else
                            lastId = NewProductList.Max(x => x.ProductId);
                    }
                }

                IDictionary<string, string> param = new Dictionary<string, string>
                {
                    {"typeProduct" , type.ToString()},
                    {"productId" , lastId.ToString()}
                };

                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    var response = await App.MobileService.InvokeApiAsync("ProductStatistics", HttpMethod.Get, param);
                    result = JArray.Parse(response.ToString());
                    ObservableCollection<Product> more = result.ToObject<ObservableCollection<Product>>();
                    if (typeGet == TYPEGET.START)
                    {
                        NewProductList = more;
                    }
                    else
                    {
                        if (type == TYPE.OLD)
                        {
                            foreach (var item in more)
                            {
                                NewProductList.Add(item);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < more.Count; i++)
                            {
                                NewProductList.Insert(i, more[i]);
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

        public async void GetBestSaleProductList(TYPEGET typeGet, TYPE type = TYPE.OLD)
        {
            try
            {
                JArray result = new JArray();
                int lastId = -1;
                if (typeGet == TYPEGET.MORE)
                {
                    if (BestProductList.Count != 0)
                    {
                        if (type == TYPE.OLD)
                            lastId = BestProductList.Min(x => x.Id);
                        else
                            lastId = BestProductList.Max(x => x.Id);
                    }
                }

                IDictionary<string, string> param = new Dictionary<string, string>
                {
                    {"productId" , lastId.ToString()},
                    {"typeProduct" , type.ToString()},
                    {"type" , "true"}
                };

                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    var response = await App.MobileService.InvokeApiAsync("ProductStatistics", HttpMethod.Get, param);
                    result = JArray.Parse(response.ToString());
                    ObservableCollection<ProductStatistic> more = result.ToObject<ObservableCollection<ProductStatistic>>();
                    if (typeGet == TYPEGET.START)
                    {
                        BestProductList = more;
                    }
                    else
                    {
                        if (type == TYPE.OLD)
                        {
                            foreach (var item in more)
                            {
                                BestProductList.Add(item);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < more.Count; i++)
                            {
                                BestProductList.Insert(i, more[i]);
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

    }
}
