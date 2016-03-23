using Newtonsoft.Json.Linq;
using PayBay.Utilities.Common;
using PayBay.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using PayBay.Services.MobileServices.PaybayNotification;
using Windows.Networking.PushNotifications;
using PayBay.Services.MobileServices.InboxSocketIO;
using PayBay.ViewModel.InboxGroup;
using Quobject.SocketIoClientDotNet.Client;

namespace PayBay.ViewModel.AccountGroup
{
    public class UserInfoViewModel : BaseViewModel
    {
        private ObservableCollection<UserInfo> _userInfoList;
        private UserInfo _userInfo;
                                                
        public ObservableCollection<UserInfo> UserAccountList
        {
            get
            {
                return _userInfoList;
            }

            set
            {
                _userInfoList = value;
                OnPropertyChanged();
            }
        }

        public UserInfo UserInfo
        {
            get
            {
                return _userInfo;
            }

            set
            {
                _userInfo = value;
                OnPropertyChanged();                          
            }
        }
                
        public UserInfoViewModel()
        {            
            MediateClass.UserVM = this;
        }
                
        public async Task<bool> LoginAccount(string mail,string password)
        {            
            byte[] pwd = Functions.GetBytes(password);
            Account account = new Account(mail, pwd);
            JToken body = JToken.FromObject(account);
            _userInfo = new UserInfo();
            IDictionary<string, string> argument = new Dictionary<string, string>
            {
                {"type" , "Test"}
            };
            try
            {
                if (Utilities.Helpers.NetworkHelper.Instance.HasInternetConnection)
                {
                    var result = await App.MobileService.InvokeApiAsync("Users", body, HttpMethod.Post, argument);
                    JObject user = JObject.Parse(result.ToString());
                    UserInfo = user.ToObject<UserInfo>();                    
                    //PaybayPushClient.UploadChannel();
                    PaybayPushClient.UploadChannel(UserInfo.UserId);
                    //if (MediateClass.MessageVM == null)
                    //    MediateClass.MessageVM = new MessageInboxViewModel();               
                    MessageInboxViewModel.registerClient();
                }
            }
            catch (Exception ex)
            {                
                return false;
            }
            return true;               
        }
                
        public async Task ResetPasswordOfUser(string email)
        {
            string newPass = "abcdXYZ123";
            byte[] newPwd = Functions.GetBytes(newPass);

            Account account = new Account(email, newPwd, newPass);
            JToken body = JToken.FromObject(account);

            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"code" , "1"}
            };

            try
            {
                var result = await App.MobileService.InvokeApiAsync("Users", body, HttpMethod.Put, param);
                if (result["ErrCode"].ToString().Equals("1"))
                {
                    await new MessageDialog("Reset password is successful.Please check your email!", "Notification!").ShowAsync();
                }
                else
                {
                    await new MessageDialog("Email is NOT exists!Please check again!", "Notification!").ShowAsync();
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }
        }

        public async Task UserSendMail(GuestMail mail)
        {
            JToken body = JToken.FromObject(mail);
            IDictionary<string, string> param = new Dictionary<string, string>
            {
                {"type" , "mail"}
            };
            try
            {
                var result = await App.MobileService.InvokeApiAsync("UserTypes", body, HttpMethod.Post, param);
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }
        }

    }
}
