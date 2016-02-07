using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using PayBayService.Models;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using Newtonsoft.Json.Linq;

namespace PayBayService.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.Application)]
    public class PaybayController : ApiController
    {
        public ApiServices Services { get; set; }

        private PayBayDatabaseEntities db = new PayBayDatabaseEntities();

        // GET api/Paybay
        public string Get()
        {
            Services.Log.Info("Hello from custom controller!");
            return "Hello";
        }
                
    }        
}
