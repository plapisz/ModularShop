using ModularShop.Shared.Abstractions.Events;

namespace ModularShop.Modules.Orders.Application.Events.External;

public sealed record ProductCreatedEvent(Guid Id, string Name, decimal Price) : IEvent;