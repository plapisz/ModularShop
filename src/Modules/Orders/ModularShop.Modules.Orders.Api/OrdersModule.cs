using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularShop.Modules.Orders.Infrastructure;
using ModularShop.Shared.Abstractions.Modules;

namespace ModularShop.Modules.Orders.Api;

internal sealed class OrdersModule : IModule
{
    public string Name => "Orders";
    public string Path => "orders";
    public IReadOnlyCollection<string> Policies => [ Security.Policies.Order.Read, Security.Policies.Order.Write ];
    
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
    }
}