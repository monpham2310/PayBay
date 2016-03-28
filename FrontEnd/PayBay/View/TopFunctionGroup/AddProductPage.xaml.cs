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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (ProductViewModel.isUpdate)
            {
                var product = MediateClass.ProductVM.SelectedProduct;
                txtProductName.Text = product.ProductName;
                txtUnitPrice.Text = product.UnitPrice.ToString();
                txtUnit.Text = product.Unit;
                txtAmount.Text = product.NumberOf.ToString();
                txtSalePrice.Text = product.SalePrice.ToString();
                cbStore.SelectedValue = product.StoreId;
                if (product.Image != null)
                {
                    if (product.Image.IndexOf("/Assets/") == -1)
                    {
                        var uriImage = new Uri(product.Image);
                        var img = new BitmapImage(uriImage);
                        imgbrImage.ImageSource = img;
                    }
                }
            }
        }

        private async void ButtonCapture_Click(object sender, RoutedEventArgs e)
        {
            media = await Functions.GetPhotoFromGallery();

            if (media != null)
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
            pgrProduct.IsActive = !pgrProduct.IsActive;
            gridProduct.IsHitTestVisible = !gridProduct.IsHitTestVisible;
            gridProduct.Opacity = (gridProduct.Opacity == 1.0) ? 0.7 : 1.0;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            ToggleProgressRing();
            bool check = false;
            if (txtProductName.Text != "" && txtUnitPrice.Text != "" && txtUnit.Text != null 
                && txtAmount.Text != "" && txtSalePrice.Text != "" && cbStore.SelectedValue != null)
            {
                Product temp = new Product();
                temp.ProductName = txtProductName.Text;
                temp.UnitPrice = float.Parse(txtUnitPrice.Text);
                temp.Unit = txtUnit.Text;
                temp.NumberOf = Convert.ToInt32(txtAmount.Text);
                temp.SalePrice = float.Parse(txtSalePrice.Text);
                temp.StoreId = Convert.ToInt32(cbStore.SelectedValue);
                
                if (!ProductViewModel.isUpdate)
                {
                    check = await MediateClass.ProductVM.InsertProduct(temp, media);
                }
                else
                {
                    temp.ProductId = MediateClass.ProductVM.SelectedProduct.ProductId;
                    temp.Image = MediateClass.ProductVM.SelectedProduct.Image;
                    temp.SasQuery = MediateClass.ProductVM.SelectedProduct.SasQuery;                    

                    check = await MediateClass.ProductVM.UpdateProduct(temp, media);
                }
                if (check)
                {
                    await new MessageDialog("Successful!", "Product").ShowAsync();
                    Frame.GoBack();
                }
                else
                    await new MessageDialog("Not successful!", "Product").ShowAsync();
            }
            else
                await new MessageDialog("Please fill the infomation!", "Product").ShowAsync();
            ToggleProgressRing();
        }
                
        private async void btDel_Click(object sender, RoutedEventArgs e)
        {
            ToggleProgressRing();
            bool check = await MediateClass.ProductVM.DeleteProduct();
            if (check)
            {
                await new MessageDialog("Delete is successful!", "Delete Product").ShowAsync();
                Frame.GoBack();
            }
            else
            {
                await new MessageDialog("Delete is not successful!", "Delete Product").ShowAsync();
            }
            ToggleProgressRing();
        }
    }
}
