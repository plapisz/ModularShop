using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ModularShop.Shared.Infrastructure.Events.Dispatchers;

internal sealed class BackgroundDispatcher(
    IEventChannel eventChannel, 
    IModuleClient moduleClient, 
    ILogger<BackgroundDispatcher> logger) 
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Running the background dispatcher.");

        await foreach (var @event in eventChannel.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                await moduleClient.PublishAsync(@event, stoppingToken);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);
            }
        }
        
        logger.LogInformation("Finished running the background dispatcher.");
    }
}