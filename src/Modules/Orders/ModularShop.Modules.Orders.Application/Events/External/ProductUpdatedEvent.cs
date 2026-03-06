using ModularShop.Shared.Abstractions.Events;

namespace ModularShop.Modules.Orders.Application.Events.External;

public sealed record ProductUpdatedEvent(Guid Id, string Name, decimal Price) : IEvent;