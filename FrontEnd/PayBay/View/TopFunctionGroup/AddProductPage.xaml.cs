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
using PayBay.Model;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PayBay.View.TopFunctionGroup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddProductPage : Page
    {
        public ObservableCollection<Product> _productList;

        public ObservableCollection<Product> ProductList
        {
            get { return _productList; }
            set
            {
                if (Equals(value, _productList)) return;
                _productList = value;
            }
        }

        public AddProductPage()
        {
            this.InitializeComponent();
            InitializeDummyData();
        }

        private void InitializeDummyData()
        {
            _productList = new ObservableCollection<Product>();
            for(int i=0; i < 5; i++)
            {
                Product product = new Product();
                product.ProductId = i;
                product.ProductName = "Product " + i;
                product.UnitPrice = i * 5000;
                product.NumberOf = i * 10;
                _productList.Add(product);
            }
        }
    }
}
