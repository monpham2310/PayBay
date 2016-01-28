using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace GoMarketService.DataObjects
{    
    [Table("STORES")]
    public partial class STORE : EntityData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public STORE()
        {
            BILL = new HashSet<BILL>();
            COMMENTS = new HashSet<COMMENT>();
            PRODUCTS = new HashSet<PRODUCT>();
            REVENUESTATISTIC = new HashSet<REVENUESTATISTIC>();
            SALESINFO = new HashSet<SALESINFO>();
        }

        [StringLength(10)]
        public string StoreID { get; set; }

        [Required]
        [StringLength(100)]
        public string StoreName { get; set; }

        [StringLength(8)]
        public string KiotNo { get; set; }

        public string Image { get; set; }

        [StringLength(12)]
        public string Phone { get; set; }

        [Required]
        [StringLength(10)]
        public string MarketID { get; set; }

        [Required]
        [StringLength(10)]
        public string OwnerID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BILL> BILL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENT> COMMENTS { get; set; }

        public virtual MARKET MARKET { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCT> PRODUCTS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REVENUESTATISTIC> REVENUESTATISTIC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SALESINFO> SALESINFO { get; set; }

        public virtual USERS USER { get; set; }
    }
}
