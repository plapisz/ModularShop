using System.Net;
using System.Net.Http.Json;
using ModularShop.Modules.Catalog.Core.Dtos;
using ModularShop.Modules.Catalog.IntegrationTests.Common;
using Shouldly;

namespace ModularShop.Modules.Catalog.IntegrationTests.Tests;

public sealed class DeactivateProductTests(CatalogWebApplicationFactory factory) : CatalogIntegrationTest(factory)
{
    [Fact]
    public async Task Deactivate_ExistingProduct_Returns204AndProductDisappearsFromGet()
    {
        // Arrange
        var seededProduct = await SeedProductAsync();

        // Act
        var deactivateResponse = await WriteClient.PutAsync($"/api/catalog/products/{seededProduct.Id}/deactivate", null);

        // Assert
        deactivateResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent);

        var getResponse = await ReadClient.GetAsync($"/api/catalog/products/{seededProduct.Id}");
        getResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Deactivate_ExistingProduct_Returns204AndProductDisappearsFromBrowse()
    {
        // Arrange
        var seededProduct = await SeedProductAsync();

        // Act
        var deactivateResponse = await WriteClient.PutAsync($"/api/catalog/products/{seededProduct.Id}/deactivate", null);

        // Assert
        deactivateResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent);

        var browseResponse = await ReadClient.GetAsync("/api/catalog/products");
        var products = await browseResponse.Content.ReadFromJsonAsync<List<ProductDto>>();
        products.ShouldNotBeNull();
        products.ShouldNotContain(p => p.Id == seededProduct.Id);
    }
    
    [Fact]
    public async Task Deactivate_NonExistentProduct_Returns400()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var response = await WriteClient.PutAsync($"/api/catalog/products/{nonExistentId}/deactivate", null);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Deactivate_NoWritePermission_Returns403()
    {
        // Arrange
        var seededProduct = await SeedProductAsync();

        // Act
        var response = await ReadClient.PutAsync($"/api/catalog/products/{seededProduct.Id}/deactivate", null);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task Deactivate_NoToken_Returns401()
    {
        // Arrange
        var seededProduct = await SeedProductAsync();

        // Act
        var response = await AnonymousClient.PutAsync($"/api/catalog/products/{seededProduct.Id}/deactivate", null);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}