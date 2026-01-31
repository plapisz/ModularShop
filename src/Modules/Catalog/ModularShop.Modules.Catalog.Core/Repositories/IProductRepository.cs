using ModularShop.Modules.Catalog.Core.Entities;

namespace ModularShop.Modules.Catalog.Core.Repositories;

public interface IProductRepository
{
    Task<Product?> GetAsync(Guid id);
    Task<IReadOnlyCollection<Product>> BrowseAsync();
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
}