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
    
    public partial class Comment
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> CommentDate { get; set; }
        public int StoreID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }
    }
}
