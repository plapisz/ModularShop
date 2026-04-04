using Microsoft.Extensions.DependencyInjection;
using ModularShop.Modules.Catalog.Api.Security;
using ModularShop.Modules.Catalog.Core.Entities;
using ModularShop.Modules.Catalog.Core.Infrastructure.Persistence;
using ModularShop.Modules.Catalog.IntegrationTests.Fixtures;

namespace ModularShop.Modules.Catalog.IntegrationTests.Common;

public abstract class CatalogIntegrationTest(CatalogWebApplicationFactory factory)
    : IClassFixture<CatalogWebApplicationFactory>, IAsyncLifetime
{
    protected HttpClient WriteClient => factory.CreateClientWithPolicies(Policies.Catalog.Read, Policies.Catalog.Write);
    protected HttpClient ReadClient => factory.CreateClientWithPolicies(Policies.Catalog.Read);
    protected HttpClient AnonymousClient { get; } = factory.CreateClientWithoutAuthentication();

    public Task InitializeAsync()
        => factory.CleanDatabaseAsync();

    public Task DisposeAsync()
        => Task.CompletedTask;

    protected Task<Product> SeedProductAsync()
        => CreateSeeder().SeedProductAsync();

    protected Task<Product> SeedInactiveProductAsync()
        => CreateSeeder().SeedInactiveProductAsync();

    private CatalogDataSeeder CreateSeeder()
    {
        var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        
        return new CatalogDataSeeder(db);
    }
}