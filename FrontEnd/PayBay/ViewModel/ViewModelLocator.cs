/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:BrandedApp"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using PayBay.ViewModel.StartGroup;
using PayBay.ViewModel.HomePageGroup;
using PayBay.ViewModel.MarketGroup;
using PayBay.ViewModel.AccountGroup;
using PayBay.ViewModel.ProductGroup;
using PayBay.ViewModel.CommentGroup;
using PayBay.ViewModel.RatingGroup;
using PayBay.View.OrderGroup;
using PayBay.ViewModel.OrderGroupViewModel;
using PayBay.ViewModel.InboxGroup;

namespace PayBay.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            //TODO: Resiger for a ViewModel here, syntax as below
            SimpleIoc.Default.Register<StartViewModel>();
			SimpleIoc.Default.Register<AdvertiseViewModel>();
            SimpleIoc.Default.Register<MarketViewModel>();
            SimpleIoc.Default.Register<UserInfoViewModel>();
            SimpleIoc.Default.Register<ProductViewModel>();
            SimpleIoc.Default.Register<KiosViewModel>();
            SimpleIoc.Default.Register<CommentViewModel>();
            SimpleIoc.Default.Register<RatingViewModel>();
            SimpleIoc.Default.Register<OrderViewModel>();
            SimpleIoc.Default.Register<MessageInboxViewModel>();
        }

        //TODO: Register to use ViewModel here, syntax as below
        public StartViewModel StartVm => ServiceLocator.Current.GetInstance<StartViewModel>();
		public AdvertiseViewModel AdvertiseVm => ServiceLocator.Current.GetInstance<AdvertiseViewModel>();
        public MarketViewModel MarketVm => ServiceLocator.Current.GetInstance<MarketViewModel>();
        public UserInfoViewModel AccountVm => ServiceLocator.Current.GetInstance<UserInfoViewModel>();
        public ProductViewModel ProductVm => ServiceLocator.Current.GetInstance<ProductViewModel>();
        public KiosViewModel KiosVm => ServiceLocator.Current.GetInstance<KiosViewModel>();
        public CommentViewModel CommentVm => ServiceLocator.Current.GetInstance<CommentViewModel>();
        public RatingViewModel RatingVm => ServiceLocator.Current.GetInstance<RatingViewModel>();
        public OrderViewModel OrderVm => ServiceLocator.Current.GetInstance<OrderViewModel>();
        public MessageInboxViewModel MessageVm => ServiceLocator.Current.GetInstance<MessageInboxViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}