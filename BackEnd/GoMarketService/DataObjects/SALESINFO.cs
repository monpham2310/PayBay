using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace GoMarketService.DataObjects
{    
    [Table("SALESINFO")]
    public partial class SALESINFO : EntityData
    {
        [Key]
        public int SaleID { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        [Required]
        [StringLength(10)]
        public string StoreID { get; set; }

        public bool isRequired { get; set; }

        public virtual STORE STORE { get; set; }
    }
}
