using ModularShop.Shared.Abstractions.Commands;

namespace ModularShop.Modules.Orders.Application.Commands;

public sealed record CancelOrderCommand(Guid OrderId) : ICommand;