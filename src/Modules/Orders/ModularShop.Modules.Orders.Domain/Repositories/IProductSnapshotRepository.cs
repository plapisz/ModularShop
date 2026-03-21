using ModularShop.Modules.Orders.Domain.Entities;

namespace ModularShop.Modules.Orders.Domain.Repositories;

public interface IProductSnapshotRepository
{
    Task<ProductSnapshot?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(ProductSnapshot productSnapshot, CancellationToken cancellationToken);
    Task UpdateAsync(ProductSnapshot productSnapshot, CancellationToken cancellationToken);
}