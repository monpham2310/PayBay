using PayBay.Model;
using PayBay.Utilities.Common;
using PayBay.ViewModel.MarketGroup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class StoreRegisterPage : Page
    {
        public StoreRegisterPage()
        {
            this.InitializeComponent();
        }

        private MarketViewModel MarketVm => (MarketViewModel)spnMarketLst.DataContext;
        StorageFile media = null;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {            
            if (KiosViewModel.isUpdate)
            {
                var store = MediateClass.KiotVM.Store;
                txtStoreName.Text = store.StoreName;
                txtKiotNo.Text = store.KiotNo;
                txtPhone.Text = store.Phone;
                cbMarket.SelectedValue = store.MarketId;
                if (store.Image != null)
                {
                    if (store.Image.IndexOf("/Assets/") == -1)
                    {
                        var uriImage = new Uri(store.Image);
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
            pgrStore.IsActive = !pgrStore.IsActive;
            gridStore.IsHitTestVisible = !gridStore.IsHitTestVisible;
            gridStore.Opacity = (gridStore.Opacity == 1.0) ? 0.7 : 1.0;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            ToggleProgressRing();
            bool check = false;
            if (txtStoreName.Text != "" && txtKiotNo.Text != "" && txtPhone.Text != null && cbMarket.SelectedValue != null)
            {
                Kios temp = new Kios();
                temp.StoreName = txtStoreName.Text;
                temp.KiotNo = txtKiotNo.Text;
                temp.Phone = txtPhone.Text;
                temp.MarketId = Convert.ToInt32(cbMarket.SelectedValue);
                temp.OwnerId = MediateClass.UserVM.UserInfo.UserId;
                if (!KiosViewModel.isUpdate)
                {
                    check = await MediateClass.KiotVM.InsertStore(temp, media);
                }
                else
                {
                    temp.StoreId = MediateClass.KiotVM.Store.StoreId;
                    temp.Image = MediateClass.KiotVM.Store.Image;
                    temp.SasQuery = MediateClass.KiotVM.Store.SasQuery;
                    temp.Rate = MediateClass.KiotVM.Store.Rate;

                    check = await MediateClass.KiotVM.UpdateStore(temp, media);
                }
                if (check)
                {
                    await new MessageDialog("Successful!", "Store").ShowAsync();
                    Frame.GoBack();
                }
                else
                    await new MessageDialog("Not successful!", "Store").ShowAsync();
            }
            else
                await new MessageDialog("Please fill the infomation!", "Store").ShowAsync();
            ToggleProgressRing();
        }

        private async void btDel_Click(object sender, RoutedEventArgs e)
        {
            ToggleProgressRing();
            bool check = await MediateClass.KiotVM.DeleteStore();
            if (check)
            {
                await new MessageDialog("Delete is successful!", "Delete Store").ShowAsync();

                Frame.GoBack();
            }
            else
            {
                await new MessageDialog("Delete is not successful!", "Delete Store").ShowAsync();
            }
            ToggleProgressRing();
        }

    }
}
