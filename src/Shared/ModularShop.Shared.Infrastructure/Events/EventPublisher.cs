using ModularShop.Shared.Abstractions.Events;
using ModularShop.Shared.Infrastructure.Events.Dispatchers;

namespace ModularShop.Shared.Infrastructure.Events;

internal sealed class EventPublisher(IAsyncEventDispatcher asyncEventDispatcher) : IEventPublisher
{
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : class, IEvent
        => await asyncEventDispatcher.PublishAsync(@event, cancellationToken);
}