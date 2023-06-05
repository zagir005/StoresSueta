using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoresApplication.Models;

namespace StoresApplication;

public partial class StoresContext : DbContext
{
    public StoresContext()
    {
        Database.EnsureCreated();
    }

    public StoresContext(DbContextOptions<StoresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<Ownership> Ownerships { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<StoreOwner> StoreOwners { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Stores;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Delivery__3214EC0795E26D11");

            entity.ToTable("Delivery");

            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Store).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Delivery__StoreI__300424B4");

            entity.HasOne(d => d.Vendor).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Delivery__Vendor__2F10007B");
        });

        modelBuilder.Entity<Ownership>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ownershi__3214EC07BD54F862");

            entity.ToTable("Ownership");

            entity.Property(e => e.Deposit).HasColumnType("money");
            entity.Property(e => e.RegisterDate).HasColumnType("date");

            entity.HasOne(d => d.Store).WithMany(p => p.Ownerships)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Ownership__Store__2C3393D0");

            entity.HasOne(d => d.StoreOwner).WithMany(p => p.Ownerships)
                .HasForeignKey(d => d.StoreOwnerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Ownership__Store__2B3F6F97");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Store__3214EC078AE0431B");

            entity.ToTable("Store");

            entity.HasIndex(e => e.Name, "UQ__Store__737584F666041DAB").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Capital).HasColumnType("money");
            entity.Property(e => e.CityArea).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Profile).HasMaxLength(50);
            entity.Property(e => e.Telephone).HasMaxLength(50);
        });

        modelBuilder.Entity<StoreOwner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StoreOwn__3214EC07E2CA20F6");

            entity.ToTable("StoreOwner");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.CityArea).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Telephone).HasMaxLength(50);
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vendor__3214EC07D2FB77B3");

            entity.ToTable("Vendor");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Telephone).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
