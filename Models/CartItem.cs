using System.ComponentModel.DataAnnotations.Schema;

namespace CLOTHAPI.Models
{
    public class CartItem
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        public User? User { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; } = 1;
        [Column("size")]
        public string? Size { get; set; }
        [Column("color")]
        public string? Color { get; set; }
        [Column("added_at")]
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
