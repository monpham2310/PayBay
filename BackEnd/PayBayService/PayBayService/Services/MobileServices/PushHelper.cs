
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
                // Get the logged-in user.
                //var currentUser = this.User as ServiceUser;

                // Use a tag to only send the notification to the logged-in user.
                //var result = await services.Push.SendAsync(message, currentUser.Id);

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