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

    public virtual DbSet<MhkAdmin> MhkAdmins { get; set; }

    public virtual DbSet<MhkCategory> MhkCategories { get; set; }

    public virtual DbSet<MhkOrder> MhkOrders { get; set; }

    public virtual DbSet<MhkOrderDetail> MhkOrderDetails { get; set; }

    public virtual DbSet<MhkProduct> MhkProducts { get; set; }

    public virtual DbSet<MhkUser> MhkUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=DESKTOP-IGIO92E;Database=Mhk_Project2;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MhkAddress>(entity =>
        {
            entity.HasKey(e => e.MhkAddressId).HasName("PK__MhkAddre__3BFE1226B5A1641D");

            entity.ToTable("MhkAddresses");

            entity.Property(e => e.MhkAddressId).HasColumnName("MhkAddressId");
            entity.Property(e => e.MhkLine1)
                .HasMaxLength(200)
                .HasColumnName("MhkLine1");
            entity.Property(e => e.MhkUserId).HasColumnName("MhkUserId");

            entity.HasOne(d => d.MhkUser).WithMany(p => p.MhkAddresses)
                .HasForeignKey(d => d.MhkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MhkAddresses_Users");
        });

        modelBuilder.Entity<MhkAdmin>(entity =>
        {
            entity.HasKey(e => e.MhkAdminId).HasName("PK__MhkAdmin__89F709C7D2F93F46");

            entity.ToTable("MhkAdmins");

            entity.HasIndex(e => e.MhkUserName, "UQ__MhkAdmin__3C7C6AE695482A8E").IsUnique();

            entity.Property(e => e.MhkAdminId).HasColumnName("MhkAdminId");
            entity.Property(e => e.MhkFullName)
                .HasMaxLength(150)
                .HasColumnName("MhkFullName");
            entity.Property(e => e.MhkPassword)
                .HasMaxLength(200)
                .HasColumnName("MhkPassword");
            entity.Property(e => e.MhkRole)
                .HasMaxLength(50)
                .HasDefaultValue("Admin")
                .HasColumnName("MhkRole");
            entity.Property(e => e.MhkUserName)
                .HasMaxLength(100)
                .HasColumnName("MhkUserName");
        });

        modelBuilder.Entity<MhkCategory>(entity =>
        {
            entity.HasKey(e => e.MhkCategoryId).HasName("PK__MhkCateg__BCC94E830BE4A933");

            entity.ToTable("MhkCategories");

            entity.Property(e => e.MhkCategoryId).HasColumnName("MhkCategoryId");
            entity.Property(e => e.MhkCategoryName)
                .HasMaxLength(100)
                .HasColumnName("MhkCategoryName");
        });

        modelBuilder.Entity<MhkOrder>(entity =>
        {
            entity.HasKey(e => e.MhkOrderId).HasName("PK__MhkOrder__97B33A64BE167CA3");

            entity.ToTable("MhkOrders");

            entity.Property(e => e.MhkOrderId).HasColumnName("MhkOrderId");
            entity.Property(e => e.MhkOrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("MhkOrderDate");
            entity.Property(e => e.MhkStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Pending")
                .HasColumnName("MhkStatus");
            entity.Property(e => e.MhkUserId).HasColumnName("MhkUserId");

            entity.HasOne(d => d.MhkUser).WithMany(p => p.MhkOrders)
                .HasForeignKey(d => d.MhkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MhkOrders_Users");
        });

        modelBuilder.Entity<MhkOrderDetail>(entity =>
        {
            entity.HasKey(e => e.MhkOrderDetailId).HasName("PK__MhkOrder__3B412F41FB57BDE3");

            entity.ToTable("MhkOrderDetails");

            entity.Property(e => e.MhkOrderDetailId).HasColumnName("MhkOrderDetailId");
            entity.Property(e => e.MhkOrderId).HasColumnName("MhkOrderId");
            entity.Property(e => e.MhkProductId).HasColumnName("MhkProductId");
            entity.Property(e => e.MhkQuantity).HasColumnName("MhkQuantity");
            entity.Property(e => e.MhkUnitPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("MhkUnitPrice");

            entity.HasOne(d => d.MhkOrder).WithMany(p => p.MhkOrderDetails)
                .HasForeignKey(d => d.MhkOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MhkOrderDetails_Orders");

            entity.HasOne(d => d.MhkProduct).WithMany(p => p.MhkOrderDetails)
                .HasForeignKey(d => d.MhkProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MhkOrderDetails_Products");
        });

        modelBuilder.Entity<MhkProduct>(entity =>
        {
            entity.HasKey(e => e.MhkProductId).HasName("PK__MhkProdu__7C8563979E0C6AF2");

            entity.ToTable("MhkProducts");

            entity.Property(e => e.MhkProductId).HasColumnName("MhkProductId");
            entity.Property(e => e.MhkCategoryId).HasColumnName("MhkCategoryId");
            entity.Property(e => e.MhkDescription)
                .HasMaxLength(500)
                .HasColumnName("MhkDescription");
            entity.Property(e => e.MhkName)
                .HasMaxLength(150)
                .HasColumnName("MhkName");
            entity.Property(e => e.MhkPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("MhkPrice");

            entity.HasOne(d => d.MhkCategory).WithMany(p => p.MhkProducts)
                .HasForeignKey(d => d.MhkCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MhkProducts_Categories");
        });

        modelBuilder.Entity<MhkUser>(entity =>
        {
            entity.HasKey(e => e.MhkUserId).HasName("PK__MhkUsers__FD4C613EC32D7737");

            entity.ToTable("MhkUsers");

            entity.HasIndex(e => e.MhkEmail, "UQ__MhkUsers__46F6BE4BF9493568").IsUnique();

            entity.Property(e => e.MhkUserId).HasColumnName("MhkUserId");
            entity.Property(e => e.MhkEmail)
                .HasMaxLength(150)
                .HasColumnName("MhkEmail");
            entity.Property(e => e.MhkFullName)
                .HasMaxLength(150)
                .HasColumnName("MhkFullName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
