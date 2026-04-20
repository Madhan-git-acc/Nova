using System.ComponentModel.DataAnnotations.Schema;

namespace CLOTHAPI.Models
{
    public class OrderItem
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("order_id")]
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("size")]
        public string? Size { get; set; }
        [Column("color")]
        public string? Color { get; set; }
        [Column("price_at_purchase")]
        public decimal PriceAtPurchase { get; set; }
    }
}
