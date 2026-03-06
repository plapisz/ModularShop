using ModularShop.Shared.Abstractions.Events;

namespace ModularShop.Modules.Catalog.Core.Events;

public sealed record ProductUpdatedEvent(Guid Id, string Name, decimal Price) : IEvent;