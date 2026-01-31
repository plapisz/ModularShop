namespace ModularShop.Modules.Catalog.Core.Dtos;

public sealed record CreateProductDto
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required decimal Price { get; init; }
    public required string Sku { get; init; }
    public required int StockQuantity { get; init; }
}