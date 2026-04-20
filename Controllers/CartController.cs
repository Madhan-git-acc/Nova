// Controllers/CartController.cs
using CLOTHAPI.Data;
using CLOTHAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static CLOTHAPI.DTOs.CartDtos;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly AppDbContext _db;
    public CartController(AppDbContext db) { _db = db; }

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var items = await _db.CartItems
            .Where(c => c.UserId == GetUserId())
            .Include(c => c.Product).ThenInclude(p => p!.Images)
            .Select(c => new CartItemDto(
                c.Id, c.ProductId, c.Product!.Name,
                c.Product.SalePrice ?? c.Product.Price,
                c.Quantity, c.Size, c.Color,
                c.Product.Images.Where(i => i.IsPrimary).Select(i => i.ImageUrl).FirstOrDefault()))
            .ToListAsync();
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(AddToCartDto dto)
    {
        var existing = await _db.CartItems.FirstOrDefaultAsync(
            c => c.UserId == GetUserId() && c.ProductId == dto.ProductId
              && c.Size == dto.Size && c.Color == dto.Color);
        if (existing != null)
        {
            existing.Quantity += dto.Quantity;
        }
        else
        {
            _db.CartItems.Add(new CartItem
            {
                UserId = GetUserId(),
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                Size = dto.Size,
                Color = dto.Color
            });
        }
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveFromCart(int id)
    {
        var item = await _db.CartItems.FirstOrDefaultAsync(
            c => c.Id == id && c.UserId == GetUserId());
        if (item == null) return NotFound();
        _db.CartItems.Remove(item);
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> ClearCart()
    {
        var items = _db.CartItems.Where(c => c.UserId == GetUserId());
        _db.CartItems.RemoveRange(items);
        await _db.SaveChangesAsync();
        return Ok();
    }
}