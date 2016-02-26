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
using PayBay.Utilities.Common;

namespace PayBay.ViewModel.PlaceholderNewHomepageGroup
{

    public class Sale : BaseViewModel
    {
        private String _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                if (value == _imagePath) return;
                _imagePath = value;
                OnPropertyChanged();
            }
        }
    }

    public class PlaceholderNewHomepageViewModel : BaseViewModel
    {
        private ObservableCollection<Sale> _saleList;
        private ObservableCollection<Sale> _saleList2;

        #region Property with calling to PropertyChanged
        public ObservableCollection<Sale> SaleList
        {
            get { return _saleList; }
            set
            {
                if (Equals(value, _saleList)) return;
                _saleList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Sale> SaleList2
        {
            get { return _saleList2; }
            set
            {
                if (Equals(value, _saleList2)) return;
                _saleList2 = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public PlaceholderNewHomepageViewModel()
        {
            InitializeData();
        }

        private void InitializeData()
        {
            _saleList = new ObservableCollection<Sale>();
            Sale sale1 = new Sale();
            sale1.ImagePath = "ms-appx:///Assets/PlaceholderAssetForNewHomepage/QuangCao.png";
            Sale sale2 = new Sale();
            sale2.ImagePath = "ms-appx:///Assets/PlaceholderAssetForNewHomepage/QuangCao2.jpg";
            Sale sale3 = new Sale();
            sale3.ImagePath = "ms-appx:///Assets/PlaceholderAssetForNewHomepage/QuangCao3.png";
            _saleList.Add(sale1);
            _saleList.Add(sale2);
            _saleList.Add(sale3);

            _saleList2 = new ObservableCollection<Sale>();
            Sale sale4 = new Sale();
            sale4.ImagePath = "ms-appx:///Assets/PlaceholderAssetForNewHomepage/QuangCao4.jpg";
            Sale sale5 = new Sale();
            sale5.ImagePath = "ms-appx:///Assets/PlaceholderAssetForNewHomepage/QuangCao5.jpg";
            _saleList2.Add(sale4);
            _saleList2.Add(sale5);
        }
    }
}
