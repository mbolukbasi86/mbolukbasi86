using Microsoft.EntityFrameworkCore;
using ECommerceAdmin.Models;

namespace ECommerceAdmin.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Elektronik", Description = "Telefon, bilgisayar ve elektronik ürünler", IsActive = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Category { Id = 2, Name = "Giyim", Description = "Erkek, kadın ve çocuk giyim ürünleri", IsActive = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Category { Id = 3, Name = "Ev & Yaşam", Description = "Ev dekorasyonu ve yaşam ürünleri", IsActive = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Category { Id = 4, Name = "Spor & Outdoor", Description = "Spor ve açık hava ürünleri", IsActive = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Akıllı Telefon Pro 12", Description = "6.7 inç OLED ekran, 256GB depolama, 5G destekli", Price = 12999.99m, StockQuantity = 50, IsActive = true, CategoryId = 1, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Product { Id = 2, Name = "Laptop Ultra 15", Description = "15.6 inç FHD, Intel Core i7, 16GB RAM, 512GB SSD", Price = 24999.99m, StockQuantity = 25, IsActive = true, CategoryId = 1, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Product { Id = 3, Name = "Erkek Slim Fit Pantolon", Description = "Pamuklu kumaş, slim fit kesim, 5 renk seçeneği", Price = 349.99m, StockQuantity = 100, IsActive = true, CategoryId = 2, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Product { Id = 4, Name = "Dekoratif Çerçeve Seti", Description = "Ahşap çerçeve, 3'lü set, farklı boyutlarda", Price = 199.99m, StockQuantity = 75, IsActive = true, CategoryId = 3, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Product { Id = 5, Name = "Yoga Matı Premium", Description = "6mm kalınlık, kaymaz zemin, çanta dahil", Price = 299.99m, StockQuantity = 60, IsActive = true, CategoryId = 4, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );
        }
    }
}
