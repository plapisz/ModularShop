using ModularShop.Shared.Abstractions.Exceptions;

namespace ModularShop.Modules.Orders.Domain.Exceptions;

public sealed class InvalidAmountException(decimal amount) : ModularShopException($"Amount: '{amount}' is not valid.")
{
    public decimal Amount { get; } = amount;
}