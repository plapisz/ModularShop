using FluentValidation;
using ModularShop.Modules.Catalog.Core.Dtos;

namespace ModularShop.Modules.Catalog.Api.Validators;

internal sealed class UpdateProductValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductValidator()
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
            .MaximumLength(100);

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0);
    }
}