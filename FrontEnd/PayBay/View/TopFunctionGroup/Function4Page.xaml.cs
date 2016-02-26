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
    public sealed partial class Function4Page : Page
    {
        public Function4Page()
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
            AdvertiseItem temp = new AdvertiseItem();
            temp.Title = txtTitle.Text;
            temp.Describes = txtDes.Text;
            temp.StartDate = dpStartDate.Date.DateTime;
            temp.EndDate = dpEndDate.Date.DateTime;
            temp.StoreId = int.Parse(txtStoreid.Text);
            temp.IsRequired = (bool)cbxRequire.IsChecked;            

            await InsertSale(temp);
        }

        private async Task InsertSale(AdvertiseItem sale)
        {
            try
            {
                JToken data = JToken.FromObject(sale);
                JToken result = await App.MobileService.InvokeApiAsync("SaleInfoes", data, HttpMethod.Post, null);

                JObject response = JObject.Parse(result.ToString());

                sale.Image = response["Image"].ToString();
                sale.SasQuery = response["SasQuery"].ToString();
                
                bool check = await Functions.Instance
                                            .UploadImageToBlob("sales", sale.Image, sale.SasQuery, media);

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
