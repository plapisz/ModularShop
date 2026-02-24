using ModularShop.Shared.Abstractions.Exceptions;

namespace ModularShop.Modules.Orders.Domain.Exceptions;

public sealed class InvalidCurrencyException() : ModularShopException("Invalid currency.");