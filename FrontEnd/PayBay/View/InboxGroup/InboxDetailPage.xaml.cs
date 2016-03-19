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
    public sealed partial class InboxDetailPage : Page
    {
        private InboxDetailViewModel MsgDetailVm => (InboxDetailViewModel)pnMsgLst.DataContext;

        public InboxDetailPage()
        {
            this.InitializeComponent();
        }

        private void ExitHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            ((Popup)Frame.Parent).IsOpen = false;
        }

        private async void btSend_Click(object sender, RoutedEventArgs e)
        {
            string content = txtMessage.Text;
            DateTime inboxDate = DateTime.UtcNow;
            await MsgDetailVm.PushMessage(content, inboxDate);
        }

        private void svMsgLst_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if(svMsgLst.VerticalOffset >= svMsgLst.ScrollableHeight)
            {
                if(MsgDetailVm != null)
                {
                    MsgDetailVm.LoadMoreMessage(TYPEGET.MORE);
                }
            }
            //else
            //{
            //    if (MsgDetailVm != null)
            //    {
            //        await MsgDetailVm.LoadMoreMessage(TYPEGET.MORE, TYPE.NEW);
            //    }
            //}
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MsgDetailVm.LoadMoreMessage(TYPEGET.START);
        }
    }
}
