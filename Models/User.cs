using System.ComponentModel.DataAnnotations.Schema;

namespace CLOTHAPI.Models
{
    public class User
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("email")]
        public string Email { get; set; } = "";
        [Column("password_hash")]
        public string PasswordHash { get; set; } = "";
        [Column("first_name")]
        public string FirstName { get; set; } = "";
        [Column("last_name")]
        public string LastName { get; set; } = "";
        [Column("role")]
        public string Role { get; set; } = "customer";
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
