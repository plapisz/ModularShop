using ModularShop.Modules.Catalog.Core.Entities;
using ModularShop.Modules.Catalog.Core.Infrastructure.Persistence;

namespace ModularShop.Modules.Catalog.IntegrationTests.Fixtures;

internal sealed class CatalogDataSeeder(CatalogDbContext context)
{
    public async Task<Product> SeedProductAsync()
        => await SeedAsync(ProductFixture.ValidProduct());

    public async Task<Product> SeedInactiveProductAsync()
        => await SeedAsync(ProductFixture.InactiveProduct());

    private async Task<Product> SeedAsync(Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();
        
        return product;
    }
}
