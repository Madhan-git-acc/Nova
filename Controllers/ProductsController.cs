// Controllers/ProductsController.cs
using CLOTHAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CLOTHAPI.DTOs.ProductDtos;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _db;
    public ProductsController(AppDbContext db) { _db = db; }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int? categoryId,
        [FromQuery] string? search,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] bool? featured,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 12)
    {

        var query = _db.Products
            .Include(p => p.Category)
            .Include(p => p.Images)
            .AsQueryable();

        if (categoryId.HasValue) query = query.Where(p => p.CategoryId == categoryId);
        if (!string.IsNullOrEmpty(search))
        {
            var term = $"%{search.Trim()}%";
            query = query.Where(p =>
                EF.Functions.ILike(p.Name, term) ||
                (p.Brand != null && EF.Functions.ILike(p.Brand, term)));
        }
        if (minPrice.HasValue) query = query.Where(p => p.Price >= minPrice);
        if (maxPrice.HasValue) query = query.Where(p => p.Price <= maxPrice);
        if (featured.HasValue) query = query.Where(p => p.IsFeatured == featured);

        var total = await query.CountAsync();
        var products = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ProductListDto(
                p.Id, p.Name, p.Price, p.SalePrice, p.Brand, p.IsFeatured,
                p.Images.Where(i => i.IsPrimary).Select(i => i.ImageUrl).FirstOrDefault(),
                p.Category != null ? p.Category.Name : null))
            .ToListAsync();

        return Ok(new { Total = total, Page = page, PageSize = pageSize, Data = products });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var p = await _db.Products
            .Include(p => p.Category)
            .Include(p => p.Images)
            .Include(p => p.Reviews)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (p == null) return NotFound();
        var avgRating = p.Reviews.Any() ? p.Reviews.Average(r => r.Rating) : 0;
        return Ok(new ProductDetailDto(
            p.Id, p.Name, p.Description, p.Price, p.SalePrice, p.StockQuantity,
            p.Sizes, p.Colors, p.Brand, p.IsFeatured,
            p.Category?.Name,
            p.Images.Select(i => i.ImageUrl).ToList(),
            Math.Round(avgRating, 1), p.Reviews.Count));
    }

    [HttpGet("featured")]
    public async Task<IActionResult> GetFeatured()
    {
        var products = await _db.Products
            .Where(p => p.IsFeatured)
            .Include(p => p.Images)
            .Include(p => p.Category)
            .Select(p => new ProductListDto(
                p.Id, p.Name, p.Price, p.SalePrice, p.Brand, p.IsFeatured,
                p.Images.Where(i => i.IsPrimary).Select(i => i.ImageUrl).FirstOrDefault(),
                p.Category != null ? p.Category.Name : null))
            .ToListAsync();
        return Ok(products);
    }
}