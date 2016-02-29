using PayBay.Services.MobileServices.PaybayNotification;
using PayBay.Utilities.Common;
using PayBay.View.StartGroup;
using PayBay.ViewModel.AccountGroup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PayBay.View.AccountGroup
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class SignInPage : Page
	{
        public UserInfoViewModel Vm => (UserInfoViewModel)DataContext;       
                
		public SignInPage()
		{
			this.InitializeComponent();
		}

		private async void Login()
		{
			string mail = EmailTextBox.Text;
			string password = PasswordBox.Password;
			if (mail != "" && password != "")
			{
				try
				{
                    btSignin.IsEnabled = false;           
					bool check = await Vm.LoginAccount(mail, password);
					((Popup)Frame.Parent).IsOpen = false;

                    //PaybayPushClient.UploadChannel();

                    MediateClass.StartPage.UserLoginSucceed();
					if(check)
					    await new MessageDialog("Login is successful!", "Notification!").ShowAsync();
                    btSignin.IsEnabled = true;
				}
				catch (Exception ex)
				{
					await new MessageDialog("Login is NOT successful!\nMaybe wrong email or password,please try again!", "Notification!").ShowAsync();
				}
			}
			else
			{
				await new MessageDialog("Please type your email and your password!", "Notification").ShowAsync();
			}
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			EmailTextBox.Focus(FocusState.Programmatic);
		}

		private void ExitHyperlinkButton_Click(object sender, RoutedEventArgs e)
		{
			((Popup)Frame.Parent).IsOpen = false;
		}
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
			Login();
        }

		private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(CreateAccountPage));
		}
        		
	}

}
