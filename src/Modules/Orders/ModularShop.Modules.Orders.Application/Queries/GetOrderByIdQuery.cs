using ModularShop.Modules.Orders.Application.Dtos;
using ModularShop.Shared.Abstractions.Queries;

namespace ModularShop.Modules.Orders.Application.Queries;

public sealed record GetOrderByIdQuery(Guid Id) : IQuery<OrderDto?>;