using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PayBay.View.StartGroup;
using Windows.UI.Popups;
using PayBay.Utilities.Common;
using Windows.UI.Core;
using PayBay.Utilities.Helpers;
using PayBay.Services.MobileServices.PaybayNotification;
using Windows.Networking.PushNotifications;
using PayBay.Model;

namespace PayBay
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
            InitializeComponent();
            Suspending += OnSuspending;
        }
        
        //public static string UrlHost = "http://localhost:4591";
        public static string UrlHost = "https://paybayservice.azure-mobile.net/";
        private static string ApplicationKey = "OilbMshzaPgvERqbTfFtLLLFwlEHFl47";
                
        // This MobileServiceClient has been configured to communicate with your Mobile Service's url
        // and application key. You're all set to start working with your Mobile Service!
        public static MobileServiceClient MobileService = new MobileServiceClient(
            UrlHost, ApplicationKey
        );
               
        private async void AcquirePushChannel()
        {
            PaybayPushClient.CurrentChannel = await Windows.Networking.PushNotifications.PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
            PaybayPushClient.CurrentChannel.PushNotificationReceived += CurrentChannel_PushNotificationReceived;
        }

        private void CurrentChannel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            //lock (this)
            //{
            //    var messageContent = args.ToastNotification.Content;
            //    var textElements = messageContent.GetElementsByTagName("text");
            //    var imgElement = messageContent.GetElementsByTagName("image");
            //    var msgId = textElements.ElementAt(4).InnerText;
            //    var id = textElements.ElementAt(3).InnerText;
            //    var userId = textElements.ElementAt(5).InnerText;
            //    var ownerId = textElements.ElementAt(6).InnerText;
            //    var msgContent = textElements.ElementAt(1).InnerText;
            //    var msgDate = textElements.ElementAt(2).InnerText;
            //    var userName = textElements.ElementAt(0).InnerText;
            //    var avatar = imgElement.ElementAt(0).Attributes[1].InnerText;
            //    var chatMessage = new MessageInbox()
            //    {
            //        MessageId = int.Parse(msgId),
            //        UserId = int.Parse(userId),
            //        OwnerId = int.Parse(ownerId),
            //        UserName = userName,
            //        Avatar = avatar
            //    };

            //    var chatMessageDetail = new InboxDetail()
            //    {
            //        ID = int.Parse(id),
            //        MessageId = int.Parse(msgId),
            //        UserId = int.Parse(userId),
            //        UserName = userName,
            //        Avatar = avatar,
            //        Content = msgContent,
            //        InboxDate = Convert.ToDateTime(string.Format(msgDate, "MM/dd/yyyy HH:mm:ss"))
            //    };

            //    if (MediateClass.MessageVM.MessageList.Count(x => x.MessageId == chatMessage.MessageId) == 0)
            //        MediateClass.MessageVM.MessageList.Add(chatMessage);
            //    MediateClass.MsgDetailVM.DetailList.Insert(0, chatMessageDetail);
            //}
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            //TODO: Disable these line of code to remove FrameRateCounter
            //#if DEBUG
            //            if (System.Diagnostics.Debugger.IsAttached)
            //            {
            //                this.DebugSettings.EnableFrameRateCounter = true;
            //            }
            //#endif

            AcquirePushChannel();

            Frame rootFrame = Window.Current.Content as Frame;
            
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
                                
                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application

                }

				// Place the frame in the current Window
				Window.Current.Content = rootFrame;
                				
			}

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(StartPage), e.Arguments);
            }

            if (!NetworkHelper.Instance.HasInternetConnection)
            {
                await new MessageDialog("No internet connection is avaliable. The full functionality of the app isn't avaliable.").ShowAsync();
            }

            //Register device
            //PaybayPushClient.UploadChannel();

            // Ensure the current window is active
            Window.Current.Activate();                        
        }
                
        protected override void OnActivated(IActivatedEventArgs args)
        {
#if WINDOWS_PHONE_APP
            if (args.Kind == ActivationKind.WebAuthenticationBrokerContinuation)
            {
                // Completes the sign-in process started by LoginAsync.
                // Change 'MobileService' to the name of your MobileServiceClient instance. 
                App.MobileService.LoginComplete(args as WebAuthenticationBrokerContinuationEventArgs);
            }
#endif
            base.OnActivated(args);
        }
                
        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
