using Bogus;
using ModularShop.Modules.Catalog.Core.Dtos;
using ModularShop.Modules.Catalog.Core.Entities;

namespace ModularShop.Modules.Catalog.IntegrationTests.Fixtures;

public static class ProductFixture
{
    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
        .RuleFor(x => x.Id, f => f.Random.Guid())
        .RuleFor(x => x.Name, f => f.Commerce.ProductName())
        .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
        .RuleFor(x => x.Price, f => f.Finance.Amount(1, 10000))
        .RuleFor(x => x.Sku, f => f.Commerce.Ean8())
        .RuleFor(x => x.StockQuantity, f => f.Random.Int(1, 100)) 
        .RuleFor(x => x.IsActive, _ => true);
    
    private static readonly Faker<CreateProductDto> CreateProductDtoFaker = new Faker<CreateProductDto>()
        .RuleFor(x => x.Name, f => f.Commerce.ProductName())
        .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
        .RuleFor(x => x.Price, f => f.Finance.Amount(1, 10000))
        .RuleFor(x => x.Sku, f => f.Commerce.Ean8())
        .RuleFor(x => x.StockQuantity, f => f.Random.Int(1, 100));
    
    private static readonly Faker<UpdateProductDto> UpdateProductDtoFaker = new Faker<UpdateProductDto>()
        .RuleFor(x => x.Name, f => f.Commerce.ProductName())
        .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
        .RuleFor(x => x.Price, f => f.Finance.Amount(1, 10000))
        .RuleFor(x => x.Sku, f => f.Commerce.Ean8())
        .RuleFor(x => x.StockQuantity, f => f.Random.Int(1, 100));

    public static Product ValidProduct()
        => ProductFaker.Generate();
    
    public static CreateProductDto ValidCreateProductDto()
        => CreateProductDtoFaker.Generate();
    
    public static CreateProductDto InvalidCreateProductDto() 
        => new()
        {
            Name = "",
            Description = "",
            Price = -1,
            Sku = "",
            StockQuantity = -5
        };
    
    public static UpdateProductDto ValidUpdateProductDto()
        => UpdateProductDtoFaker.Generate();
    
    public static UpdateProductDto InvalidUpdateProductDto() 
        => new()
        {
            Id = Guid.NewGuid(),
            Name = "",
            Description = "",
            Price = -1,
            Sku = "",
            StockQuantity = -5
        };
}