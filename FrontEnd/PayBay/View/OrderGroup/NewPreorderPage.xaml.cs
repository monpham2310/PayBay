using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PayBay.View.OrderGroup
{
    public sealed partial class NewPreorderPage : UserControl
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
                _dummyList = value;
            }
        }

        public NewPreorderPage()
        {
            this.InitializeComponent();

            DummyList = new ObservableCollection<string>();
            for (int i = 0; i < 10; i++)
                DummyList.Add("haha");
        }
    }
}
