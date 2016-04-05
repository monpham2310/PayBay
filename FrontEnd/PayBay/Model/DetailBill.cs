using PayBay.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBay.Model
{
    public class DetailBill : BaseViewModel
    {
        private int _id;
        private int _billId;
        private int _productId;
        private string _productName;
        private string _image;
        private int _numberOf;
        private double _unitPrice;
        private string _unit;

        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public int BillId
        {
            get
            {
                return _billId;
            }

            set
            {
                _billId = value;
                OnPropertyChanged();
            }
        }

        public int ProductId
        {
            get
            {
                return _productId;
            }

            set
            {
                _productId = value;
                OnPropertyChanged();
            }
        }

        public int NumberOf
        {
            get
            {
                return _numberOf;
            }

            set
            {
                _numberOf = value;
                OnPropertyChanged();
            }
        }

        public double UnitPrice
        {
            get
            {
                return _unitPrice;
            }

            set
            {
                _unitPrice = value;
                OnPropertyChanged();
            }
        }

        public string Unit
        {
            get
            {
                return _unit;
            }

            set
            {
                _unit = value;
                OnPropertyChanged();
            }
        }

        public string ProductName
        {
            get
            {
                return _productName;
            }

            set
            {
                _productName = value;
                OnPropertyChanged();
            }
        }

        public string Image
        {
            get
            {
                return _image;
            }

            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        public DetailBill()
        {

        }

        public DetailBill(int productId, int numberOf, double unitPrice, string unit)
        {
            _productId = productId;
            _numberOf = numberOf;
            _unitPrice = unitPrice;
            _unit = unit;
        }
    }
}
