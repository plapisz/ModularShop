using ModularShop.Modules.Orders.Domain.Aggregates;

namespace ModularShop.Modules.Orders.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetAsync(Guid id);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
}