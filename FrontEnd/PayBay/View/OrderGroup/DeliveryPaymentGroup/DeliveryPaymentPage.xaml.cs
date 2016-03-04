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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PayBay.View.OrderGroup.DeliveryPaymentGroup
{
    public sealed partial class DeliveryPaymentPage : UserControl
    {
        public DeliveryPaymentPage()
        {
            this.InitializeComponent();
        }

        private void tbxInitPay_GotFocus(object sender, RoutedEventArgs e)
        {
            tblInitPay.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0xD1, 0x58, 0x58));
        }

        private void tbxInitPay_LostFocus(object sender, RoutedEventArgs e)
        {
            tblInitPay.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0x96, 0x94, 0x94));
        }

        private void tbxBalPay_GotFocus(object sender, RoutedEventArgs e)
        {
            tblBalPay.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0xD1, 0x58, 0x58));
        }

        private void tbxBalPay_LostFocus(object sender, RoutedEventArgs e)
        {
            tblBalPay.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0x96, 0x94, 0x94));
        }
    }
}
