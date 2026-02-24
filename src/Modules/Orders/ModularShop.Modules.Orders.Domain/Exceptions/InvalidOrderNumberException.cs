using ModularShop.Shared.Abstractions.Exceptions;

namespace ModularShop.Modules.Orders.Domain.Exceptions;

public sealed class InvalidOrderNumberException() : ModularShopException("Order number is invalid.");