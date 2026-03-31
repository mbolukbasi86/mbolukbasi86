using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootStrapECommerce.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        // BLOB image data stored in the database
        [Required]
        public byte[] Data { get; set; } = Array.Empty<byte>();

        [StringLength(100)]
        public string ContentType { get; set; } = "image/jpeg";

        // Navigation property
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }
    }
}
