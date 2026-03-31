using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BootStrapECommerce.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OldPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "ImagePath", "IsActive", "Name", "OldPrice", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, "Elektronik", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yüksek performanslı dizüstü bilgisayar, 16GB RAM, 512GB SSD", "/images/products/laptop.jpg", true, "Laptop Pro X1", 29999.99m, 24999.99m, 10 },
                    { 2, "Elektronik", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gürültü engelleme özellikli bluetooth kulaklık", "/images/products/headphones.jpg", true, "Kablosuz Kulaklık", 1799.99m, 1299.99m, 25 },
                    { 3, "Elektronik", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sağlık takip özellikli akıllı saat, su geçirmez", "/images/products/smartwatch.jpg", true, "Akıllı Saat", null, 3499.99m, 15 },
                    { 4, "Bilgisayar", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "RGB aydınlatmalı mekanik oyuncu klavyesi", "/images/products/keyboard.jpg", true, "Mekanik Klavye", 1199.99m, 899.99m, 20 },
                    { 5, "Bilgisayar", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "16000 DPI hassasiyetli oyuncu faresi", "/images/products/mouse.jpg", true, "Oyuncu Mouse", null, 449.99m, 30 },
                    { 6, "Bilgisayar", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "27 inç 4K UHD IPS panel oyuncu monitörü", "/images/products/monitor.jpg", true, "4K Monitör", 10999.99m, 8999.99m, 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
