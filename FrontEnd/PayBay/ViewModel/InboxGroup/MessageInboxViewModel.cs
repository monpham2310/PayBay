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
            _messageList = new ObservableCollection<MessageInbox>();
            _messageSelected = new MessageInbox();
            GetMessageLstOfStore(MediateClass.UserVM.UserInfo.UserId);
        }

        public async void GetMessageLstOfStore(int ownerId)
        {
            JArray result = new JArray();
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"ownerId" , ownerId.ToString()}
            };
            try
            {
                var response = await App.MobileService.InvokeApiAsync("MessageInboxes", HttpMethod.Get, param);
                result = JArray.Parse(response.ToString());
                MessageList = result.ToObject<ObservableCollection<MessageInbox>>();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Message Inbox!").ShowAsync();
            }
        }

    }
}
