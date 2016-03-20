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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PayBay.View.TopFunctionGroup
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class InboxPage : Page
	{

        InboxIO inbox;

        public InboxPage()
		{
			this.InitializeComponent();
            inbox = new InboxIO("http://immense-reef-32079.herokuapp.com/", 0);
            inbox.registerClient("Huy");
        }

        public ObservableCollection<string> MessageList
        {
            get
            {
                return inbox.MessageList;
            }
        }

        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            if (tbxMessage.Text != "")
                inbox.sendMessage("Tam", tbxMessage.Text);
        }
    }
}
