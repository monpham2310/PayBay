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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PayBay.View.MiddleFunctionGroup
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class AboutUsPage : Page
	{
		public AboutUsPage()
		{
			this.InitializeComponent();
		}

		private void btnPhone_Click(object sender, RoutedEventArgs e)
		{

		}

		private void btnEmail_Click(object sender, RoutedEventArgs e)
		{
			popupEmail.IsOpen = true;
			srvMain.IsHitTestVisible = false;
			srvMain.Opacity = 0.4;

			ProcessPopupSizeAndPos();

			frameEmail.Navigate(typeof(EmailPage));
		}

		private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			ProcessPopupSizeAndPos();
		}

		private void popupEmail_Closed(object sender, object e)
		{
			srvMain.Opacity = 1.0;
			srvMain.IsHitTestVisible = true;
		}

		private void ProcessPopupSizeAndPos()
		{
			frameEmail.Height = ActualHeight * 0.85;
			frameEmail.Width = frameEmail.Height / 1.5;

			if (frameEmail.Width < 330)
				frameEmail.Width = 330;

			popupEmail.HorizontalOffset = (ActualWidth - frameEmail.Width) / 2;
			popupEmail.VerticalOffset = (ActualHeight - frameEmail.Height) / 2;
		}
	}
}
