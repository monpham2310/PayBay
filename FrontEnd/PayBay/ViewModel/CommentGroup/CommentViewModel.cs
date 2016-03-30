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

        public async void GetCommentOfStore(TYPEGET typeGet, TYPE type=0)
        {
            string lastId = "";
            int storeId = MediateClass.KiotVM.SelectedStore.StoreId;
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
                    }
                }
            }
            catch (Exception ex)
            {                
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }
            finally
            {
                isResponsed = false;
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
                        //JArray response = JArray.Parse(result.ToString());                        
                        //ObservableCollection<Comment> updateCmt = response.ToObject<ObservableCollection<Comment>>();
                        if (!inKios)
                        {
                            //CommentLstOfStore = updateCmt;
                            GetCommentOfStore(TYPEGET.START);
                        }
                        else
                        {
                            //for (int i = 0; i < updateCmt.Count; i++)
                            //{
                            //    CommentLstOfStore.Insert(i, updateCmt[i]);
                            //}
                            GetCommentOfStore(TYPEGET.MORE, TYPE.NEW);
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {                
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }
            finally
            {
                isCommented = false;
            }   
        }
                
    }
}
