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
    
    public partial class Bill
    {
        public int BillId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int StoreID { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public Nullable<double> ReducedPrice { get; set; }
        public int UserID { get; set; }
        public string ShipMethod { get; set; }
        public string TradeTerm { get; set; }
        public string AgreeredShippingDate { get; set; }
        public Nullable<System.DateTime> ShippingDate { get; set; }
        public Nullable<bool> IsAccepted { get; set; }
    }
}
