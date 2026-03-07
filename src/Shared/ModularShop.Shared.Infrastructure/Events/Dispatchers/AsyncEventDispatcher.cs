using ModularShop.Shared.Abstractions.Events;

namespace ModularShop.Shared.Infrastructure.Events.Dispatchers;

internal sealed class AsyncEventDispatcher(IEventChannel eventChannel) : IAsyncEventDispatcher
{
    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent
        => await eventChannel.Writer.WriteAsync(@event);
}