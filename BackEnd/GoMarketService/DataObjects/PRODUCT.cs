using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace GoMarketService.DataObjects
{
    [Table("PRODUCTS")]
    public partial class PRODUCT : EntityData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCT()
        {
            DETAILBILLs = new HashSet<DETAILBILL>();
            PRODUCTSTATISTICs = new HashSet<PRODUCTSTATISTIC>();
        }

        [StringLength(10)]
        public string ProductID { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        public string Image { get; set; }

        public double UnitPrice { get; set; }

        public int NumberOf { get; set; }

        [StringLength(20)]
        public string Unit { get; set; }

        [Required]
        [StringLength(10)]
        public string StoreID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETAILBILL> DETAILBILLs { get; set; }

        public virtual STORE STORE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCTSTATISTIC> PRODUCTSTATISTICs { get; set; }
    }
}
