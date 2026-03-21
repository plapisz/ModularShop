using ModularShop.Modules.Orders.Application.Dtos;
using ModularShop.Modules.Orders.Application.ReadRepositories;
using ModularShop.Shared.Abstractions.Queries;

namespace ModularShop.Modules.Orders.Application.Queries.Handlers;

public sealed class BrowseOrdersQueryHandler(IOrderReadRepository orderReadRepository) 
    : IQueryHandler<BrowseOrdersQuery, IReadOnlyCollection<OrderDto>>
{
    public async Task<IReadOnlyCollection<OrderDto>> HandleAsync(BrowseOrdersQuery query, CancellationToken cancellationToken)
        => await orderReadRepository.BrowseAsync(cancellationToken);
}