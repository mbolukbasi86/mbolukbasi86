using Microsoft.EntityFrameworkCore;
using BootStrapECommerce.Models;

namespace BootStrapECommerce.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        // Photos table stores product images as BLOB (varbinary(MAX))
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One Product has many Photos
            modelBuilder.Entity<Photo>()
                .HasOne(ph => ph.Product)
                .WithMany(p => p.Photos)
                .HasForeignKey(ph => ph.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
