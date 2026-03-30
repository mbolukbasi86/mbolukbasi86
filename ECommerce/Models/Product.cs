using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        [StringLength(200, ErrorMessage = "Ürün adı en fazla 200 karakter olabilir.")]
        [Display(Name = "Ürün Adı")]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Açıklama en fazla 1000 karakter olabilir.")]
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Fiyat zorunludur.")]
        [Range(0.01, 999999.99, ErrorMessage = "Fiyat 0.01 ile 999999.99 arasında olmalıdır.")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Fiyat (₺)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stok miktarı zorunludur.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stok miktarı 0 veya daha büyük olmalıdır.")]
        [Display(Name = "Stok Miktarı")]
        public int StockQuantity { get; set; }

        [StringLength(500)]
        [Display(Name = "Görsel URL")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Aktif")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Güncellenme Tarihi")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Kategori seçimi zorunludur.")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
