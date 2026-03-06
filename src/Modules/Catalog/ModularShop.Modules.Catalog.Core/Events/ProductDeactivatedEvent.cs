using ModularShop.Shared.Abstractions.Events;

namespace ModularShop.Modules.Catalog.Core.Events;

public sealed record ProductDeactivatedEvent(Guid Id) : IEvent;