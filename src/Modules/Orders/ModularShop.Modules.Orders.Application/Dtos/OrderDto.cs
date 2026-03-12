using ModularShop.Modules.Orders.Domain.Enums;

namespace ModularShop.Modules.Orders.Application.Dtos;

public sealed record OrderDto
{
    public required Guid Id { get; init; }
    public required string Number { get; init; }
    public required Guid CustomerId { get; init; }
    public required OrderStatus Status { get; init; }
    public required decimal TotalPrice { get; init; }
    public required IReadOnlyCollection<OrderItemDto> Items { get; init; }
}