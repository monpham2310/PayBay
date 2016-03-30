using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PayBay.ViewModel.ProductGroup;
using PayBay.View.OrderGroup;
using PayBay.Model;
using PayBay.Utilities.Common;
using Windows.UI.Popups;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PayBay.View.MarketGroup.KiosGroup
{
	public sealed partial class KiosProductPage : UserControl
	{
		private ProductViewModel ProductVm => (ProductViewModel)DataContext;

		public KiosProductPage()
		{
			this.InitializeComponent();
		}

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (ProductVm != null)
            {
                ProductVm.GetProductsOfStore(TYPEGET.START);
            }
        }

        private void scrollvProductLst_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {            
            if(scrollvProductLst.VerticalOffset >= scrollvProductLst.ScrollableHeight)
            {
                if(ProductVm != null)
                {
                    ProductVm.GetProductsOfStore(TYPEGET.MORE);
                }
            }
            else if (scrollvProductLst.VerticalOffset == 0)
            {
                if (ProductVm != null)
                {
                    ProductVm.GetProductsOfStore(TYPEGET.MORE, TYPE.NEW);
                }
            }
        }

        public T FindDescendant<T>(DependencyObject obj) where T : DependencyObject
        {
            // Check if this object is the specified type
            if (obj is T)
                return obj as T;

            // Check for children
            int childrenCount = VisualTreeHelper.GetChildrenCount(obj);
            if (childrenCount < 1)
                return null;

            // First check all the children
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child is T)
                    return child as T;
            }

            // Then check the childrens children
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = FindDescendant<T>(VisualTreeHelper.GetChild(obj, i));
                if (child != null && child is T)
                    return child as T;
            }

            return null;
        }

        private async void checkBtn_Click(object sender, RoutedEventArgs e)
        {            
            ProductVm.ProductOrderList.Clear();
            int checkCount = 0;
            int checkIsOut = 0;
            foreach (Product product in listViewProductsOfStore.Items)
            {
                if (product.OrderUnit > 0)
                {
                    if (product.OrderUnit <= product.NumberOf)
                        ProductVm.ProductOrderList.Add(product);
                    else
                        checkIsOut++;
                }
                else
                {
                    checkCount++;
                }
            }
            if(checkCount == listViewProductsOfStore.Items.Count)
            {
                await new MessageDialog("You haven't select product.Please check again!", "Notification!").ShowAsync();
                return;
            }
            if(checkIsOut > 0)
            {
                await new MessageDialog("Have "+ checkIsOut +" products is out.Please check again!", "Notification!").ShowAsync();
                return;
            }

            if (ProductVm.ProductOrderList.Count > 0)
                MediateClass.KiosPage.Frame.Navigate(typeof(OrderPage));
        }
                
    }
}
