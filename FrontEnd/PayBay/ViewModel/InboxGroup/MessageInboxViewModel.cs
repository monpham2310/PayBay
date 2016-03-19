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
    public class MessageInboxViewModel : BaseViewModel
    {
        private static bool isResponsed = false;
        private static bool isRequested = false;
        private ObservableCollection<MessageInbox> _messageList;
        private MessageInbox _messageSelected;

        public ObservableCollection<MessageInbox> MessageList
        {
            get
            {
                return _messageList;
            }

            set
            {
                _messageList = value;
                OnPropertyChanged();
            }
        }

        public MessageInbox MessageSelected
        {
            get
            {
                return _messageSelected;
            }

            set
            {
                _messageSelected = value;
                OnPropertyChanged();
            }
        }

        public MessageInboxViewModel()
        {
            MediateClass.MessageVM = this;
            _messageList = new ObservableCollection<MessageInbox>();
            _messageSelected = new MessageInbox();
            
            //LoadMoreMessageList(TYPEGET.START);
        }
                
        public async void LoadMoreMessageList(TYPEGET typeGet, TYPE type = TYPE.OLD)
        {
            int lastId = -1;
            if (typeGet == TYPEGET.MORE)
            {
                if (type == TYPE.OLD)
                {
                    lastId = MessageList.Min(x => x.MessageId);
                }
                else
                {
                    lastId = MessageList.Max(x => x.MessageId);
                }
            }

            int userId = MediateClass.UserVM.UserInfo.UserId;

            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"messageId" , lastId.ToString()},
                {"userId" , userId.ToString()},
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
                        JToken result = await App.MobileService.InvokeApiAsync("MessageInboxes", HttpMethod.Get, param);
                        JArray response = JArray.Parse(result.ToString());
                        if (typeGet == TYPEGET.START)
                        {
                            MessageList = response.ToObject<ObservableCollection<MessageInbox>>();
                        }
                        else
                        {
                            ObservableCollection<MessageInbox> more = response.ToObject<ObservableCollection<MessageInbox>>();
                            if (type == TYPE.OLD)
                            {
                                foreach (var item in more)
                                {
                                    MessageList.Add(item);
                                }
                            }
                            else
                            {                                
                                for (int i = 0; i < more.Count; i++)
                                {
                                    MessageList.Insert(i, more[i]);
                                }
                            }
                        }
                        if(MediateClass.isBtInbox)
                            await NewMessage();
                    }
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Message Inbox!").ShowAsync();
            }
            finally
            {
                isResponsed = false;
            }
        }

        public async Task NewMessage()
        {
            //int userId = MediateClass.UserVM.UserInfo.UserId;
            //int ownerId = MediateClass.KiotVM.SelectedStore.OwnerId;

            //MessageInbox message = new MessageInbox(userId, ownerId);
            //JToken body = JToken.FromObject(message);
            //JObject result = new JObject();

            //try
            //{
            //    if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
            //    {
            //        if (!isRequested)
            //        {
            //            isRequested = true;
            //            var response = await App.MobileService.InvokeApiAsync("MessageInboxes", body, HttpMethod.Post, null);
            //            if (response.ToString() != "{}")
            //            {
            //                message = response.ToObject<MessageInbox>();                                                       
            //                if (MessageList.Count(x => x.MessageId == message.MessageId) == 0)
            //                    MessageList.Insert(0, message);
            //                MessageSelected = MessageList[MessageList.Count - 1];
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    await new MessageDialog(ex.Message.ToString(), "Inbox Detail!").ShowAsync();
            //}
            //finally
            //{
            //    isRequested = false;
            //    MediateClass.isBtInbox = false;
            //}
        }

    }
}
