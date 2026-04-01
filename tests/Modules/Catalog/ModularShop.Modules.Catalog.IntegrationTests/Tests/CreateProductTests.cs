using System.Net;
using System.Net.Http.Json;
using ModularShop.Modules.Catalog.Core.Dtos;
using ModularShop.Modules.Catalog.IntegrationTests.Common;
using ModularShop.Modules.Catalog.IntegrationTests.Fixtures;
using Shouldly;

namespace ModularShop.Modules.Catalog.IntegrationTests.Tests;

public sealed class CreateProductTests(CatalogWebApplicationFactory factory) : CatalogIntegrationTest(factory)
{
    [Fact]
    public async Task Create_ValidData_Returns201AndProductIsPersisted()
    {
        // Arrange
        var dto = ProductFixture.ValidCreateProductDto();

        // Act
        var createResponse = await WriteClient.PostAsJsonAsync("/api/catalog/products", dto);

        // Assert
        createResponse.StatusCode.ShouldBe(HttpStatusCode.Created);
        createResponse.Headers.Location.ShouldNotBeNull();

        var getResponse = await ReadClient.GetAsync(createResponse.Headers.Location.ToString());

        getResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

        var product = await getResponse.Content.ReadFromJsonAsync<ProductDto>();
        product.ShouldNotBeNull();
        product.Name.ShouldBe(dto.Name);
        product.Description.ShouldBe(dto.Description);
        product.Price.ShouldBe(dto.Price);
        product.Sku.ShouldBe(dto.Sku);
        product.StockQuantity.ShouldBe(dto.StockQuantity);
    }

    [Fact]
    public async Task Create_InvalidData_Returns400()
    {
        // Arrange
        var dto = ProductFixture.InvalidCreateProductDto();

        // Act
        var response = await WriteClient.PostAsJsonAsync("/api/catalog/products", dto);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Create_NoWritePermission_Returns403()
    {
        // Arrange
        var dto = ProductFixture.ValidCreateProductDto();

        // Act
        var response = await ReadClient.PostAsJsonAsync("/api/catalog/products", dto);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task Create_NoToken_Returns401()
    {
        // Arrange
        var dto = ProductFixture.ValidCreateProductDto();
        
        // Act
        var response = await AnonymousClient.PostAsJsonAsync("/api/catalog/products", dto);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}