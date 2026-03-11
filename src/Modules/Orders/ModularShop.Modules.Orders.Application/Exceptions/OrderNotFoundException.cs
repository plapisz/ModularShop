using ModularShop.Shared.Abstractions.Exceptions;

namespace ModularShop.Modules.Orders.Application.Exceptions;

public sealed class OrderNotFoundException(Guid id) : ModularShopException($"Order with ID: '{id}' was not found.")
{
    public Guid Id { get; } = id;
}