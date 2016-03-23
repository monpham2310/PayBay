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
using Windows.UI.Core;
using PayBay.ViewModel.MarketGroup;
using PayBay.ViewModel.ProductGroup;
using PayBay.View.MarketGroup;
using PayBay.Model;
using Windows.UI.Popups;
using PayBay.Utilities.Common;
using PayBay.ViewModel.HomePageGroup;

namespace PayBay.View.AppBarFunctionGroup
{
    public sealed partial class SearchPage : Page
    {

        private MarketViewModel MarketVm => (MarketViewModel)scrollvMarket.DataContext;
        private AdvertiseViewModel SaleVm => (AdvertiseViewModel)scrollvSale.DataContext;
        private ProductViewModel ProductVm => (ProductViewModel)scrollvProducts.DataContext;
        
        public SearchPage() 
        {
            InitializeComponent();
            //SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;            
		}

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (MediateClass.MarketVM != null)
                MediateClass.MarketVM.LoadMoreMarket(TYPEGET.START);
            if (MediateClass.AdvertiseVM != null)
                MediateClass.AdvertiseVM.LoadMoreSale(TYPEGET.START);
            if (MediateClass.ProductVM != null)
                MediateClass.ProductVM.LoadMoreProduct(TYPEGET.START);
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

        private void btSearchSale_Click(object sender, RoutedEventArgs e)
        {
            string name = txtSearchSale.Text;
            if (SaleVm != null)
            {
                if (!string.IsNullOrEmpty(txtSearchSale.Text))
                    SaleVm.LoadMoreSale(name,TYPEGET.START);
                else
                    SaleVm.LoadMoreSale(TYPEGET.START);
            }
        }

        private void btSearchProduct_Click(object sender, RoutedEventArgs e)
        {
            string name = txtSearchProduct.Text;
            if (ProductVm != null)
            {
                if (!string.IsNullOrEmpty(txtSearchProduct.Text))
                    ProductVm.LoadMoreProduct(name, TYPEGET.START);
                else
                    ProductVm.LoadMoreProduct(TYPEGET.START);
            }
        }

        private void btSearchMarket_Click(object sender, RoutedEventArgs e)
        {
            string name = txtSearchMarket.Text;
            if(MarketVm != null)
            {
                if (!string.IsNullOrEmpty(txtSearchMarket.Text))
                    MarketVm.LoadMoreMarket(name, TYPEGET.START);
                else
                    MarketVm.LoadMoreMarket(TYPEGET.START);
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
                        MarketVm.LoadMoreMarket(TYPEGET.MORE);
                    else
                        MarketVm.LoadMoreMarket(txtSearchMarket.Text, TYPEGET.MORE);
                }
            }
            //scroll at top
            else if(scrollvMarket.VerticalOffset == 0)
            {
                if (MarketVm != null)
                {
                    if (string.IsNullOrEmpty(txtSearchSale.Text))
                        MarketVm.LoadMoreMarket(TYPEGET.MORE, TYPE.NEW);
                    else
                        MarketVm.LoadMoreMarket(txtSearchSale.Text, TYPEGET.MORE, TYPE.NEW);
                }
            }
        }

        private void scrollvSale_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            //scroll at bottom
            if (scrollvSale.VerticalOffset >= scrollvSale.ScrollableHeight)
            {
                if (SaleVm != null)
                {
                    if (string.IsNullOrEmpty(txtSearchSale.Text))
                        SaleVm.LoadMoreSale(TYPEGET.MORE);
                    else
                        SaleVm.LoadMoreSale(txtSearchSale.Text, TYPEGET.MORE);
                }
            }
            //scroll at top
            else if (scrollvSale.VerticalOffset == 0)
            {
                if (SaleVm != null)
                {
                    if (string.IsNullOrEmpty(txtSearchSale.Text))
                        SaleVm.LoadMoreSale(TYPEGET.MORE, TYPE.NEW);
                    else
                        SaleVm.LoadMoreSale(txtSearchSale.Text, TYPEGET.MORE, TYPE.NEW);
                }
            }
        }

		private void marketListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
            if (MarketVm != null)
                MarketVm.SelectedMarket = (Market)marketListBox.SelectedItem;
			Frame.Navigate(typeof(MarketPage), NavigationMode.Forward);
		}

        private void scrollvProducts_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if(scrollvProducts.VerticalOffset == 0)
            {
                if (ProductVm != null)
                {
                    if (string.IsNullOrEmpty(txtSearchProduct.Text))
                        ProductVm.LoadMoreProduct(TYPEGET.MORE, TYPE.NEW);
                    else
                        ProductVm.LoadMoreProduct(txtSearchProduct.Text, TYPEGET.MORE, TYPE.NEW);
                }
            }
            else if(scrollvProducts.VerticalOffset >= scrollvProducts.ScrollableHeight)
            {
                if (ProductVm != null)
                {
                    if (string.IsNullOrEmpty(txtSearchProduct.Text))
                        ProductVm.LoadMoreProduct(TYPEGET.MORE);
                    else
                        ProductVm.LoadMoreProduct(txtSearchProduct.Text, TYPEGET.MORE);
                }
            }
        }
    }
}
