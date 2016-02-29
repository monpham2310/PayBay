using Microsoft.WindowsAzure.Messaging;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace PayBay.Services.MobileServices.PaybayNotification
{
    internal class PaybayPushClient
    {
        public async static void UploadChannel() 
        {
            try
            {
                var channel = await Windows.Networking.PushNotifications.PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

                //var hub = new NotificationHub("paybaynotification",
                //    "Endpoint=sb://paybay.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=9OM2zW2+YOksKqnCdLrv387FWy1b4SfUFWUiuJM5edk=");

                //var result =
                //    await hub.RegisterNativeAsync(
                //    channel.Uri.ToString());

                try
                {
                    // Create a native push notification registration.
                    await App.MobileService.GetPush().RegisterNativeAsync(channel.Uri);

                }
                catch (Exception exception)
                {
                    HandleRegisterException(exception);
                }

                await App.MobileService.GetPush().RegisterNativeAsync(channel.Uri);
            }
            catch (Exception exception)
            {
                HandleRegisterException(exception);
            }
        }
                
        private async static void HandleRegisterException(Exception exception)
        {
            await new MessageDialog(exception.Message.ToString(), "Notification!").ShowAsync();
        }
    }
}
