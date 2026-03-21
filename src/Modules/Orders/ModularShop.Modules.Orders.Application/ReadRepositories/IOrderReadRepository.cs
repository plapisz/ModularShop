using ModularShop.Modules.Orders.Application.Dtos;

namespace ModularShop.Modules.Orders.Application.ReadRepositories;

public interface IOrderReadRepository
{
    Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<OrderDto>> BrowseAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<OrderDto>> BrowseByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken);
}