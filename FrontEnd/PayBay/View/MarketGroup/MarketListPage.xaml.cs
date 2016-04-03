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
using System.Collections.ObjectModel;
using PayBay.ViewModel.MarketGroup;
using PayBay.Utilities.Common;
using PayBay.Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PayBay.View.MarketGroup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MarketListPage : Page
    {
        private MarketViewModel MarketVm => (MarketViewModel)DataContext;

        public MarketListPage()
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
            if(svMarket.VerticalOffset == 0)
            {
                MarketVm.LoadMoreMarket(TYPEGET.MORE, TYPE.NEW);
            }
            else if (svMarket.VerticalOffset >= svMarket.ScrollableHeight)
            {
                MarketVm.LoadMoreMarket(TYPEGET.MORE);
            }
        }

        private void lvMarket_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if(lvMarket.SelectedItem != null)
            {
                if (MarketVm != null)
                    MarketVm.SelectedMarket = (Market)lvMarket.SelectedItem;
                Frame.Navigate(typeof(MarketPage), NavigationMode.Forward);
            }  
        }
    }
}
