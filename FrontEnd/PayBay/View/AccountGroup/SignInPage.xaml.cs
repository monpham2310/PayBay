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

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			UsernameTextBox.Focus(FocusState.Programmatic);
		}

		private void ExitHyperlinkButton_Click(object sender, RoutedEventArgs e)
		{
			((Popup)Frame.Parent).IsOpen = false;
		}
        
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            StartPage startPage = new StartPage();       
            try
            {
                await Vm.LoginAccount(username, password);
                ((Popup)Frame.Parent).IsOpen = false;
                MediateClass.StartPage.UserLoginSucceed();                
                await new MessageDialog("Login is successful!", "Notification!").ShowAsync();
            }
            catch (Exception ex)
            {
                await new MessageDialog("Login is not successful.Please try again!","Notification!").ShowAsync();
            }
        }

		private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(CreateAccountPage));
		}
	}

}
