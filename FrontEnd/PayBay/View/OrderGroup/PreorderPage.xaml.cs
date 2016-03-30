using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections.ObjectModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PayBay.ViewModel;
using PayBay.ViewModel.ProductGroup;
using PayBay.Utilities.Common;
using PayBay.ViewModel.OrderGroupViewModel;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PayBay.View.OrderGroup
{    
    public sealed partial class PreorderPage : UserControl
    {

        private ProductViewModel ProductVm => (ProductViewModel)DataContext;
        private OrderViewModel OrderVm => (OrderViewModel)spnPrice.DataContext;

        public PreorderPage()
        {
            this.InitializeComponent();                 
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btCallNegotiation_Click(object sender, RoutedEventArgs e)
        {
            Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI(MediateClass.KiotVM.SelectedStore.Phone, MediateClass.KiotVM.SelectedStore.StoreName);
        }
                
    }
}
