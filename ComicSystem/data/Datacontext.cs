using ComicSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.data{
    public class ComicSystemContext : DbContext
{
    public ComicSystemContext(DbContextOptions<ComicSystemContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<ComicBook> ComicBooks { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<RentalDetail> RentalDetails { get; set; } 
     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

       
        modelBuilder.Entity<Rental>()
            .HasOne(r => r.Customer)  
            .WithMany()              
            .HasForeignKey(r => r.CustomerID);
    }
}
}