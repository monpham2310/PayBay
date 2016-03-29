using PayBay.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBay.Model
{
    public class ProductStatistic : BaseViewModel
    {
        private int id;
        private int billID;
        private int productID;
        private string productName;
        private string image;
        private int numberOf;
        private double unitPrice;
        private string unit;
        private DateTime saleDate;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public int BillID
        {
            get
            {
                return billID;
            }

            set
            {
                billID = value;
                OnPropertyChanged();
            }
        }

        public int ProductID
        {
            get
            {
                return productID;
            }

            set
            {
                productID = value;
                OnPropertyChanged();
            }
        }

        public int NumberOf
        {
            get
            {
                return numberOf;
            }

            set
            {
                numberOf = value;
                OnPropertyChanged();
            }
        }

        public double UnitPrice
        {
            get
            {
                return unitPrice;
            }

            set
            {
                unitPrice = value;
                OnPropertyChanged();
            }
        }

        public string Unit
        {
            get
            {
                return unit;
            }

            set
            {
                unit = value;
                OnPropertyChanged();
            }
        }

        public DateTime SaleDate
        {
            get
            {
                return saleDate;
            }

            set
            {
                saleDate = value;
                OnPropertyChanged();
            }
        }

        public string ProductName
        {
            get
            {
                return productName;
            }

            set
            {
                productName = value;
                OnPropertyChanged();
            }
        }

        public string Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
                OnPropertyChanged();
            }
        }
    }
}
