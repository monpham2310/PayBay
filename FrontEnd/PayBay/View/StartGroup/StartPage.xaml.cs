using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using PayBay.Model;
using PayBay.ViewModel.StartGroup;
using System.Threading.Tasks;
using Windows.UI.Core;
using PayBay.View.AccountGroup;
using PayBay.Common;
using PayBay.View.AppBarFunctionGroup;

namespace PayBay.View.StartGroup
{
    public sealed partial class StartPage
    {
        public StartViewModel Vm => (StartViewModel)DataContext;

        public StartPage()
        {
            InitializeComponent();
            MediateClass.StartPage = this;
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
            asbSearch.IsEnabled = true;         
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

        private void MainSplitView_PaneClosed(SplitView sender, object args)
        {
            asbSearch.IsEnabled = false;
        }

        public void UserLoginSucceed()
        {
            if (Vm.UserLogin != null)
            {
                AvatarEllipse.Visibility = Visibility.Collapsed;
                SignInButton.Visibility = Visibility.Collapsed;
                UserAvatarElipse.Visibility = Visibility.Visible;
                UserInfoViewButton.Visibility = Visibility.Visible;

                if (Vm.UserLogin.Avatar == null)
                {
                    Vm.UserLogin.Avatar = "/Assets/Square150x150Logo.scale-100.png";
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchPage));
        }

		private void SignInButton_Click(object sender, RoutedEventArgs e)
		{
			MainSplitView.IsPaneOpen = false;
			AccountPopup.IsOpen = true;

			MainGrid.Opacity = 0.4;
			MainGrid.IsHitTestVisible = false;

			AccountFrame.Height = ActualHeight * 0.8;
			AccountFrame.Width = AccountFrame.Height / 1.5;

			AccountPopup.HorizontalOffset = (ActualWidth - AccountFrame.Width) / 2;
			AccountPopup.VerticalOffset = (ActualHeight - AccountFrame.Height) / 2;

			AccountFrame.Navigate(typeof(SignInPage));
                        
		}

		private void JoinFreeButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(CreateAccountPage));
		}

		private void AccountPopup_Closed(object sender, object e)
		{
			MainGrid.Opacity = 1.0;
			MainGrid.IsHitTestVisible = true;
		}

        private void UserInfoViewButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
