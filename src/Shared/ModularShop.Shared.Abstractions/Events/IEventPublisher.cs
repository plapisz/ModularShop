namespace ModularShop.Shared.Abstractions.Events;

public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent;
}