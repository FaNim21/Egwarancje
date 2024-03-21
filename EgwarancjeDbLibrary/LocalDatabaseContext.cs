using EgwarancjeDbLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EgwarancjeDbLibrary;

public class LocalDatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }


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
}
