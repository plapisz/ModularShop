using ModularShop.Shared.Abstractions.Events;

namespace ModularShop.Shared.Infrastructure.Events;

internal interface IModuleClient
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : class, IEvent;
}