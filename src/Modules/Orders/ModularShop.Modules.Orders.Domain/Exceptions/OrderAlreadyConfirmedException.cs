using ModularShop.Shared.Abstractions.Exceptions;

namespace ModularShop.Modules.Orders.Domain.Exceptions;

public sealed class OrderAlreadyConfirmedException(string orderNumber) 
    : ModularShopException($"Order: '{orderNumber}' already confirmed.")
{
    public string OrderNumber { get; } = orderNumber;
}