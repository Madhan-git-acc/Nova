using System.ComponentModel.DataAnnotations.Schema;

namespace CLOTHAPI.Models
{
    public class ProductImage
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        [Column("image_url")]
        public string ImageUrl { get; set; } = "";
        [Column("is_primary")]
        public bool IsPrimary { get; set; }
    }
}
