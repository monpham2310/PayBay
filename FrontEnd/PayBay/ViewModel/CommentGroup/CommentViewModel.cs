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

namespace PayBay.ViewModel.CommentGroup
{
    public class CommentViewModel : BaseViewModel
    {
        private ObservableCollection<Comment> _commentLstOfStore;

        public ObservableCollection<Comment> CommentLstOfStore
        {
            get
            {
                return _commentLstOfStore;
            }

            set
            {
                _commentLstOfStore = value;
                OnPropertyChanged();
            }
        }

        public CommentViewModel()
        {
            MediateClass.CommentVM = this;
            CommentLstOfStore = new ObservableCollection<Comment>();
        }

        public async Task GetCommentOfStore(int storeId)
        {
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"storeId" , storeId.ToString()}
            };
            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    JToken result = await App.MobileService.InvokeApiAsync("Comments", HttpMethod.Get, param);
                    JArray response = JArray.Parse(result.ToString());
                    CommentLstOfStore = response.ToObject<ObservableCollection<Comment>>();
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }
        } 

        public async Task UserComment(string content,int storeId)
        {
            UserInfo currentUser = MediateClass.UserVM.UserInfo;
            Comment comment = new Comment();
            comment.CommentDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            comment.StoreId = storeId;
            comment.UserId = currentUser.UserId;
            comment.Username = currentUser.Username;
            comment.Avatar = currentUser.Avatar;
            comment.Content = content;

            JToken body = JToken.FromObject(comment);

            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    JToken result = await App.MobileService.InvokeApiAsync("Comments", body, HttpMethod.Post, null);
                    JObject response = JObject.Parse(result.ToString());
                    if (response["ErrCode"].ToString().Equals("1"))
                    {
                        CommentLstOfStore.Add(comment);
                        var list = CommentLstOfStore.OrderByDescending(x => x.CommentDate).ToList();
                        CommentLstOfStore.Clear();
                        for (int i = 0; i < 4; i++)
                        {
                            CommentLstOfStore.Add(list[i]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }            
        }

    }
}
