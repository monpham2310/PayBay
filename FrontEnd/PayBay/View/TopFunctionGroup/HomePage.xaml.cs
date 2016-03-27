﻿using System;
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
using PayBay.ViewModel.HomePageGroup;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using PayBay.Model;
using System.Net.Http;
using PayBay.Utilities.Common;
using Windows.UI.Core;
using Windows.UI.Popups;
using PayBay.Utilities.Helpers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PayBay.View.TopFunctionGroup
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class HomePage : Page
	{
		private int change = 1;
		public AdvertiseViewModel AdVm => (AdvertiseViewModel)DataContext;

		public HomePage()
		{
			this.InitializeComponent();               			
		}

		private void FlipViewLoop()
        {
			
			DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += (o, a) =>
            {
                // If we'd go out of bounds then reverse
                //int newIndex = flipViewAdvertise.SelectedIndex + change;
                if (flipViewAdvertise.SelectedIndex >= flipViewAdvertise.Items.Count - 1)
                {
                    //change *= -1;
                    flipViewAdvertise.SelectedIndex = 0;
                }
                else
                {
                    flipViewAdvertise.SelectedIndex += change;
                }                
            };

            timer.Start();
        }
        
		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
            //FlipViewLoop();
            AdVm.InitializeDataFromDB();
		}

        private void ToBlankPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MyFavoritesPage), NavigationMode.Forward);
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(AddMarketPage));
		}

		private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			Grid grid = sender as Grid;

			grid.Height = grid.ActualWidth;
		}
	}
}
