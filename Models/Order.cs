using System.ComponentModel.DataAnnotations.Schema;

namespace CLOTHAPI.Models
{
    public class Order
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        public User? User { get; set; }
        [Column("address_id")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        [Column("status")]
        public string Status { get; set; } = "pending";
        [Column("total_amount")]
        public decimal TotalAmount { get; set; }
        [Column("payment_method")]
        public string? PaymentMethod { get; set; }
        [Column("payment_status")]
        public string PaymentStatus { get; set; } = "unpaid";
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
