using Logisticsm.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Logisticsm.DAL.Data;

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
	{
		optionsBuilder.UseSqlServer("Server=SAANTB102-1\\SQLEXPRESS;Database=OrderDB;User Id=sa;Password=123;TrustServerCertificate=True");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<AirTransport>(entity =>
		{
			entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
		});

		modelBuilder.Entity<AirTransportDetail>(entity =>
		{
			entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
		});

		modelBuilder.Entity<Customer>(entity =>
		{
			entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
		});

		modelBuilder.Entity<ExpressTransport>(entity =>
		{
			entity.Property(e => e.Id).ValueGeneratedOnAdd();
		});

		modelBuilder.Entity<ExpressTransportDetail>(entity =>
		{
			entity.Property(e => e.Id).ValueGeneratedOnAdd();
			entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
		});

		modelBuilder.Entity<Member>(entity =>
		{
			entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
		});

		modelBuilder.Entity<SeaTransport>(entity =>
		{
			entity.Property(e => e.InsertDate).HasDefaultValueSql("(getdate())");
		});

		modelBuilder.Entity<SeaTransportDetail>(entity =>
		{
			entity.Property(e => e.Id).ValueGeneratedOnAdd();
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
