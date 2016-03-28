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
        private ObservableCollection<DetailBill> _detailList;

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
        #endregion

        public OrderViewModel()
        {
            MediateClass.OrderVM = this;            
            InitializeBill();
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

            _billOfUser = new Bill(createDate, storeId, storeName, totalPrice, reducePrice, userId, userName);
            _detailList = new ObservableCollection<DetailBill>();
            foreach (Product item in MediateClass.ProductVM.ProductOrderList)
            {
                DetailBill detail = new DetailBill(item.ProductId, item.OrderUnit, item.UnitPrice, item.Unit);
                _detailList.Add(detail);
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

    }
}
