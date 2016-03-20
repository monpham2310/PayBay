using PayBay.Model;
using PayBay.Utilities.Common;
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
    public sealed partial class MessageInboxPage : Page
    {
        private MessageInboxViewModel MessageVm => (MessageInboxViewModel)scrollvMessage.DataContext;

        public MessageInboxPage()
        {            
            this.InitializeComponent();            
        }
                
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MessageVm.LoadMoreMessageList(TYPEGET.START);
            //if (MediateClass.isBtInbox)
            //{
            //    await MessageVm.NewMessage();
            //    ViewDetailMessage();
            //}
        }

        private void ViewDetailMessage()
        {
            MessageVm.MessageSelected = (MessageInbox)lvComments.SelectedItem;
            if (MediateClass.MsgDetailVM != null)
                MediateClass.MsgDetailVM.LoadMoreMessage(TYPEGET.START);
            MessagePopup.IsOpen = true;
            MainGrid.IsHitTestVisible = false;
            MainGrid.Opacity = 0.4;

            ProcessPopupSizeAndPos();

            MessageFrame.Navigate(typeof(InboxDetailPage));
        }

        private void lvComments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(MessageVm != null)
            {
                ViewDetailMessage();
            }
        }

        private void MessagePopup_Closed(object sender, object e)
        {
            MainGrid.Opacity = 1.0;
            MainGrid.IsHitTestVisible = true;
        }

        private void ProcessPopupSizeAndPos()
        {
            MessageFrame.Height = ActualHeight * 0.85;
            MessageFrame.Width = MessageFrame.Height / 1.5;

            if (MessageFrame.Width < 330)
                MessageFrame.Width = 330;

            MessagePopup.HorizontalOffset = (ActualWidth - MessageFrame.Width) / 2;
            MessagePopup.VerticalOffset = (ActualHeight - MessageFrame.Height) / 2;
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ProcessPopupSizeAndPos();
        }

        private void scrollvMessage_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if(scrollvMessage.VerticalOffset == 0)
            {
                if (MessageVm != null)
                    MessageVm.LoadMoreMessageList(TYPEGET.MORE, TYPE.NEW);
            }
            else
            {
                if (MessageVm != null)
                    MessageVm.LoadMoreMessageList(TYPEGET.MORE);
            }
        }
    }
}
