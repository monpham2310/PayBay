using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace GoMarketService.DataObjects
{    
    [Table("COMMENTS")]
    public partial class COMMENT : EntityData
    {
        public int ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CommentDate { get; set; }

        public TimeSpan? CommentTime { get; set; }

        [Required]
        [StringLength(10)]
        public string StoreID { get; set; }

        public string Content { get; set; }

        [StringLength(10)]
        public string UserID { get; set; }

        public virtual STORE STORE { get; set; }

        public virtual USERS USER { get; set; }
    }
}
