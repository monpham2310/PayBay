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
using Windows.Storage;
using PayBay.Utilities.Common;
using Windows.UI.Xaml.Media.Imaging;
using PayBay.Model;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Windows.UI.Popups;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PayBay.View.TopFunctionGroup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddMarketPage : Page
    {
        StorageFile media = null;

        public AddMarketPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (MarketViewModel.isUpdate)
            {
                var market = MediateClass.MarketVM.SelectedMarket;
                txtMarketName.Text = market.MarketName;
                txtAddress.Text = market.Address;
                txtPhone.Text = market.Phone;
                txtLat.Text = market.Latitute.ToString();
                txtLng.Text = market.Longitute.ToString();                
                if (market.Image != null)
                {
                    if (market.Image.IndexOf("/Assets/") == -1)
                    {
                        var uriImage = new Uri(market.Image);
                        var img = new BitmapImage(uriImage);
                        imgbrImage.ImageSource = img;
                    }
                }
            }
        }

        private async void ButtonCapture_Click(object sender, RoutedEventArgs e)
        {
            media = await Functions.GetPhotoFromGallery();

            if(media != null)
            {
                var stream = await media.OpenAsync(FileAccessMode.Read);
                var bitmapImage = new BitmapImage();
                await bitmapImage.SetSourceAsync(stream);

                var decoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(stream);
                imgbrImage.ImageSource = bitmapImage;
            }
        }

        private void ToggleProgressRing()
        {
            pgrMarket.IsActive = !pgrMarket.IsActive;
            gridMarket.IsHitTestVisible = !gridMarket.IsHitTestVisible;
            gridMarket.Opacity = (gridMarket.Opacity == 1.0) ? 0.7 : 1.0;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            ToggleProgressRing();
            bool check = false;
            Market selectedMarket = MediateClass.MarketVM.SelectedMarket;
            if (txtMarketName.Text != "" && txtAddress.Text != null && txtPhone.Text != null && txtLat.Text != null && txtLng.Text != null)
            {                
                Market temp = new Market();
                temp.MarketName = txtMarketName.Text;
                temp.Address = txtAddress.Text;
                temp.Phone = txtPhone.Text;
                temp.Latitute = float.Parse(txtLat.Text);
                temp.Longitute = float.Parse(txtLng.Text);

                if (!MarketViewModel.isUpdate)
                {
                    check = await MediateClass.MarketVM.InsertMarket(temp, media);
                }
                else
                {
                    temp.MarketId = selectedMarket.MarketId;
                    temp.Image = selectedMarket.Image;
                    temp.SasQuery = selectedMarket.SasQuery;

                    check = await MediateClass.MarketVM.UpdateMarket(temp, media);
                }
                if (check)
                {
                    await new MessageDialog("Successful!", "Market").ShowAsync();
                    Frame.GoBack();
                }
                else
                    await new MessageDialog("Not successful!", "Market").ShowAsync();
                            
            }
            else
                await new MessageDialog("Please fill the infomation!", "Market").ShowAsync();
            ToggleProgressRing();
        }
                
        //private async void btnLocation_Click(object sender, RoutedEventArgs e)
        //{
        //    var accessStatus = await Geolocator.RequestAccessAsync();

        //    switch (accessStatus)
        //    {
        //        case GeolocationAccessStatus.Allowed:
        //            Geolocator geolocator = new Geolocator() { DesiredAccuracyInMeters = 0 };
        //            Geoposition pos = await geolocator.GetGeopositionAsync();

        //            tbLatitude.Text = "Latitude: " + pos.Coordinate.Point.Position.Latitude;
        //            tbLongitude.Text = "Longitude: " + pos.Coordinate.Point.Position.Longitude;

        //            break;
        //        case GeolocationAccessStatus.Denied:
        //            await new MessageDialog("Access to location is denied.", "Notification!").ShowAsync();
        //            break;
        //        case GeolocationAccessStatus.Unspecified:
        //            await new MessageDialog("Unspecified error.", "Notification!").ShowAsync();
        //            break;
        //    }
        //}

        private async void btDel_Click(object sender, RoutedEventArgs e)
        {
            ToggleProgressRing();
            bool check = await MediateClass.MarketVM.DeleteMarket();
            if (check)
            {
                await new MessageDialog("Delete is successful!", "Delete Market").ShowAsync();
                Frame.GoBack();
            }
            else
            {
                await new MessageDialog("Delete is not successful!", "Delete Market").ShowAsync();
            }
            ToggleProgressRing();
        }
                
    }
}
