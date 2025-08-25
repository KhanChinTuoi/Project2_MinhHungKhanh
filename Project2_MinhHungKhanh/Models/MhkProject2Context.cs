using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Project2_MinhHungKhanh.Models;

public partial class MhkProject2Context : DbContext
{
    public MhkProject2Context()
    {
    }

    public MhkProject2Context(DbContextOptions<MhkProject2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<MhkAddress> MhkAddresses { get; set; }

    public virtual DbSet<MhkCategory> MhkCategories { get; set; }

    public virtual DbSet<MhkOrder> MhkOrders { get; set; }

    public virtual DbSet<MhkOrderDetail> MhkOrderDetails { get; set; }

    public virtual DbSet<MhkProduct> MhkProducts { get; set; }

    public virtual DbSet<MhkUser> MhkUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=TRONGNHAT\\HUNG;Database=MHK_Project2;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MhkAddress>(entity =>
        {
            entity.HasKey(e => e.MhkAddressId).HasName("PK__mhkAddre__3BFE1226B5A1641D");

            entity.ToTable("mhkAddresses");

            entity.Property(e => e.MhkAddressId).HasColumnName("mhkAddressId");
            entity.Property(e => e.MhkLine1)
                .HasMaxLength(200)
                .HasColumnName("mhkLine1");
            entity.Property(e => e.MhkUserId).HasColumnName("mhkUserId");

            entity.HasOne(d => d.MhkUser).WithMany(p => p.MhkAddresses)
                .HasForeignKey(d => d.MhkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mhkAddresses_Users");
        });

        modelBuilder.Entity<MhkCategory>(entity =>
        {
            entity.HasKey(e => e.MhkCategoryId).HasName("PK__mhkCateg__BCC94E830BE4A933");

            entity.ToTable("mhkCategories");

            entity.Property(e => e.MhkCategoryId).HasColumnName("mhkCategoryId");
            entity.Property(e => e.MhkCategoryName)
                .HasMaxLength(100)
                .HasColumnName("mhkCategoryName");
        });

        modelBuilder.Entity<MhkOrder>(entity =>
        {
            entity.HasKey(e => e.MhkOrderId).HasName("PK__mhkOrder__97B33A64BE167CA3");

            entity.ToTable("mhkOrders");

            entity.Property(e => e.MhkOrderId).HasColumnName("mhkOrderId");
            entity.Property(e => e.MhkOrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("mhkOrderDate");
            entity.Property(e => e.MhkStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Pending")
                .HasColumnName("mhkStatus");
            entity.Property(e => e.MhkUserId).HasColumnName("mhkUserId");

            entity.HasOne(d => d.MhkUser).WithMany(p => p.MhkOrders)
                .HasForeignKey(d => d.MhkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mhkOrders_Users");
        });

        modelBuilder.Entity<MhkOrderDetail>(entity =>
        {
            entity.HasKey(e => e.MhkOrderDetailId).HasName("PK__mhkOrder__3B412F41FB57BDE3");

            entity.ToTable("mhkOrderDetails");

            entity.Property(e => e.MhkOrderDetailId).HasColumnName("mhkOrderDetailId");
            entity.Property(e => e.MhkOrderId).HasColumnName("mhkOrderId");
            entity.Property(e => e.MhkProductId).HasColumnName("mhkProductId");
            entity.Property(e => e.MhkQuantity).HasColumnName("mhkQuantity");
            entity.Property(e => e.MhkUnitPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("mhkUnitPrice");

            entity.HasOne(d => d.MhkOrder).WithMany(p => p.MhkOrderDetails)
                .HasForeignKey(d => d.MhkOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mhkOrderDetails_Orders");

            entity.HasOne(d => d.MhkProduct).WithMany(p => p.MhkOrderDetails)
                .HasForeignKey(d => d.MhkProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mhkOrderDetails_Products");
        });

        modelBuilder.Entity<MhkProduct>(entity =>
        {
            entity.HasKey(e => e.MhkProductId).HasName("PK__mhkProdu__7C8563979E0C6AF2");

            entity.ToTable("mhkProducts");

            entity.Property(e => e.MhkProductId).HasColumnName("mhkProductId");
            entity.Property(e => e.MhkCategoryId).HasColumnName("mhkCategoryId");
            entity.Property(e => e.MhkDescription)
                .HasMaxLength(500)
                .HasColumnName("mhkDescription");
            entity.Property(e => e.MhkName)
                .HasMaxLength(150)
                .HasColumnName("mhkName");
            entity.Property(e => e.MhkPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("mhkPrice");

            entity.HasOne(d => d.MhkCategory).WithMany(p => p.MhkProducts)
                .HasForeignKey(d => d.MhkCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mhkProducts_Categories");
        });

        modelBuilder.Entity<MhkUser>(entity =>
        {
            entity.HasKey(e => e.MhkUserId).HasName("PK__mhkUsers__FD4C613EC32D7737");

            entity.ToTable("mhkUsers");

            entity.HasIndex(e => e.MhkEmail, "UQ__mhkUsers__46F6BE4BF9493568").IsUnique();

            entity.Property(e => e.MhkUserId).HasColumnName("mhkUserId");
            entity.Property(e => e.MhkEmail)
                .HasMaxLength(150)
                .HasColumnName("mhkEmail");
            entity.Property(e => e.MhkFullName)
                .HasMaxLength(150)
                .HasColumnName("mhkFullName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
