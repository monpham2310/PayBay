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
        private Socket _socket;        
        private ObservableCollection<MessageInbox> _messageList;       
        MessageInbox receivedMessage;
        private int receiverID = -1;
        private string HostURL = "http://immense-reef-32079.herokuapp.com/";
        private int portNumber = 0;
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

        public MessageInboxViewModel()
        {
            MessageList = new ObservableCollection<MessageInbox>();
            receivedMessage = new MessageInbox();
            if (portNumber > 0)
                _socket = IO.Socket(HostURL + ":" + portNumber);
            else
                _socket = IO.Socket(HostURL);

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
                receiverID = receivedMessage.UserID;
            });

        }

        private void updateMessageListUponReceivingMessage(object sender, object e)
        {
            if (this != null)
            {
                if (receivedMessage != null)
                {
                    MessageList.Add(receivedMessage);
                    receivedMessage = null;
                }
            }
        }

        public void registerClient()
        {
            if (MediateClass.UserVM.UserInfo != null)
            {
                _socket.Emit("storeMyID", MediateClass.UserVM.UserInfo.UserId);
            }
        }

        public async Task<bool> sendMessage(string message)
        {
            try {
                if (MediateClass.UserVM != null)
                {
                    receiverID = (receiverID == -1) ? MediateClass.KiotVM.SelectedStore.OwnerId : receiverID;
                    int userId = MediateClass.UserVM.UserInfo.UserId;
                    string name = MediateClass.UserVM.UserInfo.Username;
                    string avatar = MediateClass.UserVM.UserInfo.Avatar;
                    DateTime inboxDate = DateTime.Now;

                    MessageInbox inbox = new MessageInbox
                    {
                        UserID = receiverID,
                        OwnerId = userId,
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

    }
}
