using ModularShop.Modules.Orders.Domain.Aggregates;

namespace ModularShop.Modules.Orders.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(Order order, CancellationToken cancellationToken);
    Task UpdateAsync(Order order, CancellationToken cancellationToken);
}