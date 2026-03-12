using ModularShop.Modules.Orders.Application.Dtos;
using ModularShop.Modules.Orders.Application.ReadRepositories;
using ModularShop.Shared.Abstractions.Queries;

namespace ModularShop.Modules.Orders.Application.Queries.Handlers;

public sealed class BrowseClientOrdersQueryHandler(IOrderReadRepository orderReadRepository)
    : IQueryHandler<BrowseClientOrdersQuery, IReadOnlyCollection<OrderDto>>
{
    public async Task<IReadOnlyCollection<OrderDto>> HandleAsync(BrowseClientOrdersQuery query)
        => await orderReadRepository.BrowseByClientIdAsync(query.ClientId);
}