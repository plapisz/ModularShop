using Microsoft.Extensions.DependencyInjection;
using ModularShop.Modules.Catalog.Core.Repositories;
using ModularShop.Modules.Catalog.Core.Services;

namespace ModularShop.Modules.Catalog.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSingleton<IProductRepository, InMemoryProductRepository>();
        services.AddSingleton<IProductService, InMemoryProductService>();

        return services;
    }
}