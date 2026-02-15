using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ModularShop.Modules.Identity.Core.Entities;
using ModularShop.Modules.Identity.Core.Repositories;
using ModularShop.Modules.Identity.Core.Security;
using ModularShop.Modules.Identity.Core.Services;
using ModularShop.Modules.Identity.Core.Time;

namespace ModularShop.Modules.Identity.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSingleton<IUserRepository, InMemoryUserRepository>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddSingleton<IPasswordManager, PasswordManager>();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddSingleton<IAuthenticator, Authenticator>();
        services.AddSingleton<IClock, Clock>();
        
        return services;
    }
}