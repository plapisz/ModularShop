namespace ModularShop.Shared.Infrastructure.Events;

internal sealed class ModuleBroadcastRegistration(Type receiverType, Func<object, Task> action)
{
    public Type ReceiverType { get; } = receiverType;
    public Func<object, Task> Action { get; } = action;
    public string Key => ReceiverType.Name;
}