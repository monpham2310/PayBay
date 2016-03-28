using PayBay.Model;
using PayBay.Utilities.Common;
using PayBay.ViewModel.HomePageGroup;
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
    public sealed partial class SaleManagePage : Page
    {
        private KiosViewModel KiotVm => (KiosViewModel)DataContext;
        private AdvertiseViewModel AdVm => (AdvertiseViewModel)svSales.DataContext;

        public SaleManagePage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(KiotVm != null && AdVm != null)
            {                
                AdVm.GetSaleOfOwner(TYPEGET.START);
                KiotVm.GetStoresOfOwner();
            }
        }

        private void svSales_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if(svSales.VerticalOffset == 0)
            {
                if (AdVm != null)
                    AdVm.GetSaleOfOwner(TYPEGET.MORE, TYPE.NEW);
            }
            else if(svSales.VerticalOffset >= svSales.ScrollableHeight)
            {
                if (AdVm != null)
                    AdVm.GetSaleOfOwner(TYPEGET.MORE);
            }
        }

        private void gvSales_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if(gvSales.SelectedItem != null)
            {
                AdVm.SelectedSale = (AdvertiseItem)gvSales.SelectedItem;
                AdvertiseViewModel.isUpdate = true;
                Frame.Navigate(typeof(AddSalePage), NavigationMode.Forward);
            }
        }

        private void btnAddSale_Click(object sender, RoutedEventArgs e)
        {
            AdvertiseViewModel.isUpdate = false;
            Frame.Navigate(typeof(AddSalePage), NavigationMode.Forward);
        }
    }
}
