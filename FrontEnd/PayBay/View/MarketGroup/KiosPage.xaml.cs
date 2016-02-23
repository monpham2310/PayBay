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

namespace PayBay.View.MarketGroup
{
    public sealed partial class KiosPage : Page
    {
        private KiosViewModel KiosVm => (KiosViewModel)gridviewKiosList.DataContext;

        public KiosPage()
        {
            this.InitializeComponent();
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string storeName = tbxSearch.Text;
            if(KiosVm != null)
            {
                if (!string.IsNullOrEmpty(storeName))
                    await KiosVm.LoadMoreStore(storeName, Functions.TYPEGET.START);
                else
                    await KiosVm.LoadMoreStore(Functions.TYPEGET.START);
            }
        }

        private async void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if(scrollvStore.VerticalOffset >= scrollvStore.ScrollableHeight)
            {
                if (KiosVm != null)
                {
                    if (!string.IsNullOrEmpty(tbxSearch.Text))
                        await KiosVm.LoadMoreStore(tbxSearch.Text, Functions.TYPEGET.MORE);
                    else
                        await KiosVm.LoadMoreStore(Functions.TYPEGET.MORE);
                }
            }
            else if (scrollvStore.VerticalOffset == 0)
            {

            }
        }

        //private void BackHyperlinkButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Frame.GoBack();
        //}

    }
}
