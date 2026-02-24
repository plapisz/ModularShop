using ModularShop.Modules.Orders.Domain.Entities;

namespace ModularShop.Modules.Orders.Domain.Repositories;

public interface IProductSnapshotRepository
{
    Task<ProductSnapshot?> GetAsync(Guid id);
    Task AddAsync(ProductSnapshot productSnapshot);
    Task UpdateAsync(ProductSnapshot productSnapshot);
}