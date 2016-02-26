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
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using PayBay.Model;
using System.Net.Http;
using PayBay.Utilities.Common;
using Windows.UI.Core;
using Windows.UI.Popups;
using PayBay.Utilities.Helpers;

namespace PayBay.View.TopFunctionGroup
{
    public sealed partial class HomePageNew : Page
    {
        public HomePageNew()
        {
            this.InitializeComponent();
        }
    }
}
