using System.Threading.Channels;
using ModularShop.Shared.Abstractions.Events;

namespace ModularShop.Shared.Infrastructure.Events.Dispatchers;

public interface IEventChannel
{
    ChannelReader<IEvent> Reader { get; }
    ChannelWriter<IEvent> Writer { get; }
}