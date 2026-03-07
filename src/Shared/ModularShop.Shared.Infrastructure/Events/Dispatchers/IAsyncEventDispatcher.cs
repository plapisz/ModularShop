using ModularShop.Shared.Abstractions.Events;

namespace ModularShop.Shared.Infrastructure.Events.Dispatchers;

internal interface IAsyncEventDispatcher
{
    Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent;
}