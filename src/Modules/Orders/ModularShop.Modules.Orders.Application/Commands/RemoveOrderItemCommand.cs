using ModularShop.Shared.Abstractions.Commands;

namespace ModularShop.Modules.Orders.Application.Commands;

public sealed record RemoveOrderItemCommand(Guid OrderId, Guid ProductId) : ICommand;