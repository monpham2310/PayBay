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
using PayBay.ViewModel.ProductGroup;
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
    }
}
