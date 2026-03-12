namespace ModularShop.Modules.Orders.Application.Dtos;

public sealed record OrderItemDto
{
    public required string ProductName { get; init; }
    public required decimal UnitPrice { get; init; }
    public required int Quantity { get; init; }
    public required decimal TotalPrice { get; init; }
}