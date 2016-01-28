using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace GoMarketService.DataObjects
{    
    [Table("PRODUCTSTATISTIC")]
    public partial class PRODUCTSTATISTIC : EntityData
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string BillID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string ProductID { get; set; }

        public int? NumberOf { get; set; }

        public double? UnitPrice { get; set; }

        [StringLength(20)]
        public string Unit { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SaleDate { get; set; }

        public virtual BILL BILL { get; set; }

        public virtual PRODUCT PRODUCT { get; set; }
    }
}
