using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ModularShop.Shared.Abstractions;
using ModularShop.Shared.Abstractions.Modules;
using ModularShop.Shared.Abstractions.Options;

namespace ModularShop.Shared.Infrastructure.Auth;

internal static class Extensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services, 
        IConfiguration configuration,
        IReadOnlyCollection<IModule> modules)
    {
        var options = configuration.GetOptions<AuthOptions>("auth");
        services
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.Audience = options.Audience;
                o.IncludeErrorDetails = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = options.Issuer,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey))
                };
            });
        
        var policies = modules.SelectMany(m => m.Policies).ToList();
        services.AddAuthorization(authorization =>
        {
            foreach (var policy in policies)
            {
                authorization.AddPolicy(policy, x => x.RequireClaim("permissions", policy));
            }
        });
        
        return services;
    }
}