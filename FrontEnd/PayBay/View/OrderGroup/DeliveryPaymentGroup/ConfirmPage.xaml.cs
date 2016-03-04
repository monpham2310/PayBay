using PayBay.Utilities.Common;
using PayBay.ViewModel.OrderGroupViewModel;
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
    public sealed partial class ConfirmPage : UserControl
    {
        private OrderViewModel OrderVm => (OrderViewModel)DataContext;

        public ConfirmPage()
        {
            this.InitializeComponent();
        }

        private async void btSubmit_Click(object sender, RoutedEventArgs e)
        {
            MediateClass.OrderPage.ShowProgressBar();     
            if (OrderVm != null)
            {
                await OrderVm.SubmitBill();
            }
            MediateClass.OrderPage.HiddenProgressBar();
            MediateClass.OrderPage.Frame.GoBack();
        }
    }
}
