using Newtonsoft.Json.Linq;
using PayBay.Common;
using PayBay.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

        static byte[] GetBytes(string str)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            //byte[] bytes = new byte[str.Length * sizeof(char)];
            //System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            string str = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            //char[] chars = new char[bytes.Length / sizeof(char)];
            //System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            //return new string(chars);
            return str;
        }

        public async Task LoginAccount(string username,string password)
        {            
            byte[] pwd = GetBytes(password);
            Account account = new Account(username, pwd);
            JToken body = JToken.FromObject(account);
            IDictionary<string, string> argument = new Dictionary<string, string>
            {
                {"type" , "Test"}
            };
            var result = await App.MobileService.InvokeApiAsync("Users", body, HttpMethod.Post, argument);
            JObject user = JObject.Parse(result.ToString());
            UserInfo = user.ToObject<UserInfo>();
            MediateClass.StartVM.UserLogin = new UserSignin(UserInfo.Avatar, UserInfo.Username);            
        }

    }
}
