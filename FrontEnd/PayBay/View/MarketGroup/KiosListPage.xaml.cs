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
using PayBay.ViewModel.AccountGroup;
using PayBay.Model;
using Newtonsoft.Json.Linq;
using PayBay.Utilities.Common;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PayBay.ViewModel.MarketGroup;
using PayBay.ViewModel.ProductGroup;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Popups;
using PayBay.ViewModel.CommentGroup;
using PayBay.View.MarketGroup.KiosGroup;

namespace PayBay.View.MarketGroup
{
    public sealed partial class KiosListPage : Page
    {
        private KiosViewModel KiosVm => (KiosViewModel)gridviewKiosList.DataContext;
        private CommentViewModel CommentVm => (CommentViewModel)scrollvComment.DataContext;
        private ProductViewModel ProductVm => (ProductViewModel)scrollvProduct.DataContext;

        public KiosListPage()
        {
            this.InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string storeName = tbxSearch.Text;
            int marketId = MediateClass.MarketVM.SelectedMarket.MarketId;
            if(KiosVm != null)
            {
                if (!string.IsNullOrEmpty(storeName))
                    KiosVm.LoadMoreStore(marketId, storeName, TYPEGET.START);
                else
                    KiosVm.LoadMoreStore(marketId, TYPEGET.START);
            }
        }

        private void kiosItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            splitviewKios.IsPaneOpen = true;
            if (KiosVm != null)
            {
                if (gridviewKiosList.SelectedItem != null)
                {
                    KiosVm.SelectedStore = (Kios)gridviewKiosList.SelectedItem;
                    int selectedId = KiosVm.SelectedStore.StoreId;
                    MediateClass.ProductVM.GetProductsOfStore(selectedId,TYPEGET.START);
                    MediateClass.CommentVM.GetCommentOfStore(selectedId,TYPEGET.START);
                }
            }
            txtComment.Text = "";
        }

