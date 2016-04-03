using PayBay.Utilities.Common;
using PayBay.View.MarketGroup;
using PayBay.ViewModel.HomePageGroup;
using PayBay.ViewModel.ProductGroup;
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

namespace PayBay.View.TopFunctionGroup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        private AdvertiseViewModel AdVm => (AdvertiseViewModel)gridImageSale.DataContext;
        private ProductStatisticViewModel ProStatisticVm => (ProductStatisticViewModel)DataContext;

        public HomePage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (AdVm != null)
                AdVm.InitializeDataFromDB();
            if (ProStatisticVm != null)
            {
                ProStatisticVm.GetBestSaleProductList(TYPEGET.START);
                ProStatisticVm.GetNewProductList(TYPEGET.START);
            }
        }

        private void svBestPro_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if(svBestPro.HorizontalOffset == 0)
            {
                if(ProStatisticVm != null)
                    ProStatisticVm.GetBestSaleProductList(TYPEGET.MORE, TYPE.NEW);
            }
            else if(svBestPro.HorizontalOffset >= svBestPro.ScrollableWidth)
            {
                if (ProStatisticVm != null)
                    ProStatisticVm.GetBestSaleProductList(TYPEGET.MORE);
            }
        }

        private void svNewProduct_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (svNewProduct.HorizontalOffset == 0)
            {
                if (ProStatisticVm != null)
                    ProStatisticVm.GetNewProductList(TYPEGET.MORE, TYPE.NEW);
            }
            else if (svNewProduct.HorizontalOffset >= svNewProduct.ScrollableWidth)
            {
                if (ProStatisticVm != null)
                    ProStatisticVm.GetNewProductList(TYPEGET.MORE);
            }
        }

        private void btnMarket_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MarketListPage), NavigationMode.Forward);
        }
    }
}