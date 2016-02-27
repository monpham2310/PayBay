using PayBay.Utilities.Common;
using PayBay.ViewModel.MarketGroup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PayBay.View.MarketGroup
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MarketPage : Page
	{
        private MarketViewModel MarketVm => (MarketViewModel)spnHeader.DataContext;
		public MarketPage()
		{
			this.InitializeComponent();            
            this.Loaded += Page_Loaded;
		}

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetUpMap();
        }

        private void ShopNowButton_Click(object sender, RoutedEventArgs e)
        {            
            Frame.Navigate(typeof(KiosListPage));
        }

        private void BackHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (MarketVm != null)
                MarketVm.SelectedMarket = null;
            Frame.GoBack();
        }

        private void SetUpMap()
        {
            //SelectedItem.Address here
            //string addressToGeocode = "Lê Lợi, Ho Chi Minh City, Tp. Hồ Chí Minh";
                        
            BasicGeoposition pos = new BasicGeoposition();
            pos.Latitude = MarketVm.SelectedMarket.Latitute;
            pos.Longitude = MarketVm.SelectedMarket.Longitute;

            Geopoint point = new Geopoint(pos);

            //Convert address to a point on map
            //MapLocationFinderResult result =
            //      await MapLocationFinder.FindLocationsAsync(
            //                        "",                  
            //                        point,
            //                        3);
                        
            //Setting for map control
            MarketAddressMap.ZoomInteractionMode = MapInteractionMode.GestureAndControl;
            MarketAddressMap.TiltInteractionMode = MapInteractionMode.GestureAndControl;
            MarketAddressMap.MapServiceToken = "xtSqkEYq82UbuykTpmRG~yab3f1f6UWvpGYeOOmhKUA~Aonl0iVFHHl4JarYn7g7GF4Gdd7l0XR2BIH9zDMXYVSyWFHl8TS9_MN5c7N0vXPX";          
            MarketAddressMap.ZoomLevel = 16;
            MarketAddressMap.LandmarksVisible = true;

            // Set the map location.
            //MarketAddressMap.Center = result.Locations[0].Point;
            MarketAddressMap.Center = point;

            // Add an icon for the market address, temporarily use fullstar2.png
            MapIcon mapIcon = new MapIcon();
            mapIcon.Image = RandomAccessStreamReference.CreateFromUri(
              new Uri("ms-appx:///Assets/Rating/fullstar2.png"));
            mapIcon.NormalizedAnchorPoint = new Point(0.5, 0.5);
            mapIcon.Location = point;
            mapIcon.Title = "Market Here !!!";
            MarketAddressMap.MapElements.Add(mapIcon);

        }
    }
}
