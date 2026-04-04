using System.Net;
using System.Net.Http.Json;
using ModularShop.Modules.Catalog.Core.Dtos;
using ModularShop.Modules.Catalog.IntegrationTests.Common;
using Shouldly;

namespace ModularShop.Modules.Catalog.IntegrationTests.Tests;

public sealed class GetProductTests(CatalogWebApplicationFactory factory) : CatalogIntegrationTest(factory)
{
    [Fact]
    public async Task Get_ExistingActiveProduct_Returns200WithCorrectData()
    {
        // Arrange
        var seededProduct = await SeedProductAsync();

        // Act
        var response = await ReadClient.GetAsync($"/api/catalog/products/{seededProduct.Id}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var dto = await response.Content.ReadFromJsonAsync<ProductDto>();
        dto.ShouldNotBeNull();
        dto.Id.ShouldBe(seededProduct.Id);
        dto.Name.ShouldBe(seededProduct.Name);
        dto.Description.ShouldBe(seededProduct.Description);
        dto.Price.ShouldBe(seededProduct.Price);
        dto.Sku.ShouldBe(seededProduct.Sku);
        dto.StockQuantity.ShouldBe(seededProduct.StockQuantity);
    }

    [Fact]
    public async Task Get_InactiveProduct_Returns404()
    {
        // Arrange
        var seededProduct = await SeedInactiveProductAsync();

        // Act
        var response = await ReadClient.GetAsync($"/api/catalog/products/{seededProduct.Id}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Get_NonExistentProduct_Returns404()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();
        
        // Act
        var response = await ReadClient.GetAsync($"/api/catalog/products/{nonExistentId}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Get_NoToken_Returns401()
    {
        // Arrange
        var seededProduct = await SeedProductAsync();

        // Act
        var response = await AnonymousClient.GetAsync($"/api/catalog/products/{seededProduct.Id}");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}