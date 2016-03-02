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
        private static bool isResponsed = false;
        private static bool isCommented = false;

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

        public async void GetCommentOfStore(int storeId, TYPEGET typeGet, TYPE type=0)
        {
            string lastId = "";
            if (typeGet == TYPEGET.MORE)
            {
                if (CommentLstOfStore.Count != 0)
                {
                    if (type == TYPE.OLD)
                        lastId = CommentLstOfStore.Min(x => x.Id).ToString();
                    else
                        lastId = CommentLstOfStore.Max(x => x.Id).ToString();
                }
            }
            else
                lastId = "-1";
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"storeId" , storeId.ToString()},
                {"commentId" , lastId},
                {"type" , type.ToString()}
            };
            if(lastId != "")
                await SendData(typeGet, type, param);
        } 

        private async Task SendData(TYPEGET typeGet, TYPE type, IDictionary<string,string> param)
        {
            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    if (!isResponsed)
                    {
                        isResponsed = true;
                        JToken result = await App.MobileService.InvokeApiAsync("Comments", HttpMethod.Get, param);
                        JArray response = JArray.Parse(result.ToString());
                        if (typeGet == TYPEGET.START)
                        {
                            CommentLstOfStore = response.ToObject<ObservableCollection<Comment>>();
                        }
                        else
                        {
                            ObservableCollection<Comment> more = response.ToObject<ObservableCollection<Comment>>();
                            if (type == TYPE.OLD)
                            {
                                foreach (var item in more)
                                {
                                    CommentLstOfStore.Add(item);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < more.Count; i++)
                                {
                                    CommentLstOfStore.Insert(i, more[i]);
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

        public async Task UserComment(string content, int storeId, TYPEGET type, bool inKios=false)
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
                    if (!isCommented)
                    {
                        isCommented = true;
                        JToken result = await App.MobileService.InvokeApiAsync("Comments", body, HttpMethod.Post, null);
                        JObject response = JObject.Parse(result.ToString());
                        if (response["ErrCode"].ToString().Equals("1"))
                        {                            
                            string lastId = "-1";
                            if (!inKios)
                                lastId = CommentLstOfStore.Max(x => x.Id).ToString();
                            IDictionary<string, string> param = new Dictionary<string, string>
                            {
                                {"storeId" , storeId.ToString()},
                                {"commentId" , lastId},
                                {"type" , TYPE.OLD.ToString()}
                            };
                            if (!inKios)
                                await SendData(TYPEGET.START, TYPE.OLD, param);
                            else
                                await SendData(TYPEGET.MORE, TYPE.NEW, param);
                        }
                    }
                    isCommented = false;
                }
            }
            catch (Exception ex)
            {
                isCommented = false;
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }            
        }
                
    }
}
