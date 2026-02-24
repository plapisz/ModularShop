using ModularShop.Shared.Abstractions.Exceptions;

namespace ModularShop.Modules.Orders.Domain.Exceptions;

public sealed class EmptyMoneyCollectionException() : ModularShopException("Money collection is empty.");