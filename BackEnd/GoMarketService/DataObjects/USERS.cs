using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace GoMarketService.DataObjects
{
    [Table("USERS")]
    public partial class USERS : EntityData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USERS()
        {
            BILL = new HashSet<BILL>();
            COMMENTS = new HashSet<COMMENT>();
            STORES = new HashSet<STORE>();
        }

        [StringLength(10)]
        public string UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        [StringLength(12)]
        public string Phone { get; set; }

        public bool Gender { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public string Avatar { get; set; }

        [StringLength(30)]
        public string Username { get; set; }

        public byte[] Pass { get; set; }

        public int TypeID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BILL> BILL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENT> COMMENTS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STORE> STORES { get; set; }

        public virtual USERTYPE USERTYPE { get; set; }
    }
}
