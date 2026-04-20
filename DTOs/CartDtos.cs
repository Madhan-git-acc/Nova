namespace CLOTHAPI.DTOs
{
    public class CartDtos
    {
        public record AddToCartDto(int ProductId, int Quantity, string? Size, string? Color);
        public record CartItemDto(int Id, int ProductId, string ProductName, decimal Price,
            int Quantity, string? Size, string? Color, string? ImageUrl);
    }
}
