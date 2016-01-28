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
            FunctionsListView.SelectedIndex = 0;
            Vm.NavigateToFunction(MainFrame, MenuFunc.HomePage);
        }

        // Define a member variable for storing the signed-in user. 
        //private MobileServiceUser user;

        // Define a method that performs the authentication process
        // using a Facebook sign-in. 
        //private async System.Threading.Tasks.Task<bool> AuthenticateAsync()
        //{
        //    string message;
        //    bool success = false;
        //    try
        //    {
        //        // Change 'MobileService' to the name of your MobileServiceClient instance.
        //        // Sign-in using Facebook authentication.
        //        user = await App.MobileService
        //            .LoginAsync(MobileServiceAuthenticationProvider.Facebook);
        //        message =
        //            string.Format("You are now signed in - {0}", user.UserId);

        //        success = true;
        //    }
        //    catch (InvalidOperationException)
        //    {
        //        message = "You must log in. Login Required";
        //    }

        //    var dialog = new MessageDialog(message);
        //    dialog.Commands.Add(new UICommand("OK"));
        //    await dialog.ShowAsync();
        //    return success;
        //}

        //private async void ButtonLogin_Click(object sender, RoutedEventArgs e)
        //{
        //    // Login the user and then load data from the mobile app.
        //    //if (await AuthenticateAsync())
        //    {
        //        // Hide the login button and load items from the mobile app.
        //        ButtonLogin.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        //        //await InitLocalStoreAsync(); //offline sync support.
        //        //await RefreshTodoItems();
        //    }
        //}

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
            //MainSplitView.IsPaneOpen = false;
        }

        /// <summary>
        /// To deselect the bottome listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FunctionsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FunctionsListView.SelectedIndex != -1)
            {
                BottomListView.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// To deselect the function listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BottomListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BottomListView.SelectedIndex != -1)
            {
                FunctionsListView.SelectedIndex = -1;
            }
        }
    }
}
