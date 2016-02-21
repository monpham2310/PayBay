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

        private async void SummitButton_Click(object sender, RoutedEventArgs e)
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
                    user.TypeId = typeid;

                    await InsertUser(user);
                }
                else
                {
                    await new MessageDialog("Your email is NOT valid!Please try again!", "Notification").ShowAsync();
                }
            }
            else
            {
                await new MessageDialog("Please fill all the infomation!Thank you!", "Notification").ShowAsync();
            }
        }

        private async Task InsertUser(UserInfo user)
        {
            try
            {
                JToken data = JToken.FromObject(user);
                JToken result = await App.MobileService.InvokeApiAsync("Users", data, HttpMethod.Post, null);

                JObject response = JObject.Parse(result.ToString());

                user.UserId = int.Parse(response["UserId"].ToString());
                user.Avatar = response["Avatar"].ToString();
                user.SasQuery = response["SasQuery"].ToString();

                string userName = user.Username.ToLower();
                userName = (userName.IndexOf(" ") != 1) ? userName.Replace(" ", "") : userName;

                var func = Functions.GetInstance();

                bool check = await func.UploadImageToBlob("users", userName, user.UserId, user.Avatar, user.SasQuery, mediaFile);
                if (check)
                {
                    await new MessageDialog("Create Account is successful!", "Notification!").ShowAsync();
                }
                else
                {
                    await new MessageDialog("Create Account is NOT successful!", "Notification!").ShowAsync();
                }                                         
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message.ToString(), "Notification!").ShowAsync();
            }
        }
               
    }
}
