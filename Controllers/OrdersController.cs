// Controllers/OrdersController.cs
using CLOTHAPI.Data;
using CLOTHAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static CLOTHAPI.DTOs.OrderDtos;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _db;
    public OrdersController(AppDbContext db) { _db = db; }

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(PlaceOrderDto dto)
    {
        var cartItems = await _db.CartItems
            .Where(c => c.UserId == GetUserId())
            .Include(c => c.Product)
            .ToListAsync();
        if (!cartItems.Any()) return BadRequest("Cart is empty.");

        var total = cartItems.Sum(c => (c.Product!.SalePrice ?? c.Product.Price) * c.Quantity);
        var order = new Order
        {
            UserId = GetUserId(),
            AddressId = dto.AddressId,
            TotalAmount = total,
            PaymentMethod = dto.PaymentMethod,
            OrderItems = cartItems.Select(c => new OrderItem
            {
                ProductId = c.ProductId,
                Quantity = c.Quantity,
                Size = c.Size,
                Color = c.Color,
                PriceAtPurchase = c.Product!.SalePrice ?? c.Product.Price
            }).ToList()
        };
        _db.Orders.Add(order);
        _db.CartItems.RemoveRange(cartItems);
        await _db.SaveChangesAsync();
        return Ok(new { order.Id, order.TotalAmount, order.Status });
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _db.Orders
            .Where(o => o.UserId == GetUserId())
            .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => new OrderDto(
                o.Id, o.Status, o.TotalAmount, o.CreatedAt,
                o.PaymentMethod ?? "", o.PaymentStatus,
                o.OrderItems.Select(oi => new OrderItemDto(
                    oi.ProductId, oi.Product!.Name, oi.Quantity,
                    oi.Size, oi.Color, oi.PriceAtPurchase)).ToList()))
            .ToListAsync();
        return Ok(orders);
    }
}