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

namespace PayBay.Services.MobileServices.InboxSocketIO
{
    public class InboxIO
    {
        private Socket _socket;
        private String _customerID;
        private ObservableCollection<String> _messageList;
        private String receivedMessage;

        public ObservableCollection<string> MessageList
        {
            get
            {
                return _messageList;
            }

            set
            {
                if (Equals(value, _messageList)) return;
                _messageList = value;
            }
        }

        public InboxIO(String hostURL, int portNumber)
        {
            MessageList = new ObservableCollection<String>();
            if (portNumber > 0)
                _socket = IO.Socket(hostURL + ":" + portNumber);
            else
                _socket = IO.Socket(hostURL);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += updateMessageListUponReceivingMessage;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            timer.Start();

            _socket.On(Socket.EVENT_CONNECT, () =>
            {
                _socket.Emit("testwp", "hi");

            });

            _socket.On(Socket.EVENT_MESSAGE, (data) =>
            {
                JObject received = (JObject)data;
                receivedMessage = received.GetValue("CustomerID") + ": " + received.GetValue("msg");
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

        public void registerClient(String customerID)
        {
            _customerID = customerID;
            _socket.Emit("storeMyID", _customerID);
        }

        public void sendMessage(String receiverID, String message)
        {
            JObject jsonRequest = JObject.FromObject(new { hisId = receiverID, msg = message });
            Debug.WriteLine("JSON: " + jsonRequest);
            _socket.Send(jsonRequest);
            _messageList.Add(_customerID + ": " + message);
        }


    }
}
