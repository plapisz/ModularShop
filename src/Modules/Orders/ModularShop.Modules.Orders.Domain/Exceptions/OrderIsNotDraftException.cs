using ModularShop.Shared.Abstractions.Exceptions;

namespace ModularShop.Modules.Orders.Domain.Exceptions;

public sealed class OrderIsNotDraftException(string orderNumber) 
    : ModularShopException($"Order '{orderNumber} is not draft.")
{
    public string OrderNumber { get; } = orderNumber;
}