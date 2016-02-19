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
using PayBay.Utilities.Handler;

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
                
        public async Task LoginAccount(string mail,string password)
        {            
            byte[] pwd = Functions.GetBytes(password);
            Account account = new Account(mail, pwd);
            JToken body = JToken.FromObject(account);
            IDictionary<string, string> argument = new Dictionary<string, string>
            {
                {"type" , "Test"}
            };
            var result = await App.MobileService.InvokeApiAsync("Users", body, HttpMethod.Post, argument);
            JObject user = JObject.Parse(result.ToString());
            UserInfo = user.ToObject<UserInfo>();
             
            DelegateHandler.RemoteFuncArg = new DelegateHandler.FuncArgCallHandler(MediateClass.StartVM.InitializeUserLogin);
            DelegateHandler.RemoteFuncArg(UserInfo.Avatar, UserInfo.Username);        
        }

    }
}
