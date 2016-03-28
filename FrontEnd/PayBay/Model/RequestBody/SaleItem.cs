using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBay.Model.RequestBody
{
    public class SaleItem
    {
        public int SaleId { get; set; }
        public int OwnerId { get; set; }
        public Utilities.Common.TYPE Type { get; set; }

        public SaleItem() { }

        public SaleItem(int saleId, int ownerId, Utilities.Common.TYPE type)
        {
            SaleId = saleId;
            OwnerId = ownerId;
            Type = type;
        }
    }
}
