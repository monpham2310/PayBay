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
    public sealed partial class MarketManagePage : Page
    {
        private MarketViewModel MarketVm => (MarketViewModel)svMarket.DataContext;

        public MarketManagePage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (MarketVm != null)
                MarketVm.LoadMoreMarket(TYPEGET.START);
        }

        private void svMarket_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            //scroll at bottom
            if (svMarket.VerticalOffset >= svMarket.ScrollableHeight)
            {
                if (MarketVm != null)
                {
                    MarketVm.LoadMoreMarket(TYPEGET.MORE);                    
                }
            }
            //scroll at top
            else if (svMarket.VerticalOffset == 0)
            {
                if (MarketVm != null)
                {
                    MarketVm.LoadMoreMarket(TYPEGET.MORE, TYPE.NEW);                    
                }
            }
        }
                
        private void gvMarket_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (gvMarket.SelectedItem != null)
            {
                MarketVm.SelectedMarket = (Market)gvMarket.SelectedItem;
                MarketViewModel.isUpdate = true;
                Frame.Navigate(typeof(AddMarketPage), NavigationMode.Forward);
            }
        }

        private void btnAddMarket_Click(object sender, RoutedEventArgs e)
        {
            MarketViewModel.isUpdate = false;
            Frame.Navigate(typeof(AddMarketPage), NavigationMode.Forward);
        }
                
    }
}
