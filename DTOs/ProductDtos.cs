namespace CLOTHAPI.DTOs
{
    public class ProductDtos
    {
        public record ProductListDto(int Id, string Name, decimal Price, decimal? SalePrice,
                    string? Brand, bool IsFeatured, string? PrimaryImage, string? CategoryName);
        public record ProductDetailDto(int Id, string Name, string? Description, decimal Price,
            decimal? SalePrice, int StockQuantity, string? Sizes, string? Colors, string? Brand,
            bool IsFeatured, string? CategoryName, List<string> Images, double AverageRating, int ReviewCount);
    }
}
