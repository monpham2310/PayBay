using PayBay.Utilities.Common;
using PayBay.ViewModel.CommentGroup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PayBay.View.MarketGroup.KiosGroup
{
	public sealed partial class CommentPage : UserControl
	{
        private CommentViewModel CommentVm => (CommentViewModel)DataContext;

		public CommentPage()
		{
			this.InitializeComponent();
		}

        private async void btSend_Click(object sender, RoutedEventArgs e)
        {
            int storeId = MediateClass.KiotVM.SelectedStore.StoreId;
            await CommentVm.UserComment(txtComment.Text, storeId, TYPEGET.MORE, true);
            txtComment.Text = "";                        
        }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            int storeId = MediateClass.KiotVM.SelectedStore.StoreId;
            if(scrollvComentLst.VerticalOffset >= scrollvComentLst.ScrollableHeight)
            {
                if(CommentVm != null)
                    CommentVm.GetCommentOfStore(storeId, TYPEGET.MORE);
            }
            else if(scrollvComentLst.VerticalOffset == 0)
            {
                if(CommentVm != null)
                    CommentVm.GetCommentOfStore(storeId, TYPEGET.MORE, TYPE.NEW);
            }
        }
    }
}
