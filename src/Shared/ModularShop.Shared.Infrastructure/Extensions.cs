using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModularShop.Shared.Abstractions.Options;
using ModularShop.Shared.Infrastructure.Api;
using ModularShop.Shared.Infrastructure.Auth;
using ModularShop.Shared.Infrastructure.Exceptions;
using ModularShop.Shared.Infrastructure.Modules;

namespace ModularShop.Shared.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SqlServerOptions>(configuration.GetSection("sqlServer"));
        services.Configure<AuthOptions>(configuration.GetSection("auth"));

        var modules = ModuleDiscovery.Discover(configuration);
        foreach (var module in modules)
        {
            module.Register(services, configuration);
        }

        services
            .AddErrorHandling()
            .AddControllers()
            .AddModules(modules);

        services.AddAuth(configuration, modules);

        return services;
    }
    
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseErrorHandling();
        app.UseAuthorization();
        app.UseRouting();
        app.UseAuthorization();

        return app;
    }
    
    public static IHostBuilder AddModuleConfiguration(this IHostBuilder builder)
        => builder.ConfigureAppConfiguration((ctx, cfg) =>
        {
            foreach (var file in GetFiles("module.*.json", ctx))
            {
                cfg.AddJsonFile(file, optional: false, reloadOnChange: true);
            }

            foreach (var file in GetFiles($"module.*.{ctx.HostingEnvironment.EnvironmentName}.json", ctx))
            {
                cfg.AddJsonFile(file, optional: true, reloadOnChange: true);
            }
        });

    private static IEnumerable<string> GetFiles(string pattern, HostBuilderContext ctx) 
        => Directory.EnumerateFiles( 
            ctx.HostingEnvironment.ContentRootPath,
            pattern,
            SearchOption.AllDirectories);
}