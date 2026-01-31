namespace ModularShop.Modules.Catalog.Core.Dtos;

public sealed record ProductDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required decimal Price { get; init; }
    public required string Sku { get; init; }
    public required int StockQuantity { get; init; }
}