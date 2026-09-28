using Microsoft.EntityFrameworkCore;
using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Infra.Data.Context;

public class AppDbContext(DbContextOptions<AppDbContext>options) : DbContext(options)
{
    
    public DbSet<Voucher> Vouchers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User>Users { get; set; }
    public DbSet<Order>Orders { get; set; }
    public DbSet<Category> Categories { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}