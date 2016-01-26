using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayBay.Model;

namespace PayBay.ViewModel.MarketGroup
{
    public class MarketViewModel : BaseViewModel
    {

        private Market _newMarket;

        #region Property with calling to PropertyChanged
        public Market NewMarket
        {
            get { return _newMarket; }
            set
            {
                if (Equals(value, _newMarket)) return;
                _newMarket = value;
                OnPropertyChanged();
            }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public MarketViewModel()
        {
            InitializeData();
        }

        /// <summary>
        /// Initialize market
        /// </summary>
        private void InitializeData()
        {
            NewMarket = new Market();
            NewMarket.Name = "Chợ Bến Thành";
            NewMarket.Address = "Phường Bến Thành, Quận 1, TP.HCM";
            NewMarket.Image = "/Assets/Market/ChoBenThanh/Main.jpg";
            NewMarket.Itemtypes = "Quần áo, Giày dép, Thực phẩm, Đồ khô...";
            NewMarket.Map = "/Assets/Market/ChoBenThanh/Map.png";
        }
    }
}
