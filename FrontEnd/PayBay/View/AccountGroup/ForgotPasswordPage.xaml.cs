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
    public sealed partial class ForgotPasswordPage : Page
    {
        private UserInfoViewModel AccountVm => (UserInfoViewModel)DataContext;

        public ForgotPasswordPage()
        {
            this.InitializeComponent();
        }

        private async void Reset()
        {
            string email = EmailTextBox.Text;

            if (!string.IsNullOrEmpty(email))
            {
                await AccountVm.ResetPasswordOfUser(email);
            }
            else
            {
                await new MessageDialog("Please type your email!", "Notification!").ShowAsync();
            }
            pgrAccount.IsActive = false;
            gridAccount.IsHitTestVisible = true;
            gridAccount.Opacity = 1.0;
            Frame.GoBack();
        }

        private void btReset_Click(object sender, RoutedEventArgs e)
        {
            pgrAccount.IsActive = true;
            gridAccount.IsHitTestVisible = false;
            gridAccount.Opacity = 0.7;

            Reset();
        }

        private void BackHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
