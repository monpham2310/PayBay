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

namespace PayBay.ViewModel.InboxGroup
{
    public class InboxDetailViewModel : BaseViewModel
    {
        private static bool isResponsed = false;
        private static bool isSended = false;
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
            MediateClass.MsgDetailVM = this;
            _detailList = new ObservableCollection<InboxDetail>();
            //if(MediateClass.MessageVM.MessageSelected != null)
            //    LoadMoreMessage(TYPEGET.START);
        }
                
        public async void LoadMoreMessage(TYPEGET typeGet, TYPE type = TYPE.OLD)
        {
            int lastId = -1;
            if(typeGet == TYPEGET.MORE)
            {
                if (type == TYPE.OLD) {
                    lastId = DetailList.Min(x => x.ID);
                }
                else
                {
                    lastId = DetailList.Max(x => x.ID);
                }
            }

            int messageId = MediateClass.MessageVM.MessageSelected.MessageId;
                  
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"id" , lastId.ToString()},
                {"messageId" , messageId.ToString()},
                {"type" , type.ToString()}
            };

            await SendData(typeGet, type, param);
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
                        JToken result = await App.MobileService.InvokeApiAsync("InboxDetails", HttpMethod.Get, param);
                        JArray response = JArray.Parse(result.ToString());
                        if (typeGet == TYPEGET.START)
                        {
                            DetailList = response.ToObject<ObservableCollection<InboxDetail>>();
                        }
                        else
                        {
                            ObservableCollection<InboxDetail> more = response.ToObject<ObservableCollection<InboxDetail>>();
                            if (type == TYPE.OLD)
                            {                                
                                foreach (var item in more)
                                {
                                    DetailList.Add(item);
                                }
                            }
                            else
                            {                                
                                for (int i = 0; i < more.Count; i++)
                                {
                                    DetailList.Insert(i, more[i]);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Inbox Detail!").ShowAsync();
            }
            finally
            {
                isResponsed = false;
            }
        }

        public async Task PushMessage(string content, DateTime inboxDate)
        {
            //int messageId = MediateClass.MessageVM.MessageSelected.MessageId;
            
            //InboxDetail inbox = new InboxDetail(messageId, inboxDate, content);
            //try
            //{
            //    if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
            //    {
            //        if (!isSended)
            //        {
            //            isSended = true;
            //            JToken body = JToken.FromObject(inbox);
            //            var response = await App.MobileService.InvokeApiAsync("InboxDetails", body, HttpMethod.Post, null);
            //            inbox = response.ToObject<InboxDetail>();
            //            DetailList.Insert(0, inbox);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    await new MessageDialog(ex.Message.ToString(), "Inbox Detail!").ShowAsync();
            //}
            //finally
            //{
            //    isSended = false;
            //}
        }

    }
}
