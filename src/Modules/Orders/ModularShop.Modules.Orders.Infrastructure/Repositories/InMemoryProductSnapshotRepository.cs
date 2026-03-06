using System.Collections.Concurrent;
using ModularShop.Modules.Orders.Domain.Entities;
using ModularShop.Modules.Orders.Domain.Repositories;

namespace ModularShop.Modules.Orders.Infrastructure.Repositories;

internal sealed class InMemoryProductSnapshotRepository : IProductSnapshotRepository
{
    private readonly ConcurrentDictionary<Guid, ProductSnapshot> _productSnapshots = new();
    
    public Task<ProductSnapshot?> GetAsync(Guid id)
        => Task.FromResult(_productSnapshots.GetValueOrDefault(id));

    public Task AddAsync(ProductSnapshot productSnapshot)
    {
        _productSnapshots.TryAdd(productSnapshot.Id, productSnapshot);
        
        return Task.CompletedTask;
    }

    public Task UpdateAsync(ProductSnapshot productSnapshot)
    {
        _productSnapshots[productSnapshot.Id] = productSnapshot;
        
        return Task.CompletedTask;
    }
}