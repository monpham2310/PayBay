using PayBay.Utilities.Common;
using PayBay.ViewModel.MarketGroup;
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

namespace PayBay.View.TopFunctionGroup.Manage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductManagePage : Page
    {
        private KiosViewModel KiotVm => (KiosViewModel)DataContext;
        private ProductViewModel ProductVm => (ProductViewModel)svProduct.DataContext;

        public ProductManagePage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(KiotVm != null && ProductVm != null)
            {
                ProductVm.LoadProductsOfStoreOwner(TYPEGET.START);
                KiotVm.GetStoresOfOwner();
            }
        }

        private void svProduct_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if(svProduct.VerticalOffset == 0)
            {
                if (ProductVm != null)
                    ProductVm.LoadProductsOfStoreOwner(TYPEGET.MORE, TYPE.NEW);
            }
            else if(svProduct.VerticalOffset >= svProduct.ScrollableHeight)
            {
                if (ProductVm != null)
                    ProductVm.LoadProductsOfStoreOwner(TYPEGET.MORE);
            }
        }

        private void gvProduct_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if(gvProduct.SelectedItem != null)
            {

            }
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {

        }
                
    }
}
