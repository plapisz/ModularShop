using ModularShop.Modules.Orders.Application.Dtos;

namespace ModularShop.Modules.Orders.Application.ReadRepositories;

public interface IOrderReadRepository
{
    Task<OrderDto?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<OrderDto>> BrowseAsync();
    Task<IReadOnlyList<OrderDto>> BrowseByCustomerIdAsync(Guid customerId);
}