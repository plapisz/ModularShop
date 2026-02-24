using ModularShop.Shared.Abstractions.Exceptions;

namespace ModularShop.Modules.Orders.Domain.Exceptions;

public sealed class InvalidProductNameException() : ModularShopException("Invalid product name.");