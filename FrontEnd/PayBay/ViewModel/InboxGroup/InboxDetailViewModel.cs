using Newtonsoft.Json.Linq;
using PayBay.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace PayBay.ViewModel.InboxGroup
{
    public class InboxDetailViewModel : BaseViewModel
    {
        private ObservableCollection<InboxDetail> _detailList;

        public ObservableCollection<InboxDetail> DetailList
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

        public InboxDetailViewModel()
        {
            _detailList = new ObservableCollection<InboxDetail>();
        }

        public async Task GetMessageDetail(int messageId)
        {
            JArray result = new JArray();
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"messageId" , messageId.ToString()}
            };
            try
            {
                var response = await App.MobileService.InvokeApiAsync("InboxDetails", HttpMethod.Get, param);
                result = JArray.Parse(response.ToString());
                DetailList = result.ToObject<ObservableCollection<InboxDetail>>();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Detail Message!").ShowAsync();
            }
        }

    }
}
