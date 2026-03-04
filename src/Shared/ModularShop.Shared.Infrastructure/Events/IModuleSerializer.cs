namespace ModularShop.Shared.Infrastructure.Events;

internal interface IModuleSerializer
{
    byte[] Serialize<T>(T value);
    object Deserialize(byte[] value, Type type);
}