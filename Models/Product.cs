using System.ComponentModel.DataAnnotations.Schema;

namespace CLOTHAPI.Models
{
    public class Product
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = "";
        [Column("description")]
        public string? Description { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [Column("sale_price")]
        public decimal? SalePrice { get; set; }
        [Column("stock_quantity")]
        public int StockQuantity { get; set; }
        [Column("category_id")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        [Column("sizes")]
        public string? Sizes { get; set; }
        [Column("colors")]
        public string? Colors { get; set; }
        [Column("brand")]
        public string? Brand { get; set; }
        [Column("is_featured")]
        public bool IsFeatured { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
