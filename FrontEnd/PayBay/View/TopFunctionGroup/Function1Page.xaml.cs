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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PayBay.View.TopFunctionGroup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Function1Page : Page
    {

        public MarketViewModel MarketVm => (MarketViewModel)DataContext;
        StorageFile media = null;

        public Function1Page()
        {
            this.InitializeComponent();
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
                imagePreview.Source = bitmapImage;
            }
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Market temp = new Market();
            temp.MarketName = txtMarketName.Text;
            temp.Address = txtAddr.Text;
            temp.Phone = txtPhone.Text;
            
            await InsertMarket(temp);
        }

        private async Task InsertMarket(Market market)
        {
            try
            {
                JToken data = JToken.FromObject(market);
                JToken result = await App.MobileService.InvokeApiAsync("Markets", data, HttpMethod.Post, null);

                JObject response = JObject.Parse(result.ToString());
                                
                market.Image = response["Image"].ToString();
                market.SasQuery = response["SasQuery"].ToString();
                string marketName = market.MarketName.ToLower();
                marketName = (marketName.IndexOf(" ") != -1) ? marketName.Replace(" ", "") : marketName;

                bool check = await Functions.Instance
                                            .UploadImageToBlob("markets", market.Image, market.SasQuery, media);

                imagePreview.Source = null;
                //viewModel.ProductList.Add(product);
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }
        }
                
    }
}
