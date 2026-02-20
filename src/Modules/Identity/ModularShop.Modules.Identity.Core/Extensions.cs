using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModularShop.Modules.Identity.Core.Entities;
using ModularShop.Modules.Identity.Core.Infrastructure.Persistence;
using ModularShop.Modules.Identity.Core.Infrastructure.Persistence.Repositories;
using ModularShop.Modules.Identity.Core.Repositories;
using ModularShop.Modules.Identity.Core.Security;
using ModularShop.Modules.Identity.Core.Services;
using ModularShop.Modules.Identity.Core.Time;
using ModularShop.Shared.Abstractions;
using ModularShop.Shared.Abstractions.Options;

namespace ModularShop.Modules.Identity.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlServerOptions = configuration.GetOptions<SqlServerOptions>("sqlServer");
        services.AddDbContext<IdentityDbContext>(options =>
            options.UseSqlServer( sqlServerOptions.ConnectionString, sqlOptions =>
                sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "identity")));
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddSingleton<IPasswordManager, PasswordManager>();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddSingleton<IAuthenticator, Authenticator>();
        services.AddSingleton<IClock, Clock>();
        
        return services;
    }
}