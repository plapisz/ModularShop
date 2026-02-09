using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularShop.Modules.Catalog.Core.Infrastructure.Persistence;
using ModularShop.Modules.Catalog.Core.Infrastructure.Persistence.Repositories;
using ModularShop.Modules.Catalog.Core.Repositories;
using ModularShop.Modules.Catalog.Core.Services;
using ModularShop.Shared.Abstractions;
using ModularShop.Shared.Abstractions.Options;

namespace ModularShop.Modules.Catalog.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlServerOptions = configuration.GetOptions<SqlServerOptions>("sqlServer");
        services.AddDbContext<CatalogDbContext>(options =>
            options.UseSqlServer( sqlServerOptions.ConnectionString, sqlOptions =>
                sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "catalog")));

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}