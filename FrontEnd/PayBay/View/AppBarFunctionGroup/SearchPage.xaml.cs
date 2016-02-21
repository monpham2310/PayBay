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
using PayBay.ViewModel.ProductGroup;
using PayBay.Model;
using Windows.UI.Popups;

namespace PayBay.View.AppBarFunctionGroup
{
    public sealed partial class SearchPage : Page
    {

        private MarketViewModel MarketVm => (MarketViewModel)marketListBox.DataContext;
        private ProductViewModel ProductVm => (ProductViewModel)productListBox.DataContext;
        
        public SearchPage()
        {
            InitializeComponent();                        
        }
                
        private void BackHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Grid grid = sender as Grid;

            grid.Height = grid.ActualWidth;
        }

        private async void btSearchProduct_Click(object sender, RoutedEventArgs e)
        {
            string name = txtSearchProduct.Text;
            if (ProductVm != null)
            {
                if(!string.IsNullOrEmpty(txtSearchProduct.Text))
                    await ProductVm.GetProductFollowName(name);
            }
        }

        private async void btSearchMarket_Click(object sender, RoutedEventArgs e)
        {
            string name = txtSearchMarket.Text;
            if(MarketVm != null)
            {
                if(!string.IsNullOrEmpty(txtSearchMarket.Text))
                    await MarketVm.GetMarketFollowName(name);
            }
        }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            //scroll at bottom
            if(scrollvMarket.VerticalOffset >= scrollvMarket.ScrollableHeight)
            {
                if (MarketVm != null)
                {
                    if (string.IsNullOrEmpty(txtSearchMarket.Text))
                        MarketVm.LoadMoreMarket();
                    else
                        MarketVm.LoadMoreMarket(txtSearchMarket.Text);
                }
            }
            //scroll at top
            else if(scrollvMarket.VerticalOffset == 0)
            {

            }
        }

        private void scrollvProduct_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            //scroll at bottom
            if (scrollvProduct.VerticalOffset >= scrollvProduct.ScrollableHeight)
            {
                if (ProductVm != null)
                {
                    if (string.IsNullOrEmpty(txtSearchProduct.Text))
                        ProductVm.LoadMoreProduct();
                    else
                        ProductVm.LoadMoreProduct(txtSearchProduct.Text);
                }
            }
            //scroll at top
            else if (scrollvProduct.VerticalOffset == 0)
            {

            }
        }
    }
}
