using EgwarancjeDbLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EgwarancjeDbLibrary;

public class LocalDatabaseContext : DbContext
{
    public User? User { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderSpec> OrdersSpec { get; set; }


    public LocalDatabaseContext() { }
    public LocalDatabaseContext(DbContextOptions<LocalDatabaseContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string sqlconn = $"Data Source={DbConsts.AddressIP};Initial Catalog={DbConsts.DbName};User ID={DbConsts.DbLoginUserName};Password={DbConsts.DbLoginPassword};TrustServerCertificate=True";
        optionsBuilder.UseSqlServer(sqlconn);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Konfiguracja relacji między tabelami
        modelBuilder.Entity<User>()
            .HasMany(o => o.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId)
            .IsRequired(false);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderSpecs)
            .WithOne(o => o.Order)
            .HasForeignKey(o => o.OrderId)
            .IsRequired(false);

        modelBuilder.Entity<OrderSpec>()
            .HasOne(o => o.Order)
            .WithMany(o => o.OrderSpecs)
            .HasForeignKey(o => o.OrderId)
            .IsRequired();
    }
}
