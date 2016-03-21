using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quobject.SocketIoClientDotNet.Client;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using PayBay.Utilities.Common;
using PayBay.Model;

namespace PayBay.Services.MobileServices.InboxSocketIO
{
    public class InboxIO
    {
        private static InboxIO m_Instance = null;
        private static string HostURL = "http://immense-reef-32079.herokuapp.com/";

        public static InboxIO Instance
        {
            get
            {
                return m_Instance ?? (m_Instance = new InboxIO(HostURL, 0));
            }

            protected set
            {
                m_Instance = value;
            }
        }
        private Socket _socket;
        //private String _customerID;
        private ObservableCollection<MessageInbox> _messageList;
        //private String receivedMessage;
        MessageInbox receivedMessage;

        public ObservableCollection<MessageInbox> MessageList
        {
            get
            {
                return _messageList;
            }

            set
            {                
                _messageList = value;
            }
        }

        public InboxIO(String hostURL, int portNumber)
        {
            _messageList = new ObservableCollection<MessageInbox>();
            receivedMessage = new MessageInbox();
            if (portNumber > 0)
                _socket = IO.Socket(hostURL + ":" + portNumber);
            else
                _socket = IO.Socket(hostURL);

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

        private void updateMessageListUponReceivingMessage(object sender, object e)
        {
            if (this != null)
            {
                if (receivedMessage != null)
                {
                    _messageList.Add(receivedMessage);
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
                
        public void sendMessage(string message)
        {
            if (MediateClass.KiotVM != null && MediateClass.UserVM != null)
            {
                int receiverID = MediateClass.KiotVM.SelectedStore.OwnerId;
                int userId = MediateClass.UserVM.UserInfo.UserId;
                string name = MediateClass.UserVM.UserInfo.Avatar;
                string avatar = MediateClass.UserVM.UserInfo.Avatar;
                DateTime inboxDate = DateTime.Now;

                MessageInbox inbox = new MessageInbox
                {
                    UserId = receiverID,
                    OwnerId = userId,
                    UserName = name,
                    Avatar = avatar,
                    InboxDate = inboxDate,
                    Content = message
                };

                JObject jsonRequest = JObject.FromObject(inbox);
                Debug.WriteLine("JSON: " + jsonRequest);
                _socket.Send(jsonRequest);
                _messageList.Add(inbox);
            }
        }

    }
}
