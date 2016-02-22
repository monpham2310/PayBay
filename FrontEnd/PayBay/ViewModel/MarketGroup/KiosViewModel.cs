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
    public class KiosViewModel : BaseViewModel
    {
        private ObservableCollection<Kios> _kiosList;

        #region Property with calling to PropertyChanged
        public ObservableCollection<Kios> KiosList
        {
            get { return _kiosList; }
            set
            {
                if (Equals(value, _kiosList)) return;
                _kiosList = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public KiosViewModel()
        {
            InitializeData();
        }

        private void InitializeData()
        {
            _kiosList = new ObservableCollection<Kios>();
            for (int i = 0; i < 9; i++)
            {
                Kios k1 = new Kios();
                if (i < 5)
                {
                    k1.Avatar = "ms-appx:///Assets/Troll/namdog.jpg";
                    k1.Kiosname = "#DogStore";
                    k1.Name = "CoHoNam";
                    k1.Rating = 1.5f;
                }
                else
                {
                    k1.Avatar = "ms-appx:///Assets/Troll/huygay.png";
                    k1.Kiosname = "#GayStore";
                    k1.Name = "GayHuy";
                    k1.Rating = 1.5f;
                }
                _kiosList.Add(k1);

            }
        }
    }
}
