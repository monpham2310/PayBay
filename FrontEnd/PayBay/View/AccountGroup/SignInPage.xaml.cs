using PayBay.Common;
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
            try
            {
                await Vm.LoginAccount(username, password);
                ((Popup)Frame.Parent).IsOpen = false;
                MediateClass.StartPage.UserLoginSucceed();
            }
            catch (Exception ex)
            {
                MessageDialog dialog = new MessageDialog("Login isn't successful!");
                            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                                async () => { await dialog.ShowAsync(); });
            }
        }

    }
}
