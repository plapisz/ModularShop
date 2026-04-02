using System.Net;
using System.Net.Http.Json;
using ModularShop.Modules.Catalog.Core.Dtos;
using ModularShop.Modules.Catalog.IntegrationTests.Common;
using ModularShop.Modules.Catalog.IntegrationTests.Fixtures;
using Shouldly;

namespace ModularShop.Modules.Catalog.IntegrationTests.Tests;

public sealed class UpdateProductTests(CatalogWebApplicationFactory factory) : CatalogIntegrationTest(factory)
{
    [Fact]
    public async Task Update_ExistingProduct_Returns204AndDataIsPersisted()
    {
        // Arrange
        var seededProduct = await SeedProductAsync();
        var dto = ProductFixture.ValidUpdateProductDto();

        // Act
        var updateResponse = await WriteClient.PutAsJsonAsync($"/api/catalog/products/{seededProduct.Id}", dto);

        // Assert
        updateResponse.StatusCode.ShouldBe(HttpStatusCode.NoContent);

        var getResponse = await ReadClient.GetAsync($"/api/catalog/products/{seededProduct.Id}");

        var product = await getResponse.Content.ReadFromJsonAsync<ProductDto>();
        product.ShouldNotBeNull();
        product.Name.ShouldBe(dto.Name);
        product.Description.ShouldBe(dto.Description);
        product.Price.ShouldBe(dto.Price);
        product.Sku.ShouldBe(dto.Sku);
        product.StockQuantity.ShouldBe(dto.StockQuantity);
    }

    [Fact]
    public async Task Update_NonExistentProduct_Returns400()
    {
        // Arrange
        var dto = ProductFixture.ValidUpdateProductDto();

        // Act
        var response = await WriteClient.PutAsJsonAsync($"/api/catalog/products/{Guid.NewGuid()}", dto);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Update_InvalidData_Returns400()
    {
        // Arrange
        var seededProduct = await SeedProductAsync();
        var dto = ProductFixture.InvalidUpdateProductDto();

        // Act
        var response = await WriteClient.PutAsJsonAsync($"/api/catalog/products/{seededProduct.Id}", dto);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Update_NoWritePermission_Returns403()
    {
        // Arrange
        var seededProduct = await SeedProductAsync();
        var dto = ProductFixture.ValidUpdateProductDto();

        // Act
        var response = await ReadClient.PutAsJsonAsync($"/api/catalog/products/{seededProduct.Id}", dto);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task Update_NoToken_Returns401()
    {
        // Arrange
        var seededProduct = await SeedProductAsync();
        var dto = ProductFixture.ValidUpdateProductDto();

        // Act
        var response = await AnonymousClient.PutAsJsonAsync($"/api/catalog/products/{seededProduct.Id}", dto);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}