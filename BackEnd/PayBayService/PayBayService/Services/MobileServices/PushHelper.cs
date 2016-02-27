
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.ServiceBus.Notifications;
using System.Configuration;

namespace PayBayService.Services.MobileServices
{
    public static class PushHelper
    {
        public static async Task SendToastAsync(this ApiServices Services,string content)
        {
            // Define a native MPNS push notification.
            var payload = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                             @"<toast><visual><binding template=""ToastText01"">" +
                             @"<text id=""1"">" + content + @"</text>" +
                             @"</binding></visual></toast>";

            // Get the notification hub name from the service settings.
            var hubName = ConfigurationManager.AppSettings["MS_NotificationHubName"].ToString();

            // Get the connection string for the notification hub
            // from the mobile service runtime settings.
            string hubConnectionString = ConfigurationManager.ConnectionStrings["MS_NotificationHubConnectionString"].ToString();
            if (hubConnectionString != "")
            {
                // Create a new hubs client. 
                NotificationHubClient hub =
                    NotificationHubClient.CreateClientFromConnectionString(
                    hubConnectionString, hubName);
                var result = await hub.SendMpnsNativeNotificationAsync(payload);
                //// Log the result of the push notification.
                //Services.Log.Info(string.Format("Notification:{0} -state: {1}", 
                // result.TrackingId.ToString(), result.State.ToString()));
            }                        
        }
        public static async Task SendToastAsync(this ApiServices services, string title, string content)
        {
            WindowsPushMessage message = new WindowsPushMessage
            {
                XmlPayload = "<toast>\n" +
                             " <visual>\n" +
                             " <binding template=\"ToastText03\">\n" +
                             $" <text id=\"1\">{title}</text>\n" +
                             $" <text id=\"2\">{content}</text>\n" +
                             " </binding> \n" +
                             " </visual>\n" +
                             "</toast>"
            };
            try
            {
                var result = await services.Push.SendAsync(message);
                services.Log.Info(result.State.ToString());
            }
            catch (System.Exception ex)
            {
                services.Log.Error(ex.Message, null, "Push.SendAsync Error");
            }
        }
        public static async Task SendToastAsync(this ApiServices services, string content, IEnumerable<string> tags)
        {
            WindowsPushMessage message = new WindowsPushMessage
            {
                XmlPayload = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                             @"<toast><visual><binding template=""ToastText01"">" +
                             @"<text id=""1"">" + content + @"</text>" +
                             @"</binding></visual></toast>"
            };
            try
            {
                var result = await services.Push.SendAsync(message, tags);
                services.Log.Info(result.State.ToString());
            }
            catch (System.Exception ex)
            {
                services.Log.Error(ex.Message, null, "Push.SendAsync Error");
            }
        }
        public static async Task SendToastAsync(this ApiServices services, string title, string content, IEnumerable<string> tags)
        {
            WindowsPushMessage message = new WindowsPushMessage
            {
                XmlPayload = "<toast>\n" +
                             " <visual>\n" +
                             " <binding template=\"ToastText03\">\n" +
                             $" <text id=\"1\">{title}</text>\n" +
                             $" <text id=\"2\">{content}</text>\n" +
                             " </binding> \n" +
                             " </visual>\n" +
                             "</toast>"
            };
            try
            {
                var result = await services.Push.SendAsync(message, tags);
                services.Log.Info(result.State.ToString());
            }
            catch (System.Exception ex)
            {
                services.Log.Error(ex.Message, null, "Push.SendAsync Error");
            }
        }
        public static async Task SendToastAsync(this ApiServices services, string title, string content, string tag)
        {
            WindowsPushMessage message = new WindowsPushMessage
            {
                XmlPayload = "<toast>\n" +
                             " <visual>\n" +
                             " <binding template=\"ToastText03\">\n" +
                             $" <text id=\"1\">{title}</text>\n" +
                             $" <text id=\"2\">{content}</text>\n" +
                             " </binding> \n" +
                             " </visual>\n" +
                             "</toast>"

            };
            try
            {
                var result = await services.Push.SendAsync(message, tag);
                services.Log.Info(result.State.ToString());
            }
            catch (System.Exception ex)
            {
                services.Log.Error(ex.Message, null, "Push.SendAsync Error");
            }
        }
        public static async Task SendToastAsync(this ApiServices services, string title, string content, Uri image, string tag)
        {
            WindowsPushMessage message = new WindowsPushMessage
            {
                XmlPayload = "<toast>\n" +
                             " <visual>\n" +
                             " <binding template=\"ToastImageAndText03\">\n" +
                             $" <image id=\"1\" src=\"{image}\" alt=\"image1\"/>\n" +
                             $" <text id=\"1\">{title}</text>\n" +
                             $" <text id=\"2\">{content}</text>\n" +
                             " </binding> \n" +
                             " </visual>\n" +
                             "</toast>"
            };
            try
            {
                var result = await services.Push.SendAsync(message, tag);
                services.Log.Info(result.State.ToString());
            }
            catch (System.Exception ex)
            {
                services.Log.Error(ex.Message, null, "Push.SendAsync Error");
            }
        }
        public static async Task SendToastAsync(this ApiServices services, string title, string content, Uri image, IEnumerable<string> tags)
        {
            WindowsPushMessage message = new WindowsPushMessage
            {
                XmlPayload = "<toast>\n" +
                             " <visual>\n" +
                             " <binding template=\"ToastImageAndText03\">\n" +
                             $" <image id=\"1\" src=\"{image}\" alt=\"image1\"/>\n" +
                             $" <text id=\"1\">{title}</text>\n" +
                             $" <text id=\"2\">{content}</text>\n" +
                             " </binding> \n" +
                             " </visual>\n" +
                             "</toast>"
            };
            try
            {
                var result = await services.Push.SendAsync(message, tags);
                services.Log.Info(result.State.ToString());
            }
            catch (System.Exception ex)
            {
                services.Log.Error(ex.Message, null, "Push.SendAsync Error");
            }
        }

    }
}