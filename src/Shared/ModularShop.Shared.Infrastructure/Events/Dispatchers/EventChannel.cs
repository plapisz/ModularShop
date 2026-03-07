using System.Threading.Channels;
using ModularShop.Shared.Abstractions.Events;

namespace ModularShop.Shared.Infrastructure.Events.Dispatchers;

internal sealed class EventChannel : IEventChannel
{
    private readonly Channel<IEvent> _events = Channel.CreateUnbounded<IEvent>();

    public ChannelReader<IEvent> Reader => _events.Reader;
    public ChannelWriter<IEvent> Writer => _events.Writer;
}