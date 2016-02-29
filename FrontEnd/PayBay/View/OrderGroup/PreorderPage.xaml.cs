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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PayBay.View.OrderGroup
{
    public class FakeData : BaseViewModel
    {
        private string _productname;
        private int _price;
        private int _amount;
        private string _seller;
        private int _id;

        public string ProductName
        {
            get { return _productname; }
            set
            {
                if (value == _productname) return;
                _productname = value;
                OnPropertyChanged();
            }
        }

        public string Seller
        {
            get { return _seller; }
            set
            {
                if (value == _seller) return;
                _seller = value;
                OnPropertyChanged();
            }
        }

        public int Price
        {
            get { return _price; }
            set
            {
                if (value == _price) return;
                _price = value;
                OnPropertyChanged();
            }
        }

        public int Amount
        {
            get { return _amount; }
            set
            {
                if (value == _amount) return;
                _amount = value;
                OnPropertyChanged();
            }
        }

        public int Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }
    }

    public class FakeDataVm : BaseViewModel
    {
        private ObservableCollection<FakeData> _fakelist;

        #region Property with calling to PropertyChanged
        public ObservableCollection<FakeData> FakeList
        {
            get { return _fakelist; }
            set
            {
                if (Equals(value, _fakelist)) return;
                _fakelist = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public FakeDataVm()
        {
            _fakelist = new ObservableCollection<FakeData>();
            for (int i=0; i < 5; i++)
            {
                FakeData fakeitem = new FakeData();
                fakeitem.Id = i + 1;
                fakeitem.ProductName = "BaConSoi";
                fakeitem.Price = 50000;
                fakeitem.Seller = "Huy";
                fakeitem.Amount = 100;
                _fakelist.Add(fakeitem);
            }
        }
    }

    public sealed partial class PreorderPage : UserControl
    {

        public PreorderPage()
        {
            this.InitializeComponent();
        }
    }
}
