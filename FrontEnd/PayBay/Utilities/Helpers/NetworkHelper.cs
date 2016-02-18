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
        public static bool HasInternetConnection { get; private set; }

        public NetworkHelper()
        {
            NetworkInformation.NetworkStatusChanged += NetworkInformationOnNetworkStatusChanged;
            CheckInternetConnection();
        }

        private void NetworkInformationOnNetworkStatusChanged(object sender)
        {
            CheckInternetConnection();
        }

        public void CheckInternetConnection()
        {
            HasInternetConnection = NetworkInterface.GetIsNetworkAvailable();

            ConnectionProfile InternetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
            NetworkConnectivityLevel connection = InternetConnectionProfile.GetNetworkConnectivityLevel();
            if (connection == NetworkConnectivityLevel.None || connection == NetworkConnectivityLevel.LocalAccess
                || InternetConnectionProfile == null)
            {
                HasInternetConnection = false;
            }
        }
    }
}
