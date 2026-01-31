using ModularShop.Modules.Catalog.Core.Dtos;

namespace ModularShop.Modules.Catalog.Core.Services;

public interface IProductService
{
    Task<Guid> CreateAsync(CreateProductDto dto);
    Task<ProductDto?> GetAsync(Guid id);
    Task<IReadOnlyCollection<ProductDto>> BrowseAsync();
    Task UpdateAsync(UpdateProductDto dto);
    Task DeactivateAsync(Guid id);
}