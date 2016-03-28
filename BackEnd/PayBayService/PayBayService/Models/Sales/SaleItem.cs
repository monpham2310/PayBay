using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayBayService.Models.Sales
{
    public class SaleItem
    {
        public int SaleId { get; set; }
        public int OwnerId { get; set; }
        public Common.TYPE Type { get; set; }
    }
}