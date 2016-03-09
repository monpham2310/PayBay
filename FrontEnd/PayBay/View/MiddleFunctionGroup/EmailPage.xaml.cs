using PayBay.Model;
using PayBay.Utilities.Common;
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
	public sealed partial class EmailPage : Page
	{
		public EmailPage()
		{
			this.InitializeComponent();
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			((Popup)Frame.Parent).IsOpen = false;
		}

		private async void btnSend_Click(object sender, RoutedEventArgs e)
		{
            string email = tbEmail.Text;
            string pass = pbPassword.Password;
            string title = tbTitle.Text;
            string content = tbContent.Text;

            if (email != "" && pass != "" && title != "" && content != "")
            {
                GuestMail mail = new GuestMail(email, pass, title, content);
                await MediateClass.UserVM.UserSendMail(mail);
            }
		} 
	}
}
