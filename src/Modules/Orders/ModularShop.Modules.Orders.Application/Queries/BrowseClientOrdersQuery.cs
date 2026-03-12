using ModularShop.Modules.Orders.Application.Dtos;
using ModularShop.Shared.Abstractions.Queries;

namespace ModularShop.Modules.Orders.Application.Queries;

public sealed record BrowseClientOrdersQuery(Guid ClientId) : IQuery<IReadOnlyCollection<OrderDto>>;