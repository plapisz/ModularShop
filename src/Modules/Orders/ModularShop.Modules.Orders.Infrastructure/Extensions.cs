using Microsoft.Extensions.DependencyInjection;
using ModularShop.Modules.Orders.Domain.Repositories;
using ModularShop.Modules.Orders.Infrastructure.Repositories;

namespace ModularShop.Modules.Orders.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IProductSnapshotRepository, InMemoryProductSnapshotRepository>();
        
        return services;
    }
}