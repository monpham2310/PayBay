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

namespace PayBay.View.AppBarFunctionGroup
{
    public sealed partial class SearchPage : Page
    {

        private MarketViewModel MarketVm => (MarketViewModel)scrollvMarket.DataContext;
        private ProductViewModel ProductVm => (ProductViewModel)scrollvProduct.DataContext;
        
        public SearchPage()
        {
            InitializeComponent();
			//SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

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
                if (!string.IsNullOrEmpty(txtSearchProduct.Text))
                    await ProductVm.LoadMoreProduct(name,Functions.TYPEGET.START);
                else
                    await ProductVm.LoadMoreProduct(Functions.TYPEGET.START);
            }
        }

        private async void btSearchMarket_Click(object sender, RoutedEventArgs e)
        {
            string name = txtSearchMarket.Text;
            if(MarketVm != null)
            {
                if (!string.IsNullOrEmpty(txtSearchMarket.Text))
                    await MarketVm.LoadMoreMarket(name, Functions.TYPEGET.START);
                else
                    await MarketVm.LoadMoreMarket(Functions.TYPEGET.START);
            }
        }

        private async void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            //scroll at bottom
            if(scrollvMarket.VerticalOffset >= scrollvMarket.ScrollableHeight)
            {
                if (MarketVm != null)
                {
                    if (string.IsNullOrEmpty(txtSearchMarket.Text))
                        await MarketVm.LoadMoreMarket(Functions.TYPEGET.MORE);
                    else
                        await MarketVm.LoadMoreMarket(txtSearchMarket.Text, Functions.TYPEGET.MORE);
                }
            }
            //scroll at top
            else if(scrollvMarket.VerticalOffset == 0)
            {

            }
        }

        private async void scrollvProduct_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            //scroll at bottom
            if (scrollvProduct.VerticalOffset >= scrollvProduct.ScrollableHeight)
            {
                if (ProductVm != null)
                {
                    if (string.IsNullOrEmpty(txtSearchProduct.Text))
                        await ProductVm.LoadMoreProduct(Functions.TYPEGET.MORE);
                    else
                        await ProductVm.LoadMoreProduct(txtSearchProduct.Text, Functions.TYPEGET.MORE);
                }
            }
            //scroll at top
            else if (scrollvProduct.VerticalOffset == 0)
            {

            }
        }

		private void marketListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
            if (MarketVm != null)
                MarketVm.SelectedMarket = (Market)marketListBox.SelectedItem;
			Frame.Navigate(typeof(MarketPage));
		}
	}
}
