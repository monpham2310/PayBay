using Newtonsoft.Json.Linq;
using PayBay.Model;
using PayBay.Utilities.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PayBay.View.TopFunctionGroup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Function3Page : Page
    {
        public Function3Page()
        {
            this.InitializeComponent();
        }

        StorageFile media = null;

        private async void ButtonCapture_Click(object sender, RoutedEventArgs e)
        {
            media = await Functions.GetPhotoFromGallery();

            if (media != null)
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
            Kios temp = new Kios();
            temp.StoreName = txtStoreName.Text;
            temp.KiotNo = txtKiotNo.Text;
            temp.Phone = txtPhone.Text;
            temp.MarketId = int.Parse(txtMarket.Text);
            temp.OwnerId = int.Parse(txtOwnerId.Text);

            await InsertStore(temp);
        }

        private async Task InsertStore(Kios store)
        {
            try
            {
                JToken data = JToken.FromObject(store);
                JToken result = await App.MobileService.InvokeApiAsync("Stores", data, HttpMethod.Post, null);

                JObject response = JObject.Parse(result.ToString());

                store.Image = response["Image"].ToString();
                store.SasQuery = response["SasQuery"].ToString();
                string productName = store.StoreName.ToLower();

                bool check = await Functions.GetInstance()
                                            .UploadImageToBlob("stores", store.StoreName, store.MarketId, store.Image, store.SasQuery, media);

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
