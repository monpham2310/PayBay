using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayBayService.Models.Products
{
    public class ProductInfo
    {
        public int OwnerID { get; set; }
        public int ProductID { get; set; }
        public Common.TYPE Type { get; set; }
    }
}