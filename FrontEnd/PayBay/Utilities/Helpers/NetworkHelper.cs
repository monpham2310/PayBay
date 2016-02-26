using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace PayBay.Utilities.Helpers
{
    public class NetworkHelper
    {        
        private static NetworkHelper _networkAvailabilty;
        public static NetworkHelper Instance
        {
            get { return _networkAvailabilty ?? (_networkAvailabilty = new NetworkHelper()); }
            set { _networkAvailabilty = value; }
        }

        private bool _hasInternetConnection;
        public event Action<bool> OnNetworkAvailabilityChange = delegate { };

        public bool HasInternetConnection
        {
            get
            {
                return _hasInternetConnection;
            }
            protected set
            {
                if (_hasInternetConnection == value) return;
                _hasInternetConnection = value;
                OnNetworkAvailabilityChange(value);
            }
        }

        private void CheckInternetAccess()
        {
            var connectionProfile = NetworkInformation.GetInternetConnectionProfile();
            HasInternetConnection = (connectionProfile != null &&
                                 connectionProfile.GetNetworkConnectivityLevel() ==
                                 NetworkConnectivityLevel.InternetAccess);            
        }

        private void NetworkInformationOnNetworkStatusChanged(object sender)
        {
            CheckInternetAccess();            
        }

        private NetworkHelper()
        {
            NetworkInformation.NetworkStatusChanged += new NetworkStatusChangedEventHandler(NetworkInformationOnNetworkStatusChanged);
            CheckInternetAccess();
        }

    }
}
