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

    protected async Task<Product> SeedProductAsync()
    {
        using var scope = factory.Services.CreateScope();
    
        var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        var seeder = new CatalogDataSeeder(dbContext);
    
        return await seeder.SeedProductAsync();
    }
}