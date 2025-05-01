using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GDB.Web.Core.Models;

public partial class GDBContext : DbContext
{
    public GDBContext()
    {
    }

    public GDBContext(DbContextOptions<GDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdvertiseSource> AdvertiseSources { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<ExpensesAndSale> ExpensesAndSales { get; set; }

    public virtual DbSet<FoodPackingType> FoodPackingTypes { get; set; }

    public virtual DbSet<Grocery> Groceries { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<InventoryHistory> InventoryHistories { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderStater> OrderStaters { get; set; }

    public virtual DbSet<OrderType> OrderTypes { get; set; }

    public virtual DbSet<OrderTypeItem> OrderTypeItems { get; set; }

    public virtual DbSet<OrderTypePrice> OrderTypePrices { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductHistory> ProductHistories { get; set; }

    public virtual DbSet<Stater> Staters { get; set; }

    public virtual DbSet<StaterPrice> StaterPrices { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserFeedBack> UserFeedBacks { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    public virtual DbSet<WeekDatum> WeekData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=GaddamDumBiriyaniDB;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdvertiseSource>(entity =>
        {
            entity.HasKey(e => e.AdvertiseId);

            entity.ToTable("AdvertiseSource");

            entity.Property(e => e.AdvertiseDescription)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FirstName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ReferredBy)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.ExpensesId);

            entity.ToTable("Expense");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ExpensesDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.QuantityDescription)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ExpensesAndSale>(entity =>
        {
            entity.HasKey(e => e.Esid);

            entity.Property(e => e.Esid).HasColumnName("ESId");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.TotalExpenses).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalSales).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<FoodPackingType>(entity =>
        {
            entity.Property(e => e.FoodPackingTypeId).ValueGeneratedNever();
            entity.Property(e => e.FoodPackingTypeDescription)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Grocery>(entity =>
        {
            entity.ToTable("Grocery");

            entity.Property(e => e.GroceryDescription)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.ToTable("Inventory");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<InventoryHistory>(entity =>
        {
            entity.ToTable("InventoryHistory");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Location");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.LocationDescription)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AmountPaidDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<OrderStater>(entity =>
        {
            entity.ToTable("OrderStater");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<OrderType>(entity =>
        {
            entity.ToTable("OrderType");

            entity.Property(e => e.OrderTypeName).HasMaxLength(300);
        });

        modelBuilder.Entity<OrderTypeItem>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("OrderTypeItem");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ItemDescription)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OrderItemId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<OrderTypePrice>(entity =>
        {
            entity.ToTable("OrderTypePrice");

            entity.Property(e => e.OrderTypePrice1)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("OrderTypePrice");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.ToTable("PaymentType");

            entity.Property(e => e.PaymentTypeDescription)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Modifieddate).HasColumnType("datetime");
            entity.Property(e => e.ProductName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ProductPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PurchasedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ProductHistory>(entity =>
        {
            entity.ToTable("ProductHistory");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ProductName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.PurchasedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Stater>(entity =>
        {
            entity.HasKey(e => e.StaterId).HasName("PK_Starter");

            entity.ToTable("Stater");

            entity.Property(e => e.StaterDescription)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StaterPrice>(entity =>
        {
            entity.HasKey(e => e.StaterItemId);

            entity.ToTable("StaterPrice");

            entity.Property(e => e.StaterPrice1)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("StaterPrice");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.ToTable("Store");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.StoreName).HasMaxLength(250);
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.ToTable("SubCategory");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Modifieddate).HasColumnType("datetime");
            entity.Property(e => e.SubCategoryName)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmailId).HasMaxLength(250);
            entity.Property(e => e.FirstName).HasMaxLength(150);
            entity.Property(e => e.LastName).HasMaxLength(150);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Salt).HasMaxLength(255);
        });

        modelBuilder.Entity<UserFeedBack>(entity =>
        {
            entity.HasKey(e => e.Fid);

            entity.ToTable("UserFeedBack");

            entity.Property(e => e.Fid).HasColumnName("FId");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.ToTable("Vendor");

            entity.Property(e => e.VendorName)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<WeekDatum>(entity =>
        {
            entity.HasKey(e => e.WeekId);

            entity.Property(e => e.WeekDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
