using ModularShop.Modules.Catalog.Core.Entities;

namespace ModularShop.Modules.Catalog.Core.Repositories;

public interface IProductRepository
{
    Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Product>> BrowseAsync(CancellationToken cancellationToken);
    Task AddAsync(Product product, CancellationToken cancellationToken);
    Task UpdateAsync(Product product, CancellationToken cancellationToken);
}