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

namespace PayBay.ViewModel.OrderGroupViewModel
{
    public class OrderViewModel : BaseViewModel
    {
        private Bill _billOfUser;
        private Bill _selectedBill;

        private ObservableCollection<DetailBill> _detailBillList;
        private ObservableCollection<DetailBill> _detailList;
        private ObservableCollection<Bill> _billOfStoreOwner;

        #region
        public Bill BillOfUser
        {
            get
            {
                return _billOfUser;
            }

            set
            {
                _billOfUser = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DetailBill> DetailList
        {
            get
            {
                return _detailList;
            }

            set
            {
                _detailList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Bill> BillOfStoreOwner
        {
            get
            {
                return _billOfStoreOwner;
            }

            set
            {
                _billOfStoreOwner = value;
                OnPropertyChanged();
            }
        }

        public Bill SelectedBill
        {
            get
            {
                return _selectedBill;
            }

            set
            {
                _selectedBill = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DetailBill> DetailBillList
        {
            get
            {
                return _detailBillList;
            }

            set
            {
                _detailBillList = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public OrderViewModel()
        {
            MediateClass.OrderVM = this;
            BillOfUser = new Bill();
            DetailList = new ObservableCollection<DetailBill>();
            SelectedBill = new Bill();
            DetailBillList = new ObservableCollection<DetailBill>();
            //InitializeBill();
        }

        public void InitializeBill()
        {
            DateTime createDate = DateTime.Now;
            int storeId = MediateClass.KiotVM.SelectedStore.StoreId;
            string storeName = MediateClass.KiotVM.SelectedStore.StoreName;
            double totalPrice = MediateClass.ProductVM.ProductOrderList.Sum(x => x.UnitPrice*x.OrderUnit);            
            double reducePrice = 0;
            int userId = MediateClass.UserVM.UserInfo.UserId;
            string userName = MediateClass.KiotVM.SelectedStore.Username;
            //bool isShipped = false;
            //string note = "";
            //string shipMethod = "";
            //string agreeShipDate = "";
            //DateTime shipDate;

            BillOfUser = new Bill(createDate, storeId, storeName, totalPrice, reducePrice, userId, userName);
            DetailList = new ObservableCollection<DetailBill>();
            foreach (Product item in MediateClass.ProductVM.ProductOrderList)
            {
                DetailBill detail = new DetailBill(item.ProductId, item.OrderUnit, item.UnitPrice, item.Unit);
                DetailList.Add(detail);
            }            
        }

        public async Task SubmitBill()
        {
            try
            {
                JToken bill = JToken.FromObject(BillOfUser);
                
                var resultBill = await App.MobileService.InvokeApiAsync("Bills", bill, HttpMethod.Post, null);
                JObject myBill = JObject.Parse(resultBill.ToString());
                Bill temp = myBill.ToObject<Bill>();
                BillOfUser.BillId = temp.BillId;
                foreach (DetailBill item in DetailList)
                { 
                    item.BillId = temp.BillId;
                }

                JToken detailbill = JToken.FromObject(DetailList);
                var resultDetail = await App.MobileService.InvokeApiAsync("DetailBills", detailbill, HttpMethod.Post, null);
                JObject response = JObject.Parse(resultDetail.ToString());
                if (response["ErrCode"].ToString().Equals("1"))
                {
                    await new MessageDialog("Submit is successful!", "Notification!").ShowAsync();
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(),"Order Page").ShowAsync();
            }

        }

        public async void LoadBillOfStoreOwner()
        {
            int ownerId = MediateClass.UserVM.UserInfo.UserId;
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"ownerId" , ownerId.ToString()},
                {"isStoreOwner" , "true"}
            };

            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    var result = await App.MobileService.InvokeApiAsync("Bills", HttpMethod.Get, param);
                    JArray response = JArray.Parse(result.ToString());
                    BillOfStoreOwner = response.ToObject<ObservableCollection<Bill>>();
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Order Page").ShowAsync();
            }
        }

        public async void LoadDetailBillOfBill()
        {
            int bill = SelectedBill.BillId;
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"billId" , bill.ToString()},
                {"isManage" , "true"}
            };
            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    var result = await App.MobileService.InvokeApiAsync("DetailBills", HttpMethod.Get, param);
                    JArray response = JArray.Parse(result.ToString());
                    DetailBillList = response.ToObject<ObservableCollection<DetailBill>>();
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Order Page").ShowAsync();
            }
        }

    }
}
