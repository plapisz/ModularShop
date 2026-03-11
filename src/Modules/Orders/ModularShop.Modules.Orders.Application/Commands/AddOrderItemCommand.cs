using ModularShop.Shared.Abstractions.Commands;

namespace ModularShop.Modules.Orders.Application.Commands;

public sealed record AddOrderItemCommand(Guid OrderId, Guid ProductId, int Quantity) : ICommand;