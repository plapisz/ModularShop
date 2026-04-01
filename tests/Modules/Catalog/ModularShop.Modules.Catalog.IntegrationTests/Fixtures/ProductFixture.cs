using Bogus;
using ModularShop.Modules.Catalog.Core.Dtos;

namespace ModularShop.Modules.Catalog.IntegrationTests.Fixtures;

public static class ProductFixture
{
    private static readonly Faker<CreateProductDto> CreateProductDtoFaker = new Faker<CreateProductDto>()
        .RuleFor(x => x.Name, f => f.Commerce.ProductName())
        .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
        .RuleFor(x => x.Price, f => f.Finance.Amount(1, 10000))
        .RuleFor(x => x.Sku, f => f.Commerce.Ean8())
        .RuleFor(x => x.StockQuantity, f => f.Random.Int(1, 100));
    
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
}