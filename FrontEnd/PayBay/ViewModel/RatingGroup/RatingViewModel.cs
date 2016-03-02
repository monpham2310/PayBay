using Newtonsoft.Json.Linq;
using PayBay.Model;
using PayBay.Utilities.Common;
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
            MediateClass.RateVm = this;
            _rate = new Rating();
            LoadStarRated();
        }

        public async void LoadStarRated()
        {
            int user = MediateClass.UserVM.UserInfo.UserId;
            int store = MediateClass.KiotVM.SelectedStore.StoreId;

            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"userId" , user.ToString()},
                {"storeId" , store.ToString()}
            };

            try
            {
                var result = await App.MobileService.InvokeApiAsync("StatisticRatings", HttpMethod.Get, param);
                JObject response = JObject.Parse(result.ToString());
                Rate = response.ToObject<Rating>();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }
        }

        public async Task PostRate(double rated)
        {
            int user = MediateClass.UserVM.UserInfo.UserId;
            int store = MediateClass.KiotVM.SelectedStore.StoreId;

            Rating UserRate = new Rating(user, store, rated);

            JObject result = new JObject();
            JToken body = JToken.FromObject(UserRate);
            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    var message = await App.MobileService.InvokeApiAsync("StatisticRatings", body, HttpMethod.Post, null);
                    result = JObject.Parse(message.ToString());

                    Kios temp = result.ToObject<Kios>();

                    UpdateDataLocal(temp, UserRate);
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }            
        }

        private void UpdateDataLocal(Kios temp, Rating UserRate)
        {
            Rate.RateOfUser = UserRate.RateOfUser;
            if (MediateClass.KiotVM.SelectedStore.StoreId == temp.StoreId)
                MediateClass.KiotVM.SelectedStore.Rate = temp.Rate;
            for (int i=0;i<MediateClass.KiotVM.KiosList.Count;i++)
            {
                if(MediateClass.KiotVM.KiosList[i].StoreId == temp.StoreId)
                {
                    MediateClass.KiotVM.KiosList[i].Rate = temp.Rate;
                    break;
                }
            }
            
            for(int j = 0; j < MediateClass.CommentVM.CommentLstOfStore.Count; j++)
            {
                if(MediateClass.CommentVM.CommentLstOfStore[j].UserId == MediateClass.UserVM.UserInfo.UserId 
                        && MediateClass.CommentVM.CommentLstOfStore[j].StoreId == UserRate.StoreId)
                {
                    MediateClass.CommentVM.CommentLstOfStore[j].Rated = UserRate.RateOfUser;
                }
            }    
                   
        }
                
    }
}
