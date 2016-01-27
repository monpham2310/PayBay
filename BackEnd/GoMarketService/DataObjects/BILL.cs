using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace GoMarketService.DataObjects
{
    [Table("BILL")]
    public partial class BILL : EntityData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BILL()
        {
            DETAILBILLs = new HashSet<DETAILBILL>();
            PRODUCTSTATISTICs = new HashSet<PRODUCTSTATISTIC>();
            REVENUESTATISTICs = new HashSet<REVENUESTATISTIC>();
        }

        [StringLength(10)]
        public string BillID { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(10)]
        public string StoreID { get; set; }

        public double? TotalPrice { get; set; }

        public double? ReducedPrice { get; set; }

        [StringLength(10)]
        public string UserID { get; set; }

        public virtual STORE STORE { get; set; }

        public virtual USER USER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETAILBILL> DETAILBILLs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCTSTATISTIC> PRODUCTSTATISTICs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REVENUESTATISTIC> REVENUESTATISTICs { get; set; }
    }
}
