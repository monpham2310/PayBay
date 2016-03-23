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

        private void btInbox_Click(object sender, RoutedEventArgs e)
        {
            if (MediateClass.MessageVM != null && MediateClass.KiotVM.SelectedStore != null)
            {
                MediateClass.MessageVM.UserChated = MediateClass.KiotVM.SelectedStore.OwnerId;
                MediateClass.MessageVM.LoadInboxHitory(TYPEGET.START);
            }
            Frame.Navigate(typeof(InboxPage), NavigationMode.Forward);            
        }

		private void linkHide_Click(object sender, RoutedEventArgs e)
		{
			if (linkHide.Content.ToString() == "Hide")
			{
				gridInfo.Visibility = Visibility.Collapsed;
				linkHide.Content = "Show";
			}
			else
			{
				gridInfo.Visibility = Visibility.Visible;
				linkHide.Content = "Hide";
			}
		}
	}
}