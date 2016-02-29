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

        private void scrollvProductLst_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            int storeId = MediateClass.KiotVM.SelectedStore.StoreId;
            if(scrollvProductLst.VerticalOffset >= scrollvProductLst.ScrollableHeight)
            {
                if(ProductVm != null)
                {
                    ProductVm.GetProductsOfStore(storeId, TYPEGET.MORE);
                }
            }
            else if (scrollvProductLst.VerticalOffset == 0)
            {
                if (ProductVm != null)
                {
                    ProductVm.GetProductsOfStore(storeId, TYPEGET.MORE, TYPE.NEW);
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

        private void checkBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (Product product in listViewProductsOfStore.Items)
            {
                if (Int32.Parse(product.OrderUnit) > 0)
                {
                    ProductUnitOrder unitOrder = new ProductUnitOrder(product, Int32.Parse(product.OrderUnit));
                    ProductVm.ProductOrderList.Add(unitOrder);
                }
            }

            if (ProductVm.ProductOrderList.Count > 0)
                ((Frame)Window.Current.Content).Navigate(typeof(OrderPage));
        }
    }
}
