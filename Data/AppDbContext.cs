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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed some sample products with image paths
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Laptop Pro X1",
                    Description = "Yüksek performanslı dizüstü bilgisayar, 16GB RAM, 512GB SSD",
                    Price = 24999.99m,
                    OldPrice = 29999.99m,
                    ImagePath = "/images/products/laptop.jpg",
                    Category = "Elektronik",
                    Stock = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new Product
                {
                    Id = 2,
                    Name = "Kablosuz Kulaklık",
                    Description = "Gürültü engelleme özellikli bluetooth kulaklık",
                    Price = 1299.99m,
                    OldPrice = 1799.99m,
                    ImagePath = "/images/products/headphones.jpg",
                    Category = "Elektronik",
                    Stock = 25,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new Product
                {
                    Id = 3,
                    Name = "Akıllı Saat",
                    Description = "Sağlık takip özellikli akıllı saat, su geçirmez",
                    Price = 3499.99m,
                    OldPrice = null,
                    ImagePath = "/images/products/smartwatch.jpg",
                    Category = "Elektronik",
                    Stock = 15,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new Product
                {
                    Id = 4,
                    Name = "Mekanik Klavye",
                    Description = "RGB aydınlatmalı mekanik oyuncu klavyesi",
                    Price = 899.99m,
                    OldPrice = 1199.99m,
                    ImagePath = "/images/products/keyboard.jpg",
                    Category = "Bilgisayar",
                    Stock = 20,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new Product
                {
                    Id = 5,
                    Name = "Oyuncu Mouse",
                    Description = "16000 DPI hassasiyetli oyuncu faresi",
                    Price = 449.99m,
                    OldPrice = null,
                    ImagePath = "/images/products/mouse.jpg",
                    Category = "Bilgisayar",
                    Stock = 30,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new Product
                {
                    Id = 6,
                    Name = "4K Monitör",
                    Description = "27 inç 4K UHD IPS panel oyuncu monitörü",
                    Price = 8999.99m,
                    OldPrice = 10999.99m,
                    ImagePath = "/images/products/monitor.jpg",
                    Category = "Bilgisayar",
                    Stock = 8,
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1)
                }
            );
        }
    }
}
