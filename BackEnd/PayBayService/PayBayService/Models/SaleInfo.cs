//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PayBayService.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SaleInfo
    {
        public int SaleId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Describes { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public int StoreID { get; set; }
        public bool isRequired { get; set; }
        public string SasQuery { get; set; }
    }
}
