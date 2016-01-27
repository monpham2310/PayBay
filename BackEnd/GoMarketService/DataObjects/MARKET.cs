using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace GoMarketService.DataObjects
{ 
    [Table("MARKETS")]
    public partial class MARKET : EntityData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MARKET()
        {
            STORES = new HashSet<STORE>();
        }

        [StringLength(10)]
        public string MarketID { get; set; }

        [Required]
        [StringLength(100)]
        public string MarketName { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(12)]
        public string Phone { get; set; }

        public string Image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STORE> STORES { get; set; }
    }
}
