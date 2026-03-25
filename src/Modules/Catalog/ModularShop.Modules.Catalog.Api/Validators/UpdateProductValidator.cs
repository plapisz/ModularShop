using FluentValidation;
using ModularShop.Modules.Catalog.Core.Dtos;
using ModularShop.Modules.Catalog.Core.Repositories;

namespace ModularShop.Modules.Catalog.Api.Validators;

internal sealed class UpdateProductValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductValidator(IProductRepository productRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(2000);

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .PrecisionScale(18, 2, ignoreTrailingZeros: true);

        RuleFor(x => x.Sku)
            .NotEmpty()
            .MaximumLength(100)
            .MustAsync(async (dto, sku, cancellationToken) => 
                !await productRepository.ExistsBySkuAsync(sku, dto.Id, cancellationToken))
            .WithMessage("Product with SKU '{PropertyValue}' already exists.");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0);
    }
}