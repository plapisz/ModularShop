using ModularShop.Shared.Abstractions.Events;

namespace ModularShop.Modules.Orders.Application.Events.External;

public sealed record ProductDeactivatedEvent(Guid Id) : IEvent;