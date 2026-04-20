namespace CLOTHAPI.Data;

using CLOTHAPI.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<User>().ToTable("users");
        mb.Entity<Category>().ToTable("categories");
        mb.Entity<Product>().ToTable("products");
        mb.Entity<ProductImage>().ToTable("product_images");
        mb.Entity<CartItem>().ToTable("cart_items");
        mb.Entity<Order>().ToTable("orders");
        mb.Entity<OrderItem>().ToTable("order_items");
        mb.Entity<Address>().ToTable("addresses");
        mb.Entity<Review>().ToTable("reviews");

        // Column name mappings (snake_case ↔ PascalCase)
        mb.Entity<User>().Property(u => u.PasswordHash).HasColumnName("password_hash");
        mb.Entity<User>().Property(u => u.FirstName).HasColumnName("first_name");
        mb.Entity<User>().Property(u => u.LastName).HasColumnName("last_name");
        mb.Entity<User>().Property(u => u.CreatedAt).HasColumnName("created_at");
        mb.Entity<Product>().Property(p => p.SalePrice).HasColumnName("sale_price");
        mb.Entity<Product>().Property(p => p.StockQuantity).HasColumnName("stock_quantity");
        mb.Entity<Product>().Property(p => p.CategoryId).HasColumnName("category_id");
        mb.Entity<Product>().Property(p => p.IsFeatured).HasColumnName("is_featured");
        mb.Entity<Product>().Property(p => p.CreatedAt).HasColumnName("created_at");
        mb.Entity<ProductImage>().Property(p => p.ProductId).HasColumnName("product_id");
        mb.Entity<ProductImage>().Property(p => p.ImageUrl).HasColumnName("image_url");
        mb.Entity<ProductImage>().Property(p => p.IsPrimary).HasColumnName("is_primary");
        mb.Entity<CartItem>().Property(c => c.UserId).HasColumnName("user_id");
        mb.Entity<CartItem>().Property(c => c.ProductId).HasColumnName("product_id");
        mb.Entity<CartItem>().Property(c => c.AddedAt).HasColumnName("added_at");
        mb.Entity<Order>().Property(o => o.UserId).HasColumnName("user_id");
        mb.Entity<Order>().Property(o => o.AddressId).HasColumnName("address_id");
        mb.Entity<Order>().Property(o => o.TotalAmount).HasColumnName("total_amount");
        mb.Entity<Order>().Property(o => o.PaymentMethod).HasColumnName("payment_method");
        mb.Entity<Order>().Property(o => o.PaymentStatus).HasColumnName("payment_status");
        mb.Entity<Order>().Property(o => o.CreatedAt).HasColumnName("created_at");
        mb.Entity<OrderItem>().Property(o => o.OrderId).HasColumnName("order_id");
        mb.Entity<OrderItem>().Property(o => o.ProductId).HasColumnName("product_id");
        mb.Entity<OrderItem>().Property(o => o.PriceAtPurchase).HasColumnName("price_at_purchase");
        mb.Entity<Address>().Property(a => a.UserId).HasColumnName("user_id");
        mb.Entity<Address>().Property(a => a.FullName).HasColumnName("full_name");
        mb.Entity<Address>().Property(a => a.Line1).HasColumnName("line1");
        mb.Entity<Address>().Property(a => a.Line2).HasColumnName("line2");
        mb.Entity<Address>().Property(a => a.PostalCode).HasColumnName("postal_code");
        mb.Entity<Address>().Property(a => a.IsDefault).HasColumnName("is_default");
        mb.Entity<Review>().Property(r => r.ProductId).HasColumnName("product_id");
        mb.Entity<Review>().Property(r => r.UserId).HasColumnName("user_id");
        mb.Entity<Review>().Property(r => r.CreatedAt).HasColumnName("created_at");
        mb.Entity<Category>().Property(c => c.ImageUrl).HasColumnName("image_url");
    }
}