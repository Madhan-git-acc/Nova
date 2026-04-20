using System.ComponentModel.DataAnnotations.Schema;

namespace CLOTHAPI.Models
{
    public class Review
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        public User? User { get; set; }
        [Column("rating")]
        public int Rating { get; set; }
        [Column("comment")]
        public string? Comment { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
