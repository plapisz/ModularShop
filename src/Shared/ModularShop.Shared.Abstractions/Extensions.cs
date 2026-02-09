using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ModularShop.Shared.Abstractions;

public static class Extensions
{
    public static IServiceCollection RegisterOptions<TOptions>(
        this IServiceCollection services,
        string sectionName)
        where TOptions : class
    {
        services.AddOptions<TOptions>()
            .BindConfiguration(sectionName);

        return services;
    }
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }
}