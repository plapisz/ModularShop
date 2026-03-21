using ModularShop.Modules.Orders.Application.Dtos;
using ModularShop.Modules.Orders.Application.ReadRepositories;
using ModularShop.Shared.Abstractions.Queries;

namespace ModularShop.Modules.Orders.Application.Queries.Handlers;

public sealed class BrowseCustomerOrdersQueryHandler(IOrderReadRepository orderReadRepository)
    : IQueryHandler<BrowseCustomerOrdersQuery, IReadOnlyCollection<OrderDto>>
{
    public async Task<IReadOnlyCollection<OrderDto>> HandleAsync(BrowseCustomerOrdersQuery query, CancellationToken cancellationToken)
        => await orderReadRepository.BrowseByCustomerIdAsync(query.CustomerId, cancellationToken);
}