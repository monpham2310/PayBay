using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
	public sealed partial class MyFavoritesPage : Page
	{
		public MyFavoritesPage()
		{
			this.InitializeComponent();
		}

        private async void btnLocation_Click(object sender, RoutedEventArgs e)
        {
            var accessStatus = await Geolocator.RequestAccessAsync();

            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    Geolocator geolocator = new Geolocator() { DesiredAccuracyInMeters = 0 };
                    Geoposition pos = await geolocator.GetGeopositionAsync(
                                            maximumAge: TimeSpan.FromMinutes(5),
                                            timeout: TimeSpan.FromSeconds(10));

                    tbLatitude.Text = "Latitude: " + pos.Coordinate.Point.Position.Latitude;
                    tbLongitude.Text = "Longitude: " + pos.Coordinate.Point.Position.Longitude;

                    break;
                case GeolocationAccessStatus.Denied:
                    await new MessageDialog("Access to location is denied.", "Notification!").ShowAsync();
                    break;
                case GeolocationAccessStatus.Unspecified:
                    await new MessageDialog("Unspecified error.", "Notification!").ShowAsync();
                    break;
            }
        }

    }
}
