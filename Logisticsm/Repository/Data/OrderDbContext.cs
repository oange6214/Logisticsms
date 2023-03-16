using System;
using System.Collections.Generic;
using Logisticsm.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logisticsm.Repository.Data;

public partial class OrderDbContext : DbContext
{
    public OrderDbContext()
    {
    }

    public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AirTransport> AirTransports { get; set; }

    public virtual DbSet<AirTransportDetail> AirTransportDetails { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<ExpressTransport> ExpressTransports { get; set; }

    public virtual DbSet<ExpressTransportDetail> ExpressTransportDetails { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<SeaTransport> SeaTransports { get; set; }

    public virtual DbSet<SeaTransportDetail> SeaTransportDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=OrderDB;User Id=sa;Password=123;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AirTransport>(entity =>
        {
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Customer).WithMany(p => p.AirTransports).HasConstraintName("FK_AirTransport_Customer");
        });

        modelBuilder.Entity<AirTransportDetail>(entity =>
        {
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.AirTransport).WithMany(p => p.AirTransportDetails).HasConstraintName("FK_AirTransportDetail_AirTransport");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<ExpressTransport>(entity =>
        {
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Customer).WithMany(p => p.ExpressTransports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExpressTransport_Customer");
        });

        modelBuilder.Entity<ExpressTransportDetail>(entity =>
        {
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.ExpressTransport).WithMany(p => p.ExpressTransportDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExpressTransportDetail_ExpressTransport");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<SeaTransport>(entity =>
        {
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Customer).WithMany(p => p.SeaTransports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SeaTransport_Customer");
        });

        modelBuilder.Entity<SeaTransportDetail>(entity =>
        {
            entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.SeaTransport).WithMany(p => p.SeaTransportDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SeaTransportDetail_SeaTransport");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
