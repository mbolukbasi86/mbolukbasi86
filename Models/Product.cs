using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootStrapECommerce.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? OldPrice { get; set; }

        // Image path stored relative to wwwroot (e.g. /images/products/product1.jpg)
        [StringLength(500)]
        public string? ImagePath { get; set; }

        [StringLength(100)]
        public string? Category { get; set; }

        public int Stock { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [NotMapped]
        public string DisplayImagePath => string.IsNullOrEmpty(ImagePath)
            ? "/images/products/no-image.png"
            : ImagePath;
    }
}
