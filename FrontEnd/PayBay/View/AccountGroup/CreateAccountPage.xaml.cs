using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using PayBay.Utilities.Common;
using PayBay.ViewModel.AccountGroup;
using PayBay.Model;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PayBay.View.AccountGroup
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class CreateAccountPage : Page
	{
        StorageFile mediaFile = null;

		public CreateAccountPage()
		{
			this.InitializeComponent();
		}

        private void isShowControl(bool isShow)
        {
            BackHyperlinkButton.Visibility = (isShow) ? Visibility.Collapsed : Visibility.Visible;
            ExitHyperlinkButton.Visibility = (isShow) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (UserInfoViewModel.isViewInfo)
            {
                tblTitle.Text = "Account Infomation";
                isShowControl(true);
                var userInfo = MediateClass.UserVM.UserInfo;
                if (userInfo.Avatar != "/Assets/lol.jpg")
                {
                    var uriAvatar = new Uri(userInfo.Avatar);
                    var img = new BitmapImage(uriAvatar);
                    AvatarImage.ImageSource = img;
                }
                FullNameTextBox.Text = userInfo.Username;
                EmailTextBox.Text = userInfo.Email;
                PhoneTextBox.Text = userInfo.Phone;
                AddressTextBox.Text = userInfo.Address;
                BirthdayDatePicker.Date = userInfo.Birthday;
                MaleRadioButton.IsChecked = userInfo.Gender;

                if (userInfo.TypeId != 1)
                {
                    TypeCommboBox.SelectedItem = (userInfo.TypeId == 2) ? StoreOwner : User;
                }
                else
                {
                    TypeCommboBox.IsEnabled = false;
                }
            }
            else
                isShowControl(false);
        }

        private void BackHyperlinkButton_Click(object sender, RoutedEventArgs e)
		{            
			Frame.GoBack();                
        }

        private async void AvatarButton_Click(object sender, RoutedEventArgs e)
        {            
            mediaFile = await Functions.GetPhotoFromGallery();

            if (mediaFile != null)
            { 
                var stream = await mediaFile.OpenAsync(FileAccessMode.Read);
                var bitmapImage = new BitmapImage();
                await bitmapImage.SetSourceAsync(stream);

                var decoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(stream);
                AvatarImage.ImageSource = bitmapImage;
            }
        }

        private async void alterOrCreateUser()
        {
            UserInfo user = new UserInfo();

            if (FullNameTextBox.Text != "" && AddressTextBox.Text != "" && PhoneTextBox.Text != "" && EmailTextBox.Text != ""
                && PasswordTextBox.Password != "")
            {
                if (Functions.EmailIsValid(EmailTextBox.Text))
                {                    
                    user.Username = FullNameTextBox.Text;
                    user.Address = AddressTextBox.Text;
                    user.Phone = PhoneTextBox.Text;
                    user.Email = EmailTextBox.Text;
                    user.Gender = ((bool)MaleRadioButton.IsChecked) ? true : false;
                    user.Pass = Functions.GetBytes(PasswordTextBox.Password);
                    user.Birthday = BirthdayDatePicker.Date.DateTime;
                    ComboBoxItem ComboItem = (ComboBoxItem)TypeCommboBox.SelectedItem;
                    int typeid = int.Parse(ComboItem.Tag.ToString());                    
                    
                    if (!UserInfoViewModel.isViewInfo)
                    {
                        user.TypeId = typeid;
                        bool result = await MediateClass.UserVM.AlterOrCreateUser(user, mediaFile, HttpMethod.Post);
                        if (result)
                        {
                            await new MessageDialog("Create Account is successful!", "Notification!").ShowAsync();
                            Frame.GoBack();
                        }
                        else
                        {
                            await new MessageDialog("Upload avatar is not successful!", "Notification!").ShowAsync();
                            EmailTextBox.Focus(FocusState.Pointer);
                        }
                    }
                    else
                    {
                        user.Avatar = MediateClass.UserVM.UserInfo.Avatar;
                        user.SasQuery = MediateClass.UserVM.UserInfo.SasQuery;
                        user.UserId = MediateClass.UserVM.UserInfo.UserId;
                        user.TypeId = (MediateClass.UserVM.UserInfo.TypeId == 1) ? MediateClass.UserVM.UserInfo.TypeId : typeid;
                        bool result = await MediateClass.UserVM.AlterOrCreateUser(user, mediaFile, HttpMethod.Put);
                        if (result)
                        {
                            await new MessageDialog("Update Account is successful!", "Notification!").ShowAsync();
                            ((Popup)Frame.Parent).IsOpen = false;
                            UserInfoViewModel.isViewInfo = false;
                        }
                        else
                        {
                            await new MessageDialog("Update account is not successful!", "Notification!").ShowAsync();
                            EmailTextBox.Focus(FocusState.Pointer);
                        }
                    }
                }
                else
                {
                    await new MessageDialog("Your email is NOT valid!Please try again!", "Notification").ShowAsync();
                    EmailTextBox.Focus(FocusState.Pointer);
                }
            }
            else
            {
                if(!UserInfoViewModel.isViewInfo)
                    await new MessageDialog("Please fill all the infomation!Thank you!", "Notification").ShowAsync();
                else
                    await new MessageDialog("Please confirm your password!", "Notification").ShowAsync();
            }
        }

        private void SummitButton_Click(object sender, RoutedEventArgs e)
        {
            alterOrCreateUser();
        }

        private void ExitHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            ((Popup)Frame.Parent).IsOpen = false;
        }
    }
}
