using PayBay.Utilities.Common;
using PayBay.ViewModel.MarketGroup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace PayBay.View.OrderGroup
{
    public sealed partial class OrderPage : Page
    {
        public OrderPage()
        {
            if (MediateClass.OrderVM != null)
                MediateClass.OrderVM.InitializeBill();
            MediateClass.OrderPage = this;
            this.InitializeComponent();            
        }
                
        public void pivotOrder_SelectionChanged(object sender,SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0].Equals(PivotItem1))
            {
                pivotOrder.SelectedIndex = 0;
            }

            ProgressBar progbar = ((ProgressBar)VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(pivotOrder, 0), 1), 0), 0), 0), 0), 0), 1), 0), 0), 0));

            switch (pivotOrder.SelectedIndex)
            {
                case 0:
                    progbar.Value = 0;
                    break;
                case 1:
                    progbar.Value = 50f;
                    break;
                case 2:
                    progbar.Value = 100f;
                    break;
                default:
                    progbar.Value = 0;
                    break;
            }

        }

        public void ShowProgressBar()
        {
            pgrBill.IsActive = true;
            gridOrder.IsHitTestVisible = false;
            gridOrder.Opacity = 0.7;
        }

        public void HiddenProgressBar()
        {
            pgrBill.IsActive = false;
            gridOrder.IsHitTestVisible = true;
            gridOrder.Opacity = 1.0;
        }

    }
}
