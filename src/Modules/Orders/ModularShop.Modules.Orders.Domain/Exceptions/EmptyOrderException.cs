using ModularShop.Shared.Abstractions.Exceptions;

namespace ModularShop.Modules.Orders.Domain.Exceptions;

public sealed class EmptyOrderException(string orderNumber) : ModularShopException($"Order: '{orderNumber}' is empty.")
{
    public string OrderNumber { get; } = orderNumber;
}