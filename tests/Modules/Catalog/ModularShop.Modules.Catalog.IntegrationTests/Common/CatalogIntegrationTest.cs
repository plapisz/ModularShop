using ModularShop.Modules.Catalog.Api.Security;

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
}