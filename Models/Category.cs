using System.ComponentModel.DataAnnotations.Schema;

namespace CLOTHAPI.Models
{
    public class Category
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = "";
        [Column("slug")]
        public string Slug { get; set; } = "";
        [Column("description")]
        public string? Description { get; set; }
        [Column("image_url")]
        public string? ImageUrl { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
