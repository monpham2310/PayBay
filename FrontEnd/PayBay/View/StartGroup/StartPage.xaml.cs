using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using PayBay.Model;
using PayBay.ViewModel.StartGroup;

namespace PayBay.View.StartGroup
{
    public sealed partial class StartPage
    {
        public StartViewModel Vm => (StartViewModel)DataContext;

        public StartPage()
        {
            InitializeComponent();

			//TODO: comment out 3 line of code below to return to defaul view: TitleBar is a white bar, do nothing
			//Set title bar
			//CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
			//coreTitleBar.ExtendViewIntoTitleBar = true;
			//Window.Current.SetTitleBar(TitleGrid);

			Loaded += StartPage_Loaded;
        }

        private void StartPage_Loaded(object sender, RoutedEventArgs e)
        {
            //Default page when open app
            TopFunctionsListView.SelectedIndex = 0;
            Vm.NavigateToFunction(MainFrame, MenuFunc.HomePage);
        }
               
        /// <summary>
        /// Open and close the paneview of SplitView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;            
        }

        /// <summary>
        /// Navigate subframe to te===
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuListItem_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            MenuListItem m = ((Grid)sender).Tag as MenuListItem;
            Debug.Assert(m != null, "m != null");
            Vm.NavigateToFunction(MainFrame, m.MenuF);
            MainSplitView.IsPaneOpen = false;
        }

        /// <summary>
        /// To deselect the bottom and middle listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopFunctionsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TopFunctionsListView.SelectedIndex != -1)
            {
				MiddleFunctionsListView.SelectedIndex = -1;
                BottomListView.SelectedIndex = -1;
            }
        }

		/// <summary>
		/// To deselect the top and bottom listview
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MiddleFunctionsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (MiddleFunctionsListView.SelectedIndex != -1)
			{
				TopFunctionsListView.SelectedIndex = -1;
				BottomListView.SelectedIndex = -1;
			}
		}

		/// <summary>
		/// To deselect the top and middle listview
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BottomListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BottomListView.SelectedIndex != -1)
            {
                TopFunctionsListView.SelectedIndex = -1;
				MiddleFunctionsListView.SelectedIndex = -1;
            }
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
