using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ModularShop.Modules.Catalog.Core.Infrastructure.Persistence;
using ModularShop.Shared.Abstractions.Events;
using Testcontainers.MsSql;

namespace ModularShop.Modules.Catalog.IntegrationTests.Common;

public sealed class CatalogWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _sqlContainer = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
        .WithPassword("X9!rTq7@Lp2#")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");
        
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<DbContextOptions<CatalogDbContext>>();
            services.AddDbContext<CatalogDbContext>(options =>
                options.UseSqlServer(
                    _sqlContainer.GetConnectionString(),
                    sqlOptions => sqlOptions.MigrationsHistoryTable(
                        "__EFMigrationsHistory", "catalog")));

            services.RemoveAll<IAuthenticationSchemeProvider>();
            services.RemoveAll<IAuthenticationHandlerProvider>();
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = TestAuthenticationHandler.SchemeName;
                o.DefaultChallengeScheme = TestAuthenticationHandler.SchemeName;
            }).AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(TestAuthenticationHandler.SchemeName, _ => { });

            services.RemoveAll<IEventPublisher>();
            services.AddSingleton<IEventPublisher, NullEventPublisher>();
        });
    }

    public async Task InitializeAsync()
    {
        await _sqlContainer.StartAsync();

        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        await db.Database.MigrateAsync();
    }

    public new async Task DisposeAsync()
    {
        await _sqlContainer.StopAsync();
        await _sqlContainer.DisposeAsync();
    }

    public HttpClient CreateClientWithPolicies(params string[] policies)
    {
        var client = CreateClient();
        
        client.DefaultRequestHeaders.Add(TestAuthenticationHandler.ClaimsHeader, string.Join(",", policies));

        return client;
    }
    
    public HttpClient CreateClientWithoutAuthentication()
    {
        var client = CreateClient();
        
        client.DefaultRequestHeaders.Add(TestAuthenticationHandler.UnauthenticatedHeader, "true");
        
        return client;
    }

    public async Task CleanDatabaseAsync()
    {
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        await db.Database.ExecuteSqlRawAsync("DELETE FROM catalog.Products");
    }
}
