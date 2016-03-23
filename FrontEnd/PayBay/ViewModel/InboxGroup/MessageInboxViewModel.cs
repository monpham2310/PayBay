using Newtonsoft.Json.Linq;
using PayBay.Model;
using PayBay.Utilities.Common;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace PayBay.ViewModel.InboxGroup
{
    public class MessageInboxViewModel : BaseViewModel
    {
        private static Socket _socket;        
        private ObservableCollection<MessageInbox> _messageList;       
        private MessageInbox receivedMessage;
        private int _userChated;

        private ObservableCollection<MessageInbox> _messageLstHistory;

        private int receiverID = -1;
        private static string HostURL = "http://immense-reef-32079.herokuapp.com/";
        private static bool isResponsed = false;
        private static bool isSended = false;

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
                
        public ObservableCollection<MessageInbox> MessageLstHistory
        {
            get
            {
                return _messageLstHistory;
            }

            set
            {
                _messageLstHistory = value;
                OnPropertyChanged();
            }
        }

        public int UserChated
        {
            get
            {
                return _userChated;
            }

            set
            {
                _userChated = value;
                OnPropertyChanged();
            }
        }

        public static void registerClient()
        {
            _socket = IO.Socket(HostURL);
            if (MediateClass.UserVM.UserInfo != null)
            {
                _socket.Emit("storeMyID", MediateClass.UserVM.UserInfo.UserId);
            }
        }

        public void InitSocket()
        {            
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += updateMessageListUponReceivingMessage;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            _socket.On(Socket.EVENT_CONNECT, () =>
            {
                _socket.Emit("testwp", "hi");
            });

            _socket.On(Socket.EVENT_MESSAGE, (data) =>
            {
                JObject received = (JObject)data;
                receivedMessage = received.ToObject<MessageInbox>();
            });
        }

        public MessageInboxViewModel()
        {
            MediateClass.MessageVM = this;
            _messageList = new ObservableCollection<MessageInbox>();
            _userChated = -1;                       
            InitSocket();            
            LoadMessageList();
        }

        private void updateMessageListUponReceivingMessage(object sender, object e)
        {
            if (this != null)
            {
                if (receivedMessage != null)
                {
                    receiverID = receivedMessage.OwnerID;
                    Debug.WriteLine("RECEIVED: " + receivedMessage.Content);
                    MessageList.Add(receivedMessage);
                    MediateClass.InboxPage.ScrollToBottom();
                    receivedMessage = null;
                }
            }
        }
                
        public async Task<bool> sendMessage(string message)
        {
            try {
                if (MediateClass.UserVM != null)
                {
                    receiverID = (receiverID == -1 || receiverID == 0) ? UserChated : receiverID;
                    int userId = MediateClass.UserVM.UserInfo.UserId;
                    string name = MediateClass.UserVM.UserInfo.Username;
                    string avatar = MediateClass.UserVM.UserInfo.Avatar;
                    DateTime inboxDate = DateTime.Now;

                    MessageInbox inbox = new MessageInbox
                    {
                        UserID = receiverID,
                        OwnerID = userId,
                        UserName = name,
                        Avatar = avatar,
                        InboxDate = inboxDate,
                        Content = message
                    };
                    if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                    {
                        if (!isSended)
                        {
                            isSended = true;                                                                                    
                            JToken body = JToken.FromObject(inbox);
                            var result = await App.MobileService.InvokeApiAsync("MessageInboxes", body, HttpMethod.Post, null);
                            if (result.ToString() != "{}")
                            {
                                inbox = result.ToObject<MessageInbox>();                                
                                JObject jsonRequest = JObject.FromObject(inbox);
                                Debug.WriteLine("JSON: " + jsonRequest);
                                _socket.Send(jsonRequest);
                                MessageList.Add(inbox);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Message!").ShowAsync();
                return false;
            }
            finally
            {
                isSended = false;
            }
            return true;
        }

        public async void LoadMessageList()
        {
            int userId = MediateClass.UserVM.UserInfo.UserId;
            
            IDictionary<string, string> param = new Dictionary<string, string>
            {                
                {"userId" , userId.ToString()}                
            };

            await GetData(param);
        }

        public async void LoadInboxHitory(TYPEGET typeGet, TYPE type = TYPE.OLD)
        {          
            int userID = MediateClass.UserVM.UserInfo.UserId;
            int userChat = UserChated;
            int msgId = -1;
            if(typeGet == TYPEGET.MORE)
            {
                if(type == TYPE.OLD)
                {
                    msgId = MessageList.Min(x => x.MessageId);
                }
                else
                {
                    msgId = MessageList.Max(x => x.MessageId);
                }
            }

            MessageInfo msg = new MessageInfo(msgId, userID, userChat, type);

            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"type" , "1"}
            };
                        
            await SendData(typeGet, type, msg, param);
        }

        private async Task GetData(IDictionary<string, string> param)
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
                        MessageLstHistory = response.ToObject<ObservableCollection<MessageInbox>>();                        
                    }
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Message!").ShowAsync();
            }
            finally
            {
                isResponsed = false;
            }
        }

        private async Task SendData(TYPEGET typeGet, TYPE type, MessageInfo param, IDictionary<string,string> arg)
        {
            try
            {
                JToken body = JToken.FromObject(param);
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    if (!isResponsed)
                    {
                        isResponsed = true;
                        JToken result = await App.MobileService.InvokeApiAsync("MessageInboxes", body, HttpMethod.Get, arg);
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
                                for (int i = 0; i < more.Count; i++)
                                {
                                    MessageList.Insert(i, more[i]);
                                }
                            }
                            else
                            {                                
                                foreach (var item in more)
                                {
                                    MessageList.Add(item);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Message!").ShowAsync();
            }
            finally
            {
                isResponsed = false;
            }
        }

    }
}
