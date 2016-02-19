using System.Collections.ObjectModel;
using System.Linq;
using PayBay.Model;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Net.Http;
using System;
using Windows.UI.Popups;

namespace PayBay.ViewModel.ProductGroup
{
    public class ProductViewModel : BaseViewModel
    {
        private Product _newProduct;
        private ObservableCollection<Product> _productList;

        #region Property with calling to PropertyChanged
        public Product NewProduct
        {
            get { return _newProduct; }
            set
            {
                if (Equals(value, _newProduct)) return;
                _newProduct = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Product> ProductList
        {
            get
            {
                return _productList;
            }

            set
            {
                if (Equals(value, _productList)) return;
                _productList = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public ProductViewModel()
        {
            InitializeData();
        }

        private void InitializeData()
        {
            _productList = new ObservableCollection<Product>();

            _newProduct = new Product();
            _newProduct.Image = "/Assets/Product/Beef.jpg";
            _newProduct.Name = "Thịt Bò";
            _newProduct.Price = "30000";
            _newProduct.SalePrice = "23000";
            _newProduct.StoreName = "Quầy Thịt";

            _productList.Add(_newProduct);

            Product secondProduct = new Product();
            secondProduct.Image = "/Assets/Product/RauMuong.jpg";
            secondProduct.Name = "Rau Muống";
            secondProduct.Price = "5000";
            secondProduct.SalePrice = "4000";
            secondProduct.StoreName = "Quầy Rau";
            _productList.Add(secondProduct);

            for (int i = 0; i < 5; i++)
            {
                Product thirdProduct = new Product();
                thirdProduct.Image = "/Assets/Product/Apple.jpg";
                thirdProduct.Name = "Táo";
                thirdProduct.Price = "25000";
                thirdProduct.SalePrice = "20000";
                thirdProduct.StoreName = "Quầy Trái Cây";
                _productList.Add(thirdProduct);
            }
        }
    }
}
