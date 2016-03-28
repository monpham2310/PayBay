using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBay.Model.RequestBody
{
    public class ProductInfo
    {
        public int OwnerID { get; set; }
        public int ProductID { get; set; }
        public Utilities.Common.TYPE Type { get; set; }

        public ProductInfo() { }

        public ProductInfo(int ownerId, int productId, Utilities.Common.TYPE type)
        {
            OwnerID = ownerId;
            ProductID = productId;
            Type = type;
        }

    }
}
