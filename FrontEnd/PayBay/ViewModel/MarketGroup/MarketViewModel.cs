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

namespace PayBay.ViewModel.MarketGroup
{
    public class MarketViewModel : BaseViewModel
    {

        private Market _newMarket;
        private ObservableCollection<Market> _marketItemList;

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

        public ObservableCollection<Market> MarketItemList
        {
            get
            {
                return _marketItemList;
            }

            set
            {
                if (Equals(value, _marketItemList)) return;
                _marketItemList = value;
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

            _marketItemList = new ObservableCollection<Market>();

            NewMarket = new Market();
            NewMarket.Name = "Chợ Bến Thành";
            NewMarket.Address = "Phường Bến Thành, Quận 1, TP.HCM";
            NewMarket.Image = "/Assets/Market/ChoBenThanh/Main.jpg";
            NewMarket.Itemtypes = "Quần áo, Giày dép, Thực phẩm, Đồ khô...";
            NewMarket.Map = "/Assets/Market/ChoBenThanh/Map.png";
            NewMarket.Phone = "0932273623";
            _marketItemList.Add(NewMarket);

            for (int i = 0; i < 5; i++)
            {
                Market secondMarket = new Market();
                secondMarket.Name = "Chợ Bánh Thền";
                secondMarket.Address = "Phường Bánh Thền, Quận 100, TP.NNT";
                secondMarket.Image = "/Assets/Market/ChoBenThanh/Main.jpg";
                secondMarket.Itemtypes = "Bánh mì, Bút chì, Hủ tiếu mì...";
                secondMarket.Map = "/Assets/Market/ChoBenThanh/Map.png";
                secondMarket.Phone = "0938393592";
                _marketItemList.Add(secondMarket);
            }

        }
    }
}
