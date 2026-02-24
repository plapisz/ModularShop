using ModularShop.Shared.Abstractions.Exceptions;

namespace ModularShop.Modules.Orders.Domain.Exceptions;

public sealed class InvalidMultiplierException(int factor) : ModularShopException($"Factor: '{factor}' is not valid.")
{
    public int Factor { get; } = factor;
}