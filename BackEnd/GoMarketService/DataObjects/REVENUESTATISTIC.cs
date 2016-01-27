using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace GoMarketService.DataObjects
{
    [Table("REVENUESTATISTIC")]
    public partial class REVENUESTATISTIC : EntityData
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string StoreID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string BillID { get; set; }

        public double? Total { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        public virtual BILL BILL { get; set; }

        public virtual STORE STORE { get; set; }
    }
}
