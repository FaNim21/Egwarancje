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
        string srvrdbname = "egwarancje";
        string srvrname = "192.168.0.165";
        string srvrusername = "fanim";
        string srvrpassword = "fanim123";

        string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword};TrustServerCertificate=True";
        optionsBuilder.UseSqlServer(sqlconn);
    }
}
