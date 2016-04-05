using Newtonsoft.Json.Linq;
using PayBay.Model;
using PayBay.Utilities.Common;
using PayBay.ViewModel.HomePageGroup;
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
    public sealed partial class AddSalePage : Page
    {
        public AddSalePage()
        {
            this.InitializeComponent();
        }

        StorageFile media = null;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (AdvertiseViewModel.isUpdate)
            {
                var sale = MediateClass.AdvertiseVM.SelectedSale;
                txtTitle.Text = sale.Title;
                txtDescribes.Text = sale.Describes;
                dpStartDate.Date = sale.StartDate;
                dpEndDate.Date = sale.EndDate;
                cbStore.SelectedValue = sale.StoreId;
                if (sale.Image != null)
                {
                    if (sale.Image.IndexOf("/Assets/") == -1)
                    {
                        var uriImage = new Uri(sale.Image);
                        var img = new BitmapImage(uriImage);
                        imgbrImage.ImageSource = img;
                    }
                }
            }
            else
            {
                dpStartDate.Date = DateTime.UtcNow;
                dpEndDate.Date = DateTime.UtcNow;
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
            pgrSale.IsActive = !pgrSale.IsActive;
            gridSale.IsHitTestVisible = !gridSale.IsHitTestVisible;
            gridSale.Opacity = (gridSale.Opacity == 1.0) ? 0.7 : 1.0;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            ToggleProgressRing();
            bool check = false;
            if (txtTitle.Text != "" && txtDescribes.Text != null && cbStore.SelectedValue != null)
            {
                if (dpStartDate.Date <= dpEndDate.Date)
                {
                    AdvertiseItem temp = new AdvertiseItem();
                    temp.Title = txtTitle.Text;
                    temp.Describes = txtDescribes.Text;
                    temp.StartDate = dpStartDate.Date.DateTime;
                    temp.EndDate = dpEndDate.Date.DateTime;
                    temp.StoreId = Convert.ToInt32(cbStore.SelectedValue);
                    temp.IsRequired = MediateClass.AdvertiseVM.SelectedSale.IsRequired;
                    if (!AdvertiseViewModel.isUpdate)
                    {
                        check = await MediateClass.AdvertiseVM.InsertSale(temp, media);
                    }
                    else
                    {
                        temp.SaleId = MediateClass.AdvertiseVM.SelectedSale.SaleId;
                        temp.Image = MediateClass.AdvertiseVM.SelectedSale.Image;
                        temp.SasQuery = MediateClass.AdvertiseVM.SelectedSale.SasQuery;

                        check = await MediateClass.AdvertiseVM.UpdateSale(temp, media);
                    }
                    if (check)
                    {
                        await new MessageDialog("Successful!", "Sales").ShowAsync();
                        Frame.GoBack();
                    }
                    else
                        await new MessageDialog("Not successful!", "Sales").ShowAsync();
                }
                else
                {
                    await new MessageDialog("Start date must be lower or equal end date!", "Sales").ShowAsync();
                }
            }
            else
                await new MessageDialog("Please fill the infomation!", "Sale").ShowAsync();
            ToggleProgressRing();
        }
                
        private async void btDel_Click(object sender, RoutedEventArgs e)
        {
            ToggleProgressRing();
            bool check = await MediateClass.AdvertiseVM.DeleteSale();
            if (check)
            {
                await new MessageDialog("Delete is successful!", "Delete Sale").ShowAsync();
                Frame.GoBack();
            }
            else
            {
                await new MessageDialog("Delete is not successful!", "Delete Sale").ShowAsync();
            }
            ToggleProgressRing();
        }
                
    }
}
