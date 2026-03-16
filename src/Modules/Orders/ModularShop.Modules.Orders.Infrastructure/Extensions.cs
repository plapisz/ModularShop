using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularShop.Modules.Orders.Application.ReadRepositories;
using ModularShop.Modules.Orders.Domain.Repositories;
using ModularShop.Modules.Orders.Infrastructure.Persistence.Read.Connection;
using ModularShop.Modules.Orders.Infrastructure.Persistence.Read.Repositories;
using ModularShop.Modules.Orders.Infrastructure.Persistence.Write;
using ModularShop.Modules.Orders.Infrastructure.Persistence.Write.Repositories;
using ModularShop.Shared.Abstractions;
using ModularShop.Shared.Abstractions.Options;

namespace ModularShop.Modules.Orders.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlServerOptions = configuration.GetOptions<SqlServerOptions>("sqlServer");
        services.AddDbContext<OrdersDbContext>(options =>
            options.UseSqlServer( sqlServerOptions.ConnectionString, sqlOptions =>
                sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "orders")));

        services.AddScoped<IProductSnapshotRepository, ProductSnapshotRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>();
        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        
        return services;
    }
}