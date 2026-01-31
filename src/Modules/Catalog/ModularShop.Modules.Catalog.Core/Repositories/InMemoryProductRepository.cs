using System.Collections.Concurrent;
using ModularShop.Modules.Catalog.Core.Entities;

namespace ModularShop.Modules.Catalog.Core.Repositories;

internal sealed class InMemoryProductRepository : IProductRepository
{
    private readonly ConcurrentDictionary<Guid, Product> _products = new();
    
    public Task<Product?> GetAsync(Guid id) 
        => Task.FromResult(_products.GetValueOrDefault(id));

    public Task<IReadOnlyCollection<Product>> BrowseAsync()
        => Task.FromResult<IReadOnlyCollection<Product>>(_products.Values.ToList());

    public Task AddAsync(Product product)
    {
        _products.TryAdd(product.Id, product);
        
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Product product)
    {
        _products[product.Id] = product;
        
        return Task.CompletedTask;
    }
}