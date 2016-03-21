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
using Quobject.SocketIoClientDotNet.Client;
using System.Diagnostics;
using PayBay.Services.MobileServices.InboxSocketIO;
using System.Collections.ObjectModel;
using PayBay.Utilities.Common;
using PayBay.ViewModel.InboxGroup;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PayBay.View.TopFunctionGroup
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class InboxPage : Page
	{
        private MessageInboxViewModel MessageVm => (MessageInboxViewModel)DataContext;               
        public InboxPage()
		{
			this.InitializeComponent();            
        }
                
        private async void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            if (tbxMessage.Text != "" && MessageVm != null)
            {
                bool check = await MessageVm.sendMessage(tbxMessage.Text);
                if (check)
                    tbxMessage.Text = "";
            }
        }

        private void svMessage_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {

        }
    }
}
