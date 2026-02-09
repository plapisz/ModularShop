using Microsoft.EntityFrameworkCore;
using ModularShop.Modules.Catalog.Core.Entities;
using ModularShop.Modules.Catalog.Core.Repositories;

namespace ModularShop.Modules.Catalog.Core.Infrastructure.Persistence.Repositories;

internal sealed class ProductRepository(CatalogDbContext context) : IProductRepository
{
    public async Task<Product?> GetAsync(Guid id)
        => await context.Products.SingleOrDefaultAsync(x =>x.Id == id);

    public async Task<IReadOnlyCollection<Product>> BrowseAsync()
        => await context.Products.ToListAsync();

    public async Task AddAsync(Product product)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync();
    }
}