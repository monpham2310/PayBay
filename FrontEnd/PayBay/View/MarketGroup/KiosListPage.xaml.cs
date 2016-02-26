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
using PayBay.Utilities.Handler;
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
        private CommentViewModel CommentVm => (CommentViewModel)lvComments.DataContext;    

        public KiosListPage()
        {
            this.InitializeComponent();
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string storeName = tbxSearch.Text;
            if(KiosVm != null)
            {
                if (!string.IsNullOrEmpty(storeName))
                    await KiosVm.LoadMoreStore(storeName, TYPEGET.START);
                else
                    await KiosVm.LoadMoreStore(TYPEGET.START);
            }
        }

        private async void kiosItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            splitviewKios.IsPaneOpen = true;
            if (KiosVm != null)
            {
                KiosVm.SelectedStore = (Kios)gridviewKiosList.SelectedItem;
                int selectedId = KiosVm.SelectedStore.StoreId;
                await MediateClass.ProductVM.GetProductsOfStore(selectedId);
                await MediateClass.CommentVM.GetCommentOfStore(selectedId);
            }
            txtComment.Text = "";
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

        private async void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if(scrollvStore.VerticalOffset >= scrollvStore.ScrollableHeight)
            {
                if (KiosVm != null)
                {
                    if (!string.IsNullOrEmpty(tbxSearch.Text))
                        await KiosVm.LoadMoreStore(tbxSearch.Text, TYPEGET.MORE);
                    else
                        await KiosVm.LoadMoreStore(TYPEGET.MORE);
                }
            }
            else if (scrollvStore.VerticalOffset == 0)
            {

            }
        }

        private async void btSend_Click(object sender, RoutedEventArgs e)
        {
            if(MediateClass.UserVM.UserInfo != null)
            {
                await CommentVm.UserComment(txtComment.Text, KiosVm.SelectedStore.StoreId);                
            }
            else
            {
                await new MessageDialog("You are not login.Please login to comment!","Notification!").ShowAsync();
            }
        }             

		private void linkSeeComments_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(KiosPage));
		}
		
		private void linkSeeProducts_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(KiosPage));
		}

        private void btCallMobile_Click(object sender, RoutedEventArgs e)
        {
            Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI("+84932273623", "TamBaDao");
        }
    }
}
