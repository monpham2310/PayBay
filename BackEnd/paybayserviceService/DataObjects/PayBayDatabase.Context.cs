﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace paybayserviceService.DataObjects
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PayBayDatabaseEntities : DbContext
    {

        private const string connectionStringName = "Name=MS_TableConnectionString";

        public PayBayDatabaseEntities() : base(connectionStringName)
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<DetailBill> DetailBills { get; set; }
        public virtual DbSet<Market> Markets { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductStatistic> ProductStatistics { get; set; }
        public virtual DbSet<RevenueStatistic> RevenueStatistics { get; set; }
        public virtual DbSet<SaleInfo> SaleInfoes { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
    }
}
