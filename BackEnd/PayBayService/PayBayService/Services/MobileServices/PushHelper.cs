
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Notifications;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using Microsoft.ServiceBus.Notifications;
using System.Configuration;
using Newtonsoft.Json.Linq;

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
            string XmlPayload = "<toast>\n" +
                             " <visual>\n" +
                             " <binding template=\"ToastImageAndText03\">\n" +
                             $" <image id=\"1\" src=\"{image}\" alt=\"image1\"/>\n" +
                             $" <text id=\"1\">{title}</text>\n" +
                             $" <text id=\"2\">{content}</text>\n" +
                             " </binding> \n" +
                             " </visual>\n" +
                             "</toast>";
            //WindowsPushMessage message = new WindowsPushMessage
            //{
            //    XmlPayload = "<toast>\n" +
            //                 " <visual>\n" +
            //                 " <binding template=\"ToastImageAndText03\">\n" +
            //                 $" <image id=\"1\" src=\"{image}\" alt=\"image1\"/>\n" +
            //                 $" <text id=\"1\">{title}</text>\n" +
            //                 $" <text id=\"2\">{content}</text>\n" +
            //                 " </binding> \n" +
            //                 " </visual>\n" +
            //                 "</toast>"
            //};

            string message = string.Format(XmlPayload);
            try
            {
                var result = await services.Push.HubClient.SendWindowsNativeNotificationAsync(message, tag);
                services.Log.Info(result.State.ToString());
            }
            catch (System.Exception ex)
            {
                services.Log.Error(ex.Message, null, "Push.SendAsync Error");
            }
        }
        public static async Task SendToastAsync(this ApiServices services, string title, string content, Uri image)
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
                var result = await services.Push.SendAsync(message);
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
        public static async Task SendToastAsync(this ApiServices services, JObject obj, string tags)
        {
            WindowsPushMessage message = new WindowsPushMessage
            {
                XmlPayload = "<toast>\n" +
                             " <visual>\n" +
                             " <binding template=\"ToastImageAndText03\">\n" +
                             $" <image id=\"1\" src=\"{obj["Avatar"].ToString()}\" alt=\"image1\"/>\n" +
                             $" <text id=\"1\">{obj["Username"].ToString()}</text>\n" +
                             $" <text id=\"2\">{obj["Content"].ToString()}</text>\n" +
                             $" <text id=\"3\">{obj["InboxDate"].ToString()}</text>\n" +
                             $" <text id=\"4\">{obj["ID"].ToString()}</text>\n" +
                             $" <text id=\"5\">{obj["MessageID"].ToString()}</text>\n" +
                             $" <text id=\"6\">{obj["UserID"].ToString()}</text>\n" +
                             $" <text id=\"7\">{obj["OwnerID"].ToString()}</text>\n" +
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