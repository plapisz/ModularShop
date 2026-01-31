namespace ModularShop.Modules.Catalog.Core.Entities;

public sealed class Product
{
    public required Guid Id { get; init; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required string Sku { get; set; }
    public required int StockQuantity { get; set; }
    public required bool IsActive { get; set; }
}