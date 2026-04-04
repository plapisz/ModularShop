using System.Net;
using System.Net.Http.Json;
using ModularShop.Modules.Catalog.Core.Dtos;
using ModularShop.Modules.Catalog.IntegrationTests.Common;
using Shouldly;

namespace ModularShop.Modules.Catalog.IntegrationTests.Tests;

public sealed class BrowseProductsTests(CatalogWebApplicationFactory factory) : CatalogIntegrationTest(factory)
{
    [Fact]
    public async Task Browse_ExistentProducts_Returns200WithActiveProducts()
    {
        // Arrange
        var activeProduct1 = await SeedProductAsync();
        var activeProduct2 = await SeedProductAsync();
        var inactiveProduct = await SeedInactiveProductAsync();

        // Act
        var response = await ReadClient.GetAsync("/api/catalog/products");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
        products.ShouldNotBeNull();
        products.Count.ShouldBe(2);
        products.ShouldContain(p => p.Id == activeProduct1.Id);
        products.ShouldContain(p => p.Id == activeProduct2.Id);
        products.ShouldNotContain(p => p.Id == inactiveProduct.Id);
    }

    [Fact]
    public async Task Browse_NoActiveProducts_Returns200WithEmptyList()
    {
        // Arrange
        await SeedInactiveProductAsync();
        
        // Act
        var response = await ReadClient.GetAsync("/api/catalog/products");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
        products.ShouldNotBeNull();
        products.Count.ShouldBe(0);
    }

    [Fact]
    public async Task Browse_NoToken_Returns401()
    {
        // Arrange
        await SeedProductAsync();
        
        // Act
        var response = await AnonymousClient.GetAsync("/api/catalog/products");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}