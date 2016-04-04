using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using PayBay.Model;
using PayBay.ViewModel.StartGroup;
using System.Threading.Tasks;
using Windows.UI.Core;
using PayBay.View.AccountGroup;
using PayBay.Utilities.Common;
using PayBay.View.AppBarFunctionGroup;
using System;
using Windows.UI.Popups;
using PayBay.Utilities.Helpers;
using PayBay.ViewModel;
using PayBay.ViewModel.AccountGroup;
using PayBay.View.TopFunctionGroup;
using PayBay.View.InboxGroup;

namespace PayBay.View.StartGroup
{
	public sealed partial class StartPage
	{
		private StartViewModel Vm => (StartViewModel)DataContext;

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

        public void MoveHomePage()
        {
            MainFrame.Navigate(typeof(HomePage), NavigationMode.Back);
        }

		private void StartPage_Loaded(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigated += OnNavigated;
			MainFrame.NavigationFailed += OnNavigationFailed;

			// Register a handler for BackRequested events and set the
			// visibility of the Back button
			SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;

			SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
				MainFrame.CanGoBack ?
				AppViewBackButtonVisibility.Visible :
				AppViewBackButtonVisibility.Collapsed;

			//Default page when open app
			TopFunctionsListView.SelectedIndex = 0;
			Vm.NavigateToFunction(MainFrame, MenuFunc.HomePage);
            if(MediateClass.UserVM != null)
            {
                if(MediateClass.UserVM.UserInfo != null)
                    isLoginControl(true);
            }

			if (SettingsHelper.GetSetting<bool?>("Remember") == true)
			{
				AttempLogin();
			}
		}

		private async void AttempLogin()
		{
			UserInfoViewModel UserVm = ((ViewModelLocator)App.Current.Resources["Locator"]).AccountVm as UserInfoViewModel;
			String mail = SettingsHelper.GetSetting<String>("Mail");
			String password = SettingsHelper.GetSetting<String>("Password");

			bool check = await UserVm.LoginAccount(mail, password);

			if (check)
			{
				UserLoginSucceed();
			}
			else
			{
				await new MessageDialog("Attempted login is not successful", "Notification!").ShowAsync();
			}
		}

		private void OnBackRequested(object sender, BackRequestedEventArgs e)
		{
			if (MainFrame.CanGoBack)
			{
				e.Handled = true;
				MainFrame.GoBack();
			}
		}

		/// <summary>
		/// Invoked when Navigation to a certain page and update the Back button's visibility
		/// </summary>
		/// <param name="sender">The Frame which navigate</param>
		/// <param name="e"></param>
		private void OnNavigated(object sender, NavigationEventArgs e)
		{
            //if (((NavigationMode)e.Parameter) == NavigationMode.New)
            //{
            //	SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            //}
            //else
            //{
            //	SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            //}
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                ((Frame)sender).CanGoBack ?
                AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;

        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
		{
			throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
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
			}
		}

		private async void AccountButton_Click(object sender, RoutedEventArgs e)
		{
            if (MediateClass.UserVM.UserInfo != null)
            {
                showPopup();
                UserInfoViewModel.isUpdate = true;
                AccountFrame.Navigate(typeof(CreateAccountPage));
            }
            else
                await new MessageDialog("You are not login!", "Notification!").ShowAsync();
        }

		private void MainSplitView_PaneClosed(SplitView sender, object args)
		{
		}

        public void isLoginControl(bool visibility)
        {
            bool isLogin = visibility;
            bool isNotLogin = !visibility;

            AvatarEllipse.Visibility = (isNotLogin) ? Visibility.Visible : Visibility.Collapsed;
            SignInButton.Visibility = (isNotLogin) ? Visibility.Visible : Visibility.Collapsed;

            UserAvatarElipse.Visibility = (isLogin) ? Visibility.Visible : Visibility.Collapsed;
            UserInfoViewButton.Visibility = (isLogin) ? Visibility.Visible : Visibility.Collapsed;
        }

		public void UserLoginSucceed()
		{            
            if (MediateClass.UserVM.UserInfo != null)
			{
                isLoginControl(true);
                MediateClass.StartVM.EnableFunction(MediateClass.UserVM.UserInfo.TypeId);
            }
		}

		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{                          
			MainFrame.Navigate(typeof(SearchPage), NavigationMode.Forward);
		}

        private void showPopup()
        {
            AccountPopup.IsOpen = true;
            MainSplitView.IsPaneOpen = false;
            MainGrid.IsHitTestVisible = false;
            MainGrid.Opacity = 0.4;

            ProcessPopupSizeAndPos();
        }

		private void SignInButton_Click(object sender, RoutedEventArgs e)
		{
            showPopup();

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
        		
		private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			ProcessPopupSizeAndPos();
		}

		private void ProcessPopupSizeAndPos()
		{
			AccountFrame.Height = ActualHeight * 0.85;
			AccountFrame.Width = AccountFrame.Height / 1.5;

			if (AccountFrame.Width < 330)
				AccountFrame.Width = 330;

			AccountPopup.HorizontalOffset = (ActualWidth - AccountFrame.Width) / 2;
			AccountPopup.VerticalOffset = (ActualHeight - AccountFrame.Height) / 2;
		}

        private async void InboxButton_Click(object sender, RoutedEventArgs e)
        {
            if (MediateClass.UserVM != null)
            {
                if (MediateClass.UserVM.UserInfo != null)
                {                    
                    MainFrame.Navigate(typeof(InboxListPage), NavigationMode.Forward);
                }
                else
                {
                    await new MessageDialog("Login is required!", "Notification!").ShowAsync();
                }
            }
        }

        private void UserInfoViewButton_Click(object sender, RoutedEventArgs e)
        {
            showPopup();
            UserInfoViewModel.isUpdate = true;
            AccountFrame.Navigate(typeof(CreateAccountPage));            
        }

    }

}
