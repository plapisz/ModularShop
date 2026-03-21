using ModularShop.Modules.Catalog.Core.Dtos;
using ModularShop.Modules.Catalog.Core.Entities;
using ModularShop.Modules.Catalog.Core.Events;
using ModularShop.Modules.Catalog.Core.Exceptions;
using ModularShop.Modules.Catalog.Core.Repositories;
using ModularShop.Shared.Abstractions.Events;

namespace ModularShop.Modules.Catalog.Core.Services;

internal sealed class ProductService(IProductRepository repository, IEventPublisher eventPublisher) : IProductService
{
    public async Task<Guid> CreateAsync(CreateProductDto dto, CancellationToken cancellationToken)
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

        await repository.AddAsync(product, cancellationToken);
        await eventPublisher.PublishAsync(new ProductCreatedEvent(product.Id, product.Name, product.Price), cancellationToken);
        
        return product.Id;
    }

    public async Task<ProductDto?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await repository.GetAsync(id, cancellationToken);
        
        return product is null || !product.IsActive 
            ? null 
            : MapToDto(product);
    }

    public async Task<IReadOnlyCollection<ProductDto>> BrowseAsync(CancellationToken cancellationToken)
    {
        var products = await repository.BrowseAsync(cancellationToken);

        return products
            .Where(x => x.IsActive)
            .Select(MapToDto)
            .ToList();
    }

    public async Task UpdateAsync(UpdateProductDto dto, CancellationToken cancellationToken)
    {
        var product = await repository.GetAsync(dto.Id, cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException(dto.Id);
        }

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.Sku = dto.Sku;
        product.StockQuantity = dto.StockQuantity;

        await repository.UpdateAsync(product, cancellationToken);
        await eventPublisher.PublishAsync(new ProductUpdatedEvent(product.Id, product.Name, product.Price), cancellationToken);
    }

    public async Task DeactivateAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await repository.GetAsync(id, cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException(id);
        }

        product.IsActive = false;

        await repository.UpdateAsync(product, cancellationToken);
        await eventPublisher.PublishAsync(new ProductDeactivatedEvent(product.Id), cancellationToken);
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