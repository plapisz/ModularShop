using ModularShop.Modules.Catalog.Core.Dtos;
using ModularShop.Modules.Catalog.Core.Entities;
using ModularShop.Modules.Catalog.Core.Exceptions;
using ModularShop.Modules.Catalog.Core.Repositories;

namespace ModularShop.Modules.Catalog.Core.Services;

internal sealed class InMemoryProductService(IProductRepository repository) : IProductService
{
    public async Task<Guid> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Sku = dto.Sku,
            StockQuantity = dto.StockQuantity,
            IsActive = true,
        };

        await repository.AddAsync(product);
        
        return product.Id;
    }

    public async Task<ProductDto?> GetAsync(Guid id)
    {
        var product = await repository.GetAsync(id);
        
        return product is null || !product.IsActive 
            ? null 
            : MapToDto(product);
    }

    public async Task<IReadOnlyCollection<ProductDto>> BrowseAsync()
    {
        var products = await repository.BrowseAsync();

        return products
            .Where(x => x.IsActive)
            .Select(MapToDto)
            .ToList();
    }

    public async Task UpdateAsync(UpdateProductDto dto)
    {
        var product = await repository.GetAsync(dto.Id);
        if (product is null)
        {
            throw new ProductNotFoundException(dto.Id);
        }

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.Sku = dto.Sku;
        product.StockQuantity = dto.StockQuantity;

        await repository.UpdateAsync(product);
    }

    public async Task DeactivateAsync(Guid id)
    {
        var product = await repository.GetAsync(id);
        if (product is null)
        {
            throw new ProductNotFoundException(id);
        }

        product.IsActive = false;

        await repository.UpdateAsync(product);
    }
    
    private static ProductDto MapToDto(Product product)
        => new()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Sku = product.Sku,
            StockQuantity = product.StockQuantity
        };
}