namespace CLOTHAPI.DTOs
{
    public class OrderDtos
    {
        public record PlaceOrderDto(int AddressId, string PaymentMethod);
        public record OrderDto(int Id, string Status, decimal TotalAmount, DateTime CreatedAt,
            string PaymentMethod, string PaymentStatus, List<OrderItemDto> Items);
        public record OrderItemDto(int ProductId, string ProductName, int Quantity,
            string? Size, string? Color, decimal Price);
    }
}
