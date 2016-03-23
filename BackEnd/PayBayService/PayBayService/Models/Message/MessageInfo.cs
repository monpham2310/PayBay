using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayBayService.Models.Message
{
    public class MessageInfo
    {
        public int MessageID { get; set; }
        public int UserID { get; set; }
        public int UserChat { get; set; }
        public Common.TYPE Type { get; set; }
    }
}