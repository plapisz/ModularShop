using ModularShop.Shared.Abstractions.Events;

namespace ModularShop.Shared.Infrastructure.Events.Dispatchers;

internal interface IAsyncEventDispatcher
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : class, IEvent;
}