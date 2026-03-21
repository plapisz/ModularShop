using Microsoft.EntityFrameworkCore;
using ModularShop.Modules.Catalog.Core.Entities;
using ModularShop.Modules.Catalog.Core.Repositories;

namespace ModularShop.Modules.Catalog.Core.Infrastructure.Persistence.Repositories;

internal sealed class ProductRepository(CatalogDbContext context) : IProductRepository
{
    public async Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken)
        => await context.Products.SingleOrDefaultAsync(x =>x.Id == id, cancellationToken);

    public async Task<IReadOnlyCollection<Product>> BrowseAsync(CancellationToken cancellationToken)
        => await context.Products.ToListAsync(cancellationToken);

    public async Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        await context.Products.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync(cancellationToken);
    }
}