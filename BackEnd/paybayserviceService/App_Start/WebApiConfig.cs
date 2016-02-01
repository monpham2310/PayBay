using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using paybayserviceService.DataObjects;

namespace paybayserviceService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            
            // Set default and null value handling to "Include" for Json Serializer
            config.Formatters.JsonFormatter.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include;
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;

            //Database.SetInitializer(new paybayserviceInitializer());
            //var migrator = new DbMigrator(new Configuration());
            //migrator.Update();

        }
    }

    public class paybayserviceInitializer : ClearDatabaseSchemaIfModelChanges<PayBayDatabaseEntities>
    {
        protected override void Seed(PayBayDatabaseEntities context)
        {
            //List<TodoItem> todoItems = new List<TodoItem>
            //{
            //    new TodoItem { Id = Guid.NewGuid().ToString(), Text = "First item", Complete = false },
            //    new TodoItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Complete = false },
            //};

            //foreach (TodoItem todoItem in todoItems)
            //{
            //    context.Set<TodoItem>().Add(todoItem);
            //}

            //List<UserType> userType = new List<UserType>
            //{
            //    new UserType { Id = Guid.NewGuid().ToString(), TypeId = 1, TypeName = "Admin" },
            //    new UserType { Id = Guid.NewGuid().ToString(), TypeId = 2, TypeName = "Store Owner" },
            //    new UserType { Id = Guid.NewGuid().ToString(), TypeId = 3, TypeName = "User" }
            //};

            //foreach (UserType usertype in userType)
            //{
            //    context.Set<UserType>().Add(usertype);
            //}

            base.Seed(context);
        }
    }
}

