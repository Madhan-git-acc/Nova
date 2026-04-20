using System.ComponentModel.DataAnnotations.Schema;

namespace CLOTHAPI.Models
{
    public class Address
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        public User? User { get; set; }
        [Column("full_name")]
        public string? FullName { get; set; }
        [Column("line1")]
        public string? Line1 { get; set; }
        [Column("line2")]
        public string? Line2 { get; set; }
        [Column("city")]
        public string? City { get; set; }
        [Column("state")]
        public string? State { get; set; }
        [Column("postal_code")]
        public string? PostalCode { get; set; }
        [Column("country")]
        public string Country { get; set; } = "India";
        [Column("is_default")]
        public bool IsDefault { get; set; }
    }
}
