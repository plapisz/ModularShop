using ModularShop.Shared.Abstractions.Events;

namespace ModularShop.Modules.Catalog.Core.Events;

public sealed record ProductCreatedEvent(Guid Id, string Name, decimal Price) : IEvent;