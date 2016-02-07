using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace PayBayService.Common
{
    public class Methods
    {
        public static JObject CustomResponseMessage(int code, string message)
        {
            JObject response = new JObject();
            response.Add("ErrCode", code);
            response.Add("ErrMsg", message);

            return response;
        }
    }
}