using ModularShop.Shared.Abstractions.Exceptions;

namespace ModularShop.Modules.Orders.Domain.Exceptions;

public sealed class InvalidQuantityException(int quantity) : ModularShopException($"Quantity: '{quantity} is invalid.")
{
    public int Quantity { get; } = quantity;
}