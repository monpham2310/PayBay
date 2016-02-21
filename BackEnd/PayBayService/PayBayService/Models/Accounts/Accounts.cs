using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayBayService.Models.Accounts
{
    public class Account
    {
        public string Email { get; set; }
        public byte[] Password { get; set; }
    }
}