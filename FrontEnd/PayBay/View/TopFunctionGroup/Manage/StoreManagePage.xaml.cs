using PayBay.Model;
using PayBay.Utilities.Common;
using PayBay.ViewModel.MarketGroup;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PayBay.View.TopFunctionGroup.Manage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StoreManagePage : Page
    {
        private KiosViewModel KiotVm => (KiosViewModel)svStore.DataContext;
        private MarketViewModel MarketVm => (MarketViewModel)DataContext;

        public StoreManagePage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(KiotVm != null)
            {
                KiotVm.GetStoresOfOwner();
            }
            if(MarketVm != null)
                MarketVm.GetAllMarket();
        }

        private void svStore_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {

        }
                
        private void btnAddStore_Click(object sender, RoutedEventArgs e)
        {
            KiosViewModel.isUpdate = false;
            Frame.Navigate(typeof(StoreRegisterPage), NavigationMode.Forward);
        }

        private void lbStore_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if(lbStore.SelectedItem != null)
            {
                KiosViewModel.isUpdate = true;
                KiotVm.Store = (Kios)lbStore.SelectedItem;
                Frame.Navigate(typeof(StoreRegisterPage), NavigationMode.Forward);
            }
        }
    }
}