        private void kiosItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            splitviewKios.IsPaneOpen = !splitviewKios.IsPaneOpen;
            if (KiosVm != null)
            {
                if (gridviewKiosList.SelectedItem != null)
                {
                    KiosVm.SelectedStore = (Kios)gridviewKiosList.SelectedItem;
                    int selectedId = KiosVm.SelectedStore.StoreId;
                    MediateClass.ProductVM.GetProductsOfStore(selectedId, TYPEGET.START);
                    MediateClass.CommentVM.GetCommentOfStore(selectedId, TYPEGET.START);                    
                }
            }            
        }

        //private void btnStar1_Click(object sender, RoutedEventArgs e)
        //{
        //    imgBtnStar1.Source = new BitmapImage(new Uri("ms-appx:///Assets/Rating/fullstar.png"));
        //}

        //private void btnStar2_Click(object sender, RoutedEventArgs e)
        //{
        //    imgBtnStar1.Source = new BitmapImage(new Uri("ms-appx:///Assets/Rating/fullstar.png"));
        //    imgBtnStar2.Source = new BitmapImage(new Uri("ms-appx:///Assets/Rating/fullstar.png"));
        //}

        //private void btnStar3_Click(object sender, RoutedEventArgs e)
        //{
        //    imgBtnStar1.Source = new BitmapImage(new Uri("ms-appx:///Assets/Rating/fullstar.png"));
        //    imgBtnStar2.Source = new BitmapImage(new Uri("ms-appx:///Assets/Rating/fullstar.png"));
        //    imgBtnStar3.Source = new BitmapImage(new Uri("ms-appx:///Assets/Rating/fullstar.png"));
        //}

        //private void btnStar4_Click(object sender, RoutedEventArgs e)
        //{
        //    imgBtnStar1.Source = new BitmapImage(new Uri("ms-appx:///Assets/Rating/fullstar.png"));
        //    imgBtnStar2.Source = new BitmapImage(new Uri("ms-appx:///Assets/Rating/fullstar.png"));
        //    imgBtnStar3.Source = new BitmapImage(new Uri("ms-appx:///Assets/Rating/fullstar.png"));
        //    imgBtnStar4.Source = new BitmapImage(new Uri("ms-appx:///Assets/Rating/fullstar.png"));
        //}

        //private void btnStar5_Click(object sender, RoutedEventArgs e)
        //{
        //    imgBtnStar1.Source = new BitmapImage(new Uri("ms-appx:///Assets/Rating/fullstar.png"));
        //    imgBtnStar2.Source = new BitmapImage(new Uri("ms-appx:///Assets/Rating/fullstar.png"));
        //    imgBtnStar3.Source = new BitmapImage(new Uri("ms-appx:///Assets/Rating/fullstar.png"));
        //    imgBtnStar4.Source = new BitmapImage(new Uri("ms-appx:///Assets/Rating/fullstar.png"));
        //    imgBtnStar5.Source = new BitmapImage(new Uri("ms-appx:///Assets/Rating/fullstar.png"));
        //}

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            int marketId = MediateClass.MarketVM.SelectedMarket.MarketId;
            if(scrollvStore.VerticalOffset >= scrollvStore.ScrollableHeight)
            {
                if (KiosVm != null)
                {
                    if (!string.IsNullOrEmpty(tbxSearch.Text))
                        KiosVm.LoadMoreStore(marketId, tbxSearch.Text, TYPEGET.MORE);
                    else
                        KiosVm.LoadMoreStore(marketId, TYPEGET.MORE);
                }
            }
            else if (scrollvStore.VerticalOffset == 0)
            {
                if (KiosVm != null)
                {
                    if (!string.IsNullOrEmpty(tbxSearch.Text))
                        KiosVm.LoadMoreStore(marketId, tbxSearch.Text, TYPEGET.MORE, TYPE.NEW);
                    else
                        KiosVm.LoadMoreStore(marketId, TYPEGET.MORE, TYPE.NEW);
                }
            }
        }

        private async void btSend_Click(object sender, RoutedEventArgs e)
        {
            if(MediateClass.UserVM.UserInfo != null)
            {
                await CommentVm.UserComment(txtComment.Text, KiosVm.SelectedStore.StoreId, TYPEGET.START);
                txtComment.Text = "";                
            }
            else
            {
                await new MessageDialog("You are not login.Please login to comment!","Notification!").ShowAsync();
            }
        }             

		private async void linkSeeComments_Click(object sender, RoutedEventArgs e)
		{
            if (MediateClass.UserVM.UserInfo != null)
            {
                if(MediateClass.RateVm != null)
                    MediateClass.RateVm.LoadStarRated();
                Frame.Navigate(typeof(KiosPage));
            }
            else
            {
                await new MessageDialog("You are not login.Login is required!", "Notification!").ShowAsync();
            }
		}
		
		private async void linkSeeProducts_Click(object sender, RoutedEventArgs e)
		{
            if (MediateClass.UserVM.UserInfo != null)
            {
                if (MediateClass.RateVm != null)
                    MediateClass.RateVm.LoadStarRated();
                Frame.Navigate(typeof(KiosPage));
            }
            else
            {
                await new MessageDialog("You are not login.Login is required!", "Notification!").ShowAsync();
            }
		}

        private void btCallMobile_Click(object sender, RoutedEventArgs e)
        {
            Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI(KiosVm.SelectedStore.Phone, KiosVm.SelectedStore.StoreName);

            
        }
        
        async private void btMessage_Click(object sender, RoutedEventArgs e)
        {
            Windows.ApplicationModel.Chat.ChatMessage msg = new Windows.ApplicationModel.Chat.ChatMessage();
            msg.Body = "This is body of demo message.";
            msg.Recipients.Add(KiosVm.SelectedStore.Phone);
            await Windows.ApplicationModel.Chat.ChatMessageManager.ShowComposeSmsMessageAsync(msg);
        }                     

        private void scrvSliderOfStore_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            int storeId = KiosVm.SelectedStore.StoreId;
            if (scrollvProduct.VerticalOffset == 0)
            {
                if (ProductVm != null && KiosVm != null && CommentVm != null)
                {
                    ProductVm.GetProductsOfStore(storeId, TYPEGET.START);
                    CommentVm.GetCommentOfStore(storeId, TYPEGET.START);
                }
            }
            else if (scrollvComment.VerticalOffset >= scrollvStore.ScrollableHeight)
            {
                
            }
        }
                
    }
}
