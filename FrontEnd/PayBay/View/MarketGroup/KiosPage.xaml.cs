using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PayBay.ViewModel.AccountGroup;
using PayBay.Model;
using Newtonsoft.Json.Linq;
using PayBay.Utilities.Common;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PayBay.Utilities.Handler;

namespace PayBay.View.MarketGroup
{
    public sealed partial class KiosPage : Page
    {

        public KiosPage()
        {
            this.InitializeComponent();
        }

        private void BackHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
