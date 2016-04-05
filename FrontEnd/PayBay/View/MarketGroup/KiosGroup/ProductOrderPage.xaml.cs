using PayBay.Model;
using PayBay.Utilities.Common;
using PayBay.View.OrderGroup;
using PayBay.ViewModel.ProductGroup;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PayBay.View.MarketGroup.KiosGroup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductOrderPage : Page
    {
        private ProductViewModel ProductVm => (ProductViewModel)DataContext;

        public ProductOrderPage()
        {
            this.InitializeComponent();
        }

        private void btnSub_Click(object sender, RoutedEventArgs e)
        {
            ProductVm.SelectedProduct.OrderUnit--;

            if (ProductVm.SelectedProduct.OrderUnit < 1)
            {
                ProductVm.SelectedProduct.OrderUnit = 1;
            }

            //tbOrderNum.Text = orderNum.ToString();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ProductVm.SelectedProduct.OrderUnit++;

            if (ProductVm.SelectedProduct.OrderUnit > ProductVm.SelectedProduct.NumberOf)
            {
                ProductVm.SelectedProduct.OrderUnit = ProductVm.SelectedProduct.NumberOf;
            }

            //tbOrderNum.Text = orderNum.ToString();
        }

        private void tbOrderNum_LostFocus(object sender, RoutedEventArgs e)
        {
            //int number;
            //var numOrder = tbOrderNum.Text;
            //var stockNum = Int32.Parse(tblStock.Text);

            //if (!Int32.TryParse(numOrder, out number) || Int32.Parse(numOrder) < 1)
            //{
            //    numOrder = "1";
            //}
            //else if (Int32.Parse(numOrder) > stockNum)
            //{
            //    numOrder = stockNum.ToString();
            //}

            //tbOrderNum.Text = numOrder;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ProductVm.SelectedProduct.OrderUnit > 0)
            {
                ProductVm.ProductOrderList.Add(ProductVm.SelectedProduct);
            }
            else
            {
                await new MessageDialog("You mustn't orderd with number zero!", "Notification").ShowAsync();
            }
        }

        private async void btnPayNow_Click(object sender, RoutedEventArgs e)
        {
            if(ProductVm.ProductOrderList.Count > 0)
            {
                MediateClass.KiosPage.Frame.Navigate(typeof(OrderPage));
            }
            else
            {
                await new MessageDialog("Your cart is NULL!", "Notification").ShowAsync();
            }
        }

        private void btnBackToShop_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
              
        
        private void tblCancel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (lvProductOrder.SelectedItem != null)
            {
                Product product = (Product)lvProductOrder.SelectedItem;
                if (ProductVm.ProductOrderList.IndexOf(product) != -1)
                {
                    ProductVm.ProductOrderList.Remove(product);
                }
            }
        }
    }
}
