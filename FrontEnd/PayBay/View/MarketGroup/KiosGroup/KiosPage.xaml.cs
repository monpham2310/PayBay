using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PayBay.ViewModel.MarketGroup;
using PayBay.Utilities.Common;
using PayBay.View.TopFunctionGroup;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PayBay.View.MarketGroup.KiosGroup
{
    public sealed partial class KiosPage : Page
    {
        private KiosViewModel KiosVm => (KiosViewModel)DataContext;       

        public KiosPage()
        {
            this.InitializeComponent();
            MediateClass.KiosPage = this;
        }

        private void btCallStore_Click(object sender, RoutedEventArgs e)
        {
            Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI(KiosVm.SelectedStore.Phone, KiosVm.SelectedStore.StoreName);
        }

        private async void btInbox_Click(object sender, RoutedEventArgs e)
        {         
            if(KiosVm.SelectedStore.OwnerId != MediateClass.UserVM.UserInfo.UserId)   
                Frame.Navigate(typeof(InboxPage), NavigationMode.Forward);            
            else
                await new MessageDialog("You can not send message to your store!", "Message").ShowAsync();
        }

		private void linkHide_Click(object sender, RoutedEventArgs e)
		{
			//if (linkHide.Content.ToString() == "Hide")
			//{
			//	gridInfo.Visibility = Visibility.Collapsed;
			//	linkHide.Content = "Show";
			//}
			//else
			//{
			//	gridInfo.Visibility = Visibility.Visible;
			//	linkHide.Content = "Hide";
			//}
		}

        private void btnToggleHeader_Click(object sender, RoutedEventArgs e)
        {
            if (gridHeader.Visibility == Visibility.Visible)
            {
                gridHeader.Visibility = Visibility.Collapsed;
                btnToggleHeader.Content = "\uE019";
            }
            else
            {
                gridHeader.Visibility = Visibility.Visible;
                btnToggleHeader.Content = "\uE018";
            }
        }
    }
}