using ModularShop.Modules.Catalog.Core.Dtos;

namespace ModularShop.Modules.Catalog.Core.Services;

public interface IProductService
{
    Task<Guid> CreateAsync(CreateProductDto dto, CancellationToken cancellationToken);
    Task<ProductDto?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<ProductDto>> BrowseAsync(CancellationToken cancellationToken);
    Task UpdateAsync(UpdateProductDto dto, CancellationToken cancellationToken);
    Task DeactivateAsync(Guid id, CancellationToken cancellationToken);
}