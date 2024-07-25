using EgwarancjeDbLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EgwarancjeAPI;

public class LocalDatabaseContext : DbContext
{
    public User? User { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderSpec> OrdersSpec { get; set; }

    public DbSet<Warranty> Warranties { get; set; }
    public DbSet<WarrantySpec> WarrantiesSpecs { get; set; }

    public DbSet<Attachment> Attachments { get; set; }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Cart> Carts { get; set; }


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
            .HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasMany(u => u.Addresses)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasOne(u => u.Cart)
            .WithOne(c => c.User)
            .HasForeignKey<Cart>(c => c.UserId)
            .IsRequired();

        //Order
        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderSpecs)
            .WithOne(os => os.Order)
            .HasForeignKey(os => os.OrderId)
            .IsRequired();

        modelBuilder.Entity<OrderSpec>()
            .HasOne(o => o.Order)
            .WithMany(o => o.OrderSpecs)
            .HasForeignKey(o => o.OrderId)
            .IsRequired();


        modelBuilder.Entity<Attachment>()
            .HasOne(a => a.WarrantySpec)
            .WithMany(w => w.Attachments)
            .HasForeignKey(a => a.WarrantySpecId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Warranty>()
            .HasOne(w => w.Order)
            .WithMany()
            .HasForeignKey(w => w.OrderId)
            .IsRequired();

        modelBuilder.Entity<WarrantySpec>()
            .HasOne(ws => ws.OrderSpec)
            .WithMany()
            .HasForeignKey(ws => ws.OrderSpecId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<WarrantySpec>()
            .HasOne(ws => ws.Warranty)
            .WithMany()
            .HasForeignKey(ws => ws.WarrantyId)
            .IsRequired();

        modelBuilder.Entity<Warranty>()
            .HasMany(w => w.WarrantySpecs)
            .WithOne(ws => ws.Warranty)
            .HasForeignKey(ws => ws.WarrantyId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}
