using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularShop.Shared.Abstractions.Modules;

namespace ModularShop.Modules.Orders.Api;

internal sealed class OrdersModule : IModule
{
    internal const string BasePath = "orders";
    
    public string Name => "Orders";
    public string Path => BasePath;
    public IReadOnlyCollection<string> Policies => [];
    
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        
    }
}