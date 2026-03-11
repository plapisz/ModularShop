using ModularShop.Shared.Abstractions.Commands;

namespace ModularShop.Modules.Orders.Application.Commands;

public sealed record CreateOrderCommand(Guid OrderId, Guid CustomerId) : ICommand;