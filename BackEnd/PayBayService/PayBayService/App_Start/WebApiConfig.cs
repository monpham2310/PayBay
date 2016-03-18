using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Web.Http;
using PayBayService.Models;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security.Providers;

namespace PayBayService
{
    public static class WebApiConfig
    {
        public static ApiServices Services;

        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();
                        
            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;          
             
            Services = new ApiServices(config);

            config.Formatters.JsonFormatter.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include;
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;

            //config.MapHttpAttributeRoutes();

            config.Routes.IgnoreRoute("ignoreRoute","{resource}.axd/{*pathInfo}");

            config.Routes.MapHttpRoute(
                name: "Api",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            #region LoginProvider

            //options.LoginProviders.Remove(typeof(FacebookLoginProvider));
            //options.LoginProviders.Add(typeof(FacebookLoginAuthenticationProvider));

            #endregion

            //options.PushAuthorization = Microsoft.WindowsAzure.Mobile.Service.Security.AuthorizationLevel.User;
            config.SetIsHosted(true);

        }
    }

    public class MobileServiceInitializer : DropCreateDatabaseIfModelChanges<PayBayDatabaseEntities>
    {
        protected override void Seed(PayBayDatabaseEntities context)
        {            
            base.Seed(context);
        }
    }
}

