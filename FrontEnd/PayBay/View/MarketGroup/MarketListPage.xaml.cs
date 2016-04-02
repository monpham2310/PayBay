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
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PayBay.View.MarketGroup
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MarketListPage : Page
    {
        private ObservableCollection<string> _dummyList;

        public ObservableCollection<string> DummyList
        {
            get
            {
                return _dummyList;
            }

            set
            {
                if (Equals(value, _dummyList)) return;
                _dummyList = value;
            }
        }

        public MarketListPage()
        {
            this.InitializeComponent();

            DummyList = new ObservableCollection<string>();
            for (int i = 0; i < 5; i++)
            {
                string haha = "blabla";
                DummyList.Add(haha);
            }
        }
    }
}
