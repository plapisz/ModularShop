using ModularShop.Modules.Catalog.Core.Entities;
using ModularShop.Modules.Catalog.Core.Infrastructure.Persistence;

namespace ModularShop.Modules.Catalog.IntegrationTests.Fixtures;

internal sealed class CatalogDataSeeder(CatalogDbContext context)
{
    public async Task<Product> SeedProductAsync()
    {
        var product = ProductFixture.ValidProduct();
        
        context.Products.Add(product);
        await context.SaveChangesAsync();
        
        return product;
    }
}
