using PayBay.Model;
using PayBay.Utilities.Common;
using PayBay.View.MarketGroup;
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

namespace PayBay.View.TopFunctionGroup.SuggestionGroup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NearbyPage : Page
    {
        private MarketViewModel MarketVm => (MarketViewModel)DataContext;

        public NearbyPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(MarketVm != null)
            {
                MarketVm.SuggestedMarketList();
            }
        }

        private void lvMarket_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (lvMarket.SelectedItem != null)
            {
                if (MarketVm != null)
                    MarketVm.SelectedMarket = (Market)lvMarket.SelectedItem;
                MediateClass.SuggestPage.Frame.Navigate(typeof(MarketPage), NavigationMode.Forward);
            }
        }
    }
}
