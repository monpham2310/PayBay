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
            
        }

        //TODO: Register to use ViewModel here, syntax as below
        public StartViewModel StartVm => ServiceLocator.Current.GetInstance<StartViewModel>();
		public AdvertiseViewModel AdvertiseVm => ServiceLocator.Current.GetInstance<AdvertiseViewModel>();
        

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}