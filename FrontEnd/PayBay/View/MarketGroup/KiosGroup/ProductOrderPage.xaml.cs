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

namespace PayBay.View.MarketGroup.KiosGroup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductOrderPage : Page
    {
        public ProductOrderPage()
        {
            this.InitializeComponent();
        }

        private void btnSub_Click(object sender, RoutedEventArgs e)
        {
            var orderNum = Int32.Parse(tbOrderNum.Text) - 1;

            if (orderNum < 1)
            {
                orderNum = 1;
            }

            tbOrderNum.Text = orderNum.ToString();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var orderNum = Int32.Parse(tbOrderNum.Text) + 1;
            var stockNum = Int32.Parse(tblStock.Text);

            if (orderNum > stockNum)
            {
                orderNum = stockNum;
            }

            tbOrderNum.Text = orderNum.ToString();
        }

        private void tbOrderNum_LostFocus(object sender, RoutedEventArgs e)
        {
            int number;
            var numOrder = tbOrderNum.Text;
            var stockNum = Int32.Parse(tblStock.Text);

            if (!Int32.TryParse(numOrder, out number) || Int32.Parse(numOrder) < 1)
            {
                numOrder = "1";
            }
            else if (Int32.Parse(numOrder) > stockNum)
            {
                numOrder = stockNum.ToString();
            }

            tbOrderNum.Text = numOrder;
        }
    }
}
