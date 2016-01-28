using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using GoMarketService.DataObjects;

namespace GoMarketService.Models
{
    public partial class GoMarketContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to alter your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        private const string connectionStringName = "Name=MS_TableConnectionString";

        public GoMarketContext() : base(connectionStringName)
        {
        }

        public DbSet<BILL> BILL { get; set; }
        public DbSet<COMMENT> COMMENTS { get; set; }
        public DbSet<DETAILBILL> DETAILBILL { get; set; }
        public DbSet<MARKET> MARKETS { get; set; }
        public DbSet<PRODUCT> PRODUCTS { get; set; }
        public DbSet<PRODUCTSTATISTIC> PRODUCTSTATISTIC { get; set; }
        public DbSet<REVENUESTATISTIC> REVENUESTATISTIC { get; set; }
        public DbSet<SALESINFO> SALESINFO { get; set; }
        public DbSet<STORE> STORES { get; set; }
        public DbSet<USERS> USERS { get; set; }
        public DbSet<USERTYPE> USERTYPE { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));

            modelBuilder.Entity<BILL>()
                .Property(e => e.BillID)
                .IsUnicode(false);

            modelBuilder.Entity<BILL>()
                .Property(e => e.StoreID)
                .IsUnicode(false);

            modelBuilder.Entity<BILL>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<BILL>()
                .HasMany(e => e.DETAILBILL)
                .WithRequired(e => e.BILL)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BILL>()
                .HasMany(e => e.PRODUCTSTATISTIC)
                .WithRequired(e => e.BILL)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BILL>()
                .HasMany(e => e.REVENUESTATISTIC)
                .WithRequired(e => e.BILL)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<COMMENT>()
                .Property(e => e.StoreID)
                .IsUnicode(false);

            modelBuilder.Entity<COMMENT>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<DETAILBILL>()
                .Property(e => e.BillID)
                .IsUnicode(false);

            modelBuilder.Entity<DETAILBILL>()
                .Property(e => e.ProductID)
                .IsUnicode(false);

            modelBuilder.Entity<MARKET>()
                .Property(e => e.MarketID)
                .IsUnicode(false);

            modelBuilder.Entity<MARKET>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<MARKET>()
                .HasMany(e => e.STORES)
                .WithRequired(e => e.MARKET)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRODUCT>()
                .Property(e => e.ProductID)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCT>()
                .Property(e => e.StoreID)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCT>()
                .HasMany(e => e.DETAILBILLs)
                .WithRequired(e => e.PRODUCT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRODUCT>()
                .HasMany(e => e.PRODUCTSTATISTICs)
                .WithRequired(e => e.PRODUCT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRODUCTSTATISTIC>()
                .Property(e => e.BillID)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCTSTATISTIC>()
                .Property(e => e.ProductID)
                .IsUnicode(false);

            modelBuilder.Entity<REVENUESTATISTIC>()
                .Property(e => e.StoreID)
                .IsUnicode(false);

            modelBuilder.Entity<REVENUESTATISTIC>()
                .Property(e => e.BillID)
                .IsUnicode(false);

            modelBuilder.Entity<SALESINFO>()
                .Property(e => e.StoreID)
                .IsUnicode(false);

            modelBuilder.Entity<STORE>()
                .Property(e => e.StoreID)
                .IsUnicode(false);

            modelBuilder.Entity<STORE>()
                .Property(e => e.KiotNo)
                .IsUnicode(false);

            modelBuilder.Entity<STORE>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<STORE>()
                .Property(e => e.MarketID)
                .IsUnicode(false);

            modelBuilder.Entity<STORE>()
                .Property(e => e.OwnerID)
                .IsUnicode(false);

            modelBuilder.Entity<STORE>()
                .HasMany(e => e.BILL)
                .WithRequired(e => e.STORE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<STORE>()
                .HasMany(e => e.COMMENTS)
                .WithRequired(e => e.STORE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<STORE>()
                .HasMany(e => e.PRODUCTS)
                .WithRequired(e => e.STORE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<STORE>()
                .HasMany(e => e.REVENUESTATISTIC)
                .WithRequired(e => e.STORE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<STORE>()
                .HasMany(e => e.SALESINFO)
                .WithRequired(e => e.STORE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .HasMany(e => e.STORES)
                .WithRequired(e => e.USER)
                .HasForeignKey(e => e.OwnerID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USERTYPE>()
                .Property(e => e.TypeName)
                .IsUnicode(false);

            modelBuilder.Entity<USERTYPE>()
                .HasMany(e => e.USERS)
                .WithRequired(e => e.USERTYPE)
                .WillCascadeOnDelete(false);

        }
                
    }

}
