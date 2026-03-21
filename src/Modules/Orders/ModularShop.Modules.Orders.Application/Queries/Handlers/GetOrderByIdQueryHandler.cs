using ModularShop.Modules.Orders.Application.Dtos;
using ModularShop.Modules.Orders.Application.ReadRepositories;
using ModularShop.Shared.Abstractions.Queries;

namespace ModularShop.Modules.Orders.Application.Queries.Handlers;

public sealed class GetOrderByIdQueryHandler(IOrderReadRepository orderReadRepository) 
    : IQueryHandler<GetOrderByIdQuery, OrderDto?>
{
    public async Task<OrderDto?> HandleAsync(GetOrderByIdQuery query, CancellationToken cancellationToken)
        => await orderReadRepository.GetByIdAsync(query.Id, cancellationToken);
}