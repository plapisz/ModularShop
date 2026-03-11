using ModularShop.Shared.Abstractions.Exceptions;

namespace ModularShop.Modules.Orders.Application.Exceptions;

public sealed class ProductNotFoundException(Guid id) : ModularShopException($"Product with ID: '{id}' was not found.")
{
    public Guid Id { get; } = id;
}