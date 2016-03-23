using PayBay.Model;
using PayBay.Utilities.Common;
using PayBay.View.TopFunctionGroup;
using PayBay.ViewModel.InboxGroup;
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

namespace PayBay.View.InboxGroup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InboxListPage : Page
    {
        private MessageInboxViewModel MessageVm => (MessageInboxViewModel)svMessageLst.DataContext;

        public InboxListPage()
        {
            this.InitializeComponent();
        }

        private void svMessageLst_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            
        }
                
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(MediateClass.MessageVM != null)
            {
                MediateClass.MessageVM.LoadMessageList();
            }
        }

        private void lvMsgHistory_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (lvMsgHistory.SelectedItem != null)
            {
                MessageInbox msg = (MessageInbox)lvMsgHistory.SelectedItem;

                if (MediateClass.MessageVM != null)
                {
                    MessageInboxViewModel.UserChated = msg.UserID;
                    MediateClass.MessageVM.LoadInboxHitory(TYPEGET.START);
                }
                Frame.Navigate(typeof(InboxPage), NavigationMode.Forward);
            }            
        }
    }
}
