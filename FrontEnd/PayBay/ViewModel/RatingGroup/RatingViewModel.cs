using Newtonsoft.Json.Linq;
using PayBay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace PayBay.ViewModel.RatingGroup
{
    public class RatingViewModel : BaseViewModel
    {
        private Rating _rate;

        public Rating Rate
        {
            get
            {
                return _rate;
            }

            set
            {
                _rate = value;
                OnPropertyChanged();
            }
        }

        public RatingViewModel()
        {
            Rate = new Rating();
        }

        public async Task<bool> PostRate(Rating numOfRate)
        {
            JObject result = new JObject();
            JToken body = JToken.FromObject(numOfRate);
            try
            {
                if (Utilities.Helpers.NetworkHelper.HasInternetConnection)
                {
                    var message = await App.MobileService.InvokeApiAsync("StatisticRatings", body, HttpMethod.Post, null);
                    result = JObject.Parse(message.ToString());
                    if (result["ErrCode"].ToString() == "1")
                        return true;
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }
            return false;
        }

    }
}
