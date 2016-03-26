using Microsoft.WindowsAzure.MobileServices;
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
using PayBay.Utilities.Helpers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PayBay.View.AccountGroup
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class SignInPage : Page
	{
        public UserInfoViewModel UserVm => (UserInfoViewModel)DataContext;
        // Define a member variable for storing the signed-in user. 
        private MobileServiceUser user;

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
                btSignin.IsEnabled = false;           
				bool check = await UserVm.LoginAccount(mail, password);
				((Popup)Frame.Parent).IsOpen = false;
                                                
                if (check)
                {                    
                    await new MessageDialog("Login is successful!", "Notification!").ShowAsync();
                    MediateClass.StartPage.UserLoginSucceed();

					if (chkRemember.IsChecked == true)
					{
						SettingsHelper.SetSetting("Remember", chkRemember.IsChecked);
						SettingsHelper.SetSetting<String>("Mail", mail);
						SettingsHelper.SetSetting<String>("Password", password);
					}
				}
                else
                    await new MessageDialog("Login is NOT successful!", "Notification!").ShowAsync();
                btSignin.IsEnabled = true;				
			}
			else
			{
				await new MessageDialog("Please type your email and your password!", "Notification").ShowAsync();
			}

			ToggleProgressRing();
		}

		private void ToggleProgressRing()
		{
			pgrAccount.IsActive = !pgrAccount.IsActive;
			gridAccount.IsHitTestVisible = !gridAccount.IsHitTestVisible;
			gridAccount.Opacity = (gridAccount.Opacity == 1.0) ? 0.7 : 1.0;
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
			ToggleProgressRing();
			Login();            
        }

		private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(CreateAccountPage));
		}

		private void btForgotPass_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(ForgotPasswordPage));
		}

		private void Page_KeyUp(object sender, KeyRoutedEventArgs e)
		{
			if (e.Key == Windows.System.VirtualKey.Enter)
			{
				ToggleProgressRing();
				Login();
			}
		}

		//private async void btLoginFb_Click(object sender, RoutedEventArgs e)
		//{
		//    if (await AuthenticateAsync())
		//    {
		//        PaybayPushClient.UploadChannel();
		//        // Hide the login button and load items from the mobile app.
		//        btLoginFb.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
		//        //await InitLocalStoreAsync(); //offline sync support.               
		//    }
		//}

		// Define a method that performs the authentication process
		// using a Facebook sign-in. 
		private async System.Threading.Tasks.Task<bool> AuthenticateAsync()
        {
            string message;
            bool success = false;
            try
            {
                // Change 'MobileService' to the name of your MobileServiceClient instance.
                // Sign-in using Facebook authentication.
                
                user = await App.MobileService
                    .LoginAsync(MobileServiceAuthenticationProvider.Facebook, true);
                message =
                    string.Format("You are now signed in - {0}", user.UserId);

                success = true;
            }
            catch (Exception ex)
            {
                message = "You must log in. Login Required";
            }

            var dialog = new MessageDialog(message);
            dialog.Commands.Add(new UICommand("OK"));
            await dialog.ShowAsync();
            return success;
        }

		//private async System.Threading.Tasks.Task<bool> AuthenticateAsync()
		//{
		//    string message;
		//    bool success = false;

		//    // This sample uses the Facebook provider.
		//    var provider = MobileServiceAuthenticationProvider.Facebook;

		//    // Use the PasswordVault to securely store and access credentials.
		//    PasswordVault vault = new PasswordVault();
		//    PasswordCredential credential = null;

		//    try
		//    {
		//        // Try to get an existing credential from the vault.
		//        credential = vault.FindAllByResource(provider.ToString()).FirstOrDefault();
		//    }
		//    catch (Exception)
		//    {
		//        // When there is no matching resource an error occurs, which we ignore.
		//    }

		//    if (credential != null)
		//    {
		//        // Create a user from the stored credentials.
		//        user = new MobileServiceUser(credential.UserName);
		//        credential.RetrievePassword();
		//        user.MobileServiceAuthenticationToken = credential.Password;

		//        // Set the user from the stored credentials.
		//        App.MobileService.CurrentUser = user;

		//        // Consider adding a check to determine if the token is 
		//        // expired, as shown in this post: http://aka.ms/jww5vp.

		//        success = true;
		//        message = string.Format("Cached credentials for user - {0}", user.UserId);
		//    }
		//    else
		//    {
		//        try
		//        {
		//            // Login with the identity provider.
		//            user = await App.MobileService
		//                .LoginAsync(provider);

		//            // Create and store the user credentials.
		//            credential = new PasswordCredential(provider.ToString(),
		//                user.UserId, user.MobileServiceAuthenticationToken);
		//            vault.Add(credential);

		//            success = true;
		//            message = string.Format("You are now logged in - {0}", user.UserId);
		//        }
		//        catch (MobileServiceInvalidOperationException)
		//        {
		//            message = "You must log in. Login Required";
		//        }
		//    }

		//    var dialog = new MessageDialog(message);
		//    dialog.Commands.Add(new UICommand("OK"));
		//    await dialog.ShowAsync();

		//    return success;
		//}

	}

}
