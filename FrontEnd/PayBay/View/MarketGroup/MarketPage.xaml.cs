﻿using PayBay.Utilities.Common;
using PayBay.ViewModel.MarketGroup;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PayBay.View.MarketGroup
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MarketPage : Page
	{
        private MarketViewModel MarketVm => (MarketViewModel)spnHeader.DataContext;
		public MarketPage()
		{
			this.InitializeComponent();
		}

        private void ShopNowButton_Click(object sender, RoutedEventArgs e)
        {            
            Frame.Navigate(typeof(KiosPage));
        }

        private void BackHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (MarketVm != null)
                MarketVm.SelectedMarket = null;
            Frame.GoBack();
        }
    }
}
