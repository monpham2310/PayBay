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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PayBay.View.AccountGroup
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class CreateAccountPage : Page
	{
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
			FileOpenPicker fp = new FileOpenPicker();     
			fp.FileTypeFilter.Add(".jpeg");
			fp.FileTypeFilter.Add(".png");
			fp.FileTypeFilter.Add(".bmp");
			fp.FileTypeFilter.Add(".jpg");
			
			StorageFile file = await fp.PickSingleFileAsync();

			using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
			{
				// Set the image source to the selected bitmap
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.DecodePixelHeight = 80;
				bitmapImage.DecodePixelWidth = 80;
				await bitmapImage.SetSourceAsync(fileStream);

				((VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(AvatarButton, 
					0) as Grid, 
					0) as Ellipse)
					.Fill as ImageBrush)
					.ImageSource = bitmapImage;
			}
		}
	}
}
