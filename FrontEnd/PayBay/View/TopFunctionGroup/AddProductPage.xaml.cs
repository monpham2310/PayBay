using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json.Linq;
using PayBay.Model;
using PayBay.Utilities.Common;
using PayBay.ViewModel.HomePageGroup;
using PayBay.ViewModel.ProductGroup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Pickers;
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
    public sealed partial class AddProductPage : Page
    {
        public AddProductPage()
        {
            this.InitializeComponent();            
        }

        StorageFile media = null;
        MediaCapture cameraCapture;
        bool IsCaptureInProgress;
        CoreApplicationView view;
        string ImagePath;

        ProductViewModel viewModel = new ProductViewModel();

        private async Task GetPhotoFromGallery()
        {
            ImagePath = string.Empty;
            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            filePicker.ViewMode = PickerViewMode.Thumbnail;

            // Filter to include a sample subset of file types
            filePicker.FileTypeFilter.Clear();
            filePicker.FileTypeFilter.Add(".bmp");
            filePicker.FileTypeFilter.Add(".png");
            filePicker.FileTypeFilter.Add(".jpeg");
            filePicker.FileTypeFilter.Add(".jpg");

            media = await filePicker.PickSingleFileAsync();

            var stream = await media.OpenAsync(FileAccessMode.Read);
            var bitmapImage = new BitmapImage();
            await bitmapImage.SetSourceAsync(stream);

            var decoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(stream);
            imagePreview.Visibility = Visibility.Visible;
            imagePreview.Source = bitmapImage;
        }


        private async Task CaptureImage()
        {
            // Capture a new photo or video from the device.
            cameraCapture = new MediaCapture();
            cameraCapture.Failed += cameraCapture_Failed;

            // Initialize the camera for capture.
            await cameraCapture.InitializeAsync();

#if WINDOWS_PHONE_APP
    cameraCapture.SetPreviewRotation(VideoRotation.Clockwise90Degrees);
    cameraCapture.SetRecordRotation(VideoRotation.Clockwise90Degrees);
#endif

            captureGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //previewElement.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //previewElement.Source = cameraCapture;
            await cameraCapture.StartPreviewAsync();
        }

        private async void cameraCapture_Failed(MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs)
        {
            // It's safest to return this back onto the UI thread to show the message dialog.
            MessageDialog dialog = new MessageDialog(errorEventArgs.Message);
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                async () => { await dialog.ShowAsync(); });
        }

        private async void ButtonCapture_Click(object sender, RoutedEventArgs e)
        {
            //await CaptureImage();
            await GetPhotoFromGallery();
        }

        private byte[] GetFileByteArray(string filename)
        {
            FileStream oFileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);

            // Create a byte array of file size.
            byte[] FileByteArrayData = new byte[oFileStream.Length];

            //Read file in bytes from stream into the byte array
            oFileStream.Read(FileByteArrayData, 0, System.Convert.ToInt32(oFileStream.Length));

            //Close the File Stream
            oFileStream.Dispose();

            return FileByteArrayData; //return the byte data
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Product temp = new Product();
            temp.ProductName = txtProName.Text;
            temp.UnitPrice = float.Parse(txtUPrice.Text);
            temp.Unit = txtUnit.Text;
            temp.NumberOf = int.Parse(txtNumber.Text);
            temp.StoreId = int.Parse(txtStoreid.Text);
            temp.ImportDate = DateTime.UtcNow;
            temp.SalePrice = float.Parse(txtSPrice.Text);


            await InsertProduct(temp);
        }

        private async Task InsertProduct(Product product)
        {
            try
            {
                JToken data = JToken.FromObject(product);
                JToken result = await App.MobileService.InvokeApiAsync("Products", data, HttpMethod.Post, null);

                JObject response = JObject.Parse(result.ToString());

                product.Image = response["Image"].ToString();
                product.SasQuery = response["SasQuery"].ToString();
                string productName = product.ProductName.ToLower();

                bool check = await Functions.Instance
                                            .UploadImageToBlob("products",product.Image,product.SasQuery,media);

                await ResetCaptureAsync();
                //viewModel.ProductList.Add(product);
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }
        }

        private async Task ResetCaptureAsync()
        {
            //previewElement.Source = null;
            imagePreview.Source = null;

            // Make sure we stop the preview and release resources.
            await cameraCapture.StopPreviewAsync();
            cameraCapture.Dispose();
            //media = null;
        }

        //private async void previewElement_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    // Block multiple taps.
        //    if (!IsCaptureInProgress)
        //    {
        //        IsCaptureInProgress = true;

        //        // Create the temporary storage file.
        //        media = await ApplicationData.Current.LocalFolder
        //            .CreateFileAsync("capture.jpg", CreationCollisionOption.ReplaceExisting);

        //        // Take the picture and store it locally as a JPEG.
        //        await cameraCapture.CapturePhotoToStorageFileAsync(
        //            ImageEncodingProperties.CreateJpeg(), media);

        //        // Use the stored image as the preview source.
        //        BitmapImage tempBitmap = new BitmapImage(new Uri(media.Path));
        //        imagePreview.Source = tempBitmap;
        //        imagePreview.Visibility = Visibility.Visible;
        //        previewElement.Visibility = Visibility.Collapsed;
        //        IsCaptureInProgress = false;
        //    }
        //}
    }
}
