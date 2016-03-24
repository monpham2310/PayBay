using Microsoft.WindowsAzure.Messaging;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.Networking.PushNotifications;
using Windows.Storage;

namespace PayBay.Services.MobileServices.PaybayNotification
{
    internal class PaybayPushClient
    {
        public static PushNotificationChannel CurrentChannel { get; set; }

        public async static void UploadChannel() 
        {                                            
            try
            {
                //var hub = new NotificationHub("paybaynotification",
                //    "Endpoint=sb://paybay.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=9OM2zW2+YOksKqnCdLrv387FWy1b4SfUFWUiuJM5edk=");

                //var result =
                //    await hub.RegisterNativeAsync(
                //    channel.Uri.ToString());

                // Create a native push notification registration.              
                await App.MobileService.GetPush().RegisterNativeAsync(CurrentChannel.Uri);

            }
            catch (Exception exception)
            {
                HandleRegisterException(exception);
            }                                               
            
        }

        public async static void UploadChannel(int userId)
        {           
            try
            {
                //var hub = new NotificationHub("paybaynotification",
                //    "Endpoint=sb://paybay.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=9OM2zW2+YOksKqnCdLrv387FWy1b4SfUFWUiuJM5edk=");

                //var result =
                //    await hub.RegisterNativeAsync(
                //    channel.Uri.ToString());

                // Create a native push notification registration.   
                string[] tags = { userId.ToString() };
                ApplicationData.Current.LocalSettings.Values["tags"] = tags;
                await App.MobileService.GetPush().RegisterNativeAsync(CurrentChannel.Uri, tags);                

            }
            catch (Exception exception)
            {
                HandleRegisterException(exception);
            }

        }
               
        private async static void HandleRegisterException(Exception exception)
        {
            //await new MessageDialog(exception.Message.ToString(), "Notification!").ShowAsync();
        }
    }
}
