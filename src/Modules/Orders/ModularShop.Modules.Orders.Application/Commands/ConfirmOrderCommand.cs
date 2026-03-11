using ModularShop.Shared.Abstractions.Commands;

namespace ModularShop.Modules.Orders.Application.Commands;

public sealed record ConfirmOrderCommand(Guid OrderId) : ICommand;